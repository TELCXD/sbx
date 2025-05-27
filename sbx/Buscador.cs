
using Newtonsoft.Json;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using System.Data;
using System.Windows.Forms;

namespace sbx
{
    public partial class Buscador : Form
    {
        private string? _Origen;
        private readonly IProveedor _IProveedor;
        private readonly IProducto _IProducto;
        private readonly ICliente _ICliente;
        public delegate void EnviarId(int id);
        public event EnviarId EnviaId;
        int fila = 0;
        int Id = 0;

        public Buscador(IProveedor proveedor, IProducto producto, ICliente cliente)
        {
            InitializeComponent();
            _IProveedor = proveedor;
            _IProducto = producto;
            _ICliente = cliente;
        }

        public string? Origen
        {
            get => _Origen;
            set => _Origen = value;
        }

        private void Buscador_Load(object sender, EventArgs e)
        {
            CargaDatosIniciales();
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            switch (Origen)
            {
                case "Entradas_busca_proveedor":
                    await ConsultaProveedor();
                    break;
                case "Entradas_busca_producto":
                    await ConsultaProducto();
                    break;
                case "Salidas_busca_proveedor":
                    await ConsultaProveedor();
                    break;
                case "Salidas_busca_producto":
                    await ConsultaProducto();
                    break; 
                case "AgregaPrecios_busca_producto":
                    await ConsultaProducto();
                    break;
                case "AgregaPrecios_busca_cliente":
                    await ConsultaCliente();
                    break;
                case "Add_listaPrecio_busca_producto":
                    await ConsultaProducto();
                    break; 
                default:
                    break; 
            }
        }

        private void CargaDatosIniciales()
        {
            List<string> opciones = new List<string>();

            switch (Origen)
            {
                case "Entradas_busca_proveedor":
                    opciones = new List<string> { "Nombre", "Num Doc" };
                    break;
                case "Entradas_busca_producto":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                case "Salidas_busca_proveedor":
                    opciones = new List<string> { "Nombre", "Num Doc" };
                    break;
                case "Salidas_busca_producto":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                case "AgregaPrecios_busca_producto":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                case "AgregaPrecios_busca_cliente":
                    opciones = new List<string> { "Nombre", "Num Doc" };
                    break;
                case "Add_listaPrecio_busca_producto":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                default:
                    break;
            }

            cbx_campo_filtro.DataSource = opciones;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private async Task ConsultaProveedor()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IProveedor.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dataTable.Columns.Add("Activo", typeof(string));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Estado"] != DBNull.Value)
                        {
                            bool valor = Convert.ToBoolean(row["Estado"]);
                            row["Activo"] = valor ? "Sí" : "No";
                        }
                        else
                        {
                            row["Activo"] = "No";
                        }
                    }

                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["IdProveedor"].Visible = false;
                    dtg_buscador.Columns["IdIdentificationType"].Visible = false;
                    dtg_buscador.Columns["CreationDate"].Visible = false;
                    dtg_buscador.Columns["UpdateDate"].Visible = false;
                    dtg_buscador.Columns["IdUserAction"].Visible = false;
                    dtg_buscador.Columns["Estado"].Visible = false;
                }
            }
        }

        private async Task ConsultaProducto()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IProducto.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {                    
                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["IdProducto"].Visible = false;
                    dtg_buscador.Columns["CostoBase"].Visible = false;
                    dtg_buscador.Columns["PrecioBase"].Visible = false;
                    dtg_buscador.Columns["EsInventariable"].Visible = false;
                    dtg_buscador.Columns["Iva"].Visible = false;
                    dtg_buscador.Columns["IdCategoria"].Visible = false;
                    dtg_buscador.Columns["NombreCategoria"].Visible = false;
                    dtg_buscador.Columns["IdMarca"].Visible = false;
                    dtg_buscador.Columns["NombreMarca"].Visible = false;
                    dtg_buscador.Columns["UpdateDate"].Visible = false;
                    dtg_buscador.Columns["IdUnidadMedida"].Visible = false;
                    dtg_buscador.Columns["NombreUnidadMedida"].Visible = false;
                    dtg_buscador.Columns["CreationDate"].Visible = false;
                    dtg_buscador.Columns["UpdateDate"].Visible = false;
                    dtg_buscador.Columns["IdUserAction"].Visible = false;
                }
            }
        }

        private async Task ConsultaCliente()
        {
            dtg_buscador.DataSource = null;

            var resp = await _ICliente.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dataTable.Columns.Add("Activo", typeof(string));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Estado"] != DBNull.Value)
                        {
                            bool valor = Convert.ToBoolean(row["Estado"]);
                            row["Activo"] = valor ? "Sí" : "No";
                        }
                        else
                        {
                            row["Activo"] = "No";
                        }
                    }

                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["IdCliente"].Visible = false;
                    dtg_buscador.Columns["IdIdentificationType"].Visible = false;
                    dtg_buscador.Columns["CreationDate"].Visible = false;
                    dtg_buscador.Columns["UpdateDate"].Visible = false;
                    dtg_buscador.Columns["IdUserAction"].Visible = false;
                    dtg_buscador.Columns["Estado"].Visible = false;
                }
            }
        }

        private void dtg_buscador_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_buscador.Rows.Count > 0)
            {
                fila = dtg_buscador.CurrentRow.Index;
                Id = Convert.ToInt32(dtg_buscador[0, fila].Value);
                EnviaId(Id);
                this.Close();
            }
        }
    }
}
