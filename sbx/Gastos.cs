using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.Gastos;
using System.Data;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace sbx
{
    public partial class Gastos : Form
    {
        private DetalleGastos? _DetalleGastos;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGastos _IGastos;
        private dynamic? _Permisos;

        public Gastos(IServiceProvider serviceProvider, IGastos gastos)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IGastos = gastos;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Gastos_Load(object sender, EventArgs e)
        {
            await Buscar();
        }

        private void btn_detalle_Click(object sender, EventArgs e)
        {
            if (_DetalleGastos != null && !_DetalleGastos.IsDisposed)
            {
                _DetalleGastos.BringToFront();
                _DetalleGastos.WindowState = FormWindowState.Normal;
                return;
            }

            _DetalleGastos = _serviceProvider.GetRequiredService<DetalleGastos>();
            _DetalleGastos.Permisos = _Permisos;
            _DetalleGastos.FechaIni = dtp_fecha_inicio.Value;
            _DetalleGastos.FechaFin = dtp_fecha_fin.Value;
            _DetalleGastos.FormClosed += (s, args) => _DetalleGastos = null;
            _DetalleGastos.Show();
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            var resp = await _IGastos.BuscarReporte(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string json = JsonConvert.SerializeObject(resp.Data);

            DataTable? dataTable = JsonConvert.DeserializeObject<DataTable>(json);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //Gastos totales
                    double gastos_totales = dataTable.AsEnumerable().Sum(row => row.Field<double>("ValorGasto"));
                    lbl_gastos_totales.Text = gastos_totales.ToString("N2", new CultureInfo("es-CO"));

                    //Gastos por mes
                    var agrupado = from row in dataTable.AsEnumerable()
                                   let Mes = row.Field<DateTime>("CreationDate").Month
                                   group row by Mes into g
                                   select new
                                   {
                                       Mes = g.Key,
                                       Suma = g.Sum(r => r.Field<double>("ValorGasto"))
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

                    chart1.ChartAreas[0].AxisX.CustomLabels.Clear();
                    chart1.ChartAreas[0].AxisY.CustomLabels.Clear();

                    chart1.Series.Clear();
                    chart1.Series.Add(serie);

                    chart1.ChartAreas[0].RecalculateAxesScale();
                    chart1.Invalidate();

                    foreach (var row in agrupado.OrderBy(x => x.Mes))
                    {
                        int mes = row.Mes;
                        string nombreMes = new CultureInfo("es-CO").DateTimeFormat.GetMonthName(mes);

                        CustomLabel label = new CustomLabel(mes - 0.5, mes + 0.5, nombreMes, 0, LabelMarkStyle.None);
                        chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);
                    }

                    //Gastos
                    var agrupadoXGasto = from row in dataTable.AsEnumerable()
                                             let Subcategoria = row.Field<string>("Subcategoria")
                                             group row by Subcategoria into g
                                             select new
                                             {
                                                 Subcategoria = g.Key,
                                                 Suma = g.Sum(r => r.Field<double>("ValorGasto"))
                                             };

                    double totalGeneral = agrupadoXGasto.Sum(x => x.Suma);

                    Series serieXGasto = new Series("Valor");
                    serieXGasto.ChartType = SeriesChartType.Pie;

                    foreach (var row in agrupadoXGasto.OrderBy(x => x.Subcategoria))
                    {
                        string subcategoria = row.Subcategoria;
                        double valor = row.Suma;

                        double porcentaje = (valor / totalGeneral) * 100;

                        int pointIndex = serieXGasto.Points.AddY(valor);
                        DataPoint punto = serieXGasto.Points[pointIndex];

                        punto.Label = $"{porcentaje.ToString("N2", new CultureInfo("es-CO"))}%";
                        punto.LabelForeColor = Color.White;
                        punto.LegendText = subcategoria;
                    }

                    serieXGasto.IsValueShownAsLabel = true;

                    chart2.Series.Clear();
                    chart2.Series.Add(serieXGasto);
                    chart2.ChartAreas[0].RecalculateAxesScale();
                    chart2.Invalidate();

                    //Gastos por detalle
                    var agrupadoXDetalle = from row in dataTable.AsEnumerable()
                                                 let Dtll = row.Field<string>("Detalle")
                                                 group row by Dtll into g
                                                 select new
                                                 {
                                                     Dtll = g.Key,
                                                     Suma = g.Sum(r => r.Field<double>("ValorGasto"))
                                                 };

                    Series serieXDetalle = new Series("Valor");
                    serieXDetalle.ChartType = SeriesChartType.Area;

                    int index = 0;
                    foreach (var row in agrupadoXDetalle.OrderBy(x => x.Dtll))
                    {
                        var x = row.Dtll;
                        var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                        serieXDetalle.Points.AddXY(index, y);
                        serieXDetalle.Points[index].AxisLabel = x;
                        index++;
                    }

                    serieXDetalle.IsValueShownAsLabel = false;
                    serieXDetalle.LabelFormat = "#,0";
                    chart3.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                    chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;


                    chart3.Series.Clear();
                    chart3.Series.Add(serieXDetalle);
                    chart3.ChartAreas[0].RecalculateAxesScale();
                    chart3.Invalidate();
                }
                else
                {
                    lbl_gastos_totales.Text = "-";

                    chart1.Series.Clear();
                    chart2.Series.Clear();
                    chart3.Series.Clear();
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lbl_gastos_totales.Text = "-";

                chart1.Series.Clear();
                chart2.Series.Clear();
                chart3.Series.Clear();
                MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Cursor = Cursors.Default;
        }
    }
}
