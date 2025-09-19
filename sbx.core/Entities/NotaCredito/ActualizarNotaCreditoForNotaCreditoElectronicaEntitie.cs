
namespace sbx.core.Entities.NotaCredito
{
    public class ActualizarNotaCreditoForNotaCreditoElectronicaEntitie
    {
        public int IdNotaCredito { get; set; }
        public string NumberNotaCreditoDIAN { get; set; } = string.Empty;
        public string EstadoNotaCreditoDIAN { get; set; } = string.Empty;
        public string NotaCreditoJSON { get; set; } = string.Empty;
        public string qr_image { get; set; } = string.Empty;
        public string NotaCreditoRequestJSON { get; set; } = string.Empty;
        public string ResponseFactusError { get; set; } = string.Empty;
    }
}
