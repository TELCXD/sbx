using sbx.core.Entities;

namespace sbx.core.Interfaces.PrecioCliente
{
    public interface IPrecioCliente
    {
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientProducto);
    }
}
