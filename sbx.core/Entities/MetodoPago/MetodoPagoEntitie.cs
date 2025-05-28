
namespace sbx.core.Entities.MetodoPago
{
    public class MetodoPagoEntitie
    {
        public int IdMetodoPago { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int RequiereReferencia { get; set; }
        public int PermiteVuelto { get; set; }
        public int TieneComision { get; set; }
        public int PorcentajeComision { get; set; }
        public int Activo { get; set; }
    }
}
