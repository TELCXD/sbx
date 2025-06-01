using sbx.core.Entities;
using sbx.core.Entities.Parametros;

namespace sbx.core.Interfaces.Parametros
{
    public interface IParametros
    {
        Task<Response<dynamic>> List(string Nombre);
        Task<Response<dynamic>> Update(List<ParametrosEntitie> parametrosEntitie);
    }
}
