using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.Reportes;

namespace sbx.repositories.Reportes
{
    public class ReportesRepository: IReportes
    {
        private readonly string _connectionString;

        public ReportesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string TipoReporte, DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    switch (TipoReporte)
                    {
                        case "Resumen - Ganancias y perdidas":
                            sql = @"SELECT 
                                    B.IdProducto,
                                    B.NombreProducto,
                                    SUM(B.Cantidad) Cantidad,
                                    SUM((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) VentaNetaFinal,
                                    SUM((B.Cantidad * B.CostoUnitario))CostoTotal,
                                    SUM(((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) - (B.Cantidad * B.CostoUnitario)) GananciaBruta        
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_Cliente C ON A.IdCliente = C.IdCliente
                                    INNER JOIN T_User D ON D.IdUser = A.IdUserAction
                                    WHERE 
                                    (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
                                    AND A.Estado = 'FACTURADA' ";
                            break;
                        case "Resumen por factura - Ganancias y perdidas":
                            sql = @"SELECT 
                                    A.CreationDate,
									CONCAT(A.IdUserAction,'-',D.UserName) Usuario,
                                    A.IdVenta,                                  
                                    CONCAT(A.Prefijo,A.Consecutivo) Factura,
                                    ISNULL(A.EstadoFacturaDIAN,'') EstadoFacturaDIAN,
									ISNULL(A.NumberFacturaDIAN,'') NumberFacturaDIAN,
									A.FacturaJSON,
	                                B.IdProducto,
                                    B.NombreProducto,
                                    B.Cantidad,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    ISNULL(B.Descuento, 0) AS DescuentoPorcentaje,
                                    B.Impuesto AS ImpuestoPorcentaje,
                                    (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)  AS VentaNetaFinal,
                                    (B.Cantidad * B.CostoUnitario) AS CostoTotal,
                                    -- Ganancia/Pérdida
                                    ((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) - (B.Cantidad * B.CostoUnitario) AS GananciaBruta,   
                                    -- Margen sobre venta
                                    CASE 
                                        WHEN (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) > 0 
                                        THEN ((((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) - (B.Cantidad * B.CostoUnitario)) / 
                                              ((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) )) * 100
                                        ELSE 0 
                                    END AS MargenPorcentaje      
                                FROM T_Ventas A
                                INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                INNER JOIN T_Cliente C ON A.IdCliente = C.IdCliente
                                INNER JOIN T_User D ON D.IdUser = A.IdUserAction
                                WHERE (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) AND
                                A.Estado = 'FACTURADA' ";
                            break;
                        case "Detallado -  Ganancias y perdidas":
                            sql = @"SELECT 
                                    A.CreationDate,
									CONCAT(A.IdUserAction,'-',D.UserName) Usuario,
                                    A.IdVenta,
                                    A.Prefijo,
	                                A.Consecutivo,
                                    CONCAT(A.Prefijo,A.Consecutivo) Factura,
	                                A.IdCliente,
	                                C.NumeroDocumento,
	                                C.NombreRazonSocial,
                                    CONCAT(C.NumeroDocumento, '-',C.NombreRazonSocial) Cliente,
                                    A.IdVendedor,
									E.NumeroDocumento NumeroDocumento,
									CONCAT(A.IdVendedor,'-',E.Nombre,' ',E.Apellido) Vendedor,
	                                A.IdUserAction,
	                                D.UserName,
	                                A.Estado,
                                    ISNULL(A.EstadoFacturaDIAN,'') EstadoFacturaDIAN,
									ISNULL(A.NumberFacturaDIAN,'') NumberFacturaDIAN,
									A.FacturaJSON,
	                                B.IdProducto,
	                                B.Sku,
	                                B.CodigoBarras,
                                    B.NombreProducto,
                                    B.UnidadMedida,
                                    B.Cantidad,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    ISNULL(B.Descuento, 0) AS DescuentoPorcentaje,
                                    B.Impuesto AS ImpuestoPorcentaje,
                                    -- Cálculos directos
                                    (B.Cantidad * B.PrecioUnitario) AS VentaBruta,
                                    (B.Cantidad * B.PrecioUnitario) * (ISNULL(B.Descuento, 0) / 100.0) AS DescuentoValor,
                                    (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100.0) AS VentaConDescuento,
                                    (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100.0)  AS ImpuestoValor,
                                    (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100.0)  AS VentaNetaFinal,
                                    (B.Cantidad * B.CostoUnitario) AS CostoTotal,
                                    -- Ganancia/Pérdida
                                    ((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100.0) ) - (B.Cantidad * B.CostoUnitario) AS GananciaBruta,
                                    -- Margen sobre venta
                                    CASE 
                                        WHEN (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)  > 0 
                                        THEN ((((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) - (B.Cantidad * B.CostoUnitario)) / 
                                              ((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) )) * 100
                                        ELSE 0 
                                    END AS MargenPorcentaje,
                                    -- Markup sobre costo
                                    CASE 
                                        WHEN (B.Cantidad * B.CostoUnitario) > 0 
                                        THEN ((((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) ) - (B.Cantidad * B.CostoUnitario)) / 
                                              (B.Cantidad * B.CostoUnitario)) * 100
                                        ELSE 0 
                                    END AS MarkupPorcentaje
    
                                FROM T_Ventas A
                                INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                INNER JOIN T_Cliente C ON C.IdCliente = A.IdCliente
                                INNER JOIN T_User D ON D.IdUser = A.IdUserAction
                                INNER JOIN T_Vendedor E ON E.IdVendedor = A.IdVendedor
                                WHERE (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) AND 
                                A.Estado = 'FACTURADA' ";
                            break;
                        default:
                            break;
                    }

                    string Where = "";
                    string Filtro = "";
                    string groupby = "";
                    string orderby = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre Cliente":
                                    Where = $" AND C.NombreRazonSocial LIKE @Filtro ";
                                    break;
                                case "Num Doc Cliente":
                                    Where = $" AND C.NumeroDocumento LIKE @Filtro ";
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
                                case "Nombre producto":
                                    Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) LIKE @Filtro ";
                                    break;
                                case "Nombre usuario":
                                    Where = $" AND D.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $" AND A.IdUserAction LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato + "%";
                            break;
                        case "Igual a":
                            switch (campoFiltro)
                            {
                                case "Nombre Cliente":
                                    Where = $" AND C.NombreRazonSocial = @Filtro ";
                                    break;
                                case "Num Doc Cliente":
                                    Where = $" AND C.NumeroDocumento = @Filtro ";
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
                                case "Nombre producto":
                                    Where = $" AND B.NombreProducto = @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) = @Filtro ";
                                    break;
                                case "Nombre usuario":
                                    Where = $" AND D.UserName = @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $" AND A.IdUserAction = @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato;
                            break;
                        case "Contiene":
                            switch (campoFiltro)
                            {
                                case "Nombre Cliente":
                                    Where = $" AND C.NombreRazonSocial LIKE @Filtro ";
                                    break;
                                case "Num Doc Cliente":
                                    Where = $" AND C.NumeroDocumento LIKE @Filtro ";
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
                                case "Nombre producto":
                                    Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    break;
                                case "Prefijo-Consecutivo":
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) LIKE @Filtro ";
                                    break;
                                case "Nombre usuario":
                                    Where = $" AND D.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $" AND A.IdUserAction LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    orderby = " ORDER BY A.CreationDate DESC, A.IdVenta; ";

                    if (TipoReporte == "Resumen - Ganancias y perdidas") 
                    {
                        groupby = " GROUP BY B.IdProducto, B.NombreProducto ";
                        orderby = " ORDER BY B.IdProducto ";
                    }

                    sql += Where + groupby + orderby;

                    dynamic resultado = await connection.QueryAsync(sql, new { Filtro, FechaIni, FechaFn });

                    if (resultado.Count == 0 && campoFiltro == "Prefijo-Consecutivo")
                    {
                        sql = sql.Replace("AND CONCAT(A.Prefijo,A.Consecutivo)", "AND A.NumberFacturaDIAN");

                        resultado = await connection.QueryAsync(sql, new { Filtro, FechaIni, FechaFn });
                    }

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
