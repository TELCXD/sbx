using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.RangoNumeracion;
using System.Globalization;

namespace sbx
{
    public partial class Productos : Form
    {
        private dynamic? _Permisos;
        private AgregarProducto? _AgregarProducto;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private int Id_Producto = 0;

        public Productos(IServiceProvider serviceProvider, IProducto producto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = producto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaProductos();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarProducto = _serviceProvider.GetRequiredService<AgregarProducto>();
                _AgregarProducto.Permisos = _Permisos;
                _AgregarProducto.Id_Producto = 0;
                _AgregarProducto.FormClosed += (s, args) => _AgregarProducto = null;
                _AgregarProducto.ShowDialog();
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
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            //btn_eliminar.Enabled = item.ToUpdate == 1 ? true : false;
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

        private async Task ConsultaProductos()
        {
            errorProvider1.Clear();
            if (cbx_campo_filtro.Text == "Id") 
            {
                bool esNumerico = int.TryParse(txt_buscar.Text, out int valor);
                if (!esNumerico) 
                {
                    errorProvider1.SetError(txt_buscar, "Ingrese un valor numerico");
                    return;
                }
            }

            var resp = await _IProducto.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_producto.Rows.Clear();

            if (resp.Data != null) 
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_producto.Rows.Add(
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.Nombre,
                            0,
                            item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                            item.PrecioBase.ToString("N2", new CultureInfo("es-CO")),
                            item.Iva.ToString().Replace('.', ','),
                            item.EsInventariable == true ? "Si" : "No",
                            item.NombreUnidadMedida,
                            item.NombreMarca,
                            item.NombreCategoria);
                    }
                }
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                if (dtg_producto.SelectedRows.Count > 0)
                {
                    var row = dtg_producto.SelectedRows[0];
                    if (row.Cells["cl_idProducto"].Value != null)
                    {
                        Id_Producto = Convert.ToInt32(row.Cells["cl_idProducto"].Value);
                        if (_Permisos != null)
                        {
                            _AgregarProducto = _serviceProvider.GetRequiredService<AgregarProducto>();
                            _AgregarProducto.Permisos = _Permisos;
                            _AgregarProducto.Id_Producto = Id_Producto;
                            _AgregarProducto.FormClosed += (s, args) => _AgregarProducto = null;
                            _AgregarProducto.ShowDialog();
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
