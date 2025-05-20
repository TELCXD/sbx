using sbx.core.Entities;

namespace sbx.core.Interfaces.IdentificationType
{
    public interface IIdentificationType
    {
        Task<Response<dynamic>> ListIdentificationType();
    }
}
