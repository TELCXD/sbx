using sbx.core.Entities;

namespace sbx.core.Interfaces.Reportes
{
    public interface IReportes
    {
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string TipoReporte, DateTime FechaInicio, DateTime FechaFin);
    }
}
