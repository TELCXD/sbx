
namespace sbx.core.Entities.NotaCredito
{
    public class NotaCreditoEntitie
    {
        public int IdNotaCredito { get; set; }
        public int IdVenta { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public string Consecutivo { get; set; } = string.Empty;
        public Int64 Desde { get; set; }
        public Int64 Hasta { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Motivo { get; set; }
        public int IdUserAction { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DetalleNotaCredito> detalleNotaCredito { get; set; } = new();
        public int Id_RangoNumeracion { get; set; }
        public int IdRangoDIAN { get; set; }
        public int CodigoDocumento { get; set; }
        public string resolution_number { get; set; } = string.Empty;
        public string technical_key { get; set; } = string.Empty;
        public int is_active { get; set; }
        public bool is_expired { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
