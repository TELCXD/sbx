using sbx.core.Entities;
using sbx.core.Entities.NotaCredito;

namespace sbx.core.Interfaces.NotaCredito
{
    public interface INotaCredito
    {
        Task<Response<dynamic>> Create(NotaCreditoEntitie notaCreditoEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> ActualizarDataNotaCreditoElectronica(ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie, int IdUser);
    }
}
