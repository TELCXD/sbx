using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class AgregaDetalleSalida : Form
    {
        private dynamic? _Permisos;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        DetalleSalidaInventarioEntitie DetalleSalida = new DetalleSalidaInventarioEntitie();
        char decimalSeparator = ',';
        public delegate void EnviarDetalle(DetalleSalidaInventarioEntitie AdddetalleSalidasInv);
        public event EnviarDetalle Enviar_Detalle;
        private readonly IEntradaInventario _IEntradaInventario;

        public AgregaDetalleSalida(IServiceProvider serviceProvider, IProducto producto, IEntradaInventario entradaInventario)
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

        private void AgregaDetalleSalida_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
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
                            btn_add_producto.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_busca_pr.Enabled = item.ToRead == 1 ? true : false;
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

        private void btn_busca_pr_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Salidas_busca_producto";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProducto.List(id);
            if (resp.Data != null)
            {

                DetalleSalida.IdProducto = resp.Data[0].IdProducto;
                DetalleSalida.Sku = resp.Data[0].Sku;
                DetalleSalida.CodigoBarras = resp.Data[0].CodigoBarras;
                DetalleSalida.Nombre = resp.Data[0].Nombre;

                txt_producto.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                lbl_nombre_producto.Text = resp.Data[0].Nombre;
            }
        }

        private async void btn_add_producto_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_producto.Text.Trim() != "" && txt_costo.Text.Trim() != "" && txt_cantidad.Text.Trim() != "" && txt_total.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.')) > 0) 
                {
                    var resp = await _IEntradaInventario.Entradas(DetalleSalida.IdProducto);

                    if (resp.Data != null)
                    {
                        if (Convert.ToDecimal(resp.Data[0].Entradas) > 0) 
                        {
                            DetalleSalida.CodigoLote = txt_lote.Text;
                            if (chek_fecha_vencimiento.Checked) { DetalleSalida.FechaVencimiento = dtp_fecha_vencimiento.Value; } else { DetalleSalida.FechaVencimiento = null; }
                            DetalleSalida.Cantidad = Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.'));
                            DetalleSalida.CostoUnitario = Convert.ToDecimal(txt_costo.Text, new CultureInfo("es-CO"));
                            DetalleSalida.Total = Convert.ToDecimal(txt_total.Text, new CultureInfo("es-CO"));

                            Enviar_Detalle(DetalleSalida);

                            txt_producto.Text = "";
                            lbl_nombre_producto.Text = "";
                            txt_lote.Text = "";
                            chek_fecha_vencimiento.Checked = false;
                            txt_cantidad.Text = "";
                            txt_costo.Text = "";
                            txt_total.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Para poder registrar salidas del producto, deben existir previamente entradas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    errorProvider1.SetError(txt_cantidad, "Cantidad debe ser mayor a cero");
                }
            }
            else
            {
                if (txt_producto.Text.Trim() == "") { errorProvider1.SetError(txt_producto, "Debe ingresar un producto"); }
                if (txt_total.Text.Trim() == "") { errorProvider1.SetError(txt_total, "Debe ingresar cantidad y costo para calcular total"); }
            }
        }

        private void txt_cantidad_KeyUp(object sender, KeyEventArgs e)
        {
            calculartotal();
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculartotal();
        }

        private void txt_cantidad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_cantidad.Text, out _))
            {
                errorProvider1.SetError(txt_cantidad, "Ingrese un valor numerico");
            }
        }

        private void txt_costo_KeyUp(object sender, KeyEventArgs e)
        {
            calculartotal();
        }

        private void txt_costo_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculartotal();
        }

        private void calculartotal()
        {
            errorProvider1.Clear();
            txt_total.Text = "";
            if (txt_cantidad.Text.Trim() != "" && txt_costo.Text.Trim() != "")
            {
                decimal Cantidad = Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.'));
                decimal Costo = Convert.ToDecimal(txt_costo.Text, new CultureInfo("es-CO"));
                decimal SubTotal = Costo * Cantidad;

                txt_total.Text = SubTotal.ToString("N2", new CultureInfo("es-CO"));
            }
            else
            {
                if (txt_cantidad.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_cantidad, "Ingresa cantidad");
                }

                if (txt_costo.Text.Trim() == "")
                {
                    errorProvider1.SetError(txt_costo, "Ingresa costo");
                }
            }
        }

        private void txt_costo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_costo.Text, out _))
            {
                errorProvider1.SetError(txt_costo, "Ingrese un valor numerico");
            }
        }
    }
}
