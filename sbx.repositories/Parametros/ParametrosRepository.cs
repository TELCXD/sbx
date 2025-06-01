using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Parametros;
using sbx.core.Interfaces.Parametros;

namespace sbx.repositories.Parametros
{
    public class ParametrosRepository: IParametros
    {
        private readonly string _connectionString;

        public ParametrosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> List(string Nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string where = $" WHERE Nombre = '{Nombre}' ";

                    string sql = "SELECT IdParametro, Nombre, Value FROM T_Parametros";

                    if (Nombre != "") 
                    {
                        sql = sql + where;
                    }

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

        public async Task<Response<dynamic>> Update(List<ParametrosEntitie> parametrosEntitie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";
                    int Correcto = 0;
                    int Error = 0;

                    foreach (var item in parametrosEntitie)
                    {
                        sql = @$" UPDATE T_Parametros SET Value = @Value
                                  WHERE Nombre = @Nombre";

                        var parametros = new
                        {
                            item.Nombre,
                            item.Value
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            Correcto++;
                        }
                        else
                        {
                            Error++;
                        }
                    }
                    response.Flag = true;
                    response.Message = $"Proceso finalizado, Correctos: {Correcto} - Errores: {Error}";
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
