using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.PrecioProducto;
using sbx.core.Interfaces.PrecioProducto;

namespace sbx.repositories.PrecioProducto
{
    public class PrecioProductoRepository: IPrecioProducto
    {
        private readonly string _connectionString;

        public PrecioProductoRepository(string connectionString)
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
                                    A.IdListaPrecio,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.Nombre,
                                    A.Precio
                                    FROM T_PreciosProducto A
                                    INNER JOIN T_Productos B ON A.IdProducto = B.IdProducto ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdListaPrecio = {Id}";
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

        public async Task<Response<dynamic>> CreateUpdate(PrecioProductoEntitie precioProductoEntitie, int IdUser)
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

                    sql = $@"MERGE T_PreciosProducto AS target
                            USING (SELECT @IdListaPrecio AS IdListaPrecio, @IdProducto AS IdProducto) AS source
                            ON target.IdListaPrecio = source.IdListaPrecio AND target.IdProducto = source.IdProducto
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    Precio = @Precio,
                                    UpdateDate = @UpdateDate,
                                    IdUserAction = @IdUserAction
                            WHEN NOT MATCHED THEN
                                INSERT (IdListaPrecio, IdProducto, Precio, CreationDate, IdUserAction)
                                VALUES (@IdListaPrecio, @IdProducto, @Precio, @CreationDate, @IdUserAction); ";

                    var parametros = new
                    {
                        precioProductoEntitie.IdListaPrecio,
                        precioProductoEntitie.IdProducto,
                        precioProductoEntitie.Precio,
                        CreationDate = FechaActual,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = precioProductoEntitie.IdListaPrecio > 0 ? "Lista precios actualizada correctamente" : "Lista precios creada correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = precioProductoEntitie.IdListaPrecio > 0 ? "Se presento un error al intentar actualizar lista de precios" : "Se presento un error al intentar crear lista de precios";
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

        public async Task<Response<dynamic>> RemoveProducto(PrecioProductoEntitie precioProductoEntitie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    sql = $@" DELETE FROM T_PreciosProducto WHERE IdListaPrecio = @IdListaPrecio AND IdProducto = @IdProducto ";

                    var parametros = new
                    {
                        precioProductoEntitie.IdListaPrecio,
                        precioProductoEntitie.IdProducto,
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Producto eliminado de la lista correctamente";
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

        public async Task<Response<dynamic>> PrecioListaPreciosTipoCliente(int IdProducto, int IdListaPrecio)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                    string sql = $@"SELECT A.NombreLista,A.IdTipoCliente,C.Nombre TipoCliente,A.FechaInicio, A.FechaFin, B.Precio 
                                    FROM T_ListasPrecios A
                                    INNER JOIN T_PreciosProducto B ON A.IdListaPrecio = B.IdListaPrecio
                                    INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente
                                    WHERE B.IdListaPrecio = {IdListaPrecio} AND IdProducto = {IdProducto} AND FechaInicio >= '{FechaActual}' AND FechaFin <= '{FechaActual}' ";

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
