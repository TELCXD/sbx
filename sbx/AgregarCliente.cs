using sbx.core.Entities.Cliente;
using sbx.core.Interfaces.Ciudad;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.Departamento;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.Pais;
using sbx.core.Interfaces.ResponsabilidadTributaria;
using sbx.core.Interfaces.TipoCliente;
using sbx.core.Interfaces.TipoContribuyente;
using sbx.core.Interfaces.TipoResponsabilidad;
using System.ComponentModel.DataAnnotations;

namespace sbx
{
    public partial class AgregarCliente : Form
    {
        private dynamic? _Permisos;
        private int _Id_Cliente;
        private readonly IIdentificationType _IIdentificationType;
        private readonly ICliente _ICliente;
        public readonly ITipoCliente _ITipoCliente;
        private readonly ITipoResponsabilidad _ITipoResponsabilidad;
        private readonly IResponsabilidadTributaria _IResponsabilidadTributaria;
        private readonly ITipoContribuyente _ITipoContribuyente;
        private readonly IPais _IPais;
        private readonly IDepartamento _IDepartamento;
        private readonly ICiudad _ICiudad;
        public AgregarCliente(ICliente cliente, IIdentificationType identificationType, ITipoCliente tipoCliente, ITipoResponsabilidad tipoResponsabilidad,
            IResponsabilidadTributaria responsabilidadTributaria, ITipoContribuyente tipoContribuyente, IPais pais, IDepartamento departamento, ICiudad ciudad)
        {
            InitializeComponent();
            _ICliente = cliente;
            _IIdentificationType = identificationType;
            _ITipoCliente = tipoCliente;
            _ITipoResponsabilidad = tipoResponsabilidad;
            _IResponsabilidadTributaria = responsabilidadTributaria;
            _ITipoContribuyente = tipoContribuyente;
            _IPais = pais;
            _IDepartamento = departamento;
            _ICiudad = ciudad;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Cliente
        {
            get => _Id_Cliente;
            set => _Id_Cliente = value;
        }

        private async void AgregarCliente_Load(object sender, EventArgs e)
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

            resp = await _ITipoCliente.ListTipoCliente();
            cbx_tipo_cliente.DataSource = resp.Data;
            cbx_tipo_cliente.ValueMember = "IdTipoCliente";
            cbx_tipo_cliente.DisplayMember = "Nombre";
            cbx_tipo_cliente.SelectedIndex = 0;

            resp = await _ITipoResponsabilidad.ListTipoResponsabilidad();
            cbx_tipo_responsabilidad.DataSource = resp.Data;
            cbx_tipo_responsabilidad.ValueMember = "IdTipoResponsabilidad";
            cbx_tipo_responsabilidad.DisplayMember = "Nombre";
            cbx_tipo_responsabilidad.SelectedIndex = 0;

            resp = await _IResponsabilidadTributaria.ListResponsabilidadTributaria();
            cbx_responsabilidad_tributaria.DataSource = resp.Data;
            cbx_responsabilidad_tributaria.ValueMember = "IdResponsabilidadTributaria";
            cbx_responsabilidad_tributaria.DisplayMember = "Nombre";
            cbx_responsabilidad_tributaria.SelectedIndex = 0;

            resp = await _ITipoContribuyente.ListTipoContribuyente();
            cbx_tipo_contribuyente.DataSource = resp.Data;
            cbx_tipo_contribuyente.ValueMember = "IdTipoContribuyente";
            cbx_tipo_contribuyente.DisplayMember = "Nombre";
            cbx_tipo_contribuyente.SelectedIndex = 0;

            resp = await _IPais.ListPais();
            cbx_pais.DataSource = resp.Data;
            cbx_pais.ValueMember = "IdCountry";
            cbx_pais.DisplayMember = "CountryName";
            cbx_pais.SelectedIndex = 0;

            resp = await _IDepartamento.ListDepartamento();
            cbx_departamento.DataSource = resp.Data;
            cbx_departamento.ValueMember = "IdDepartament";
            cbx_departamento.DisplayMember = "DepartmentName";
            cbx_departamento.SelectedIndex = 0;

            resp = await _ICiudad.ListCiudadForDepartament(Convert.ToInt32(cbx_departamento.SelectedValue));
            cbx_municipio.DataSource = resp.Data;
            cbx_municipio.ValueMember = "IdCity";
            cbx_municipio.DisplayMember = "CityName";
            cbx_municipio.SelectedIndex = 0;

            cbx_estado.SelectedIndex = 0;

            if (Id_Cliente > 0)
            {
                resp = await _ICliente.List(Id_Cliente);

                if (resp.Data != null)
                {
                    cbx_tipo_identificacion.SelectedValue = resp.Data[0].IdIdentificationType;
                    txt_numero_documento.Text = resp.Data[0].NumeroDocumento;
                    txt_nombre_razon_social.Text = resp.Data[0].NombreRazonSocial;
                    cbx_tipo_cliente.SelectedValue = resp.Data[0].IdTipoCliente;
                    txt_direccion.Text = resp.Data[0].Direccion;
                    txt_telefono.Text = resp.Data[0].Telefono;
                    txt_email.Text = resp.Data[0].Email;
                    cbx_estado.SelectedIndex = resp.Data[0].Estado == true ? 0 : 1;
                    cbx_tipo_responsabilidad.SelectedValue = resp.Data[0]?.IdTipoResponsabilidad;
                    cbx_responsabilidad_tributaria.SelectedValue = resp.Data[0]?.IdResponsabilidadTributaria;
                    cbx_tipo_contribuyente.SelectedValue = resp.Data[0]?.IdTipoContribuyente;
                    cbx_pais.SelectedValue = resp.Data[0]?.IdCountry;
                    cbx_departamento.SelectedValue = resp.Data[0]?.IdDepartament;

                    var respCity = await _ICiudad.ListCiudadForDepartament(Convert.ToInt32(cbx_departamento.SelectedValue));
                    cbx_municipio.DataSource = respCity.Data;
                    cbx_municipio.ValueMember = "IdCity";
                    cbx_municipio.DisplayMember = "CityName";

                    cbx_municipio.SelectedValue = resp.Data[0]?.IdCity;
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

            var Datos = new ClienteEntitie
            {
                IdCliente = Id_Cliente,
                IdIdentificationType = Convert.ToInt32(cbx_tipo_identificacion.SelectedValue),
                NumeroDocumento = txt_numero_documento.Text,
                NombreRazonSocial = txt_nombre_razon_social.Text,
                IdTipoCliente = Convert.ToInt32(cbx_tipo_cliente.SelectedValue),
                Direccion = txt_direccion.Text,
                Telefono = txt_telefono.Text,
                Email = txt_email.Text,
                Estado = cbx_estado.Text == "Activo" ? 1 : 0,
                IdTipoResponsabilidad = Convert.ToInt32(cbx_tipo_responsabilidad.SelectedValue),
                IdResponsabilidadTributaria = Convert.ToInt32(cbx_responsabilidad_tributaria.SelectedValue),
                IdTipoContribuyente = Convert.ToInt32(cbx_tipo_contribuyente.SelectedValue),
                IdCountry = Convert.ToInt32(cbx_pais.SelectedValue),
                IdDepartament = Convert.ToInt32(cbx_departamento.SelectedValue),
                IdCity = Convert.ToInt32(cbx_municipio.SelectedValue)
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
            {
                var Exist = await _ICliente.ExisteNumeroDocumento(txt_numero_documento.Text.Trim(), Id_Cliente);
                if (Exist) { errorProvider1.SetError(txt_numero_documento, "Numero documento ya existe"); valido++; }
                Exist = await _ICliente.ExisteNombreRazonSocial(txt_nombre_razon_social.Text.Trim(), Id_Cliente);
                if (Exist) { errorProvider1.SetError(txt_nombre_razon_social, "Nombre ya existe"); valido++; }
                Exist = await _ICliente.ExisteTelefono(txt_telefono.Text.Trim(), Id_Cliente);
                if (Exist) { errorProvider1.SetError(txt_telefono, "Telefono ya existe"); valido++; }
                Exist = await _ICliente.ExisteEmail(txt_email.Text.Trim(), Id_Cliente);
                if (Exist) { errorProvider1.SetError(txt_email, "Email ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _ICliente.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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

        private async void cbx_departamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (dynamic?)cbx_departamento.SelectedItem;
            int idDepart = item!.IdDepartament;

            var resp = await _ICiudad.ListCiudadForDepartament(idDepart);
            cbx_municipio.DataSource = resp.Data;
            cbx_municipio.ValueMember = "IdCity";
            cbx_municipio.DisplayMember = "CityName";
            cbx_municipio.SelectedIndex = 0;
        }
    }
}
