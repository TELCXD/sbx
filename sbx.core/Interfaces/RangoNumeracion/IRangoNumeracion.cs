using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;

namespace sbx.core.Interfaces.RangoNumeracion
{
    public interface IRangoNumeracion
    {
        Task<Response<dynamic>> CreateUpdate(RangoNumeracionEntitie rangoNumeracionEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> ListEnUso(int Id);
    }
}
