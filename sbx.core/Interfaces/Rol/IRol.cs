using sbx.core.Entities;

namespace sbx.core.Interfaces.Rol
{
    public interface IRol
    {
        Task<Response<dynamic>> List();
    }
}
