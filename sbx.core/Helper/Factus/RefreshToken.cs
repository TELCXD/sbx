using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.Auth;

namespace sbx.core.Helper.Factus
{
    public class RefreshToken
    {
        public Response<dynamic> RefreshAuth(AuthEntitie authEntitie)
        {
            var response = new Response<dynamic>();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, authEntitie.url_api);
                request.Headers.Add("Accept", "application/json");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(authEntitie.grant_type), "grant_type");
                content.Add(new StringContent(authEntitie.client_id), "client_id");
                content.Add(new StringContent(authEntitie.client_secret), "client_secret");
                content.Add(new StringContent(authEntitie.username), "username");
                content.Add(new StringContent(authEntitie.Passwords), "password");
                request.Content = content;
                var respHttp = client.Send(request);

                int statusCode = (int)respHttp.StatusCode;

                if (respHttp.IsSuccessStatusCode)
                {
                    var repContent = respHttp.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrWhiteSpace(repContent))
                    {
                        var contentResp = JsonConvert.DeserializeObject<ResponseAuthEntitie>(repContent);

                        response.Flag = true;
                        response.Message = "Autenticacion exitosa";
                        response.Data = contentResp;
                    }
                }
                else
                {
                    var errorContent = respHttp.Content.ReadAsStringAsync();
                    response.Flag = false;
                    response.Message = $"Error en autenticacion: {errorContent} ";
                    response.Data = statusCode;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Flag = false;
                response.Message = $"Error en refresh token: {ex.Message} ";
                return response;
            }
        }
    }
}
