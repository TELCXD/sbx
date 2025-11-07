using sbx.core.Interfaces.FechaVencimiento;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;

namespace sbx
{
    public partial class FechasVencimiento : Form
    {
        private readonly IFechaVencimiento _IFechaVencimiento;
        private readonly IParametros _IParametros;
        private dynamic? _Permisos;
        private readonly IProducto _IProducto;

        public FechasVencimiento(IFechaVencimiento fechaVencimiento, IParametros parametros, IProducto iProducto)
        {
            InitializeComponent();
            _IFechaVencimiento = fechaVencimiento;
            _IParametros = parametros;
            _IProducto = iProducto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void FechasVencimiento_Load(object sender, EventArgs e)
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
                            btn_buscar.Enabled = item.ToRead == 1 ? true : false;
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
            await ConsultaProductos();
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

            var resp = await _IFechaVencimiento.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_producto.Rows.Clear();

            string Estado = "";
            DateTime hoy = DateTime.Today;

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        if (Convert.ToDecimal(item.Stock) > 0) 
                        {
                            int diasDiferencia = (Convert.ToDateTime(item.FechaVencimiento) - hoy).Days;

                            if (diasDiferencia < 0)
                            {
                                Estado = $"Vencido hace {diasDiferencia} dias";
                            }
                            else if (diasDiferencia == 0)
                            {
                                Estado = "¡Vence hoy!";
                            }
                            else if (diasDiferencia <= 7)
                            {
                                Estado = $"Próximo a vencer en {diasDiferencia} días";
                            }
                            else
                            {
                                Estado = $"Faltan {diasDiferencia} días para vencerse.";
                            }

                            int rowIndex = dtg_producto.Rows.Add(
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.Nombre,
                                item.FechaVencimiento,
                                item.Stock,
                                Estado);

                            DataGridViewRow fila = dtg_producto.Rows[rowIndex];

                            if (diasDiferencia < 0)
                            {
                                fila.Cells["cl_estado"].Style.BackColor = Color.Red;
                            }
                            else if (diasDiferencia == 0)
                            {
                                fila.Cells["cl_estado"].Style.BackColor = Color.Orange;
                            }
                            else if (diasDiferencia <= 7)
                            {
                                fila.Cells["cl_estado"].Style.BackColor = Color.Yellow;
                            }
                            else
                            {
                                fila.Cells["cl_estado"].Style.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
                else
                {
                    if (cbx_campo_filtro.Text == "Codigo barras")
                    {
                        var respVerificaCB = await _IProducto.ListCodigoBarras2(txt_buscar.Text);

                        if (respVerificaCB.Data != null)
                        {
                            if (respVerificaCB.Data.Count > 0)
                            {
                                int Idprd = respVerificaCB.Data[0].IdProducto;
                                var respFn = await _IFechaVencimiento.Buscar(Idprd.ToString(), "Id", "Igual a");

                                if (respFn.Data != null)
                                {
                                    if (respFn.Data.Count > 0)
                                    {
                                        foreach (var item in respFn.Data)
                                        {
                                            if (Convert.ToDecimal(item.Stock) > 0)
                                            {
                                                int diasDiferencia = (Convert.ToDateTime(item.FechaVencimiento) - hoy).Days;

                                                if (diasDiferencia < 0)
                                                {
                                                    Estado = $"Vencido hace {diasDiferencia} dias";
                                                }
                                                else if (diasDiferencia == 0)
                                                {
                                                    Estado = "¡Vence hoy!";
                                                }
                                                else if (diasDiferencia <= 7)
                                                {
                                                    Estado = $"Próximo a vencer en {diasDiferencia} días";
                                                }
                                                else
                                                {
                                                    Estado = $"Faltan {diasDiferencia} días para vencerse.";
                                                }

                                                int rowIndex = dtg_producto.Rows.Add(
                                                    item.IdProducto,
                                                    item.Sku,
                                                    item.CodigoBarras,
                                                    item.Nombre,
                                                    item.FechaVencimiento,
                                                    item.Stock,
                                                    Estado);

                                                DataGridViewRow fila = dtg_producto.Rows[rowIndex];

                                                if (diasDiferencia < 0)
                                                {
                                                    fila.Cells["cl_estado"].Style.BackColor = Color.Red;
                                                }
                                                else if (diasDiferencia == 0)
                                                {
                                                    fila.Cells["cl_estado"].Style.BackColor = Color.Orange;
                                                }
                                                else if (diasDiferencia <= 7)
                                                {
                                                    fila.Cells["cl_estado"].Style.BackColor = Color.Yellow;
                                                }
                                                else
                                                {
                                                    fila.Cells["cl_estado"].Style.BackColor = Color.LightGreen;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaProductos();
            }
        }
    }
}
