using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Producto;

namespace sbx
{
    public partial class AddProductoPromocion : Form
    {
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private int IdProducto;
        public delegate void EnviarProducto(int IdProducto);
        public event EnviarProducto Enviar_producto;
        private dynamic? _Permisos;

        public AddProductoPromocion(IServiceProvider serviceProvider, IProducto iProducto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = iProducto;
        }

        private void AddProductoPromocion_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "promociones":
                            btn_add_producto.Enabled = item.ToCreate == 1 ? true : false;
                            btn_busca_producto.Enabled = item.ToRead == 1 ? true : false;
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

        private void btn_busca_producto_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Add_listaPrecio_busca_producto";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProducto.List(id);
            if (resp.Data != null)
            {
                IdProducto = id;
                txt_producto.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                lbl_nombre_producto.Text = resp.Data[0].Nombre;
            }
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_producto.Text.Trim() != "")
            {
                Enviar_producto(IdProducto);
                this.Close();
            }
            else
            {
                if (txt_producto.Text.Trim() == "") { errorProvider1.SetError(txt_producto, "Debe seleccionar un producto"); }
            }
        }
    }
}
