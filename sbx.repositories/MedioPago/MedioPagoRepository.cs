using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.MedioPago;

namespace sbx.repositories.MedioPago
{
    public class MedioPagoRepository: IMedioPago
    {
        private readonly string _connectionString;

        public MedioPagoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> List()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    IdMetodoPago,
                                    Codigo,
                                    Nombre,
                                    RequiereReferencia,
                                    PermiteVuelto,
                                    TieneComision,
                                    PorcentajeComision,
                                    Activo  
                                    FROM T_MetodoPago 
                                    ORDER BY IdMetodoPago ";

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
