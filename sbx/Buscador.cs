using Newtonsoft.Json;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.ConversionProducto;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.Usuario;
using sbx.core.Interfaces.Venta;
using System.Data;
using System.Globalization;

namespace sbx
{
    public partial class Buscador : Form
    {
        private string? _Origen;
        private readonly IProveedor _IProveedor;
        private readonly IProducto _IProducto;
        private readonly ICliente _ICliente;
        private readonly IUsuario _IUsuario;
        private readonly IVenta _IVenta;
        public delegate void EnviarId(int id);
        public event EnviarId EnviaId;
        private readonly IParametros _IParametros;
        int fila = 0;
        int Id = 0;

        public Buscador(IProveedor proveedor, IProducto producto, ICliente cliente, IUsuario usuario, IVenta venta, IParametros parametros)
        {
            InitializeComponent();
            _IProveedor = proveedor;
            _IProducto = producto;
            _ICliente = cliente;
            _IUsuario = usuario;
            _IVenta = venta;
            _IParametros = parametros;
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
            panel1.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
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
                    await ConsultaProductoPrecioCliente();
                    break;
                case "AgregaPrecios_busca_cliente":
                    await ConsultaClientePrecios();
                    break;
                case "Add_listaPrecio_busca_producto":
                    await ConsultaProductoPrecioCliente();
                    break;
                case "Add_AgregaVenta_busca_producto":
                    await ConsultaProducto();
                    break;
                case "Add_AgregaVenta_busca_cliente":
                    await ConsultaCliente();
                    break;
                case "Busca_usuario":
                    await ConsultaUsuarios();
                    break;
                case "Busca_factura":
                    await ConsultaFactura();
                    break;
                case "Add_ConversionProductoPadre":
                    await ConsultaProductoPadre();
                    break;
                case "Add_ConversionProductoHijo":
                    await ConsultaProductoHijo();
                    break;
                default:
                    break;
            }
            this.Cursor = Cursors.Default;
            panel1.Enabled = true;
        }

        private async void CargaDatosIniciales()
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
                case "Add_AgregaVenta_busca_producto":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                case "Add_AgregaVenta_busca_cliente":
                    opciones = new List<string> { "Nombre", "Num Doc" };
                    break;
                case "Busca_usuario":
                    opciones = new List<string> { "Nombre" };
                    break;
                case "Busca_factura":
                    opciones = new List<string> { "Factura", "Identificacion cliente", "Nombre cliente" };
                    break;
                case "Add_ConversionProductoPadre":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                case "Add_ConversionProductoHijo":
                    opciones = new List<string> { "Nombre", "Id", "Sku", "Codigo barras" };
                    break;
                default:
                    break;
            }

            cbx_campo_filtro.DataSource = opciones;
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

                    dtg_buscador.Columns["Nombre"].Width = 500;
                    dtg_buscador.Columns["IdProducto"].Visible = true;
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

        private async Task ConsultaProductoPrecioCliente()
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

                    dtg_buscador.Columns["Nombre"].Width = 450;
                    dtg_buscador.Columns["IdProducto"].Visible = true;
                    dtg_buscador.Columns["CostoBase"].Visible = false;
                    dtg_buscador.Columns["PrecioBase"].DefaultCellStyle.Format = "N2";
                    dtg_buscador.Columns["PrecioBase"].DefaultCellStyle.FormatProvider = new CultureInfo("es-CO");
                    dtg_buscador.Columns["PrecioBase"].Visible = true;
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

        private async Task ConsultaClientePrecios()
        {
            dtg_buscador.DataSource = null;

            var resp = await _ICliente.BuscarPrecio(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

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

        private async Task ConsultaUsuarios()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IUsuario.List(0);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dataTable.Columns.Add("Activo", typeof(string));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Activo"] != DBNull.Value)
                        {
                            bool valor = Convert.ToBoolean(row["Activo"]);
                            row["Activo"] = valor ? "Sí" : "No";
                        }
                        else
                        {
                            row["Activo"] = "No";
                        }
                    }

                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["CreationDate"].Visible = false;
                    dtg_buscador.Columns["UpdateDate"].Visible = false;
                    dtg_buscador.Columns["IdUserAction"].Visible = false;
                }
            }
        }

        private async Task ConsultaFactura()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IVenta.BuscarFactura(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dtg_buscador.DataSource = dataTable;
                }
            }
        }

        private async Task ConsultaProductoPadre()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IProducto.BuscarProductoPadre(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["Nombre"].Width = 500;
                    dtg_buscador.Columns["IdProducto"].Visible = true;
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

        private async Task ConsultaProductoHijo()
        {
            dtg_buscador.DataSource = null;

            var resp = await _IProducto.BuscarProductoHijo(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            if (resp.Data != null)
            {
                var json = JsonConvert.SerializeObject(resp.Data);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                if (dataTable.Rows.Count > 0)
                {
                    dtg_buscador.DataSource = dataTable;

                    dtg_buscador.Columns["Nombre"].Width = 500;
                    dtg_buscador.Columns["IdProducto"].Visible = true;
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

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                panel1.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
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
                    case "Add_AgregaVenta_busca_producto":
                        await ConsultaProducto();
                        break;
                    case "Add_AgregaVenta_busca_cliente":
                        await ConsultaCliente();
                        break;
                    case "Busca_usuario":
                        await ConsultaUsuarios();
                        break;
                    case "Busca_factura":
                        await ConsultaFactura();
                        break;
                    case "Add_ConversionProductoPadre":
                        await ConsultaProductoPadre();
                        break;
                    case "Add_ConversionProductoHijo":
                        await ConsultaProductoHijo();
                        break;
                    default:
                        break;
                }
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
            }
        }
    }
}
