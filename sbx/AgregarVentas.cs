using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.AgregaVenta;
using sbx.core.Entities.PagosVenta;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.Banco;
using sbx.core.Interfaces.Caja;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.MedioPago;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.PrecioProducto;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.PromocionProducto;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.Vendedor;
using sbx.core.Interfaces.Venta;
using System.Globalization;
using System.Text;

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
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly IVenta _IVenta;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        string busqueda = "";
        AgregaVentaEntitie agregaVentaEntitie = new AgregaVentaEntitie();
        VentaEntitie venta = new VentaEntitie();
        char decimalSeparator = ',';
        private AgregarProducto? _AgregarProducto;
        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;
        private AgregarCliente? _AgregarCliente;
        int IdTipoCliente = 0;
        bool CargaListaPrecio = false;
        private Ventas? _Ventas;
        private readonly ICaja _ICaja;
        private AddApertura _AddApertura;
        private bool CajaAperturada = false;
        private AgregarNotaCredito? _AgregarNotaCredito;
        private AddPagosEfectivo? _AddPagosEfectivo;

        public AgregarVentas(IListaPrecios listaPrecios, IVendedor vendedor, IMedioPago medioPago,
            IBanco banco, IServiceProvider serviceProvider, IProducto iProducto, ICliente cliente, IPrecioCliente precioCliente,
            IPrecioProducto precioProducto, IPromocionProducto promocionProducto, IRangoNumeracion iRangoNumeracion, IVenta venta,
            ITienda tienda, IParametros parametros, ICaja caja)
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
            _IRangoNumeracion = iRangoNumeracion;
            _IRangoNumeracion = iRangoNumeracion;
            _IVenta = venta;
            _ITienda = tienda;
            _IParametros = parametros;
            _ICaja = caja;
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

            CargaListaPrecio = true;

            resp = await _IVendedor.List(0);
            cbx_vendedor.DataSource = resp.Data;
            cbx_vendedor.ValueMember = "IdVendedor";
            cbx_vendedor.DisplayMember = "NombreCompleto";
            cbx_vendedor.SelectedIndex = 0;

            resp = await _ICliente.List(1);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                    txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                    IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);
                }
            }

            resp = await _IMedioPago.List(0);
            cbx_medio_pago.DataSource = resp.Data;
            cbx_medio_pago.ValueMember = "IdMetodoPago";
            cbx_medio_pago.DisplayMember = "Nombre";
            cbx_medio_pago.SelectedIndex = 0;

            resp = await _IBanco.List();
            cbx_banco.DataSource = resp.Data;
            cbx_banco.ValueMember = "IdBanco";
            cbx_banco.DisplayMember = "Nombre";
            cbx_banco.SelectedIndex = 0;

            pnl_pagos.Enabled = false;

            var DataParametros = await _IParametros.List("Buscar en venta por");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    string BuscarPor = DataParametros.Data[0].Value;
                    cbx_busca_por.Text = BuscarPor;
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
                        case "ventas":
                            txt_buscar_producto.Enabled = item.ToRead == 1 ? true : false;
                            btn_busca_producto.Enabled = item.ToRead == 1 ? true : false;
                            btn_nuevo_producto.Enabled = item.ToCreate == 1 ? true : false;
                            txt_busca_cliente.Enabled = item.ToRead == 1 ? true : false;
                            //btn_ventas_suspendidas.Enabled = item.ToRead == 1 ? true : false;
                            //btn_suspender.Enabled = item.ToCreate == 1 ? true : false;
                            btn_ver_ventas.Enabled = item.ToRead == 1 ? true : false;
                            btn_cancelar.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_busca_cliente.Enabled = item.ToRead == 1 ? true : false;
                            btn_nuevo_cliente.Enabled = item.ToCreate == 1 ? true : false;
                            txt_valor_pago.Enabled = item.ToCreate == 1 ? true : false;
                            btn_completar_venta.Enabled = item.ToCreate == 1 ? true : false;
                            btn_pagos_en_efectivo.Enabled = item.ToCreate == 1 ? true : false;
                            break;
                        case "notaCredito":
                            btn_devolucion.Enabled = item.ToCreate == 1 ? true : false;
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
            ValidarModoPagos();
        }

        private void ValidarModoPagos()
        {
            if (cbx_medio_pago.Text == "Transferencia" || cbx_medio_pago.Text == "Tarjeta Crédito" || cbx_medio_pago.Text == "Tarjeta Débito")
            {
                txt_referencia_pago.Enabled = true;
                cbx_banco.Enabled = true;
                if (cbx_banco.DataSource != null) { cbx_banco.SelectedIndex = 1; }
                txt_valor_pago.Text = lbl_total.Text;
                txt_valor_pago.Enabled = false;
                lbl_cambio.Text = "0";
            }

            if (cbx_medio_pago.Text == "Nequi" || cbx_medio_pago.Text == "DaviPlata" || cbx_medio_pago.Text == "Bancolombia QR")
            {
                txt_referencia_pago.Enabled = true;
                cbx_banco.Enabled = false;
                if (cbx_banco.DataSource != null) { cbx_banco.SelectedIndex = 0; }
                txt_valor_pago.Text = lbl_total.Text;
                txt_valor_pago.Enabled = false;
                lbl_cambio.Text = "0";
            }

            if (cbx_medio_pago.Text == "Efectivo")
            {
                txt_referencia_pago.Enabled = false;
                txt_referencia_pago.Text = "";
                cbx_banco.Enabled = false;
                if (cbx_banco.DataSource != null) { cbx_banco.SelectedIndex = 0; }
                txt_valor_pago.Text = "";
                txt_valor_pago.Enabled = true;
                lbl_cambio.Text = "_";
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

            bool continuar = true;

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
                        if (txt_busca_cliente.Text.Trim() != "")
                        {
                            var resp = await _ICliente.ListNumDoc(txt_busca_cliente.Text);

                            if (resp.Data != null)
                            {
                                if (resp.Data.Count == 0)
                                {
                                    errorProvider1.SetError(txt_busca_cliente, $"Numero documento {txt_busca_cliente.Text} no encontrado");
                                    lbl_nombre_cliente.Text = "_";
                                    continuar = false;
                                }
                            }
                        }
                        else
                        {
                            var resp = await _ICliente.List(1);
                            if (resp.Data != null)
                            {
                                if (resp.Data.Count > 0)
                                {
                                    agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                                    txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                                    IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);
                                }
                            }
                        }

                        if (continuar)
                        {
                            if (txt_buscar_producto.Text.Trim() != "")
                            {
                                CajaAperturada = false;

                                var DataProducto = await _IProducto.List(Convert.ToInt32(txt_buscar_producto.Text));

                                if (DataProducto.Data != null)
                                {
                                    if (DataProducto.Data.Count > 0)
                                    {
                                        var estadoCaja = await _ICaja.EstadoCaja(Convert.ToInt32(_Permisos?[0]?.IdUser));
                                        if (estadoCaja.Data != null)
                                        {
                                            if (estadoCaja.Data.Count > 0)
                                            {
                                                if (estadoCaja.Data[0].Estado == "CERRADA")
                                                {
                                                    if (_Permisos != null)
                                                    {
                                                        _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                                                        _AddApertura.Permisos = _Permisos;
                                                        _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                                                        _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                                                        _AddApertura.ShowDialog();
                                                    }
                                                }
                                                else if (estadoCaja.Data[0].Estado == "ABIERTA")
                                                {
                                                    CajaAperturada = true;
                                                }
                                            }
                                            else
                                            {
                                                if (_Permisos != null)
                                                {
                                                    _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                                                    _AddApertura.Permisos = _Permisos;
                                                    _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                                                    _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                                                    _AddApertura.ShowDialog();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show($"No se logro obtener informacion de estado de caja", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            CajaAperturada = false;
                                        }

                                        if (CajaAperturada)
                                        {
                                            var DataParametros = await _IParametros.List("Validar stock para venta");

                                            if (DataParametros.Data != null)
                                            {
                                                if (DataParametros.Data.Count > 0)
                                                {
                                                    string ValidaStock = DataParametros.Data[0].Value;
                                                    if (ValidaStock == "SI")
                                                    {
                                                        decimal Stock = DataProducto.Data[0].Stock;
                                                        if (Stock > 0)
                                                        {
                                                            IdentificarPrecio(DataProducto);
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show($"Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IdentificarPrecio(DataProducto);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se encontro informacion del producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                errorProvider1.SetError(txt_buscar_producto, $"Debe ingresar un {cbx_busca_por.Text}");
                            }
                        }
                    }
                }
            }
            else if (cbx_busca_por.Text == "Sku" || cbx_busca_por.Text == "Codigo barras")
            {
                //Enter
                if (e.KeyChar == (char)13)
                {
                    if (txt_busca_cliente.Text.Trim() != "")
                    {
                        var resp = await _ICliente.ListNumDoc(txt_busca_cliente.Text);

                        if (resp.Data != null)
                        {
                            if (resp.Data.Count == 0)
                            {
                                errorProvider1.SetError(txt_busca_cliente, $"Numero documento {txt_busca_cliente.Text} no encontrado");
                                lbl_nombre_cliente.Text = "_";
                                continuar = false;
                            }
                        }
                    }
                    else
                    {
                        var resp = await _ICliente.List(1);
                        if (resp.Data != null)
                        {
                            if (resp.Data.Count > 0)
                            {
                                agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                                txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                                lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                                IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);
                            }
                        }
                    }

                    if (continuar)
                    {
                        if (txt_buscar_producto.Text.Trim() != "")
                        {
                            if (cbx_busca_por.Text == "Sku")
                            {
                                var DataProducto = await _IProducto.ListSku(txt_buscar_producto.Text);

                                if (DataProducto.Data != null)
                                {
                                    if (DataProducto.Data.Count > 0)
                                    {
                                        IdentificarPrecio(DataProducto);
                                    }
                                    else
                                    {
                                        errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                    }
                                }
                            }
                            else if (cbx_busca_por.Text == "Codigo barras")
                            {
                                var DataProducto = await _IProducto.ListCodigoBarras(txt_buscar_producto.Text);

                                if (DataProducto.Data != null)
                                {
                                    if (DataProducto.Data.Count > 0)
                                    {
                                        IdentificarPrecio(DataProducto);
                                    }
                                    else
                                    {
                                        errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                    }
                                }
                            }
                        }
                        else
                        {
                            errorProvider1.SetError(txt_buscar_producto, $"Debe ingresar un {cbx_busca_por.Text}");
                        }
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
            errorProvider1.Clear();

            if (busqueda == "Add_AgregaVenta_busca_cliente")
            {
                var resp = await _ICliente.List(id);
                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                        txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                        lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                        IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);

                        if (Convert.ToInt32(cbx_lista_precio.SelectedValue) > 1)
                        {
                            resp = await _IListaPrecios.List(Convert.ToInt32(cbx_lista_precio.SelectedValue));
                            if (resp.Data != null)
                            {
                                if (resp.Data.Count > 0)
                                {
                                    if (IdTipoCliente != Convert.ToInt32(resp.Data[0].IdTipoCliente))
                                    {
                                        MessageBox.Show($"La lista de precios {cbx_lista_precio.Text} no está disponible para el tipo de cliente asignado a {lbl_nombre_cliente.Text}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        cbx_lista_precio.SelectedIndex = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (busqueda == "Add_AgregaVenta_busca_producto")
            {
                bool continuar = true;
                CajaAperturada = false;

                if (txt_busca_cliente.Text.Trim() != "")
                {
                    var resp = await _ICliente.ListNumDoc(txt_busca_cliente.Text);

                    if (resp.Data != null)
                    {
                        if (resp.Data.Count == 0)
                        {
                            errorProvider1.SetError(txt_busca_cliente, $"Numero documento {txt_busca_cliente.Text} no encontrado");
                            lbl_nombre_cliente.Text = "_";
                            continuar = false;
                        }
                    }
                }
                else
                {
                    var resp = await _ICliente.List(1);
                    if (resp.Data != null)
                    {
                        if (resp.Data.Count > 0)
                        {
                            agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                            txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                            lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                            IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);
                        }
                    }
                }

                if (continuar)
                {
                    var DataProducto = await _IProducto.List(id);
                    if (DataProducto.Data != null)
                    {
                        if (DataProducto.Data.Count > 0)
                        {
                            var estadoCaja = await _ICaja.EstadoCaja(Convert.ToInt32(_Permisos?[0]?.IdUser));
                            if (estadoCaja.Data != null)
                            {
                                if (estadoCaja.Data.Count > 0)
                                {
                                    if (estadoCaja.Data[0].Estado == "CERRADA")
                                    {
                                        if (_Permisos != null)
                                        {
                                            _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                                            _AddApertura.Permisos = _Permisos;
                                            _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                                            _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                                            _AddApertura.ShowDialog();
                                        }
                                    }
                                    else if (estadoCaja.Data[0].Estado == "ABIERTA")
                                    {
                                        CajaAperturada = true;
                                    }
                                }
                                else
                                {
                                    if (_Permisos != null)
                                    {
                                        _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                                        _AddApertura.Permisos = _Permisos;
                                        _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                                        _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                                        _AddApertura.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"No se logro obtener informacion de estado de caja", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CajaAperturada = false;
                            }

                            if (CajaAperturada)
                            {
                                var DataParametros = await _IParametros.List("Validar stock para venta");

                                if (DataParametros.Data != null)
                                {
                                    if (DataParametros.Data.Count > 0)
                                    {
                                        string ValidaStock = DataParametros.Data[0].Value;
                                        if (ValidaStock == "SI")
                                        {
                                            decimal Stock = DataProducto.Data[0].Stock;
                                            if (Stock > 0)
                                            {
                                                IdentificarPrecio(DataProducto);
                                            }
                                            else
                                            {
                                                MessageBox.Show($"Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            IdentificarPrecio(DataProducto);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontro informacion del producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontro informacion del producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void IdentificarPrecio(dynamic DataProducto)
        {
            bool Continuar = true;

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
                    if (resp1.Data.Count > 0)
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
                             Total.ToString("N2", new CultureInfo("es-CO")),
                             DataProducto.Data[0].NombreUnidadMedida,
                             DataProducto.Data[0].CostoBase.ToString("N2", new CultureInfo("es-CO"))
                            );

                        Continuar = false;
                        cbx_lista_precio.SelectedIndex = 0;
                        cbx_lista_precio.Enabled = false;
                    }
                }
            }
            //2. ¿Pertenece a una lista de precios por tipo de cliente?
            agregaVentaEntitie.IdListaPrecio = Convert.ToInt32(cbx_lista_precio.SelectedValue);
            if (agregaVentaEntitie.IdListaPrecio > 1 && Continuar == true)
            {
                var resp2 = await _IPrecioProducto.PrecioListaPreciosTipoCliente(Convert.ToInt32(DataProducto.Data[0].IdProducto), agregaVentaEntitie.IdListaPrecio, IdTipoCliente);
                if (resp2.Data != null)
                {
                    if (resp2.Data.Count > 0)
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
                             Total.ToString("N2", new CultureInfo("es-CO")),
                             DataProducto.Data[0].NombreUnidadMedida,
                             DataProducto.Data[0].CostoBase.ToString("N2", new CultureInfo("es-CO"))
                            );

                        Continuar = false;
                    }
                }
            }
            //3. ¿Hay promociones activas? => aplicar sobre el precio base
            if (Continuar == true)
            {
                var resp3 = await _IPromocionProducto.PromocionActiva(Convert.ToInt32(DataProducto.Data[0].IdProducto));
                if (resp3.Data != null)
                {
                    if (resp3.Data.Count > 0)
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
                             Total.ToString("N2", new CultureInfo("es-CO")),
                             DataProducto.Data[0].NombreUnidadMedida,
                             DataProducto.Data[0].CostoBase.ToString("N2", new CultureInfo("es-CO"))
                            );

                        Continuar = false;
                    }
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
                     Total.ToString("N2", new CultureInfo("es-CO")),
                     DataProducto.Data[0].NombreUnidadMedida,
                     DataProducto.Data[0].CostoBase.ToString("N2", new CultureInfo("es-CO"))
                    );

                Continuar = false;
            }

            if (Continuar == false) { txt_buscar_producto.Text = ""; }

            if (dtg_producto.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dtg_producto.Rows)
                {
                    Cantidad += Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    Subtotal += Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    SubtotalLinea = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                    DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                    Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                }
                Total = (Subtotal - Descuento) + Impuesto;

                lbl_cantidadProductos.Text = Cantidad.ToString(new CultureInfo("es-CO"));
                lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));

                cbx_lista_precio.Enabled = false;
                txt_busca_cliente.Enabled = false;
                btn_busca_cliente.Enabled = false;
                pnl_pagos.Enabled = true;
                ValidarModoPagos();
            }
            else
            {
                cbx_lista_precio.Enabled = true;
                txt_busca_cliente.Enabled = true;
                btn_busca_cliente.Enabled = true;
                pnl_pagos.Enabled = false;
                cbx_lista_precio.Enabled = true;
            }
        }

        private void txt_valor_pago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                GuardarVenta();
                e.Handled = true; // opcional: evita beep
                return;
            }

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
                else if (Cambio > 0)
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

        private void btn_nuevo_cliente_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarCliente = _serviceProvider.GetRequiredService<AgregarCliente>();
                _AgregarCliente.Permisos = _Permisos;
                _AgregarCliente.Id_Cliente = 0;
                _AgregarCliente.FormClosed += (s, args) => _AgregarCliente = null;
                _AgregarCliente.ShowDialog();
            }
        }

        private void dtg_producto_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Remover eventos anteriores para evitar duplicados
            e.Control.KeyPress -= new KeyPressEventHandler(dtg_producto_KeyPress);

            if (dtg_producto.CurrentCell.ColumnIndex == 5 || dtg_producto.CurrentCell.ColumnIndex == 6)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dtg_producto_KeyPress);
            }
        }

        private void dtg_producto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void dtg_producto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                var celda = dtg_producto[e.ColumnIndex, e.RowIndex];

                if (celda.Value == null || string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    if (e.ColumnIndex == 5)
                    {
                        celda.Value = 1;
                    }
                    else if (e.ColumnIndex == 6)
                    {
                        celda.Value = 0;
                    }

                    decimal precio = Convert.ToDecimal(dtg_producto[4, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal cantidad = Convert.ToDecimal(dtg_producto[5, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal desc = Convert.ToDecimal(dtg_producto[6, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal iva = Convert.ToDecimal(dtg_producto[7, e.RowIndex].Value, new CultureInfo("es-CO"));

                    decimal total = CalcularTotal(precio, iva, desc);
                    total = total * cantidad;

                    dtg_producto[8, e.RowIndex].Value = total.ToString("N2", new CultureInfo("es-CO"));

                    RecalcularTotal();
                }
                else
                {
                    if (e.ColumnIndex == 5 && Convert.ToDecimal(celda.Value, new CultureInfo("es-CO")) <= 0)
                    {
                        celda.Value = 1;
                    }

                    decimal precio = Convert.ToDecimal(dtg_producto[4, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal cantidad = Convert.ToDecimal(dtg_producto[5, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal desc = Convert.ToDecimal(dtg_producto[6, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal iva = Convert.ToDecimal(dtg_producto[7, e.RowIndex].Value, new CultureInfo("es-CO"));

                    decimal total = CalcularTotal(precio, iva, desc);
                    total = total * cantidad;

                    dtg_producto[8, e.RowIndex].Value = total.ToString("N2", new CultureInfo("es-CO"));

                    RecalcularTotal();
                }
            }
        }

        private void RecalcularTotal()
        {
            Cantidad = 0;
            Subtotal = 0;
            Descuento = 0;
            Impuesto = 0;

            if (dtg_producto.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dtg_producto.Rows)
                {
                    Cantidad += Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    Subtotal += Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    SubtotalLinea = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                    Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                    DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                    Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                }
                Total = (Subtotal - Descuento) + Impuesto;

                lbl_cantidadProductos.Text = Cantidad.ToString(new CultureInfo("es-CO"));
                lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));

                btn_busca_cliente.Enabled = false;
                pnl_pagos.Enabled = true;
                ValidarModoPagos();
            }
            else
            {
                btn_busca_cliente.Enabled = true;
                pnl_pagos.Enabled = false;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de cancelar la venta?",
                             "Confirmar cancelacion",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("No se encontro informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void Limpiar()
        {
            cbx_lista_precio.Enabled = true;
            cbx_lista_precio.SelectedIndex = 0;
            cbx_vendedor.SelectedIndex = 0;

            var resp = await _ICliente.List(1);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                    txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                }
            }

            cbx_lista_precio.Enabled = true;
            txt_busca_cliente.Enabled = true;
            btn_busca_cliente.Enabled = true;

            dtg_producto.Rows.Clear();

            cbx_medio_pago.SelectedIndex = 0;
            txt_referencia_pago.Text = "";
            cbx_banco.SelectedIndex = 0;
            txt_valor_pago.Text = "";
            lbl_cambio.Text = "_";

            lbl_cantidadProductos.Text = "_";
            lbl_subtotal.Text = "_";
            lbl_descuento.Text = "_";
            lbl_impuesto.Text = "_";
            lbl_total.Text = "_";

            cbx_busca_por.SelectedIndex = 0;

            pnl_pagos.Enabled = false;

            errorProvider1.Clear();
        }

        private async void cbx_lista_precio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CargaListaPrecio)
            {
                if (Convert.ToInt32(cbx_lista_precio.SelectedValue) > 1)
                {
                    var resp = await _IListaPrecios.List(Convert.ToInt32(cbx_lista_precio.SelectedValue));
                    if (resp.Data != null)
                    {
                        if (resp.Data.Count > 0)
                        {
                            if (IdTipoCliente != Convert.ToInt32(resp.Data[0].IdTipoCliente))
                            {
                                MessageBox.Show($"La lista de precios {cbx_lista_precio.Text} no está disponible para el tipo de cliente asignado a {lbl_nombre_cliente.Text}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cbx_lista_precio.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        private void btn_suspender_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {

            }
            else
            {
                MessageBox.Show($"No hay productos agregados para suspender la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_completar_venta_Click(object sender, EventArgs e)
        {
            GuardarVenta();
        }

        private async void GuardarVenta()
        {
            bool continuar = true;
            errorProvider1.Clear();

            if (txt_valor_pago.Text.Trim() != "")
            {
                if (cbx_medio_pago.Text == "Efectivo")
                {
                    if (Convert.ToDecimal(lbl_cambio.Text, new CultureInfo("es-CO")) < 0)
                    {
                        continuar = false;
                    }
                }

                if (continuar)
                {
                    DateTime FechaVencimiento = DateTime.Now;
                    venta.IdCliente = agregaVentaEntitie.IdCliente;
                    venta.IdVendedor = Convert.ToInt32(cbx_vendedor.SelectedValue);
                    venta.IdMetodoPago = Convert.ToInt32(cbx_medio_pago.SelectedValue);
                    venta.Estado = "FACTURADA";
                    var resp = await _IRangoNumeracion.List(0);
                    if (resp.Data != null)
                    {
                        if (resp.Data.Count > 0)
                        {
                            venta.Prefijo = resp.Data[0].Prefijo;
                            FechaVencimiento = Convert.ToDateTime(resp.Data[0].FechaVencimiento);
                        }

                        DateTime FechaActual = DateTime.Now;
                        FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                        if (FechaVencimiento >= FechaActual)
                        {
                            List<DetalleVentaEntitie> detalleVentas = new List<DetalleVentaEntitie>();

                            foreach (DataGridViewRow fila in dtg_producto.Rows)
                            {
                                DetalleVentaEntitie Detalle = new DetalleVentaEntitie
                                {
                                    IdProducto = Convert.ToInt32(fila.Cells["cl_idProducto"].Value),
                                    Sku = fila.Cells["cl_sku"].Value?.ToString() ?? "",
                                    CodigoBarras = fila.Cells["cl_codigo_barras"].Value?.ToString() ?? "",
                                    UnidadMedida = fila.Cells["cl_unidad_medida"].Value?.ToString() ?? "",
                                    NombreProducto = fila.Cells["cl_nombre"].Value?.ToString() ?? "",
                                    Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO")),
                                    PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")),
                                    Descuento = Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")),
                                    Impuesto = Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")),
                                    CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo"].Value, new CultureInfo("es-CO"))
                                };

                                detalleVentas.Add(Detalle);
                            }

                            venta.detalleVentas = detalleVentas;

                            List<PagosVentaEntitie> pagosVentaEntities = new List<PagosVentaEntitie>();
                            PagosVentaEntitie pagosVentaEntitie = new PagosVentaEntitie
                            {
                                IdMetodoPago = Convert.ToInt32(cbx_medio_pago.SelectedValue),
                                Recibido = Convert.ToDecimal(txt_valor_pago.Text, new CultureInfo("es-CO")),
                                Monto = Convert.ToDecimal(lbl_total.Text, new CultureInfo("es-CO")),
                                Referencia = txt_referencia_pago.Text,
                                IdBanco = Convert.ToInt32(cbx_banco.SelectedValue)
                            };
                            pagosVentaEntities.Add(pagosVentaEntitie);
                            venta.pagosVenta = pagosVentaEntities;

                            var respGuardado = await _IVenta.Create(venta, Convert.ToInt32(_Permisos?[0]?.IdUser));

                            if (respGuardado != null)
                            {
                                if (respGuardado.Flag == true)
                                {
                                    MessageBox.Show(respGuardado.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Limpiar();

                                    //Imprime Tirilla
                                    var DataTienda = await _ITienda.List();
                                    if (DataTienda.Data != null)
                                    {
                                        if (DataTienda.Data.Count > 0)
                                        {
                                            var DataVenta = await _IVenta.List(respGuardado.Data);

                                            FacturaPOSEntitie DataFactura = new FacturaPOSEntitie();

                                            DataFactura.NumeroFactura = DataVenta.Data[0].Factura;
                                            DataFactura.Fecha = DataVenta.Data[0].FechaFactura;
                                            DataFactura.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                            DataFactura.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                            DataFactura.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                            DataFactura.NIT = DataTienda.Data[0].NumeroDocumento;
                                            DataFactura.UserNameFactura = DataVenta.Data[0].IdUserActionFactura + " - " + DataVenta.Data[0].UserNameFactura;
                                            DataFactura.NombreCliente = DataVenta.Data[0].NumeroDocumento + " - " + DataVenta.Data[0].NombreRazonSocial;
                                            DataFactura.NombreVendedor = DataVenta.Data[0].NumeroDocumentoVendedor + " - " + DataVenta.Data[0].NombreVendedor;
                                            DataFactura.FormaPago = DataVenta.Data[0].NombreMetodoPago;
                                            DataFactura.Recibido = DataVenta.Data[0].Recibido;

                                            Cantidad = 0;
                                            Subtotal = 0;
                                            Descuento = 0;
                                            Impuesto = 0;
                                            SubtotalLinea = 0;
                                            DescuentoLinea = 0;

                                            foreach (var item in DataVenta.Data)
                                            {
                                                Cantidad += Convert.ToDecimal(item.Cantidad);
                                                Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                                SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                                Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                                DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                                Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                            }
                                            Total = (Subtotal - Descuento) + Impuesto;

                                            DataFactura.CantidadTotal = Cantidad;
                                            DataFactura.Subtotal = Subtotal;
                                            DataFactura.Descuento = Descuento;
                                            DataFactura.Impuesto = Impuesto;
                                            DataFactura.Total = Total;
                                            DataFactura.Cambio = DataFactura.Recibido - Total;

                                            List<ItemFacturaEntitie> ListItemFacturaEntitie = new List<ItemFacturaEntitie>();

                                            decimal precio;
                                            decimal cantidad;
                                            decimal desc;
                                            decimal iva;
                                            decimal totalLinea;
                                            decimal total;

                                            foreach (var item in DataVenta.Data)
                                            {
                                                precio = Convert.ToDecimal(item.PrecioUnitario);
                                                cantidad = Convert.ToDecimal(item.Cantidad);
                                                desc = Convert.ToDecimal(item.Descuento);
                                                iva = Convert.ToDecimal(item.Impuesto);

                                                total = CalcularTotal(precio, iva, desc);
                                                total = total * cantidad;

                                                string UnidadMedidaAbreviada;

                                                switch (item.UnidadMedida)
                                                {
                                                    case "Unidad (und)":
                                                        UnidadMedidaAbreviada = "Unidad (und)";
                                                        break;
                                                    case "Caja (caja)":
                                                        UnidadMedidaAbreviada = "caja";
                                                        break;
                                                    case "Paquete (paq)":
                                                        UnidadMedidaAbreviada = "paq";
                                                        break;
                                                    case "Bolsa (bol)":
                                                        UnidadMedidaAbreviada = "bol";
                                                        break;
                                                    case "Litro (lt)":
                                                        UnidadMedidaAbreviada = "lt";
                                                        break;
                                                    case "Mililitro (ml)":
                                                        UnidadMedidaAbreviada = "ml";
                                                        break;
                                                    case "Kilogramo (kg)":
                                                        UnidadMedidaAbreviada = "kg";
                                                        break;
                                                    case "Gramo (g)":
                                                        UnidadMedidaAbreviada = "g";
                                                        break;
                                                    case "Metro (m)":
                                                        UnidadMedidaAbreviada = "m";
                                                        break;
                                                    case "Par (par)":
                                                        UnidadMedidaAbreviada = "par";
                                                        break;
                                                    default:
                                                        UnidadMedidaAbreviada = "";
                                                        break;
                                                }

                                                var ItemFactura = new ItemFacturaEntitie
                                                {
                                                    Codigo = item.IdProducto,
                                                    Descripcion = item.NombreProducto,
                                                    Cantidad = item.Cantidad,
                                                    UnidadMedida = UnidadMedidaAbreviada,
                                                    PrecioUnitario = item.PrecioUnitario,
                                                    Descuento = item.Descuento,
                                                    Impuesto = item.Impuesto,
                                                    Total = total
                                                };

                                                ListItemFacturaEntitie.Add(ItemFactura);
                                            }

                                            DataFactura.Items = ListItemFacturaEntitie;

                                            var DataParametros = await _IParametros.List("");

                                            if (DataParametros.Data != null)
                                            {
                                                if (DataParametros.Data.Count > 0)
                                                {
                                                    int ANCHO_TIRILLA = 0;
                                                    string PreguntarParaImprimir = "";
                                                    string Impresora = "";
                                                    foreach (var itemParametros in DataParametros.Data)
                                                    {
                                                        switch (itemParametros.Nombre)
                                                        {
                                                            case "Ancho tirilla":
                                                                ANCHO_TIRILLA = Convert.ToInt32(itemParametros.Value);
                                                                break;
                                                            case "Preguntar imprimir factura en venta":
                                                                PreguntarParaImprimir = itemParametros.Value;
                                                                break;
                                                            case "Impresora":
                                                                Impresora = itemParametros.Value;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }

                                                    StringBuilder tirilla = GenerarTirillaPOS.GenerarTirilla(DataFactura, ANCHO_TIRILLA);

                                                    string carpetaFacturas = "Facturas";
                                                    if (!Directory.Exists(carpetaFacturas))
                                                    {
                                                        Directory.CreateDirectory(carpetaFacturas);
                                                    }

                                                    File.WriteAllText(Path.Combine(carpetaFacturas, $"factura_{DataFactura.NumeroFactura}.txt"),
                                                                      tirilla.ToString(),
                                                                      Encoding.UTF8);

                                                    if (PreguntarParaImprimir == "SI")
                                                    {
                                                        DialogResult result = MessageBox.Show("¿Está seguro de imprimir la factura?",
                                                        "Confirmar cancelacion",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                                                        if (result == DialogResult.Yes)
                                                        {
                                                            RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString());
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("No se encuentra informacion de ancho de tirilla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(respGuardado.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se registro venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Rango de numeracion vencido, fecha de vencimiento: {FechaVencimiento}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    errorProvider1.SetError(lbl_cambio, "Valor debe ser mayor o igual a cero");
                }
            }
            else
            {
                errorProvider1.SetError(txt_valor_pago, "Debe ingresar el valor a pagar");
            }
        }

        private async void txt_busca_cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
            else
            {
                //Enter
                if (e.KeyChar == (char)13)
                {
                    if (txt_busca_cliente.Text.Trim() != "")
                    {
                        var resp = await _ICliente.ListNumDoc(txt_busca_cliente.Text);

                        if (resp.Data != null)
                        {
                            if (resp.Data.Count > 0)
                            {
                                agregaVentaEntitie.IdCliente = Convert.ToInt32(resp.Data[0].IdCliente);
                                txt_busca_cliente.Text = resp.Data[0].NumeroDocumento;
                                lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                                IdTipoCliente = Convert.ToInt32(resp.Data[0].IdTipoCliente);
                            }
                            else
                            {
                                lbl_nombre_cliente.Text = "_";
                                errorProvider1.SetError(txt_busca_cliente, $"Numero documento {txt_busca_cliente.Text} no encontrado");
                            }
                        }
                    }
                    else
                    {
                        lbl_nombre_cliente.Text = "_";
                        errorProvider1.SetError(txt_busca_cliente, $"Debe ingresar un numero de documento");
                    }
                }
            }
        }

        private void btn_ver_ventas_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _Ventas = _serviceProvider.GetRequiredService<Ventas>();
                _Ventas.Permisos = _Permisos;
                _Ventas.FormClosed += (s, args) => _Ventas = null;
                _Ventas.ShowDialog();
            }
        }

        private void ConfirmacionAperturaCaja(bool CajaAbierta)
        {
            CajaAperturada = CajaAbierta;
        }

        private void btn_devolucion_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarNotaCredito = _serviceProvider.GetRequiredService<AgregarNotaCredito>();
                _AgregarNotaCredito.Permisos = _Permisos;
                _AgregarNotaCredito.FormClosed += (s, args) => _AgregarNotaCredito = null;
                _AgregarNotaCredito.ShowDialog();
            }
        }

        private async void btn_pagos_en_efectivo_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                CajaAperturada = false;
                var estadoCaja = await _ICaja.EstadoCaja(Convert.ToInt32(_Permisos?[0]?.IdUser));
                if (estadoCaja.Data != null)
                {
                    if (estadoCaja.Data.Count > 0)
                    {
                        if (estadoCaja.Data[0].Estado == "CERRADA")
                        {
                            if (_Permisos != null)
                            {
                                _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                                _AddApertura.Permisos = _Permisos;
                                _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                                _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                                _AddApertura.ShowDialog();
                            }
                        }
                        else if (estadoCaja.Data[0].Estado == "ABIERTA")
                        {
                            CajaAperturada = true;
                        }
                    }
                    else
                    {
                        if (_Permisos != null)
                        {
                            _AddApertura = _serviceProvider.GetRequiredService<AddApertura>();
                            _AddApertura.Permisos = _Permisos;
                            _AddApertura.ConfirmacionAperturaCaja += ConfirmacionAperturaCaja;
                            _AddApertura.FormClosed += (s, args) => _AddApertura = null;
                            _AddApertura.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"No se logro obtener informacion de estado de caja", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CajaAperturada = false;
                }

                if (CajaAperturada)
                {
                    _AddPagosEfectivo = _serviceProvider.GetRequiredService<AddPagosEfectivo>();
                    _AddPagosEfectivo.Permisos = _Permisos;
                    _AddPagosEfectivo.Id_Pago = 0;
                    _AddPagosEfectivo.FormClosed += (s, args) => _AddPagosEfectivo = null;
                    _AddPagosEfectivo.ShowDialog();
                } 
            }
        }
    }
}
