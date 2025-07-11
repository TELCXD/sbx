using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class Inventario : Form
    {
        private dynamic? _Permisos;
        private Entradas? _Entradas;
        private Salidas? _Salidas;
        private ConversionProducto? _ConversionProducto;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEntradaInventario _IEntradaInventario;
        private readonly IParametros _IParametros;
        private DetalleVenta? _DetalleVenta;
        private DetalleProdDevo? _DetalleProdDevo;

        public Inventario(IServiceProvider serviceProvider, IEntradaInventario entradaInventario, IParametros iParametros)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IEntradaInventario = entradaInventario;
            _IParametros = iParametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Inventario_Load(object sender, EventArgs e)
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

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "inventario":
                            btn_entrada.Enabled = item.ToCreate == 1 ? true : false;
                            btn_salida.Enabled = item.ToUpdate == 1 ? true : false;
                            break;
                        case "conversionProducto":
                            btn_agrupar_productos.Enabled = item.ToCreate == 1 ? true : false;
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

        private void btn_entrada_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _Entradas = _serviceProvider.GetRequiredService<Entradas>();
                _Entradas.Permisos = _Permisos;
                _Entradas.FormClosed += (s, args) => _Entradas = null;
                _Entradas.ShowDialog();
            }
        }

        private async Task ConsultaInventario()
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
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            var resp = await _IEntradaInventario.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_inventario.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    decimal TotalStock = 0;

                    foreach (var item in resp.Data)
                    {
                        if (item.TipoMovimiento == "Entrada")
                        { TotalStock += item.Cantidad; }
                        else if (item.TipoMovimiento == "Salida")
                        { TotalStock -= item.Cantidad; }
                        else if (item.TipoMovimiento == "Salida por Venta")
                        { TotalStock -= item.Cantidad; }
                        else if (item.TipoMovimiento == "Entrada por Nota credito")
                        { TotalStock += item.Cantidad; }

                        dtg_inventario.Rows.Add(
                            item.IdDocumento,
                            item.Fecha,
                            item.Documento,
                            item.TipoMovimiento,
                            item.Cantidad,
                            item.IdProducto,
                            item.Nombre,
                            item.Sku,
                            item.Comentario,
                            item.Tipo,
                            item.CodigoBarras,
                            item.CodigoLote,
                            item.FechaVencimiento,
                            item.Usuario);
                    }

                    lbl_total.Text = TotalStock.ToString("N2", new CultureInfo("es-CO"));
                }
            }

            this.Cursor = Cursors.Default;
            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaInventario();
        }

        private void btn_salida_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _Salidas = _serviceProvider.GetRequiredService<Salidas>();
                _Salidas.Permisos = _Permisos;
                _Salidas.FormClosed += (s, args) => _Salidas = null;
                _Salidas.ShowDialog();
            }
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaInventario();
            }
        }

        private void btn_agrupar_productos_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _ConversionProducto = _serviceProvider.GetRequiredService<ConversionProducto>();
                _ConversionProducto.Permisos = _Permisos;
                _ConversionProducto.FormClosed += (s, args) => _ConversionProducto = null;
                _ConversionProducto.ShowDialog();
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_inventario.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar fila seleccionada?",
                       "Confirmar",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_inventario.SelectedRows.Count > 0)
                    {
                        var row = dtg_inventario.SelectedRows[0];
                        if (row.Cells["cl_id_documento"].Value != null && row.Cells["cl_movimiento"].Value != null)
                        {
                            int Id_documento = Convert.ToInt32(row.Cells["cl_id_documento"].Value);
                            string TipoMovimiento = row.Cells["cl_movimiento"].Value.ToString() ?? "";

                            if (TipoMovimiento == "Entrada")
                            {

                            }
                            else if (TipoMovimiento == "Salida")
                            {

                            }
                            else if (TipoMovimiento == "Salida por Venta")
                            {

                            }
                            else if (TipoMovimiento == "Entrada por Nota credito")
                            {

                            }

                            //var resp = await _IProducto.Eliminar(Id_documento);

                            //if (resp != null)
                            //{
                            //    if (resp.Flag == true)
                            //    {
                            //        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        await ConsultaProductos();
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    }
                            //}
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtg_inventario_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_inventario.Rows.Count > 0)
            {
                if (dtg_inventario.SelectedRows.Count > 0)
                {
                    if (_Permisos != null)
                    {
                        foreach (DataGridViewRow rows in dtg_inventario.SelectedRows)
                        {
                            string TipoMovimiento = rows.Cells["cl_movimiento"].Value.ToString() ?? "";

                            if (TipoMovimiento == "Entrada")
                            {
                                _Entradas = _serviceProvider.GetRequiredService<Entradas>();
                                _Entradas.Permisos = _Permisos;
                                _Entradas.Id_Entrada = Convert.ToInt32(rows.Cells["cl_id_documento"].Value);
                                _Entradas.FormClosed += (s, args) => _Entradas = null;
                                _Entradas.ShowDialog();
                            }
                            else if (TipoMovimiento == "Salida")
                            {
                                _Salidas = _serviceProvider.GetRequiredService<Salidas>();
                                _Salidas.Permisos = _Permisos;
                                _Salidas.Id_Salida = Convert.ToInt32(rows.Cells["cl_id_documento"].Value);
                                _Salidas.FormClosed += (s, args) => _Salidas = null;
                                _Salidas.ShowDialog();
                            }
                            else if (TipoMovimiento == "Salida por Venta")
                            {
                                _DetalleVenta = _serviceProvider.GetRequiredService<DetalleVenta>();
                                _DetalleVenta.Permisos = _Permisos;
                                _DetalleVenta.Id_Venta = Convert.ToInt32(rows.Cells["cl_id_documento"].Value);
                                _DetalleVenta.FormClosed += (s, args) => _DetalleVenta = null;
                                _DetalleVenta.ShowDialog();
                            }
                            else if (TipoMovimiento == "Entrada por Nota credito")
                            {
                                _DetalleProdDevo = _serviceProvider.GetRequiredService<DetalleProdDevo>();
                                _DetalleProdDevo.Permisos = _Permisos;
                                _DetalleProdDevo.Id_NotaCredito = Convert.ToInt32(rows.Cells["cl_id_documento"].Value);
                                _DetalleProdDevo.FormClosed += (s, args) => _DetalleProdDevo = null;
                                _DetalleProdDevo.ShowDialog();
                            }
                        }
                    }
                }
            }
        }
    }
}
