using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class PreciosClientes : Form
    {
        private dynamic? _Permisos;
        private int Id_precios_cliente = 0;
        private readonly IPrecioCliente _IPrecioCliente;

        public PreciosClientes(IPrecioCliente precioCliente)
        {
            InitializeComponent();
            _IPrecioCliente = precioCliente;
        }

        private void PreciosClientes_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            cbx_client_producto.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
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
                        case "preciosClientes":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            //btn_eliminar.Enabled = item.ToDelete == 1 ? true : false;
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

        private void cbx_client_producto_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();
            if (cbx_client_producto.Text == "Cliente")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Num Doc" });

            }
            else if (cbx_client_producto.Text == "Producto")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            }
            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaPrecioCliente();
        }

        private async Task ConsultaPrecioCliente()
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

            var resp = await _IPrecioCliente.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_producto.Text);

            dtg_cliente_precio.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_cliente_precio.Rows.Add(
                            item.llavePrimaria,
                            item.IdCliente,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.TipoCliente,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.Nombre,
                            item.PrecioEspecial.ToString("N2", new CultureInfo("es-CO")),
                            item.FechaInicio,
                            item.FechaFin);
                    }
                }
            }
        }
    }
}
