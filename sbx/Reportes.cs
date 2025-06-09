using sbx.core.Interfaces.Reportes;
using System.Globalization;

namespace sbx
{
    public partial class Reportes : Form
    {
        private readonly IReportes _IReportes;

        public Reportes(IReportes reportes)
        {
            InitializeComponent();
            _IReportes = reportes;
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            cbx_tipo_reporte.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void cbx_client_venta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();

            if (cbx_tipo_reporte.Text == "Resumen - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre producto", "Id" });
            }

            if (cbx_tipo_reporte.Text == "Detallado -  Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
            }

            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            var resp = await _IReportes.Buscar(txt_buscar.Text,cbx_campo_filtro.Text,cbx_tipo_filtro.Text,cbx_tipo_reporte.Text,dtp_fecha_inicio.Value,dtp_fecha_fin.Value);

            dtg_reportes.Columns.Clear();

            
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

                    if (cbx_tipo_reporte.Text == "Resumen - Ganancias y perdidas")
                    {
                        dtg_reportes.Columns.Add("cl_CreationDate", "Fecha");
                        dtg_reportes.Columns.Add("cl_Factura", "Factura");
                        dtg_reportes.Columns.Add("cl_IdProducto", "Id");
                        dtg_reportes.Columns.Add("cl_NombreProducto", "Nombre");
                        dtg_reportes.Columns.Add("cl_Cantidad", "Cantidad");
                        dtg_reportes.Columns.Add("cl_PrecioUnitario", "Precio");
                        dtg_reportes.Columns.Add("cl_CostoUnitario", "Costo");
                        dtg_reportes.Columns.Add("cl_Descuento", "Desc %");
                        dtg_reportes.Columns.Add("cl_Impuesto", "Iva %");
                        dtg_reportes.Columns.Add("cl_VentaNetaFinal", "Venta Total");
                        dtg_reportes.Columns.Add("cl_CostoTotal", "Costo Total");
                        dtg_reportes.Columns.Add("cl_GananciaBruta", "Ganancia");
                        dtg_reportes.Columns.Add("cl_MargenPorcentaje", "Margen");

                        foreach (var item in resp.Data)
                        {
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.DescuentoPorcentaje));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.DescuentoPorcentaje));
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.ImpuestoPorcentaje));
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.ImpuestoPorcentaje));
                            TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                            dtg_reportes.Rows.Add(item.CreationDate, item.Factura, item.IdProducto, item.NombreProducto,
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")), 
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.DescuentoPorcentaje, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.ImpuestoPorcentaje, new CultureInfo("es-CO")), 
                                item.VentaNetaFinal.ToString("N2", new CultureInfo("es-CO")), 
                                item.CostoTotal.ToString("N2", new CultureInfo("es-CO")), 
                                item.GananciaBruta.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.MargenPorcentaje, new CultureInfo("es-CO")));
                        }

                        Total += (Subtotal - Descuento) + Impuesto;

                        lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    }
                    else if (cbx_tipo_reporte.Text == "Detallado -  Ganancias y perdidas")
                    {
                        dtg_reportes.Columns.Add("cl_CreationDate", "Fecha");
                        dtg_reportes.Columns.Add("cl_Usuario", "Usuario");
                        dtg_reportes.Columns.Add("cl_Factura", "Factura");
                        dtg_reportes.Columns.Add("cl_Cliente", "Cliente");
                        dtg_reportes.Columns.Add("cl_Vendedor", "Vendedor");
                        dtg_reportes.Columns.Add("cl_IdProducto", "Id");
                        dtg_reportes.Columns.Add("cl_Sku", "Sku");
                        dtg_reportes.Columns.Add("cl_CodigoBarras", "Codigo Barras");
                        dtg_reportes.Columns.Add("cl_NombreProducto", "Nombre");
                        dtg_reportes.Columns.Add("cl_UnidadMedida", "Unidad Medida");
                        dtg_reportes.Columns.Add("cl_Cantidad", "Cantidad");
                        dtg_reportes.Columns.Add("cl_PrecioUnitario", "Precio");
                        dtg_reportes.Columns.Add("cl_CostoUnitario", "Costo");
                        dtg_reportes.Columns.Add("cl_Descuento", "Desc %");
                        dtg_reportes.Columns.Add("cl_Impuesto", "Iva %");
                        dtg_reportes.Columns.Add("cl_VentaNetaFinal", "Venta Total");
                        dtg_reportes.Columns.Add("cl_CostoTotal", "Costo Total");
                        dtg_reportes.Columns.Add("cl_GananciaBruta", "Ganancia");
                        dtg_reportes.Columns.Add("cl_MargenPorcentaje", "Margen");

                        foreach (var item in resp.Data)
                        {
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.DescuentoPorcentaje));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.DescuentoPorcentaje));
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.ImpuestoPorcentaje));
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.ImpuestoPorcentaje));
                            TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                            dtg_reportes.Rows.Add(item.CreationDate,item.Usuario, item.Factura,item.Cliente,item.Vendedor, 
                                item.IdProducto,item.Sku,item.CodigoBarras, item.NombreProducto,item.UnidadMedida,
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.DescuentoPorcentaje, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.ImpuestoPorcentaje, new CultureInfo("es-CO")),
                                item.VentaNetaFinal.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoTotal.ToString("N2", new CultureInfo("es-CO")),
                                item.GananciaBruta.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.MargenPorcentaje, new CultureInfo("es-CO")));
                        }

                        Total += (Subtotal - Descuento) + Impuesto;

                        lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    }
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
                lbl_cantidadProductos.Text = "_";
                lbl_subtotal.Text = "_";
                lbl_descuento.Text = "_";
                lbl_impuesto.Text = "_";
                lbl_total.Text = "_";

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
    }
}
