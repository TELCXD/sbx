using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.AgrupacionProducto;
using sbx.core.Interfaces.ConversionProducto;

namespace sbx.repositories.ConversionProducto
{
    public class ConversionProductoRepository : IConversionProducto
    {

        private readonly string _connectionString;

        public ConversionProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string PadreHijo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    A.IdProductoPadre,
                                    B.Sku SkuPadre,
                                    B.CodigoBarras CodigoBarraasPadre,
                                    B.Nombre NombrePadre,
                                    A.IdProductoHijo,
                                    C.Sku SkuHijo,
                                    C.CodigoBarras CodigoBarrasHijo,
                                    C.Nombre NombreHijo,
                                    A.Cantidad,
                                    A.CreationDate,
                                    A.UpdateDate,
                                    A.IdUserAction
                                    FROM T_ConversionesProducto A
                                    INNER JOIN T_Productos B ON A.IdProductoPadre = B.IdProducto
                                    INNER JOIN T_Productos C ON A.IdProductoHijo = C.IdProducto
                                    INNER JOIN T_User D ON A.IdUserAction = D.IdUser ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    if (PadreHijo == "Padre") 
                                    {
                                        Where = $"WHERE REPLACE(B.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE REPLACE(C.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    }
                                    break;
                                case "Id":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE A.IdProductoPadre LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE A.IdProductoHijo LIKE @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.Sku LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.Sku LIKE @Filtro ";
                                    }
                                    break;
                                case "Codigo barras":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.CodigoBarras LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.CodigoBarras LIKE @Filtro ";
                                    }
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
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE REPLACE(B.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE REPLACE(C.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    }
                                    break;
                                case "Id":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE A.IdProductoPadre = @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE A.IdProductoHijo = @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.Sku = @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.Sku = @Filtro ";
                                    }
                                    break;
                                case "Codigo barras":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.CodigoBarras = @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.CodigoBarras = @Filtro ";
                                    }
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
                                    Where = $"WHERE  ";

                                    foreach (string parte in DatoSeparado)
                                    {
                                        if (PadreHijo == "Padre")
                                        {
                                            Where += $" REPLACE(B.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                        }
                                        else
                                        {
                                            Where += $" REPLACE(C.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                        }
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE A.IdProductoPadre LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE A.IdProductoHijo LIKE @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.Sku LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.Sku LIKE @Filtro ";
                                    }   
                                    break;
                                case "Codigo barras":
                                    if (PadreHijo == "Padre")
                                    {
                                        Where = $"WHERE B.CodigoBarras LIKE @Filtro ";
                                    }
                                    else
                                    {
                                        Where = $"WHERE C.CodigoBarras LIKE @Filtro ";
                                    }
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

                    sql += PadreHijo == "Padre" ? " ORDER BY REPLACE(B.Nombre, ' ', '') " : " ORDER BY REPLACE(C.Nombre, ' ', '') ";

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

        public async Task<Response<dynamic>> CreateUpdate(ConversionProductoEntitie conversionProductoEntitie, int IdUser)
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

                    if (Convert.ToInt64(conversionProductoEntitie.Llave) > 0)
                    {
                        sql = @$" UPDATE T_ConversionesProducto 
                                  SET 
                                  IdProductoPadre = @IdProductoPadre,
                                  IdProductoHijo = @IdProductoHijo,
                                  Cantidad = @Cantidad, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE CONCAT(IdProductoPadre,IdProductoHijo) = @Llave";

                        var parametros = new
                        {
                            IdProductoPadre = conversionProductoEntitie.IdProductoPadre,
                            IdProductoHijo = conversionProductoEntitie.IdProductoHijo,
                            Cantidad = conversionProductoEntitie.Cantidad,
                            Llave = Convert.ToInt32(conversionProductoEntitie.Llave),
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Conversion de producto actualizada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar conversion de producto";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_ConversionesProducto (IdProductoPadre,IdProductoHijo,Cantidad,CreationDate, IdUserAction)
                                  VALUES(@IdProductoPadre,@IdProductoHijo,@Cantidad,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdProductoPadre = conversionProductoEntitie.IdProductoPadre,
                            IdProductoHijo = conversionProductoEntitie.IdProductoHijo,
                            Cantidad = conversionProductoEntitie.Cantidad,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Conversion de producto creada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear conversion de producto";
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

        public async Task<Response<dynamic>> List(Int64 Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    A.IdProductoPadre,
                                    B.Sku SkuPadre,
                                    B.CodigoBarras CodigoBarraasPadre,
                                    B.Nombre NombrePadre,
                                    A.IdProductoHijo,
                                    C.Sku SkuHijo,
                                    C.CodigoBarras CodigoBarrasHijo,
                                    C.Nombre NombreHijo,
                                    A.Cantidad,
                                    A.CreationDate,
                                    A.UpdateDate,
                                    A.IdUserAction
                                    FROM T_ConversionesProducto A
                                    INNER JOIN T_Productos B ON A.IdProductoPadre = B.IdProducto
                                    INNER JOIN T_Productos C ON A.IdProductoHijo = C.IdProducto
                                    INNER JOIN T_User D ON A.IdUserAction = D.IdUser ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE CONCAT(IdProductoPadre,IdProductoHijo) = {Id}";
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

        public async Task<Response<dynamic>> Eliminar(Int64 Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @$" DELETE FROM T_ConversionesProducto WHERE CONCAT(IdProductoPadre,IdProductoHijo) = {Id} ";

                    int FilasAfectadas = await connection.ExecuteAsync(sql);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Conversion de producto eliminada correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Se presento un error al intentar eliminar conversion de producto";
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
