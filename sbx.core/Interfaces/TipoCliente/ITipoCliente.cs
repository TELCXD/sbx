using sbx.core.Entities;

namespace sbx.core.Interfaces.TipoCliente
{
    public interface ITipoCliente
    {
        Task<Response<dynamic>> ListTipoCliente();
    }
}
