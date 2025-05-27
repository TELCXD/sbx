using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Promociones;
using sbx.core.Interfaces.Promociones;

namespace sbx.repositories.Promociones
{
    public class PromocionesRepository : IPromociones
    {
        private readonly string _connectionString;

        public PromocionesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdPromocion
                                  ,A.NombrePromocion
                                  ,A.IdTipoPromocion
                                  ,B.Nombre NombreTipoPromocion
                                  ,A.Porcentaje
                                  ,A.FechaInicio
                                  ,A.FechaFin
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  FROM T_Promociones A
                                  INNER JOIN T_TipoPromocion B ON A.IdTipoPromocion = B.IdTipoPromocion ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.NombrePromocion LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato + "%";
                            break;
                        case "Igual a":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.NombrePromocion = @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato;
                            break;
                        case "Contiene":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.NombrePromocion LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where;

                    dynamic resultado = await connection.QueryAsync(sql, new { Filtro });

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

        public async Task<Response<dynamic>> CreateUpdate(PromocionesEntitie promocionesEntitie, int IdUser)
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

                    var parametros = new
                    {
                        promocionesEntitie.IdPromocion,
                        promocionesEntitie.NombrePromocion,
                        promocionesEntitie.IdTipoPromocion,
                        promocionesEntitie.Porcentaje,
                        promocionesEntitie.FechaInicio,
                        promocionesEntitie.FechaFin,
                        CreationDate = FechaActual,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    if (promocionesEntitie.IdPromocion > 0)
                    {
                        sql = @$" UPDATE T_Promociones SET 
                                  NombrePromocion = @NombrePromocion,
                                  IdTipoPromocion = @IdTipoPromocion,
                                  Porcentaje = @Porcentaje,
                                  FechaInicio = @FechaInicio,
                                  FechaFin = @FechaFin,
                                  UpdateDate = @UpdateDate, 
                                  IdUserAction = @IdUserAction
                                  WHERE IdPromocion = @IdPromocion";

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "promocion actualizada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar promocion";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Promociones (NombrePromocion,IdTipoPromocion,Porcentaje,FechaInicio,FechaFin,
                                  CreationDate, IdUserAction)
                                  VALUES(@NombrePromocion,@IdTipoPromocion,@Porcentaje,@FechaInicio,@FechaFin,@CreationDate,@IdUserAction); 
                                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int idLista = await connection.ExecuteScalarAsync<int>(sql, parametros);

                        if (idLista > 0)
                        {
                            response.Flag = true;
                            response.Message = "Promocion creada correctamente";
                            response.Data = idLista;
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear promocion";
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
                                   A.IdPromocion
                                  ,A.NombrePromocion
                                  ,A.IdTipoPromocion
                                  ,B.Nombre NombreTipoPromocion
                                  ,A.Porcentaje
                                  ,A.FechaInicio
                                  ,A.FechaFin
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  FROM T_Promociones A
                                  INNER JOIN T_TipoPromocion B ON A.IdTipoPromocion = B.IdTipoPromocion ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdPromocion = {Id}";
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
