
namespace sbx.core.Entities.Caja
{
    public class CajaEntitie
    {
        public int IdApertura_Cierre_caja { get; set; }
        public int IdUserAction { get; set; }
        public DateTime FechaHoraApertura { get; set; }
        public decimal MontoInicialDeclarado { get; set; }
        public DateTime FechaHoraCierre { get; set; }
        public decimal MontoFinalDeclarado { get; set; }
        public decimal VentasTotales { get; set; }
        public decimal PagosEnEfectivo { get; set; }
        public decimal PagosEnNequi { get; set; }
        public decimal PagosEnDaviPlata { get; set; }
        public decimal PagosEnBancolombiaQR { get; set; }
        public decimal PagosEnTransferencia { get; set; }
        public decimal PagosEnTarjetaCredito { get; set; }
        public decimal PagosEnTarjetaDebito { get; set; }
        public decimal Diferencia { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
    }
}
