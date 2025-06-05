using sbx.core.Entities;
using sbx.core.Entities.Venta;

namespace sbx.core.Interfaces.Venta
{
    public interface IVenta
    {
        Task<Response<dynamic>> Create(VentaEntitie ventaEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin, int idUser, string RolName);
        Task<Response<dynamic>> VentasTotales(int IdUser, DateTime FechaHoraApertura);
        Task<Response<dynamic>> BuscarFactura(string dato, string campoFiltro, string tipoFiltro);
    }
}
