using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Proveedor;
using System.Data;

namespace sbx
{
    public partial class Proveedores : Form
    {
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProveedor _IProveedor;
        private AgregaProveedor? _AgregaProveedor;
        private readonly IParametros _IParametros;
        private int Id_Proveedor = 0;
        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";

        public Proveedores(IServiceProvider serviceProvider, IProveedor proveedor, IParametros iParametros)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProveedor = proveedor;
            _IParametros = iParametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Proveedores_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;

            BuscarPor = "";
            ModoRedondeo = "N/A";
            MultiploRendondeo = "50";

            var DataParametros = await _IParametros.List("");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    foreach (var itemParametros in DataParametros.Data)
                    {
                        switch (itemParametros.Nombre)
                        {
                            case "Tipo filtro producto":
                                BuscarPor = itemParametros.Value;
                                break;
                            case "Modo Redondeo":
                                ModoRedondeo = itemParametros.Value;
                                break;
                            case "Multiplo Rendondeo":
                                MultiploRendondeo = itemParametros.Value;
                                break;
                            default:
                                break;
                        }
                    }

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
                        case "proveedor":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_eliminar.Enabled = item.ToDelete == 1 ? true : false;
                            btn_exportar.Enabled = item.ToRead == 1 ? true : false;
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
            await ConsultaProveedor();
        }

        private async Task ConsultaProveedor()
        {
            errorProvider1.Clear();

            var resp = await _IProveedor.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_proveedor.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_proveedor.Rows.Clear();
                    foreach (var item in resp.Data)
                    {
                        dtg_proveedor.Rows.Add(
                            item.IdProveedor,
                            item.IdentificationType,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.CityName,
                            item.Direccion,
                            item.Telefono,
                            item.Email,
                            item.Estado,
                            item.NombreTipoContribuyente,
                            item.NombreResponsabilidad,
                            item.NombreResponsabilidadTributaria);
                    }
                }
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaProveedor = _serviceProvider.GetRequiredService<AgregaProveedor>();
                _AgregaProveedor.Permisos = _Permisos;
                _AgregaProveedor.Id_Proveedor = 0;
                _AgregaProveedor.FormClosed += (s, args) => _AgregaProveedor = null;
                _AgregaProveedor.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_proveedor.Rows.Count > 0)
            {
                if (dtg_proveedor.SelectedRows.Count > 0)
                {
                    var row = dtg_proveedor.SelectedRows[0];
                    if (row.Cells["cl_IdProveedor"].Value != null)
                    {
                        Id_Proveedor = Convert.ToInt32(row.Cells["cl_IdProveedor"].Value);

                        if (Id_Proveedor > 1)
                        {
                            if (_Permisos != null)
                            {
                                _AgregaProveedor = _serviceProvider.GetRequiredService<AgregaProveedor>();
                                _AgregaProveedor.Permisos = _Permisos;
                                _AgregaProveedor.Id_Proveedor = Id_Proveedor;
                                _AgregaProveedor.FormClosed += (s, args) => _AgregaProveedor = null;
                                _AgregaProveedor.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("El proveedor por defecto no puede ser modificado", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_proveedor.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar proveedor seleccionada?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_proveedor.SelectedRows.Count > 0)
                    {
                        var row = dtg_proveedor.SelectedRows[0];
                        if (row.Cells["cl_IdProveedor"].Value != null)
                        {
                            Id_Proveedor = Convert.ToInt32(row.Cells["cl_IdProveedor"].Value);

                            if (Id_Proveedor > 1)
                            {
                                var resp = await _IProveedor.Eliminar(Id_Proveedor);

                                if (resp != null)
                                {
                                    if (resp.Flag == true)
                                    {
                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        await ConsultaProveedor();
                                    }
                                    else
                                    {
                                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No es posible eliminar este proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void btn_exportar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;
            dtg_proveedor.Enabled = false;

            var resp = await _IProveedor.BuscarExportar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

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
                else
                {
                    this.Cursor = Cursors.Default;
                    panel1.Enabled = true;
                    dtg_proveedor.Enabled = true;
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
                dtg_proveedor.Enabled = true;
            }
        }

        public void ExportarExcel(DataTable dataTable)
        {
            using var sfd = new SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos de Excel (*.xlsx)|*.xlsx",
                FileName = "ExportadoProveedor.xlsx"
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
                    dtg_proveedor.Enabled = true;

                    MessageBox.Show("Archivo exportado con éxito.", "Exportación completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    panel1.Enabled = true;
                    dtg_proveedor.Enabled = true;

                    MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
                dtg_proveedor.Enabled = true;
            }
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProveedor();
            }
        }
    }
}
