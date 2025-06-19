using sbx.core.Entities;
using sbx.core.Entities.AgrupacionProducto;

namespace sbx.core.Interfaces.ConversionProducto
{
    public interface IConversionProducto
    {
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string PadreHijo);
        Task<Response<dynamic>> CreateUpdate(ConversionProductoEntitie conversionProductoEntitie, int IdUser);
        Task<Response<dynamic>> List(Int64 Id);
        Task<Response<dynamic>> Eliminar(Int64 Id);
    }
}
