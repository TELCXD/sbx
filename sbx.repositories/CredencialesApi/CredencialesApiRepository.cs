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

                    string sql = @"  SELECT 
                                     IdCredencialesApi
                                    ,url_api
                                    ,Descripcion
                                    ,Variable1
                                    ,Variable2
                                    ,Variable3
                                    ,Variable4
                                    ,Variable5
                                    ,Variable6
                                    ,Variable7
                                    ,Variable8
                                    ,Variable9
                                    ,Variable10
                                    ,Variable11
                                    ,Variable12
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

        public async Task<Response<dynamic>> ListId(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@" SELECT 
                                     IdCredencialesApi
                                    ,url_api
                                    ,Descripcion
                                    ,Variable1
                                    ,Variable2
                                    ,Variable3
                                    ,Variable4
                                    ,Variable5
                                    ,Variable6
                                    ,Variable7
                                    ,Variable8
                                    ,Variable9
                                    ,Variable10
                                    ,Variable11
                                    ,Variable12
                                    FROM T_CredencialesApi 
                                    WHERE IdCredencialesApi = {Id} ";

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
                        sql = @$" UPDATE T_CredencialesApi SET Descripcion = @Descripcion,
                                  url_api = @url_api,Variable1 = @Variable1,
                                  Variable2=@Variable2,Variable3=@Variable3,
                                  Variable4=@Variable4,Variable5=@Variable5,
                                  Variable6=@Variable6,Variable7=@Variable7,
                                  Variable8=@Variable8,Variable9=@Variable9,
                                  Variable10=@Variable10,Variable11=@Variable11,
                                  Variable12=@Variable12,
                                  UpdateDate=@UpdateDate,IdUserAction=@IdUserAction
                                  WHERE IdCredencialesApi = @IdCredencialesApi";

                        var parametros = new
                        {
                            credencialesApiEntitie.IdCredencialesApi,
                            credencialesApiEntitie.url_api,
                            credencialesApiEntitie.Descripcion,
                            credencialesApiEntitie.Variable1,
                            credencialesApiEntitie.Variable2,
                            credencialesApiEntitie.Variable3,
                            credencialesApiEntitie.Variable4,
                            credencialesApiEntitie.Variable5,
                            credencialesApiEntitie.Variable6,
                            credencialesApiEntitie.Variable7,
                            credencialesApiEntitie.Variable8,
                            credencialesApiEntitie.Variable9,
                            credencialesApiEntitie.Variable10,
                            credencialesApiEntitie.Variable11,
                            credencialesApiEntitie.Variable12,
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
                        sql = @$" INSERT INTO T_CredencialesApi (url_api,Descripcion,Variable1,Variable2,Variable3,Variable4,Variable5,
                                  Variable6,Variable7,Variable8,Variable9,Variable10,Variable11,Variable12,CreationDate,IdUserAction)
                                  VALUES(@url_api,@Descripcion,@Variable1,@Variable2,@Variable3,@Variable4,@Variable5,
                                         @Variable6,@Variable7,@Variable8,@Variable9,@Variable10,
                                         @Variable11,@Variable12,@CreationDate,@IdUserAction) ";

                        var parametros = new
                        {
                            credencialesApiEntitie.IdCredencialesApi,
                            credencialesApiEntitie.url_api,
                            credencialesApiEntitie.Descripcion,
                            credencialesApiEntitie.Variable1,
                            credencialesApiEntitie.Variable2,
                            credencialesApiEntitie.Variable3,
                            credencialesApiEntitie.Variable4,
                            credencialesApiEntitie.Variable5,
                            credencialesApiEntitie.Variable6,
                            credencialesApiEntitie.Variable7,
                            credencialesApiEntitie.Variable8,
                            credencialesApiEntitie.Variable9,
                            credencialesApiEntitie.Variable10,
                            credencialesApiEntitie.Variable11,
                            credencialesApiEntitie.Variable12,
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

        public async Task<Response<dynamic>> Eliminar(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $"DELETE FROM T_CredencialesApi WHERE IdCredencialesApi = {Id}";

                    var rowsAffected = await connection.ExecuteAsync(sql);

                    string Mensaje = "";

                    if (rowsAffected > 0)
                    {
                        Mensaje = "Se elimino correctamente";
                        response.Flag = true;
                    }
                    else
                    {
                        Mensaje = "Se presento un error al intentar eliminar";
                        response.Flag = false;
                    }
                
                    response.Message = Mensaje;
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
