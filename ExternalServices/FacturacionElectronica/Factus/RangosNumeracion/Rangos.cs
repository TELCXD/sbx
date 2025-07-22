using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Interfaces.FacturacionElectronica;

namespace ExternalServices.FacturacionElectronica.Factus.RangosNumeracion
{
    public class Rangos: IRangoNumeracionFE
    {
        public Response<dynamic> ConsultaRangoDIAN(string Token, string urlApi, RangosEntitie rangosEntitie)
        {
            var response = new Response<dynamic>();

            try
            {
                var client = new HttpClient();
                string Url = @$"{urlApi}?
                filter[id]={rangosEntitie.Id}&
                filter[document]={rangosEntitie.document}&
                filter[resolution_number]={rangosEntitie.resolution_number}&
                filter[technical_key]={rangosEntitie.technical_key}&
                filter[is_active]={rangosEntitie.is_active}"
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace(" ", ""); ;

                var request = new HttpRequestMessage(HttpMethod.Get, Url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + Token);

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
