using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.AgregaVenta;
using sbx.core.Interfaces.Banco;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.MedioPago;
using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.PrecioProducto;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.PromocionProducto;
using sbx.core.Interfaces.Vendedor;
using System.Globalization;

namespace sbx
{
    public partial class AgregarVentas : Form
    {
        private readonly IListaPrecios _IListaPrecios;
        private dynamic? _Permisos;
        private readonly IVendedor _IVendedor;
        private readonly IMedioPago _IMedioPago;
        private readonly IBanco _IBanco;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private readonly ICliente _ICliente;
        private readonly IPrecioCliente _IPrecioCliente;
        private readonly IPrecioProducto _IPrecioProducto;
        private readonly IPromocionProducto _IPromocionProducto;
        string busqueda = "";
        AgregaVentaEntitie agregaVentaEntitie = new AgregaVentaEntitie();
        char decimalSeparator = ',';
        private AgregarProducto? _AgregarProducto;
        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;

        public AgregarVentas(IListaPrecios listaPrecios, IVendedor vendedor, IMedioPago medioPago,
            IBanco banco, IServiceProvider serviceProvider, IProducto iProducto, ICliente cliente, IPrecioCliente precioCliente, IPrecioProducto precioProducto, IPromocionProducto promocionProducto)
        {
            InitializeComponent();
            _IListaPrecios = listaPrecios;
            _IVendedor = vendedor;
            _IMedioPago = medioPago;
            _IBanco = banco;
            _serviceProvider = serviceProvider;
            _IProducto = iProducto;
            _ICliente = cliente;
            _IPrecioCliente = precioCliente;
            _IPrecioProducto = precioProducto;
            _IPromocionProducto = promocionProducto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void AgregarVentas_Load(object sender, EventArgs e)
        {
            await CargaDatosIniciales();
            ValidaPermisos();
        }

        private async Task CargaDatosIniciales()
        {
            var resp = await _IListaPrecios.List(0);
            cbx_lista_precio.DataSource = resp.Data;
            cbx_lista_precio.ValueMember = "IdListaPrecio";
            cbx_lista_precio.DisplayMember = "NombreLista";
            cbx_lista_precio.SelectedIndex = 0;

            resp = await _IVendedor.List(0);
            cbx_vendedor.DataSource = resp.Data;
            cbx_vendedor.ValueMember = "IdVendedor";
            cbx_vendedor.DisplayMember = "NombreCompleto";
            cbx_vendedor.SelectedIndex = 0;

            resp = await _IVendedor.List(0);
            cbx_vendedor.DataSource = resp.Data;
            cbx_vendedor.ValueMember = "IdVendedor";
            cbx_vendedor.DisplayMember = "NombreCompleto";
            cbx_vendedor.SelectedIndex = 0;

            resp = await _IMedioPago.List();
            cbx_medio_pago.DataSource = resp.Data;
            cbx_medio_pago.ValueMember = "IdMetodoPago";
            cbx_medio_pago.DisplayMember = "Nombre";
            cbx_medio_pago.SelectedIndex = 0;

            resp = await _IBanco.List();
            cbx_banco.DataSource = resp.Data;
            cbx_banco.ValueMember = "IdBanco";
            cbx_banco.DisplayMember = "Nombre";
            cbx_banco.SelectedIndex = 0;

            cbx_busca_por.SelectedIndex = 0;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "ventas":
                            txt_buscar_producto.Enabled = item.ToRead == 1 ? true : false;
                            btn_busca_producto.Enabled = item.ToRead == 1 ? true : false;
                            btn_nuevo_producto.Enabled = item.ToCreate == 1 ? true : false;
                            btn_ventas_suspendidas.Enabled = item.ToRead == 1 ? true : false;
                            btn_suspender.Enabled = item.ToCreate == 1 ? true : false;
                            btn_cancelar.Enabled = item.ToUpdate == 1 ? true : false;
                            txt_busca_cliente.Enabled = item.ToRead == 1 ? true : false;
                            btn_busca_cliente.Enabled = item.ToRead == 1 ? true : false;
                            btn_nuevo_cliente.Enabled = item.ToCreate == 1 ? true : false;
                            txt_valor_pago.Enabled = item.ToCreate == 1 ? true : false;
                            btn_completar_venta.Enabled = item.ToCreate == 1 ? true : false;
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

        private void cbx_medio_pago_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbx_medio_pago.Text == "Transferencia" || cbx_medio_pago.Text == "Tarjeta Crédito" || cbx_medio_pago.Text == "Tarjeta Débito")
            {
                txt_referencia_pago.Enabled = true;
                cbx_banco.Enabled = true;
            }

            if (cbx_medio_pago.Text == "Nequi" || cbx_medio_pago.Text == "DaviPlata" || cbx_medio_pago.Text == "Bancolombia QR")
            {
                txt_referencia_pago.Enabled = true;
                cbx_banco.Enabled = false;
            }

            if (cbx_medio_pago.Text == "Efectivo")
            {
                txt_referencia_pago.Enabled = false;
                txt_referencia_pago.Text = "";
                cbx_banco.Enabled = false;

            }

            lbl_metodo_pago.Text = cbx_medio_pago.Text;
        }

