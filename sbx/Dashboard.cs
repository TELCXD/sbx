using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.Dashboard;
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
        public Dashboard(IDashboard dashboard, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IDashboard = dashboard;
            _serviceProvider = serviceProvider;
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            var resp = await _IDashboard.Buscar(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string json = JsonConvert.SerializeObject(resp.Data);

            DataTable? dataTable = JsonConvert.DeserializeObject<DataTable>(json);

            if (dataTable != null)
            {
                //Ventas totales
                double VentaNetaFinal = dataTable.AsEnumerable().Sum(row => row.Field<double>("VentaNetaFinal"));
                lbl_ventas_totales.Text = VentaNetaFinal.ToString("N2", new CultureInfo("es-CO"));

                //Total ganancias
                double TotalGanancias = dataTable.AsEnumerable().Sum(row => row.Field<double>("GananciaBruta"));
                lbl_ganancias_totales.Text = TotalGanancias.ToString("N2", new CultureInfo("es-CO"));

                //Margen promedio
                double MargenPromedio = dataTable.AsEnumerable().Average(row => row.Field<double>("MargenPorcentaje"));
                lbl_margen_porcentaje.Text = MargenPromedio.ToString("N2", new CultureInfo("es-CO"));

                //Ventas por mes
                var agrupado = from row in dataTable.AsEnumerable()
                               let Mes = row.Field<DateTime>("CreationDate").Month
                               group row by Mes into g
                               select new
                               {
                                   Mes = g.Key,
                                   Suma = g.Sum(r => r.Field<double>("VentaNetaFinal"))
                               };

                Series serie = new Series("Valor");
                serie.ChartType = SeriesChartType.Column;

                foreach (var row in agrupado.OrderBy(x => x.Mes))
                {
                    var x = row.Mes;
                    var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                    serie.Points.AddXY(x, y);
                }

                serie.IsValueShownAsLabel = true;
                serie.LabelFormat = "#,0";
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                chart1.Series.Clear();
                chart1.Series.Add(serie);

                //Ventas por producto
                var agrupadoXproducto = from row in dataTable.AsEnumerable()
                                        let IdProducto = row.Field<Int64>("IdProducto")
                                        group row by IdProducto into g
                                        select new
                                        {
                                            IdProducto = g.Key,
                                            Suma = g.Sum(r => r.Field<double>("VentaNetaFinal"))
                                        };

                Series serieXproducto = new Series("Valor");
                serieXproducto.ChartType = SeriesChartType.Bar;

                foreach (var row in agrupadoXproducto.OrderBy(x => x.IdProducto))
                {
                    var x = row.IdProducto;
                    var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                    serieXproducto.Points.AddXY(x, y);
                }

                serieXproducto.IsValueShownAsLabel = true;
                serieXproducto.LabelFormat = "#,0";
                chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                chart2.Series.Clear();
                chart2.Series.Add(serieXproducto);

                //Medio de pago
                var agrupadoXMedioPago = from row in dataTable.AsEnumerable()
                                         let MedioPago = row.Field<string>("MedioPago")
                                         group row by MedioPago into g
                                         select new
                                         {
                                             MedioPago = g.Key,
                                             Suma = g.Sum(r => r.Field<double>("VentaNetaFinal"))
                                         };

                Series serieXMedioPago = new Series("Valor");
                serieXMedioPago.ChartType = SeriesChartType.Pie;

                foreach (var row in agrupadoXMedioPago.OrderBy(x => x.MedioPago))
                {
                    var x = row.MedioPago;
                    var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                    serieXMedioPago.Points.AddXY(x, y);
                }

                serieXMedioPago.IsValueShownAsLabel = false;
                serieXMedioPago["PieLabelStyle"] = "Disabled";
                //serieXMedioPago.LabelFormat = "#,0";
                //chart3.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";

                chart3.Series.Clear();
                chart3.Series.Add(serieXMedioPago);

                //Ventas diarias
                var agrupadoXVentasDiarias = from row in dataTable.AsEnumerable()
                                             let Day = row.Field<DateTime>("CreationDate").Day
                                             group row by Day into g
                                             select new
                                             {
                                                 Day = g.Key,
                                                 Suma = g.Sum(r => r.Field<double>("VentaNetaFinal"))
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
            _Reportes.FormClosed += (s, args) => _Reportes = null;
            _Reportes.Show();
        }
    }
}
