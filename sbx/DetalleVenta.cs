using sbx.core.Interfaces.Venta;
using System.Globalization;

namespace sbx
{
    public partial class DetalleVenta : Form
    {
        private int _Id_Venta;
        private dynamic? _Permisos;
        private readonly IVenta _IVenta;

        public DetalleVenta(IVenta venta)
        {
            InitializeComponent();
            _IVenta = venta;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Venta
        {
            get => _Id_Venta;
            set => _Id_Venta = value;
        }

        private async void DetalleVenta_Load(object sender, EventArgs e)
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
                        case "ventas":
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

        private async Task CargaDatosIniciales()
        {
            if (Id_Venta > 0)
            {
                var resp = await _IVenta.List(Id_Venta);

                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        lbl_factura.Text = resp.Data[0].Factura;
                        lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                        lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                        lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                        lbl_referencia.Text = resp.Data[0].Referencia;
                        lbl_banco.Text = resp.Data[0].NombreBanco;
                        lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - "+ resp.Data[0].UserNameFactura;

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
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.NombreProducto,
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                                TotalLinea.ToString("N2", new CultureInfo("es-CO")));
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
                        MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se encontro id venta a consultar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }
    }
}
