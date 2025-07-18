using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.Auth;
using sbx.core.Entities.RangoNumeracion;

namespace sbx.core.Helper.Factus
{
    public class Ranges
    {
        public Response<dynamic> GetRangos(RangosEntitie rangosEntitie, ResponseAuthEntitie responseAuthEntitie)
        {
            var response = new Response<dynamic>();

            try
            {
                var client = new HttpClient();
                string url = $@"{rangosEntitie.url_api}?
                filter[id]={rangosEntitie.Id}&
                filter[document]={rangosEntitie.document}&
                filter[resolution_number]={rangosEntitie.resolution_number}&
                filter[technical_key]={rangosEntitie.technical_key}&
                filter[is_active]={rangosEntitie.is_active}";

                url = url.Replace("\r\n", "");

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + responseAuthEntitie.access_token);
                var respHttp = client.Send(request);
                int statusCode = (int)respHttp.StatusCode;

                if (respHttp.IsSuccessStatusCode)
                {
                    var repContent = respHttp.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrWhiteSpace(repContent))
                    {
                        var contentResp = JsonConvert.DeserializeObject<ResponseRanges>(repContent);

                        response.Flag = true;
                        response.Message = "Consulta de rangos exitosa";
                        response.Data = contentResp;
                    }
                }
                else
                {
                    var errorContent = respHttp.Content.ReadAsStringAsync();
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
