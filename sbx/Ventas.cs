using ExternalServices.FacturacionElectronica.Factus.Facturas;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.Auth;
using sbx.core.Entities.FacturaEletronica;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.FacturacionElectronica;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.Venta;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class Ventas : Form
    {
        private dynamic? _Permisos;
        private readonly IVenta _IVenta;
        private readonly IServiceProvider _serviceProvider;
        private DetalleVenta? _DetalleVenta;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        private readonly IAuthService _IAuthService;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly IFacturas _IFacturas;
        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;
        bool FacturaElectronica = false;

        public Ventas(IVenta venta, IServiceProvider serviceProvider, ITienda tienda,
            IParametros iParametros, IAuthService iAuthService, 
            IRangoNumeracion rangoNumeracion, IFacturas facturas)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
            _ITienda = tienda;
            _IParametros = iParametros;
            _IAuthService = iAuthService;
            _IRangoNumeracion = rangoNumeracion;
            _IFacturas = facturas;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            cbx_client_venta.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "ventas":
                            btn_imprimir.Enabled = item.ToCreate == 1 ? true : false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay informacion de permisos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbx_client_producto_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();
            if (cbx_client_venta.Text == "Factura")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo" });
            }
            else if (cbx_client_venta.Text == "Cliente")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Num Doc" });
            }
            else if (cbx_client_venta.Text == "Producto")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            }
            else if (cbx_client_venta.Text == "Usuario")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id" });
            }
            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaProductos();
        }

        private async Task ConsultaProductos()
        {
            errorProvider1.Clear();

            if (cbx_campo_filtro.Text == "Id")
            {
                bool esNumerico = int.TryParse(txt_buscar.Text, out int valor);
                if (!esNumerico)
                {
                    errorProvider1.SetError(txt_buscar, "Ingrese un valor numerico");
                    return;
                }
            }

            var resp = await _IVenta.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_venta.Text, dtp_fecha_inicio.Value, dtp_fecha_fin.Value, Convert.ToInt32(_Permisos?[0]?.IdUser), _Permisos?[0]?.NameRole);

            dtg_ventas.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    decimal Subtotal = 0;
                    decimal cantidadTotal = 0;
                    decimal DescuentoLinea;
                    decimal Descuento = 0;
                    decimal Impuesto = 0;
                    decimal ImpuestoLinea;
                    decimal SubtotalLinea;
                    decimal Total = 0;
                    decimal TotalLinea;

                    foreach (var item in resp.Data)
                    {
                        if (item.Estado == "FACTURADA") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                        if (item.Estado == "FACTURADA") { Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad); }
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        if (item.Estado == "FACTURADA") { Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento)); }
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        if (item.Estado == "FACTURADA") { Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto)); }
                        ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;
                        TotalLinea = (SubtotalLinea - DescuentoLinea);

                        int rowIndex = dtg_ventas.Rows.Add(
                            item.FechaFactura,
                            item.IdVenta,
                            item.NumberFacturaDIAN != "" ? item.NumberFacturaDIAN : item.Factura,
                            item.EstadoFacturaDIAN != "" ? item.Estado == "FACTURADA" ? item.EstadoFacturaDIAN : item.Estado : item.Estado,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.NombreProducto,
                            item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                            TotalLinea.ToString("N2", new CultureInfo("es-CO")),
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.IdUserActionFactura + " - " + item.UserNameFactura);

                        var celdaEstado = dtg_ventas.Rows[rowIndex].Cells[3];
                        if (celdaEstado.Value.ToString() == "PENDIENTE EMITIR")
                        {
                            celdaEstado.Style.BackColor = Color.LightSalmon;
                            //celdaEstado.Style.ForeColor = Color.White;
                        }
                    }

                    Total = (Subtotal - Descuento);
                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;
                    //Total += (Subtotal - Descuento) + Impuesto;

                    lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                    lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                    lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    lbl_cantidadProductos.Text = "_";
                    lbl_subtotal.Text = "_";
                    lbl_descuento.Text = "_";
                    lbl_impuesto.Text = "_";
                    lbl_total.Text = "_";

                    MessageBox.Show("No se encuentran datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se encuentran datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            return ValorDescuento;
        }

        private void dtg_ventas_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_ventas.Rows.Count > 0)
            {
                if (dtg_ventas.SelectedRows.Count > 0)
                {
                    if (_Permisos != null)
                    {
                        _DetalleVenta = _serviceProvider.GetRequiredService<DetalleVenta>();
                        _DetalleVenta.Permisos = _Permisos;
                        foreach (DataGridViewRow rows in dtg_ventas.SelectedRows)
                        {
                            _DetalleVenta.Id_Venta = Convert.ToInt32(rows.Cells["cl_id_venta"].Value);
                        }
                        _DetalleVenta.FormClosed += (s, args) => _DetalleVenta = null;
                        _DetalleVenta.ShowDialog();
                    }
                }
            }
        }

        private async void btn_imprimir_Click(object sender, EventArgs e)
        {
            FacturaElectronica = false;

            if (dtg_ventas.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de imprimir la factura?",
                        "Confirmar cancelacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_ventas.SelectedRows.Count > 0)
                    {
                        var row = dtg_ventas.SelectedRows[0];
                        if (row.Cells["cl_id_venta"].Value != null)
                        {
                            var Id_venta = Convert.ToInt32(row.Cells["cl_id_venta"].Value);
                            //Imprime Tirilla
                            var DataTienda = await _ITienda.List();
                            if (DataTienda.Data != null)
                            {
                                if (DataTienda.Data.Count > 0)
                                {
                                    var DataVenta = await _IVenta.List(Id_venta);

                                    FacturaPOSEntitie DataFactura = new FacturaPOSEntitie();

                                    DataFactura.NumeroFactura = DataVenta.Data![0].NumberFacturaDIAN == "" ? DataVenta.Data![0].Factura : DataVenta.Data![0].NumberFacturaDIAN;
                                    DataFactura.Fecha = DataVenta.Data[0].FechaFactura;
                                    DataFactura.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                    DataFactura.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                    DataFactura.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                    DataFactura.NIT = DataTienda.Data[0].NumeroDocumento;
                                    DataFactura.UserNameFactura = DataVenta.Data[0].IdUserActionFactura + " - " + DataVenta.Data[0].UserNameFactura;
                                    DataFactura.NombreCliente = DataVenta.Data[0].NumeroDocumento + " - " + DataVenta.Data[0].NombreRazonSocial;
                                    DataFactura.NombreVendedor = DataVenta.Data[0].NumeroDocumentoVendedor + " - " + DataVenta.Data[0].NombreVendedor;
                                    DataFactura.Estado = DataVenta.Data[0].EstadoFacturaDIAN == "" ? DataVenta.Data[0].Estado : DataVenta.Data[0].EstadoFacturaDIAN;
                                    DataFactura.FormaPago = DataVenta.Data[0].NombreMetodoPago;
                                    DataFactura.Recibido = DataVenta.Data[0].Recibido;

                                    Cantidad = 0;
                                    Subtotal = 0;
                                    Descuento = 0;
                                    Impuesto = 0;
                                    SubtotalLinea = 0;
                                    DescuentoLinea = 0;

                                    foreach (var item in DataVenta.Data)
                                    {
                                        Cantidad += Convert.ToDecimal(item.Cantidad);
                                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                    }

                                    Total = (Subtotal - Descuento);
                                    //Total = (Subtotal - Descuento) + Impuesto;
                                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                                    DataFactura.CantidadTotal = Cantidad;
                                    DataFactura.Subtotal = SubtotalMenosImpuesto;
                                    DataFactura.Descuento = Descuento;
                                    DataFactura.Impuesto = Impuesto;
                                    DataFactura.Total = Total;
                                    DataFactura.Cambio = DataFactura.Recibido - Total;

                                    List<ItemFacturaEntitie> ListItemFacturaEntitie = new List<ItemFacturaEntitie>();

                                    decimal precio;
                                    decimal cantidad;
                                    decimal desc;
                                    decimal iva;
                                    decimal totalLinea;
                                    decimal total;

                                    foreach (var item in DataVenta.Data)
                                    {
                                        precio = Convert.ToDecimal(item.PrecioUnitario);
                                        cantidad = Convert.ToDecimal(item.Cantidad);
                                        desc = Convert.ToDecimal(item.Descuento);
                                        iva = Convert.ToDecimal(item.Impuesto);

                                        total = CalcularTotal(precio, 0, desc);
                                        total = total * cantidad;

                                        string UnidadMedidaAbreviada;

                                        switch (item.UnidadMedida)
                                        {
                                            case "Unidad (und)":
                                                UnidadMedidaAbreviada = "Unidad (und)";
                                                break;
                                            case "Caja (caja)":
                                                UnidadMedidaAbreviada = "caja";
                                                break;
                                            case "Paquete (paq)":
                                                UnidadMedidaAbreviada = "paq";
                                                break;
                                            case "Bolsa (bol)":
                                                UnidadMedidaAbreviada = "bol";
                                                break;
                                            case "Litro (lt)":
                                                UnidadMedidaAbreviada = "lt";
                                                break;
                                            case "Mililitro (ml)":
                                                UnidadMedidaAbreviada = "ml";
                                                break;
                                            case "Kilogramo (kg)":
                                                UnidadMedidaAbreviada = "kg";
                                                break;
                                            case "Gramo (g)":
                                                UnidadMedidaAbreviada = "g";
                                                break;
                                            case "Metro (m)":
                                                UnidadMedidaAbreviada = "m";
                                                break;
                                            case "Par (par)":
                                                UnidadMedidaAbreviada = "par";
                                                break;
                                            default:
                                                UnidadMedidaAbreviada = "";
                                                break;
                                        }

                                        var ItemFactura = new ItemFacturaEntitie
                                        {
                                            Codigo = item.IdProducto,
                                            Descripcion = item.NombreProducto,
                                            Cantidad = item.Cantidad,
                                            UnidadMedida = UnidadMedidaAbreviada,
                                            PrecioUnitario = item.PrecioUnitario,
                                            Descuento = item.Descuento,
                                            Impuesto = item.Impuesto,
                                            Total = total
                                        };

                                        ListItemFacturaEntitie.Add(ItemFactura);
                                    }

                                    DataFactura.Items = ListItemFacturaEntitie;

                                    var DataParametros = await _IParametros.List("");

                                    if (DataParametros.Data != null)
                                    {
                                        if (DataParametros.Data.Count > 0)
                                        {
                                            int ANCHO_TIRILLA = 0;
                                            string Impresora = "";
                                            string MensajeFinalTirilla = "";
                                            int LineasAbajo = 0;
                                            foreach (var itemParametros in DataParametros.Data)
                                            {
                                                switch (itemParametros.Nombre)
                                                {
                                                    case "Ancho tirilla":
                                                        ANCHO_TIRILLA = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    case "Impresora":
                                                        Impresora = itemParametros.Value;
                                                        break;
                                                    case "Mensaje final tirilla":
                                                        MensajeFinalTirilla = itemParametros.Value;
                                                        break;
                                                    case "lineas abajo de la tirilla":
                                                        LineasAbajo = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            if (DataVenta.Data![0].NumberFacturaDIAN != "") { FacturaElectronica = true; }

                                            DataFactura.FacturaElectronica = FacturaElectronica;
                                            DataFactura.FacturaJSON = !string.IsNullOrEmpty(DataVenta.Data![0].FacturaJSON) ? DataVenta.Data![0].FacturaJSON : "";

                                            StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaFactura(DataFactura, ANCHO_TIRILLA, MensajeFinalTirilla, false);

                                            string carpetaFacturas = "Facturas";
                                            if (!Directory.Exists(carpetaFacturas))
                                            {
                                                Directory.CreateDirectory(carpetaFacturas);
                                            }

                                            File.WriteAllText(Path.Combine(carpetaFacturas, $"factura_{DataFactura.NumeroFactura}.txt"),
                                                                      tirilla.ToString(),
                                                                      Encoding.UTF8);

                                            if (FacturaElectronica)
                                            {
                                                dynamic datosFacturaElectronica = JsonConvert.DeserializeObject<dynamic>(DataVenta.Data[0].FacturaJSON);

                                                string qr_img = datosFacturaElectronica!.data.bill.qr_image;

                                                GuardarTirillaComoImagen(tirilla, qr_img, DataFactura.NumeroFactura);

                                                RawPrinterHelper.SendStringToPrinterConQr(Impresora, tirilla.ToString(), LineasAbajo, qr_img);
                                            }
                                            else
                                            {
                                                RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString(), LineasAbajo);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProductos();
            }
        }

        private void dtg_ventas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dtg_ventas.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    dtg_ventas.ClearSelection();
                    dtg_ventas.Rows[hit.RowIndex].Selected = true;

                    dtg_ventas.CurrentCell = dtg_ventas.Rows[hit.RowIndex].Cells[0];

                    var row = dtg_ventas.SelectedRows[0];
                    if (row.Cells["cl_estado"].Value.ToString() == "PENDIENTE EMITIR") 
                    {
                        contextMenuStrip1.Show(dtg_ventas, e.Location);
                    }
                }
            }
        }

        private async void MEmitir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;
            try
            {
                var respDoc = await _IRangoNumeracion.IdentificaDocumento(21);

                if (respDoc.Data != null)
                {
                    if (respDoc.Data.Count > 0)
                    {
                        long NumDesde = respDoc.Data[0].NumeroDesde;
                        long NumHasta = respDoc.Data[0].NumeroHasta;
                        long Actual = respDoc.Data[0].ConsecutivoActual;
                        int IdRangoDIAN = respDoc.Data[0].IdRangoDIAN;

                        var row = dtg_ventas.SelectedRows[0];
                        int IdVenta = Convert.ToInt32(row.Cells["cl_id_venta"].Value);
                        if (IdVenta > 0)
                        {
                            if (Actual <= NumHasta)
                            {
                                var DataFacturaRegistrada = new Response<dynamic>();
                                DataFacturaRegistrada = await _IVenta.List(IdVenta);

                                try
                                {
                                    AuthEntitie authEntitie = new AuthEntitie
                                    {
                                        url_api = ConfigurationManager.AppSettings["UrlAuthPOST"]!,
                                        grant_type = ConfigurationManager.AppSettings["grant_typeAuth"]!,
                                        client_id = ConfigurationManager.AppSettings["client_id"]!,
                                        client_secret = ConfigurationManager.AppSettings["client_secret"]!,
                                        username = ConfigurationManager.AppSettings["username"]!,
                                        Passwords = ConfigurationManager.AppSettings["password"]!
                                    };

                                    if (!string.IsNullOrEmpty(authEntitie.url_api) && !string.IsNullOrEmpty(authEntitie.grant_type)
                                         && !string.IsNullOrEmpty(authEntitie.client_id) && !string.IsNullOrEmpty(authEntitie.client_secret)
                                         && !string.IsNullOrEmpty(authEntitie.username) && !string.IsNullOrEmpty(authEntitie.Passwords))
                                    {
                                        var RespAuth = _IAuthService.Autenticacion(authEntitie);

                                        if (RespAuth.Data != null)
                                        {
                                            if (RespAuth.Flag && RespAuth.Data.access_token != "")
                                            {
                                                string Token = RespAuth.Data.access_token.ToString();
                                                string UrlCrearValidarFactura = ConfigurationManager.AppSettings["UrlCrearValidarFacturaPOST"]!;

                                                if (DataFacturaRegistrada.Data != null)
                                                {
                                                    if (DataFacturaRegistrada.Data.Count > 0)
                                                    {
                                                        Customer customer = new Customer();
                                                        int TipoIdentificacion = Convert.ToInt32(DataFacturaRegistrada.Data[0].IdIdentificationType);
                                                        customer.identification_document_id =
                                                            (TipoIdentificacion == 1 ? 3 : //Cédula de ciudadanía
                                                            TipoIdentificacion == 2 ? 5 : //Cédula de extranjería
                                                            TipoIdentificacion == 3 ? 3 : //RUT de momento se maneja con Cédula de ciudadanía
                                                            TipoIdentificacion == 4 ? 6 : 3).ToString(); //NIT

                                                        customer.identification = DataFacturaRegistrada.Data[0].NumeroDocumento.ToString();
                                                        if (TipoIdentificacion == 4) //NIT
                                                        {
                                                            customer.company = DataFacturaRegistrada.Data[0].NombreRazonSocial.ToString();
                                                            customer.trade_name = "";
                                                        }
                                                        else
                                                        {
                                                            customer.names = DataFacturaRegistrada.Data[0].NombreRazonSocial.ToString();
                                                        }
                                                        customer.address = DataFacturaRegistrada.Data[0].Direccion.ToString();
                                                        customer.email = DataFacturaRegistrada.Data[0].Email.ToString();
                                                        customer.phone = DataFacturaRegistrada.Data[0].Telefono.ToString();
                                                        customer.legal_organization_id = (TipoIdentificacion == 4 ? 1 : 2).ToString(); // 1. Juridico, 2. Natural
                                                        customer.tribute_id = "21"; // 18. IVA, 21. No aplica *
                                                        customer.municipality_id = "1079"; //Cali, valle del cauca

                                                        List<WithholdingTax> ListwithholdingTax = new List<WithholdingTax>();
                                                        List<Item> Listitems = new List<Item>();
                                                        foreach (var ItemsVenta in DataFacturaRegistrada.Data)
                                                        {
                                                            //WithholdingTax withholdingTax = new WithholdingTax
                                                            //{
                                                            //    Code = "01",
                                                            //    WithholdingTaxRate = Convert.ToDecimal(ItemsVenta.Impuesto, new CultureInfo("es-CO"))
                                                            //};

                                                            //ListwithholdingTax.Add(withholdingTax);

                                                            Item item = new Item
                                                            {
                                                                code_reference = ItemsVenta.IdProducto.ToString(),
                                                                name = ItemsVenta.NombreProducto.ToString(),
                                                                quantity = Convert.ToDecimal(ItemsVenta.Cantidad, new CultureInfo("es-CO")),
                                                                discount_rate = Convert.ToDecimal(ItemsVenta.Descuento, new CultureInfo("es-CO")),
                                                                price = Convert.ToDecimal(ItemsVenta.PrecioUnitario, new CultureInfo("es-CO")),
                                                                tax_rate = Convert.ToDecimal(ItemsVenta.Impuesto, new CultureInfo("es-CO")),
                                                                unit_measure_id =
                                                                (ItemsVenta.IdUnidadMedida == 1 ? 70 : //Unidad
                                                                ItemsVenta.IdUnidadMedida == 7 ? 414 : //kilogramo
                                                                ItemsVenta.IdUnidadMedida == 11 ? 449 : //libra
                                                                ItemsVenta.IdUnidadMedida == 9 ? 512 : //metro
                                                                ItemsVenta.IdUnidadMedida == 12 ? 874 : //galón
                                                                70), //En cualquier otro caso Unidad
                                                                standard_code_id = 1, //Estándar de adopción del contribuyente
                                                                is_excluded = 0, // excluido de IVA (0: no, 1: sí).
                                                                tribute_id = 1, //Impuesto sobre la Ventas
                                                                                //WithholdingTaxes = ListwithholdingTax
                                                            };

                                                            Listitems.Add(item);
                                                        }

                                                        FacturaRequest facturaRequest = new FacturaRequest
                                                        {
                                                            numbering_range_id = IdRangoDIAN,
                                                            reference_code = DataFacturaRegistrada.Data[0].Factura.ToString(),
                                                            observation = "",
                                                            payment_form = "1",
                                                            payment_due_date = "",
                                                            payment_method_code = "10",
                                                            //billing_period = billingPeriod,
                                                            customer = customer,
                                                            items = Listitems
                                                        };

                                                        ActualizarFacturaForFacturaElectronicaEntitie actualizarFacturaForFacturaElectronicaEntitie
                                                        = new ActualizarFacturaForFacturaElectronicaEntitie();

                                                        var responseFacturaElectronica = _IFacturas.CreaValidaFactura(Token, UrlCrearValidarFactura, facturaRequest);

                                                        if (!responseFacturaElectronica.Flag)
                                                        {
                                                            MessageBox.Show($"Error en Emicion de factura electronica: {responseFacturaElectronica?.Data} - {responseFacturaElectronica?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }
                                                        else
                                                        {
                                                            if (responseFacturaElectronica.Data != null)
                                                            {
                                                                actualizarFacturaForFacturaElectronicaEntitie.IdVenta = Convert.ToInt32(DataFacturaRegistrada.Data[0].IdVenta);
                                                                actualizarFacturaForFacturaElectronicaEntitie.NumberFacturaDIAN = responseFacturaElectronica.Data!.data.bill.number;
                                                                actualizarFacturaForFacturaElectronicaEntitie.EstadoFacturaDIAN = "EMITIDA";
                                                                actualizarFacturaForFacturaElectronicaEntitie.FacturaJSON = responseFacturaElectronica.Data.ToString();
                                                                actualizarFacturaForFacturaElectronicaEntitie.qr_image = responseFacturaElectronica.Data!.data.bill.qr_image;

                                                                //Actualizar informacion de facturacion electronica en base de datos
                                                                var respActualizaDataFacturaElectronica = await _IVenta.ActualizarDataFacturaElectronica(actualizarFacturaForFacturaElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                if (!respActualizaDataFacturaElectronica.Flag)
                                                                {
                                                                    MessageBox.Show($"Se presento un error al intentar actualizar informacion de factura electronica en base de datos, Error: {respActualizaDataFacturaElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show($"No encontro informacion de factura registrada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show($"No encontro informacion de factura registrada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show($"Error en autenticacion,  no se emitira factura electronica: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Error en autenticacion, no se emitira factura electronica: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"No se encuentra informacion completa de Url Apis, no se emitira factura electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Se presento una excepcion debido a esto se registrara factura en estado pendiente de Emitir, Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show($@"Ha excedido el rango autorizado de numeración (de {NumDesde} a {NumHasta}).
                                                      Por favor, solicite una nueva resolución de numeración para continuar con la emisión de facturas.",
                                                  "Límite de facturación alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
            }
        }

        public static void GuardarTirillaComoImagen(StringBuilder tirilla, string base64QR, string numeroFactura)
        {
            // Configuración de la "impresora"
            int anchoPx = 384; // 58mm típico
            int altoEstimado = 2000; // Estimado; puedes ajustar según necesidad

            Bitmap bmp = new Bitmap(anchoPx, altoEstimado);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                Font fuente = new Font("Consolas", 10);
                Brush pincel = Brushes.Black;
                float y = 0;

                // Dibujar texto línea por línea
                foreach (string linea in tirilla.ToString().Split('\n'))
                {
                    g.DrawString(linea.TrimEnd(), fuente, pincel, new PointF(0, y));
                    y += fuente.GetHeight();
                }

                // Insertar QR (si viene)
                if (!string.IsNullOrWhiteSpace(base64QR))
                {
                    try
                    {
                        string base64QRLimpia = base64QR
                        .Replace("data:image/png;base64,", "")
                        .Replace("\r", "")
                        .Replace("\n", "")
                        .Trim();

                        byte[] qrBytes = Convert.FromBase64String(base64QRLimpia);
                        using (MemoryStream ms = new MemoryStream(qrBytes))
                        using (Image qrImage = Image.FromStream(ms))
                        {
                            int qrSize = 100; // Tamaño del QR en px
                            g.DrawImage(qrImage, new Rectangle((anchoPx - qrSize) / 2, (int)y + 10, qrSize, qrSize));
                            y += qrSize + 20; // Dejar espacio abajo
                        }
                    }
                    catch
                    {
                        g.DrawString("[Error al cargar QR]", fuente, Brushes.Red, new PointF(0, y));
                    }
                }
            }

            // Recortar la imagen al alto usado
            Rectangle crop = new Rectangle(0, 0, anchoPx, (int)Math.Ceiling((double)altoEstimado));
            Bitmap tirillaFinal = bmp.Clone(crop, bmp.PixelFormat);

            // Guardar como imagen
            string carpeta = "Facturas";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, $"factura_{numeroFactura}.png");
            tirillaFinal.Save(ruta, ImageFormat.Png);
        }
    }
}
