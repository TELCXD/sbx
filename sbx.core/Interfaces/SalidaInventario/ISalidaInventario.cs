using sbx.core.Entities;
using sbx.core.Entities.SalidaInventario;

namespace sbx.core.Interfaces.SalidaInventario
{
    public interface ISalidaInventario
    {
        Task<Response<dynamic>> CreateUpdate(SalidaInventarioEntitie salidaInventarioEntitie, int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> List(int Id);
    }
}
