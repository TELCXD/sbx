
namespace sbx.core.Entities.Cotizacion
{
    public class CotizacionPOSEntitie
    {
        public string NumeroCotizacion { get; set; } = "";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NombreEmpresa { get; set; } = "";
        public string DireccionEmpresa { get; set; } = "";
        public string TelefonoEmpresa { get; set; } = "";
        public string NIT { get; set; } = "";
        public string UserNameCotizacion { get; set; }
        public string NombreCliente { get; set; } = "CLIENTE GENERAL";
        public string NombreVendedor { get; set; }
        public List<ItemCotizacionEntitie> Items { get; set; } = new();
        public decimal CantidadTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public int DiasVencimiento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal iva { get; set; }
        public decimal inc { get; set; }
        public decimal incBolsa { get; set; }
    }
}
