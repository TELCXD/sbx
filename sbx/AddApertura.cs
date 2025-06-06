using sbx.core.Entities.Caja;
using sbx.core.Interfaces.Caja;
using System.Globalization;

namespace sbx
{
    public partial class AddApertura : Form
    {
        private readonly ICaja _ICaja;
        char decimalSeparator = ',';
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        public delegate void ConfirmacionApertura(bool CajaAbierta);
        public event ConfirmacionApertura ConfirmacionAperturaCaja;

        public AddApertura(ICaja caja, IServiceProvider serviceProvider)
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

        private void AddApertura_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
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

        private void txt_monto_inicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_apertura_Click(object sender, EventArgs e)
        {
            await AbrirCaja();
        }

        private async Task AbrirCaja()
        {
            if (txt_monto_inicial.Text.Trim() != "")
            {
                var estadoCaja = await _ICaja.EstadoCaja(Convert.ToInt32(_Permisos?[0]?.IdUser));
                if (estadoCaja.Data != null)
                {
                    CajaEntitie Apertura;

                    if (estadoCaja.Data.Count > 0)
                    {
                        if (estadoCaja.Data[0].Estado == "ABIERTA")
                        {
                            MessageBox.Show("Caja ya esta abierta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            Apertura = new CajaEntitie
                            {
                                IdApertura_Cierre_caja = 0,
                                MontoInicialDeclarado = Convert.ToDecimal(txt_monto_inicial.Text, new CultureInfo("es-CO")),
                                IdUserAction = Convert.ToInt32(_Permisos?[0]?.IdUser),
                                Estado = "ABIERTA"
                            };
                        }
                    }
                    else
                    {
                        Apertura = new CajaEntitie
                        {
                            IdApertura_Cierre_caja = 0,
                            MontoInicialDeclarado = Convert.ToDecimal(txt_monto_inicial.Text, new CultureInfo("es-CO")),
                            IdUserAction = Convert.ToInt32(_Permisos?[0]?.IdUser),
                            Estado = "ABIERTA"
                        };
                    }

                    var resp = await _ICaja.CreateUpdate(Apertura);

                    if (resp != null)
                    {
                        if (resp.Flag == true)
                        {
                            MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ConfirmacionAperturaCaja(true);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ConfirmacionAperturaCaja(false);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("NO se obtuvo informacion de cajas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar monto inicial", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddApertura_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmacionAperturaCaja(false);
        }
    }
}
