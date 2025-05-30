﻿using System.Globalization;
using System.Text;

namespace sbx.core.Entities.Venta
{
    public class GenerarTirillaPOS
    {
        private const int ANCHO_TIRILLA = 40;

        public static string GenerarTirilla(FacturaPOSEntitie factura)
        {
            var sb = new StringBuilder();

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
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Items
            sb.AppendLine("ITEM - DESCRIPCION");
            sb.AppendLine("CANT | U.M | PRECIO_UNI | DESC% | TOTAL | IVA");
            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            foreach (var item in factura.Items)
            {
                // Descripción (puede ocupar varias líneas)
                var descripcionLineas = DividirTexto(item.Codigo + " - "+ item.Descripcion, ANCHO_TIRILLA - 5);
                foreach (var linea in descripcionLineas)
                {
                    sb.AppendLine($"{linea}");
                }

                // Línea con cantidad y precio
                var lineaCantidad = $"{item.Cantidad} {item.UnidadMedida} {item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO"))} {item.Descuento} {item.Total.ToString("N2", new CultureInfo("es-CO"))} {item.Impuesto}";
                sb.AppendLine(lineaCantidad);
            }

            sb.AppendLine(new string('-', ANCHO_TIRILLA));

            // Totales
            sb.AppendLine($"{"SUBTOTAL:",-20} {factura.Subtotal.ToString("N2", new CultureInfo("es-CO"))}");
            sb.AppendLine($"{"DESCUENTO:",-20} {factura.Descuento}");
            sb.AppendLine($"{"IVA:",-20} {factura.Impuesto}");
            sb.AppendLine($"{"TOTAL:",-20} {factura.Total.ToString("N2", new CultureInfo("es-CO"))}");
            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Forma de pago
            sb.AppendLine($"FORMA DE PAGO: {factura.FormaPago}");
            if (factura.FormaPago.ToUpper() == "EFECTIVO")
            {
                sb.AppendLine($"{"RECIBIDO:",-20} {factura.Recibido.ToString("N2", new CultureInfo("es-CO"))}");
                sb.AppendLine($"{"CAMBIO:",-20} {factura.Cambio.ToString("N2", new CultureInfo("es-CO"))}");
            }

            sb.AppendLine(new string('=', ANCHO_TIRILLA));

            // Pie de página
            sb.AppendLine(CentrarTexto("GRACIAS POR SU COMPRA"));
            sb.AppendLine(CentrarTexto("VUELVA PRONTO"));
            sb.AppendLine();
            sb.AppendLine(CentrarTexto($"Sistema SBX - {DateTime.Now:dd/MM/yyyy HH:mm}"));

            return sb.ToString();
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
