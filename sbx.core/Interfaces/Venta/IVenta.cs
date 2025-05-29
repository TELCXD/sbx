using sbx.core.Entities;
using sbx.core.Entities.Venta;

namespace sbx.core.Interfaces.Venta
{
    public interface IVenta
    {
        Task<Response<dynamic>> Create(VentaEntitie ventaEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
    }
}
