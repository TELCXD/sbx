
using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.RangoNumeracion
{
    public class RangoNumeracionEntitie
    {
        public int Id_RangoNumeracion { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int Id_TipoDocumentoRangoNumeracion { get; set; }
        [Required(ErrorMessage = "El Prefijo es obligatorio")]
        public string Prefijo { get; set; }
        [Required(ErrorMessage = "El Numero desde es obligatorio")]
        public string NumeroDesde { get; set; }
        [Required(ErrorMessage = "El Numero hasta es obligatorio")]
        public string NumeroHasta { get; set; }
        public string? NumeroAutorizacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio")]
        public int Active { get; set; }
        [Required(ErrorMessage = "Debe indicar si es Numeracion autorizada o no")]
        public int NumeracionAutorizada { get; set; }
    }
}
