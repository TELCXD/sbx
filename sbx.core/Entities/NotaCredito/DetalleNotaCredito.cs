
namespace sbx.core.Entities.NotaCredito
{
    public class DetalleNotaCredito
    {
        public int IdNotaCreditoDetalle { get; set; }
        public int IdNotaCredito { get; set; }
        public int IdDetalleVenta { get; set; }
        public int IdProducto { get; set; }
        public string Sku { get; set; }
        public string CodigoBarras { get; set; }
        public string NombreProducto { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public DateTime CreationDate { get; set; }
        public int IdUserAction { get; set; }
        public string NombreTributo { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
