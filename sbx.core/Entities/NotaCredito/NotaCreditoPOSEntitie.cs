using sbx.core.Entities.Venta;

namespace sbx.core.Entities.NotaCredito
{
    public class NotaCreditoPOSEntitie
    {
        public string NumeroNotaCredito { get; set; } = "";
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NombreEmpresa { get; set; } = "";
        public string DireccionEmpresa { get; set; } = "";
        public string TelefonoEmpresa { get; set; } = "";
        public string NIT { get; set; } = "";
        public string UserNameNotaCredito { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = "CLIENTE GENERAL";
        public List<ItemFacturaEntitie> Items { get; set; } = new();
        public decimal CantidadTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string NumberNotaCreditoDIAN { get; set; } = string.Empty;
        public string EstadoNotaCreditoDIAN { get; set; } = string.Empty;
        public string NotaCreditoJSON { get; set; }
        public bool NotaCreditoElectronica { get; set; }
        public string qr_image { get; set; } = string.Empty;
        public decimal iva { get; set; }
        public decimal inc { get; set; }
        public decimal incBolsa { get; set; }
    }
}
