using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.PromocionProducto;
using sbx.core.Interfaces.PromocionProducto;

namespace sbx.repositories.PromocionProducto
{
    public class PromocionProductoRepository : IPromocionProducto
    {
        private readonly string _connectionString;

        public PromocionProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
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
                                    A.IdPromocion,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.Nombre
                                    FROM T_PromocionesProductos A
                                    INNER JOIN T_Productos B ON A.IdProducto = B.IdProducto ";

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

        public async Task<Response<dynamic>> CreateUpdate(PromocionProductoEntitie promocionProductoEntitie, int IdUser)
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

                    sql = $@"MERGE T_PromocionesProductos AS target
                            USING (SELECT @IdPromocion AS IdPromocion, @IdProducto AS IdProducto) AS source
                            ON target.IdPromocion = source.IdPromocion AND target.IdProducto = source.IdProducto
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    UpdateDate = @UpdateDate,
                                    IdUserAction = @IdUserAction
                            WHEN NOT MATCHED THEN
                                INSERT (IdPromocion, IdProducto, CreationDate, IdUserAction)
                                VALUES (@IdPromocion, @IdProducto, @CreationDate, @IdUserAction); ";

                    var parametros = new
                    {
                        promocionProductoEntitie.IdPromocion,
                        promocionProductoEntitie.IdProducto,
                        CreationDate = FechaActual,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = promocionProductoEntitie.IdPromocion > 0 ? "Promocion actualizada correctamente" : "Promocion creada correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = promocionProductoEntitie.IdPromocion > 0 ? "Se presento un error al intentar actualizar Promocion" : "Se presento un error al intentar crear Promocion";
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

        public async Task<Response<dynamic>> RemoveProducto(PromocionProductoEntitie promocionProductoEntitie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    sql = $@" DELETE FROM T_PromocionesProductos WHERE IdPromocion = @IdPromocion AND IdProducto = @IdProducto ";

                    var parametros = new
                    {
                        promocionProductoEntitie.IdPromocion,
                        promocionProductoEntitie.IdProducto,
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Producto eliminado de la promocion correctamente";
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

        public async Task<Response<dynamic>> PromocionActiva(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                    string sql = $@"SELECT A.IdPromocion,B.IdProducto, A.IdTipoPromocion, C.Nombre TipoPromocion, A.FechaInicio, A.FechaFin, A.Porcentaje
                                            FROM T_Promociones A
                                            INNER JOIN T_PromocionesProductos B ON A.IdPromocion = B.IdPromocion
                                            INNER JOIN T_TipoPromocion C ON A.IdTipoPromocion = C.IdTipoPromocion
                                    WHERE   IdProducto = {IdProducto} AND @FechaActual >= FechaInicio AND @FechaActual <= FechaFin ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaActual });

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
