using sbx.core.Interfaces.Reportes;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Newtonsoft.Json;
using System.Data;
using sbx.core.Entities.Reportes;

namespace sbx
{
    public partial class Reportes : Form
    {
        private readonly IReportes _IReportes;
        List<ResumenGananciasPerdidas> ListResumenGananciasPerdidas = new List<ResumenGananciasPerdidas>();

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

        private void cbx_tipo_reporte_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();

            if (cbx_tipo_reporte.Text == "Resumen por factura - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre producto", "Id" });
                label7.Visible = false;
                lbl_cantidad_resumen.Visible = false;
                panel3.Visible = true;
                label4.Visible = true;
                label6.Visible = true;
                label8.Visible = true;
                label10.Visible = true;
                label12.Visible = true;
                lbl_cantidadProductos.Visible = true;
                lbl_subtotal.Visible = true;
                lbl_descuento.Visible = true;
                lbl_impuesto.Visible = true;
                lbl_total.Visible = true;
            }

            if (cbx_tipo_reporte.Text == "Resumen - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre producto", "Id" });
                label7.Visible = true;
                lbl_cantidad_resumen.Visible = true;
                panel3.Visible = false;
                label4.Visible = false;
                label6.Visible = false;
                label8.Visible = false;
                label10.Visible = false;
                label12.Visible = false;
                lbl_cantidadProductos.Visible = false;
                lbl_subtotal.Visible = false;
                lbl_descuento.Visible = false;
                lbl_impuesto.Visible = false;
                lbl_total.Visible = false;
            }

            if (cbx_tipo_reporte.Text == "Detallado -  Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
                label7.Visible = false;
                lbl_cantidad_resumen.Visible = false;
                panel3.Visible = true;
                label4.Visible = true;
                label6.Visible = true;
                label8.Visible = true;
                label10.Visible = true;
                label12.Visible = true;
                lbl_cantidadProductos.Visible = true;
                lbl_subtotal.Visible = true;
                lbl_descuento.Visible = true;
                lbl_impuesto.Visible = true;
                lbl_total.Visible = true;
            }

            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            btn_buscar.Enabled = false;
            txt_buscar.Enabled = false;
            cbx_tipo_reporte.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            var resp = await _IReportes.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_tipo_reporte.Text, dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

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
                    decimal TotalCostos = 0;
                    decimal TotalVentas = 0;
                    decimal Ganancia = 0;

                    if (cbx_tipo_reporte.Text == "Resumen por factura - Ganancias y perdidas")
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
                            TotalCostos += Convert.ToDecimal(item.CostoTotal);
                            TotalVentas += Convert.ToDecimal(item.VentaNetaFinal);
                            Ganancia += Convert.ToDecimal(item.GananciaBruta);

                            dtg_reportes.Rows.Add(item.CreationDate, item.Factura, item.IdProducto, item.NombreProducto,
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.DescuentoPorcentaje, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.ImpuestoPorcentaje, new CultureInfo("es-CO")),
                                item.VentaNetaFinal.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoTotal.ToString("N2", new CultureInfo("es-CO")),
                                item.GananciaBruta.ToString("N2", new CultureInfo("es-CO")),
                                item.MargenPorcentaje.ToString("N2", new CultureInfo("es-CO")));
                        }

                        Total += (Subtotal - Descuento) + Impuesto;

                        lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));

                        lbl_costos.Text = TotalCostos.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ventas.Text = TotalVentas.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ganancia.Text = Ganancia.ToString("N2", new CultureInfo("es-CO"));
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
                            TotalCostos += Convert.ToDecimal(item.CostoTotal);
                            TotalVentas += Convert.ToDecimal(item.VentaNetaFinal);
                            Ganancia += Convert.ToDecimal(item.GananciaBruta);

                            dtg_reportes.Rows.Add(item.CreationDate, item.Usuario, item.Factura, item.Cliente, item.Vendedor,
                                item.IdProducto, item.Sku, item.CodigoBarras, item.NombreProducto, item.UnidadMedida,
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

                        lbl_costos.Text = TotalCostos.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ventas.Text = TotalVentas.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ganancia.Text = Ganancia.ToString("N2", new CultureInfo("es-CO"));
                    }
                    else if (cbx_tipo_reporte.Text == "Resumen - Ganancias y perdidas")
                    {
                        dtg_reportes.Columns.Add("cl_IdProducto", "Id");
                        dtg_reportes.Columns.Add("cl_NombreProducto", "Nombre");
                        dtg_reportes.Columns.Add("cl_Cantidad", "Cantidad");
                        dtg_reportes.Columns.Add("cl_VentaNetaFinal", "Venta Total");
                        dtg_reportes.Columns.Add("cl_CostoTotal", "Costo Total");
                        dtg_reportes.Columns.Add("cl_GananciaBruta", "Ganancia");

                        foreach (var item in resp.Data)
                        {
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            TotalCostos += Convert.ToDecimal(item.CostoTotal);
                            TotalVentas += Convert.ToDecimal(item.VentaNetaFinal);
                            Ganancia += Convert.ToDecimal(item.GananciaBruta);

                            dtg_reportes.Rows.Add(
                                item.IdProducto, item.NombreProducto,
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                item.VentaNetaFinal.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoTotal.ToString("N2", new CultureInfo("es-CO")),
                                item.GananciaBruta.ToString("N2", new CultureInfo("es-CO")));
                        }

                        lbl_cantidad_resumen.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_costos.Text = TotalCostos.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ventas.Text = TotalVentas.ToString("N2", new CultureInfo("es-CO"));
                        lbl_ganancia.Text = Ganancia.ToString("N2", new CultureInfo("es-CO"));
                    }
                }
                else
                {
                    lbl_cantidadProductos.Text = "_";
                    lbl_subtotal.Text = "_";
                    lbl_descuento.Text = "_";
                    lbl_impuesto.Text = "_";
                    lbl_total.Text = "_";

                    lbl_cantidad_resumen.Text = "_";
                    lbl_costos.Text = "_";
                    lbl_ventas.Text = "_";
                    lbl_ganancia.Text = "_";
                }
            }
            else
            {
                lbl_cantidadProductos.Text = "_";
                lbl_subtotal.Text = "_";
                lbl_descuento.Text = "_";
                lbl_impuesto.Text = "_";
                lbl_total.Text = "_";

                lbl_cantidad_resumen.Text = "_";
                lbl_costos.Text = "_";
                lbl_ventas.Text = "_";
                lbl_ganancia.Text = "_";              
            }

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            cbx_tipo_reporte.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
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

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await Buscar();
            }
        }

        private async void btn_imprimir_pdf_Click(object sender, EventArgs e)
        {
            var resp = await _IReportes.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, "Resumen - Ganancias y perdidas", dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                        saveFileDialog.Title = "Guardar reporte como PDF";
                        saveFileDialog.FileName = "Reporte.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            btn_buscar.Enabled = false;
                            txt_buscar.Enabled = false;
                            cbx_tipo_reporte.Enabled = false;
                            this.Cursor = Cursors.WaitCursor;

                            string json = JsonConvert.SerializeObject(resp.Data);

                            DataTable? dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                            if (dataTable != null)
                            {
                                ListResumenGananciasPerdidas.Clear();

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    var resumenGananciasPerdidas = new ResumenGananciasPerdidas
                                    {
                                        IdProducto = Convert.ToInt32(row["IdProducto"]),
                                        NombreProducto = row["NombreProducto"].ToString() ?? "",
                                        Cantidad = Convert.ToDecimal(row["Cantidad"], new CultureInfo("es-CO")),
                                        VentaNetaFinal = Convert.ToDecimal(row["VentaNetaFinal"], new CultureInfo("es-CO")),
                                        CostoTotal = Convert.ToDecimal(row["CostoTotal"], new CultureInfo("es-CO")),
                                        GananciaBruta = Convert.ToDecimal(row["GananciaBruta"], new CultureInfo("es-CO"))
                                    };

                                    ListResumenGananciasPerdidas.Add(resumenGananciasPerdidas);
                                }

                                GenerarPdf(saveFileDialog.FileName);

                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                {
                                    FileName = saveFileDialog.FileName,
                                    UseShellExecute = true
                                });
                            }

                            btn_buscar.Enabled = true;
                            txt_buscar.Enabled = true;
                            cbx_tipo_reporte.Enabled = true;
                            txt_buscar.Focus();
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("PDF generado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GenerarPdf(string ruta)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Element(ComposeHeader);

                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf(ruta);
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item()
                        .Text("Resumen de ventas")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium)
                        .AlignCenter();

                    column.Item().Text(text =>
                    {
                        text.Span($"Fecha inicial: {dtp_fecha_inicio.Text}").SemiBold();
                        text.Span("");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span($"Fecha final: {dtp_fecha_fin.Text}").SemiBold();
                        text.Span("");
                    });
                });

                //row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(3);

                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(40);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                });

                table.Header(header =>
                {
                    header.Cell().Element(Style).Text("Id").Bold();
                    header.Cell().Element(Style).Text("Nombre").Bold();
                    header.Cell().Element(Style).Text("T. Cantidad").SemiBold();
                    header.Cell().Element(Style).Text("T. Costo").Bold();
                    header.Cell().Element(Style).Text("T. Ventas").Bold();
                    header.Cell().Element(Style).Text("T. Ganancias").Bold();
                });

                foreach (var item in ListResumenGananciasPerdidas)
                {
                    table.Cell().Element(Style).Text(item.IdProducto.ToString());
                    table.Cell().Element(Style).Text(item.NombreProducto.ToString());
                    table.Cell().Element(Style).Text(item.Cantidad.ToString(new CultureInfo("es-CO")));
                    table.Cell().Element(Style).Text(item.CostoTotal.ToString("N2", new CultureInfo("es-CO")));
                    table.Cell().Element(Style).Text(item.VentaNetaFinal.ToString("N2", new CultureInfo("es-CO")));
                    table.Cell().Element(Style).Text(item.GananciaBruta.ToString("N2", new CultureInfo("es-CO")));
                }

                var totalCantidad = ListResumenGananciasPerdidas.Sum(x => x.Cantidad);
                var totalCosto = ListResumenGananciasPerdidas.Sum(x => x.CostoTotal);
                var totalVentaNetaFinal = ListResumenGananciasPerdidas.Sum(x => x.VentaNetaFinal);
                var totalGananciaBruta = ListResumenGananciasPerdidas.Sum(x => x.GananciaBruta);

                table.Cell().ColumnSpan(2)
                     .Background(Colors.Grey.Lighten2)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten3)
                     .AlignRight()
                     .Padding(5)
                     .Text("Totales");

                table.Cell().ColumnSpan(1)
                     .Background(Colors.Grey.Lighten2)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten3)
                     .AlignCenter()
                     .Padding(5)
                     .Text(totalCantidad.ToString(new CultureInfo("es-CO")));

                table.Cell().ColumnSpan(1)
                     .Background(Colors.Grey.Lighten2)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten3)
                     .AlignCenter()
                     .Padding(5)
                     .Text(totalCosto.ToString("N2", new CultureInfo("es-CO")));

                table.Cell().ColumnSpan(1)
                     .Background(Colors.Grey.Lighten2)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten3)
                     .AlignCenter()
                     .Padding(5)
                     .Text(totalVentaNetaFinal.ToString("N2", new CultureInfo("es-CO")));

                table.Cell().ColumnSpan(1)
                     .Background(Colors.Grey.Lighten2)
                     .Border(1)
                     .BorderColor(Colors.Grey.Lighten3)
                     .AlignCenter()
                     .Padding(5)
                     .Text(totalGananciaBruta.ToString("N2", new CultureInfo("es-CO")));

                IContainer Style(IContainer container)
                {
                    return container

                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten3)
                        .Background(Colors.White)
                        .Padding(5)
                        .AlignCenter()
                        .AlignMiddle();
                }
            });
        }
    }
}
