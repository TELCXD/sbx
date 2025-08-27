using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sbx.core.Entities.Auth;
using sbx.core.Entities.NotaCredito;
using sbx.core.Entities.NotaCreditoElectronica;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.FacturacionElectronica;
using sbx.core.Interfaces.NotaCredito;
using sbx.core.Interfaces.NotaCreditoElectronica;
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
    public partial class AgregarNotaCredito : Form
    {
        private readonly IVenta _IVenta;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly INotaCredito _INotaCredito;
        private readonly IAuthService _IAuthService;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly INotasCreditoElectronica _INotasCreditoElectronica;
        private readonly IRangoNumeracionFE _IRangoNumeracionFE;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        private int _Id_Nota_Credito;
        private int _Id_Id_Venta;
        int IdFactura = 0;
        private dynamic? _Permisos;
        decimal TotalParaDevolucion = 0;
        char decimalSeparator = ',';
        string FacturaJSON = "";
        int IdIdentificationType = 0;
        bool FacturaElectronica = false;
        decimal Subtotal = 0;
        decimal Cantidad = 0;
        decimal cantidadTotal = 0;
        decimal DescuentoLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal ImpuestoLinea;
        decimal SubtotalLinea;
        decimal Total = 0;
        decimal TotalLinea;
        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";

        NotaCreditoEntitie notaCreditoEntitie = new NotaCreditoEntitie();

        public AgregarNotaCredito(IVenta venta, IServiceProvider serviceProvider, INotaCredito notaCredito, 
            IAuthService authService, IRangoNumeracion iRangoNumeracion, INotasCreditoElectronica notasCreditoElectronica, 
            IRangoNumeracionFE iRangoNumeracionFE, ITienda tienda, IParametros parametros)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
            _INotaCredito = notaCredito;
            _IAuthService = authService;
            _IRangoNumeracion = iRangoNumeracion;
            _INotasCreditoElectronica = notasCreditoElectronica;
            _IRangoNumeracionFE = iRangoNumeracionFE;
            _ITienda = tienda;
            _IParametros = parametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Nota_Credito
        {
            get => _Id_Nota_Credito;
            set => _Id_Nota_Credito = value;
        }

        public int Id_Venta
        {
            get => _Id_Id_Venta;
            set => _Id_Id_Venta = value;
        }

        private async void AgregarNotaCredito_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "notaCredito":
                            btn_busca_factura.Enabled = item.ToRead == 1 ? true : false;
                            btn_devolucion.Enabled = item.ToCreate == 1 ? true : false;
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

        private async Task CargaDatosIniciales()
        {
            BuscarPor = "";
            ModoRedondeo = "N/A";
            MultiploRendondeo = "50";

            var DataParametros = await _IParametros.List("");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    foreach (var itemParametros in DataParametros.Data)
                    {
                        switch (itemParametros.Nombre)
                        {
                            case "Buscar en venta por":
                                BuscarPor = itemParametros.Value;
                                break;
                            case "Modo Redondeo":
                                ModoRedondeo = itemParametros.Value;
                                break;
                            case "Multiplo Rendondeo":
                                MultiploRendondeo = itemParametros.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            if (Id_Venta > 0) 
            {
                btn_busca_factura.Enabled = false;
                btn_devolucion.Enabled = false;
                txt_motivo_devolucion.Enabled = false;

                var resp = await _IVenta.List(Id_Venta);
                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        IdIdentificationType = Convert.ToInt32(resp.Data[0].IdIdentificationType);
                        dtg_ventas.Rows.Clear();
                        chk_marcar_todo.Checked = false;
                        txt_busca_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                        lbl_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                        if (resp.Data[0].NumberFacturaDIAN != "") { FacturaElectronica = true; } else { FacturaElectronica = false; }
                        lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                        lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                        lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                        lbl_referencia.Text = resp.Data[0].Referencia;
                        lbl_banco.Text = resp.Data[0].NombreBanco;
                        lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;
                        txt_motivo_devolucion.Text = resp.Data[0].MotivoNotaCredito.ToString();

                        decimal Subtotal = 0;
                        decimal cantidadTotal = 0;
                        decimal DescuentoLinea;
                        decimal Descuento = 0;
                        decimal Impuesto = 0;
                        decimal ImpuestoLinea;
                        decimal SubtotalLinea;
                        decimal Total = 0;
                        decimal TotalLinea;
                        decimal iva = 0;
                        decimal inc = 0;
                        decimal incBolsa = 0;

                        foreach (var item in resp.Data)
                        {
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            if (item.NombreTributo == "INC Bolsas")
                            {
                                Impuesto += Convert.ToDecimal(item.Impuesto);
                                ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                            }
                            else
                            {
                                Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                            }

                            if (item.NombreTributo == "INC Bolsas")
                            {
                                incBolsa += Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO"));
                            }
                            else if (item.NombreTributo == "IVA")
                            {
                                iva += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                            }
                            else if (item.NombreTributo == "INC")
                            {
                                inc += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                            }

                            TotalLinea = (SubtotalLinea - DescuentoLinea);
                            //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                            dtg_ventas.Rows.Add(
                                item.IdDetalleVenta,
                                false,
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.NombreProducto,
                                item.UnidadMedida,
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                                item.NombreTributo,
                                Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                                TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                        }

                        Total += (Subtotal - Descuento);
                        //Total += (Subtotal - Descuento) + Impuesto;
                        decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                        lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                        lbl_inc_bolsa.Text = incBolsa.ToString("N2", new CultureInfo("es-CO"));
                        lbl_inc.Text = inc.ToString("N2", new CultureInfo("es-CO"));
                        lbl_iva.Text = iva.ToString("N2", new CultureInfo("es-CO"));

                        chk_marcar_todo.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btn_busca_factura_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Busca_factura";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            IdFactura = id;
            var resp = await _IVenta.List(id);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    if (resp.Data[0].Estado == "ANULADA") 
                    {
                        if (resp.Data[0].IdNotaCredito > 0) 
                        {
                            MessageBox.Show($"Factura: {resp.Data[0].Factura} en estado ANULADA con Nota credito: NC-{resp.Data[0].IdNotaCredito} ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Factura: {resp.Data[0].Factura} en estado ANULADA ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }

                    FacturaJSON = resp.Data[0].FacturaJSON;
                    IdIdentificationType = Convert.ToInt32(resp.Data[0].IdIdentificationType);
                    dtg_ventas.Rows.Clear();
                    chk_marcar_todo.Checked = false;
                    txt_busca_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                    lbl_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                    if (resp.Data[0].NumberFacturaDIAN != "") { FacturaElectronica = true; } else {  FacturaElectronica = false; } 
                    lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                    lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                    lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                    lbl_referencia.Text = resp.Data[0].Referencia;
                    lbl_banco.Text = resp.Data[0].NombreBanco;
                    lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;

                    Subtotal = 0;
                    cantidadTotal = 0;
                    DescuentoLinea = 0;
                    Descuento = 0;
                    Impuesto = 0;
                    ImpuestoLinea = 0;
                    SubtotalLinea = 0;
                    Total = 0;
                    TotalLinea = 0;
                    decimal iva = 0;
                    decimal inc = 0;
                    decimal incBolsa = 0;

                    foreach (var item in resp.Data)
                    {
                        cantidadTotal += Convert.ToDecimal(item.Cantidad);
                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        if (item.NombreTributo == "INC Bolsas") 
                        {
                            Impuesto += Convert.ToDecimal(item.Impuesto);
                            ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                        }
                        else
                        {
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        }

                        if (item.NombreTributo == "INC Bolsas")
                        {
                            incBolsa += Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO"));
                        }
                        else if (item.NombreTributo == "IVA")
                        {
                            iva += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                        }
                        else if (item.NombreTributo == "INC")
                        {
                            inc += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                        }

                        TotalLinea = (SubtotalLinea - DescuentoLinea);
                        //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                        dtg_ventas.Rows.Add(
                            item.IdDetalleVenta,
                            false,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.NombreProducto,
                            item.UnidadMedida,
                            item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                            item.NombreTributo,
                            Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                            TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                    }

                    Total += (Subtotal - Descuento);
                    //Total += (Subtotal - Descuento) + Impuesto;
                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                    lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                    lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                    lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    lbl_inc_bolsa.Text = incBolsa.ToString("N2", new CultureInfo("es-CO"));
                    lbl_inc.Text = inc.ToString("N2", new CultureInfo("es-CO"));
                    lbl_iva.Text = iva.ToString("N2", new CultureInfo("es-CO"));

                    chk_marcar_todo.Checked = true;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_devolucion_parcial_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int Contador = 0;
                bool TodoProductTieneCantDevo = true;

                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) > 0)
                    {
                        Contador++;
                    }

                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) <= 0)
                    {
                        TodoProductTieneCantDevo = false;
                    }
                }

                if (Contador > 0 && TodoProductTieneCantDevo)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de realizar devolucion?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var respDoc = await _IRangoNumeracion.IdentificaDocumento(22);

                        if (respDoc.Data != null)
                        {
                            if (respDoc.Data.Count > 0)
                            {
                                long NumDesde = respDoc.Data[0].NumeroDesde;
                                long NumHasta = respDoc.Data[0].NumeroHasta;
                                long Actual = respDoc.Data[0].ConsecutivoActual;
                                string NumeroResolucion = respDoc.Data[0].NumeroResolucion.ToString();
                                string ClaveTecnica = respDoc.Data[0].ClaveTecnica.ToString();
                                int Id_RangoNumeracion = respDoc.Data[0].Id_RangoNumeracion;
                                int IdRangoDIAN = respDoc.Data[0].IdRangoDIAN;
                                int IsActive = Convert.ToInt32(respDoc.Data[0].Active);

                                bool NotaCreditoElectronica = false;
                                DateTime FechaExpedicion = DateTime.Now;
                                DateTime FechaVencimiento = DateTime.Now;
                                string Token = "";
                                bool Continuar = true;

                                if (Convert.ToInt32(respDoc.Data[0].DocElectronico) == 1) 
                                { 
                                    NotaCreditoElectronica = true;

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

                                        if (RespAuth != null)
                                        {
                                            if (RespAuth.Flag && RespAuth.Data!.access_token != "")
                                            {
                                                Token = RespAuth.Data!.access_token.ToString();
                                                string UrlRangos = ConfigurationManager.AppSettings["UrlRangosGET"]!;

                                                RangosEntitie rangosEntitie = new RangosEntitie
                                                {
                                                    Id = IdRangoDIAN,
                                                    document = 22,
                                                    resolution_number = NumeroResolucion,
                                                    technical_key = ClaveTecnica,
                                                    is_active = IsActive
                                                };

                                                var respGetRangos = _IRangoNumeracionFE.ConsultaRangoDIAN(Token, UrlRangos, rangosEntitie);

                                                if (respGetRangos != null)
                                                {
                                                    if (respGetRangos.Flag)
                                                    {
                                                        JObject dataObject = (JObject)respGetRangos.Data!.data;
                                                        JArray? dataArray = dataObject["data"] as JArray;
                                                        JObject? item = dataArray![0] as JObject;

                                                        Actual = item!.Value<long>("current");

                                                        //Actual = Convert.ToInt64(respGetRangos.Data!.data[0].current);
                                                    }
                                                    else
                                                    {
                                                        Continuar = false;
                                                        Actual = respDoc.Data[0].ConsecutivoActual;
                                                        MessageBox.Show($"Error en consulta de rango numeracion, no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }
                                                }
                                                else
                                                {
                                                    Continuar = false;
                                                    Actual = respDoc.Data[0].ConsecutivoActual;
                                                    MessageBox.Show($"Error en consulta de rango numeracion, no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                            }
                                            else
                                            {
                                                Continuar = false;
                                                Actual = respDoc.Data[0].ConsecutivoActual;
                                                MessageBox.Show($"Error en autenticacion, no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            Continuar = false;
                                            Actual = respDoc.Data[0].ConsecutivoActual;
                                            MessageBox.Show($"Error en autenticacion, no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        Continuar = false;
                                        Actual = respDoc.Data[0].ConsecutivoActual;
                                        MessageBox.Show($"No se encuentra informacion completa de Url Apis, no se emitira nota electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }

                                if (FacturaElectronica == true && NotaCreditoElectronica == false) 
                                {
                                    MessageBox.Show($"Dado que la factura a anular es electrónica, es obligatorio que la nota crédito también sea electrónica. El proceso ha sido cancelado y no se generará la nota crédito.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Continuar = false;
                                }

                                if (Continuar) 
                                {
                                    if (Actual <= NumHasta)
                                    {
                                        notaCreditoEntitie.IdVenta = IdFactura;
                                        notaCreditoEntitie.Motivo = txt_motivo_devolucion.Text;
                                        notaCreditoEntitie.Estado = "REGISTRADA";
                                        notaCreditoEntitie.Id_RangoNumeracion = Id_RangoNumeracion;
                                        notaCreditoEntitie.IdRangoDIAN = IdRangoDIAN;
                                        notaCreditoEntitie.Prefijo = respDoc.Data[0].Prefijo;
                                        notaCreditoEntitie.Desde = respDoc.Data[0].NumeroDesde;
                                        notaCreditoEntitie.Hasta = respDoc.Data[0].NumeroHasta;
                                        notaCreditoEntitie.resolution_number = NumeroResolucion;
                                        notaCreditoEntitie.technical_key = ClaveTecnica;

                                        FechaExpedicion = Convert.ToDateTime(respDoc.Data[0].FechaExpedicion);
                                        FechaVencimiento = Convert.ToDateTime(respDoc.Data[0].FechaVencimiento);

                                        DateTime FechaActual = DateTime.Now;
                                        FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                                        if (FechaVencimiento >= FechaActual)
                                        {
                                            foreach (DataGridViewRow fila in dtg_ventas.Rows)
                                            {
                                                if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                                                {
                                                    DetalleNotaCredito detalleNotaCredito = new DetalleNotaCredito
                                                    {
                                                        IdDetalleVenta = Convert.ToInt32(fila.Cells["cl_id_detalle_venta"].Value),
                                                        IdProducto = Convert.ToInt32(fila.Cells["cl_idProducto"].Value),
                                                        Sku = fila.Cells["cl_sku"].Value.ToString() ?? "",
                                                        CodigoBarras = fila.Cells["cl_codigo_barras"].Value.ToString() ?? "",
                                                        NombreProducto = fila.Cells["cl_nombre"].Value.ToString() ?? "",
                                                        Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")),
                                                        PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")),
                                                        UnidadMedida = fila.Cells["cl_unidadMedida"].Value.ToString() ?? "",
                                                        Descuento = Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")),
                                                        Impuesto = Convert.ToDecimal(fila.Cells["cl_impuesto"].Value, new CultureInfo("es-CO")),
                                                        NombreTributo = fila.Cells["cl_tributo"].Value.ToString() ?? ""
                                                    };

                                                    notaCreditoEntitie.detalleNotaCredito.Add(detalleNotaCredito);
                                                }
                                            }

                                            var resp = await _INotaCredito.Create(notaCreditoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                            if (resp != null)
                                            {
                                                if (resp.Flag == true)
                                                {
                                                    int IdNotaCreditoCreada = resp.Data;

                                                    ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie =
                                                        new ActualizarNotaCreditoForNotaCreditoElectronicaEntitie();

                                                    try
                                                    {
                                                        if (NotaCreditoElectronica == true)
                                                        {
                                                            if (!string.IsNullOrEmpty(FacturaJSON))
                                                            {
                                                                string UrlCrearValidarNotaCredito = ConfigurationManager.AppSettings["UrlCrearValidarNotaCreditoPOST"]!;

                                                                dynamic? resultado = JsonConvert.DeserializeObject<dynamic>(FacturaJSON);

                                                                if (resultado != null)
                                                                {
                                                                    int _bill_id = resultado.data.bill.id;

                                                                    Customer customer = new Customer();
                                                                    customer.identification_document_id =
                                                                       (IdIdentificationType == 1 ? 3 : //Cédula de ciudadanía
                                                                       IdIdentificationType == 2 ? 5 : //Cédula de extranjería
                                                                       IdIdentificationType == 3 ? 3 : //RUT de momento se maneja con Cédula de ciudadanía
                                                                       IdIdentificationType == 4 ? 6 : 3).ToString(); //NIT

                                                                    customer.identification = resultado.data.customer.identification;
                                                                    customer.dv = resultado.data.customer.dv ?? "";
                                                                    if (IdIdentificationType == 4) //NIT
                                                                    {
                                                                        customer.company = resultado.data.customer.company;
                                                                        customer.trade_name = resultado.data.customer.trade_name;
                                                                    }
                                                                    else
                                                                    {
                                                                        customer.names = resultado.data.customer.names;
                                                                    }
                                                                    customer.address = resultado.data.customer.address;
                                                                    customer.email = resultado.data.customer.email;
                                                                    customer.phone = resultado.data.customer.phone;
                                                                    customer.legal_organization_id = resultado.data.customer.legal_organization.id; // 1. Juridico, 2. Natural
                                                                    customer.tribute_id = resultado.data.customer.tribute.id; // 18. IVA, 21. No aplica *
                                                                    if (resultado.data.customer.municipality is JArray municipalityArray && municipalityArray.Count > 0) 
                                                                    {
                                                                        customer.municipality_id = municipalityArray[0]["id"]?.ToString() ?? "";
                                                                    }
                                                                    else
                                                                    {
                                                                        customer.municipality_id = "";
                                                                    }
                                                                    //customer.municipality_id = resultado.data.customer.municipality.id.ToString() ?? ""; //"1079"; //Cali, valle del cauca

                                                                    List<Item> Listitems = new List<Item>();
                                                                    foreach (var ItemsVenta in resultado.data.items)
                                                                    {
                                                                        //WithholdingTax withholdingTax = new WithholdingTax
                                                                        //{
                                                                        //    Code = "01",
                                                                        //    WithholdingTaxRate = Convert.ToDecimal(ItemsVenta.Impuesto, new CultureInfo("es-CO"))
                                                                        //};

                                                                        //ListwithholdingTax.Add(withholdingTax);

                                                                        Item item = new Item
                                                                        {
                                                                            note = ItemsVenta.note,
                                                                            code_reference = ItemsVenta.code_reference,
                                                                            name = ItemsVenta.name,
                                                                            quantity = Convert.ToDecimal(ItemsVenta.quantity, new CultureInfo("es-CO")),
                                                                            discount_rate = Convert.ToDecimal(ItemsVenta.discount_rate, new CultureInfo("es-CO")),
                                                                            price = Convert.ToDecimal(ItemsVenta.price, new CultureInfo("es-CO")),
                                                                            tax_rate = Convert.ToDecimal(ItemsVenta.tax_rate, new CultureInfo("es-CO")),
                                                                            unit_measure_id = ItemsVenta.unit_measure.id,
                                                                            standard_code_id = ItemsVenta.standard_code.id,
                                                                            is_excluded = ItemsVenta.is_excluded,
                                                                            tribute_id = ItemsVenta.tribute.id,
                                                                            //withholding_taxes = ItemsVenta.withholding_taxes
                                                                        };

                                                                        Listitems.Add(item);
                                                                    }

                                                                    NotaCreditoRequest notaCreditoRequest = new NotaCreditoRequest
                                                                    {
                                                                        numbering_range_id = IdRangoDIAN,
                                                                        correction_concept_code = 2, //Anulación de factura electrónica.
                                                                        customization_id = 20, //Nota Crédito que referencia una factura electrónica.
                                                                        bill_id = _bill_id,
                                                                        reference_code = IdNotaCreditoCreada.ToString(),
                                                                        send_email = resultado.data.bill.send_email == 0 ? false : true,
                                                                        observation = txt_motivo_devolucion.Text,
                                                                        payment_method_code = resultado.data.bill.payment_method.code,
                                                                        customer = customer,
                                                                        items = Listitems
                                                                    };

                                                                    var responseNotaCreditoElectronica = _INotasCreditoElectronica.CreaValidaNotaCredito(Token, UrlCrearValidarNotaCredito, notaCreditoRequest);

                                                                    if (!responseNotaCreditoElectronica.Flag)
                                                                    {
                                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";

                                                                        MessageBox.Show($"Error en Emicion de nota electronica: {responseNotaCreditoElectronica?.Data} - {responseNotaCreditoElectronica?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (responseNotaCreditoElectronica.Data != null)
                                                                        {
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NumberNotaCreditoDIAN = responseNotaCreditoElectronica.Data!.data.credit_note.number;
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "EMITIDA";
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NotaCreditoJSON = responseNotaCreditoElectronica.Data.ToString();
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.qr_image = responseNotaCreditoElectronica.Data!.data.credit_note.qr_image;
                                                                        }
                                                                    }
                                                                    //Actualizar informacion de nota credito electronica en base de datos
                                                                    var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                    if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                    {
                                                                        MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                        this.Close();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                    //Actualizar informacion de nota credito electronica en base de datos
                                                                    var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                    if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                    {
                                                                        MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                    }

                                                                    MessageBox.Show($"Error no se logro leer JSON de factura electronica,  no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                    this.Close();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                //Actualizar informacion de nota credito electronica en base de datos
                                                                var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                {
                                                                    MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                }

                                                                MessageBox.Show($"No encontro informacion JSON de factura electronica a anular, se registrara nota credito en estado pendiente de Emitir", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                this.Close();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            this.Close();
                                                        }

                                                        //Imprime Tirilla
                                                        var DataTienda = await _ITienda.List();
                                                        if (DataTienda.Data != null)
                                                        {
                                                            if (DataTienda.Data.Count > 0)
                                                            {
                                                                var DataNotaCr = await _INotaCredito.List(IdNotaCreditoCreada);

                                                                NotaCreditoPOSEntitie DataNotaCredito = new NotaCreditoPOSEntitie();

                                                                DataNotaCredito.NumeroNotaCredito = DataNotaCr.Data![0].NumberNotaCreditoDIAN == "" ? DataNotaCr.Data![0].NotaCredito : DataNotaCr.Data![0].NumberNotaCreditoDIAN;
                                                                DataNotaCredito.NumeroFactura = DataNotaCr.Data![0].Factura;
                                                                DataNotaCredito.Fecha = DataNotaCr.Data[0].CreationDate;
                                                                DataNotaCredito.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                                                DataNotaCredito.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                                                DataNotaCredito.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                                                DataNotaCredito.NIT = DataTienda.Data[0].NumeroDocumento;
                                                                DataNotaCredito.UserNameNotaCredito = DataNotaCr.Data[0].IdUserActionNotaCredito + " - " + DataNotaCr.Data[0].UserName;
                                                                DataNotaCredito.NombreCliente = "";

                                                                DataNotaCredito.Estado = DataNotaCr.Data![0].EstadoNotaCreditoDIAN == "" ? DataNotaCr.Data[0].Estado : DataNotaCr.Data![0].EstadoNotaCreditoDIAN;

                                                                Cantidad = 0;
                                                                Subtotal = 0;
                                                                Descuento = 0;
                                                                Impuesto = 0;
                                                                SubtotalLinea = 0;
                                                                DescuentoLinea = 0;
                                                                decimal iva = 0;
                                                                decimal inc = 0;
                                                                decimal incBolsa = 0;

                                                                foreach (var item in DataNotaCr.Data)
                                                                {
                                                                    Cantidad += Convert.ToDecimal(item.Cantidad);
                                                                    Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                                                    SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                                                    Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                                                    DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));

                                                                    if (item.NombreTributo == "INC Bolsas")
                                                                    {
                                                                        incBolsa += Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO"));
                                                                    }
                                                                    else if (item.NombreTributo == "IVA")
                                                                    {
                                                                        iva += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                                                    }
                                                                    else if (item.NombreTributo == "INC")
                                                                    {
                                                                        inc += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                                                    }
                                                                }

                                                                Impuesto = iva + inc + incBolsa;

                                                                Total = (Subtotal - Descuento);
                                                                //Total = (Subtotal - Descuento) + Impuesto;
                                                                decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                                                                DataNotaCredito.CantidadTotal = Cantidad;
                                                                DataNotaCredito.Subtotal = SubtotalMenosImpuesto;
                                                                DataNotaCredito.Descuento = Descuento;
                                                                DataNotaCredito.Impuesto = Impuesto;
                                                                DataNotaCredito.iva = iva;
                                                                DataNotaCredito.inc = inc;
                                                                DataNotaCredito.incBolsa = incBolsa;
                                                                DataNotaCredito.Total = Total;

                                                                List<ItemFacturaEntitie> ListItemFacturaEntitie = new List<ItemFacturaEntitie>();

                                                                decimal precio;
                                                                decimal cantidad;
                                                                decimal desc;
                                                                decimal total;

                                                                foreach (var item in DataNotaCr.Data)
                                                                {
                                                                    precio = Convert.ToDecimal(item.PrecioUnitario);
                                                                    cantidad = Convert.ToDecimal(item.Cantidad);
                                                                    desc = Convert.ToDecimal(item.Descuento);

                                                                    total = CalcularTotal(precio, 0, desc);
                                                                    total = total * cantidad;

                                                                    string UnidadMedidaAbreviada;

                                                                    switch (item.UnidadMedida)
                                                                    {
                                                                        case "Unidad":
                                                                            UnidadMedidaAbreviada = "Und";
                                                                            break;
                                                                        case "kilogramo":
                                                                            UnidadMedidaAbreviada = "KGM";
                                                                            break;
                                                                        case "libra":
                                                                            UnidadMedidaAbreviada = "LBR";
                                                                            break;
                                                                        case "metro":
                                                                            UnidadMedidaAbreviada = "MTR";
                                                                            break;
                                                                        case "galón":
                                                                            UnidadMedidaAbreviada = "GLL";
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

                                                                DataNotaCredito.Items = ListItemFacturaEntitie;

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

                                                                        if (DataNotaCr.Data![0].NumberNotaCreditoDIAN != "") { NotaCreditoElectronica = true; }
                                                                        DataNotaCredito.NotaCreditoElectronica = NotaCreditoElectronica;
                                                                        DataNotaCredito.NotaCreditoJSON = !string.IsNullOrEmpty(DataNotaCr.Data![0].NotaCreditoJSON) ? DataNotaCr.Data![0].NotaCreditoJSON : "";

                                                                        StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaNotaCredito(DataNotaCredito, ANCHO_TIRILLA, MensajeFinalTirilla, false);

                                                                        string carpetaNotasCredito = "NotasCredito";
                                                                        if (!Directory.Exists(carpetaNotasCredito))
                                                                        {
                                                                            Directory.CreateDirectory(carpetaNotasCredito);
                                                                        }

                                                                        File.WriteAllText(Path.Combine(carpetaNotasCredito, $"nota_credito_{DataNotaCredito.NumeroNotaCredito}.txt"),
                                                                                                  tirilla.ToString(),
                                                                                                  Encoding.UTF8);

                                                                        if (NotaCreditoElectronica)
                                                                        {
                                                                            dynamic datosNotaCreditoElectronica = JsonConvert.DeserializeObject<dynamic>(DataNotaCr.Data![0].NotaCreditoJSON);

                                                                            GuardarTirillaComoImagen(tirilla, datosNotaCreditoElectronica!.data.credit_note.qr_image.ToString(), DataNotaCredito.NumeroNotaCredito);

                                                                            RawPrinterHelper.SendStringToPrinterConQr(Impresora, tirilla.ToString(), LineasAbajo, datosNotaCreditoElectronica!.data.credit_note.qr_image.ToString());
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
                                                    catch (Exception ex)
                                                    {
                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                        //Actualizar informacion de nota credito electronica en base de datos
                                                        var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                        if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                        {
                                                            MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Se presento una excepcion debido a esto se registrara nota credito en estado pendiente de Emitir, Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }

                                                        this.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("No se obtuvo informacion de proceso creacion de nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Ha excedido el rango de fechas autorizadas (de {FechaExpedicion} a {FechaVencimiento}) ", "Rango de numeracion vencido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($@"Ha excedido el rango autorizado de numeración (de {NumDesde} a {NumHasta}).
                                                      Por favor, solicite una nueva resolución de numeración para continuar con la emisión de nota credito.",
                                                          "Límite de notas credito alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (!TodoProductTieneCantDevo)
                    {
                        MessageBox.Show("Todos los productos seleccionados debe tener Cant.dev mayor a cero, favor verificar ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar 1 producto y total devolucion debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            finally
            {
                panel2.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            if (ModoRedondeo != "N/A")
            {
                var valorRendondeado = Redondear(ValorDescuento, Convert.ToInt32(MultiploRendondeo));
                ValorDescuento = valorRendondeado;
            }

            return ValorDescuento;
        }

        decimal Redondear(decimal valor, int multiplo)
        {
            decimal valorRendondeado = 0;

            switch (ModoRedondeo)
            {
                case "Hacia arriba":
                    valorRendondeado = (decimal)(Math.Ceiling((decimal)valor / multiplo) * multiplo);
                    break;
                case "Hacia abajo":
                    valorRendondeado = (decimal)(Math.Floor((decimal)valor / multiplo) * multiplo);
                    break;
                case "Hacia arriba o hacia abajo":
                    valorRendondeado = (decimal)(Math.Round((decimal)valor / multiplo) * multiplo);
                    break;

                default:
                    break;
            }

            return valorRendondeado;
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

        private void dtg_ventas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalCantidadDevo;
            decimal TotalLineaDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (dtg_ventas.Columns[e.ColumnIndex].Name == "cl_seleccionado")
            {
                dtg_ventas.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //bool isChecked = Convert.ToBoolean(dtg_ventas.Rows[e.RowIndex].Cells["cl_seleccionado"].Value);

                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo);
                        //TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }
                    else
                    {
                        fila.Cells["cl_cantidad_devolver"].Value = 0;
                    }
                }

                lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
            }
        }

        private void chk_marcar_todo_CheckedChanged(object sender, EventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (chk_marcar_todo.Checked)
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = true;
                    TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                    ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_impuesto"].Value, new CultureInfo("es-CO")));
                    TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo);
                    //TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                    TotalParaDevolucion += TotalLineaDevo;
                }
            }
            else
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = false;
                    fila.Cells["cl_cantidad_devolver"].Value = 0;
                }
            }

            lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
            lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
        }

        private void dtg_ventas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (e.ColumnIndex == 9)
            {
                var celda = dtg_ventas[e.ColumnIndex, e.RowIndex];

                if (celda.Value == null || string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    if (e.ColumnIndex == 9)
                    {
                        celda.Value = 0;
                    }

                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    decimal cantidad = Convert.ToDecimal(dtg_ventas[8, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal cantidadDevo = Convert.ToDecimal(dtg_ventas[9, e.RowIndex].Value, new CultureInfo("es-CO"));

                    if (cantidadDevo > cantidad)
                    {
                        MessageBox.Show("Cantidad a devolver no puede ser mayor a cantidad vendida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (e.ColumnIndex == 9)
                        {
                            celda.Value = 0;
                        }
                        return;
                    }

                    dtg_ventas[1, e.RowIndex].Value = 1;
                   
                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
            }
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private void dtg_ventas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void dtg_ventas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Remover eventos anteriores para evitar duplicados
            e.Control.KeyPress -= new KeyPressEventHandler(dtg_ventas_KeyPress);

            if (dtg_ventas.CurrentCell.ColumnIndex == 9)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dtg_ventas_KeyPress);
            }
        }

        public static void GuardarTirillaComoImagen(StringBuilder tirilla, string base64QR, string numeroNotaCredito)
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
            string carpeta = "NotasCredito";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, $"nota_credito_{numeroNotaCredito}.png");
            tirillaFinal.Save(ruta, ImageFormat.Png);
        }
    }
}
