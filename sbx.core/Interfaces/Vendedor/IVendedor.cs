using sbx.core.Entities;
using sbx.core.Entities.Vendedor;

namespace sbx.core.Interfaces.Vendedor
{
    public interface IVendedor
    {
        Task<Response<dynamic>> CreateUpdate(VendedorEntitie vendedorEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<bool> ExisteNumeroDocumento(string numeroDocumento, int Id_Vendedor);
        Task<bool> ExisteTelefono(string telefono, int Id_Vendedor);
        Task<bool> ExisteEmail(string email, int Id_Vendedor);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> ListActivos(int Id);
    }
}
