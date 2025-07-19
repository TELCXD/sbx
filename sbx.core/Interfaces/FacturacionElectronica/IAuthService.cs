using sbx.core.Entities;
using sbx.core.Entities.Auth;

namespace sbx.core.Interfaces.FacturacionElectronica
{
    public interface IAuthService
    {
        Response<dynamic> Autenticacion(AuthEntitie authEntitie);
        Response<dynamic> RefreshToken(AuthEntitie authEntitie);
    }
}
