using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.Dashboard;
using sbx.core.Interfaces.Parametros;
using System.Data;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace sbx
{
    public partial class Dashboard : Form
    {
        private readonly IDashboard _IDashboard;
        private Reportes? _Reportes;
        private readonly IServiceProvider _serviceProvider;
        private readonly IParametros _IParametros;
        private dynamic? _Permisos;

        public Dashboard(IDashboard dashboard, IServiceProvider serviceProvider, IParametros iParametros)
        {
            InitializeComponent();
            _IDashboard = dashboard;
            _serviceProvider = serviceProvider;
            _IParametros = iParametros;
        }

        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Dashboard_Load(object sender, EventArgs e)
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

            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            decimal Subtotal = 0;
            decimal cantidadTotal = 0;
            decimal DescuentoLinea;
            decimal Descuento = 0;
            decimal Impuesto = 0;
            decimal ImpuestoLinea;
            decimal SubtotalLinea;
            decimal TotalLinea;
            decimal iva = 0;
            decimal inc = 0;
            decimal incBolsa = 0;
            decimal CostoLinea = 0;

            var resp = await _IDashboard.Buscar(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            DataTable dtGrafico = new DataTable();
            dtGrafico.Columns.Add("CreationDate", typeof(DateTime));
            dtGrafico.Columns.Add("IdProducto", typeof(Int64));
            dtGrafico.Columns.Add("NombreProducto", typeof(string));
            dtGrafico.Columns.Add("MedioPago", typeof(string));
            dtGrafico.Columns.Add("CostoTotal", typeof(decimal));
            dtGrafico.Columns.Add("VentaTotal", typeof(decimal));
            dtGrafico.Columns.Add("Ganancia", typeof(decimal));

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        Subtotal = 0;
                        cantidadTotal = 0;
                        DescuentoLinea = 0;
                        Descuento = 0;
                        Impuesto = 0;
                        ImpuestoLinea = 0;
                        SubtotalLinea = 0;
                        TotalLinea = 0;
                        iva = 0;
                        inc = 0;
                        incBolsa = 0;
                        CostoLinea = 0;

                        if (item.Estado == "FACTURADA") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                        if (item.Estado == "FACTURADA") { Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad); }
                        if (item.Estado == "FACTURADA") { CostoLinea = Convert.ToDecimal(item.CostoUnitario) * Convert.ToDecimal(item.Cantidad); }
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        if (item.Estado == "FACTURADA") { Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento)); }
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        if (item.NombreTributo == "INC Bolsas")
                        {
                            if (item.Estado == "FACTURADA") { Impuesto += Convert.ToDecimal(item.Impuesto); }
                            ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                        }
                        else
                        {
                            if (item.Estado == "FACTURADA") { Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto)); }
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        }

                        if (item.Estado == "FACTURADA")
                        {
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

                        //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;
                        TotalLinea = (SubtotalLinea - DescuentoLinea);

                        dtGrafico.Rows.Add(
                            item.CreationDate,
                            item.IdProducto,
                            item.NombreProducto,
                            item.MedioPago,
                            CostoLinea,
                            TotalLinea,
                            TotalLinea - CostoLinea
                            );
                    }
                }
            }

            if (dtGrafico != null)
            {
                if (dtGrafico.Rows.Count > 0) 
                {
                    //Ventas totales
                    decimal VentaNetaFinal = dtGrafico.AsEnumerable().Sum(row => row.Field<decimal>("VentaTotal"));
                    lbl_ventas_totales.Text = VentaNetaFinal.ToString("N2", new CultureInfo("es-CO"));

                    //Total ganancias
                    decimal TotalGanancias = dtGrafico.AsEnumerable().Sum(row => row.Field<decimal>("Ganancia"));
                    lbl_ganancias_totales.Text = TotalGanancias.ToString("N2", new CultureInfo("es-CO"));

                    //Total Costos
                    decimal TotalCosto = dtGrafico.AsEnumerable().Sum(row => row.Field<decimal>("CostoTotal"));
                    lbl_costos_totales.Text = TotalCosto.ToString("N2", new CultureInfo("es-CO"));

                    //Ventas por mes
                    var agrupado = from row in dtGrafico.AsEnumerable()
                                   let Mes = row.Field<DateTime>("CreationDate").Month
                                   group row by Mes into g
                                   select new
                                   {
                                       Mes = g.Key,
                                       Suma = g.Sum(r => r.Field<decimal>("VentaTotal"))
                                   };

                    Series serie = new Series("Valor");
                    serie.ChartType = SeriesChartType.Column;

                    foreach (var row in agrupado.OrderBy(x => x.Mes))
                    {
                        var x = row.Mes;
                        var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                        serie.Points.AddXY(x, y);
                    }

                    serie.IsValueShownAsLabel = false;
                    serie.LabelFormat = "#,0";
                    chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                    chart1.Series.Clear();
                    chart1.Series.Add(serie);
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    chart1.Invalidate();

                    chart1.ChartAreas[0].AxisX.CustomLabels.Clear();
                    foreach (var row in agrupado.OrderBy(x => x.Mes))
                    {
                        int mes = row.Mes;
                        string nombreMes = new CultureInfo("es-CO").DateTimeFormat.GetMonthName(mes);

                        CustomLabel label = new CustomLabel(mes - 0.5, mes + 0.5, nombreMes, 0, LabelMarkStyle.None);
                        chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);
                    }

                    //Ventas por producto
                    var agrupadoXproducto = (from row in dtGrafico.AsEnumerable()
                                            let IdProducto = row.Field<Int64>("IdProducto")
                                            group row by IdProducto into g
                                            select new
                                            {
                                                IdProducto = g.Key,
                                                NombreProducto = g.First().Field<string>("NombreProducto"),
                                                Suma = g.Sum(r => r.Field<decimal>("VentaTotal"))
                                            }).OrderByDescending(x => x.Suma)
                                             .Take(5);

                    Series serieXproducto = new Series("Valor");
                    serieXproducto.ChartType = SeriesChartType.Bar;
                    chart2.ChartAreas[0].AxisX.CustomLabels.Clear();
                    chart2.ChartAreas[0].AxisY.CustomLabels.Clear();
                    chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 7);
                    chart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 7);

                    serieXproducto.Points.Clear();

                    decimal maximo = agrupadoXproducto.Max(r => r.Suma);
                    chart2.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(maximo) * 1.1;
                    chart2.ChartAreas[0].AxisY.Minimum = 0;

                    int posicion = 1;
                    foreach (var row in agrupadoXproducto.OrderBy(x => x.IdProducto))
                    {
                        var x = row.IdProducto;
                        var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                        serieXproducto.Points.AddXY(posicion,y);

                        string etiqueta = $"{row.IdProducto} - {(row.NombreProducto.Length > 10 ? row.NombreProducto.Substring(0, 10) : row.NombreProducto)}";
                        chart2.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(
                        posicion - 0.5, posicion + 0.5, etiqueta, 0, LabelMarkStyle.None));

                        posicion++;
                    }

                    serieXproducto.IsValueShownAsLabel = true;
                    serieXproducto.LabelFormat = "#,0";
                    chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                    chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                    chart2.Series.Clear();
                    chart2.Series.Add(serieXproducto);
                    chart2.ChartAreas[0].RecalculateAxesScale();
                    chart2.Invalidate();

                    //Medio de pago
                    var agrupadoXMedioPago = from row in dtGrafico.AsEnumerable()
                                             let MedioPago = row.Field<string>("MedioPago")
                                             group row by MedioPago into g
                                             select new
                                             {
                                                 MedioPago = g.Key,
                                                 Suma = g.Sum(r => r.Field<decimal>("VentaTotal"))
                                             };

                    decimal totalGeneral = agrupadoXMedioPago.Sum(x => x.Suma);

                    Series serieXMedioPago = new Series("Valor");
                    serieXMedioPago.ChartType = SeriesChartType.Pie;

                    foreach (var row in agrupadoXMedioPago.OrderBy(x => x.MedioPago))
                    {
                        string medioPago = row.MedioPago;
                        decimal valor = row.Suma;

                        decimal porcentaje = (valor / totalGeneral) * 100;

                        int pointIndex = serieXMedioPago.Points.AddY(valor);
                        DataPoint punto = serieXMedioPago.Points[pointIndex];

                        punto.Label = $"{porcentaje.ToString("N2",new CultureInfo("es-CO"))}%";
                        punto.LabelForeColor = Color.White;
                        punto.LegendText = medioPago;
                    }

                    serieXMedioPago.IsValueShownAsLabel = true;

                    chart3.Series.Clear();
                    chart3.Series.Add(serieXMedioPago);
                    chart3.ChartAreas[0].RecalculateAxesScale();
                    chart3.Invalidate();

                    //Ventas diarias
                    var agrupadoXVentasDiarias = from row in dtGrafico.AsEnumerable()
                                                 let Day = row.Field<DateTime>("CreationDate").Day
                                                 group row by Day into g
                                                 select new
                                                 {
                                                     Day = g.Key,
                                                     Suma = g.Sum(r => r.Field<decimal>("VentaTotal"))
                                                 };

                    Series serieXVentasDiarias = new Series("Valor");
                    serieXVentasDiarias.ChartType = SeriesChartType.Line;

                    foreach (var row in agrupadoXVentasDiarias.OrderBy(x => x.Day))
                    {
                        var x = row.Day;
                        var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                        serieXVentasDiarias.Points.AddXY(x, y);
                    }

                    serieXVentasDiarias.IsValueShownAsLabel = true;
                    serieXVentasDiarias.LabelFormat = "#,0";
                    chart4.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                    chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart4.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

                    chart4.Series.Clear();
                    chart4.Series.Add(serieXVentasDiarias);
                }
                else
                {
                    lbl_ventas_totales.Text = "-";
                    lbl_ganancias_totales.Text = "-";
                    lbl_costos_totales.Text = "-";

                    chart1.Series.Clear();
                    chart2.Series.Clear();
                    chart3.Series.Clear();
                    chart4.Series.Clear();
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lbl_ventas_totales.Text = "-";
                lbl_ganancias_totales.Text = "-";
                lbl_costos_totales.Text = "-";

                chart1.Series.Clear();
                chart2.Series.Clear();
                chart3.Series.Clear();
                chart4.Series.Clear();
                MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Cursor = Cursors.Default;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private void btn_detalle_Click(object sender, EventArgs e)
        {
            if (_Reportes != null && !_Reportes.IsDisposed)
            {
                _Reportes.BringToFront();
                _Reportes.WindowState = FormWindowState.Normal;
                return;
            }

            _Reportes = _serviceProvider.GetRequiredService<Reportes>();
            _Reportes.Permisos = _Permisos;
            _Reportes.FechaIni = dtp_fecha_inicio.Value;
            _Reportes.FechaFin = dtp_fecha_fin.Value;
            _Reportes.FormClosed += (s, args) => _Reportes = null;
            _Reportes.Show();
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
    }
}
