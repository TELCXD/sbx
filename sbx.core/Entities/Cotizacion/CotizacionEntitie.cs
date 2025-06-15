
namespace sbx.core.Entities.Cotizacion
{
    public class CotizacionEntitie
    {
        public int IdCotizacion { get; set; }
        public int IdListaPrecio { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int DiasVencimiento { get; set; }
        public string Estado { get; set; }
        public List<DetalleCotizacionEntitie> detalleCotizacion { get; set; } = new();
    }
}
