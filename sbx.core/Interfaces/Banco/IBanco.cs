using sbx.core.Entities;

namespace sbx.core.Interfaces.Banco
{
    public interface IBanco
    {
        Task<Response<dynamic>> List();
    }
}
