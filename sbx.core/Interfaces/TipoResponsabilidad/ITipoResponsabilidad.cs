using sbx.core.Entities;

namespace sbx.core.Interfaces.TipoResponsabilidad
{
    public interface ITipoResponsabilidad
    {
        Task<Response<dynamic>> ListTipoResponsabilidad();
    }
}
