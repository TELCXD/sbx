using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class Kits : Form
    {
        private dynamic? _Permisos;
        private AgregaProductoGrupo? _AgregaProductoGrupo;
        private readonly IServiceProvider _serviceProvider;
        private readonly IParametros _IParametros;
        private readonly IProducto _IProducto;

        public Kits(IServiceProvider serviceProvider, IParametros parametros, IProducto producto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IParametros = parametros;
            _IProducto = producto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaProductoGrupo = _serviceProvider.GetRequiredService<AgregaProductoGrupo>();
                _AgregaProductoGrupo.Permisos = _Permisos;
                _AgregaProductoGrupo.Id_Producto = 0;
                _AgregaProductoGrupo.FormClosed += (s, args) => _AgregaProductoGrupo = null;
                _AgregaProductoGrupo.ShowDialog();
            }
        }

        private async void Kits_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            cbx_campo_filtro.SelectedIndex = 0;

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
                        case "productos":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_eliminar.Enabled = item.ToDelete == 1 ? true : false;
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
            await ConsultaProdGrupo();
        }

        private async Task ConsultaProdGrupo()
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

            var resp = await _IProducto.BuscarProdGrupoDetalle(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_producto.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_producto.Rows.Add(
                            item.IdProductoGrupo,
                            item.Sku,
                            item.CodigoBarras,
                            item.Nombre,
                            item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                            item.PrecioBase.ToString("N2", new CultureInfo("es-CO")),
                            item.Stock);
                    }
                }
            }

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProdGrupo();
            }
        }

        private void dtg_producto_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                if (dtg_producto.SelectedRows.Count > 0)
                {
                    if (_Permisos != null)
                    {
                        _AgregaProductoGrupo = _serviceProvider.GetRequiredService<AgregaProductoGrupo>();
                        _AgregaProductoGrupo.Permisos = _Permisos;
                        foreach (DataGridViewRow rows in dtg_producto.SelectedRows)
                        {
                            _AgregaProductoGrupo.Id_Producto = Convert.ToInt32(rows.Cells["cl_id"].Value);
                        }
                        _AgregaProductoGrupo.FormClosed += (s, args) => _AgregaProductoGrupo = null;
                        _AgregaProductoGrupo.ShowDialog();
                    }
                }
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar el kit seleccionado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_producto.SelectedRows.Count > 0)
                    {
                        var row = dtg_producto.SelectedRows[0];
                        if (row.Cells["cl_id"].Value != null)
                        {
                            int Id_Producto = Convert.ToInt32(row.Cells["cl_id"].Value);

                            var resp = await _IProducto.EliminarPrdGrp(Id_Producto);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await ConsultaProdGrupo();
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
    }
}
