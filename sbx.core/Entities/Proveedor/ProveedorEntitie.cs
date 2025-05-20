using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.Proveedor
{
    public class ProveedorEntitie
    {
        public int IdProveedor { get; set; }
        public int IdIdentificationType { get; set; }
        [Required(ErrorMessage = "El Numero Documento es obligatorio")]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "El Nombre RazonSocial es obligatorio")]
        public string NombreRazonSocial { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene el formato correcto.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo distribucion no tiene el formato correcto.")]
        public string Email { get; set; }
        public int Estado { get; set; }
    }
}
