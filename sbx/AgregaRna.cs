using sbx.core.Entities.RangoNumeracion;
using sbx.core.Interfaces.TipoDocumentoRangoNumeracion;
using sbx.core.Interfaces.RangoNumeracion;
using System.ComponentModel.DataAnnotations;
using sbx.core.Interfaces.FacturacionElectronica;
using System.Configuration;
using sbx.core.Entities.Auth;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sbx
{
    public partial class AgregaRna : Form
    {
        private dynamic? _Permisos;
        private int _Id_RangoNumeracion;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly ITipoDocumentoRangoNumeracion _ITipoDocumentoRangoNumeracion;
        private readonly IRangoNumeracionFE _IRangoNumeracionFE;
        private readonly IAuthService _IAuthService;

        public AgregaRna(IRangoNumeracion rangoNumeracion, ITipoDocumentoRangoNumeracion iTipoDocumentoRangoNumeracion, IRangoNumeracionFE rangoNumeracionFE, IAuthService iAuthService)
        {
            InitializeComponent();
            _IRangoNumeracion = rangoNumeracion;
            _ITipoDocumentoRangoNumeracion = iTipoDocumentoRangoNumeracion;
            _IRangoNumeracionFE = rangoNumeracionFE;
            _IAuthService = iAuthService;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_RangoNumeracion
        {
            get => _Id_RangoNumeracion;
            set => _Id_RangoNumeracion = value;
        }

        private async void AgregaRna_Load(object sender, EventArgs e)
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
                        case "ajustes":
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
            var resp = await _ITipoDocumentoRangoNumeracion.ListTipoDocumentoRangoNumeracion();
            cbx_tipo_documento.DataSource = resp.Data;
            cbx_tipo_documento.ValueMember = "Id_TipoDocumentoRangoNumeracion";
            cbx_tipo_documento.DisplayMember = "Nombre";
            cbx_tipo_documento.SelectedIndex = 0;
            cbx_expired.SelectedIndex = 1;
            cbx_estado.SelectedIndex = 0;
            cbx_en_uso.SelectedIndex = 0;

            if (Id_RangoNumeracion > 0)
            {
                resp = await _IRangoNumeracion.List(Id_RangoNumeracion);

                if (resp.Data != null)
                {
                    txt_id_dian.Text = resp.Data[0].IdRangoDIAN.ToString();
                    cbx_tipo_documento.SelectedValue = resp.Data[0].Id_TipoDocumentoRangoNumeracion;
                    txt_prefijo.Text = resp.Data[0].Prefijo;
                    txt_numero_desde.Text = resp.Data[0].NumeroDesde.ToString();
                    txt_numero_hasta.Text = resp.Data[0].NumeroHasta.ToString();
                    txt_numero_autorizacion.Text = resp.Data[0].NumeroResolucion;
                    txt_clave_Tecnica.Text = resp.Data[0].ClaveTecnica.ToString();
                    dtpFechaExpedicion.Value = resp.Data[0].FechaExpedicion;
                    dtpk_fecha_vencimiento.Value = resp.Data[0].FechaVencimiento;
                    cbx_expired.SelectedIndex = resp.Data[0].Vencido == true ? 0 : 1;
                    cbx_estado.SelectedIndex = resp.Data[0].Active == true ? 0 : 1;
                    cbx_en_uso.SelectedIndex = resp.Data[0].EnUso == true ? 0 : 1;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel3.Enabled = false;
            panel4.Enabled = false;
            try
            {
                bool Valido = true;
                errorProvider1.Clear();

                if (txt_id_dian.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_id_dian, "Debe ingresar un id");
                    Valido = false;
                }

                if (txt_prefijo.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_prefijo, "Debe ingresar un prefijo");
                    Valido = false;
                }

                if (txt_numero_desde.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_numero_desde, "Debe ingresar numero desde");
                    Valido = false;
                }

                if (txt_numero_hasta.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_numero_hasta, "Debe ingresar numero hasta");
                    Valido = false;
                }

                if (dtpFechaExpedicion.Value > dtpk_fecha_vencimiento.Value)
                {
                    errorProvider1.SetError(dtpFechaExpedicion, "Fecha de expedicion debe ser menor a fecha de vencimiento");
                    Valido = false;
                }

                if (Valido)
                {
                    var Exist = await _IRangoNumeracion.ExisteIdRangoDIAN(txt_id_dian.Text.Trim(), Id_RangoNumeracion);
                    if (Exist) { errorProvider1.SetError(txt_id_dian, "Id rango numeracion ya existe"); Valido = false; }
                    Exist = await _IRangoNumeracion.ExistePrefijo(txt_prefijo.Text.Trim(), Id_RangoNumeracion);
                    if (Exist) { errorProvider1.SetError(txt_prefijo, "Prefijo ya existe"); Valido = false; }
                    if (txt_numero_autorizacion.Text.Trim() != "")
                    {
                        Exist = await _IRangoNumeracion.ExisteResolucion(txt_numero_autorizacion.Text.Trim(), Id_RangoNumeracion);
                        if (Exist) { errorProvider1.SetError(txt_numero_autorizacion, "Numero resolucion ya existe"); Valido = false; }
                    }
                    if (txt_clave_Tecnica.Text.Trim() != "")
                    {
                        Exist = await _IRangoNumeracion.ExisteClaveTecnica(txt_clave_Tecnica.Text.Trim(), Id_RangoNumeracion);
                        if (Exist) { errorProvider1.SetError(txt_clave_Tecnica, "Clave tecnica ya existe"); Valido = false; }
                    }
                }

                if (Valido)
                {
                    var Datos = new RangoNumeracionEntitie
                    {
                        Id_RangoNumeracion = Id_RangoNumeracion,
                        Id_RangoDIAN = Convert.ToInt32(txt_id_dian.Text),
                        Id_TipoDocumentoRangoNumeracion = Convert.ToInt32(cbx_tipo_documento.SelectedValue),
                        Prefijo = txt_prefijo.Text,
                        NumeroDesde = txt_numero_desde.Text,
                        NumeroHasta = txt_numero_hasta.Text,
                        NumeroResolucion = txt_numero_autorizacion.Text,
                        ClaveTecnica = txt_clave_Tecnica.Text,
                        FechaExpedicion = dtpFechaExpedicion.Value,
                        FechaVencimiento = dtpk_fecha_vencimiento.Value,
                        Vencido = cbx_expired.SelectedIndex == 0 ? 1 : 0,
                        Active = cbx_estado.SelectedIndex == 0 ? 1 : 0,
                        EnUso = cbx_en_uso.SelectedIndex == 0 ? 1 : 0
                    };

                    var validationContext = new ValidationContext(Datos);
                    var validationResults = new System.Collections.Generic.List<ValidationResult>();

                    errorProvider1.Clear();

                    bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

                    if (esValido)
                    {
                        if (Datos.NumeroResolucion != "" && Datos.ClaveTecnica != "") 
                        {
                            //Autenticacion DIAN
                            AuthEntitie authEntitie = new AuthEntitie
                            {
                                url_api = ConfigurationManager.AppSettings["UrlAuthPOST"]!,
                                grant_type = ConfigurationManager.AppSettings["grant_typeAuth"]!,
                                client_id = ConfigurationManager.AppSettings["client_id"]!,
                                client_secret = ConfigurationManager.AppSettings["client_secret"]!,
                                username = ConfigurationManager.AppSettings["username"]!,
                                Passwords = ConfigurationManager.AppSettings["password"]!
                            };

                            if (!string.IsNullOrEmpty(authEntitie.url_api) && !string.IsNullOrEmpty(authEntitie.grant_type)
                                 && !string.IsNullOrEmpty(authEntitie.client_id) && !string.IsNullOrEmpty(authEntitie.client_secret)
                                 && !string.IsNullOrEmpty(authEntitie.username) && !string.IsNullOrEmpty(authEntitie.Passwords))
                            {
                                var RespAuth = _IAuthService.Autenticacion(authEntitie);

                                if (RespAuth.Data != null)
                                {
                                    if (RespAuth.Flag && RespAuth.Data.access_token != "")
                                    {
                                        //Valida existencia de rango en DIAN
                                        string Token = RespAuth.Data.access_token.ToString();
                                        string UrlRangos = ConfigurationManager.AppSettings["UrlRangosGET"]!;
                                        RangosEntitie rangosEntitie = new RangosEntitie
                                        {
                                            Id = Datos.Id_RangoDIAN,
                                            document = Datos.Id_TipoDocumentoRangoNumeracion,
                                            resolution_number = Datos.NumeroResolucion,
                                            technical_key = Datos.ClaveTecnica,
                                            is_active = Datos.Active
                                        };

                                        var respRangoDian = _IRangoNumeracionFE.ConsultaRangoDIAN(Token, UrlRangos, rangosEntitie);

                                        if (respRangoDian.Data != null)
                                        {
                                            if (respRangoDian.Flag)
                                            {
                                                int count = ((JArray)respRangoDian.Data.data).Count;

                                                if (respRangoDian?.Data != null && ((JArray)respRangoDian!.Data.data).Count == 0)
                                                {
                                                    MessageBox.Show($"La respuesta fue exitosa pero NO se encontraron datos: {respRangoDian?.Data} - {respRangoDian?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                                else
                                                {
                                                    string id = respRangoDian!.Data.data[0].id;
                                                    string prefix = respRangoDian.Data.data[0].prefix;
                                                    string from = respRangoDian.Data.data[0].from;
                                                    string to = respRangoDian.Data.data[0].to;
                                                    string resolution_number = respRangoDian.Data.data[0].resolution_number;
                                                    DateTime start_date = Convert.ToDateTime(respRangoDian.Data.data[0].start_date);
                                                    DateTime end_date = Convert.ToDateTime(respRangoDian.Data.data[0].end_date);
                                                    string technical_key = respRangoDian.Data.data[0].technical_key;
                                                    bool is_expired = respRangoDian.Data.data[0].is_expired;

                                                    if (Convert.ToInt32(id) == Datos.Id_RangoDIAN
                                                        &&
                                                        prefix == Datos.Prefijo &&
                                                        from == Datos.NumeroDesde &&
                                                        to == Datos.NumeroHasta &&
                                                        resolution_number == Datos.NumeroResolucion &&
                                                        start_date.ToString("yyyy-MM-dd") == Datos.FechaExpedicion.ToString("yyyy-MM-dd") &&
                                                        end_date.ToString("yyyy-MM-dd") == Datos.FechaVencimiento.ToString("yyyy-MM-dd") &&
                                                        technical_key == Datos.ClaveTecnica &&
                                                        is_expired == (Datos.Vencido == 1 ? true : false)
                                                        )
                                                    {
                                                        if (cbx_en_uso.SelectedIndex == 0)
                                                        {
                                                            var respEnUso = await _IRangoNumeracion.ListEnUso(Datos.Id_RangoNumeracion, Datos.Id_TipoDocumentoRangoNumeracion);

                                                            if (respEnUso.Data != null)
                                                            {
                                                                if (respEnUso.Data.Count > 0)
                                                                {
                                                                    DialogResult result = MessageBox.Show("Otro rango de numeracion esta en uso, esta seguro que desea cambiarlo? si no desea cambiarlo seleccione en campo En uso en NO",
                                                                   "Confirmar",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question);
                                                                    if (result == DialogResult.Yes)
                                                                    {
                                                                        var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                                                                else
                                                                {
                                                                    var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                    if (resp != null)
                                                                    {
                                                                        if (resp.Flag == true)
                                                                        {
                                                                            MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                            //Limpiar();
                                                                            this.Close();
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("No se encontro data", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                            if (resp != null)
                                                            {
                                                                if (resp.Flag == true)
                                                                {
                                                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    //Limpiar();
                                                                    this.Close();
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show($"Rango de numeracion no coincide: {respRangoDian?.Data} - {respRangoDian?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show($"Error en consulta de rango, NO se guardara informacion de rango de numeracion: {respRangoDian?.Data} - {respRangoDian?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Error en consulta de rango, NO se guardara informacion de rango de numeracion: {respRangoDian?.Data} - {respRangoDian?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Error en autenticacion, NO se guardara informacion de rango de numeracion: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Error en autenticacion, NO se guardara informacion de rango de numeracion: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"No se encuentra informacion completa de Url Apis, no se guardara informacion de rango de numeracion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (cbx_en_uso.SelectedIndex == 0)
                            {
                                var respEnUso = await _IRangoNumeracion.ListEnUso(Datos.Id_RangoNumeracion, Datos.Id_TipoDocumentoRangoNumeracion);

                                if (respEnUso.Data != null)
                                {
                                    if (respEnUso.Data.Count > 0)
                                    {
                                        DialogResult result = MessageBox.Show("Otro rango de numeracion esta en uso, esta seguro que desea cambiarlo? si no desea cambiarlo seleccione en campo En uso en NO",
                                       "Confirmar",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);
                                        if (result == DialogResult.Yes)
                                        {
                                            var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                                    else
                                    {
                                        var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        if (resp != null)
                                        {
                                            if (resp.Flag == true)
                                            {
                                                MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //Limpiar();
                                                this.Close();
                                            }
                                            else
                                            {
                                                MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se encontro data", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                var resp = await _IRangoNumeracion.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                if (resp != null)
                                {
                                    if (resp.Flag == true)
                                    {
                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //Limpiar();
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
                    else
                    {
                        foreach (var validationResult in validationResults)
                        {
                            if (validationResult.MemberNames.Contains("Id_TipoDocumentoRangoNumeracion"))
                            {
                                errorProvider1.SetError(cbx_tipo_documento, validationResult.ErrorMessage);
                            }

                            if (validationResult.MemberNames.Contains("Prefijo"))
                            {
                                errorProvider1.SetError(txt_prefijo, validationResult.ErrorMessage);
                            }

                            if (validationResult.MemberNames.Contains("NumeroDesde"))
                            {
                                errorProvider1.SetError(txt_numero_desde, validationResult.ErrorMessage);
                            }

                            if (validationResult.MemberNames.Contains("NumeroHasta"))
                            {
                                errorProvider1.SetError(txt_numero_hasta, validationResult.ErrorMessage);
                            }

                            if (validationResult.MemberNames.Contains("Active"))
                            {
                                errorProvider1.SetError(cbx_estado, validationResult.ErrorMessage);
                            }

                            if (validationResult.MemberNames.Contains("NumeracionAutorizada"))
                            {
                                errorProvider1.SetError(cbx_en_uso, validationResult.ErrorMessage);
                            }
                        }
                    }
                }
            }
            finally
            {
                panel3.Enabled = true;
                panel4.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void Limpiar()
        {
            cbx_tipo_documento.SelectedIndex = 0;
            txt_prefijo.Text = "";
            txt_numero_desde.Text = "";
            txt_numero_hasta.Text = "";
            txt_numero_autorizacion.Text = "";
            dtpk_fecha_vencimiento.Value = DateTime.Now;
            cbx_estado.SelectedIndex = 0;
            cbx_en_uso.SelectedIndex = 0;
        }

        private void txt_numero_desde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_numero_hasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_id_dian_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
