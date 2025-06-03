using sbx.core.Entities;
using sbx.core.Entities.Caja;

namespace sbx.core.Interfaces.Caja
{
    public interface ICaja
    {
        Task<Response<dynamic>> CreateUpdate(CajaEntitie cajaEntitie);
        Task<Response<dynamic>> List(int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, DateTime Finicio, DateTime Ffin);
        Task<Response<dynamic>> EstadoCaja(int IdUser);
    }
}
