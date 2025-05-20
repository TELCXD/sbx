
using sbx.core.Entities;

namespace sbx.core.Interfaces.CodigoPostal
{
    public interface ICodigoPostal
    {
        Task<Response<dynamic>> ListCodigoPostal();
    }
}
