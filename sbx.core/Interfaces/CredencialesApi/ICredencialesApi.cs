using sbx.core.Entities;
using sbx.core.Entities.CrendencialesApi;

namespace sbx.core.Interfaces.CredencialesApi
{
    public interface ICredencialesApi
    {
        Task<Response<dynamic>> List();
        Task<Response<dynamic>> CreateUpdate(CredencialesApiEntitie credencialesApiEntitie, int IdUser);
        Task<Response<dynamic>> ListId(int Id);
        Task<Response<dynamic>> Eliminar(int Id);
        Task<Response<dynamic>> ListGrupo(int Id);
        Task<bool> ExisteUrl(string Url, int IdCredencialesApi);
        Task<Response<dynamic>> ListGrupo(string Grupo);
    }
}
