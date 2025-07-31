
namespace sbx.core.Entities.Venta
{
    public class FacturaPOSEntitie
    {
        public string NumeroFactura { get; set; } = "";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NombreEmpresa { get; set; } = "";
        public string DireccionEmpresa { get; set; } = "";
        public string TelefonoEmpresa { get; set; } = "";
        public string NIT { get; set; } = "";
        public string UserNameFactura { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = "CLIENTE GENERAL";
        public string NombreVendedor { get; set; } = string.Empty;
        public List<ItemFacturaEntitie> Items { get; set; } = new();
        public decimal CantidadTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public string Tributo { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string FormaPago { get; set; } = "EFECTIVO";
        public decimal Recibido { get; set; }
        public decimal Cambio { get; set; }
        public string NumberFacturaDIAN { get; set; } = string.Empty;
        public string EstadoFacturaDIAN { get; set; } = string.Empty;
        public string FacturaJSON { get; set; }
        public bool FacturaElectronica { get; set; }
        public string qr_image { get; set; } = string.Empty;
        public decimal iva { get; set; }
        public decimal inc { get; set; }
        public decimal incBolsa { get; set; }
    }
}
