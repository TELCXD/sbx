using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.Caja;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.Caja;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Venta;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class Caja : Form
    {
        private readonly ICaja _ICaja;
        private dynamic? _Permisos;
        private AddApertura? _AddApertura;
        private readonly IServiceProvider _serviceProvider;
        private AddCierre? _AddCierre;
        private bool CajaAperturada = false;
        private readonly IParametros _IParametros;

        public Caja(ICaja caja, IServiceProvider serviceProvider, IParametros iParametros)
        {
            InitializeComponent();
            _ICaja = caja;
            _serviceProvider = serviceProvider;
            _IParametros = iParametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "caja":
                            btn_apertura.Enabled = item.ToCreate == 1 ? true : false;
                            btn_cierre.Enabled = item.ToCreate == 1 ? true : false;
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

        private async void btn_buscar_Click(object sender, EventArgs e)
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

            var resp = await _ICaja.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, dtp_F_ini.Value, dtp_F_f.Value, Convert.ToInt32(_Permisos?[0]?.IdUser), _Permisos?[0]?.NameRole);

            dtg_aperturas_cierres_caja.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_aperturas_cierres_caja.Rows.Add(
                            item.IdApertura_Cierre_caja,
                            item.IdUserAction,
                            item.Usuario,
                            item.FechaHoraApertura?.ToString(),
                            item.MontoInicialDeclarado.ToString("N2", new CultureInfo("es-CO")),
                            item.FechaHoraCierre?.ToString(),
                            (item.MontoFinalDeclarado ?? 0).ToString("N2", new CultureInfo("es-CO")),
                            (item.VentasTotales ?? 0).ToString("N2", new CultureInfo("es-CO")),
                            (item.PagosEnEfectivo ?? 0).ToString("N2", new CultureInfo("es-CO")),
                            (item.Diferencia ?? 0).ToString("N2", new CultureInfo("es-CO")),
                            item.Estado?.ToString());
                    }
                }
            }
        }

        private void btn_apertura_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                _AddApertura.Permisos = _Permisos;
                _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                _AddApertura.ShowDialog();
            }
        }

        private void btn_cierre_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AddCierre = _serviceProvider.GetRequiredService<AddCierre>();
                _AddCierre.Permisos = _Permisos;
                _AddCierre.FormClosed += (s, args) => _AddCierre = null;
                _AddCierre.ShowDialog();
            }
        }

        private void ConfirmacionAperturaCaja(bool CajaAbierta)
        {
            CajaAperturada = CajaAbierta;
        }

        private async void btn_imprimir_Click(object sender, EventArgs e)
        {
            if (dtg_aperturas_cierres_caja.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de imprimir?",
                        "Confirmar cancelacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_aperturas_cierres_caja.SelectedRows.Count > 0)
                    {
                        var row = dtg_aperturas_cierres_caja.SelectedRows[0];
                        if (row.Cells["cl_IdApertura_Cierre_caja"].Value != null)
                        {
                            var Id_Cierre_Apertura = Convert.ToInt32(row.Cells["cl_IdApertura_Cierre_caja"].Value);

                            var DataCaja = await _ICaja.ListForId(Id_Cierre_Apertura);

                            if (DataCaja.Data != null)
                            {
                                if (DataCaja.Data.Count > 0) 
                                {
                                    CajaEntitie cajaEntitie = new CajaEntitie();

                                    cajaEntitie.IdApertura_Cierre_caja = DataCaja.Data[0].IdApertura_Cierre_caja;
                                    cajaEntitie.Usuario = DataCaja.Data[0].Usuario;
                                    cajaEntitie.FechaHoraApertura = DataCaja.Data[0].FechaHoraApertura;   
                                    if(DataCaja.Data[0].FechaHoraCierre != null)
                                    {
                                        cajaEntitie.FechaHoraCierre = DataCaja.Data[0].FechaHoraCierre;
                                    }
                                    cajaEntitie.MontoInicialDeclarado = DataCaja.Data[0].MontoInicialDeclarado;
                                    cajaEntitie.VentasTotales = DataCaja.Data[0].VentasTotales;
                                    cajaEntitie.PagosEnEfectivo = DataCaja.Data[0].PagosEnEfectivo;
                                    cajaEntitie.MontoFinalDeclarado = DataCaja.Data[0].MontoFinalDeclarado;
                                    cajaEntitie.Diferencia = DataCaja.Data[0].Diferencia;
                                    cajaEntitie.Estado = DataCaja.Data[0].Estado;

                                    var DataParametros = await _IParametros.List("");

                                    if (DataParametros.Data != null)
                                    {
                                        if (DataParametros.Data.Count > 0)
                                        {
                                            int ANCHO_TIRILLA = 0;
                                            string Impresora = "";
                                            int LineasAbajo = 0;

                                            foreach (var itemParametros in DataParametros.Data)
                                            {
                                                switch (itemParametros.Nombre)
                                                {
                                                    case "Ancho tirilla":
                                                        ANCHO_TIRILLA = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    case "Impresora":
                                                        Impresora = itemParametros.Value;
                                                        break;
                                                    case "lineas abajo de la tirilla":
                                                        LineasAbajo = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaCajaCierre(cajaEntitie, ANCHO_TIRILLA);

                                            string carpetaCaja = "Caja";
                                            if (!Directory.Exists(carpetaCaja))
                                            {
                                                Directory.CreateDirectory(carpetaCaja);
                                            }

                                            File.WriteAllText(Path.Combine(carpetaCaja, $"Caja_{cajaEntitie.IdApertura_Cierre_caja}.txt"),
                                                                      tirilla.ToString(),
                                                                      Encoding.UTF8);

                                            RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString(), LineasAbajo);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccinar una fila", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
