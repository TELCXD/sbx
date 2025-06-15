using sbx.core.Entities;
using sbx.core.Entities.Cotizacion;

namespace sbx.core.Interfaces.Cotizacion
{
    public interface ICotizacion
    {
        Task<Response<dynamic>> CreateCotizacion(CotizacionEntitie cotizacionEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin, int idUser, string RolName);
        Task<Response<dynamic>> CambioEstadoCotizacion(int IdCotizacion, string Estado, int IdVenta, int IdUser);
    }
}
