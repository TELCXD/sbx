using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.ReporteGeneral;
using System.Data;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace sbx
{
    public partial class ReporteGeneral : Form
    {
        private readonly IReporteGeneral _IReporteGeneral;
        private dynamic? _Permisos;
        private Reportes? _Reportes;
        private DetalleGastos? _DetalleGastos;
        private readonly IServiceProvider _serviceProvider;
        private Inventario? _Inventario;

        public ReporteGeneral(IReporteGeneral reporteGeneral, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IReporteGeneral = reporteGeneral;
            _serviceProvider = serviceProvider;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void ReporteGeneral_Load(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            double VentasTotales = 0;
            double ComprasTotales = 0;
            double GastosTotales = 0;
            double SalidasTotales = 0;

            chart1.Series.Clear();

            lbl_compras.Text = "0";
            lbl_ventas_totales.Text = "0";
            lbl_gastos.Text = "0";

            //VENTAS - COMPRAS - GASTOS - SALIDAS
            var respVentasComprasGastos = await _IReporteGeneral.BuscarReporteGeneral(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string jsonVentasComprasGastos = JsonConvert.SerializeObject(respVentasComprasGastos.Data);

            DataTable? dataTableVentasComprasGastos = JsonConvert.DeserializeObject<DataTable>(jsonVentasComprasGastos);

            if (dataTableVentasComprasGastos != null)
            {
                if (dataTableVentasComprasGastos.Rows.Count > 0)
                {
                    var agrupado = from row in dataTableVentasComprasGastos.AsEnumerable()
                                   let IdModulo = row.Field<Int64>("Id")
                                   group row by IdModulo into g
                                   select new
                                   {
                                       IdModulo = g.Key,
                                       Modulo = g.First().Field<string>("Modulo"),
                                       Suma = g.Sum(r => r.Field<double>("Valor"))
                                   };

                    Series serie = new Series("_");
                    serie.ChartType = SeriesChartType.Column;

                    int index = 0;
                    foreach (var row in agrupado.OrderBy(x => x.IdModulo))
                    {
                        var x = row.IdModulo;
                        var y = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                        serie.Points.AddXY(x, y);
                        serie.Points[index].AxisLabel = row.Modulo;
                        if (index == 0)
                        {
                            serie.Points[index].Color = Color.SteelBlue;
                        }
                        else if (index == 1)
                        {
                            serie.Points[index].Color = Color.SeaGreen;
                        }
                        else if (index == 2)
                        {
                            serie.Points[index].Color = Color.IndianRed;
                        }
                        else if (index == 3)
                        {
                            serie.Points[index].Color = Color.SteelBlue;
                        }

                        index++;
                    }

                    serie.IsValueShownAsLabel = true;
                    serie.LabelFormat = "#,0";
                    chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,0";
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                    chart1.Series.Clear();
                    chart1.Series.Add(serie);
                    chart1.ChartAreas[0].RecalculateAxesScale();
                    chart1.Invalidate();
                }
            }

            //VENTAS
            var respVentas = await _IReporteGeneral.BuscarReporteVentas(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string jsonVentas = JsonConvert.SerializeObject(respVentas.Data);

            DataTable? dataTableVentas = JsonConvert.DeserializeObject<DataTable>(jsonVentas);

            if (dataTableVentas != null)
            {
                if (dataTableVentas.Rows.Count > 0)
                {
                    VentasTotales = dataTableVentas.AsEnumerable().Sum(row => row.Field<double>("VentaNetaFinal"));
                    lbl_ventas_totales.Text = VentasTotales.ToString("N2", new CultureInfo("es-CO"));
                }
            }

            //COMPRAS
            var respCompras = await _IReporteGeneral.BuscarReporteCompras(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string jsonCompras = JsonConvert.SerializeObject(respCompras.Data);

            DataTable? dataTableCompras = JsonConvert.DeserializeObject<DataTable>(jsonCompras);

            if (dataTableCompras != null)
            {
                if (dataTableCompras.Rows.Count > 0)
                {
                    ComprasTotales = dataTableCompras.AsEnumerable().Sum(row => row.Field<double>("CostoNetoFinal"));
                    lbl_compras.Text = ComprasTotales.ToString("N2", new CultureInfo("es-CO"));
                }
            }

            //GASTOS
            var respGastos = await _IReporteGeneral.BuscarReporteGastos(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string jsonGastos = JsonConvert.SerializeObject(respGastos.Data);

            DataTable? dataTableGastos = JsonConvert.DeserializeObject<DataTable>(jsonGastos);

            if (dataTableGastos != null)
            {
                if (dataTableGastos.Rows.Count > 0)
                {
                    GastosTotales = dataTableGastos.AsEnumerable().Sum(row => row.Field<double>("ValorGasto"));
                    lbl_gastos.Text = GastosTotales.ToString("N2", new CultureInfo("es-CO"));
                }
            }

            //SALIDAS
            var respSalidas = await _IReporteGeneral.BuscarReporteSalidas(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            string jsonSalidas = JsonConvert.SerializeObject(respSalidas.Data);

            DataTable? dataTableSalidas = JsonConvert.DeserializeObject<DataTable>(jsonSalidas);

            if (dataTableSalidas != null)
            {
                if (dataTableSalidas.Rows.Count > 0)
                {
                    SalidasTotales = dataTableSalidas.AsEnumerable().Sum(row => row.Field<double>("ValorSalidas"));
                    lbl_salidas.Text = SalidasTotales.ToString("N2", new CultureInfo("es-CO"));
                }
            }

            //RESULTADO
            double Resultado = VentasTotales - (SalidasTotales + GastosTotales);

            lbl_resultado.Text = Resultado.ToString("N2", new CultureInfo("es-CO"));

            //if (Resultado > 0)
            //{
            //    lbl_resultado.ForeColor = Color.SeaGreen;
            //}
            //else
            //{
            //    lbl_resultado.ForeColor = Color.Red;
            //}

            this.Cursor = Cursors.Default;
        }

        private void btn_detalle_venta_Click(object sender, EventArgs e)
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

        private void btn_egresos_Click(object sender, EventArgs e)
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

        private void btn_detalle_compras_Click(object sender, EventArgs e)
        {
            if (_Inventario != null && !_Inventario.IsDisposed)
            {
                _Inventario.BringToFront();
                _Inventario.WindowState = FormWindowState.Normal;
                return;
            }

            _Inventario = _serviceProvider.GetRequiredService<Inventario>();
            _Inventario.Permisos = _Permisos;
            _Inventario.TipoMovimiento = "Entrada";
            _Inventario.FechaIni = dtp_fecha_inicio.Value;
            _Inventario.FechaFin = dtp_fecha_fin.Value;
            _Inventario.FormClosed += (s, args) => _Inventario = null;
            _Inventario.Show();
        }

        private void btn_salidas_Click(object sender, EventArgs e)
        {
            if (_Inventario != null && !_Inventario.IsDisposed)
            {
                _Inventario.BringToFront();
                _Inventario.WindowState = FormWindowState.Normal;
                return;
            }

            _Inventario = _serviceProvider.GetRequiredService<Inventario>();
            _Inventario.Permisos = _Permisos;
            _Inventario.TipoMovimiento = "Salida";
            _Inventario.FechaIni = dtp_fecha_inicio.Value;
            _Inventario.FechaFin = dtp_fecha_fin.Value;
            _Inventario.FormClosed += (s, args) => _Inventario = null;
            _Inventario.Show();
        }
    }
}
