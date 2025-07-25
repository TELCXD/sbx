using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.NotaCreditoElectronica;
using sbx.core.Interfaces.NotaCreditoElectronica;

namespace ExternalServices.FacturacionElectronica.Factus.NotasCredito
{
    public class NotasCredito : INotasCreditoElectronica
    {
        public Response<dynamic> CreaValidaNotaCredito(string Token, string urlApi, NotaCreditoRequest notaCreditoRequest)
        {
            var response = new Response<dynamic>();

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, urlApi);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + Token);

                string JsonNotaCredito = JsonConvert.SerializeObject(notaCreditoRequest, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var content = new StringContent(JsonNotaCredito, null, "application/json");
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
                        response.Message = "Creacion de nota credito electronica exitosa";
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
