
using sbx.core.Entities;

namespace sbx.core.Interfaces.ActividadEconomica
{
    public interface IActividadEconomica
    {
        Task<Response<dynamic>> ListActividadEconomica();
    }
}
