using sbx.core.Entities;
using sbx.core.Entities.ListaPrecios;

namespace sbx.core.Interfaces.ListaPrecios
{
    public interface IListaPrecios
    {
        Task<Response<dynamic>> CreateUpdate(ListaPreciosEntitie listaPreciosEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
    }
}
