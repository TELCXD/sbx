using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;

namespace sbx.core.Interfaces.FacturacionElectronica
{
    public interface IRangoNumeracionFE
    {
        Response<dynamic> ConsultaRangoDIAN(string Token, string urlApi, RangosEntitie rangosEntitie);
    }
}
