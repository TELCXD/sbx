using sbx.core.Entities;

namespace sbx.core.Interfaces.Dashboard
{
    public interface IDashboard
    {
        Task<Response<dynamic>> Buscar(DateTime FechaInicio, DateTime FechaFin);
    }
}
