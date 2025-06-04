using sbx.core.Entities;
using sbx.core.Entities.usuario;

namespace sbx.core.Interfaces.Usuario
{
    public interface IUsuario
    {
        Task<Response<dynamic>> CreateUpdate(usuarioEntitie UsuarioEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<bool> ExisteNumeroDocumento(string IdentificationNumber, int IdUser);
        Task<bool> ExisteTelefono(string TelephoneNumber, int IdUser);
        Task<bool> ExisteEmail(string Email, int IdUser);
    }
}
