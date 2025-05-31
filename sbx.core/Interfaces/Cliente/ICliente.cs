using sbx.core.Entities;
using sbx.core.Entities.Cliente;

namespace sbx.core.Interfaces.Cliente
{
    public interface ICliente
    {
        Task<Response<dynamic>> CreateUpdate(ClienteEntitie clienteEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<bool> ExisteNumeroDocumento(string numeroDocumento, int Id_Proveedor);
        Task<bool> ExisteNombreRazonSocial(string nombreRazonSocial, int Id_Proveedor);
        Task<bool> ExisteTelefono(string telefono, int Id_Proveedor);
        Task<bool> ExisteEmail(string email, int Id_Proveedor);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro); 
        Task<Response<dynamic>> ListNumDoc(string NumDoc);
    }
}
