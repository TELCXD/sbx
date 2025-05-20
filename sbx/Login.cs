using sbx.core.Entities;
using sbx.core.Interfaces;
using System.Windows.Forms;

namespace sbx
{
    public partial class Login : Form
    {
        private readonly ILogin _ILogin;
        private readonly Inicio _Inicio;

        public Login(ILogin login, Inicio inicio)
        {
            InitializeComponent();
            _ILogin = login;
            _Inicio = inicio;   
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            await mtd_login();
        }

        private async void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                await mtd_login();
            }
        }
        #region login
        private async Task mtd_login()
        {
            btn_login.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            string nombreUsuario = txt_user.Text.Trim();
            string contrasena = txt_password.Text.Trim();
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                errorProvider1.SetError(txt_user, "Debe ingresar usuario");
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                errorProvider1.SetError(txt_password, "Debe ingresar contraseña");
            }

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                this.Cursor = Cursors.Default;
                return;
            }

            var resp = await _ILogin.ValidarUsuario(nombreUsuario, contrasena);

            if (resp != null)
            {
                if (resp.Flag == true && resp.Message == "Credenciales correctas")
                {
                    _Inicio.Permisos = resp.Data;
                    _Inicio.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            btn_login.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
