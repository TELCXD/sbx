using sbx.core.Entities;

namespace sbx.core.Interfaces.TipoPromocion
{
    public interface ITipoPromocion
    {
        Task<Response<dynamic>> ListTipoPromocion();
    }
}
