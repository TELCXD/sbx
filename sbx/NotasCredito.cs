using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using sbx.core.Entities.Auth;
using sbx.core.Entities.NotaCredito;
using sbx.core.Entities.NotaCreditoElectronica;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.FacturacionElectronica;
using sbx.core.Interfaces.NotaCredito;
using sbx.core.Interfaces.NotaCreditoElectronica;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.Venta;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class NotasCredito : Form
    {
        private dynamic? _Permisos;
        private readonly INotaCredito _INotaCredito;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        private readonly IServiceProvider _serviceProvider;
        private AgregarNotaCredito? _AgregarNotaCredito;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly INotasCreditoElectronica _INotasCreditoElectronica;
        private readonly IAuthService _IAuthService;
        private readonly IVenta _IVenta;
        private readonly IRangoNumeracionFE _IRangoNumeracionFE;

        public NotasCredito(INotaCredito notaCredito, ITienda tienda, IParametros parametros, IServiceProvider serviceProvider,
            IRangoNumeracion rangoNumeracion, INotasCreditoElectronica notasCreditoElectronica, 
            IAuthService iAuthService, IVenta iVenta, IRangoNumeracionFE iRangoNumeracionFE)
        {
            InitializeComponent();
            _INotaCredito = notaCredito;
            _ITienda = tienda;
            _IParametros = parametros;
            _serviceProvider = serviceProvider;
            _IRangoNumeracion = rangoNumeracion;
            _INotasCreditoElectronica = notasCreditoElectronica;
            _IAuthService = iAuthService;
            _IVenta = iVenta;
            _IRangoNumeracionFE = iRangoNumeracionFE;
        }

        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;
        bool NotaCreditoElectronica = false;
        bool FacturaElectronica = false;
        NotaCreditoEntitie notaCreditoEntitie = new NotaCreditoEntitie();
        string FacturaJSON = "";
        int IdIdentificationType = 0;
        string MotivoNotaCredito = "";

        private void NotasCredito_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            cbx_client_venta.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
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
                            btn_imprimir.Enabled = item.ToCreate == 1 ? true : false;
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
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

        private async Task ConsultaNotascredito()
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

            var resp = await _INotaCredito.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_venta.Text, dtp_fecha_inicio.Value, dtp_fecha_fin.Value, Convert.ToInt32(_Permisos?[0]?.IdUser), _Permisos?[0]?.NameRole);

            dtg_nota_credito.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    decimal Subtotal = 0;
                    decimal cantidadTotal = 0;
                    decimal DescuentoLinea;
                    decimal Descuento = 0;
                    decimal Impuesto = 0;
                    decimal ImpuestoLinea;
                    decimal SubtotalLinea;
                    decimal Total = 0;
                    decimal TotalLinea;
                    decimal iva = 0;
                    decimal inc = 0;
                    decimal incBolsa = 0;

                    foreach (var item in resp.Data)
                    {
                        if (item.Estado == "REGISTRADA") { cantidadTotal += Convert.ToDecimal(item.Cantidad); }
                        if (item.Estado == "REGISTRADA") { Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad); }
                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                        if (item.Estado == "REGISTRADA") { Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento)); }
                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                        if (item.NombreTributo == "INC Bolsas") 
                        {
                            if (item.Estado == "REGISTRADA") { Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto)); }
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                        }
                        else
                        {
                            if (item.Estado == "REGISTRADA") { Impuesto += Convert.ToDecimal(item.Impuesto); }
                            ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                        }

                        if (item.Estado == "REGISTRADA")
                        {
                            if (item.NombreTributo == "INC Bolsas")
                            {
                                incBolsa += Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO"));
                            }
                            else if (item.NombreTributo == "IVA")
                            {
                                iva += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                            }
                            else if (item.NombreTributo == "INC")
                            {
                                inc += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                            }
                        }

                        //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;
                        TotalLinea = (SubtotalLinea - DescuentoLinea);

                        int rowIndex = dtg_nota_credito.Rows.Add(
                            item.CreationDate,
                            item.IdNotaCredito,
                            item.IdVenta,
                            item.NumberNotaCreditoDIAN != "" ? item.NumberNotaCreditoDIAN : item.NotaCredito,
                            item.EstadoNotaCreditoDIAN != "" ? item.Estado == "REGISTRADA" ? item.EstadoNotaCreditoDIAN : item.Estado : item.Estado,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.NombreProducto,
                            item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                            Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                            item.NombreTributo,
                            Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                            TotalLinea.ToString("N2", new CultureInfo("es-CO")),
                            item.IdUserAction + " - " + item.UserName);

                        var celdaEstado = dtg_nota_credito.Rows[rowIndex].Cells[4];
                        if (celdaEstado.Value.ToString() == "PENDIENTE EMITIR")
                        {
                            celdaEstado.Style.BackColor = Color.LightSalmon;
                            //celdaEstado.Style.ForeColor = Color.White;
                        }

                        if (item.NumberNotaCreditoDIAN != "") { NotaCreditoElectronica = true; }
                    }

                    Total = (Subtotal - Descuento);
                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;
                    //Total += (Subtotal - Descuento) + Impuesto;

                    lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                    lbl_subtotal.Text = SubtotalMenosImpuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                    lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    lbl_inc_bolsa.Text = incBolsa.ToString("N2", new CultureInfo("es-CO"));
                    lbl_inc.Text = inc.ToString("N2", new CultureInfo("es-CO"));
                    lbl_iva.Text = iva.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    lbl_cantidadProductos.Text = "_";
                    lbl_subtotal.Text = "_";
                    lbl_descuento.Text = "_";
                    lbl_impuesto.Text = "_";
                    lbl_total.Text = "_";

                    MessageBox.Show("No se encuentran datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se encuentran datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void cbx_client_venta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();
            if (cbx_client_venta.Text == "Nota credito")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo" });
            }
            else if (cbx_client_venta.Text == "Producto")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            }
            else if (cbx_client_venta.Text == "Usuario")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id" });
            }
            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaNotascredito();
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await ConsultaNotascredito();
            }
        }

        private async void btn_imprimir_Click(object sender, EventArgs e)
        {
            NotaCreditoElectronica = false;

            if (dtg_nota_credito.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de imprimir nota credito?",
                        "Confirmar cancelacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_nota_credito.SelectedRows.Count > 0)
                    {
                        var row = dtg_nota_credito.SelectedRows[0];
                        if (row.Cells["cl_id_nota_credito"].Value != null)
                        {
                            var Id_nota_credito = Convert.ToInt32(row.Cells["cl_id_nota_credito"].Value);
                            //Imprime Tirilla
                            var DataTienda = await _ITienda.List();
                            if (DataTienda.Data != null)
                            {
                                if (DataTienda.Data.Count > 0)
                                {
                                    var DataNotaCr = await _INotaCredito.List(Id_nota_credito);

                                    NotaCreditoPOSEntitie DataNotaCredito = new NotaCreditoPOSEntitie();

                                    DataNotaCredito.NumeroNotaCredito = DataNotaCr.Data![0].NumberNotaCreditoDIAN == "" ? DataNotaCr.Data![0].NotaCredito : DataNotaCr.Data![0].NumberNotaCreditoDIAN;
                                    DataNotaCredito.NumeroFactura = DataNotaCr.Data![0].Factura;
                                    DataNotaCredito.Fecha = DataNotaCr.Data[0].CreationDate;
                                    DataNotaCredito.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                                    DataNotaCredito.DireccionEmpresa = DataTienda.Data[0].Direccion;
                                    DataNotaCredito.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                                    DataNotaCredito.NIT = DataTienda.Data[0].NumeroDocumento;
                                    DataNotaCredito.UserNameNotaCredito = DataNotaCr.Data[0].IdUserActionNotaCredito + " - " + DataNotaCr.Data[0].UserName;
                                    DataNotaCredito.NombreCliente = "";

                                    DataNotaCredito.Estado = DataNotaCr.Data![0].EstadoNotaCreditoDIAN == "" ? DataNotaCr.Data[0].Estado : DataNotaCr.Data![0].EstadoNotaCreditoDIAN;

                                    Cantidad = 0;
                                    Subtotal = 0;
                                    Descuento = 0;
                                    Impuesto = 0;
                                    SubtotalLinea = 0;
                                    DescuentoLinea = 0;
                                    decimal iva = 0;
                                    decimal inc = 0;
                                    decimal incBolsa = 0;

                                    foreach (var item in DataNotaCr.Data)
                                    {
                                        Cantidad += Convert.ToDecimal(item.Cantidad);
                                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                                        if (item.NombreTributo == "INC Bolsas")
                                        {
                                            incBolsa += Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO"));
                                        }
                                        else if (item.NombreTributo == "IVA")
                                        {
                                            iva += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                        }
                                        else if (item.NombreTributo == "INC")
                                        {
                                            inc += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")));
                                        }
                                    }

                                    Impuesto = iva + inc + incBolsa;

                                    Total = (Subtotal - Descuento);
                                    //Total = (Subtotal - Descuento) + Impuesto;
                                    decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                                    DataNotaCredito.CantidadTotal = Cantidad;
                                    DataNotaCredito.Subtotal = SubtotalMenosImpuesto;
                                    DataNotaCredito.Descuento = Descuento;
                                    DataNotaCredito.Impuesto = Impuesto;
                                    DataNotaCredito.iva = iva;
                                    DataNotaCredito.inc = inc;
                                    DataNotaCredito.incBolsa = incBolsa;
                                    DataNotaCredito.Total = Total;

                                    List<ItemFacturaEntitie> ListItemFacturaEntitie = new List<ItemFacturaEntitie>();

                                    decimal precio;
                                    decimal cantidad;
                                    decimal desc;
                                    decimal total;

                                    foreach (var item in DataNotaCr.Data)
                                    {
                                        precio = Convert.ToDecimal(item.PrecioUnitario);
                                        cantidad = Convert.ToDecimal(item.Cantidad);
                                        desc = Convert.ToDecimal(item.Descuento);

                                        total = CalcularTotal(precio, 0, desc);
                                        total = total * cantidad;

                                        string UnidadMedidaAbreviada;

                                        switch (item.UnidadMedida)
                                        {
                                            case "Unidad":
                                                UnidadMedidaAbreviada = "Und";
                                                break;
                                            case "kilogramo":
                                                UnidadMedidaAbreviada = "KGM";
                                                break;
                                            case "libra":
                                                UnidadMedidaAbreviada = "LBR";
                                                break;
                                            case "metro":
                                                UnidadMedidaAbreviada = "MTR";
                                                break;
                                            case "galón":
                                                UnidadMedidaAbreviada = "GLL";
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

                                    DataNotaCredito.Items = ListItemFacturaEntitie;

                                    var DataParametros = await _IParametros.List("");

                                    if (DataParametros.Data != null)
                                    {
                                        if (DataParametros.Data.Count > 0)
                                        {
                                            int ANCHO_TIRILLA = 0;
                                            string Impresora = "";
                                            string MensajeFinalTirilla = "";
                                            int LineasAbajo = 0;
                                            foreach (var itemParametros in DataParametros.Data)
                                            {
                                                switch (itemParametros.Nombre)
                                                {
                                                    case "Ancho tirilla":
                                                        ANCHO_TIRILLA = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    case "Impresora":
                                                        Impresora = itemParametros.Value;
                                                        break;
                                                    case "Mensaje final tirilla":
                                                        MensajeFinalTirilla = itemParametros.Value;
                                                        break;
                                                    case "lineas abajo de la tirilla":
                                                        LineasAbajo = Convert.ToInt32(itemParametros.Value);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            if (DataNotaCr.Data![0].NumberNotaCreditoDIAN != "") { NotaCreditoElectronica = true; }
                                            DataNotaCredito.NotaCreditoElectronica = NotaCreditoElectronica;
                                            DataNotaCredito.NotaCreditoJSON = !string.IsNullOrEmpty(DataNotaCr.Data![0].NotaCreditoJSON) ? DataNotaCr.Data![0].NotaCreditoJSON : "";

                                            StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaNotaCredito(DataNotaCredito, ANCHO_TIRILLA, MensajeFinalTirilla, false);

                                            string carpetaNotasCredito = "NotasCredito";
                                            if (!Directory.Exists(carpetaNotasCredito))
                                            {
                                                Directory.CreateDirectory(carpetaNotasCredito);
                                            }

                                            File.WriteAllText(Path.Combine(carpetaNotasCredito, $"nota_credito_{DataNotaCredito.NumeroNotaCredito}.txt"),
                                                                      tirilla.ToString(),
                                                                      Encoding.UTF8);

                                            if (NotaCreditoElectronica)
                                            {
                                                dynamic datosNotaCreditoElectronica = JsonConvert.DeserializeObject<dynamic>(DataNotaCr.Data![0].NotaCreditoJSON);

                                                GuardarTirillaComoImagen(tirilla, datosNotaCreditoElectronica!.data.credit_note.qr_image.ToString(), DataNotaCredito.NumeroNotaCredito);

                                                RawPrinterHelper.SendStringToPrinterConQr(Impresora, tirilla.ToString(), LineasAbajo, datosNotaCreditoElectronica!.data.credit_note.qr_image.ToString());
                                            }
                                            else
                                            {
                                                RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString(), LineasAbajo);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        public static void GuardarTirillaComoImagen(StringBuilder tirilla, string base64QR, string numeroNotaCredito)
        {
            // Configuración de la "impresora"
            int anchoPx = 384; // 58mm típico
            int altoEstimado = 2000; // Estimado; puedes ajustar según necesidad

            Bitmap bmp = new Bitmap(anchoPx, altoEstimado);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                Font fuente = new Font("Consolas", 10);
                Brush pincel = Brushes.Black;
                float y = 0;

                // Dibujar texto línea por línea
                foreach (string linea in tirilla.ToString().Split('\n'))
                {
                    g.DrawString(linea.TrimEnd(), fuente, pincel, new PointF(0, y));
                    y += fuente.GetHeight();
                }

                // Insertar QR (si viene)
                if (!string.IsNullOrWhiteSpace(base64QR))
                {
                    try
                    {
                        string base64QRLimpia = base64QR
                        .Replace("data:image/png;base64,", "")
                        .Replace("\r", "")
                        .Replace("\n", "")
                        .Trim();

                        byte[] qrBytes = Convert.FromBase64String(base64QRLimpia);
                        using (MemoryStream ms = new MemoryStream(qrBytes))
                        using (Image qrImage = Image.FromStream(ms))
                        {
                            int qrSize = 100; // Tamaño del QR en px
                            g.DrawImage(qrImage, new Rectangle((anchoPx - qrSize) / 2, (int)y + 10, qrSize, qrSize));
                            y += qrSize + 20; // Dejar espacio abajo
                        }
                    }
                    catch
                    {
                        g.DrawString("[Error al cargar QR]", fuente, Brushes.Red, new PointF(0, y));
                    }
                }
            }

            // Recortar la imagen al alto usado
            Rectangle crop = new Rectangle(0, 0, anchoPx, (int)Math.Ceiling((double)altoEstimado));
            Bitmap tirillaFinal = bmp.Clone(crop, bmp.PixelFormat);

            // Guardar como imagen
            string carpeta = "NotasCredito";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, $"nota_credito_{numeroNotaCredito}.png");
            tirillaFinal.Save(ruta, ImageFormat.Png);
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarNotaCredito = _serviceProvider.GetRequiredService<AgregarNotaCredito>();
                _AgregarNotaCredito.Permisos = _Permisos;
                _AgregarNotaCredito.Id_Venta = 0;
                _AgregarNotaCredito.FormClosed += (s, args) => _AgregarNotaCredito = null;
                _AgregarNotaCredito.ShowDialog();
            }
        }

        private void dtg_nota_credito_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_nota_credito.Rows.Count > 0)
            {
                if (dtg_nota_credito.SelectedRows.Count > 0)
                {
                    if (_Permisos != null)
                    {
                        _AgregarNotaCredito = _serviceProvider.GetRequiredService<AgregarNotaCredito>();
                        _AgregarNotaCredito.Permisos = _Permisos;
                        foreach (DataGridViewRow rows in dtg_nota_credito.SelectedRows)
                        {
                            _AgregarNotaCredito.Id_Venta = Convert.ToInt32(rows.Cells["cl_id_venta"].Value);
                        }
                        _AgregarNotaCredito.FormClosed += (s, args) => _AgregarNotaCredito = null;
                        _AgregarNotaCredito.ShowDialog();
                    }
                }
            }
        }

        private void dtg_nota_credito_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dtg_nota_credito.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    dtg_nota_credito.ClearSelection();
                    dtg_nota_credito.Rows[hit.RowIndex].Selected = true;

                    dtg_nota_credito.CurrentCell = dtg_nota_credito.Rows[hit.RowIndex].Cells[0];

                    var row = dtg_nota_credito.SelectedRows[0];
                    if (row.Cells["cl_estado"].Value.ToString() == "PENDIENTE EMITIR")
                    {
                        contextMenuStrip1.Show(dtg_nota_credito, e.Location);
                    }
                }
            }
        }

        private async void emitirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            panel1.Enabled = false;

            try
            {
                var respDoc = await _IRangoNumeracion.IdentificaDocumento(22);

                if (respDoc.Data != null)
                {
                    if (respDoc.Data.Count > 0)
                    {
                        int IdRangoDIAN = Convert.ToInt32(respDoc.Data[0].IdRangoDIAN);
                        long NumDesde = respDoc.Data[0].NumeroDesde;
                        long NumHasta = respDoc.Data[0].NumeroHasta;
                        string NumeroResolucion = respDoc.Data[0].NumeroResolucion.ToString();
                        string ClaveTecnica = respDoc.Data[0].ClaveTecnica.ToString();
                        int IsActive = Convert.ToInt32(respDoc.Data[0].Active);

                        bool NotaCreditoElectronica = false;
                        
                        var row = dtg_nota_credito.SelectedRows[0];
                        int IdVenta = Convert.ToInt32(row.Cells["cl_id_venta"].Value);
                        int IdNotaCredito = Convert.ToInt32(row.Cells["cl_id_nota_credito"].Value);

                        var resp = await _IVenta.List(IdVenta);
                        if (resp.Data != null)
                        {
                            if (resp.Data.Count > 0)
                            {
                                FacturaJSON = resp.Data[0].FacturaJSON;
                                if (resp.Data[0].NumberFacturaDIAN != "") { FacturaElectronica = true; } else { FacturaElectronica = false; }
                                IdIdentificationType = Convert.ToInt32(resp.Data[0].IdIdentificationType);
                                MotivoNotaCredito = resp.Data[0].MotivoNotaCredito.ToString();

                                if (Convert.ToInt32(respDoc.Data[0].DocElectronico) == 1) 
                                { 
                                    NotaCreditoElectronica = true;
                                }

                                if (FacturaElectronica == true && NotaCreditoElectronica == false)
                                {
                                    MessageBox.Show($"Dado que la factura a anular es electrónica, es obligatorio que la nota crédito también sea electrónica. El proceso ha sido cancelado y no se generará la nota crédito.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie =
                                new ActualizarNotaCreditoForNotaCreditoElectronicaEntitie();

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

                                            if (RespAuth != null)
                                            {
                                                if (RespAuth.Flag && RespAuth.Data!.access_token != "")
                                                {
                                                    string Token = RespAuth.Data!.access_token.ToString();

                                                    string UrlRangos = ConfigurationManager.AppSettings["UrlRangosGET"]!;

                                                    RangosEntitie rangosEntitie = new RangosEntitie
                                                    {
                                                        Id = IdRangoDIAN,
                                                        document = 22,
                                                        resolution_number = NumeroResolucion,
                                                        technical_key = ClaveTecnica,
                                                        is_active = IsActive
                                                    };

                                                    var respGetRangos = _IRangoNumeracionFE.ConsultaRangoDIAN(Token, UrlRangos, rangosEntitie);

                                                    if (respGetRangos != null)
                                                    {
                                                        if (respGetRangos.Flag)
                                                        {
                                                            long current = Convert.ToInt64(respGetRangos.Data!.data[0].current);

                                                            if (current <= NumHasta)
                                                            {
                                                                DateTime FechaExpedicion = DateTime.Now;
                                                                DateTime FechaVencimiento = DateTime.Now;

                                                                FechaExpedicion = Convert.ToDateTime(respDoc.Data[0].FechaExpedicion);
                                                                FechaVencimiento = Convert.ToDateTime(respDoc.Data[0].FechaVencimiento);

                                                                DateTime FechaActual = DateTime.Now;
                                                                FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                                                                if (FechaVencimiento >= FechaActual)
                                                                {
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
                                                                            reference_code = IdNotaCredito.ToString(),
                                                                            send_email = resultado.data.bill.send_email == 0 ? false : true,
                                                                            observation = MotivoNotaCredito,
                                                                            payment_method_code = resultado.data.bill.payment_method.code,
                                                                            customer = customer,
                                                                            items = Listitems
                                                                        };

                                                                        string UrlCrearValidarNotaCredito = ConfigurationManager.AppSettings["UrlCrearValidarNotaCreditoPOST"]!;

                                                                        var responseNotaCreditoElectronica = _INotasCreditoElectronica.CreaValidaNotaCredito(Token, UrlCrearValidarNotaCredito, notaCreditoRequest);

                                                                        if (!responseNotaCreditoElectronica.Flag)
                                                                        {
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
                                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";

                                                                            MessageBox.Show($"Error en Emicion de nota credito electronica: {responseNotaCreditoElectronica?.Data} - {responseNotaCreditoElectronica?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (responseNotaCreditoElectronica.Data != null)
                                                                            {
                                                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
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
                                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
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
                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
                                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                            //Actualizar informacion de nota credito electronica en base de datos
                                                            var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                            if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                            {
                                                                MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            }

                                                            MessageBox.Show($"Error en consulta de consecutivo rango de numeracion, no se emitira nota credito electronica: {RespAuth?.Data} - {RespAuth?.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            this.Close();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
                                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                        //Actualizar informacion de nota credito electronica en base de datos
                                                        var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                        if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                        {
                                                            MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }

                                                        MessageBox.Show($"Error en consulta de consecutivo rango de numeracion, no se emitira nota credito electronica ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        this.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
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
                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
                                                actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN = "PENDIENTE EMITIR";
                                                //Actualizar informacion de nota credito electronica en base de datos
                                                var respActualizaDataNotaCreditoElectronica = await _INotaCredito.ActualizarDataNotaCreditoElectronica(actualizarNotaCreditoForNotaCreditoElectronicaEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                                if (!respActualizaDataNotaCreditoElectronica.Flag)
                                                {
                                                    MessageBox.Show($"Se presento un error al intentar actualizar informacion de nota credito electronica en base de datos, Error: {respActualizaDataNotaCreditoElectronica.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }

                                                MessageBox.Show($"Error en autenticacion, no se emitira nota credito electronica", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                this.Close();
                                            }
                                        }
                                        else
                                        {
                                            actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
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
                                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito = IdNotaCredito;
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
                                    MessageBox.Show($"Documento no es electronico", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
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
                    else
                    {
                        MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro informacion de rangos de numeracion, no se registrara la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panel1.Enabled = true;
            }
        }
    }
}
