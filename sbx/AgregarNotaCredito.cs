using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Entities.Auth;
using sbx.core.Entities.NotaCredito;
using sbx.core.Entities.NotaCreditoElectronica;
using sbx.core.Interfaces.FacturacionElectronica;
using sbx.core.Interfaces.NotaCredito;
using sbx.core.Interfaces.NotaCreditoElectronica;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Venta;
using System.Configuration;
using System.Globalization;

namespace sbx
{
    public partial class AgregarNotaCredito : Form
    {
        private readonly IVenta _IVenta;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly INotaCredito _INotaCredito;
        private readonly IAuthService _IAuthService;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly INotasCreditoElectronica _INotasCreditoElectronica;
        private int _Id_Nota_Credito;
        private int _Id_Id_Venta;
        int IdFactura = 0;
        private dynamic? _Permisos;
        decimal TotalParaDevolucion = 0;
        char decimalSeparator = ',';
        string FacturaJSON = "";
        int IdIdentificationType = 0;
        bool FacturaElectronica = false;

        NotaCreditoEntitie notaCreditoEntitie = new NotaCreditoEntitie();

        public AgregarNotaCredito(IVenta venta, IServiceProvider serviceProvider, INotaCredito notaCredito, 
            IAuthService authService, IRangoNumeracion iRangoNumeracion, INotasCreditoElectronica notasCreditoElectronica)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
            _INotaCredito = notaCredito;
            _IAuthService = authService;
            _IRangoNumeracion = iRangoNumeracion;
            _INotasCreditoElectronica = notasCreditoElectronica;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Nota_Credito
        {
            get => _Id_Nota_Credito;
            set => _Id_Nota_Credito = value;
        }

        public int Id_Venta
        {
            get => _Id_Id_Venta;
            set => _Id_Id_Venta = value;
        }

        private async void AgregarNotaCredito_Load(object sender, EventArgs e)
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
                        case "notaCredito":
                            btn_busca_factura.Enabled = item.ToRead == 1 ? true : false;
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

        private async Task CargaDatosIniciales()
        {
            if (Id_Venta > 0) 
            {
                btn_busca_factura.Enabled = false;
                btn_devolucion.Enabled = false;
                txt_motivo_devolucion.Enabled = false;

                var resp = await _IVenta.List(Id_Venta);
                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        IdIdentificationType = Convert.ToInt32(resp.Data[0].IdIdentificationType);
                        dtg_ventas.Rows.Clear();
                        chk_marcar_todo.Checked = false;
                        txt_busca_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                        lbl_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                        if (resp.Data[0].NumberFacturaDIAN != "") { FacturaElectronica = true; } else { FacturaElectronica = false; }
                        lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                        lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                        lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                        lbl_referencia.Text = resp.Data[0].Referencia;
                        lbl_banco.Text = resp.Data[0].NombreBanco;
                        lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;

                        decimal Subtotal = 0;
                        decimal cantidadTotal = 0;
                        decimal DescuentoLinea;
                        decimal Descuento = 0;
                        decimal Impuesto = 0;
                        decimal ImpuestoLinea;
                        decimal SubtotalLinea;
                        decimal Total = 0;
                        decimal TotalLinea;

                        foreach (var item in resp.Data)
                        {
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));

                            TotalLinea = (SubtotalLinea - DescuentoLinea);
                            //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                            dtg_ventas.Rows.Add(
                                item.IdDetalleVenta,
                                false,
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.NombreProducto,
                                item.UnidadMedida,
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                                TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                        }

