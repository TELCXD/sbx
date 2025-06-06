using sbx.core.Entities;
using sbx.core.Entities.Pagos;

namespace sbx.core.Interfaces.Pago
{
    public interface IPagosEfectivo
    {
        Task<Response<dynamic>> List(int IdUser, DateTime FechaHoraApertura);
        Task<Response<dynamic>> CreateUpdate(PagosEfectivoEntitie pagosEfectivoEntitie, int IdUser);
    }
}
