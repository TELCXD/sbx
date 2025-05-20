using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;

namespace sbx.core.Interfaces.EntradaInventario
{
    public interface IEntradaInventario
    {
        Task<Response<dynamic>> CreateUpdate(EntradasInventarioEntitie entradasInventarioEntitie, int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> Entradas(int IdProducto);
    }
}
