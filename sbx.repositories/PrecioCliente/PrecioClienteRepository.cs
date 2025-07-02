using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.PrecioCliente;
using sbx.core.Interfaces.PrecioCliente;

namespace sbx.repositories.PrecioCliente
{
    public class PrecioClienteRepository : IPrecioCliente
    {
        private readonly string _connectionString;

        public PrecioClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    CONCAT(A.IdCliente,A.IdProducto) llavePrimaria,
                                    B.IdCliente,
                                    B.IdIdentificationType,
                                    D.IdentificationType,
                                    B.NumeroDocumento,
                                    B.NombreRazonSocial,
                                    E.Nombre TipoCliente,
                                    C.IdProducto,
                                    C.Sku,
                                    C.CodigoBarras,
                                    C.Nombre,
                                    A.PrecioEspecial,
                                    A.FechaInicio,
                                    A.FechaFin
                                    FROM T_PreciosCliente A
                                    INNER JOIN T_Cliente B ON B.IdCliente = A.IdCliente
                                    INNER JOIN T_IdentificationType D ON D.IdIdentificationType = B.IdIdentificationType
                                    INNER JOIN T_Productos C ON C.IdProducto = A.IdProducto 
                                    INNER JOIN T_TipoCliente E ON E.IdTipoCliente = B.IdTipoCliente ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    if (clientProducto == "Cliente") 
                                    {
                                        Where = $"WHERE B.NombreRazonSocial LIKE @Filtro ";
                                    }
                                    else if(clientProducto == "Producto")
                                    {
                                        Where = $"WHERE C.Nombre LIKE @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE B.NumeroDocumento LIKE @Filtro ";                                    
                                    break;
                                case "Id":
                                    Where = $"WHERE C.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE C.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE C.CodigoBarras LIKE @Filtro ";
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
                                    if (clientProducto == "Cliente")
                                    {
                                        Where = $"WHERE B.NombreRazonSocial = @Filtro ";
                                    }
                                    else if (clientProducto == "Producto")
                                    {
                                        Where = $"WHERE C.Nombre = @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE B.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    Where = $"WHERE C.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE C.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE C.CodigoBarras = @Filtro ";
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
                                    if (clientProducto == "Cliente")
                                    {
                                        Where = $"WHERE B.NombreRazonSocial LIKE @Filtro ";
                                    }
                                    else if (clientProducto == "Producto")
                                    {
                                        Where = $"WHERE C.Nombre LIKE @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE B.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    Where = $"WHERE C.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE C.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE C.CodigoBarras LIKE @Filtro ";
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

        public async Task<Response<dynamic>> CreateUpdate(PrecioClienteEntitie precioCliente, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                await connection.OpenAsync();

                try
                {
                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (precioCliente.llavePrimaria > 0)
                    {
                        sql = @"UPDATE T_PreciosCliente SET IdProducto = @IdProducto, PrecioEspecial = @PrecioEspecial,
                                FechaInicio = @FechaInicio, FechaFin = @FechaFin, UpdateDate = @UpdateDate, IdUserAction = @IdUserAction
                                WHERE IdCliente = @IdCliente AND IdProducto = @IdProducto;";
                    }
                    else
                    {
                        sql = @"INSERT INTO T_PreciosCliente (IdCliente, IdProducto, PrecioEspecial, FechaInicio, FechaFin, CreationDate, IdUserAction)
                                VALUES (@IdCliente, @IdProducto, @PrecioEspecial, @FechaInicio, @FechaFin, @CreationDate,@IdUserAction);";
                    }

                    int FilasAfectadas = await connection.ExecuteAsync(sql,
                    new
                    {
                        precioCliente.IdCliente,
                        precioCliente.IdProducto,
                        precioCliente.PrecioEspecial,
                        precioCliente.FechaInicio,
                        precioCliente.FechaFin,
                        CreationDate = FechaActual,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    });

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = precioCliente.llavePrimaria > 0 ?  "Precio cliente actualizado correctamente" : "Precio cliente creado correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = precioCliente.llavePrimaria > 0 ? "Se presento un error al intentar actualizar precio cliente" : "Se presento un error al intentar crear precio cliente";
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

        public async Task<Response<dynamic>> List(long Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT 
                                    CONCAT(A.IdCliente,A.IdProducto) llavePrimaria,
                                    B.IdCliente,
                                    B.IdIdentificationType,
                                    D.IdentificationType,
                                    B.NumeroDocumento,
                                    B.NombreRazonSocial,
                                    E.Nombre TipoCliente,
                                    C.IdProducto,
                                    C.Sku,
                                    C.CodigoBarras,
                                    C.Nombre,
                                    A.PrecioEspecial,
                                    A.FechaInicio,
                                    A.FechaFin
                                    FROM T_PreciosCliente A
                                    INNER JOIN T_Cliente B ON B.IdCliente = A.IdCliente
                                    INNER JOIN T_IdentificationType D ON D.IdIdentificationType = B.IdIdentificationType
                                    INNER JOIN T_Productos C ON C.IdProducto = A.IdProducto 
                                    INNER JOIN T_TipoCliente E ON E.IdTipoCliente = B.IdTipoCliente 
                                    WHERE CONCAT(A.IdCliente,A.IdProducto) = {Id} ";

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

        public async Task<Response<dynamic>> PrecioClientePersonalizado(int IdProducto, int IdCliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd"));

                    string sql = $@"SELECT IdCliente, IdProducto,PrecioEspecial,FechaInicio,FechaFin 
                                    FROM T_PreciosCliente
                                    WHERE IdProducto = {IdProducto} AND IdCliente = {IdCliente} AND (@FechaActual >= FechaInicio  AND @FechaActual <= FechaFin) ";

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

        public async Task<Response<dynamic>> Eliminar(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string Mensaje = "";
                    string sql = $"DELETE FROM T_PreciosCliente WHERE CONCAT(IdCliente, IdProducto) = {Id}";

                    var rowsAffected = await connection.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        Mensaje = "Se elimino correctamente el precio de cliente";
                        response.Flag = true;
                    }
                    else
                    {
                        Mensaje = "Se presento un error al intentar eliminar el precio de cliente";
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
