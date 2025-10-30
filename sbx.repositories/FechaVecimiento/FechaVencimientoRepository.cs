using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.FechaVencimiento;

namespace sbx.repositories.FechaVecimiento
{
    public class FechaVencimientoRepository: IFechaVencimiento
    {
        private readonly string _connectionString;

        public FechaVencimientoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    M.IdProducto,
	                                ISNULL(P.Sku,'') Sku,
	                                ISNULL(P.CodigoBarras,'') CodigoBarras,
	                                P.Nombre,
                                    M.FechaVencimiento,
                                    SUM(M.Cantidad) AS Stock 
                                FROM (
                                    -- Entradas (positivas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        Cantidad
                                    FROM T_DetalleEntradasInventario
	                                WHERE FechaVencimiento != '1900-01-01'

                                    UNION ALL

                                    -- Salidas (negativas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        -Cantidad AS Cantidad
                                    FROM T_DetalleSalidasInventario
	                                WHERE FechaVencimiento != '1900-01-01'

									UNION ALL

									--Notas credito
									SELECT
									IdProducto,
									FechaVencimiento,
									Cantidad
									FROM T_NotaCreditoDetalle
									WHERE FechaVencimiento != '1900-01-01'

                                    UNION ALL

									--Ventas
									SELECT
									IdProducto,
									FechaVencimiento,
									-Cantidad AS Cantidad
									FROM T_DetalleVenta
									WHERE FechaVencimiento != '1900-01-01'

                                ) AS M
                                INNER JOIN T_Productos P ON M.IdProducto = P.IdProducto ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(P.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(P.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(P.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    string Groupby = @" GROUP BY 
                                    M.IdProducto,
                                    M.FechaVencimiento,
	                                P.Sku,
	                                P.CodigoBarras,
	                                P.Nombre ";

                    sql += Where + Groupby + " ORDER BY M.FechaVencimiento,  M.IdProducto ";

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

        public async Task<Response<dynamic>> BuscarStock(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    M.IdProducto,
	                                ISNULL(P.Sku,'') Sku,
	                                ISNULL(P.CodigoBarras,'') CodigoBarras,
	                                P.Nombre,
                                    M.FechaVencimiento,
                                    SUM(M.Cantidad) AS Stock 
                                FROM (
                                    -- Entradas (positivas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        Cantidad
                                    FROM T_DetalleEntradasInventario

                                    UNION ALL

                                    -- Salidas (negativas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        -Cantidad AS Cantidad
                                    FROM T_DetalleSalidasInventario

									UNION ALL

									--Notas credito
									SELECT
									IdProducto,
									FechaVencimiento,
									Cantidad
									FROM T_NotaCreditoDetalle

                                    UNION ALL

									--Ventas
									SELECT
									IdProducto,
									FechaVencimiento,
									-Cantidad AS Cantidad
									FROM T_DetalleVenta

                                ) AS M
                                INNER JOIN T_Productos P ON M.IdProducto = P.IdProducto ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(P.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(P.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(P.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE P.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE P.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE P.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    string Groupby = @" GROUP BY 
                                    M.IdProducto,
                                    M.FechaVencimiento,
	                                P.Sku,
	                                P.CodigoBarras,
	                                P.Nombre ";

                    sql += Where + Groupby + " ORDER BY M.IdProducto, M.FechaVencimiento ";

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

        public async Task<Response<dynamic>> BuscarxIdProducto(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT 
                                    IdProducto,
                                    Sku,
                                    CodigoBarras,
                                    Nombre,
                                    FechaVencimiento,
                                    Stock
                                    FROM (
                                    SELECT
                                    M.IdProducto,
	                                ISNULL(P.Sku,'') Sku,
	                                ISNULL(P.CodigoBarras,'') CodigoBarras,
	                                P.Nombre,
                                    M.FechaVencimiento,
                                    SUM(M.Cantidad) AS Stock
                                FROM (
                                    -- Entradas (positivas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        Cantidad
                                    FROM T_DetalleEntradasInventario

                                    UNION ALL

                                    -- Salidas (negativas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        -Cantidad AS Cantidad
                                    FROM T_DetalleSalidasInventario

                                    UNION ALL

                                    --Ventas
									SELECT
									IdProducto,
									FechaVencimiento,
									-Cantidad AS Cantidad
									FROM T_DetalleVenta

									UNION ALL

									--Notas credito
									SELECT
									IdProducto,
									FechaVencimiento,
									Cantidad
									FROM T_NotaCreditoDetalle
                                ) AS M
                                INNER JOIN T_Productos P ON M.IdProducto = P.IdProducto
								
								WHERE M.IdProducto = {IdProducto}
								   
								GROUP BY
                                    M.IdProducto,
                                    M.FechaVencimiento,
	                                P.Sku,
	                                P.CodigoBarras,
	                                P.Nombre  
									) AS S

									WHERE S.Stock > 0

									ORDER BY S.FechaVencimiento, S.IdProducto ";

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

        public async Task<Response<dynamic>> BuscarxIdProductoTieneVence(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT 
                                    IdProducto,
                                    Sku,
                                    CodigoBarras,
                                    Nombre,
                                    FechaVencimiento,
                                    Stock
                                    FROM (
                                    SELECT
                                    M.IdProducto,
	                                ISNULL(P.Sku,'') Sku,
	                                ISNULL(P.CodigoBarras,'') CodigoBarras,
	                                P.Nombre,
                                    M.FechaVencimiento,
                                    SUM(M.Cantidad) AS Stock
                                FROM (
                                    -- Entradas (positivas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        Cantidad
                                    FROM T_DetalleEntradasInventario
                                    --WHERE FechaVencimiento != '1900-01-01'

                                    UNION ALL

                                    -- Salidas (negativas)
                                    SELECT 
                                        IdProducto, 
                                        FechaVencimiento, 
                                        -Cantidad AS Cantidad
                                    FROM T_DetalleSalidasInventario IdEntradaOrSalida
                                    --WHERE FechaVencimiento != '1900-01-01'

                                    UNION ALL

                                    -- Notas credito (positivas)
									SELECT 
										IdProducto,
										FechaVencimiento,
										Cantidad
									FROM T_NotaCreditoDetalle 

									UNION ALL

                                    -- Ventas (negativas)
									SELECT 
										IdProducto,
										FechaVencimiento,
										-Cantidad AS Cantidad
									FROM T_DetalleVenta

                                ) AS M
                                INNER JOIN T_Productos P ON M.IdProducto = P.IdProducto
								
								WHERE M.IdProducto = {IdProducto}
								   
								GROUP BY
                                    M.IdProducto,
                                    M.FechaVencimiento,
	                                P.Sku,
	                                P.CodigoBarras,
	                                P.Nombre  
									) AS S

									--WHERE S.Stock > 0

									ORDER BY S.FechaVencimiento, S.IdProducto ";

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
