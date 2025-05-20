using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.EntradaInventario;

namespace sbx.repositories.EntradaInventario
{
    public class EntradaInventarioRepository : IEntradaInventario
    {
        private readonly string _connectionString;

        public EntradaInventarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(EntradasInventarioEntitie entradasInventarioEntitie, int IdUser)
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

                    sql = @"INSERT INTO T_EntradasInventario (IdTipoEntrada, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoEntrada, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    int idEntrada = await connection.ExecuteScalarAsync<int>(sql,
                    new
                    {
                        entradasInventarioEntitie.IdTipoEntrada,
                        entradasInventarioEntitie.IdProveedor,
                        entradasInventarioEntitie.OrdenCompra,
                        entradasInventarioEntitie.NumFactura,
                        entradasInventarioEntitie.Comentario,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    },
                    transaction);

                    sql = @"INSERT INTO T_DetalleEntradasInventario (
                            IdEntradasInventario, IdProducto, CodigoLote, FechaVencimiento,
                            Cantidad, CostoUnitario, Descuento, Iva, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Iva, @CreationDate, @IdUserAction);";

                    foreach (var detalle in entradasInventarioEntitie.detalleEntradasInventarios)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdEntradasInventario = idEntrada,
                                detalle.IdProducto,
                                detalle.CodigoLote,
                                detalle.FechaVencimiento,
                                detalle.Cantidad,
                                detalle.CostoUnitario,
                                detalle.Descuento,
                                detalle.Iva,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Message = "Entrada creada correctamente";
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @" SELECT Fecha, TipoMovimiento, Cantidad, Tipo, IdProducto, Nombre, Sku, 
                                    CodigoBarras, CodigoLote, FechaVencimiento
                                    FROM
                                    (
                                    SELECT 
                                        e.CreationDate AS Fecha,
                                        'Entrada' AS TipoMovimiento,
	                                    e.Cantidad,
                                        te.Nombre AS Tipo,
                                        e.IdProducto,
	                                    p.Nombre,
	                                    p.Sku,
	                                    p.CodigoBarras,
                                        e.CodigoLote,
                                        e.FechaVencimiento
                                    FROM T_DetalleEntradasInventario e
                                    INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario
                                    INNER JOIN T_TipoEntrada te ON te.IdTipoEntrada = ei.IdTipoEntrada
                                    INNER JOIN T_Productos p ON p.IdProducto = e.IdProducto

                                    UNION ALL

                                    SELECT 
                                        s.CreationDate AS Fecha,
                                        'Salida' AS TipoMovimiento,
	                                    s.Cantidad,
                                        ts.Nombre AS Tipo,
                                        s.IdProducto,
	                                    p.Nombre,
	                                    p.Sku,
	                                    p.CodigoBarras,
                                        s.CodigoLote,
                                        s.FechaVencimiento
                                    FROM T_DetalleSalidasInventario s
                                    INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario
                                    INNER JOIN T_TipoSalida ts ON ts.IdTipoSalida = si.IdTipoSalida
                                    INNER JOIN T_Productos p ON p.IdProducto = s.IdProducto
                                    ) R ";

                    string Where = "";
                    string Filtro = "";
                    string Orderby = " ORDER BY R.Fecha ";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE R.Nombre LIKE @Filtro ";
                                    break;
                                case "Id":
                                    Where = $"WHERE R.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE R.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE R.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE R.Nombre = @Filtro ";
                                    break;
                                case "Id":
                                    Where = $"WHERE R.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE R.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE R.CodigoBarras = @Filtro ";
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
                                    Where = $"WHERE R.Nombre LIKE @Filtro ";
                                    break;
                                case "Id":
                                    Where = $"WHERE R.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE R.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE R.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where ;
                    sql += Orderby;

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

        public async Task<Response<dynamic>> Entradas(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    string sql = @"SELECT COUNT(1) Entradas FROM T_DetalleEntradasInventario WHERE IdProducto = @IdProducto";

                    dynamic resultado = await connection.QueryAsync(sql, new { IdProducto });

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
