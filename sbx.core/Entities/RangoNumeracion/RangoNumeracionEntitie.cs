
using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.RangoNumeracion
{
    public class RangoNumeracionEntitie
    {
        public int Id_RangoNumeracion { get; set; }
        public int Id_RangoDIAN { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int Id_TipoDocumentoRangoNumeracion { get; set; }
        [Required(ErrorMessage = "El Prefijo es obligatorio")]
        public string Prefijo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Numero desde es obligatorio")]
        public string NumeroDesde { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Numero hasta es obligatorio")]
        public string NumeroHasta { get; set; } = string.Empty;
        public string? NumeroResolucion { get; set; }
        public string? ClaveTecnica { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Active { get; set; }
        public int Vencido { get; set; }
        public int EnUso { get; set; }
        public int CodigoDocumento { get; set; }
    }
}
