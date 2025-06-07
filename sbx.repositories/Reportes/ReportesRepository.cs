using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;

namespace sbx.repositories.Reportes
{
    public class ReportesRepository
    {
        private readonly string _connectionString;

        public ReportesRepository(string connectionString)
        {
            _connectionString = connectionString;
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
                                    v.CreationDate,
                                    v.IdVenta,
                                    v.Prefijo,
	                                v.Consecutivo,
                                    CONCAT(v.Prefijo,'-',v.Consecutivo) Factura,
	                                v.IdCliente,
	                                A.NumeroDocumento,
	                                A.NombreRazonSocial,
	                                v.IdUserAction,
	                                B.UserName,
	                                v.Estado,
	                                dv.IdProducto,
	                                dv.Sku,
	                                dv.CodigoBarras,
                                    dv.NombreProducto,
                                    dv.Cantidad,
                                    dv.PrecioUnitario,
                                    dv.CostoUnitario,
                                    ISNULL(dv.Descuento, 0) AS DescuentoPorcentaje,
                                    dv.Impuesto AS ImpuestoPorcentaje,
    
                                    -- Cálculos directos
                                    (dv.Cantidad * dv.PrecioUnitario) AS VentaBruta,
                                    (dv.Cantidad * dv.PrecioUnitario) * (ISNULL(dv.Descuento, 0) / 100.0) AS DescuentoValor,
                                    (dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) AS VentaConDescuento,
                                    (dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (dv.Impuesto / 100.0) AS ImpuestoValor,
                                    (dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0) AS VentaNetaFinal,
                                    (dv.Cantidad * dv.CostoUnitario) AS CostoTotal,
    
                                    -- Ganancia/Pérdida
                                    ((dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0)) - (dv.Cantidad * dv.CostoUnitario) AS GananciaBruta,
    
                                    -- Margen sobre venta
                                    CASE 
                                        WHEN (dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0) > 0 
                                        THEN ((((dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0)) - (dv.Cantidad * dv.CostoUnitario)) / 
                                              ((dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0))) * 100
                                        ELSE 0 
                                    END AS MargenPorcentaje,
    
                                    -- Markup sobre costo
                                    CASE 
                                        WHEN (dv.Cantidad * dv.CostoUnitario) > 0 
                                        THEN ((((dv.Cantidad * dv.PrecioUnitario) * (1 - ISNULL(dv.Descuento, 0) / 100.0) * (1 + dv.Impuesto / 100.0)) - (dv.Cantidad * dv.CostoUnitario)) / 
                                              (dv.Cantidad * dv.CostoUnitario)) * 100
                                        ELSE 0 
                                    END AS MarkupPorcentaje
    
                                FROM T_Ventas v
                                INNER JOIN T_DetalleVenta dv ON v.IdVenta = dv.IdVenta
                                INNER JOIN T_Cliente A ON A.IdCliente = v.IdCliente
                                INNER JOIN T_User B ON B.IdUser = v.IdUserAction
                                WHERE (v.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
                                AND v.Estado = 'FACTURADA'
                                ORDER BY v.CreationDate DESC, v.IdVenta; ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre Cliente":
                                    Where = $" AND D.NombreRazonSocial LIKE @Filtro ";
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
