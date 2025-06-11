using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.EntradaInventario;
using System.Data;
using System.Globalization;

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

                    string sql = @" SELECT Fecha,IdUserAction,UserName ,Usuario ,Documento, TipoMovimiento, Cantidad, Tipo, IdProducto, Nombre, Sku, 
                                    CodigoBarras, CodigoLote, FechaVencimiento
                                    FROM
                                    (
                                    SELECT 
                                        e.CreationDate AS Fecha,
										e.IdUserAction,
										usr.UserName,
										CONCAT(e.IdUserAction,'-',usr.UserName) Usuario,
										CONCAT('EI','-',ei.IdEntradasInventario) Documento,
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
									INNER JOIN T_User usr ON usr.IdUser = e.IdUserAction

                                    UNION ALL

                                    SELECT 
                                        s.CreationDate AS Fecha,
										s.IdUserAction,
										usr.UserName,
										CONCAT(s.IdUserAction,'-',usr.UserName) Usuario,
										CONCAT('SI','-',si.IdSalidasInventario) Documento,
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
									INNER JOIN T_User usr ON usr.IdUser = s.IdUserAction

                                    UNION ALL

	                                SELECT
	                                    dvt.CreationDate AS Fecha,
										dvt.IdUserAction,
										usr.UserName,
										CONCAT(dvt.IdUserAction,'-',usr.UserName) Usuario,
										CONCAT(vt.Prefijo,'-',vt.Consecutivo) Documento,
	                                    'Salida por Venta' AS TipoMovimiento,
	                                    dvt.Cantidad,
	                                    'Ventas' AS Tipo,
	                                    dvt.IdProducto,
	                                    dvt.NombreProducto AS Nombre,
	                                    dvt.Sku,
	                                    dvt.CodigoBarras,
	                                    '' AS CodigoLote,
	                                    '' AS FechaVencimiento
	                                FROM T_Ventas vt 
									INNER JOIN T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta
									INNER JOIN T_User usr ON usr.IdUser = dvt.IdUserAction

									UNION ALL

									SELECT
									    ncd.CreationDate AS Fecha,
										ncd.IdUserAction,
										usr.UserName,
										CONCAT(ncd.IdUserAction,'-',usr.UserName) Usuario,
										CONCAT('NC','-',nc.IdNotaCredito) Documento,
										'Entrada por Nota credito' AS TipoMovimiento,
										ncd.Cantidad,
										'Devolucion' AS Tipo,
										ncd.IdProducto,
										ncd.NombreProducto AS Nombre,
										ncd.Sku,
										ncd.CodigoBarras,
										'' AS CodigoLote,
	                                    '' AS FechaVencimiento
									FROM T_NotaCredito nc INNER JOIN T_NotaCreditoDetalle ncd ON nc.IdNotaCredito  = ncd.IdNotaCredito
									INNER JOIN T_User usr ON usr.IdUser = ncd.IdUserAction
                                    ) R ";

                    string Where = "";
                    string Filtro = "";
                    string Orderby = " ORDER BY R.Fecha DESC ";

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

        public async Task<Response<dynamic>> CargueMasivoProductoEntrada(DataTable Datos, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                await connection.OpenAsync();

                using var transaction = connection.BeginTransaction();

                try
                {
                    foreach (DataRow item in Datos.Rows)
                    {
                        string sql = "";

                        DateTime FechaActual = DateTime.Now;
                        FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));


                        sql = @$" INSERT INTO T_Productos (Sku,CodigoBarras,Nombre,
                                  CostoBase,PrecioBase,EsInventariable,Iva,IdCategoria,IdMarca,IdUnidadMedida,CreationDate, IdUserAction)
                                  VALUES(NULLIF(@Sku,''),NULLIF(@CodigoBarras, ''),@Nombre,
                                  @CostoBase,@PrecioBase,@EsInventariable,@Iva,@IdCategoria,@IdMarca,@IdUnidadMedida,@CreationDate,@IdUserAction);
                                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        var parametrosProducto = new
                        {
                            Sku = item[0],
                            CodigoBarras = "",
                            Nombre = item[1].ToString().Trim() + " - " + item[2].ToString().Trim(),
                            CostoBase = 0,
                            PrecioBase = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                            EsInventariable = 1,
                            Iva = 0,
                            IdCategoria = 1,
                            IdMarca = 1,
                            IdUnidadMedida = 1,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int idProducto = await connection.ExecuteScalarAsync<int>(sql, parametrosProducto, transaction);

                        sql = @"INSERT INTO T_EntradasInventario (IdTipoEntrada, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoEntrada, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                        int idEntrada = await connection.ExecuteScalarAsync<int>(sql,
                        new
                        {
                            IdTipoEntrada = 1,
                            IdProveedor = 1,
                            OrdenCompra = "",
                            NumFactura = "",
                            Comentario = "",
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        },
                        transaction);

                        sql = @"INSERT INTO T_DetalleEntradasInventario (
                            IdEntradasInventario, IdProducto, CodigoLote, FechaVencimiento,
                            Cantidad, CostoUnitario, Descuento, Iva, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Iva, @CreationDate, @IdUserAction);";

                        await connection.ExecuteAsync(
                                sql,
                                new
                                {
                                    IdEntradasInventario = idEntrada,
                                    idProducto,
                                    CodigoLote = "",
                                    FechaVencimiento = "",
                                    Cantidad = Convert.ToDecimal(item[3], new CultureInfo("es-CO")),
                                    CostoUnitario = 0,
                                    Descuento = 0,
                                    Iva = 0,
                                    CreationDate = FechaActual,
                                    IdUserAction = IdUser
                                },
                                transaction);
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Message = "Cargue masivo creado correctamente";
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
    }
}
