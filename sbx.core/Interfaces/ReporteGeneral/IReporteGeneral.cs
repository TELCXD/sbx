using sbx.core.Entities;

namespace sbx.core.Interfaces.ReporteGeneral
{
    public interface IReporteGeneral
    {
        Task<Response<dynamic>> BuscarReporteVentas(DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> BuscarReporteCompras(DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> BuscarReporteGastos(DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> BuscarReporteGeneral(DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> BuscarReporteSalidas(DateTime FechaInicio, DateTime FechaFin);
    }
}
