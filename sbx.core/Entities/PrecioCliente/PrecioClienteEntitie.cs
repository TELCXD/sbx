
namespace sbx.core.Entities.PrecioCliente
{
    public class PrecioClienteEntitie
    {
        public long llavePrimaria { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioEspecial { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
