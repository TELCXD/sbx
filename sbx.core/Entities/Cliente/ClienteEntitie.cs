using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.Cliente
{
    public class ClienteEntitie
    {
        public int IdCliente { get; set; }
        public int IdIdentificationType { get; set; }
        [Required(ErrorMessage = "El Numero Documento es obligatorio")]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "El Nombre RazonSocial es obligatorio")]
        public string NombreRazonSocial { get; set; }
        public int IdTipoCliente { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene el formato correcto.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo distribucion no tiene el formato correcto.")]
        public string Email { get; set; }
        public int Estado { get; set; }
        public int IdTipoResponsabilidad { get; set; }
        public int IdResponsabilidadTributaria { get; set; }
        public int IdTipoContribuyente { get; set; }
        public int IdCountry { get; set; }
        public int IdDepartament { get; set; }
        public int IdCity { get; set; }
    }
}
