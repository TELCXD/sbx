using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.ResponsabilidadTributaria;

namespace sbx.repositories.ResponsabilidadTributaria
{
    public class ResponsabilidadTributariaRepository : IResponsabilidadTributaria
    {
        private readonly string _connectionString;

        public ResponsabilidadTributariaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> ListResponsabilidadTributaria()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "SELECT IdResponsabilidadTributaria, Code, Nombre FROM T_ResponsabilidadTributaria";

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
