using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Producto;
using System.Data;
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
        private ListaPrecios? _ListaPrecios;
        private Promociones? _Promociones;
        private readonly IEntradaInventario _IEntradaInventario;

        public Productos(IServiceProvider serviceProvider, IProducto producto, IEntradaInventario entradaInventario)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = producto;
            _IEntradaInventario = entradaInventario;
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
                            btn_importar.Enabled = item.ToCreate == 1 ? true : false;
                            //btn_eliminar.Enabled = item.ToUpdate == 1 ? true : false;
                            break;
                        case "listaPrecios":
                            btn_lista_precios.Enabled = item.ToRead == 1 ? true : false;
                            break;
                        case "promociones":
                            btn_promociones.Enabled = item.ToRead == 1 ? true : false;
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
                            item.Stock,
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

        private void btn_lista_precios_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _ListaPrecios = _serviceProvider.GetRequiredService<ListaPrecios>();
                _ListaPrecios.Permisos = _Permisos;
                _ListaPrecios.FormClosed += (s, args) => _ListaPrecios = null;
                _ListaPrecios.ShowDialog();
            }
        }

        private void btn_promociones_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _Promociones = _serviceProvider.GetRequiredService<Promociones>();
                _Promociones.Permisos = _Permisos;
                _Promociones.FormClosed += (s, args) => _Promociones = null;
                _Promociones.ShowDialog();
            }
        }

        private async void btn_importar_Click(object sender, EventArgs e)
        {

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Archivos Excel (*.xlsx)|*.xlsx"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable dt = LeerExcel(ofd.FileName);
                var resp = await _IEntradaInventario.CargueMasivoProductoEntrada(dt, Convert.ToInt32(_Permisos?[0]?.IdUser));

                this.Cursor = Cursors.Default;

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
        }

        private DataTable LeerExcel(string path)
        {
            var dt = new DataTable();

            using var workbook = new XLWorkbook(path);
            var worksheet = workbook.Worksheets.Worksheet(1); // Primera hoja
            bool primeraFila = true;

            foreach (var fila in worksheet.RowsUsed())
            {
                if (primeraFila)
                {
                    foreach (var celda in fila.Cells())
                    {
                        dt.Columns.Add(celda.Value.ToString());
                    }
                    primeraFila = false;
                }
                else
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (var celda in fila.Cells())
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = celda.Value.ToString();
                        i++;
                    }
                }
            }

            return dt;
        }
    }
}
