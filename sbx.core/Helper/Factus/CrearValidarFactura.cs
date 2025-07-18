using Newtonsoft.Json;
using sbx.core.Entities;
using sbx.core.Entities.Auth;
using sbx.core.Entities.RangoNumeracion;

namespace sbx.core.Helper.Factus
{
    public class CrearValidarFactura
    {
        public Response<dynamic> CreaValidaFactura(RangosEntitie rangosEntitie, ResponseAuthEntitie responseAuthEntitie)
        {
            var response = new Response<dynamic>();

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api-sandbox.factus.com.co/v1/bills/validate");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + responseAuthEntitie.access_token);

                var content = new StringContent("{\n    \"numbering_range_id\": 4,\n    \"reference_code\": \"I3\",\n    \"observation\": \"\",\n    \"payment_form\": \"1\",\n\t\"payment_due_date\": \"2024-12-30\",\n    \"payment_method_code\": \"10\",\n\t\"billing_period\": {\n        \"start_date\": \"2024-01-10\",\n        \"start_time\": \"00:00:00\",\n        \"end_date\": \"2024-02-09\",\n        \"end_time\": \"23:59:59\"\n    },\n    \"customer\": {\n        \"identification\": \"123456789\",\n        \"dv\": \"3\",\n        \"company\": \"\",\n        \"trade_name\": \"\",\n        \"names\": \"Alan Turing\",\n        \"address\": \"calle 1 # 2-68\",\n        \"email\": \"alanturing@enigmasas.com\",\n        \"phone\": \"1234567890\",\n        \"legal_organization_id\": \"2\",\n        \"tribute_id\": \"21\",\n        \"identification_document_id\": \"3\",\n        \"municipality_id\": \"980\"\n    },\n    \"items\": [\n        {\n            \"code_reference\": \"12345\",\n            \"name\": \"producto de prueba\",\n            \"quantity\": 1,\n            \"discount_rate\": 20,\n            \"price\": 50000,\n            \"tax_rate\": \"19.00\",\n            \"unit_measure_id\": 70,\n            \"standard_code_id\": 1,\n            \"is_excluded\": 0,\n            \"tribute_id\": 1,\n            \"withholding_taxes\": [\n                {\n                    \"code\": \"06\",\n                    \"withholding_tax_rate\": \"7.00\"\n                },\n                {\n                    \"code\": \"05\",\n                    \"withholding_tax_rate\": \"15.00\"\n                }\n            ]\n        },\n        {\n            \"code_reference\": \"54321\",\n            \"name\": \"producto de prueba 2\",\n            \"quantity\": 1,\n            \"discount_rate\": 0,\n            \"price\": 50000,\n            \"tax_rate\": \"5.00\",\n            \"unit_measure_id\": 70,\n            \"standard_code_id\": 1,\n            \"is_excluded\": 0,\n            \"tribute_id\": 1,\n            \"withholding_taxes\": []\n        }\n    ]\n}", null, "application/json");
                request.Content = content;

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
