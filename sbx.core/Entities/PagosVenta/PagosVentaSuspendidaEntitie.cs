
namespace sbx.core.Entities.PagosVenta
{
    public class PagosVentaSuspendidaEntitie
    {
        public int IdPagoVenta_Suspendidas { get; set; }
        public int IdVenta_Suspendidas { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Recibido { get; set; }
        public decimal Monto { get; set; }
        public string? Referencia { get; set; }
        public int IdBanco { get; set; }
    }
}
