using sbx.core.Entities;
using sbx.core.Entities.NotaCreditoElectronica;

namespace sbx.core.Interfaces.NotaCreditoElectronica
{
    public interface INotasCreditoElectronica
    {
        Response<dynamic> CreaValidaNotaCredito(string Token, string urlApi, NotaCreditoRequest notaCreditoRequest);
    }
}
