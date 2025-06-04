using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.Parametros;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Usuario;

namespace sbx
{
    public partial class Ajustes : Form
    {
        private dynamic? _Permisos;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly IServiceProvider _serviceProvider;
        private readonly IParametros _IParametros;
        private AgregaRna? _AgregaRna;
        private int Id_RangoNumeracion = 0;
        private readonly IUsuario _IUsuario;
        private AddUsuario? _AddUsuario;
        private int Id_usuario = 0;

        public Ajustes(IServiceProvider serviceProvider, IRangoNumeracion rangoNumeracion, IParametros parametros, IUsuario usuario)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IRangoNumeracion = rangoNumeracion;
            _IParametros = parametros;
            _IUsuario = usuario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Ajustes_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await ConsultaRangosNumeracion();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
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
                            btn_guardar_parametros.Enabled = item.ToCreate == 1 ? true : false;
                            break;
                        case "usuarios":
                            btn_agregar_usuario.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar_usuario.Enabled = item.ToUpdate == 1 ? true : false;
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

        private async Task ConsultaRangosNumeracion()
        {
            var resp = await _IRangoNumeracion.List(0);

            if (resp.Data != null)
            {
                dtg_rangos_numeracion.Rows.Clear();
                foreach (var item in resp.Data)
                {
                    dtg_rangos_numeracion.Rows.Add(
                        item.Id_RangoNumeracion,
                        item.Active == true ? "Active" : "Inactivo",
                        item.Id_TipoDocumentoRangoNumeracion == 1 ? "Factura Electrónica de Venta"
                        : item.Id_TipoDocumentoRangoNumeracion == 2 ? "Nota de Crédito"
                        : item.Id_TipoDocumentoRangoNumeracion == 3 ? "Factura" : "",
                        item.Prefijo,
                        item.NumeroDesde,
                        item.NumeroHasta,
                        item.NumeroAutorizacion,
                        item.FechaVencimiento,
                        "0");
                }
            }
        }

        private void btn_agregar_ra_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaRna = _serviceProvider.GetRequiredService<AgregaRna>();
                _AgregaRna.Permisos = _Permisos;
                _AgregaRna.Id_RangoNumeracion = 0;
                _AgregaRna.FormClosed += (s, args) => _AgregaRna = null;
                _AgregaRna.ShowDialog();
            }
        }

        private async void btn_buscar_ra_Click(object sender, EventArgs e)
        {
            await ConsultaRangosNumeracion();
        }

        private void btn_editar_ra_Click(object sender, EventArgs e)
        {
            if (dtg_rangos_numeracion.Rows.Count > 0)
            {
                if (dtg_rangos_numeracion.SelectedRows.Count > 0)
                {
                    var row = dtg_rangos_numeracion.SelectedRows[0];
                    if (row.Cells["cl_Nro"].Value != null)
                    {
                        Id_RangoNumeracion = Convert.ToInt32(row.Cells["cl_Nro"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaRna = _serviceProvider.GetRequiredService<AgregaRna>();
                            _AgregaRna.Permisos = _Permisos;
                            _AgregaRna.Id_RangoNumeracion = Id_RangoNumeracion;
                            _AgregaRna.FormClosed += (s, args) => _AgregaRna = null;
                            _AgregaRna.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            int selectedIndex = tabControl.SelectedIndex;

            switch (selectedIndex)
            {
                case 1:
                    ConsultaParametros();
                    break;
            }
        }

        private async void ConsultaParametros()
        {
            var resp = await _IParametros.List("");

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        switch (item.Nombre)
                        {
                            case "Validar stock para venta":
                                cbx_valida_stock_venta.Text = item.Value;
                                break;
                            case "Preguntar imprimir factura en venta":
                                cbx_pregunta_imprimir_venta.Text = item.Value;
                                break;
                            case "Ancho tirilla":
                                txt_ancho_tirilla.Text = item.Value;
                                break;
                            case "Impresora":
                                txt_impresora.Text = item.Value;
                                break;
                            case "Ruta backup":
                                txt_ruta_backup.Text = item.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private async void btn_guardar_parametros_Click(object sender, EventArgs e)
        {
            List<ParametrosEntitie> ListParametros = new List<ParametrosEntitie>();

            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        var Parametros = new ParametrosEntitie
                        {
                            Nombre = "Validar stock para venta",
                            Value = cbx_valida_stock_venta.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 1:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Preguntar imprimir factura en venta",
                            Value = cbx_pregunta_imprimir_venta.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 2:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Ancho tirilla",
                            Value = txt_ancho_tirilla.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 3:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Impresora",
                            Value = txt_impresora.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 4:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Ruta backup",
                            Value = txt_ruta_backup.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    default:
                        break;
                }
            }

            var resp = await _IParametros.Update(ListParametros);

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

        private void txt_ancho_tirilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private async void btn_buscar_usuario_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (cbx_campo_filtro.Text == "Id")
            {
                bool esNumerico = int.TryParse(txt_buscar_usuario.Text, out int valor);
                if (!esNumerico)
                {
                    errorProvider1.SetError(txt_buscar_usuario, "Ingrese un valor numerico");
                    return;
                }
            }

            var resp = await _IUsuario.Buscar(txt_buscar_usuario.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_usuario.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_usuario.Rows.Add(
                            item.IdUser,
                            item.UserName,
                            item.IdRole,
                            item.NameRole,
                            item.IdentificationType,
                            item.IdentificationNumber,
                            item.NombreCompleto,
                            item.TelephoneNumber,
                            item.BirthDate);
                    }
                }
            }
        }

        private void btn_agregar_usuario_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AddUsuario = _serviceProvider.GetRequiredService<AddUsuario>();
                _AddUsuario.Permisos = _Permisos;
                _AddUsuario.Id_Usuario = 0;
                _AddUsuario.FormClosed += (s, args) => _AddUsuario = null;
                _AddUsuario.ShowDialog();
            }
        }

        private void btn_editar_usuario_Click(object sender, EventArgs e)
        {
            if (dtg_usuario.Rows.Count > 0)
            {
                if (dtg_usuario.SelectedRows.Count > 0)
                {
                    var row = dtg_usuario.SelectedRows[0];
                    if (row.Cells["cl_IdUsuario"].Value != null)
                    {
                        Id_usuario = Convert.ToInt32(row.Cells["cl_IdUsuario"].Value);
                        if (_Permisos != null)
                        {
                            _AddUsuario = _serviceProvider.GetRequiredService<AddUsuario>();
                            _AddUsuario.Permisos = _Permisos;
                            _AddUsuario.Id_Usuario = Id_usuario;
                            _AddUsuario.FormClosed += (s, args) => _AddUsuario = null;
                            _AddUsuario.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
