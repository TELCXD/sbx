using sbx.core.Entities.RangoNumeracion;
using sbx.core.Interfaces.TipoDocumentoRangoNumeracion;
using sbx.core.Interfaces.RangoNumeracion;
using System.ComponentModel.DataAnnotations;
using sbx.core.Interfaces.Tienda;

namespace sbx
{
    public partial class AgregaRna : Form
    {
        private dynamic? _Permisos;
        private int _Id_RangoNumeracion;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly ITipoDocumentoRangoNumeracion _ITipoDocumentoRangoNumeracion;

        public AgregaRna(IRangoNumeracion rangoNumeracion, ITipoDocumentoRangoNumeracion iTipoDocumentoRangoNumeracion)
        {
            InitializeComponent();
            _IRangoNumeracion = rangoNumeracion;
            _ITipoDocumentoRangoNumeracion = iTipoDocumentoRangoNumeracion;
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

            cbx_estado.SelectedIndex = 0;
            cbx_numeracion_autorizada.SelectedIndex = 0;

            if (Id_RangoNumeracion > 0) 
            {
                resp = await _IRangoNumeracion.List(Id_RangoNumeracion);

                if (resp.Data != null) 
                {
                    cbx_tipo_documento.SelectedValue = resp.Data[0].Id_TipoDocumentoRangoNumeracion;
                    txt_prefijo.Text = resp.Data[0].Prefijo;
                    txt_numero_desde.Text = resp.Data[0].NumeroDesde;
                    txt_numero_hasta.Text = resp.Data[0].NumeroHasta;
                    txt_numero_autorizacion.Text = resp.Data[0].NumeroAutorizacion;
                    dtpk_fecha_vencimiento.Value = resp.Data[0].FechaVencimiento;
                    cbx_estado.SelectedIndex = resp.Data[0].Active == true ? 0: 1;
                    cbx_numeracion_autorizada.SelectedIndex = resp.Data[0].NumeracionAutorizada == true ? 0 : 1;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            var Datos = new RangoNumeracionEntitie
            {
                Id_RangoNumeracion = Id_RangoNumeracion,
                Id_TipoDocumentoRangoNumeracion = Convert.ToInt32(cbx_tipo_documento.SelectedValue),
                Prefijo = txt_prefijo.Text,
                NumeroDesde = txt_numero_desde.Text,
                NumeroHasta = txt_numero_hasta.Text,
                NumeroAutorizacion = txt_numero_autorizacion.Text,
                FechaVencimiento = dtpk_fecha_vencimiento.Value,
                Active = cbx_estado.SelectedIndex == 0 ? 1 : 0,
                NumeracionAutorizada = Convert.ToInt32(cbx_numeracion_autorizada.SelectedValue)
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            errorProvider1.Clear();

            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
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
                        errorProvider1.SetError(cbx_numeracion_autorizada, validationResult.ErrorMessage);
                    }
                }
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
            cbx_numeracion_autorizada.SelectedIndex = 0;
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
    }
}
