using sbx.core.Interfaces.NotaCredito;
using System.Globalization;

namespace sbx
{
    public partial class DetalleProdDevo : Form
    {
        private int _Id_Nota_credito;
        private dynamic? _Permisos;
        private readonly INotaCredito _INotaCredito;

        public DetalleProdDevo(INotaCredito notaCredito)
        {
            InitializeComponent();
            _INotaCredito = notaCredito;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_NotaCredito
        {
            get => _Id_Nota_credito;
            set => _Id_Nota_credito = value;
        }

        private async void DetalleProdDevo_Load(object sender, EventArgs e)
        {
            await CargaDatosIniciales();
        }

        private async Task CargaDatosIniciales()
        {
            if (Id_NotaCredito > 0)
            {
                var resp = await _INotaCredito.List(Id_NotaCredito);

                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        lbl_nota_credito.Text = "NC-"+ resp.Data[0].IdNotaCredito;
                        lbl_usuario.Text = resp.Data[0].Usuario;
                        txt_motivo_devolucion.Text = resp.Data[0].Motivo;

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

                            dtg_detalle_nota_credito.Rows.Add(
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

                        lbl_cantidad_devolucion.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_total_devolucion.Text = Total.ToString("N2", new CultureInfo("es-CO"));

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
