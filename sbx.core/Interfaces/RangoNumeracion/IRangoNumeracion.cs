using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Entities.Tienda;

namespace sbx.core.Interfaces.RangoNumeracion
{
    public interface IRangoNumeracion
    {
        Task<Response<dynamic>> CreateUpdate(RangoNumeracionEntitie rangoNumeracionEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
    }
}
