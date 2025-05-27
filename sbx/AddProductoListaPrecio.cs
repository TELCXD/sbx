using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class AddProductoListaPrecio : Form
    {
        private dynamic? _Permisos;
        char decimalSeparator = ',';
        public delegate void EnviarProducto(int IdProducto, decimal Precio);
        public event EnviarProducto Enviar_producto;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private int IdProducto;

        public AddProductoListaPrecio(IServiceProvider serviceProvider, IProducto iProducto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = iProducto;
        }
        
        private void AddProductoListaPrecio_Load(object sender, EventArgs e)
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
                        case "listaPrecios":
                            btn_add_precio_producto_lista.Enabled = item.ToCreate == 1 ? true : false;
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

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void btn_add_precio_producto_lista_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_producto.Text.Trim() != "" && txt_precio.Text != "")
            {
                if (Convert.ToDecimal(txt_precio.Text, new CultureInfo("es-CO")) > 0)
                {
                    decimal precio = Convert.ToDecimal(txt_precio.Text, new CultureInfo("es-CO"));
                    Enviar_producto(IdProducto, precio);
                    this.Close();
                }
                else
                {
                    errorProvider1.SetError(txt_precio, "Precio debe ser mayor a cero (0)");
                }
            }
            else
            {
                if (txt_producto.Text.Trim() == "") { errorProvider1.SetError(txt_producto, "Debe seleccionar un producto"); }
                if (txt_precio.Text.Trim() == "") { errorProvider1.SetError(txt_precio, "Debe agregar un precio"); }
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
    }
}
