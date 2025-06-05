
namespace sbx.core.Entities.NotaCredito
{
    public class NotaCreditoEntitie
    {
        public int IdNotaCredito { get; set; }
        public int IdVenta { get; set; }
        public string Motivo { get; set; }
        public int IdUserAction { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DetalleNotaCredito> detalleNotaCredito { get; set; } = new();
    }
}
