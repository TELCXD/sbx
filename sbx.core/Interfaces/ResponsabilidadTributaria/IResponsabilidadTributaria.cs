using sbx.core.Entities;

namespace sbx.core.Interfaces.ResponsabilidadTributaria
{
    public interface IResponsabilidadTributaria
    {
        Task<Response<dynamic>> ListResponsabilidadTributaria();
    }
}
