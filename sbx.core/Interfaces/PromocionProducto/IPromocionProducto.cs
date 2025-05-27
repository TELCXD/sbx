using sbx.core.Entities;
using sbx.core.Entities.PromocionProducto;

namespace sbx.core.Interfaces.PromocionProducto
{
    public interface IPromocionProducto
    {
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> CreateUpdate(PromocionProductoEntitie promocionProductoEntitie, int IdUser);
        Task<Response<dynamic>> RemoveProducto(PromocionProductoEntitie promocionProductoEntitie);
    }
}
