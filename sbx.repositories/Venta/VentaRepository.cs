using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.Venta;

namespace sbx.repositories.Venta
{
    public class VentaRepository : IVenta
    {
        private readonly string _connectionString;

        public VentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Create(VentaEntitie ventaEntitie, int IdUser)
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

                    sql = @$" INSERT INTO T_Ventas (Prefijo,Consecutivo,IdCliente,IdVendedor,IdMetodoPago,CreationDate, IdUserAction)
                                  VALUES(@Prefijo,
                                        (SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM T_Ventas WHERE Prefijo = @Prefijo),
                                        @IdCliente,@IdVendedor,@IdMetodoPago,@CreationDate,@IdUserAction);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var parametros = new
                    {
                        ventaEntitie.Prefijo,
                        ventaEntitie.IdCliente,
                        ventaEntitie.IdVendedor,
                        ventaEntitie.IdMetodoPago,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idVenta = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_DetalleVenta (
                            IdVenta, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, Descuento, Impuesto,CreationDate, IdUserAction)
                            VALUES (@IdVenta, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction)";

                    foreach (var detalle in ventaEntitie.detalleVentas)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta = idVenta,
                                detalle.IdProducto,
                                detalle.Sku,
                                detalle.CodigoBarras,
                                detalle.NombreProducto,
                                detalle.Cantidad,
                                detalle.UnidadMedida,
                                detalle.PrecioUnitario,
                                detalle.Descuento,
                                detalle.Impuesto,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    sql = @"INSERT INTO T_PagosVenta (
                            IdVenta, IdMetodoPago, Recibido, Monto, Referencia, IdBanco, CreationDate, IdUserAction)
                            VALUES (@IdVenta, @IdMetodoPago, @Recibido, @Monto, @Referencia, @IdBanco, @CreationDate, @IdUserAction)";

                    foreach (var detallePago in ventaEntitie.pagosVenta)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta = idVenta,
                                detallePago.IdMetodoPago,
                                detallePago.Recibido,
                                detallePago.Monto,
                                detallePago.Referencia,
                                detallePago.IdBanco,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Data = idVenta;
                    response.Message = "Venta creada correctamente";
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
                                    A.IdVenta,
                                    A.Prefijo,
                                    A.Consecutivo,
                                    CONCAT(A.Prefijo,'-',A.Consecutivo) Factura,
                                    A.CreationDate FechaFactura,
                                    A.IdUserAction IdUserActionFactura,
                                    A.IdVendedor,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameFactura,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.UnidadMedida,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleFactura,
                                    B.IdUserAction IdUserActionDetalleFactura,
                                    H.UserName UserNameDetalleFactura,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial,
                                    A.IdMetodoPago,
                                    F.Nombre NombreMetodoPago,
                                    C.Referencia,
                                    C.IdBanco,
                                    E.Nombre NombreBanco,
                                    C.Recibido,
                                    C.Monto,
                                    C.CreationDate FechaPagoFactura,
                                    C.IdUserAction IdUserActionPagoFactura, 
                                    I.UserName UserNamePagoFactura
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_PagosVenta C ON A.IdVenta = C.IdVenta
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_Bancos E ON C.IdBanco = E.IdBanco
                                    INNER JOIN T_MetodoPago F ON F.IdMetodoPago = A.IdMetodoPago
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    INNER JOIN T_User H ON H.IdUser = B.IdUserAction
                                    INNER JOIN T_User I ON H.IdUser = C.IdUserAction 
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdVenta = {Id}";
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    A.IdVenta,
                                    A.Prefijo,
                                    A.Consecutivo,
                                    CONCAT(A.Prefijo,'-',A.Consecutivo) Factura,
                                    A.CreationDate FechaFactura,
                                    A.IdUserAction IdUserActionFactura,
                                    A.IdVendedor,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameFactura,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleFactura,
                                    B.IdUserAction IdUserActionDetalleFactura,
                                    H.UserName UserNameDetalleFactura,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial,
                                    A.IdMetodoPago,
                                    F.Nombre NombreMetodoPago,
                                    C.Referencia,
                                    C.IdBanco,
                                    E.Nombre NombreBanco,
                                    C.Monto,
                                    C.CreationDate FechaPagoFactura,
                                    C.IdUserAction IdUserActionPagoFactura, 
                                    I.UserName UserNamePagoFactura
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_PagosVenta C ON A.IdVenta = C.IdVenta
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_Bancos E ON C.IdBanco = E.IdBanco
                                    INNER JOIN T_MetodoPago F ON F.IdMetodoPago = A.IdMetodoPago
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    INNER JOIN T_User H ON H.IdUser = B.IdUserAction
                                    INNER JOIN T_User I ON H.IdUser = C.IdUserAction 
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor
                                    WHERE 
                                    A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120) ";

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
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento LIKE @Filtro ";
                                    break;
                                case "Id":
                                    Where = $" AND B.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,'-',A.Consecutivo) LIKE @Filtro ";
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
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    Where = $" AND B.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras = @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,'-',A.Consecutivo) = @Filtro ";
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
                                    break;
                                case "Num Doc":
                                    Where = $" AND D.NumeroDocumento = @Filtro ";
                                    break;
                                case "Id":
                                    Where = $" AND B.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $" AND B.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $" AND B.CodigoBarras LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,'-',A.Consecutivo) LIKE @Filtro ";
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
    }
}
