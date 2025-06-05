using sbx.core.Entities;
using sbx.core.Entities.Permiso;

namespace sbx.core.Interfaces.Permisos
{
    public interface IPermisos
    {
        Task<Response<dynamic>> CreateUpdate(List<PermisosEntitie> permisosEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
    }
}
