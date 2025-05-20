using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
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
    }
}
