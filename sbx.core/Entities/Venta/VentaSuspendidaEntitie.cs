using sbx.core.Entities.PagosVenta;

namespace sbx.core.Entities.Venta
{
    public class VentaSuspendidaEntitie
    {
        public int IdVenta_Suspendidas { get; set; }
        public int IdListaPrecio { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int IdMetodoPago { get; set; }
        public List<DetalleVentaSuspendidaEntitie> detalleVentasSuspendida { get; set; } = new();
        public List<PagosVentaSuspendidaEntitie> pagosVentaSuspendida { get; set; } = new();
    }
}
