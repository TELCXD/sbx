using sbx.core.Entities.Tienda;
using sbx.core.Interfaces.ActividadEconomica;
using sbx.core.Interfaces.Ciudad;
using sbx.core.Interfaces.CodigoPostal;
using sbx.core.Interfaces.Departamento;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.Pais;
using sbx.core.Interfaces.ResponsabilidadTributaria;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.TipoContribuyente;
using sbx.core.Interfaces.TipoResponsabilidad;
using System.ComponentModel.DataAnnotations;

namespace sbx
{
    public partial class Tienda : Form
    {
        private dynamic? _Permisos;
        private readonly ITienda _ITienda;
        private readonly IIdentificationType _IIdentificationType;
        private readonly ITipoResponsabilidad _ITipoResponsabilidad;
        private readonly IResponsabilidadTributaria _IResponsabilidadTributaria;
        private readonly ITipoContribuyente _ITipoContribuyente;
        private readonly IPais _IPais;
        private readonly IDepartamento _IDepartamento;
        private readonly ICiudad _ICiudad;
        private readonly ICodigoPostal _ICodigoPostal;
        private readonly IActividadEconomica _IActividadEconomica;
        private int vg_IdTienda = 0;

        public Tienda(ITienda tienda, IIdentificationType identificationType, ITipoResponsabilidad tipoResponsabilidad,
            IResponsabilidadTributaria responsabilidadTributaria, ITipoContribuyente tipoContribuyente, IPais pais,
            IDepartamento departamento, ICiudad ciudad, ICodigoPostal codigoPostal, IActividadEconomica actividadEconomica)
        {
            InitializeComponent();
            _ITienda = tienda;
            _IIdentificationType = identificationType;
            _ITipoResponsabilidad = tipoResponsabilidad;
            _IResponsabilidadTributaria = responsabilidadTributaria;
            _ITipoContribuyente = tipoContribuyente;
            _IPais = pais;
            _IDepartamento = departamento;
            _ICiudad = ciudad;
            _ICodigoPostal = codigoPostal;
            _IActividadEconomica = actividadEconomica;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Tienda_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
            await CargaDatosTienda();
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            var Datostienda = new TiendaEntitie
            {
                IdTienda = vg_IdTienda,
                TipoDocumento = Convert.ToInt32(cbx_tipo_documento.SelectedValue),
                NumeroDocumento = txt_numero_documento.Text,
                DigitoVerificacion = txt_digito_verificacion.Text,
                NombreRazonSocial = txt_nombre_razon_social.Text,
                TipoResponsabilidad = Convert.ToInt32(cbx_tipo_responsabilidad.SelectedValue),
                ResponsabilidadTributaria = Convert.ToInt32(cbx_responsabilidad_tributaria.SelectedValue),
                TipoContribuyente = Convert.ToInt32(cbx_tipo_contribuyente.SelectedValue),
                CorreoDistribucion = txt_correo_distribucion.Text,
                Telefono = txt_telefono.Text,
                Direccion = txt_direccion.Text,
                Pais = Convert.ToInt32(cbx_pais.SelectedValue),
                Departamento = Convert.ToInt32(cbx_departamento.SelectedValue),
                Municipio = Convert.ToInt32(cbx_municipio.SelectedValue),
                CodigoPostal = Convert.ToInt32(cbx_codigo_postal.SelectedValue),
                ActividadEconomica = Convert.ToInt32(cbx_actividad_economica.SelectedValue),
            };

            var validationContext = new ValidationContext(Datostienda);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            errorProvider1.Clear();

            bool esValido = Validator.TryValidateObject(Datostienda, validationContext, validationResults, true);

            if (esValido)
            {
                var resp = await _ITienda.CreateUpdate(Datostienda, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (validationResult.MemberNames.Contains("TipoDocumento"))
                    {
                        errorProvider1.SetError(cbx_tipo_documento, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("NumeroDocumento"))
                    {
                        errorProvider1.SetError(txt_numero_documento, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("NombreRazonSocial"))
                    {
                        errorProvider1.SetError(txt_nombre_razon_social, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("TipoResponsabilidad"))
                    {
                        errorProvider1.SetError(cbx_tipo_responsabilidad, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("ResponsabilidadTributaria"))
                    {
                        errorProvider1.SetError(cbx_responsabilidad_tributaria, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("TipoContribuyente"))
                    {
                        errorProvider1.SetError(cbx_tipo_contribuyente, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("CorreoDistribucion"))
                    {
                        errorProvider1.SetError(txt_correo_distribucion, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Telefono"))
                    {
                        errorProvider1.SetError(txt_telefono, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Direccion"))
                    {
                        errorProvider1.SetError(txt_direccion, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Pais"))
                    {
                        errorProvider1.SetError(cbx_pais, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Departamento"))
                    {
                        errorProvider1.SetError(cbx_departamento, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Municipio"))
                    {
                        errorProvider1.SetError(cbx_municipio, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("CodigoPostal"))
                    {
                        errorProvider1.SetError(cbx_codigo_postal, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("ActividadEconomica"))
                    {
                        errorProvider1.SetError(cbx_actividad_economica, validationResult.ErrorMessage);
                    }
                }
            }
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null) 
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "tienda":
                            btn_cargar.Enabled = item.ToCreate == 1 ? true : false;
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
            cbx_tipo_documento.DataSource = resp.Data;
            cbx_tipo_documento.ValueMember = "IdIdentificationType";
            cbx_tipo_documento.DisplayMember = "IdentificationType";
            cbx_tipo_documento.SelectedIndex = 0;

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

            resp = await _ICiudad.ListCiudad();
            cbx_municipio.DataSource = resp.Data;
            cbx_municipio.ValueMember = "IdCity";
            cbx_municipio.DisplayMember = "CityName";
            cbx_municipio.SelectedIndex = 0;

            resp = await _ICodigoPostal.ListCodigoPostal();
            cbx_codigo_postal.DataSource = resp.Data;
            cbx_codigo_postal.ValueMember = "IdCodigoPostal";
            cbx_codigo_postal.DisplayMember = "Code";
            cbx_codigo_postal.SelectedIndex = 0;

            resp = await _IActividadEconomica.ListActividadEconomica();
            cbx_actividad_economica.DataSource = resp.Data;
            cbx_actividad_economica.ValueMember = "IdActividadEconomica";
            cbx_actividad_economica.DisplayMember = "Nombre";
            cbx_actividad_economica.SelectedIndex = 0;
        }

        private async Task CargaDatosTienda()
        {
            var resp = await _ITienda.List();

            if (resp.Data.Count > 0)
            {
                vg_IdTienda = resp.Data[0]?.IdTienda;
                cbx_tipo_documento.SelectedValue = resp.Data[0]?.IdIdentificationType;
                txt_numero_documento.Text = resp.Data[0]?.NumeroDocumento;
                txt_nombre_razon_social.Text = resp.Data[0]?.NombreRazonSocial;
                cbx_tipo_responsabilidad.SelectedValue = resp.Data[0]?.IdTipoResponsabilidad;
                cbx_responsabilidad_tributaria.SelectedValue = resp.Data[0]?.IdResponsabilidadTributaria;
                cbx_tipo_contribuyente.SelectedValue = resp.Data[0]?.IdTipoContribuyente;
                txt_correo_distribucion.Text = resp.Data[0]?.CorreoDistribucion;
                txt_telefono.Text = resp.Data[0]?.Telefono;
                txt_direccion.Text = resp.Data[0]?.Direccion;
                cbx_pais.SelectedValue = resp.Data[0]?.IdCountry;
                cbx_departamento.SelectedValue = resp.Data[0]?.IdDepartament;
                cbx_municipio.SelectedValue = resp.Data[0]?.IdCity;
                cbx_codigo_postal.SelectedValue = resp.Data[0]?.IdCodigoPostal;
                cbx_actividad_economica.SelectedValue = resp.Data[0]?.IdActividadEconomica;
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
