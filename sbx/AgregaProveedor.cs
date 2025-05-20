using sbx.core.Entities.Proveedor;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.Proveedor;
using System.ComponentModel.DataAnnotations;

namespace sbx
{
    public partial class AgregaProveedor : Form
    {
        private dynamic? _Permisos;
        private readonly IProveedor _IProveedor;
        private readonly IIdentificationType _IIdentificationType;
        private int _Id_Proveedor;

        public AgregaProveedor(IProveedor proveedor, IIdentificationType identificationType)
        {
            InitializeComponent();
            _IProveedor = proveedor;
            _IIdentificationType = identificationType;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Proveedor
        {
            get => _Id_Proveedor;
            set => _Id_Proveedor = value;
        }

        private async void AgregaProveedor_Load(object sender, EventArgs e)
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
                        case "proveedor":
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

            cbx_estado.SelectedIndex = 0;

            if (Id_Proveedor > 0)
            {
                resp = await _IProveedor.List(Id_Proveedor);

                if (resp.Data != null)
                {
                    cbx_tipo_identificacion.SelectedValue = resp.Data[0].IdIdentificationType;
                    txt_numero_documento.Text = resp.Data[0].NumeroDocumento;
                    txt_nombre_razon_social.Text = resp.Data[0].NombreRazonSocial;
                    txt_direccion.Text = resp.Data[0].Direccion;
                    txt_telefono.Text = resp.Data[0].Telefono;
                    txt_email.Text = resp.Data[0].Email;
                    cbx_estado.SelectedIndex = resp.Data[0].Estado == true ? 0 : 1;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_numero_documento.Text)) { errorProvider1.SetError(txt_numero_documento, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_telefono.Text)) { errorProvider1.SetError(txt_telefono, "Ingrese un valor numerico"); valido++; }

            if (valido > 0) { return; }

            var Datos = new ProveedorEntitie
            {
                IdProveedor = Id_Proveedor,
                IdIdentificationType = Convert.ToInt32(cbx_tipo_identificacion.SelectedValue),
                NumeroDocumento = txt_numero_documento.Text,
                NombreRazonSocial = txt_nombre_razon_social.Text,
                Direccion = txt_direccion.Text,
                Telefono = txt_telefono.Text,
                Email = txt_email.Text,
                Estado = cbx_estado.Text == "Activo" ? 1 : 0
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
            {
                var Exist = await _IProveedor.ExisteNumeroDocumento(txt_numero_documento.Text.Trim(), Id_Proveedor);
                if (Exist) { errorProvider1.SetError(txt_numero_documento, "Numero documento ya existe"); valido++; }
                Exist = await _IProveedor.ExisteNombreRazonSocial(txt_nombre_razon_social.Text.Trim(), Id_Proveedor);
                if (Exist) { errorProvider1.SetError(txt_nombre_razon_social, "Nombre ya existe"); valido++; }
                Exist = await _IProveedor.ExisteTelefono(txt_telefono.Text.Trim(), Id_Proveedor);
                if (Exist) { errorProvider1.SetError(txt_telefono, "Telefono ya existe"); valido++; }
                Exist = await _IProveedor.ExisteEmail(txt_email.Text.Trim(), Id_Proveedor);
                if (Exist) { errorProvider1.SetError(txt_email, "Email ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _IProveedor.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
            else
            {
                foreach (var validationResult in validationResults)
                {
                    if (validationResult.MemberNames.Contains("NumeroDocumento"))
                    {
                        errorProvider1.SetError(txt_numero_documento, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("NombreRazonSocial"))
                    {
                        errorProvider1.SetError(txt_nombre_razon_social, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Telefono"))
                    {
                        errorProvider1.SetError(txt_telefono, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Email"))
                    {
                        errorProvider1.SetError(txt_email, validationResult.ErrorMessage);
                    }
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
    }
}
