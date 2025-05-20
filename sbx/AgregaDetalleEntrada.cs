using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class AgregaDetalleEntrada : Form
    {
        private dynamic? _Permisos;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        DetalleEntradasInventarioEntitie DetalleEntrada = new DetalleEntradasInventarioEntitie();
        char decimalSeparator = ',';
        public delegate void EnviarDetalle(DetalleEntradasInventarioEntitie AdddetalleEntradasInv);
        public event EnviarDetalle Enviar_Detalle;

        public AgregaDetalleEntrada(IServiceProvider serviceProvider, IProducto producto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = producto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void AgregaDetalleEntrada_Load(object sender, EventArgs e)
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
                        case "entradas":
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
            _Buscador.Origen = "Entradas_busca_producto";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProducto.List(id);
            if (resp.Data != null)
            {

                DetalleEntrada.IdProducto = resp.Data[0].IdProducto;
                DetalleEntrada.Sku = resp.Data[0].Sku;
                DetalleEntrada.CodigoBarras = resp.Data[0].CodigoBarras;
                DetalleEntrada.Nombre = resp.Data[0].Nombre;

                txt_producto.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                lbl_nombre_producto.Text = resp.Data[0].Nombre;
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_cantidad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_cantidad.Text, out _))
            {
                errorProvider1.SetError(txt_cantidad, "Ingrese un valor numerico");
            }
        }

        private void txt_costo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_costo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_costo.Text, out _))
            {
                errorProvider1.SetError(txt_costo, "Ingrese un valor numerico");
            }
        }

        private void txt_descuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_descuento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_descuento.Text, out _))
            {
                errorProvider1.SetError(txt_descuento, "Ingrese un valor numerico");
            }
        }

        private void txt_iva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_iva_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_iva.Text, out _))
            {
                errorProvider1.SetError(txt_iva, "Ingrese un valor numerico");
            }
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_producto.Text.Trim() != "" && txt_cantidad.Text.Trim() != "" && txt_costo.Text.Trim() != "" && txt_subtotal.Text.Trim() != "" && txt_total.Text.Trim() != "" && txt_descuento.Text != "" && txt_iva.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.')) > 0)
                {
                    DetalleEntrada.CodigoLote = txt_lote.Text;
                    if (chek_fecha_vencimiento.Checked) { DetalleEntrada.FechaVencimiento = dtp_fecha_vencimiento.Value; } else { DetalleEntrada.FechaVencimiento = null; }
                    DetalleEntrada.Cantidad = Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.'));
                    DetalleEntrada.CostoUnitario = Convert.ToDecimal(txt_costo.Text, new CultureInfo("es-CO"));
                    DetalleEntrada.Descuento = Convert.ToDecimal(txt_descuento.Text.Replace(',', '.'));
                    DetalleEntrada.Iva = Convert.ToDecimal(txt_iva.Text.Replace(',', '.'));
                    DetalleEntrada.Total = Convert.ToDecimal(txt_total.Text, new CultureInfo("es-CO"));

                    Enviar_Detalle(DetalleEntrada);

                    txt_producto.Text = "";
                    lbl_nombre_producto.Text = "";
                    txt_lote.Text = "";
                    chek_fecha_vencimiento.Checked = false;
                    txt_cantidad.Text = "";
                    txt_costo.Text = "";
                    txt_subtotal.Text = "";
                    txt_descuento.Text = "";
                    txt_iva.Text = "";
                    txt_total.Text = "";
                }
                else
                {
                    errorProvider1.SetError(txt_cantidad, "Cantidad debe ser mayor a cero");
                }
            }
            else
            {
                if (txt_producto.Text.Trim() == "") { errorProvider1.SetError(txt_producto, "Debe ingresar un producto"); }
                if (txt_subtotal.Text.Trim() == "") { errorProvider1.SetError(txt_subtotal, "Debe ingresar cantidad y costo para calcular sub total"); }
                if (txt_total.Text.Trim() == "" || txt_descuento.Text.Trim() == "" || txt_iva.Text.Trim() == "") { errorProvider1.SetError(txt_total, "Debe existir sub total, descuento y iva para calcular total"); }
            }
        }

        private void calcularSubtotal()
        {
            errorProvider1.Clear();
            txt_subtotal.Text = "";
            if (txt_cantidad.Text.Trim() != "" && txt_costo.Text.Trim() != "")
            {
                decimal Cantidad = Convert.ToDecimal(txt_cantidad.Text.Replace(',', '.'));
                decimal Costo = Convert.ToDecimal(txt_costo.Text, new CultureInfo("es-CO"));
                decimal SubTotal = Costo * Cantidad;

                txt_subtotal.Text = SubTotal.ToString("N2", new CultureInfo("es-CO"));
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

            calcularTotal();
        }

        private void txt_cantidad_KeyUp(object sender, KeyEventArgs e)
        {
            calcularSubtotal();
        }

        private void txt_costo_KeyUp(object sender, KeyEventArgs e)
        {
            calcularSubtotal();
        }

        private void calcularTotal()
        {
            txt_total.Text = "";
            decimal ValorDescuento = 0;
            if (txt_descuento.Text.Trim() != "" && txt_subtotal.Text.Trim() != "")
            {
                decimal Subtotal = Convert.ToDecimal(txt_subtotal.Text, new CultureInfo("es-CO"));
                decimal DescuentoPorcentaje = Convert.ToDecimal(txt_descuento.Text, new CultureInfo("es-CO"));
                ValorDescuento = Subtotal * (DescuentoPorcentaje / 100);
                decimal Total = Subtotal - ValorDescuento;
                txt_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
            }
            if (txt_iva.Text.Trim() != "" && txt_subtotal.Text.Trim() != "")
            {
                decimal Subtotal = Convert.ToDecimal(txt_subtotal.Text, new CultureInfo("es-CO")) - ValorDescuento;
                decimal IvaPorcentaje = Convert.ToDecimal(txt_iva.Text, new CultureInfo("es-CO"));
                decimal ValorIva = Subtotal * (IvaPorcentaje / 100);
                decimal Total = Subtotal + ValorIva;
                txt_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
            }
        }

        private void txt_descuento_KeyUp(object sender, KeyEventArgs e)
        {
            calcularTotal();
        }

        private void txt_iva_KeyUp(object sender, KeyEventArgs e)
        {
            calcularTotal();
        }

        private void txt_costo_Leave(object sender, EventArgs e)
        {
            
        }

        private void chek_fecha_vencimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (chek_fecha_vencimiento.Checked) 
            {
                lbl_fechaVencimiento.Visible = true;
                dtp_fecha_vencimiento.Visible = true;
            }
            else
            {
                lbl_fechaVencimiento.Visible = false;
                dtp_fecha_vencimiento.Visible = false;
            }
        }
    }
}
