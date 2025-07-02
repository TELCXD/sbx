using sbx.core.Entities;
using sbx.core.Entities.PrecioCliente;

namespace sbx.core.Interfaces.PrecioCliente
{
    public interface IPrecioCliente
    {
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientProducto);
        Task<Response<dynamic>> CreateUpdate(PrecioClienteEntitie precioCliente, int IdUser);
        Task<Response<dynamic>> List(long Id);
        Task<Response<dynamic>> PrecioClientePersonalizado(int IdProducto, int IdCliente);
        Task<Response<dynamic>> Eliminar(int Id);

    }
}
