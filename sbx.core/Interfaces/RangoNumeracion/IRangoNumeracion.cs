using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;

namespace sbx.core.Interfaces.RangoNumeracion
{
    public interface IRangoNumeracion
    {
        Task<Response<dynamic>> CreateUpdate(RangoNumeracionEntitie rangoNumeracionEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<Response<dynamic>> ListEnUso(int Id, int Id_TipoDocumentoRangoNumeracion);
        Task<bool> ExisteIdRangoDIAN(string IdRango, int Id);
        Task<bool> ExistePrefijo(string prefijo, int Id);
        Task<bool> ExisteResolucion(string Resolucion, int Id);
        Task<bool> ExisteClaveTecnica(string ClaveTecnica, int Id);
        Task<Response<dynamic>> IdentificaDocumento(int IdTipoDocumento);
        Task<Response<dynamic>> ValidaRango(int Id);
    }
}
