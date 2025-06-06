using sbx.core.Entities.PagosVenta;

namespace sbx.core.Entities.Venta
{
    public class VentaEntitie
    {
        public int IdVenta { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public string Consecutivo { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int IdMetodoPago { get; set; }
        public string Estado { get; set; }
        public List<DetalleVentaEntitie> detalleVentas { get; set; } = new();
        public List<PagosVentaEntitie> pagosVenta { get; set; } = new();
    }
}
