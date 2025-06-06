using sbx.core.Entities.Pagos;
using sbx.core.Interfaces.Pago;
using System.Globalization;

namespace sbx
{
    public partial class AddPagosEfectivo : Form
    {
        private readonly IPagosEfectivo _IPagosEfectivo;
        char decimalSeparator = ',';
        private dynamic? _Permisos;
        private int _Id_Pago;

        public AddPagosEfectivo(IPagosEfectivo pagosEfectivo)
        {
            InitializeComponent();
            _IPagosEfectivo = pagosEfectivo;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Pago
        {
            get => _Id_Pago;
            set => _Id_Pago = value;
        }

        private void AddPagosEfectivo_Load(object sender, EventArgs e)
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
                        case "ventas":
                           btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
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

        private void txt_valor_pago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_valor_pago.Text.Trim() == "" ){ errorProvider1.SetError(txt_valor_pago, "Debe ingresar el valor del pago en efectivo"); return; }
            if (txt_descripcion.Text.Trim() == "") { errorProvider1.SetError(txt_descripcion, "Debe ingresar la descripcion"); return; }

            PagosEfectivoEntitie pagosEfectivo = new PagosEfectivoEntitie
            {
                ValorPago = Convert.ToDecimal(txt_valor_pago.Text, new CultureInfo("es-CO")),
                DescripcionPago = txt_descripcion.Text,
                IdPago = Id_Pago
            };

            var resp = await _IPagosEfectivo.CreateUpdate(pagosEfectivo, Convert.ToInt32(_Permisos?[0]?.IdUser));

            if (resp != null)
            {
                if (resp.Flag == true)
                {
                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
