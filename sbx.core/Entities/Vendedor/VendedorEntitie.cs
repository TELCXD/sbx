
namespace sbx.core.Entities.Vendedor
{
    public class VendedorEntitie
    {
        public int IdVendedor { get; set; }
        public int IdIdentificationType { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Estado { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int IdUserAction { get; set; }
    }
}
