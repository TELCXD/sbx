using sbx.core.Entities;
using sbx.core.Entities.NotaCredito;

namespace sbx.core.Interfaces.NotaCredito
{
    public interface INotaCredito
    {
        Task<Response<dynamic>> Create(NotaCreditoEntitie notaCreditoEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> ActualizarDataNotaCreditoElectronica(ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie, int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin, int idUser, string RolName);
    }
}
