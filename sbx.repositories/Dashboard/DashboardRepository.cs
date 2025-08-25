using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.Dashboard;

namespace sbx.repositories.Dashboard
{
    public class DashboardRepository: IDashboard
    {
        private readonly string _connectionString;

        public DashboardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Response<dynamic>> Buscar(DateTime FechaInicio, DateTime FechaFin)
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
									B.Cantidad,
									B.CostoUnitario,
									B.PrecioUnitario,
									B.Descuento,
									A.Estado,
									B.Impuesto,
									B.NombreTributo,         
                                    B.NombreProducto,
									E.Nombre MedioPago  
									FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_Cliente C ON A.IdCliente = C.IdCliente
                                    INNER JOIN T_User D ON D.IdUser = A.IdUserAction
									INNER JOIN T_MetodoPago E on A.IdMetodoPago = E.IdMetodoPago
                                    WHERE 
                                    (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
									AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) AND 
									A.Estado = 'FACTURADA'
									ORDER BY A.CreationDate,B.IdProducto ";

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

        public async Task<Response<dynamic>> Buscar2(DateTime FechaInicio, DateTime FechaFin)
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
                                    B.NombreProducto,
									E.Nombre MedioPago,
                                    SUM(B.Cantidad) Cantidad,
                                    SUM((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)) VentaNetaFinal,
                                    SUM((B.Cantidad * B.CostoUnitario))CostoTotal,
                                    SUM(((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)) - (B.Cantidad * B.CostoUnitario)) GananciaBruta,
									SUM(CASE 
									WHEN (B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100) > 0 
									THEN ((((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100)) - (B.Cantidad * B.CostoUnitario)) / 
										  ((B.Cantidad * B.PrecioUnitario) * (1 - ISNULL(B.Descuento, 0) / 100))) * 100
									ELSE 0 
									END) AS MargenPorcentaje         
                                    
									FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_Cliente C ON A.IdCliente = C.IdCliente
                                    INNER JOIN T_User D ON D.IdUser = A.IdUserAction
									INNER JOIN T_MetodoPago E on A.IdMetodoPago = E.IdMetodoPago
                                    WHERE 
                                    (A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
									AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) AND 
									A.Estado = 'FACTURADA'
									GROUP BY A.CreationDate,B.IdProducto, B.NombreProducto, E.Nombre
									ORDER BY A.CreationDate,B.IdProducto ";

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
