using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.Tienda
{
    public class TiendaEntitie
    {
        public int IdTienda { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int TipoDocumento { get; set; }
        [Required(ErrorMessage = "El numero de documento es obligatorio")]
        public string NumeroDocumento { get; set; }
        public string DigitoVerificacion { get; set; }
        [Required(ErrorMessage = "El nombre/razon social es obligatorio")]
        public string NombreRazonSocial { get; set; }
        [Required(ErrorMessage = "El tipo responsabilidad es obligatorio")]
        public int TipoResponsabilidad { get; set; }
        [Required(ErrorMessage = "La responsabilidad tributaria es obligatoria")]
        public int ResponsabilidadTributaria { get; set; }
        [Required(ErrorMessage = "El tipo contribuyente es obligatorio")]
        public int TipoContribuyente { get; set; }
        [Required(ErrorMessage = "El Correo distribucion es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo distribucion no tiene el formato correcto.")]
        public string CorreoDistribucion { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene el formato correcto.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El pais es obligatorio")]
        public int Pais { get; set; }
        [Required(ErrorMessage = "El departamento es obligatorio")]
        public int Departamento { get; set; }
        [Required(ErrorMessage = "El municipio es obligatorio")]
        public int Municipio { get; set; }
        [Required(ErrorMessage = "El codigo postal es obligatorio")]
        public int CodigoPostal { get; set; }
        [Required(ErrorMessage = "La actividad economica es obligatoria")]
        public int ActividadEconomica { get; set; }
        public string logo { get; set; }
    }
}
