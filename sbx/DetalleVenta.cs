using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.Venta;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class DetalleVenta : Form
    {
        private int _Id_Venta;
        private dynamic? _Permisos;
        private readonly IVenta _IVenta;
        private readonly ITienda _ITienda;
        private readonly IParametros _IParametros;
        private DetalleProdDevo? _DetalleProdDevo;
        private readonly IServiceProvider _serviceProvider;
        decimal Cantidad = 0;
        decimal Subtotal = 0;
        decimal SubtotalLinea = 0;
        decimal Descuento = 0;
        decimal Impuesto = 0;
        decimal Total = 0;
        decimal DescuentoLinea = 0;
        int IdNotaCredito = 0;
        private string _Origen;
        bool FacturaElectronica = false;
        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";

        public DetalleVenta(IVenta venta, ITienda tienda, IParametros iParametros, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IVenta = venta;
            _ITienda = tienda;
            _IParametros = iParametros;
            _serviceProvider = serviceProvider;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Venta
        {
            get => _Id_Venta;
            set => _Id_Venta = value;
        }

        public string Origen
        {
            get => _Origen;
            set => _Origen = value;
        }

        private async void DetalleVenta_Load(object sender, EventArgs e)
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
                        case "ventas":
                            btn_imprimir.Enabled = item.ToRead == 1 ? true : false;
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
            BuscarPor = "";
            ModoRedondeo = "N/A";
            MultiploRendondeo = "50";

            var DataParametros = await _IParametros.List("");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    foreach (var itemParametros in DataParametros.Data)
                    {
                        switch (itemParametros.Nombre)
                        {
                            case "Buscar en venta por":
                                BuscarPor = itemParametros.Value;
                                break;
                            case "Modo Redondeo":
                                ModoRedondeo = itemParametros.Value;
                                break;
                            case "Multiplo Rendondeo":
                                MultiploRendondeo = itemParametros.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            if (Id_Venta > 0)
            {
                var resp = await _IVenta.List(Id_Venta);

                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        lbl_factura.Text = resp.Data[0].NumberFacturaDIAN == "" ? resp.Data[0].Factura: resp.Data[0].NumberFacturaDIAN;
                        lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                        lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                        lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                        lbl_referencia.Text = resp.Data[0].Referencia;
                        lbl_banco.Text = resp.Data[0].NombreBanco;
                        lbl_estado.Text = resp.Data[0].EstadoFacturaDIAN != "" ? resp.Data[0].Estado == "FACTURADA" ? resp.Data[0].EstadoFacturaDIAN : resp.Data[0].Estado : resp.Data[0].Estado;
                        lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;

                        if (resp.Data[0].IdNotaCredito > 0) 
                        {
                            lbl_nota_credito.Text = resp.Data[0].NumberNotaCreditoDIAN == "0" ? resp.Data[0].NotaCredito : resp.Data[0].NumberNotaCreditoDIAN;
                            IdNotaCredito = resp.Data[0].IdNotaCredito;
                            if (Origen != "Inventario") { btn_ver_productos.Enabled = true; }   
                        }

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
                            cantidadTotal += Convert.ToDecimal(item.Cantidad);
                            Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                            if (item.NombreTributo == "INC Bolsas")
                            {
                                Impuesto += Convert.ToDecimal(item.Impuesto);
                                ImpuestoLinea = Convert.ToDecimal(item.Impuesto);
                            }
                            else
                            {
                                Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                            }

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

                            //TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;
                            TotalLinea = (SubtotalLinea - DescuentoLinea);

                            dtg_ventas.Rows.Add(
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.NombreProducto,
                                item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                                item.NombreTributo,
                                Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                                TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                        }

                        //Total += (Subtotal - Descuento) + Impuesto;
                        Total += (Subtotal - Descuento);
                        decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

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
                MessageBox.Show("No se encontro id venta a consultar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            if (ModoRedondeo != "N/A")
            {
                var valorRendondeado = Redondear(ValorDescuento, Convert.ToInt32(MultiploRendondeo));
                ValorDescuento = valorRendondeado;
            }

            return ValorDescuento;
        }

        decimal Redondear(decimal valor, int multiplo)
        {
            decimal valorRendondeado = 0;

            switch (ModoRedondeo)
            {
                case "Hacia arriba":
                    valorRendondeado = (decimal)(Math.Ceiling((decimal)valor / multiplo) * multiplo);
                    break;
                case "Hacia abajo":
                    valorRendondeado = (decimal)(Math.Floor((decimal)valor / multiplo) * multiplo);
                    break;
                case "Hacia arriba o hacia abajo":
                    valorRendondeado = (decimal)(Math.Round((decimal)valor / multiplo) * multiplo);
                    break;

                default:
                    break;
            }

            return valorRendondeado;
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

        private async void btn_imprimir_Click(object sender, EventArgs e)
        {
            FacturaElectronica = false;

            if (dtg_ventas.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de imprimir la factura?",
                        "Confirmar cancelacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Imprime Tirilla
                    var DataTienda = await _ITienda.List();
                    if (DataTienda.Data != null)
                    {
                        if (DataTienda.Data.Count > 0)
                        {
                            var DataVenta = await _IVenta.List(Id_Venta);

                            FacturaPOSEntitie DataFactura = new FacturaPOSEntitie();

                            DataFactura.NumeroFactura = DataVenta.Data![0].NumberFacturaDIAN == "" ? DataVenta.Data![0].Factura : DataVenta.Data![0].NumberFacturaDIAN;
                            DataFactura.Fecha = DataVenta.Data[0].FechaFactura;
                            DataFactura.NombreEmpresa = DataTienda.Data[0].NombreRazonSocial;
                            DataFactura.DireccionEmpresa = DataTienda.Data[0].Direccion;
                            DataFactura.TelefonoEmpresa = DataTienda.Data[0].Telefono;
                            DataFactura.NIT = DataTienda.Data[0].NumeroDocumento;
                            DataFactura.UserNameFactura = DataVenta.Data[0].IdUserActionFactura + " - " + DataVenta.Data[0].UserNameFactura;
                            DataFactura.NombreCliente = DataVenta.Data[0].NumeroDocumento + " - " + DataVenta.Data[0].NombreRazonSocial;
                            DataFactura.NombreVendedor = DataVenta.Data[0].IdVendedor + " - " + DataVenta.Data[0].NombreCompletoVendedor;
                            DataFactura.Estado = DataVenta.Data[0].EstadoFacturaDIAN == "" ? DataVenta.Data[0].Estado : DataVenta.Data[0].EstadoFacturaDIAN;
                            DataFactura.FormaPago = DataVenta.Data[0].NombreMetodoPago;
                            DataFactura.Recibido = DataVenta.Data[0].Recibido;

                            Cantidad = 0;
                            Subtotal = 0;
                            Descuento = 0;
                            Impuesto = 0;
                            SubtotalLinea = 0;
                            DescuentoLinea = 0;
                            decimal iva = 0;
                            decimal inc = 0;
                            decimal incBolsa = 0;

                            foreach (var item in DataVenta.Data)
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

                            //Total = (Subtotal - Descuento) + Impuesto;
                            Total = (Subtotal - Descuento);
                            decimal SubtotalMenosImpuesto = Subtotal - Impuesto;

                            DataFactura.CantidadTotal = Cantidad;
                            DataFactura.Subtotal = SubtotalMenosImpuesto;
                            DataFactura.Descuento = Descuento;
                            DataFactura.Impuesto = Impuesto;
                            DataFactura.iva = iva;
                            DataFactura.inc = inc;
                            DataFactura.incBolsa = incBolsa;
                            DataFactura.Total = Total;
                            DataFactura.Cambio = DataFactura.Recibido - Total;

                            List<ItemFacturaEntitie> ListItemFacturaEntitie = new List<ItemFacturaEntitie>();

                            decimal precio;
                            decimal cantidad;
                            decimal desc;
                            decimal total;

                            foreach (var item in DataVenta.Data)
                            {
                                precio = Convert.ToDecimal(item.PrecioUnitario);
                                cantidad = Convert.ToDecimal(item.Cantidad);
                                desc = Convert.ToDecimal(item.Descuento);
                                iva = Convert.ToDecimal(item.Impuesto);

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

                            DataFactura.Items = ListItemFacturaEntitie;

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

                                    if (DataVenta.Data![0].NumberFacturaDIAN != "") { FacturaElectronica = true; }

                                    DataFactura.FacturaElectronica = FacturaElectronica;
                                    DataFactura.FacturaJSON = !string.IsNullOrEmpty(DataVenta.Data![0].FacturaJSON) ? DataVenta.Data![0].FacturaJSON : "";

                                    StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaFactura(DataFactura, ANCHO_TIRILLA, MensajeFinalTirilla,false);

                                    string carpetaFacturas = "Facturas";
                                    if (!Directory.Exists(carpetaFacturas))
                                    {
                                        Directory.CreateDirectory(carpetaFacturas);
                                    }

                                    File.WriteAllText(Path.Combine(carpetaFacturas, $"factura_{DataFactura.NumeroFactura}.txt"),
                                                              tirilla.ToString(),
                                                              Encoding.UTF8);

                                    if (DataVenta.Data![0].NumberFacturaDIAN != "") { FacturaElectronica = true; }

                                    if (FacturaElectronica)
                                    {
                                        dynamic datosFacturaElectronica = JsonConvert.DeserializeObject<dynamic>(DataVenta.Data[0].FacturaJSON);

                                        string qr_img = datosFacturaElectronica!.data.bill.qr_image;

                                        GuardarTirillaComoImagen(tirilla, qr_img, DataFactura.NumeroFactura);

                                        RawPrinterHelper.SendStringToPrinterConQr(Impresora, tirilla.ToString(), LineasAbajo, qr_img);
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

        private void btn_ver_productos_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _DetalleProdDevo = _serviceProvider.GetRequiredService<DetalleProdDevo>();
                _DetalleProdDevo.Id_NotaCredito = IdNotaCredito;
                _DetalleProdDevo.Permisos = _Permisos;
                _DetalleProdDevo.Origen = "DetalleVenta";
                _DetalleProdDevo.FormClosed += (s, args) => _DetalleProdDevo = null;
                _DetalleProdDevo.ShowDialog();
            }
        }

        public static void GuardarTirillaComoImagen(StringBuilder tirilla, string base64QR, string numeroFactura)
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
            string carpeta = "Facturas";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, $"factura_{numeroFactura}.png");
            tirillaFinal.Save(ruta, ImageFormat.Png);
        }
    }
}
