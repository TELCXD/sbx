using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Cotizacion;
using sbx.core.Interfaces.Cotizacion;

namespace sbx.repositories.Cotizacion
{
    public class CotizacionRepository : ICotizacion
    {
        private readonly string _connectionString;

        public CotizacionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateCotizacion(CotizacionEntitie cotizacionEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                await connection.OpenAsync();

                using var transaction = connection.BeginTransaction();

                try
                {
                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));
                    DateTime FechaVencimiento = FechaActual.AddDays(cotizacionEntitie.DiasVencimiento);

                    sql = @$" INSERT INTO T_Cotizacion (IdListaPrecio,IdCliente,IdVendedor,DiasVencimiento,Estado,FechaVencimiento,CreationDate, IdUserAction)
                                  VALUES(@IdListaPrecio,@IdCliente,@IdVendedor,@DiasVencimiento,@Estado,@FechaVencimiento,@CreationDate,@IdUserAction);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    var parametros = new
                    {
                        cotizacionEntitie.IdListaPrecio,
                        cotizacionEntitie.IdCliente,
                        cotizacionEntitie.IdVendedor,
                        cotizacionEntitie.DiasVencimiento,
                        cotizacionEntitie.Estado,
                        FechaVencimiento = FechaVencimiento,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idCotizacion = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_DetalleCotizacion (
                            IdCotizacion, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, CostoUnitario, Descuento, Impuesto,CreationDate, IdUserAction)
                            VALUES (@IdCotizacion, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction)";

                    foreach (var detalle in cotizacionEntitie.detalleCotizacion)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdCotizacion = idCotizacion,
                                detalle.IdProducto,
                                detalle.Sku,
                                detalle.CodigoBarras,
                                detalle.NombreProducto,
                                detalle.Cantidad,
                                detalle.UnidadMedida,
                                detalle.PrecioUnitario,
                                detalle.CostoUnitario,
                                detalle.Descuento,
                                detalle.Impuesto,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Data = idCotizacion;
                    response.Message = "Cotizacion creada correctamente";
                    return response;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

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
                                    A.IdCotizacion,
                                    CONCAT('CTZ','-',A.IdCotizacion) Cotizacion,
                                    A.CreationDate FechaCotizacion,
                                    A.IdUserAction IdUserActionCotizacion,
                                    A.IdListaPrecio,
                                    A.IdVendedor,
                                    A.Estado,
									A.DiasVencimiento,
									A.FechaVencimiento,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameCotizacion,
                                    B.IdDetalleCotizacion,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.UnidadMedida,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleCotizacion,
                                    B.IdUserAction IdUserActionDetalleCotizacion,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial
                                    FROM T_Cotizacion A
                                    INNER JOIN T_DetalleCotizacion B ON A.IdCotizacion = B.IdCotizacion
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdCotizacion = {Id}";
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin, int idUser, string RolName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string FiltroPorUsuario = "";

                    if (RolName != "Administrador")
                    {
                        FiltroPorUsuario = $" AND A.IdUserAction = {idUser} ";
                    }

                    string sql = @"SELECT 
                                    A.IdCotizacion,
                                    CONCAT('CTZ','-',A.IdCotizacion) Cotizacion,
                                    A.CreationDate FechaCotizacion,
                                    A.IdUserAction IdUserActionCotizacion,
                                    A.IdVendedor,
                                    A.Estado,
									A.DiasVencimiento,
									A.FechaVencimiento,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameCotizacion,
                                    B.IdDetalleCotizacion,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.UnidadMedida,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleCotizacion,
                                    B.IdUserAction IdUserActionDetalleCotizacion,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial
                                    FROM T_Cotizacion A
                                    INNER JOIN T_DetalleCotizacion B ON A.IdCotizacion = B.IdCotizacion
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor
                                    WHERE 
                                    A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120) " + FiltroPorUsuario;

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    if (clientVenta == "Cliente")
                                    {
                                        Where = $" AND D.NombreRazonSocial LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName LIKE @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento LIKE @Filtro ";
                                    break;
                                case "Id":
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.IdProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND A.IdUserAction LIKE @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT('CTZ','-',A.IdCotizacion) LIKE @Filtro ";
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
                                    if (clientVenta == "Cliente")
                                    {
                                        Where = $" AND D.NombreRazonSocial = @Filtro ";
                                    }
                                    else if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto = @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName = @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.IdProducto = @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND A.IdUserAction = @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras = @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT('CTZ','-',A.IdCotizacion) = @Filtro ";
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
                                    if (clientVenta == "Cliente")
                                    {
                                        Where = $" AND D.NombreRazonSocial LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName LIKE @Filtro ";
                                    }
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.IdProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND A.IdUserAction LIKE @Filtro ";
                                    }
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT('CTZ','-',A.IdCotizacion) LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " ORDER BY A.CreationDate DESC ";

                    dynamic resultado = await connection.QueryAsync(sql, new { Filtro, FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> CambioEstadoCotizacion(int IdCotizacion, string Estado, int IdVenta, int IdUser)
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

                    sql = @$" UPDATE T_Cotizacion SET Estado = @Estado, IdVenta = @IdVenta,UpdateDate = @UpdateDate, IdUserAction = @IdUserAction WHERE IdCotizacion = @IdCotizacion ";

                    var parametros = new
                    {
                        Estado,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser,
                        IdCotizacion,
                        IdVenta
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Estado de cotizacion actualizada correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Se presento un error al intentar actualizar estado de cotizacion";
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
