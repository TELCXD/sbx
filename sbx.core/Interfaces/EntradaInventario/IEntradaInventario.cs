using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;
using System.Data;

namespace sbx.core.Interfaces.EntradaInventario
{
    public interface IEntradaInventario
    {
        Task<Response<dynamic>> CreateUpdate(EntradasInventarioEntitie entradasInventarioEntitie, int IdUser);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string tipo, DateTime FechaInicio, DateTime FechaFin);
        Task<Response<dynamic>> Entradas(int IdProducto);
        Task<Response<dynamic>> CargueMasivoProductoEntrada(DataTable Datos, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> CargueMasivoEditarProductoEntradaSalidas(DataTable Datos, int IdUser);
    }
}
