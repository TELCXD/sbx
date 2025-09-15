using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Parametros;
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
        private readonly IParametros _IParametros;

        public Productos(IServiceProvider serviceProvider, IProducto producto, IEntradaInventario entradaInventario, IParametros parametros)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = producto;
            _IEntradaInventario = entradaInventario;
            _IParametros = parametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Productos_Load(object sender, EventArgs e)
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
                            btn_exportar.Enabled = item.ToRead == 1 ? true : false;
                            btn_eliminar.Enabled = item.ToDelete == 1 ? true : false;
                            btn_editar_masivo.Enabled = item.ToUpdate == 1 ? true : false;
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
                            item.NombreTributo,
                            item.Impuesto.ToString(new CultureInfo("es-CO")),
                            item.EsInventariable == true ? "Si" : "No",
                            item.NombreUnidadMedida,
                            item.NombreMarca,
                            item.NombreCategoria);
                    }
                }
            }

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
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
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;
            dtg_producto.Enabled = false;

            try
            {
                using OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Archivos Excel (*.xlsx)|*.xlsx"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (EstaArchivoEnUso(ofd.FileName))
                    {
                        MessageBox.Show("⚠️ El archivo Excel está siendo utilizado por otra aplicación.\n\n" +
                                       "Por favor:\n" +
                                       "1. Cierre Microsoft Excel\n" +
                                       "2. Guarde los cambios si es necesario\n" +
                                       "3. Intente nuevamente",
                                       "Archivo en uso",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);

                        return;
                    }

                    DataTable dt = LeerExcel(ofd.FileName);

                    if (dt.Rows.Count >= 1)
                    {
                        int contador = 1;
                        int Error = 0;
                        string Mensaje = "";
                        foreach (DataRow item in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(item[0]?.ToString()?.Trim()))
                            {
                                var Exist = await _IProducto.ExisteSku(item[0].ToString() ?? "", 0);
                                if (Exist == true) { Mensaje = "El sku ya existe, "; Error++; }
                            }

                            if (!string.IsNullOrEmpty(item[1]?.ToString()?.Trim()))
                            {
                                var Exist = await _IProducto.ExisteCodigoBarras(item[1].ToString() ?? "", 0);
                                if (Exist == true) { Mensaje = "El codigo de barras ya existe, "; Error++; }
                            }

                            if (string.IsNullOrEmpty(item[2]?.ToString()?.Trim()))
                            {
                                Mensaje = "El nombre es obligatorio, "; Error++;
                            }
                            else
                            {
                                string nombre = item[3].ToString()?.Trim() != "" ? item[2].ToString()?.Trim() + " - " + item[3].ToString()?.Trim() : item[2].ToString()?.Trim() ?? "";
                                var Exist = await _IProducto.ExisteNombre(nombre, 0);
                                if (Exist == true) { Mensaje = "El nombre ya existe, "; Error++; }
                            }

                            if (string.IsNullOrEmpty(item[4]?.ToString()?.Trim()))
                            {
                                Mensaje += "El costo unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[4]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (!decimal.TryParse(valor, out decimal costoUnitario))
                                    {
                                        Mensaje += "El costo unitario debe ser un valor numérico válido, ";
                                        Error++;
                                    }
                                    else if (costoUnitario < 0)
                                    {
                                        Mensaje += "El costo unitario no puede ser negativo, ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El costo unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                    Error++;
                                }
                            }

                            if (string.IsNullOrEmpty(item[5]?.ToString()?.Trim()))
                            {
                                Mensaje += "El precio de venta unitario es obligatorio y debe ser mayor a cero (0), ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[5]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (!decimal.TryParse(valor, out decimal precioVentaUnitario))
                                    {
                                        Mensaje += "El precio de venta unitario debe ser un valor numérico válido, ";
                                        Error++;
                                    }
                                    else if (precioVentaUnitario == 0)
                                    {
                                        Mensaje += "El precio de venta unitario debe ser mayor a cero (0), ";
                                        Error++;
                                    }
                                    else if (precioVentaUnitario < 0)
                                    {
                                        Mensaje += "El precio de venta unitario no puede ser negativo, ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El precio de venta unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                    Error++;
                                }
                            }

                            if (string.IsNullOrEmpty(item[6]?.ToString()?.Trim()))
                            {
                                Mensaje += "El stock es obligatorio, en caso de no tener colocar el valor en cero (0) ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[6]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (!decimal.TryParse(valor, out decimal precioVentaUnitario))
                                    {
                                        Mensaje += "El stock debe ser un valor numérico válido, ";
                                        Error++;
                                    }
                                    else if (precioVentaUnitario < 0)
                                    {
                                        Mensaje += "El stock no puede ser negativo, ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El stock es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                    Error++;
                                }
                            }

                            if (string.IsNullOrEmpty(item[7]?.ToString()?.Trim()))
                            {
                                Mensaje += "El Impuesto es obligatorio, en caso de no tener colocar el valor en cero (0) ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[7]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (!decimal.TryParse(valor, out decimal Impuesto))
                                    {
                                        Mensaje += "El impuesto debe ser un valor numérico válido, ";
                                        Error++;
                                    }
                                    else if (Impuesto < 0)
                                    {
                                        Mensaje += "El impuesto no puede ser negativo, ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El impuesto es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                    Error++;
                                }
                            }

                            if (string.IsNullOrEmpty(item[8]?.ToString()?.Trim()))
                            {
                                Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[8]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (valor != "IVA" && valor != "INC")
                                    {
                                        Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                    Error++;
                                }
                            }

                            if (!string.IsNullOrEmpty(item[9]?.ToString()?.Trim()))
                            {
                                bool esFecha = DateTime.TryParse(item[9]?.ToString()?.Trim(), out DateTime fecha);
                                if (!esFecha) 
                                {
                                    Mensaje += "La fecha de vencimiento no tiene un formato de fecha valido ";
                                    Error++;
                                }
                            }

                            if (Error > 0)
                            {
                                MessageBox.Show("Hay datos erroneos en fila " + contador + " : " + Mensaje + "Favor revise y vuelta a intentar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            contador++;
                        }

                        var resp = await _IEntradaInventario.CargueMasivoProductoEntrada(dt, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                    else
                    {
                        MessageBox.Show("No hay datos para procesar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally 
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
                dtg_producto.Enabled = true;
            }
        }

        private DataTable LeerExcel(string path)
        {
            var dt = new DataTable();

            using var workbook = new XLWorkbook(path);
            var worksheet = workbook.Worksheets.Worksheet(1);

            int startRow = 2;
            int endRow = Math.Min(worksheet.LastRowUsed()?.RowNumber() ?? 100, 5000);
            int startColumn = 1; // Columna A
            int endColumn = 10;   // Columna J

            // Crear columnas (A-J)
            for (int col = startColumn; col <= endColumn; col++)
            {
                dt.Columns.Add("Col" + col);
            }

            //Identificar si cumple con los campos requeridos
            int Error = 0;
            for (int col = startColumn; col <= endColumn; col++)
            {
                var cellValue = worksheet.Cell(1, col).Value.ToString() ?? string.Empty;
                switch (col)
                {
                    case 1:
                        if (cellValue != "Sku") { Error++; }
                        break;
                    case 2:
                        if (cellValue != "Codigo barras") { Error++; }
                        break;
                    case 3:
                        if (cellValue != "Nombre *") { Error++; }
                        break;
                    case 4:
                        if (cellValue != "Marca") { Error++; }
                        break;
                    case 5:
                        if (cellValue != "Costo unitario *") { Error++; }
                        break;
                    case 6:
                        if (cellValue != "Precio venta Unitario *") { Error++; }
                        break;
                    case 7:
                        if (cellValue != "Stock *") { Error++; }
                        break;
                    case 8:
                        if (cellValue != "Impuesto % *") { Error++; }
                        break;
                    case 9:
                        if (cellValue != "Tributo (IVA, INC)") { Error++; }
                        break;
                    case 10:
                        if (cellValue != "FechaVencimiento") { Error++; }
                        break;
                    default:
                        break;
                }
            }

            if (Error == 0)
            {
                for (int row = startRow; row <= endRow; row++)
                {
                    var dataRow = dt.NewRow();

                    for (int col = startColumn; col <= endColumn; col++)
                    {
                        var cellValue = worksheet.Cell(row, col).Value.ToString() ?? string.Empty;
                        dataRow[col - startColumn] = cellValue;
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            else
            {
                MessageBox.Show(@"El archivo no cuenta con los campos necesarios para realizar el proceso, 
                                  debe tener los siguientes campos: Sku, Codigo barras,Nombre *, Marca, Costo unnitario *,
                                  Precio venta Unitario *, Stock *, Impuesto % *, Tributo (IVA, INC), FechaVencimiento Por favor revise", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private DataTable LeerExcel2(string path)
        {
            var dt = new DataTable();

            using var workbook = new XLWorkbook(path);
            var worksheet = workbook.Worksheets.Worksheet(1);

            int startRow = 2;
            int endRow = Math.Min(worksheet.LastRowUsed()?.RowNumber() ?? 100, 5000);
            int startColumn = 1; // Columna A
            int endColumn = 11;   // Columna K

            // Crear columnas (A-K)
            for (int col = startColumn; col <= endColumn; col++)
            {
                dt.Columns.Add("Col" + col);
            }

            //Identificar si cumple con los campos requeridos
            int Error = 0;
            for (int col = startColumn; col <= endColumn; col++)
            {
                var cellValue = worksheet.Cell(1, col).Value.ToString() ?? string.Empty;
                switch (col)
                {
                    case 1:
                        if (cellValue != "Id") { Error++; }
                        break;
                    case 2:
                        if (cellValue != "Sku") { Error++; }
                        break;
                    case 3:
                        if (cellValue != "Codigo barras") { Error++; }
                        break;
                    case 4:
                        if (cellValue != "Nombre *") { Error++; }
                        break;
                    case 5:
                        if (cellValue != "Costo unitario *") { Error++; }
                        break;
                    case 6:
                        if (cellValue != "Precio venta Unitario *") { Error++; }
                        break;
                    case 7:
                        if (cellValue != "Impuesto % *") { Error++; }
                        break;
                    case 8:
                        if (cellValue != "Tributo (IVA, INC)") { Error++; }
                        break;
                    case 9:
                        if (cellValue != "Movimiento") { Error++; }
                        break;
                    case 10:
                        if (cellValue != "Cantidad") { Error++; }
                        break;
                    case 11:
                        if (cellValue != "FechaVencimiento") { Error++; }
                        break;
                    default:
                        break;
                }
            }

            if (Error == 0) 
            {
                for (int row = startRow; row <= endRow; row++)
                {
                    var dataRow = dt.NewRow();

                    for (int col = startColumn; col <= endColumn; col++)
                    {
                        var cellValue = worksheet.Cell(row, col).Value.ToString() ?? string.Empty;
                        dataRow[col - startColumn] = cellValue;
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            else
            {
                MessageBox.Show(@"El archivo no cuenta con los campos necesarios para realizar el proceso, 
                                  debe tener los siguientes campos: Id, Sku, Codigo barras,Nombre *, Costo unnitario *,
                                  Precio venta Unitario *, Impuesto % *, Tributo (IVA, INC), Movimiento, Cantidad, FechaVencimiento. Por favor revise", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private static bool EstaArchivoEnUso(string path)
        {
            if (!File.Exists(path))
                return false;

            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
            catch
            {
                return false;
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
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar el producto seleccionado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_producto.SelectedRows.Count > 0)
                    {
                        var row = dtg_producto.SelectedRows[0];
                        if (row.Cells["cl_idProducto"].Value != null)
                        {
                            Id_Producto = Convert.ToInt32(row.Cells["cl_idProducto"].Value);

                            var resp = await _IProducto.Eliminar(Id_Producto);

                            if (resp != null)
                            {
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
            }
            else
            {
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_buscar_KeyUp(object sender, KeyEventArgs e)
        {

        }

        public void ExportarExcel(DataTable dataTable)
        {
            using var sfd = new SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos de Excel (*.xlsx)|*.xlsx",
                FileName = "ExportadoProductos.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Datos");

                    // Encabezados
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 1).Value = dataTable.Columns[col].ColumnName;
                    }

                    // Datos
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = dataTable.Rows[row][col]?.ToString() ?? string.Empty;
                        }
                    }

                    worksheet.Columns().AdjustToContents(); // Ajustar ancho de columnas
                    workbook.SaveAs(sfd.FileName);

                    this.Cursor = Cursors.Default;
                    panel1.Enabled = true;
                    dtg_producto.Enabled = true;

                    MessageBox.Show("Archivo exportado con éxito.", "Exportación completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    panel1.Enabled = true;
                    dtg_producto.Enabled = true;
                }
            }
        }

        private async void btn_exportar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;
            dtg_producto.Enabled = false;

            try
            {
                var resp = await _IProducto.BuscarExportarExcel(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

                if (resp != null)
                {
                    if (resp.Data != null)
                    {
                        string json = JsonConvert.SerializeObject(resp.Data);

                        DataTable? dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                        if (dataTable != null)
                        {
                            ExportarExcel(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
                dtg_producto.Enabled = true;
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
                        _AgregarProducto = _serviceProvider.GetRequiredService<AgregarProducto>();
                        _AgregarProducto.Permisos = _Permisos;
                        foreach (DataGridViewRow rows in dtg_producto.SelectedRows)
                        {
                            _AgregarProducto.Id_Producto = Convert.ToInt32(rows.Cells["cl_idProducto"].Value);
                        }
                        _AgregarProducto.FormClosed += (s, args) => _AgregarProducto = null;
                        _AgregarProducto.ShowDialog();
                    }
                }
            }
        }

        private async void btn_editar_masivo_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;
            dtg_producto.Enabled = false;
            string Mensaje = "";
            int contador = 1;
            int Error = 0;

            try
            {
                using OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Archivos Excel (*.xlsx)|*.xlsx"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (EstaArchivoEnUso(ofd.FileName))
                    {
                        MessageBox.Show("⚠️ El archivo Excel está siendo utilizado por otra aplicación.\n\n" +
                                       "Por favor:\n" +
                                       "1. Cierre Microsoft Excel\n" +
                                       "2. Guarde los cambios si es necesario\n" +
                                       "3. Intente nuevamente",
                                       "Archivo en uso",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);

                        return;
                    }

                    DataTable dt = LeerExcel2(ofd.FileName);

                    if (dt.Rows.Count >= 1)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            if (string.IsNullOrEmpty(item[0]?.ToString()?.Trim()))
                            {
                                Mensaje += "El Id producto es obligatorio ";
                                Error++;
                            }
                            else
                            {
                                string valor = item[0]?.ToString()?.Trim() ?? "";
                                if (valor != "")
                                {
                                    if (!int.TryParse(valor, out int IdProducto))
                                    {
                                        Mensaje += "El Id producto debe ser un valor numérico entero válido, ";
                                        Error++;
                                    }
                                    else if (IdProducto < 0)
                                    {
                                        Mensaje += "El Id producto no puede ser negativo, ";
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Mensaje += "El Id producto es obligatorio ";
                                    Error++;
                                }
                            }

                            if (Error == 0) 
                            {
                                if (!string.IsNullOrEmpty(item[0]?.ToString()?.Trim()))
                                {
                                    var Exist = await _IProducto.ExisteIdProducto(Convert.ToInt32(item[0]));
                                    if (Exist == false) { Mensaje = "El Id producto NO existe, "; Error++; }
                                }

                                if (Error == 0) 
                                {
                                    if (!string.IsNullOrEmpty(item[1]?.ToString()?.Trim()))
                                    {
                                        var Exist = await _IProducto.ExisteSku(item[1].ToString() ?? "", Convert.ToInt32(item[0]));
                                        if (Exist == true) { Mensaje = "El sku ya existe, en otro Id producto, "; Error++; }
                                    }

                                    if (!string.IsNullOrEmpty(item[2]?.ToString()?.Trim()))
                                    {
                                        var Exist = await _IProducto.ExisteCodigoBarras(item[2].ToString() ?? "", Convert.ToInt32(item[0]));
                                        if (Exist == true) { Mensaje = "Codigo de barras ya existe, en otro Id producto, "; Error++; }
                                    }

                                    if (string.IsNullOrEmpty(item[3]?.ToString()?.Trim()))
                                    {
                                        Mensaje = "El nombre es obligatorio, "; Error++;
                                    }
                                    else
                                    {
                                        var Exist = await _IProducto.ExisteNombre(item[3].ToString() ?? "", Convert.ToInt32(item[0]));
                                        if (Exist == true) { Mensaje = "Nombre ya existe, en otro Id producto "; Error++; }
                                    }

                                    if (string.IsNullOrEmpty(item[4]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "El costo unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[4]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (!decimal.TryParse(valor, out decimal costoUnitario))
                                            {
                                                Mensaje += "El costo unitario debe ser un valor numérico válido, ";
                                                Error++;
                                            }
                                            else if (costoUnitario < 0)
                                            {
                                                Mensaje += "El costo unitario no puede ser negativo, ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "El costo unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                            Error++;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(item[5]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "El precio de venta unitario es obligatorio y debe ser mayor a cero (0), ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[5]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (!decimal.TryParse(valor, out decimal precioVentaUnitario))
                                            {
                                                Mensaje += "El precio de venta unitario debe ser un valor numérico válido, ";
                                                Error++;
                                            }
                                            else if (precioVentaUnitario == 0)
                                            {
                                                Mensaje += "El precio de venta unitario debe ser mayor a cero (0), ";
                                                Error++;
                                            }
                                            else if (precioVentaUnitario < 0)
                                            {
                                                Mensaje += "El precio de venta unitario no puede ser negativo, ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "El precio de venta unitario es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                            Error++;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(item[6]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "El Impuesto es obligatorio, en caso de no tener colocar el valor en cero (0) ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[6]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (!decimal.TryParse(valor, out decimal Impuesto))
                                            {
                                                Mensaje += "El impuesto debe ser un valor numérico válido, ";
                                                Error++;
                                            }
                                            else if (Impuesto < 0)
                                            {
                                                Mensaje += "El impuesto no puede ser negativo, ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "El impuesto es obligatorio, en caso de no tener colocar el valor en cero (0), ";
                                            Error++;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(item[7]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[7]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (valor != "IVA" && valor != "INC")
                                            {
                                                Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "El Tributo es obligatorio, debe ser IVA ó INC ";
                                            Error++;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(item[8]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "El Movimiento es obligatorio, debe ser Entrada ó Salida ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[8]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (valor != "Entrada" && valor != "Salida")
                                            {
                                                Mensaje += "El movimiento es obligatorio, debe ser Entrada ó Salida ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "El Movimiento es obligatorio, debe ser Entrada ó Salida ";
                                            Error++;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(item[9]?.ToString()?.Trim()))
                                    {
                                        Mensaje += "La cantidad es obligatoria, en caso de no tener colocar el valor en cero (0) ";
                                        Error++;
                                    }
                                    else
                                    {
                                        string valor = item[9]?.ToString()?.Trim() ?? "";
                                        if (valor != "")
                                        {
                                            if (!decimal.TryParse(valor, out decimal precioVentaUnitario))
                                            {
                                                Mensaje += "La cantidad debe ser un valor numérico válido, ";
                                                Error++;
                                            }
                                            else if (precioVentaUnitario < 0)
                                            {
                                                Mensaje += "La cantidad no puede ser negativo, ";
                                                Error++;
                                            }
                                        }
                                        else
                                        {
                                            Mensaje += "La cantidad es obligatoria, en caso de no tener colocar el valor en cero (0), ";
                                            Error++;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(item[10]?.ToString()?.Trim()))
                                    {
                                        bool esFecha = DateTime.TryParse(item[10]?.ToString()?.Trim(), out DateTime fecha);
                                        if (!esFecha)
                                        {
                                            Mensaje += "La fecha de vencimiento no tiene un formato de fecha valido ";
                                            Error++;
                                        }
                                    }
                                }
                            }

                            if (Error > 0)
                            {
                                MessageBox.Show("Hay datos erroneos en fila " + contador + " : " + Mensaje + "Favor revise y vuelta a intentar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            contador++;
                        }

                        var resp = await _IEntradaInventario.CargueMasivoEditarProductoEntradaSalidas(dt, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                    else
                    {
                        MessageBox.Show("No hay datos para procesar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
                dtg_producto.Enabled = true;
            }
        }
    }
}
