using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces;

namespace sbx
{
    public partial class Login : Form
    {
        private readonly ILogin _ILogin;
        private Inicio? _Inicio;
        private readonly IServiceProvider _serviceProvider;

        public Login(ILogin login, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _ILogin = login;
            _serviceProvider = serviceProvider;
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
                    _Inicio = _serviceProvider.GetRequiredService<Inicio>();
                    _Inicio.Permisos = resp.Data;
                    _Inicio.FormClosed += (s, args) => _Inicio = null;
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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
