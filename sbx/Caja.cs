using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Caja;
using System.Globalization;

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

        public Caja(ICaja caja, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _ICaja = caja;
            _serviceProvider = serviceProvider;
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
    }
}
