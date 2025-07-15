using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.CrendencialesApi;
using sbx.core.Interfaces.CredencialesApi;

namespace sbx.repositories.CredencialesApi
{
    public class CredencialesApiRepository: ICredencialesApi
    {
        private readonly string _connectionString;

        public CredencialesApiRepository(string connectionString)
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

                    string sql = @" SELECT 
                                     IdCredencialesApi
                                    ,grant_type
                                    ,client_id
                                    ,client_secret
                                    ,username
                                    ,Passwords
                                    FROM T_CredencialesApi ";

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

        public async Task<Response<dynamic>> CreateUpdate(CredencialesApiEntitie credencialesApiEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (credencialesApiEntitie.IdCredencialesApi > 0)
                    {
                        sql = @$" UPDATE T_CredencialesApi SET grant_type = @grant_type,
                                  client_id=@client_id,client_secret=@client_secret,
                                  username=@username,Passwords=@Passwords,
                                  UpdateDate=@UpdateDate,IdUserAction=@IdUserAction
                                  WHERE IdCredencialesApi = @IdCredencialesApi";

                        var parametros = new
                        {
                            credencialesApiEntitie.IdCredencialesApi,
                            credencialesApiEntitie.grant_type,
                            credencialesApiEntitie.client_id,
                            credencialesApiEntitie.client_secret,
                            credencialesApiEntitie.username,
                            credencialesApiEntitie.Passwords,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Credenciales Api actualizada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar Credenciales Api";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_CredencialesApi (grant_type,client_id,client_secret,username,Passwords,
                                  CreationDate,IdUserAction)
                                  VALUES(@grant_type,@client_id,@client_secret,@username,@Passwords,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            credencialesApiEntitie.IdCredencialesApi,
                            credencialesApiEntitie.grant_type,
                            credencialesApiEntitie.client_id,
                            credencialesApiEntitie.client_secret,
                            credencialesApiEntitie.username,
                            credencialesApiEntitie.Passwords,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Credenciales Api creada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear credenciales Api";
                        }
                    }

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
