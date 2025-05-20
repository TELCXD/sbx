using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Entities.Tienda;
using sbx.core.Interfaces.RangoNumeracion;

namespace sbx.repositories.RangoNumeracion
{
    public class RangoNumeracionRepository : IRangoNumeracion
    {
        private readonly string _connectionString;

        public RangoNumeracionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(RangoNumeracionEntitie rangoNumeracionEntitie, int IdUser)
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

                    if (rangoNumeracionEntitie.Id_RangoNumeracion > 0)
                    {
                        sql = @$" UPDATE T_RangoNumeracion SET 
                                  Id_TipoDocumentoRangoNumeracion = @Id_TipoDocumentoRangoNumeracion,
                                  Prefijo = @Prefijo,
                                  NumeroDesde = @NumeroDesde, NumeroHasta = @NumeroHasta,
                                  NumeroAutorizacion = @NumeroAutorizacion, FechaVencimiento = @FechaVencimiento,
                                  Active = @Active, NumeracionAutorizada = @NumeracionAutorizada, UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction
                                  WHERE Id_RangoNumeracion = @Id_RangoNumeracion";

                        var parametros = new
                        {
                            Id_RangoNumeracion = rangoNumeracionEntitie.Id_RangoNumeracion,
                            Id_TipoDocumentoRangoNumeracion = rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion,
                            Prefijo = rangoNumeracionEntitie.Prefijo,
                            NumeroDesde = rangoNumeracionEntitie.NumeroDesde,
                            NumeroHasta = rangoNumeracionEntitie.NumeroHasta,
                            NumeroAutorizacion = rangoNumeracionEntitie.NumeroAutorizacion,
                            FechaVencimiento = rangoNumeracionEntitie.FechaVencimiento,
                            Active = rangoNumeracionEntitie.Active,
                            NumeracionAutorizada = rangoNumeracionEntitie.NumeracionAutorizada,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Rango numeracion actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar rango numeracion";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_RangoNumeracion (Id_TipoDocumentoRangoNumeracion,Prefijo,NumeroDesde,
                                  NumeroHasta,NumeroAutorizacion,FechaVencimiento,Active,NumeracionAutorizada,CreationDate,IdUserAction)
                                  VALUES(@Id_TipoDocumentoRangoNumeracion,@Prefijo,@NumeroDesde,
                                  @NumeroHasta,@NumeroAutorizacion,@FechaVencimiento,@Active,@NumeracionAutorizada,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            Id_TipoDocumentoRangoNumeracion = rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion,
                            Prefijo = rangoNumeracionEntitie.Prefijo,
                            NumeroDesde = rangoNumeracionEntitie.NumeroDesde,
                            NumeroHasta = rangoNumeracionEntitie.NumeroHasta,
                            NumeroAutorizacion = rangoNumeracionEntitie.NumeroAutorizacion,
                            FechaVencimiento = rangoNumeracionEntitie.FechaVencimiento,
                            Active = rangoNumeracionEntitie.Active,
                            NumeracionAutorizada = rangoNumeracionEntitie.NumeracionAutorizada,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Rango numeracion creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Rango numeracion";
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

        public async Task<Response<dynamic>> List(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   Id_RangoNumeracion
                                  ,Id_TipoDocumentoRangoNumeracion
                                  ,Prefijo
                                  ,NumeroDesde
                                  ,NumeroHasta
                                  ,NumeroAutorizacion
                                  ,FechaVencimiento
                                  ,Active
                                  ,NumeracionAutorizada
                                  ,CreationDate
                                  ,UpdateDate
                                  ,IdUserAction
                                  FROM T_RangoNumeracion ";

                    string Where = "";

                    if (Id > 0) 
                    {
                        Where = $"WHERE Id_RangoNumeracion = {Id}";
                        sql += Where;
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
    }
}
