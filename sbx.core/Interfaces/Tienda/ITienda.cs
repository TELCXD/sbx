using sbx.core.Entities;
using sbx.core.Entities.Tienda;

namespace sbx.core.Interfaces.Tienda
{
    public interface ITienda
    {
        Task<Response<dynamic>> CreateUpdate(TiendaEntitie tiendaEntitie, int IdUser);
        Task<Response<dynamic>> List();
    }
}
