
using sbx.core.Entities;

namespace sbx.core.Interfaces
{
    public interface ILogin
    {
        Task<Response<dynamic>> ValidarUsuario(string nombreUsuario, string contrasena);
    }
}
