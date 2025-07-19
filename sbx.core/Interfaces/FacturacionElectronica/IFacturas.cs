using sbx.core.Entities;
using sbx.core.Entities.FacturaEletronica;

namespace sbx.core.Interfaces.FacturacionElectronica
{
    public interface IFacturas
    {
        Response<dynamic> CreaValidaFactura(string Token, string urlApi, FacturaRequest facturaRequest);
    }
}
