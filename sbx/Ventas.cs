using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Venta;
using System.Globalization;

namespace sbx
{
    public partial class Ventas : Form
    {
        private dynamic? _Permisos;
        private readonly IVenta _IVenta;
        private readonly IServiceProvider _serviceProvider;
        private DetalleVenta? _DetalleVenta;

        public Ventas(IVenta venta, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
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
    }
}
