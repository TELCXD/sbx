using sbx.core.Entities.Gasto;
using sbx.core.Entities;

namespace sbx.core.Interfaces.Gastos
{
    public interface IGastos
    {
        Task<Response<dynamic>> CreateUpdate(GastoEntitie GastoEntitie, int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> Eliminar(int Id);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> BuscarReporte(DateTime FechaInicio, DateTime FechaFin);
    }
}
