using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.ReporteGeneral;

namespace sbx.repositories.ReporteGeneral
{
    public class ReporteGeneralRepository: IReporteGeneral
    {

        private readonly string _connectionString;

        public ReporteGeneralRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> BuscarReporteVentas(DateTime FechaInicio, DateTime FechaFin)
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
                                    A.CreationDate,
                                    B.IdProducto,
                                    SUM((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)) VentaNetaFinal                                             
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    WHERE 
                                    (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
                                    AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) AND 
                                    A.Estado = 'FACTURADA' 
                                    GROUP BY A.CreationDate,B.IdProducto
                                    ORDER BY A.CreationDate,B.IdProducto  ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> BuscarReporteCompras(DateTime FechaInicio, DateTime FechaFin)
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
                                    IdProducto
                                    ,SUM((Cantidad * CostoUnitario) * (1 - ISNULL(Descuento, 0) / 100)) CostoNetoFinal
                                    ,CreationDate
                                    FROM T_DetalleEntradasInventario
                                    WHERE 
                                    (CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
                                    AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
                                    GROUP BY CreationDate,IdProducto
                                    ORDER BY CreationDate,IdProducto  ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> BuscarReporteGastos(DateTime FechaInicio, DateTime FechaFin)
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
                                    Detalle,
                                    SUM(ValorGasto) ValorGasto,
                                    CreationDate
                                    FROM T_Gastos
                                    WHERE 
                                    (CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
                                    AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
                                    GROUP BY Detalle, CreationDate 
                                    ORDER BY Detalle   ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> BuscarReporteSalidas(DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @" SELECT 
                                    IdProducto
                                    ,SUM(Cantidad * CostoUnitario) ValorSalidas
                                    ,CreationDate
                                    FROM T_DetalleSalidasInventario
                                    WHERE 
                                    (CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
                                    AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
                                    GROUP BY CreationDate,IdProducto
                                    ORDER BY CreationDate,IdProducto ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> BuscarReporteGeneral(DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @" SELECT 
                                    CASE WHEN Modulo = 'COMPRAS' THEN 1 ELSE 
                                    CASE WHEN Modulo = 'VENTAS'  THEN 2 ELSE
                                    CASE WHEN Modulo = 'GASTOS'  THEN 3 ELSE
                                    CASE WHEN Modulo = 'SALIDAS' THEN 4 END END END END Id,
                                    Modulo,
                                    Valor,
                                    CreationDate
                                    FROM 
                                    (
                                    --VENTAS
                                    SELECT 
                                    'VENTAS' Modulo
                                    ,A.CreationDate,
                                    SUM((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)) Valor                                            
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    WHERE A.Estado = 'FACTURADA'
                                    GROUP BY A.CreationDate    

                                    UNION ALL

                                    --COMPRAS
                                    SELECT 
                                    'COMPRAS' Modulo
                                    ,CreationDate
                                    ,SUM((Cantidad * CostoUnitario) * (1 - ISNULL(Descuento, 0) / 100)) Valor
                                    FROM T_DetalleEntradasInventario
                                    GROUP BY CreationDate

                                    UNION ALL

                                    --SALIDAS
                                    SELECT 
                                    'SALIDAS' Modulo
                                    ,CreationDate
                                    ,SUM(Cantidad * CostoUnitario) AS Valor
                                    FROM T_DetalleSalidasInventario
                                    GROUP BY CreationDate

                                    UNION ALL

                                    --GASTOS
                                    SELECT 
                                    'GASTOS' Modulo
                                    ,CreationDate
                                    ,SUM(ValorGasto) Valor
                                    FROM T_Gastos
                                    GROUP BY CreationDate
                                    ) R
                                    WHERE 
                                    (CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
                                    AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120))
                                    ORDER BY Id, CreationDate ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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
