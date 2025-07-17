using Newtonsoft.Json;
using sbx.core.Entities.Auth;

namespace sbx.core.Helper.Factus
{
    public class Auth
    {
        public Auth() { }

        public ResponseAuthEntitie GeneraToken(AuthEntitie authEntitie)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, authEntitie.url_api);
                request.Headers.Add("Accept", "application/json");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("password"), "grant_type");
                content.Add(new StringContent("9f5fc0dd-b02a-4077-a817-6fe4a5baa029"), "client_id");
                content.Add(new StringContent("U6PHhU2T7RJyHOBAfet8spFKmwgpSAkGaRZQN896"), "client_secret");
                content.Add(new StringContent("sandbox@factus.com.co"), "username");
                content.Add(new StringContent("sandbox2024%"), "password");
                request.Content = content;
                var response =  client.Send(request);
                response.EnsureSuccessStatusCode();
                var repContent = response.Content.ReadAsStringAsync().Result;

                ResponseAuthEntitie responseAuthEntitie = new ResponseAuthEntitie();

                if (!string.IsNullOrWhiteSpace(repContent)) 
                {
                    var contentResp = JsonConvert.DeserializeObject<ResponseAuthEntitie>(repContent);

                    if (contentResp != null) 
                    {
                        responseAuthEntitie.token_type = contentResp.token_type;
                        responseAuthEntitie.expires_in = contentResp.expires_in;
                        responseAuthEntitie.access_token = contentResp.access_token;
                        responseAuthEntitie.refresh_token = contentResp.refresh_token;
                    }
                }
                           
                return responseAuthEntitie;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
