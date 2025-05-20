using sbx.core.Entities;

namespace sbx.core.Interfaces.Pais
{
    public interface IPais
    {
        Task<Response<dynamic>> ListPais();
    }
}
