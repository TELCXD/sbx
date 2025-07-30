using sbx.core.Entities;

namespace sbx.core.Interfaces.Tributos
{
    public interface ITribute
    {
        Task<Response<dynamic>> ListTribute();
    }
}
