using sbx.core.Entities.Cotizacion;
using System.Globalization;
using System.Text;

namespace sbx.core.Entities.Venta
{
    public class GenerarTirillaPOS
    {
        private static int ANCHO_TIRILLA = 32;

        public static StringBuilder GenerarTirillaFactura(FacturaPOSEntitie factura, int Pr_ANCHO_TIRILLA, string MensajeFinalTirilla)
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA;

            // Encabezado
            sb.AppendLine(CentrarTexto(factura.NombreEmpresa));
            sb.AppendLine(CentrarTexto(factura.DireccionEmpresa));
            sb.AppendLine(CentrarTexto($"Tel: {factura.TelefonoEmpresa}"));
            sb.AppendLine(CentrarTexto($"NIT: {factura.NIT}"));
            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Información de la factura
            sb.AppendLine($"FACTURA: {factura.NumeroFactura}");
            sb.AppendLine($"FECHA: {factura.Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"CAJERO: {factura.UserNameFactura}");
            sb.AppendLine($"VENDEDOR: {factura.NombreVendedor}");
            sb.AppendLine($"CLIENTE: {factura.NombreCliente}");
            if (factura.Estado == "ANULADA") 
            {
                sb.AppendLine($"ESTADO: {factura.Estado}");
            }
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Items
            sb.AppendLine("ITEM - DESCRIPCION");
            sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IVA");
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            foreach (var item in factura.Items)
            {
                // Descripción (puede ocupar varias líneas)
                var descripcionLineas = DividirTexto("Item: "+item.Codigo + "-"+ item.Descripcion, ANCHO_TIRILLA - 5);
                foreach (var linea in descripcionLineas)
                {
                    sb.AppendLine($"{linea}");
                }

                // Línea con cantidad y precio
                string cantidad = item.Cantidad.ToString().Length > 6 ? item.Cantidad.ToString().Substring(0, 6) : item.Cantidad.ToString().PadRight(6);
                string unidadMedida = item.UnidadMedida.Length > 3 ? item.UnidadMedida.Substring(0, 3): item.UnidadMedida.PadRight(3);
                string PrecioUnitario = item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Length > 10 ? item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).Substring(0,10): item.PrecioUnitario.ToString("N0", new CultureInfo("es-CO")).PadRight(10);
                string Descuento = item.Descuento.ToString().Length > 5 ? item.Descuento.ToString().Substring(0, 5) : item.Descuento.ToString().PadRight(5);
                string Total = item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Length > 10 ? item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().Substring(0, 10): item.Total.ToString("N0", new CultureInfo("es-CO")).ToString().PadRight(10);
                string impuesto = item.Impuesto.ToString().Length > 3 ? item.Impuesto.ToString().Substring(0, 3) : item.Impuesto.ToString().PadRight(3);

                var lineaCantidad = $"{cantidad}|{unidadMedida}|{PrecioUnitario}|{Descuento}|{Total}|{impuesto}";
                sb.AppendLine(lineaCantidad);
            }

            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Totales
            sb.AppendLine($"{"SUBTOTAL:",-20} {factura.Subtotal.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"DESCUENTO:",-20} {factura.Descuento.ToString("N0", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"IVA:",-20} {factura.Impuesto.ToString("N0", new CultureInfo("es-CO"))}");
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

            //Abrir cajon
            sb.Append("\x1B" + "p" + "\x00" + "\x0F" + "\x96");

            return sb;
        }

        public static StringBuilder GenerarTirillaCotizacion(CotizacionPOSEntitie cotizacion, int Pr_ANCHO_TIRILLA, string MensajeFinalTirilla)
        {
            var sb = new StringBuilder();

            ANCHO_TIRILLA = Pr_ANCHO_TIRILLA;

            // Encabezado
            sb.AppendLine(CentrarTexto(cotizacion.NombreEmpresa));
            sb.AppendLine(CentrarTexto(cotizacion.DireccionEmpresa));
            sb.AppendLine(CentrarTexto($"Tel: {cotizacion.TelefonoEmpresa}"));
            sb.AppendLine(CentrarTexto($"NIT: {cotizacion.NIT}"));
            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Información de la cotizacion
            sb.AppendLine($"COTIZACION: {cotizacion.NumeroCotizacion}");
            sb.AppendLine($"FECHA: {cotizacion.Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"CAJERO: {cotizacion.UserNameCotizacion}");
            sb.AppendLine($"VENDEDOR: {cotizacion.NombreVendedor}");
            sb.AppendLine($"CLIENTE: {cotizacion.NombreCliente}");
            sb.AppendLine($"ESTADO: {cotizacion.Estado}");
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Items
            sb.AppendLine("ITEM - DESCRIPCION");
            sb.AppendLine("CANT  |U.M|PRECIO_UNI|DESC%|TOTAL     |IVA");
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            foreach (var item in cotizacion.Items)
            {
                // Descripción (puede ocupar varias líneas)
                var descripcionLineas = DividirTexto("Item: " + item.Codigo + "-" + item.Descripcion, ANCHO_TIRILLA - 5);
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

        private static string CentrarTexto(string texto)
        {
            if (texto.Length >= ANCHO_TIRILLA) return texto.Substring(0, ANCHO_TIRILLA);

            int espacios = (ANCHO_TIRILLA - texto.Length) / 2;
            return new string(' ', espacios) + texto;
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
