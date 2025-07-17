using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.Parametros;
using sbx.core.Entities.Permiso;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.CredencialesApi;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Permisos;
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
        private readonly IPermisos _IPermisos;
        private AgregaRna? _AgregaRna;
        private int Id_RangoNumeracion = 0;
        private readonly IUsuario _IUsuario;
        private AddUsuario? _AddUsuario;
        private int Id_usuario = 0;
        private Buscador? _Buscador;
        PermisosEntitie PermisosEntitie = new PermisosEntitie();
        string MensajePersonalizado = "";
        private readonly ICredencialesApi _ICredencialesApi;
        int v_Id_credencialesApi = 0;
        private AgregaApi? _AgregaApi;

        public Ajustes(IServiceProvider serviceProvider, IRangoNumeracion rangoNumeracion,
            IParametros parametros, IUsuario usuario, IPermisos permisos, ICredencialesApi credencialesApi)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IRangoNumeracion = rangoNumeracion;
            _IParametros = parametros;
            _IUsuario = usuario;
            _IPermisos = permisos;
            _ICredencialesApi = credencialesApi;
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
            await CargaDatosCredenciales();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
            cbx_lineas_abajo.SelectedIndex = 0;
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
                        case "permisos":
                            btn_guardar_permisos.Enabled = item.ToCreate == 1 ? true : false;
                            btn_busca_usuario.Enabled = item.ToUpdate == 1 ? true : false;
                            break;
                        case "credencialesApi":
                            btn_agregar_api.Enabled = item.ToCreate == 1 ? true : false;
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
                        item.EnUso == true ? "SI" : "NO",
                        item.Id_RangoNumeracion,
                        item.Vencido == true ? "SI" : "NO",
                        item.Active == true ? "Active" : "Inactivo",
                        item.Id_TipoDocumentoRangoNumeracion == 1 ? "Factura"
                        : item.Id_TipoDocumentoRangoNumeracion == 2 ? "Factura Electrónica de Venta"
                        : item.Id_TipoDocumentoRangoNumeracion == 3 ? "Nota de Crédito" : "Nota de debito",
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
                            case "Buscar en venta por":
                                cbx_Buscarenventapor.Text = item.Value;
                                break;
                            case "Tipo filtro producto":
                                cbx_parametro_tipo_filtro_producto.Text = item.Value;
                                break;
                            case "Mensaje final tirilla":
                                txt_mensaje_final_tirilla.Text = item.Value;
                                MensajePersonalizado = item.Value;
                                break;
                            case "Ancho tirilla":
                                if (item.Value == "80") { rb_80mm.Checked = true; } else { rb_58mm.Checked = true; }
                                break;
                            case "Impresora":
                                txt_impresora.Text = item.Value;
                                break;
                            case "Ruta backup":
                                txt_ruta_backup.Text = item.Value;
                                break;
                            case "lineas abajo de la tirilla":
                                cbx_lineas_abajo.Text = item.Value;
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

            for (int i = 0; i <= 8; i++)
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
                            Nombre = "Buscar en venta por",
                            Value = cbx_Buscarenventapor.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 3:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Tipo filtro producto",
                            Value = cbx_parametro_tipo_filtro_producto.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 4:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Ancho tirilla",
                            Value = rb_80mm.Checked == true ? "80" : "58"
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 5:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Impresora",
                            Value = txt_impresora.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 6:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Mensaje final tirilla",
                            Value = txt_mensaje_final_tirilla.Text
                        };

                        ListParametros.Add(Parametros);
                        break;
                    case 7:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "Ruta backup",
                            Value = txt_ruta_backup.Text
                        };
                        ListParametros.Add(Parametros);
                        break;
                    case 8:
                        Parametros = new ParametrosEntitie
                        {
                            Nombre = "lineas abajo de la tirilla",
                            Value = cbx_lineas_abajo.Text
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

        private void btn_busca_usuario_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Busca_usuario";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int idUser)
        {
            var dataMenus = await _IPermisos.ListMenusPermisosInicial();

            dtg_permisos.Rows.Clear();

            if (dataMenus.Data != null)
            {
                if (dataMenus.Data.Count > 0)
                {
                    foreach (var item in dataMenus.Data)
                    {
                        dtg_permisos.Rows.Add(
                            item.IdUserMenu,
                            item.IdMenu,
                            item.MenuName,
                            PermisosEntitie.IdUser,
                            item.ToRead,
                            item.ToCreate,
                            item.ToUpdate,
                            item.ToDelete,
                            item.Active);
                    }
                }
            }

            PermisosEntitie.IdUser = idUser;

            var DataUsuario = await _IUsuario.List(idUser);
            if (DataUsuario.Data != null)
            {
                if (DataUsuario.Data.Count > 0)
                {
                    txt_busca_usuario.Text = DataUsuario.Data[0].IdUser + " - " + DataUsuario.Data[0].UserName;
                }
            }

            var resp = await _IPermisos.List(idUser);

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        foreach (DataGridViewRow fila in dtg_permisos.Rows)
                        {
                            if (Convert.ToInt32(item.IdMenu) == Convert.ToInt32(fila.Cells["cl_id_menu"].Value))
                            {
                                fila.Cells["cl_idUserMenu"].Value = Convert.ToInt32(item.IdUserMenu);
                                fila.Cells["cl_toRead"].Value = Convert.ToInt32(item.ToRead) == 1 ? true : false;
                                fila.Cells["cl_ToCreate"].Value = Convert.ToInt32(item.ToCreate) == 1 ? true : false;
                                fila.Cells["cl_toUpdate"].Value = Convert.ToInt32(item.ToUpdate) == 1 ? true : false;
                                fila.Cells["cl_toDelete"].Value = Convert.ToInt32(item.ToDelete) == 1 ? true : false;
                            }
                        }
                    }
                }
            }
        }

        private async void btn_guardar_permisos_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_busca_usuario.Text.Trim() != "")
            {
                List<PermisosEntitie> lisPermisosEntitie = new List<PermisosEntitie>();

                foreach (DataGridViewRow fila in dtg_permisos.Rows)
                {
                    PermisosEntitie permiso = new PermisosEntitie()
                    {
                        IdUserMenu = Convert.ToInt32(fila.Cells["cl_idUserMenu"].Value),
                        IdMenu = Convert.ToInt32(fila.Cells["cl_id_menu"].Value),
                        IdUser = PermisosEntitie.IdUser,
                        ToRead = Convert.ToInt32(fila.Cells["cl_toRead"].Value),
                        ToCreate = Convert.ToInt32(fila.Cells["cl_toCreate"].Value),
                        ToUpdate = Convert.ToInt32(fila.Cells["cl_toUpdate"].Value),
                        ToDelete = Convert.ToInt32(fila.Cells["cl_toDelete"].Value),
                        Active = Convert.ToInt32(fila.Cells["cl_estado"].Value)
                    };

                    lisPermisosEntitie.Add(permiso);
                }

                var resp = await _IPermisos.CreateUpdate(lisPermisosEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                errorProvider1.SetError(txt_busca_usuario, "Debe seleccionar un usuario");
            }
        }

        private void rb_80mm_CheckedChanged(object sender, EventArgs e)
        {
            txt_mensaje_final_tirilla.MaxLength = 42;
            txt_mensaje_final_tirilla.Text = MensajePersonalizado;
        }

        private void rb_58mm_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_mensaje_final_tirilla.MaxLength = 32;
            txt_mensaje_final_tirilla.Text = MensajePersonalizado.Length > 32 ? MensajePersonalizado.Substring(0, 32) : MensajePersonalizado;
        }

        private async Task CargaDatosCredenciales()
        {
            var resp = await _ICredencialesApi.List();

            dtg_api_variables.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_api_variables.Rows.Add(
                            item.IdCredencialesApi,
                            item.url_api,
                            item.Descripcion,
                            item.Variable1,
                            item.Variable2,
                            item.Variable3,
                            item.Variable4,
                            item.Variable5,
                            item.Variable6,
                            item.Variable7,
                            item.Variable8,
                            item.Variable9,
                            item.Variable10,
                            item.Variable11,
                            item.Variable12);
                    }
                }
            }
        }

        private void btn_editar_api_Click(object sender, EventArgs e)
        {
            if (dtg_api_variables.Rows.Count > 0)
            {
                if (dtg_api_variables.SelectedRows.Count > 0)
                {
                    var row = dtg_api_variables.SelectedRows[0];
                    if (row.Cells["cl_id"].Value != null)
                    {
                        int IdCredencialesApi = Convert.ToInt32(row.Cells["cl_id"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaApi = _serviceProvider.GetRequiredService<AgregaApi>();
                            _AgregaApi.Permisos = _Permisos;
                            _AgregaApi.Id_credencialesApi = IdCredencialesApi;
                            _AgregaApi.FormClosed += (s, args) => _AgregaApi = null;
                            _AgregaApi.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_agregar_api_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaApi = _serviceProvider.GetRequiredService<AgregaApi>();
                _AgregaApi.Permisos = _Permisos;
                _AgregaApi.Id_credencialesApi = 0;
                _AgregaApi.FormClosed += (s, args) => _AgregaApi = null;
                _AgregaApi.ShowDialog();
            }
        }

        private async void btn_eliminar_api_Click(object sender, EventArgs e)
        {
            if (dtg_api_variables.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar ?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_api_variables.SelectedRows.Count > 0)
                    {
                        var row = dtg_api_variables.SelectedRows[0];
                        if (row.Cells["cl_id"].Value != null)
                        {
                            int Id= Convert.ToInt32(row.Cells["cl_id"].Value);
                            var resp = await _ICredencialesApi.Eliminar(Id);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await CargaDatosCredenciales();
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
            else
            {
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_buscar_api_Click(object sender, EventArgs e)
        {
            await CargaDatosCredenciales();
        }

        private void btn_eliminar_ra_Click(object sender, EventArgs e)
        {

        }
    }
}
