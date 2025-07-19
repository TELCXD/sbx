using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.FacturaEletronica;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Interfaces.FacturacionElectronica;

namespace ExternalServices.FacturacionElectronica.Factus.Facturas
{
    public class Facturas: IFacturas
    {
        public Response<dynamic> CreaValidaFactura(string Token,string urlApi, FacturaRequest facturaRequest)
        {
            var response = new Response<dynamic>();

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, urlApi);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + Token);

                string JsonFactura = JsonConvert.SerializeObject(facturaRequest, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var content = new StringContent(JsonFactura, null, "application/json");
                request.Content = content;

                var respHttp = client.Send(request);
                int statusCode = (int)respHttp.StatusCode;

                if (respHttp.IsSuccessStatusCode)
                {
                    var repContent = respHttp.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrWhiteSpace(repContent))
                    {
                        var contentResp = JsonConvert.DeserializeObject<dynamic>(repContent);

                        response.Flag = true;
                        response.Message = "Consulta de rangos exitosa";
                        response.Data = contentResp;
                    }
                }
                else
                {
                    var errorContent = respHttp.Content.ReadAsStringAsync().Result;
                    response.Flag = false;
                    response.Message = $"Error: {errorContent} ";
                    response.Data = statusCode;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Flag = false;
                response.Message = $"Error: {ex.Message} ";
                return response;
            }
        }
    }
}
