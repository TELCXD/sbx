
namespace sbx.core.Entities.AgrupacionProducto
{
    public class ConversionProductoEntitie
    {
        public Int64 Llave { get; set; }
        public int IdProductoPadre { get; set; }
        public int IdProductoHijo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
