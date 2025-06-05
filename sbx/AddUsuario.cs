using sbx.core.Entities.usuario;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.Rol;
using sbx.core.Interfaces.Usuario;
using System.ComponentModel.DataAnnotations;

namespace sbx
{
    public partial class AddUsuario : Form
    {
        private int _Id_Usuario;
        private dynamic? _Permisos;
        private readonly IIdentificationType _IIdentificationType;
        private readonly IUsuario _IUsuario;
        private readonly IRol _IRol;
        private string PasswordHash = "";

        public AddUsuario(IIdentificationType identificationType, IUsuario usuario, IRol rol)
        {
            InitializeComponent();
            _IIdentificationType = identificationType;
            _IUsuario = usuario;
            _IRol = rol;
        }

        public int Id_Usuario
        {
            get => _Id_Usuario;
            set => _Id_Usuario = value;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void AddUsuario_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "clientes":
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

        private async Task CargaDatosIniciales()
        {
            var resp = await _IIdentificationType.ListIdentificationType();
            cbx_tipo_identificacion.DataSource = resp.Data;
            cbx_tipo_identificacion.ValueMember = "IdIdentificationType";
            cbx_tipo_identificacion.DisplayMember = "IdentificationType";
            cbx_tipo_identificacion.SelectedIndex = 0;

            resp = await _IRol.List();
            cbx_rol.DataSource = resp.Data;
            cbx_rol.ValueMember = "IdRole";
            cbx_rol.DisplayMember = "NameRole";
            cbx_rol.SelectedIndex = 0;

            cbx_estado.SelectedIndex = 0;

            if (Id_Usuario > 0)
            {
                resp = await _IUsuario.List(Id_Usuario);

                if (resp.Data != null)
                {
                    cbx_tipo_identificacion.SelectedValue = resp.Data[0].IdIdentificationType;
                    txt_numero_documento.Text = resp.Data[0].IdentificationNumber;
                    txt_nombre.Text = resp.Data[0].Name;
                    txt_apellido.Text = resp.Data[0].LastName;
                    txt_telefono.Text = resp.Data[0].TelephoneNumber;
                    txt_email.Text = resp.Data[0].Email;
                    cbx_estado.SelectedIndex = resp.Data[0].Estado == true ? 1 : 0;
                    txt_usuario.Text = resp.Data[0].UserName;
                    //txt_contrasena.Text = resp.Data[0].Password;
                    PasswordHash = resp.Data[0].Password;
                    cbx_rol.SelectedValue = resp.Data[0].IdRole;
                    dtp_fecha_cumpleaños.Value = resp.Data[0].BirthDate;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_numero_documento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_numero_documento.Text)) { errorProvider1.SetError(txt_numero_documento, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_telefono.Text)) { errorProvider1.SetError(txt_telefono, "Ingrese un valor numerico"); valido++; }

            if (valido > 0) { return; }

            var Datos = new usuarioEntitie
            {
                IdUser = Id_Usuario,
                IdIdentificationType = Convert.ToInt32(cbx_tipo_identificacion.SelectedValue),
                IdentificationNumber = txt_numero_documento.Text.Trim(),
                Name = txt_nombre.Text.Trim(),
                LastName = txt_apellido.Text.Trim(),
                BirthDate = dtp_fecha_cumpleaños.Value,
                IdCountry = 1,
                IdDepartament = 1,
                IdCity = 1,
                TelephoneNumber = txt_telefono.Text,
                Email = txt_email.Text.Trim(),
                IdRole = Convert.ToInt32(cbx_rol.SelectedValue),
                UserName = txt_usuario.Text.Trim(),
                Password = txt_contrasena.Text.Trim(),
                PasswordHash = this.PasswordHash,
                Active = cbx_estado.Text == "Activo" ? 1 : 0,
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
            {
                var Exist = await _IUsuario.ExisteNumeroDocumento(txt_numero_documento.Text.Trim(), Id_Usuario);
                if (Exist) { errorProvider1.SetError(txt_numero_documento, "Numero documento ya existe"); valido++; }
                Exist = await _IUsuario.ExisteTelefono(txt_telefono.Text.Trim(), Id_Usuario);
                if (Exist) { errorProvider1.SetError(txt_telefono, "Telefono ya existe"); valido++; }
                Exist = await _IUsuario.ExisteEmail(txt_email.Text.Trim(), Id_Usuario);
                if (Exist) { errorProvider1.SetError(txt_email, "Email ya existe"); valido++; }
                if (Id_Usuario == 0 && txt_contrasena.Text == "") { errorProvider1.SetError(txt_contrasena, "Debe ingresar contraseña"); valido++; }
                if (valido > 0) { return; }

                var resp = await _IUsuario.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
}
