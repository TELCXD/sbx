using Microsoft.IdentityModel.Tokens;
using sbx.core.Interfaces.Producto;

namespace sbx
{
    public partial class AgregaCodigosBarras : Form
    {
        private int _Id_Producto_grupo;
        private dynamic? _Permisos;
        private readonly IProducto _IProducto;
        int Id_producto_ = 0;
        string CodigoBarras_ = "";

        public AgregaCodigosBarras(IProducto iProducto)
        {
            InitializeComponent();
            _IProducto = iProducto;
        }

        public int Id_Producto
        {
            get => _Id_Producto_grupo;
            set => _Id_Producto_grupo = value;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void AgregaCodigosBarras_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            lbl_id_producto.Text = Id_Producto.ToString();

            if (Id_Producto > 0)
            {
                _Buscador(Id_Producto);
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
                            txt_codigo_barras.Enabled = item.ToRead == 1 ? true : false;
                            btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_quitar.Enabled = item.ToDelete == 1 ? true : false;
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

        private async void _Buscador(int id)
        {
            var resp = await _IProducto.ListCodigoBarras(id);

            dtg_prd_codigoBarras.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_prd_codigoBarras.Rows.Add(
                            item.IdProducto,
                            item.CodigoBarra);
                    }
                }
            }
        }

        private async void txt_codigo_barras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var resp = await _IProducto.ListCodigoBarrasTexto(txt_codigo_barras.Text.Trim(), Id_Producto);

                dtg_prd_codigoBarras.Rows.Clear();

                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        foreach (var item in resp.Data)
                        {
                            dtg_prd_codigoBarras.Rows.Add(
                                item.IdProducto,
                                item.CodigoBarra);
                        }
                    }
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (!txt_codigo_barras.Text.IsNullOrEmpty()) 
            {
                var resp = await _IProducto.CreateCodigoBarras(txt_codigo_barras.Text.Trim(), Id_Producto, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_codigo_barras.Text = "";
                        _Buscador(Id_Producto);
                    }
                    else
                    {
                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe colocar un codigo de barras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_quitar_Click(object sender, EventArgs e)
        {
            if (dtg_prd_codigoBarras.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea quitar el codigo de barras seleccionado?",
                       "Confirmar",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_prd_codigoBarras.SelectedRows.Count > 0)
                    {
                        var row = dtg_prd_codigoBarras.SelectedRows[0];
                        if (row.Cells["cl_id_producto"].Value != null)
                        {
                            Id_producto_ = Convert.ToInt32(row.Cells["cl_id_producto"].Value);
                            CodigoBarras_ = row.Cells["cl_codigo_barras"].Value.ToString() ?? "";

                            var resp = await _IProducto.EliminarCodigoBarras(CodigoBarras_, Id_producto_);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _Buscador(Id_Producto);
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
                MessageBox.Show("No hay codigo de barra seleccionado para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
