using sbx.core.Interfaces.FechaVencimiento;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;

namespace sbx
{
    public partial class Stock : Form
    {
        private readonly IFechaVencimiento _IFechaVencimiento;
        private readonly IParametros _IParametros;
        private dynamic? _Permisos;
        private readonly IProducto _IProducto;

        public Stock(IFechaVencimiento fechaVencimiento, IParametros parametros, IProducto producto)
        {
            InitializeComponent();
            _IFechaVencimiento = fechaVencimiento;
            _IParametros = parametros;
            _IProducto = producto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Stock_Load(object sender, EventArgs e)
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

            var resp = await _IFechaVencimiento.BuscarStock(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

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
                                item.FechaVencimiento,
                                item.Stock);
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
                                var respFn = await _IFechaVencimiento.BuscarStock(Idprd.ToString(), "Id", "Igual a");

                                if (respFn.Data != null)
                                {
                                    if (respFn.Data.Count > 0)
                                    {
                                        foreach (var item in respFn.Data)
                                        {
                                            dtg_producto.Rows.Add(
                                                    item.IdProducto,
                                                    item.Sku,
                                                    item.CodigoBarras,
                                                    item.Nombre,
                                                    item.FechaVencimiento,
                                                    item.Stock);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            await ConsultaProductos();
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
