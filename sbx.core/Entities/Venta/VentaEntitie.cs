using sbx.core.Entities.PagosVenta;

namespace sbx.core.Entities.Venta
{
    public class VentaEntitie
    {
        public int IdVenta { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public Int64 Desde { get; set; }
        public Int64 Hasta { get; set; }
        public string Consecutivo { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int IdMetodoPago { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<DetalleVentaEntitie> detalleVentas { get; set; } = new();
        public List<PagosVentaEntitie> pagosVenta { get; set; } = new();
        public int Id_RangoNumeracion { get; set; }
        public int IdRangoDIAN { get; set; }
        public int CodigoDocumento { get; set; }
        public string resolution_number { get; set; } = string.Empty;
        public string technical_key { get; set; } = string.Empty;
        public int is_active { get; set; }
        public bool is_expired { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
