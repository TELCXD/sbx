using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.Parametros;
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
        private readonly IParametros _IParametros;
        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";
        public ReporteGeneral(IReporteGeneral reporteGeneral, IServiceProvider serviceProvider, IParametros iParametros)
        {
            InitializeComponent();
            _IReporteGeneral = reporteGeneral;
            _serviceProvider = serviceProvider;
            _IParametros = iParametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void ReporteGeneral_Load(object sender, EventArgs e)
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            decimal VentasTotales = 0;
            decimal ComprasTotales = 0;
            decimal GastosTotales = 0;
            decimal SalidasTotales = 0;

            chart1.Series.Clear();

            lbl_compras.Text = "0";
            lbl_ventas_totales.Text = "0";
            lbl_gastos.Text = "0";

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

            //VENTAS - COMPRAS - GASTOS - SALIDAS
            var respVentasComprasGastos = await _IReporteGeneral.BuscarReporteGeneral(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            DataTable dtGrafico = new DataTable();
            dtGrafico.Columns.Add("Id", typeof(Int64));
            dtGrafico.Columns.Add("Modulo", typeof(string));
            dtGrafico.Columns.Add("Valor", typeof(decimal));

            if (respVentasComprasGastos.Data != null)
            {
                if (respVentasComprasGastos.Data.Count > 0)
                {
                    foreach (var item in respVentasComprasGastos.Data)
                    {
                        switch (item.Modulo)
                        {
                            case "COMPRAS":

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

                                if (item.Estado == "") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                                if (item.Estado == "") { Subtotal += Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad); }
                                SubtotalLinea = Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad);
                                if (item.Estado == "") { Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento)); }
                                DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                if (item.NombreTributo == "INC Bolsas")
                                {
                                    if (item.Estado == "") { Impuesto += Convert.ToDecimal(item.Impuesto); }
                                    ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                                }
                                else
                                {
                                    if (item.Estado == "") { Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto)); }
                                    ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                }

                                if (item.Estado == "")
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
                                    1,
                                    "COMPRAS",
                                    TotalLinea
                                    );

                                break;
                            case "VENTAS":

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

                                if (item.Estado == "FACTURADA") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                                if (item.Estado == "FACTURADA") { Subtotal += Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad); }
                                SubtotalLinea = Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad);
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
                                    2,
                                    "VENTAS",
                                    TotalLinea
                                    );

                                break;
                            case "GASTOS":

                                TotalLinea = 0;

                                TotalLinea = Convert.ToDecimal(item.Valor);

                                dtGrafico.Rows.Add(
                                    3,
                                    "GASTOS",
                                    TotalLinea
                                    );

                                break;
                            case "SALIDAS":

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

                                if (item.Estado == "") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                                if (item.Estado == "") { Subtotal += Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad); }
                                SubtotalLinea = Convert.ToDecimal(item.Valor) * Convert.ToDecimal(item.Cantidad);
                                if (item.Estado == "") { Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento)); }
                                DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                if (item.NombreTributo == "INC Bolsas")
                                {
                                    if (item.Estado == "") { Impuesto += Convert.ToDecimal(item.Impuesto); }
                                    ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                                }
                                else
                                {
                                    if (item.Estado == "") { Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto)); }
                                    ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                }

                                if (item.Estado == "")
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
                                    4,
                                    "SALIDAS",
                                    TotalLinea
                                    );

                                break;
                            default:
                                break;
                        }
                    }                
                }
            }

            if (dtGrafico != null)
            {
                if (dtGrafico.Rows.Count > 0)
                {
                    var agrupado = from row in dtGrafico.AsEnumerable()
                                   let IdModulo = row.Field<Int64>("Id")
                                   group row by IdModulo into g
                                   select new
                                   {
                                       IdModulo = g.Key,
                                       Modulo = g.First().Field<string>("Modulo"),
                                       Suma = g.Sum(r => r.Field<decimal>("Valor"))
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

                        switch (row.Modulo)
                        {
                            case "COMPRAS":
                                ComprasTotales = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                                break;
                            case "VENTAS":
                                VentasTotales = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                                break;
                            case "GASTOS":
                                GastosTotales = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                                break;
                            case "SALIDAS":
                                SalidasTotales = Convert.ToDecimal(row.Suma, new CultureInfo("es-CO"));
                                break;
                            default:
                                break;
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
            lbl_ventas_totales.Text = VentasTotales.ToString("N2", new CultureInfo("es-CO"));

            //COMPRAS
            lbl_compras.Text = ComprasTotales.ToString("N2", new CultureInfo("es-CO"));

            //GASTOS
            lbl_gastos.Text = GastosTotales.ToString("N2", new CultureInfo("es-CO"));

            //SALIDAS
            lbl_salidas.Text = SalidasTotales.ToString("N2", new CultureInfo("es-CO"));

            //RESULTADO
            decimal Resultado = VentasTotales - (SalidasTotales + GastosTotales);

            lbl_resultado.Text = Resultado.ToString("N2", new CultureInfo("es-CO"));

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
