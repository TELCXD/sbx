using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.TipoEntrada;
using System.Globalization;

namespace sbx
{
    public partial class Entradas : Form
    {
        private dynamic? _Permisos;
        private readonly ITipoEntrada _ITipoEntrada;
        private AgregaDetalleEntrada? _AgregaDetalleEntrada;
        private readonly IServiceProvider _serviceProvider;
        private Buscador? _Buscador;
        private readonly IProveedor _IProveedor;
        EntradasInventarioEntitie entradasInventarioEntitie = new EntradasInventarioEntitie();
        private readonly IProducto _IProducto;
        private readonly IEntradaInventario _IEntradaInventario;

        public Entradas(ITipoEntrada tipoEntrada, IServiceProvider serviceProvider, IProveedor proveedor, IProducto iProducto, IEntradaInventario entradaInventario)
        {
            InitializeComponent();
            _ITipoEntrada = tipoEntrada;
            _serviceProvider = serviceProvider;
            _IProveedor = proveedor;
            _IProducto = iProducto;
            _IEntradaInventario = entradaInventario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Entradas_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "entradas":
                            btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_add_producto.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_busca_pv.Enabled = item.ToRead == 1 ? true : false;
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

        private async Task CargaDatosIniciales()
        {
            var resp = await _ITipoEntrada.ListTipoEntrada();
            cbx_tipo_entrada.DataSource = resp.Data;
            cbx_tipo_entrada.ValueMember = "IdTipoEntrada";
            cbx_tipo_entrada.DisplayMember = "Nombre";
            cbx_tipo_entrada.SelectedIndex = 0;
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaDetalleEntrada = _serviceProvider.GetRequiredService<AgregaDetalleEntrada>();
                _AgregaDetalleEntrada.Permisos = _Permisos;
                _AgregaDetalleEntrada.Enviar_Detalle += _ObtenerDetalleEntrada;
                _AgregaDetalleEntrada.FormClosed += (s, args) => _AgregaDetalleEntrada = null;
                _AgregaDetalleEntrada.ShowDialog();
            }
        }

        private void btn_busca_pv_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Entradas_busca_proveedor";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProveedor.List(id);
            if (resp.Data != null)
            {
                entradasInventarioEntitie.IdProveedor = resp.Data[0].IdProveedor;
                txt_documento_proveedor.Text = resp.Data[0].NumeroDocumento;
                lbl_nombre_proveedor.Text = resp.Data[0].NombreRazonSocial;
            }
        }

        private void _ObtenerDetalleEntrada(DetalleEntradasInventarioEntitie detalleEntradasInv)
        {
            var nuevoDetalle = new DetalleEntradasInventarioEntitie
            {
                IdProducto = detalleEntradasInv.IdProducto,
                Sku = detalleEntradasInv.Sku,
                CodigoBarras = detalleEntradasInv.CodigoBarras,
                Nombre = detalleEntradasInv.Nombre,
                CodigoLote = detalleEntradasInv.CodigoLote,
                FechaVencimiento = detalleEntradasInv.FechaVencimiento,
                Cantidad = detalleEntradasInv.Cantidad,
                CostoUnitario = detalleEntradasInv.CostoUnitario,
                Descuento = detalleEntradasInv.Descuento,
                Iva = detalleEntradasInv.Iva,
                Total = detalleEntradasInv.Total
            };

            entradasInventarioEntitie.detalleEntradasInventarios.Add(nuevoDetalle);
            dtg_detalle_entrada.Rows.Clear();
            foreach (var item in entradasInventarioEntitie.detalleEntradasInventarios)
            {
                dtg_detalle_entrada.Rows.Add(
                    item.IdProducto,
                    item.Sku,
                    item.CodigoBarras,
                    item.Nombre,
                    item.CodigoLote,
                    item.FechaVencimiento,
                    item.Cantidad.ToString().Replace('.', ','),
                    item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                    item.Descuento.ToString().Replace('.', ','),
                    item.Iva.ToString().Replace('.', ','),
                    item.Total.ToString("N2", new CultureInfo("es-CO"))
                    );
            }

            if (entradasInventarioEntitie.detalleEntradasInventarios.Count > 0) 
            {
                decimal Total = entradasInventarioEntitie.detalleEntradasInventarios.Sum(d => d.Total);
                txt_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
            }
            else
            {
                txt_total.Text = "";
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_entrada.Rows.Count > 0)
            {
                entradasInventarioEntitie.IdTipoEntrada = Convert.ToInt32(cbx_tipo_entrada.SelectedValue);
                entradasInventarioEntitie.OrdenCompra = txt_orden_compra.Text;
                entradasInventarioEntitie.NumFactura = txt_num_factura.Text;
                entradasInventarioEntitie.Comentario = txt_comentario.Text;

                var resp = await _IEntradaInventario.CreateUpdate(entradasInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para guardar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
