using Newtonsoft.Json;
using sbx.core.Entities.Caja;
using sbx.core.Entities.Cotizacion;
using sbx.core.Entities.NotaCredito;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace sbx.core.Entities.Venta
{
    public class GenerarTirillaPOS
    {
        private static int ANCHO_TIRILLA = 0;

        public static StringBuilder GenerarTirillaFactura(FacturaPOSEntitie factura, int Pr_ANCHO_TIRILLA, string MensajeFinalTirilla, bool OpenCajon)
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA == 58 ? 32 : Pr_ANCHO_TIRILLA == 80 ? 42 : 0;

            //Abrir cajon
            if (OpenCajon)
            {
                sb.Append("\x1B" + "p" + "\x00" + "\x0F" + "\x96");
            }

            if (factura.FacturaElectronica) 
            {
                // Informacion empresa
                sb.AppendLine(CentrarTexto(factura.NombreEmpresa));
                sb.AppendLine(CentrarTextoLargo(factura.DireccionEmpresa));
                sb.AppendLine(CentrarTexto($"Tel: {factura.TelefonoEmpresa}"));
                sb.AppendLine(CentrarTexto($"NIT: {factura.NIT}"));
                sb.AppendLine(CentrarTexto($"FACTURA ELECTRÓNICA DE VENTA"));
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Información de la factura
                sb.AppendLine($"FACTURA: {factura.NumeroFactura}");
                sb.AppendLine($"FECHA: {factura.Fecha:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"CAJERO: {factura.UserNameFactura}");
                var LineasVendedor = DividirTexto($"VENDEDOR: {factura.NombreVendedor}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasVendedor)
                {
                    sb.AppendLine($"{linea}");
                }

                //Informacion cliente
                var LineasCliente = DividirTexto($"CLIENTE: {factura.NombreCliente}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasCliente)
                {
                    sb.AppendLine($"{linea}");
                }
                if (factura.Estado == "ANULADA")
                {
                    sb.AppendLine($"ESTADO: {factura.Estado}");
                }
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Informacion de Items
                if (ANCHO_TIRILLA == 42)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in factura.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(lineaCantidad);
                    }
                }
                else if (ANCHO_TIRILLA == 32)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|");
                    sb.AppendLine("DESC% |TOTAL         |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in factura.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 6 ? item.Descuento.ToString().Substring(0, 6) : item.Descuento.ToString().PadRight(6);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 14 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 14) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(14);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var linea2 = $"{cantidad}|{unidadMedida}|{PrecioUnitario}";
                        sb.AppendLine(linea2);
                        var linea3 = $"{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(linea3);
                    }
                }

                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Totales
                sb.AppendLine($"{"SUBTOTAL:",-20} {factura.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"DESCUENTO:",-20} {factura.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"IVA:",-20} {factura.iva.ToString("N0", new CultureInfo("es-CO"))}");
                if (factura.inc > 0) 
                {
                    sb.AppendLine($"{"INC:",-20} {factura.inc.ToString("N0", new CultureInfo("es-CO"))}");
                }
                if (factura.incBolsa > 0)
                {
                    sb.AppendLine($"{"INC Bolsa:",-20} {factura.incBolsa.ToString("N0", new CultureInfo("es-CO"))}");
                }

                sb.AppendLine($"{"TOTAL:",-20} {factura.Total.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Forma de pago
                sb.AppendLine($"FORMA DE PAGO: {factura.FormaPago}");
                if (factura.FormaPago.ToUpper() == "EFECTIVO")
                {
                    sb.AppendLine($"{"RECIBIDO:",-20} {factura.Recibido.ToString("N0", new CultureInfo("es-CO"))}");
                    sb.AppendLine($"{"CAMBIO:",-20} {factura.Cambio.ToString("N0", new CultureInfo("es-CO"))}");
                }

                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Pie de página
                sb.AppendLine();

                dynamic datosFacturaElectronica = JsonConvert.DeserializeObject<dynamic>(factura.FacturaJSON);
                if (ANCHO_TIRILLA == 42)
                {
                    sb.AppendLine(CentrarTexto("Autorización numeración de facturación"));
                    sb.AppendLine(CentrarTexto($"DIAN {datosFacturaElectronica!.data.numbering_range.resolution_number} - Prefijo: {datosFacturaElectronica!.data.numbering_range.prefix}"));
                    sb.AppendLine(CentrarTexto($"Habilita rangos de:{datosFacturaElectronica!.data.numbering_range.from.ToString()}"));
                    sb.AppendLine(CentrarTexto($"hasta: {datosFacturaElectronica!.data.numbering_range.to.ToString()}"));
                    sb.AppendLine(CentrarTexto($"Fecha desde: {datosFacturaElectronica!.data.numbering_range.start_date.ToString()}"));
                    sb.AppendLine(CentrarTexto($"hasta: {datosFacturaElectronica!.data.numbering_range.end_date.ToString()}"));
                    sb.AppendLine(CentrarTexto($"Vigencia {datosFacturaElectronica!.data.numbering_range.months} meses."));
                    sb.AppendLine();
                    sb.AppendLine(CentrarTexto("Fecha y hora de Generación:"));
                    sb.AppendLine(CentrarTexto(datosFacturaElectronica!.data.bill.validated.ToString()));
                }
                else if (ANCHO_TIRILLA == 32) 
                {
                    sb.AppendLine(CentrarTexto("Autorización de numeración"));
                    sb.AppendLine(CentrarTexto("de facturación"));
                    sb.AppendLine(CentrarTexto($"DIAN {datosFacturaElectronica!.data.numbering_range.resolution_number}"));
                    sb.AppendLine(CentrarTexto($"Prefijo: {datosFacturaElectronica!.data.numbering_range.prefix}"));
                    sb.AppendLine(CentrarTexto($"Habilita rangos de:"));
                    sb.AppendLine(CentrarTexto(datosFacturaElectronica!.data.numbering_range.from.ToString()));
                    sb.AppendLine(CentrarTexto($"hasta:"));
                    sb.AppendLine(CentrarTexto(datosFacturaElectronica!.data.numbering_range.to.ToString()));
                    sb.AppendLine(CentrarTexto($"Fecha desde:"));
                    sb.AppendLine(CentrarTexto(datosFacturaElectronica!.data.numbering_range.start_date.ToString()));
                    sb.AppendLine(CentrarTexto($"hasta:"));
                    sb.AppendLine(CentrarTexto(datosFacturaElectronica!.data.numbering_range.end_date.ToString()));
                    sb.AppendLine(CentrarTexto($"Vigencia {datosFacturaElectronica!.data.numbering_range.months} meses."));
                    sb.AppendLine();
                    sb.AppendLine("Fecha y hora de Generación:");
                    sb.AppendLine(datosFacturaElectronica!.data.bill.validated.ToString());                   
                }

                sb.AppendLine();

                var LineasCUFE = DividirTexto($"Código CUFE: {datosFacturaElectronica!.data.bill.cufe}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasCUFE)
                {
                    sb.AppendLine($"{linea}");
                }
                sb.AppendLine();

                sb.AppendLine(CentrarTexto("GRACIAS POR SU COMPRA"));
                sb.AppendLine(CentrarTexto(MensajeFinalTirilla));
                sb.AppendLine();

                sb.AppendLine($"Proveedor tecnológico: {datosFacturaElectronica!.data.company.company}");
                sb.AppendLine($"NIT: {datosFacturaElectronica!.data.company.nit}-{datosFacturaElectronica!.data.company.dv}");
                sb.AppendLine("Fabricante Software: ");
                sb.AppendLine("NIT: ");
                sb.AppendLine(CentrarTexto($"Sistema POS SBX - 313-745-0103"));
                sb.AppendLine(CentrarTexto($"www.sbx.com.co"));
            }
            else
            {
                // Encabezado
                sb.AppendLine(CentrarTexto(factura.NombreEmpresa));
                sb.AppendLine(CentrarTextoLargo(factura.DireccionEmpresa));
                sb.AppendLine(CentrarTexto($"Tel: {factura.TelefonoEmpresa}"));
                sb.AppendLine(CentrarTexto($"NIT: {factura.NIT}"));
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Información de la factura
                sb.AppendLine($"FACTURA: {factura.NumeroFactura}");
                sb.AppendLine($"FECHA: {factura.Fecha:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"CAJERO: {factura.UserNameFactura}");
                var LineasVendedor = DividirTexto($"VENDEDOR: {factura.NombreVendedor}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasVendedor)
                {
                    sb.AppendLine($"{linea}");
                }

                //Informacion cliente
                var LineasCliente = DividirTexto($"CLIENTE: {factura.NombreCliente}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasCliente)
                {
                    sb.AppendLine($"{linea}");
                }
                if (factura.Estado == "ANULADA")
                {
                    sb.AppendLine($"ESTADO: {factura.Estado}");
                }
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Items
                if (ANCHO_TIRILLA == 42)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in factura.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(lineaCantidad);
                    }
                }
                else if (ANCHO_TIRILLA == 32)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|");
                    sb.AppendLine("DESC% |TOTAL         |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in factura.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 6 ? item.Descuento.ToString().Substring(0, 6) : item.Descuento.ToString().PadRight(6);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 14 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 14) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(14);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var linea2 = $"{cantidad}|{unidadMedida}|{PrecioUnitario}";
                        sb.AppendLine(linea2);
                        var linea3 = $"{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(linea3);
                    }
                }

                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Totales
                sb.AppendLine($"{"SUBTOTAL:",-20} {factura.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"DESCUENTO:",-20} {factura.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"IVA:",-20} {factura.iva.ToString("N0", new CultureInfo("es-CO"))}");
                if (factura.inc > 0)
                {
                    sb.AppendLine($"{"INC:",-20} {factura.inc.ToString("N0", new CultureInfo("es-CO"))}");
                }
                if (factura.incBolsa > 0)
                {
                    sb.AppendLine($"{"INC Bolsa:",-20} {factura.incBolsa.ToString("N0", new CultureInfo("es-CO"))}");
                }
                sb.AppendLine($"{"TOTAL:",-20} {factura.Total.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Forma de pago
                sb.AppendLine($"FORMA DE PAGO: {factura.FormaPago}");
                if (factura.FormaPago.ToUpper() == "EFECTIVO")
                {
                    sb.AppendLine($"{"RECIBIDO:",-20} {factura.Recibido.ToString("N0", new CultureInfo("es-CO"))}");
                    sb.AppendLine($"{"CAMBIO:",-20} {factura.Cambio.ToString("N0", new CultureInfo("es-CO"))}");
                }

                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Pie de página
                sb.AppendLine(CentrarTexto("GRACIAS POR SU COMPRA"));
                sb.AppendLine(CentrarTexto(MensajeFinalTirilla));
                sb.AppendLine();
                sb.AppendLine(CentrarTexto($"Sistema POS SBX - 313-745-0103"));
                sb.AppendLine(CentrarTexto($"www.sbx.com.co"));
            }

            return sb;
        }

        public static Bitmap Base64ToBitmap(string base64Image)
        {
            var base64 = base64Image.Substring(base64Image.IndexOf(",") + 1); // Remueve encabezado "data:image/png;base64,"
            byte[] imageBytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms); //se deja la advertencia por que el software solo se usara en sistema operativo windows
            }
        }

        public static StringBuilder GenerarTirillaCotizacion(CotizacionPOSEntitie cotizacion, int Pr_ANCHO_TIRILLA, string MensajeFinalTirilla)
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA == 58 ? 32 : Pr_ANCHO_TIRILLA == 80 ? 42 : 0;

            // Encabezado
            sb.AppendLine(CentrarTexto(cotizacion.NombreEmpresa));
            sb.AppendLine(CentrarTextoLargo(cotizacion.DireccionEmpresa));
            sb.AppendLine(CentrarTexto($"Tel: {cotizacion.TelefonoEmpresa}"));
            sb.AppendLine(CentrarTexto($"NIT: {cotizacion.NIT}"));
            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Información de la cotizacion
            sb.AppendLine($"COTIZACION: {cotizacion.NumeroCotizacion}");
            sb.AppendLine($"FECHA: {cotizacion.Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"CAJERO: {cotizacion.UserNameCotizacion}");
            var LineasVendedor = DividirTexto($"VENDEDOR: {cotizacion.NombreVendedor}", ANCHO_TIRILLA - 1);
            foreach (var linea in LineasVendedor)
            {
                sb.AppendLine($"{linea}");
            }
            var LineasCliente = DividirTexto($"CLIENTE: {cotizacion.NombreCliente}", ANCHO_TIRILLA - 1);
            foreach (var linea in LineasCliente)
            {
                sb.AppendLine($"{linea}");
            }
            sb.AppendLine($"ESTADO: {cotizacion.Estado}");
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Items
            if (ANCHO_TIRILLA == 42)
            {
                sb.AppendLine("ITEM - DESCRIPCION");
                sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IVA");
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                foreach (var item in cotizacion.Items)
                {
                    // Descripción (puede ocupar varias líneas)
                    var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                    foreach (var linea in descripcionLineas)
                    {
                        sb.AppendLine($"{linea}");
                    }

                    // Línea con cantidad y precio
                    string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                    string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                    string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                    string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                    string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                    string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                    var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                    sb.AppendLine(lineaCantidad);
                }
            }
            else if (ANCHO_TIRILLA == 32)
            {
                sb.AppendLine("ITEM - DESCRIPCION");
                sb.AppendLine("CANT  |U.M|PRECIO_UNI|");
                sb.AppendLine("DESC% |TOTAL         |IVA");
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                foreach (var item in cotizacion.Items)
                {
                    // Descripción (puede ocupar varias líneas)
                    var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                    foreach (var linea in descripcionLineas)
                    {
                        sb.AppendLine($"{linea}");
                    }

                    // Línea con cantidad y precio
                    string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                    string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                    string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                    string Descuento = item.Descuento.ToString().Length > 6 ? item.Descuento.ToString().Substring(0, 6) : item.Descuento.ToString().PadRight(6);
                    string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 14 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 14) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(14);
                    string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                    var linea2 = $"{cantidad}|{unidadMedida}|{PrecioUnitario}";
                    sb.AppendLine(linea2);
                    var linea3 = $"{Descuento}|{Total}|{impuesto}";
                    sb.AppendLine(linea3);
                }
            }

            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Totales
            sb.AppendLine($"{"SUBTOTAL:",-20} {cotizacion.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"DESCUENTO:",-20} {cotizacion.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"IVA:",-20} {cotizacion.Impuesto.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"TOTAL:",-20} {cotizacion.Total.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Pie de página
            sb.AppendLine(CentrarTexto($"VALIDEZ DE LA COTIZACION {cotizacion.DiasVencimiento} DIAS."));
            sb.AppendLine(MensajeFinalTirilla);
            sb.AppendLine(CentrarTexto($"Sistema POS SBX - 313-745-0103"));
            sb.AppendLine(CentrarTexto($"www.sbx.com.co"));

            return sb;
        }

        public static StringBuilder GenerarTirillaCajaCierre(CajaEntitie caja, int Pr_ANCHO_TIRILLA) 
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA == 58 ? 32 : Pr_ANCHO_TIRILLA == 80 ? 42 : 0;

            sb.AppendLine(CentrarTexto("CIERRE DE CAJA"));
            sb.AppendLine("");
            sb.AppendLine($"Estado: {caja.Estado}");
            sb.AppendLine($"Usuario: {caja.Usuario}");
            sb.AppendLine(new string('=', ANCHO_TIRILLA));
            sb.AppendLine($"Fecha apertura: {caja.FechaHoraApertura.ToString("yyyy-MM-dd")}");
            sb.AppendLine($"Hora apertura: {caja.FechaHoraApertura.ToString("HH:mm:ss")}");
            sb.AppendLine($"Fecha cierre: {caja.FechaHoraCierre.ToString("yyyy-MM-dd")}");
            sb.AppendLine($"Hora cierre: {caja.FechaHoraCierre.ToString("HH:mm:ss")}");
            sb.AppendLine($"Monto inicial: {caja.MontoInicialDeclarado.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"Total ventas: {caja.VentasTotales.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"Pagos en efectivo: {caja.PagosEnEfectivo.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"Monto final: {caja.MontoFinalDeclarado.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"Diferencia: {caja.Diferencia.ToString("N0", new CultureInfo("es-CO"))}");

            return sb;
        }

        public static StringBuilder GenerarTirillaNotaCredito(NotaCreditoPOSEntitie NotaCredito, int Pr_ANCHO_TIRILLA, string MensajeFinalTirilla, bool OpenCajon)
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA == 58 ? 32 : Pr_ANCHO_TIRILLA == 80 ? 42 : 0;

            //Abrir cajon
            if (OpenCajon)
            {
                sb.Append("\x1B" + "p" + "\x00" + "\x0F" + "\x96");
            }

            if (NotaCredito.NotaCreditoElectronica)
            {
                // Informacion empresa
                sb.AppendLine(CentrarTexto(NotaCredito.NombreEmpresa));
                sb.AppendLine(CentrarTextoLargo(NotaCredito.DireccionEmpresa));
                sb.AppendLine(CentrarTexto($"Tel: {NotaCredito.TelefonoEmpresa}"));
                sb.AppendLine(CentrarTexto($"NIT: {NotaCredito.NIT}"));
                sb.AppendLine(CentrarTexto($"NOTA CREDITO ELECTRÓNICA"));
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Información de nota credito
                sb.AppendLine($"NOTA CREDITO: {NotaCredito.NumeroNotaCredito}");
                sb.AppendLine($"FACTURA: {NotaCredito.NumeroFactura}");
                sb.AppendLine($"FECHA: {NotaCredito.Fecha:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"USUARIO: {NotaCredito.UserNameNotaCredito}");

                sb.AppendLine($"ESTADO: {NotaCredito.Estado}");
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Informacion de Items
                if (ANCHO_TIRILLA == 42)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in NotaCredito.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(lineaCantidad);
                    }
                }
                else if (ANCHO_TIRILLA == 32)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|");
                    sb.AppendLine("DESC% |TOTAL         |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in NotaCredito.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 6 ? item.Descuento.ToString().Substring(0, 6) : item.Descuento.ToString().PadRight(6);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 14 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 14) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(14);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var linea2 = $"{cantidad}|{unidadMedida}|{PrecioUnitario}";
                        sb.AppendLine(linea2);
                        var linea3 = $"{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(linea3);
                    }
                }

                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Totales
                sb.AppendLine($"{"SUBTOTAL:",-20} {NotaCredito.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"DESCUENTO:",-20} {NotaCredito.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"IVA:",-20} {NotaCredito.iva.ToString("N0", new CultureInfo("es-CO"))}");
                if (NotaCredito.inc > 0)
                {
                    sb.AppendLine($"{"INC:",-20} {NotaCredito.inc.ToString("N0", new CultureInfo("es-CO"))}");
                }
                if (NotaCredito.incBolsa > 0)
                {
                    sb.AppendLine($"{"INC Bolsa:",-20} {NotaCredito.incBolsa.ToString("N0", new CultureInfo("es-CO"))}");
                }
                sb.AppendLine($"{"TOTAL:",-20} {NotaCredito.Total.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Pie de página
                sb.AppendLine();

                dynamic datosNotaCreditoElectronica = JsonConvert.DeserializeObject<dynamic>(NotaCredito.NotaCreditoJSON);
                if (ANCHO_TIRILLA == 42)
                {
                    //sb.AppendLine(CentrarTexto("Autorización numeración de facturación"));
                    //sb.AppendLine(CentrarTexto($"DIAN {datosNotaCreditoElectronica!.data.numbering_range.resolution_number} - Prefijo: {datosNotaCreditoElectronica!.data.numbering_range.prefix}"));
                    //sb.AppendLine(CentrarTexto($"Habilita rangos de:{datosNotaCreditoElectronica!.data.numbering_range.from.ToString()}"));
                    //sb.AppendLine(CentrarTexto($"hasta: {datosNotaCreditoElectronica!.data.numbering_range.to.ToString()}"));
                    //sb.AppendLine(CentrarTexto($"Fecha desde: {datosNotaCreditoElectronica!.data.numbering_range.start_date.ToString()}"));
                    //sb.AppendLine(CentrarTexto($"hasta: {datosNotaCreditoElectronica!.data.numbering_range.end_date.ToString()}"));
                    //sb.AppendLine(CentrarTexto($"Vigencia {datosNotaCreditoElectronica!.data.numbering_range.months} meses."));
                    //sb.AppendLine();
                    sb.AppendLine(CentrarTexto("Fecha y hora de Generación:"));
                    sb.AppendLine(CentrarTexto(datosNotaCreditoElectronica!.data.credit_note.validated.ToString()));
                }
                else if (ANCHO_TIRILLA == 32)
                {
                    //sb.AppendLine(CentrarTexto("Autorización de numeración"));
                    //sb.AppendLine(CentrarTexto("de facturación"));
                    //sb.AppendLine(CentrarTexto($"DIAN {datosNotaCreditoElectronica!.data.numbering_range.resolution_number}"));
                    //sb.AppendLine(CentrarTexto($"Prefijo: {datosNotaCreditoElectronica!.data.numbering_range.prefix}"));
                    //sb.AppendLine(CentrarTexto($"Habilita rangos de:"));
                    //sb.AppendLine(CentrarTexto(datosNotaCreditoElectronica!.data.numbering_range.from.ToString()));
                    //sb.AppendLine(CentrarTexto($"hasta:"));
                    //sb.AppendLine(CentrarTexto(datosNotaCreditoElectronica!.data.numbering_range.to.ToString()));
                    //sb.AppendLine(CentrarTexto($"Fecha desde:"));
                    //sb.AppendLine(CentrarTexto(datosNotaCreditoElectronica!.data.numbering_range.start_date.ToString()));
                    //sb.AppendLine(CentrarTexto($"hasta:"));
                    //sb.AppendLine(CentrarTexto(datosNotaCreditoElectronica!.data.numbering_range.end_date.ToString()));
                    //sb.AppendLine(CentrarTexto($"Vigencia {datosNotaCreditoElectronica!.data.numbering_range.months} meses."));
                    //sb.AppendLine();
                    sb.AppendLine("Fecha y hora de Generación:");
                    sb.AppendLine(datosNotaCreditoElectronica!.data.credit_note.validated.ToString());
                }

                sb.AppendLine();

                var LineasCUFE = DividirTexto($"Código CUFE: {datosNotaCreditoElectronica!.data.credit_note.cufe}", ANCHO_TIRILLA - 1);
                foreach (var linea in LineasCUFE)
                {
                    sb.AppendLine($"{linea}");
                }
                sb.AppendLine();

                sb.AppendLine($"Proveedor tecnológico: {datosNotaCreditoElectronica!.data.company.company}");
                sb.AppendLine($"NIT: {datosNotaCreditoElectronica!.data.company.nit}-{datosNotaCreditoElectronica!.data.company.dv}");
                sb.AppendLine("Fabricante Software: ");
                sb.AppendLine("NIT: ");
                sb.AppendLine(CentrarTexto($"Sistema POS SBX - 313-745-0103"));
                sb.AppendLine(CentrarTexto($"www.sbx.com.co"));
            }
            else
            {
                // Encabezado
                sb.AppendLine(CentrarTexto(NotaCredito.NombreEmpresa));
                sb.AppendLine(CentrarTextoLargo(NotaCredito.DireccionEmpresa));
                sb.AppendLine(CentrarTexto($"Tel: {NotaCredito.TelefonoEmpresa}"));
                sb.AppendLine(CentrarTexto($"NIT: {NotaCredito.NIT}"));
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Información de nota credito
                sb.AppendLine($"NOTA CREDITO: {NotaCredito.NumeroNotaCredito}");
                sb.AppendLine($"FACTURA: {NotaCredito.NumeroFactura}");
                sb.AppendLine($"FECHA: {NotaCredito.Fecha:dd/MM/yyyy HH:mm}");
                sb.AppendLine($"USUARIO: {NotaCredito.UserNameNotaCredito}");

                sb.AppendLine($"ESTADO: {NotaCredito.Estado}");
                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Items
                if (ANCHO_TIRILLA == 42)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in NotaCredito.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(lineaCantidad);
                    }
                }
                else if (ANCHO_TIRILLA == 32)
                {
                    sb.AppendLine("ITEM - DESCRIPCION");
                    sb.AppendLine("CANT  |U.M|PRECIO_UNI|");
                    sb.AppendLine("DESC% |TOTAL         |IMP");
                    sb.AppendLine(new string('-', ANCHO_TIRILLA));

                    foreach (var item in NotaCredito.Items)
                    {
                        // Descripción (puede ocupar varias líneas)
                        var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 1);
                        foreach (var linea in descripcionLineas)
                        {
                            sb.AppendLine($"{linea}");
                        }

                        // Línea con cantidad y precio
                        string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                        string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3) : item.UnidadMedida.PadRight(3);
                        string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0, 10) : item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                        string Descuento = item.Descuento.ToString().Length > 6 ? item.Descuento.ToString().Substring(0, 6) : item.Descuento.ToString().PadRight(6);
                        string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 14 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 14) : item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(14);
                        string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                        var linea2 = $"{cantidad}|{unidadMedida}|{PrecioUnitario}";
                        sb.AppendLine(linea2);
                        var linea3 = $"{Descuento}|{Total}|{impuesto}";
                        sb.AppendLine(linea3);
                    }
                }

                sb.AppendLine(new string('-', ANCHO_TIRILLA));

                // Totales
                sb.AppendLine($"{"SUBTOTAL:",-20} {NotaCredito.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"DESCUENTO:",-20} {NotaCredito.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"IVA:",-20} {NotaCredito.iva.ToString("N0", new CultureInfo("es-CO"))}");
                if (NotaCredito.inc > 0)
                {
                    sb.AppendLine($"{"INC:",-20} {NotaCredito.inc.ToString("N0", new CultureInfo("es-CO"))}");
                }
                if (NotaCredito.incBolsa > 0)
                {
                    sb.AppendLine($"{"INC Bolsa:",-20} {NotaCredito.incBolsa.ToString("N0", new CultureInfo("es-CO"))}");
                }
                sb.AppendLine($"{"TOTAL:",-20} {NotaCredito.Total.ToString("N0", new CultureInfo("es-CO"))}");
                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                sb.AppendLine(new string('=', ANCHO_TIRILLA));

                // Pie de página              
                sb.AppendLine();
                sb.AppendLine(CentrarTexto($"Sistema POS SBX - 313-745-0103"));
                sb.AppendLine(CentrarTexto($"www.sbx.com.co"));
            }

            return sb;
        }

        private static string CentrarTexto(string texto)
        {
            if (texto.Length >= ANCHO_TIRILLA) return texto.Substring(0, ANCHO_TIRILLA);

            int espacios = (ANCHO_TIRILLA - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }

        private static string CentrarTextoLargo(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";

            List<string> lineas = new List<string>();

            // Dividir el texto en líneas si supera el ancho
            for (int i = 0; i < texto.Length; i += ANCHO_TIRILLA)
            {
                int longitudLinea = Math.Min(ANCHO_TIRILLA, texto.Length - i);
                string linea = texto.Substring(i, longitudLinea);
                lineas.Add(linea);
            }

            // Centrar cada línea
            List<string> lineasCentradas = new List<string>();
            foreach (string linea in lineas)
            {
                if (linea.Length >= ANCHO_TIRILLA)
                {
                    lineasCentradas.Add(linea);
                }
                else
                {
                    int espacios = (ANCHO_TIRILLA - linea.Length) / 2;
                    lineasCentradas.Add(new string(' ', espacios) + linea);
                }
            }

            // Unir todas las líneas con salto de línea
            return string.Join("\n", lineasCentradas);
        }

        private static List<string> DividirTexto(string texto, int maxAncho)
        {
            var lineas = new List<string>();

            while (texto.Length > maxAncho)
            {
                var corte = texto.LastIndexOf(' ', maxAncho);
                if (corte == -1) corte = maxAncho;

                lineas.Add(texto.Substring(0, corte));
                texto = texto.Substring(corte).TrimStart();
            }

            if (texto.Length > 0)
                lineas.Add(texto);

            return lineas;
        }
    }
}
