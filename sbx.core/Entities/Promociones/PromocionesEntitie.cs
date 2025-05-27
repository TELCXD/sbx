
namespace sbx.core.Entities.Promociones
{
    public class PromocionesEntitie
    {
        public int IdPromocion { get; set; }
        public string NombrePromocion { get; set; }
        public int IdTipoPromocion { get; set; }
        public decimal Porcentaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