                        Total += (Subtotal - Descuento);
                        //Total += (Subtotal - Descuento) + Impuesto;
                        decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                        lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                        lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                        lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));

                        chk_marcar_todo.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btn_busca_factura_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Busca_factura";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            IdFactura = id;
            var resp = await _IVenta.List(id);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    if (resp.Data[0].Estado == "ANULADA") 
                    {
                        if (resp.Data[0].IdNotaCredito > 0) 
                        {
                            MessageBox.Show($"Factura: {resp.Data[0].Factura} en estado ANULADA con Nota credito: NC-{resp.Data[0].IdNotaCredito} ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Factura: {resp.Data[0].Factura} en estado ANULADA ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }

                    FacturaJSON = resp.Data[0].FacturaJSON;
                    IdIdentificationType = Convert.ToInt32(resp.Data[0].IdIdentificationType);
                    dtg_ventas.Rows.Clear();
                    chk_marcar_todo.Checked = false;
                    txt_busca_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                    lbl_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura : resp.Data[0].NumberFacturaDIAN;
                    if (resp.Data[0].NumberFacturaDIAN != "") { FacturaElectronica = true; } else {  FacturaElectronica = false; } 
                    lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                    lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                    lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                    lbl_referencia.Text = resp.Data[0].Referencia;
                    lbl_banco.Text = resp.Data[0].NombreBanco;
                    lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;

                    decimal Subtotal = 0;
                    decimal cantidadTotal = 0;
                    decimal DescuentoLinea;
                    decimal Descuento = 0;
                    decimal Impuesto = 0;
                    decimal ImpuestoLinea;
                    decimal SubtotalLinea;
                    decimal Total = 0;
                    decimal TotalLinea;

                    foreach (var item in resp.Data)
                    {
                        cantidadTotal += Convert.ToDecimal(item.Cantidad);
                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));

                        TotalLinea = (SubtotalLinea - DescuentoLinea);
                        //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                        dtg_ventas.Rows.Add(
                            item.IdDetalleVenta,
                            false,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.NombreProducto,
                            item.UnidadMedida,
                            item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                            TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                    }

                    Total += (Subtotal - Descuento);
                    //Total += (Subtotal - Descuento) + Impuesto;
                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                    lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                    lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                    lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));

                    chk_marcar_todo.Checked = true;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_devolucion_parcial_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int Contador = 0;
                bool TodoProductTieneCantDevo = true;

                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) > 0)
                    {
                        Contador++;
                    }

                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) <= 0)
                    {
                        TodoProductTieneCantDevo = false;
                    }
                }

                if (Contador > 0 && TodoProductTieneCantDevo)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de realizar devolucion?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var respDoc = await _IRangoNumeracion.IdentificaDocumento(22);

                        int Id_RangoNumeracion;
                        int IdRangoDIAN;

                        if (respDoc.Data != null)
                        {
                            if (respDoc.Data.Count > 0)
                            {
                                long NumDesde = respDoc.Data[0].NumeroDesde;
                                long NumHasta = respDoc.Data[0].NumeroHasta;
                                long Actual = respDoc.Data[0].ConsecutivoActual;
                                string NumeroResolucion = respDoc.Data[0].NumeroResolucion.ToString();
                                string ClaveTecnica = respDoc.Data[0].ClaveTecnica.ToString();
                                bool NotaCreditoElectronica = false;
                                DateTime FechaExpedicion = DateTime.Now;
                                DateTime FechaVencimiento = DateTime.Now;

                                if (Convert.ToInt32(respDoc.Data[0].DocElectronico) == 1) { NotaCreditoElectronica = true; }

                                if (FacturaElectronica == true && NotaCreditoElectronica == false) 
                                {
                                    MessageBox.Show($"Dado que la factura a anular es electrónica, es obligatorio que la nota crédito también sea electrónica. El proceso ha sido cancelado y no se generará la nota crédito.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (Actual <= NumHasta)
                                {
                                    Id_RangoNumeracion = respDoc.Data[0].Id_RangoNumeracion;
                                    IdRangoDIAN = respDoc.Data[0].IdRangoDIAN;

                                    notaCreditoEntitie.IdVenta = IdFactura;
                                    notaCreditoEntitie.Motivo = txt_motivo_devolucion.Text;
                                    notaCreditoEntitie.Estado = "REGISTRADA";
                                    notaCreditoEntitie.Id_RangoNumeracion = Id_RangoNumeracion;
                                    notaCreditoEntitie.IdRangoDIAN = IdRangoDIAN;
                                    notaCreditoEntitie.Prefijo = respDoc.Data[0].Prefijo;
                                    notaCreditoEntitie.Desde = respDoc.Data[0].NumeroDesde;
                                    notaCreditoEntitie.Hasta = respDoc.Data[0].NumeroHasta;
                                    notaCreditoEntitie.resolution_number = NumeroResolucion;
                                    notaCreditoEntitie.technical_key = ClaveTecnica;

                                    FechaExpedicion = Convert.ToDateTime(respDoc.Data[0].FechaExpedicion);
                                    FechaVencimiento = Convert.ToDateTime(respDoc.Data[0].FechaVencimiento);

                                    DateTime FechaActual = DateTime.Now;
                                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                                    if (FechaVencimiento >= FechaActual)
                                    {
                                        foreach (DataGridViewRow fila in dtg_ventas.Rows)
                                        {
                                            if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                                            {
                                                DetalleNotaCredito detalleNotaCredito = new DetalleNotaCredito
                                                {
                                                    IdDetalleVenta = Convert.ToInt32(fila.Cells["cl_id_detalle_venta"].Value),
                                                    IdProducto = Convert.ToInt32(fila.Cells["cl_idProducto"].Value),
                                                    Sku = fila.Cells["cl_sku"].Value.ToString() ?? "",
                                                    CodigoBarras = fila.Cells["cl_codigo_barras"].Value.ToString() ?? "",
                                                    NombreProducto = fila.Cells["cl_nombre"].Value.ToString() ?? "",
                                                    Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")),
                                                    PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")),
                                                    UnidadMedida = fila.Cells["cl_unidadMedida"].Value.ToString() ?? "",
                                                    Descuento = Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")),
                                                    Impuesto = Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO"))
                                                };

                                                notaCreditoEntitie.detalleNotaCredito.Add(detalleNotaCredito);
                                            }
                                        }

                                        var resp = await _INotaCredito.Create(notaCreditoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        if (resp != null)
                                        {
                                            if (resp.Flag == true)
                                            {
                                                int IdNotaCreditoCreada = resp.Data;

                                                ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie =
                                                    new ActualizarNotaCreditoForNotaCreditoElectronicaEntitie();

                                                try
                                                {
                                                    if (NotaCreditoElectronica == true)
                                                    {
                                                        if (!string.IsNullOrEmpty(FacturaJSON))
                                                        {
                                                            AuthEntitie authEntitie = new AuthEntitie
                                                            {
                                                                url_api = ConfigurationManager.AppSettings["UrlAuthPOST"]!,
                                                                grant_type = ConfigurationManager.AppSettings["grant_typeAuth"]!,
                                                                client_id = ConfigurationManager.AppSettings["client_id"]!,
                                                                client_secret = ConfigurationManager.AppSettings["client_secret"]!,
                                                                username = ConfigurationManager.AppSettings["username"]!,
                                                                Passwords = ConfigurationManager.AppSettings["password"]!
                                                            };

                                                            if (!string.IsNullOrEmpty(authEntitie.url_api) && !string.IsNullOrEmpty(authEntitie.grant_type)
                                                                && !string.IsNullOrEmpty(authEntitie.client_id) && !string.IsNullOrEmpty(authEntitie.client_secret)
                                                                && !string.IsNullOrEmpty(authEntitie.username) && !string.IsNullOrEmpty(authEntitie.Passwords))
                                                            {
                                                                var RespAuth = _IAuthService.Autenticacion(authEntitie);

                                                                if (RespAuth.Data != null)
                                                                {
                                                                    if (RespAuth.Flag && RespAuth.Data.access_token != "")
                                                                    {
                                                                        string Token = RespAuth.Data.access_token.ToString();
                                                                        string UrlCrearValidarNotaCredito = ConfigurationManager.AppSettings["UrlCrearValidarNotaCreditoPOST"]!;

                                                                        dynamic? resultado = JsonConvert.DeserializeObject<dynamic>(FacturaJSON);

                                                                        if (resultado != null)
                                                                        {
                                                                            int _bill_id = resultado.data.bill.id;

                                                                            Customer customer = new Customer();
                                                                            customer.identification_document_id =
                                                                               (IdIdentificationType == 1 ? 3 : //Cédula de ciudadanía
                                                                               IdIdentificationType == 2 ? 5 : //Cédula de extranjería
                                                                               IdIdentificationType == 3 ? 3 : //RUT de momento se maneja con Cédula de ciudadanía
                                                                               IdIdentificationType == 4 ? 6 : 3).ToString(); //NIT

                                                                            customer.identification = resultado.data.customer.identification;
                                                                            customer.dv = resultado.data.customer.dv ?? "";
                                                                            if (IdIdentificationType == 4) //NIT
                                                                            {
                                                                                customer.company = resultado.data.customer.company;
                                                                                customer.trade_name = resultado.data.customer.trade_name;
                                                                            }
                                                                            else
                                                                            {
                                                                                customer.names = resultado.data.customer.names;
                                                                            }
                                                                            customer.address = resultado.data.customer.address;
                                                                            customer.email = resultado.data.customer.email;
                                                                            customer.phone = resultado.data.customer.phone;
                                                                            customer.legal_organization_id = resultado.data.customer.legal_organization.id; // 1. Juridico, 2. Natural
                                                                            customer.tribute_id = resultado.data.customer.tribute.id; // 18. IVA, 21. No aplica *
                                                                            customer.municipality_id = "1079"; //Cali, valle del cauca

                                                                            List<Item> Listitems = new List<Item>();
                                                                            foreach (var ItemsVenta in resultado.data.items)
                                                                            {
                                                                                //WithholdingTax withholdingTax = new WithholdingTax
                                                                                //{
                                                                                //    Code = "01",
                                                                                //    WithholdingTaxRate = Convert.ToDecimal(ItemsVenta.Impuesto, new CultureInfo("es-CO"))
                                                                                //};

                                                                                //ListwithholdingTax.Add(withholdingTax);

                                                                                Item item = new Item
                                                                                {
                                                                                    note = ItemsVenta.note,
                                                                                    code_reference = ItemsVenta.code_reference,
                                                                                    name = ItemsVenta.name,
                                                                                    quantity = Convert.ToDecimal(ItemsVenta.quantity, new CultureInfo("es-CO")),
                                                                                    discount_rate = Convert.ToDecimal(ItemsVenta.discount_rate, new CultureInfo("es-CO")),
                                                                                    price = Convert.ToDecimal(ItemsVenta.price, new CultureInfo("es-CO")),
                                                                                    tax_rate = Convert.ToDecimal(ItemsVenta.tax_rate, new CultureInfo("es-CO")),
                                                                                    unit_measure_id = ItemsVenta.unit_measure.id,
                                                                                    standard_code_id = ItemsVenta.standard_code.id,
                                                                                    is_excluded = ItemsVenta.is_excluded,
                                                                                    tribute_id = ItemsVenta.tribute.id,
                                                                                    //withholding_taxes = ItemsVenta.withholding_taxes
                                                                                };

                                                                                Listitems.Add(item);
                                                                            }

                                                                            NotaCreditoRequest notaCreditoRequest = new NotaCreditoRequest
                                                                            {
                                                                                numbering_range_id = IdRangoDIAN,
                                                                                correction_concept_code = 2, //Anulación de factura electrónica.
                                                                                customization_id = 20, //Nota Crédito que referencia una factura electrónica.
                                                                                bill_id = _bill_id,
                                                                                reference_code = IdNotaCreditoCreada.ToString(),
                                                                                send_email = resultado.data.bill.send_email == 0 ? false: true,
                                                                                observation = txt_motivo_devolucion.Text,
                                                                                payment_method_code = resultado.data.bill.payment_method.code,
                                                                                customer = customer,
                                                                                items = Listitems
                                                                            };
                                                                            var responseNotaCreditoElectronica = _INotasCreditoElectronica.CreaValidaNotaCredito(Token, UrlCrearValidarNotaCredito, notaCreditoRequest);

                                                                            if (!responseNotaCreditoElectronica.Flag)
                                                                            {
                                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";

                                                                                MessageBox.Show($"Error en Emicion de nota electronica: {responseNotaCreditoElectronica?.Data} - {responseNotaCreditoElectronica?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (responseNotaCreditoElectronica.Data != null)
                                                                                {
                                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NumberNotaCreditoDIAN = responseNotaCreditoElectronica.Data!.data.credit_note.number;
                                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "EMITIDA";
                                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NotaCreditoJSON = responseNotaCreditoElectronica.Data.ToString();
                                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.qr_image = responseNotaCreditoElectronica.Data!.data.credit_note.qr_image;
                                                                                }
                                                                            }
                                                                            //Actualizar informacion de nota credito electronica en base de datos
                                                                            var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                            if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                            {
                                                                                MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                                this.Close();
                                                                            }   
                                                                        }
                                                                        else
                                                                        {
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                            //Actualizar informacion de nota credito electronica en base de datos
                                                                            var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                            if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                            {
                                                                                MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                            }

                                                                            MessageBox.Show($"Error no se logro leer JSON de factura electronica,  no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                            this.Close();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                        //Actualizar informacion de nota credito electronica en base de datos
                                                                        var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                        if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                        {
                                                                            MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }

                                                                        MessageBox.Show($"Error en autenticacion, no se emitira nota credito electronica: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        this.Close();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                    //Actualizar informacion de nota credito electronica en base de datos
                                                                    var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                    if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                    {
                                                                        MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                                                      
                                                                    }

                                                                    MessageBox.Show($"Error en autenticacion, no se emitira nota credito electronica: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                    this.Close();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                                //Actualizar informacion de nota credito electronica en base de datos
                                                                var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                                if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                                {
                                                                    MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                }

                                                                MessageBox.Show($"No se encuentra informacion completa de Url Apis, no se emitira factura electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                this.Close();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                            //Actualizar informacion de nota credito electronica en base de datos
                                                            var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                            if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                            {
                                                                MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            }

                                                            MessageBox.Show($"No encontro informacion JSON de factura electronica a anular, se registrara nota credito en estado pendiente de Emitir", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            this.Close();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        this.Close();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCreditoCreada;
                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                    //Actualizar informacion de nota credito electronica en base de datos
                                                    var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                    if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                    {
                                                        MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Se presento una excepcion debido a esto se registrara nota credito en estado pendiente de Emitir, Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }

                                                    this.Close();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se obtuvo informacion de proceso creacion de nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Ha excedido el rango de fechas autorizadas (de {FechaExpedicion} a {FechaVencimiento}) ", "Rango de numeracion vencido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($@"Ha excedido el rango autorizado de numeración (de {NumDesde} a {NumHasta}).
                                                      Por favor, solicite una nueva resolución de numeración para continuar con la emisión de nota credito.",
                                                      "Límite de notas credito alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara nota credito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (!TodoProductTieneCantDevo)
                    {
                        MessageBox.Show("Todos los productos seleccionados debe tener Cant.dev mayor a cero, favor verificar ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar 1 producto y total devolucion debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            finally
            {
                panel2.Enabled = true;
                this.Cursor = Cursors.Default;
            }
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

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private void dtg_ventas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalCantidadDevo;
            decimal TotalLineaDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (dtg_ventas.Columns[e.ColumnIndex].Name == "cl_seleccionado")
            {
                dtg_ventas.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //bool isChecked = Convert.ToBoolean(dtg_ventas.Rows[e.RowIndex].Cells["cl_seleccionado"].Value);

                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo);
                        //TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }
                    else
                    {
                        fila.Cells["cl_cantidad_devolver"].Value = 0;
                    }
                }

                lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
            }
        }

        private void chk_marcar_todo_CheckedChanged(object sender, EventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (chk_marcar_todo.Checked)
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = true;
                    TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                    ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                    TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo);
                    //TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                    TotalParaDevolucion += TotalLineaDevo;
                }
            }
            else
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = false;
                    fila.Cells["cl_cantidad_devolver"].Value = 0;
                }
            }

            lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
            lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
        }

        private void dtg_ventas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (e.ColumnIndex == 9)
            {
                var celda = dtg_ventas[e.ColumnIndex, e.RowIndex];

                if (celda.Value == null || string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    if (e.ColumnIndex == 9)
                    {
                        celda.Value = 0;
                    }

                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    decimal cantidad = Convert.ToDecimal(dtg_ventas[8, e.RowIndex].Value, new CultureInfo("es-CO"));
                    decimal cantidadDevo = Convert.ToDecimal(dtg_ventas[9, e.RowIndex].Value, new CultureInfo("es-CO"));

                    if (cantidadDevo > cantidad)
                    {
                        MessageBox.Show("Cantidad a devolver no puede ser mayor a cantidad vendida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (e.ColumnIndex == 9)
                        {
                            celda.Value = 0;
                        }
                        return;
                    }

                    dtg_ventas[1, e.RowIndex].Value = 1;
                   
                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
            }
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private void dtg_ventas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void dtg_ventas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Remover eventos anteriores para evitar duplicados
            e.Control.KeyPress -= new KeyPressEventHandler(dtg_ventas_KeyPress);

            if (dtg_ventas.CurrentCell.ColumnIndex == 9)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dtg_ventas_KeyPress);
            }
        }
    }
}
