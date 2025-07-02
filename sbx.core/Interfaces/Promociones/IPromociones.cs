using sbx.core.Entities;
using sbx.core.Entities.Promociones;

namespace sbx.core.Interfaces.Promociones
{
    public interface IPromociones
    {
        Task<Response<dynamic>> CreateUpdate(PromocionesEntitie promocionesEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> Eliminar(int Id);
    }
}
