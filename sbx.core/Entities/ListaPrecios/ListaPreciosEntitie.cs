
namespace sbx.core.Entities.ListaPrecios
{
    public class ListaPreciosEntitie
    {
        public int IdListaPrecio { get; set; }
        public string NombreLista { get; set; }
        public int IdTipoCliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
