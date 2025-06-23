using sbx.core.Entities.Cotizacion;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.Cotizacion;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Tienda;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class Cotizacion : Form
    {
        private dynamic? _Permisos;
        private readonly ICotizacion _ICotizacion;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        public delegate void EnviarId(int id);
        public event EnviarId EnviaId;
        int fila = 0;
        int Id = 0;
        private int Id_Cotizacion = 0;

        public Cotizacion(ICotizacion cotizacion, ITienda tienda, IParametros parametros)
        {
            InitializeComponent();
            _ICotizacion = cotizacion;
            _ITienda = tienda;
            _IParametros = parametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;

        private void Cotizacion_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_client_venta.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "cotizacion":
                            btn_imprimir.Enabled = item.ToRead == 1 ? true : false;
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

            var resp = await _ICotizacion.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_venta.Text, dtp_fecha_inicio.Value, dtp_fecha_fin.Value, Convert.ToInt32(_Permisos?[0]?.IdUser), _Permisos?[0]?.NameRole);

            dtg_cotizaciones.Rows.Clear();

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
                        cantidadTotal += Convert.ToDecimal(item.Cantidad);
                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                        dtg_cotizaciones.Rows.Add(
                            item.IdCotizacion,
                            item.FechaCotizacion,
                            item.Cotizacion,
                            item.Estado,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.NombreProducto,
                            item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                            TotalLinea.ToString("N2", new CultureInfo("es-CO")),
                            item.IdUserActionCotizacion + " - " + item.UserNameCotizacion);
                    }

                    Total += (Subtotal - Descuento) + Impuesto;

                    lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                    lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
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

                    //MessageBox.Show("No se encuentran datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaProductos();
        }

        private void cbx_client_venta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();
            if (cbx_client_venta.Text == "Cotizacion")
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

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProductos();
            }
        }

        private async void btn_imprimir_Click(object sender, EventArgs e)
        {
            if (dtg_cotizaciones.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de imprimir la cotizacion?",
                       "Confirmar cancelacion",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_cotizaciones.SelectedRows.Count > 0)
                    {
                        var row = dtg_cotizaciones.SelectedRows[0];
                        if (row.Cells["cl_id_cotizacion"].Value != null)
                        {
                            var Id_cotizacion = Convert.ToInt32(row.Cells["cl_id_cotizacion"].Value);

                            //Imprime Tirilla
                            var DataTienda = await _ITienda.List();
                            if (DataTienda.Data != null)
                            {
                                if (DataTienda.Data.Count > 0)
                                {
                                    var DatosCotizacion = await _ICotizacion.List(Id_cotizacion);

                                    CotizacionPOSEntitie DtCotizacion = new CotizacionPOSEntitie();

                                    DtCotizacion.NumeroCotizacion = DatosCotizacion.Data[0].Cotizacion;
                                    DtCotizacion.Fecha = DatosCotizacion.Data[0].FechaCotizacion;
                                    DtCotizacion.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                    DtCotizacion.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                    DtCotizacion.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                    DtCotizacion.NIT = DataTienda.Data[0].NumeroDocumento;
                                    DtCotizacion.UserNameCotizacion = DatosCotizacion.Data[0].IdUserActionCotizacion + " - " + DatosCotizacion.Data[0].UserNameCotizacion;
                                    DtCotizacion.NombreCliente = DatosCotizacion.Data[0].NumeroDocumento + " - " + DatosCotizacion.Data[0].NombreRazonSocial;
                                    DtCotizacion.NombreVendedor = DatosCotizacion.Data[0].NumeroDocumentoVendedor + " - " + DatosCotizacion.Data[0].NombreVendedor;
                                    DtCotizacion.Estado = DatosCotizacion.Data[0].Estado;
                                    DtCotizacion.DiasVencimiento = DatosCotizacion.Data[0].DiasVencimiento;
                                    DtCotizacion.FechaVencimiento = DatosCotizacion.Data[0].FechaVencimiento;

                                    Cantidad = 0;
                                    Subtotal = 0;
                                    Descuento = 0;
                                    Impuesto = 0;
                                    SubtotalLinea = 0;
                                    DescuentoLinea = 0;

                                    foreach (var item in DatosCotizacion.Data)
                                    {
                                        Cantidad += Convert.ToDecimal(item.Cantidad);
                                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                    }
                                    Total = (Subtotal - Descuento) + Impuesto;

                                    DtCotizacion.CantidadTotal = Cantidad;
                                    DtCotizacion.Subtotal = Subtotal;
                                    DtCotizacion.Descuento = Descuento;
                                    DtCotizacion.Impuesto = Impuesto;
                                    DtCotizacion.Total = Total;

                                    List<ItemCotizacionEntitie> ListItemCotizacionEntitie = new List<ItemCotizacionEntitie>();

                                    decimal precio;
                                    decimal cantidad;
                                    decimal desc;
                                    decimal iva;
                                    decimal totalLinea;
                                    decimal total;

                                    foreach (var item in DatosCotizacion.Data)
                                    {
                                        precio = Convert.ToDecimal(item.PrecioUnitario);
                                        cantidad = Convert.ToDecimal(item.Cantidad);
                                        desc = Convert.ToDecimal(item.Descuento);
                                        iva = Convert.ToDecimal(item.Impuesto);

                                        total = CalcularTotal(precio, iva, desc);
                                        total = total * cantidad;

                                        string UnidadMedidaAbreviada;

                                        switch (item.UnidadMedida)
                                        {
                                            case "Unidad (und)":
                                                UnidadMedidaAbreviada = "und";
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

                                        var ItemCotizacion = new ItemCotizacionEntitie
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

                                        ListItemCotizacionEntitie.Add(ItemCotizacion);
                                    }

                                    DtCotizacion.Items = ListItemCotizacionEntitie;

                                    var DataParametros = await _IParametros.List("");

                                    if (DataParametros.Data != null)
                                    {
                                        if (DataParametros.Data.Count > 0)
                                        {
                                            int ANCHO_TIRILLA = 0;
                                            string Impresora = "";
                                            string MensajeFinalTirilla = "";
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
                                                    default:
                                                        break;
                                                }
                                            }

                                            StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaCotizacion(DtCotizacion, ANCHO_TIRILLA, MensajeFinalTirilla);

                                            string carpetaCotizaciones = "Cotizaciones";
                                            if (!Directory.Exists(carpetaCotizaciones))
                                            {
                                                Directory.CreateDirectory(carpetaCotizaciones);
                                            }

                                            File.WriteAllText(Path.Combine(carpetaCotizaciones, $"cotizacion_{DtCotizacion.NumeroCotizacion}.txt"),
                                                              tirilla.ToString(),
                                                              Encoding.UTF8);

                                            RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString());
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
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private void dtg_cotizaciones_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_cotizaciones.Rows.Count > 0)
            {
                fila = dtg_cotizaciones.CurrentRow.Index;
                var Estado = dtg_cotizaciones[3, fila].Value.ToString();
                if (Estado == "PENDIENTE") 
                {
                    Id = Convert.ToInt32(dtg_cotizaciones[0, fila].Value);
                    EnviaId(Id);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La cotizacion ya fue facturada, favor seleccione cotizaciones en estado PENDIENTE", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
