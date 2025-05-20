using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.TipoDocumentoRangoNumeracion;

namespace sbx.repositories.TipoDocumentoRangoNumeracion
{
    public class TipoDocumentoRangoNumeracionRepository: ITipoDocumentoRangoNumeracion
    {
        private readonly string _connectionString;

        public TipoDocumentoRangoNumeracionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> ListTipoDocumentoRangoNumeracion()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "SELECT Id_TipoDocumentoRangoNumeracion, Nombre FROM T_TipoDocumentoRangoNumeracion";

                    dynamic resultado = await connection.QueryAsync(sql);

                    response.Flag = true;
                    response.Message = "Proceso realizado correctamente";
                    response.Data = resultado;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Flag = false;
                    response.Message = "Error: " + ex.Message;
                    return response;
                }
            }
        }
    }
}
