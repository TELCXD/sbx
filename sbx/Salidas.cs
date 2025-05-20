using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.SalidaInventario;
using sbx.core.Interfaces.TipoSalida;
using System.Globalization;

namespace sbx
{
    public partial class Salidas : Form
    {
        private dynamic? _Permisos;
        private readonly ITipoSalida _ITipoSalida;
        private readonly IServiceProvider _serviceProvider;
        private Buscador? _Buscador;
        private readonly IProveedor _IProveedor;
        private readonly IProducto _IProducto;
        SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();
        private AgregaDetalleSalida? _AgregaDetalleSalida;
        private readonly ISalidaInventario _ISalidaInventario;

        public Salidas(ITipoSalida tipoSalida, IServiceProvider serviceProvider, IProveedor proveedor, IProducto producto, ISalidaInventario salidaInventario)
        {
            InitializeComponent();
            _ITipoSalida = tipoSalida;
            _serviceProvider = serviceProvider;
            _IProducto = producto;
            _IProveedor = proveedor;
            _ISalidaInventario = salidaInventario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Salidas_Load(object sender, EventArgs e)
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
                        case "salidas":
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
            var resp = await _ITipoSalida.ListTipoSalida();
            cbx_tipo_salida.DataSource = resp.Data;
            cbx_tipo_salida.ValueMember = "IdTipoSalida";
            cbx_tipo_salida.DisplayMember = "Nombre";
            cbx_tipo_salida.SelectedIndex = 0;
        }

        private void btn_busca_pv_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Salidas_busca_proveedor";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProveedor.List(id);
            if (resp.Data != null)
            {
                salidaInventarioEntitie.IdProveedor = resp.Data[0].IdProveedor;
                txt_documento_proveedor.Text = resp.Data[0].NumeroDocumento;
                lbl_nombre_proveedor.Text = resp.Data[0].NombreRazonSocial;
            }
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaDetalleSalida = _serviceProvider.GetRequiredService<AgregaDetalleSalida>();
                _AgregaDetalleSalida.Permisos = _Permisos;
                _AgregaDetalleSalida.Enviar_Detalle += _ObtenerDetalleSalida;
                _AgregaDetalleSalida.FormClosed += (s, args) => _AgregaDetalleSalida = null;
                _AgregaDetalleSalida.ShowDialog();
            }
        }

        private void _ObtenerDetalleSalida(DetalleSalidaInventarioEntitie detalleEntradasInv)
        {
            var nuevoDetalle = new DetalleSalidaInventarioEntitie
            {
                IdProducto = detalleEntradasInv.IdProducto,
                Sku = detalleEntradasInv.Sku,
                CodigoBarras = detalleEntradasInv.CodigoBarras,
                Nombre = detalleEntradasInv.Nombre,
                CodigoLote = detalleEntradasInv.CodigoLote,
                FechaVencimiento = detalleEntradasInv.FechaVencimiento,
                Cantidad = detalleEntradasInv.Cantidad,
                CostoUnitario = detalleEntradasInv.CostoUnitario,
                Total = detalleEntradasInv.Total
            };

            salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);
            dtg_detalle_salida.Rows.Clear();
            foreach (var item in salidaInventarioEntitie.detalleSalidaInventarios)
            {
                dtg_detalle_salida.Rows.Add(
                    item.IdProducto,
                    item.Sku,
                    item.CodigoBarras,
                    item.Nombre,
                    item.CodigoLote,
                    item.FechaVencimiento,
                    item.Cantidad.ToString().Replace('.', ','),
                    item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                    item.Total.ToString("N2", new CultureInfo("es-CO"))
                );
            }

            if (salidaInventarioEntitie.detalleSalidaInventarios.Count > 0)
            {
                decimal Total = salidaInventarioEntitie.detalleSalidaInventarios.Sum(d => d.Total);
                txt_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
            }
            else
            {
                txt_total.Text = "";
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_salida.Rows.Count > 0)
            {
                salidaInventarioEntitie.IdTipoSalida = Convert.ToInt32(cbx_tipo_salida.SelectedValue);
                salidaInventarioEntitie.OrdenCompra = txt_orden_compra.Text;
                salidaInventarioEntitie.NumFactura = txt_num_factura.Text;
                salidaInventarioEntitie.Comentario = txt_comentario.Text;
                var resp = await _ISalidaInventario.CreateUpdate(salidaInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
