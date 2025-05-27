using sbx.core.Entities;
using sbx.core.Entities.PrecioProducto;

namespace sbx.core.Interfaces.PrecioProducto
{
    public interface IPrecioProducto
    {
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> CreateUpdate(PrecioProductoEntitie precioProductoEntitie, int IdUser);
        Task<Response<dynamic>> RemoveProducto(PrecioProductoEntitie precioProductoEntitie);
    }
}
