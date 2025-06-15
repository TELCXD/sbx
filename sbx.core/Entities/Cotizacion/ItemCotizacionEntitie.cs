
namespace sbx.core.Entities.Cotizacion
{
    public class ItemCotizacionEntitie
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = "";
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Impuesto { get; set; }
    }
}
