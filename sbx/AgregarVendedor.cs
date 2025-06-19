using sbx.core.Entities.Vendedor;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.Vendedor;
using System.ComponentModel.DataAnnotations;

namespace sbx
{
    public partial class AgregarVendedor : Form
    {
        private readonly IIdentificationType _IIdentificationType;
        private readonly IVendedor _IVendedor;

        public AgregarVendedor(IIdentificationType identificationType, IVendedor vendedor)
        {
            InitializeComponent();
            _IIdentificationType = identificationType;
            _IVendedor = vendedor;
        }

        private dynamic? _Permisos;
        private int _Id_Vendedor;

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Vendedor
        {
            get => _Id_Vendedor;
            set => _Id_Vendedor = value;
        }

        private async void AgregarVendedor_Load(object sender, EventArgs e)
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
                        case "vendedores":
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

            if (Id_Vendedor > 0)
            {
                resp = await _IVendedor.List(Id_Vendedor);

                if (resp.Data != null)
                {
                    cbx_tipo_identificacion.SelectedValue = resp.Data[0].IdIdentificationType;
                    txt_numero_documento.Text = resp.Data[0].NumeroDocumento;
                    txt_nombre.Text = resp.Data[0].Nombre;
                    txt_apellidos.Text = resp.Data[0].Apellido;
                    txt_telefono.Text = resp.Data[0].Telefono;
                    txt_email.Text = resp.Data[0].Email;
                    txt_direccion.Text = resp.Data[0].Direccion;
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

            var Datos = new VendedorEntitie
            {
                IdVendedor = Id_Vendedor,
                IdIdentificationType = Convert.ToInt32(cbx_tipo_identificacion.SelectedValue),
                NumeroDocumento = txt_numero_documento.Text.Trim(),
                Nombre = txt_nombre.Text.Trim(),
                Apellido = txt_apellidos.Text.Trim(),
                Telefono = txt_telefono.Text,
                Email = txt_email.Text.Trim(),
                Direccion = txt_direccion.Text.Trim(),
                Estado = cbx_estado.Text == "Activo" ? 1 : 0,
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
            {
                var Exist = await _IVendedor.ExisteNumeroDocumento(txt_numero_documento.Text.Trim(), Id_Vendedor);
                if (Exist) { errorProvider1.SetError(txt_numero_documento, "Numero documento ya existe"); valido++; }
                Exist = await _IVendedor.ExisteTelefono(txt_telefono.Text.Trim(), Id_Vendedor);
                if (Exist) { errorProvider1.SetError(txt_telefono, "Telefono ya existe"); valido++; }
                Exist = await _IVendedor.ExisteEmail(txt_email.Text.Trim(), Id_Vendedor);
                if (Exist) { errorProvider1.SetError(txt_email, "Email ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _IVendedor.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                    if (validationResult.MemberNames.Contains("IdentificationNumber"))
                    {
                        errorProvider1.SetError(txt_numero_documento, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("TelephoneNumber"))
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

        private void txt_numero_documento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
