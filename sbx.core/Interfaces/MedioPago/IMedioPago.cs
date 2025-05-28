using sbx.core.Entities;

namespace sbx.core.Interfaces.MedioPago
{
    public interface IMedioPago
    {
        Task<Response<dynamic>> List();
    }
}
