
namespace sbx.core.Entities.Venta
{
    public class ItemFacturaEntitie
    {
        public string Codigo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
