using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.ConversionProducto;
using sbx.core.Interfaces.Parametros;

namespace sbx
{
    public partial class ConversionProducto : Form
    {
        private dynamic? _Permisos;
        private readonly IParametros _IParametros;
        private readonly IConversionProducto _IConversionProducto;
        private AddConversionProducto? _AddConversionProducto;
        private readonly IServiceProvider _serviceProvider;
        private Int64 Id_ConversionesProducto_ = 0;

        public ConversionProducto(IParametros parametros, IConversionProducto conversionProducto, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IParametros = parametros;
            _IConversionProducto = conversionProducto;
            _serviceProvider = serviceProvider;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void ConversionProducto_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_padre_hijo.SelectedIndex = 0;

            var DataParametros = await _IParametros.List("Tipo filtro producto");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    string BuscarPor = DataParametros.Data[0].Value;
                    cbx_tipo_filtro.Text = BuscarPor;
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
                        case "conversionProducto":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_eliminar.Enabled = item.ToUpdate == 1 ? true : false;
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaProductos();
        }

        private async Task ConsultaProductos()
        {
            errorProvider1.Clear();
            btn_buscar.Enabled = false;
            txt_buscar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (cbx_campo_filtro.Text == "Id")
            {
                bool esNumerico = int.TryParse(txt_buscar.Text, out int valor);
                if (!esNumerico)
                {
                    errorProvider1.SetError(txt_buscar, "Ingrese un valor numerico");
                    btn_buscar.Enabled = true;
                    txt_buscar.Enabled = true;
                    txt_buscar.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            var resp = await _IConversionProducto.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_padre_hijo.Text);

            dtg_producto.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_producto.Rows.Add(
                            item.IdProductoPadre,
                            item.SkuPadre,
                            item.CodigoBarraasPadre,
                            item.NombrePadre,
                            item.IdProductoHijo,
                            item.SkuHijo,
                            item.CodigoBarrasHijo,
                            item.NombreHijo,
                            item.Cantidad);
                    }
                }
            }

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AddConversionProducto = _serviceProvider.GetRequiredService<AddConversionProducto>();
                _AddConversionProducto.Permisos = _Permisos;
                _AddConversionProducto.Id_ConversionesProducto = 0;
                _AddConversionProducto.FormClosed += (s, args) => _AddConversionProducto = null;
                _AddConversionProducto.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                if (dtg_producto.SelectedRows.Count > 0)
                {
                    var row = dtg_producto.SelectedRows[0];
                    if (row.Cells["cl_id_padre"].Value != null && row.Cells["cl_id_Hijo"].Value != null)
                    {
                        string cancatenado = string.Concat(Convert.ToInt32(row.Cells["cl_id_padre"].Value), Convert.ToInt32(row.Cells["cl_id_Hijo"].Value));
                        Id_ConversionesProducto_ = Convert.ToInt64(cancatenado);
                        if (_Permisos != null)
                        {
                            _AddConversionProducto = _serviceProvider.GetRequiredService<AddConversionProducto>();
                            _AddConversionProducto.Permisos = _Permisos;
                            _AddConversionProducto.Id_ConversionesProducto = Id_ConversionesProducto_;
                            _AddConversionProducto.FormClosed += (s, args) => _AddConversionProducto = null;
                            _AddConversionProducto.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProductos();
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                if (dtg_producto.SelectedRows.Count > 0)
                {
                    var row = dtg_producto.SelectedRows[0];
                    if (row.Cells["cl_id_padre"].Value != null && row.Cells["cl_id_Hijo"].Value != null)
                    {
                        string cancatenado = string.Concat(Convert.ToInt32(row.Cells["cl_id_padre"].Value), Convert.ToInt32(row.Cells["cl_id_Hijo"].Value));
                        Id_ConversionesProducto_ = Convert.ToInt64(cancatenado);

                        DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar el registro?",
                       "Confirmar",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            var resp = await _IConversionProducto.Eliminar(Id_ConversionesProducto_);
                            if (resp.Flag == true)
                            {
                                MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await ConsultaProductos();
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
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
