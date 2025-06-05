
using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.usuario
{
    public class usuarioEntitie
    {
        public int IdUser { get; set; }
        public int IdIdentificationType { get; set; }
        [Required(ErrorMessage = "El Numero Documento es obligatorio")]
        public string IdentificationNumber { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdCountry { get; set; }
        public int IdDepartament { get; set; }
        public int IdCity { get; set; }
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene el formato correcto.")]
        public string TelephoneNumber { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo distribucion no tiene el formato correcto.")]
        public string Email { get; set; }
        public int IdRole { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int Active { get; set; }
    }
}
