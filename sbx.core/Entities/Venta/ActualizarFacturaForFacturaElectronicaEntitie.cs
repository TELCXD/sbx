
namespace sbx.core.Entities.Venta
{
    public class ActualizarFacturaForFacturaElectronicaEntitie
    {
        public int IdVenta { get; set; }
        public string NumberFacturaDIAN { get; set; } = string.Empty;
        public string EstadoFacturaDIAN { get; set; } = string.Empty;
        public string FacturaJSON { get; set; } = string.Empty;
    }
}
