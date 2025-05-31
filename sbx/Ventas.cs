using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.Venta;
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
        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;

        public Ventas(IVenta venta, IServiceProvider serviceProvider, ITienda tienda)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
            _ITienda = tienda;
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

            var resp = await _IVenta.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_venta.Text, dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

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
                        cantidadTotal += Convert.ToDecimal(item.Cantidad);
                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                        dtg_ventas.Rows.Add(
                            item.FechaFactura,
                            item.IdVenta,
                            item.Factura,
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

                                    DataFactura.NumeroFactura = DataVenta.Data[0].Factura;
                                    DataFactura.Fecha = DataVenta.Data[0].FechaFactura;
                                    DataFactura.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                    DataFactura.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                    DataFactura.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                    DataFactura.NIT = DataTienda.Data[0].NumeroDocumento;
                                    DataFactura.UserNameFactura = DataVenta.Data[0].IdUserActionFactura + " - " + DataVenta.Data[0].UserNameFactura;
                                    DataFactura.NombreCliente = DataVenta.Data[0].NumeroDocumento + " - " + DataVenta.Data[0].NombreRazonSocial;
                                    DataFactura.NombreVendedor = DataVenta.Data[0].NumeroDocumentoVendedor + " - " + DataVenta.Data[0].NombreVendedor;
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
                                    Total = (Subtotal - Descuento) + Impuesto;

                                    DataFactura.CantidadTotal = Cantidad;
                                    DataFactura.Subtotal = Subtotal;
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

                                        total = CalcularTotal(precio, iva, desc);
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

                                    string tirilla = GenerarTirillaPOS.GenerarTirilla(DataFactura);
                                    File.WriteAllText($"factura_{DataFactura.NumeroFactura}.txt", tirilla, Encoding.UTF8);
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
    }
}
