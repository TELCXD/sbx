using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.EntradaInventario;

namespace sbx
{
    public partial class Inventario : Form
    {
        private dynamic? _Permisos;
        private Entradas? _Entradas;
        private Salidas? _Salidas;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEntradaInventario _IEntradaInventario;

        public Inventario(IServiceProvider serviceProvider, IEntradaInventario entradaInventario)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IEntradaInventario = entradaInventario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
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
            if (cbx_campo_filtro.Text == "Id")
            {
                bool esNumerico = int.TryParse(txt_buscar.Text, out int valor);
                if (!esNumerico)
                {
                    errorProvider1.SetError(txt_buscar, "Ingrese un valor numerico");
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
                        else if(item.TipoMovimiento == "Salida") 
                        { TotalStock -= item.Cantidad; }
                        else if (item.TipoMovimiento == "Salida por Venta")
                        { TotalStock -= item.Cantidad; }
                        else if (item.TipoMovimiento == "Entrada por Nota credito")
                        { TotalStock += item.Cantidad; }

                        dtg_inventario.Rows.Add(
                            item.Fecha,
                            item.Usuario,
                            item.Documento,
                            item.TipoMovimiento,
                            item.Cantidad,
                            item.Tipo,
                            item.IdProducto,
                            item.Nombre,
                            item.Sku,
                            item.CodigoBarras,
                            item.CodigoLote,
                            item.FechaVencimiento);
                    }

                    txt_total_stock.Text = TotalStock.ToString();
                }
            }
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
    }
}