        private void btn_busca_producto_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Add_AgregaVenta_busca_producto";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            busqueda = "Add_AgregaVenta_busca_producto";
            _Buscador.ShowDialog();
        }

        private async void txt_buscar_producto_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
            if (cbx_busca_por.Text == "Id")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
                {
                    e.Handled = true;
                }
                else
                {
                    //Enter
                    if (e.KeyChar == (char)13)
                    {
                        if (txt_buscar_producto.Text.Trim() != "")
                        {
                            var DataProducto = await _IProducto.List(Convert.ToInt32(txt_buscar_producto.Text));
                        }
                        else
                        {
                            errorProvider1.SetError(txt_buscar_producto, $"Debe ingresar un {cbx_busca_por.Text}");
                        }
                    }
                }
            }
            else if (cbx_busca_por.Text == "Sku" || cbx_busca_por.Text == "Codigo barras")
            {
                //Enter
                if (e.KeyChar == (char)13)
                {
                    if (txt_buscar_producto.Text.Trim() != "")
                    {
                        if (cbx_busca_por.Text == "Sku")
                        {
                            var DataProducto = await _IProducto.ListSku(txt_buscar_producto.Text);
                        }
                        else
                        {
                            var DataProducto = await _IProducto.ListCodigoBarras(txt_buscar_producto.Text);
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(txt_buscar_producto, $"Debe ingresar un {cbx_busca_por.Text}");
                    }

                }
            }
        }

        private void btn_busca_cliente_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Add_AgregaVenta_busca_cliente";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            busqueda = "Add_AgregaVenta_busca_cliente";
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            if (busqueda == "Add_AgregaVenta_busca_cliente")
            {
                var resp = await _ICliente.List(id);
                if (resp.Data != null)
                {
                    agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                    txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                }
            }
            else if (busqueda == "Add_AgregaVenta_busca_producto")
            {
                bool Continuar = true;

                var DataProducto = await _IProducto.List(id);
                if (DataProducto.Data != null)
                {
                    Cantidad = 0;
                    Subtotal = 0;
                    Descuento = 0;
                    Impuesto = 0;
                    SubtotalLinea = 0;
                    DescuentoLinea = 0;

                    //1. ¿Tiene precio personalizado? => usar PreciosCliente
                    if (agregaVentaEntitie.IdCliente > 0)
                    {
                        var resp1 = await _IPrecioCliente.PrecioClientePersonalizado(Convert.ToInt32(DataProducto.Data[0].IdProducto), agregaVentaEntitie.IdCliente);
                        if (resp1.Data != null)
                        {
                            decimal Total = CalcularTotal(resp1.Data[0].PrecioEspecial, DataProducto.Data[0].Iva, 0);

                            dtg_producto.Rows.Add(
                                 DataProducto.Data[0].IdProducto,
                                 DataProducto.Data[0].Sku,
                                 DataProducto.Data[0].CodigoBarras,
                                 DataProducto.Data[0].Nombre,
                                 resp1.Data[0].PrecioEspecial.ToString("N2", new CultureInfo("es-CO")),
                                 1,
                                 0,
                                 DataProducto.Data[0].Iva,
                                 Total.ToString("N2", new CultureInfo("es-CO"))
                                );
                            Continuar = false;
                        }
                    }
                    //2. ¿Pertenece a una lista de precios por tipo de cliente?
                    agregaVentaEntitie.IdListaPrecio = Convert.ToInt32(cbx_lista_precio.SelectedValue);
                    if (agregaVentaEntitie.IdListaPrecio > 1 && Continuar == true)
                    {
                        var resp2 = await _IPrecioProducto.PrecioListaPreciosTipoCliente(Convert.ToInt32(DataProducto.Data[0].IdProducto), agregaVentaEntitie.IdListaPrecio);
                        if (resp2.Data != null)
                        {
                            decimal Total = CalcularTotal(resp2.Data[0].Precio, DataProducto.Data[0].Iva, 0);

                            dtg_producto.Rows.Add(
                                 DataProducto.Data[0].IdProducto,
                                 DataProducto.Data[0].Sku,
                                 DataProducto.Data[0].CodigoBarras,
                                 DataProducto.Data[0].Nombre,
                                 resp2.Data[0].Precio.ToString("N2", new CultureInfo("es-CO")),
                                 1,
                                 0,
                                 DataProducto.Data[0].Iva,
                                 Total.ToString("N2", new CultureInfo("es-CO"))
                                );
                            Continuar = false;
                        }
                    }
                    //3. ¿Hay promociones activas? => aplicar sobre el precio base
                    if (Continuar == true)
                    {
                        var resp3 = await _IPromocionProducto.PromocionActiva(Convert.ToInt32(DataProducto.Data[0].IdProducto));
                        if (resp3.Data != null)
                        {
                            decimal Total = CalcularTotal(DataProducto.Data[0].PrecioBase, DataProducto.Data[0].Iva, resp3.Data[0].Porcentaje);

                            dtg_producto.Rows.Add(
                                 DataProducto.Data[0].IdProducto,
                                 DataProducto.Data[0].Sku,
                                 DataProducto.Data[0].CodigoBarras,
                                 DataProducto.Data[0].Nombre,
                                 DataProducto.Data[0].PrecioBase.ToString("N2", new CultureInfo("es-CO")),
                                 1,
                                 resp3.Data[0].Porcentaje,
                                 DataProducto.Data[0].Iva,
                                 Total.ToString("N2", new CultureInfo("es-CO"))
                                );
                            Continuar = false;
                        }
                    }
                    //4. Si nada aplica => usar PrecioBase por defecto
                    if (Continuar == true)
                    {
                        decimal Total = CalcularTotal(DataProducto.Data[0].PrecioBase, DataProducto.Data[0].Iva, 0);

                        dtg_producto.Rows.Add(
                             DataProducto.Data[0].IdProducto,
                             DataProducto.Data[0].Sku,
                             DataProducto.Data[0].CodigoBarras,
                             DataProducto.Data[0].Nombre,
                             DataProducto.Data[0].PrecioBase.ToString("N2", new CultureInfo("es-CO")),
                             1,
                             0,
                             DataProducto.Data[0].Iva,
                             Total.ToString("N2", new CultureInfo("es-CO"))
                            );
                    }

                    if (dtg_producto.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in dtg_producto.Rows)
                        {
                            Cantidad += Convert.ToDecimal(fila.Cells["cl_cantidad"].Value);
                            Subtotal += Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value);
                            SubtotalLinea = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        }
                        Total = (Subtotal - Descuento) + Impuesto;

                        lbl_cantidadProductos.Text = Cantidad.ToString();
                        lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    }
                }
            }
        }

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            return ValorDescuento;
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private void txt_valor_pago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void btn_nuevo_producto_Click(object sender, EventArgs e)
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

        private void txt_valor_pago_KeyUp(object sender, KeyEventArgs e)
        {
            decimal Cambio = 0;
            lbl_cambio.Text = "_";
            lbl_cambio.ForeColor = Color.Black;
            if (txt_valor_pago.Text.Trim() != "" && lbl_total.Text != "_") 
            {
                Cambio = Convert.ToDecimal(txt_valor_pago.Text.Trim()) - Convert.ToDecimal(lbl_total.Text.Trim(), new CultureInfo("es-CO"));
                if (Cambio < 0) 
                {
                    lbl_cambio.ForeColor = Color.Red;
                }
                else if(Cambio > 0)
                {
                    lbl_cambio.ForeColor = Color.SeaGreen;
                }
                else
                {
                    lbl_cambio.ForeColor = Color.Black;
                }

                lbl_cambio.Text = Cambio.ToString("N2", new CultureInfo("es-CO"));
            }
        }
    }
}
