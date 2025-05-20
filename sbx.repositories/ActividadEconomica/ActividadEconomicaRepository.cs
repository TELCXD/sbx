using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.ActividadEconomica;

namespace sbx.repositories.ActividadEconomica
{
    public class ActividadEconomicaRepository: IActividadEconomica
    {
        private readonly string _connectionString;

        public ActividadEconomicaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> ListActividadEconomica()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "SELECT IdActividadEconomica, Code, Nombre FROM T_ActividadEconomica";

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
