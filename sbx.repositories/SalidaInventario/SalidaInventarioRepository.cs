using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.SalidaInventario;

namespace sbx.repositories.SalidaInventario
{
    public class SalidaInventarioRepository: ISalidaInventario
    {
        private readonly string _connectionString;

        public SalidaInventarioRepository(string connectionString)
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

                    string sql = @" SELECT Fecha, TipoMovimiento, Cantidad, Tipo, IdProducto, Nombre, Sku, 
                                    CodigoBarras, CodigoLote, FechaVencimiento, Comentario 
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
                                        e.FechaVencimiento,
                                        ei.Comentario 
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
                                        s.FechaVencimiento,
                                        si.Comentario 
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

                    sql += Where;
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

        public async Task<Response<dynamic>> CreateUpdate(SalidaInventarioEntitie salidaInventarioEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                await connection.OpenAsync();

                using var transaction = connection.BeginTransaction();

                bool committed = false;

                try
                {
                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    sql = @"INSERT INTO T_SalidasInventario (IdTipoSalida, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoSalida, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    int idSalida = await connection.ExecuteScalarAsync<int>(sql,
                    new
                    {
                        salidaInventarioEntitie.IdTipoSalida,
                        salidaInventarioEntitie.IdProveedor,
                        salidaInventarioEntitie.OrdenCompra,
                        salidaInventarioEntitie.NumFactura,
                        salidaInventarioEntitie.Comentario,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    },
                    transaction);

                    sql = @"INSERT INTO T_DetalleSalidasInventario (
                            IdSalidasInventario, IdProducto, CodigoLote, FechaVencimiento,
                            Cantidad, CostoUnitario, CreationDate, IdUserAction)
                            VALUES (@IdSalidasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @CreationDate, @IdUserAction);"
                    ;

                    foreach (var detalle in salidaInventarioEntitie.detalleSalidaInventarios)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdSalidasInventario = idSalida,
                                detalle.IdProducto,
                                detalle.CodigoLote,
                                detalle.FechaVencimiento,
                                detalle.Cantidad,
                                detalle.CostoUnitario,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();
                    committed = true;

                    //Verificacion si el producto tiene conversion
                    int Error = 0;
                    int Correcto = 0;
                    int contador = 0;
                    decimal CantidadSaleRedondeadaTemp = 0;

                    foreach (var detalle in salidaInventarioEntitie.detalleSalidaInventarios)
                    {
                        contador = 0;
                        CantidadSaleRedondeadaTemp = 0;

                        sql = $@" WITH JerarquiaProductos AS (
                                SELECT
                                    A.IdProductoPadre,
                                    A.IdProductoHijo,
		                            P.Nombre NombreHijo,
		                            P.Sku SkuHijo,
		                            P.CodigoBarras CodigoBarrasHijo, 
                                    A.Cantidad,
                                    1 AS Nivel
                                FROM T_ConversionesProducto A
	                            INNER JOIN T_Productos P ON P.IdProducto = A.IdProductoHijo
                                WHERE A.IdProductoPadre = {detalle.IdProducto}

                                UNION ALL

                                SELECT
                                    jp.IdProductoPadre,
                                    B.IdProductoHijo,
		                            P.Nombre NombreHijo,
		                            P.Sku SkuHijo,
		                            P.CodigoBarras CodigoBarrasHijo, 
                                    B.Cantidad,
                                    jp.Nivel + 1
                                FROM JerarquiaProductos jp
                                INNER JOIN T_ConversionesProducto B ON jp.IdProductoHijo = B.IdProductoPadre
	                            INNER JOIN T_Productos P ON P.IdProducto = B.IdProductoHijo
                            )

                            SELECT *
                            FROM JerarquiaProductos
                            ORDER BY Nivel; ";

                        var resultado = await connection.QueryAsync(sql);

                        int cantidadRegistros = resultado.Count();

                        if (cantidadRegistros > 0)
                        {
                            contador = 0;
                            CantidadSaleRedondeadaTemp = 0;

                            foreach (var item in resultado)
                            {
                                decimal Cantidad = item.Cantidad;
                                decimal CantidadVendida = contador == 0 ? detalle.Cantidad : CantidadSaleRedondeadaTemp;

                                decimal CantidadSale = CantidadVendida * Cantidad;

                                decimal CantidadSaleRedondeada = Math.Round(CantidadSale, 2);

                                int IdProductoHijoSale = item.IdProductoHijo;
                                string Documento = "SI-"+idSalida;

                                SalidaInventarioEntitie salidaInventarioEntitie2 = new SalidaInventarioEntitie();

                                salidaInventarioEntitie2.IdTipoSalida = salidaInventarioEntitie.IdTipoSalida;
                                salidaInventarioEntitie2.IdProveedor = salidaInventarioEntitie.IdProveedor;
                                salidaInventarioEntitie2.OrdenCompra = salidaInventarioEntitie.OrdenCompra;
                                salidaInventarioEntitie2.NumFactura = salidaInventarioEntitie.OrdenCompra;
                                salidaInventarioEntitie2.Comentario = Documento + " " + salidaInventarioEntitie.Comentario;

                                var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                {
                                    IdProducto = IdProductoHijoSale,                             
                                    CodigoLote = "",
                                    FechaVencimiento = detalle.FechaVencimiento,
                                    Cantidad = CantidadSaleRedondeada,
                                    CostoUnitario = 0,
                                    Total = 0
                                };

                                salidaInventarioEntitie2.detalleSalidaInventarios.Add(nuevoDetalle);
                                var resp = await CreateUpdateAuxiliar(salidaInventarioEntitie2, IdUser);

                                if (resp != null)
                                {
                                    if (resp.Flag == true)
                                    {
                                        Correcto++;
                                    }
                                    else
                                    {
                                        Error++;
                                    }
                                }
                                else
                                {
                                    Error++;
                                }

                                contador++;
                                CantidadSaleRedondeadaTemp = CantidadSaleRedondeada;
                            }

                            //Se verifica si el producto es Hijo
                            sql = $@" WITH JerarquiaPadres AS (
                                        SELECT
                                            A.IdProductoPadre,
                                            A.IdProductoHijo,
                                            P.Nombre AS NombrePadre,
                                            P.Sku AS SkuPadre,
                                            P.CodigoBarras AS CodigoBarrasPadre,
                                            ISNULL(A.Cantidad, 1) AS Cantidad,
                                            1 AS Nivel
                                        FROM T_ConversionesProducto A
                                        INNER JOIN T_Productos P ON P.IdProducto = A.IdProductoPadre
                                        WHERE A.IdProductoHijo = {detalle.IdProducto}

                                        UNION ALL

                                        SELECT
                                            B.IdProductoPadre,
                                            jp.IdProductoPadre AS IdProductoHijo,
                                            P.Nombre AS NombrePadre,
                                            P.Sku AS SkuPadre,
                                            P.CodigoBarras AS CodigoBarrasPadre,
                                            ISNULL(B.Cantidad, 1) AS Cantidad,
                                            jp.Nivel + 1
                                        FROM JerarquiaPadres jp
                                        INNER JOIN T_ConversionesProducto B ON jp.IdProductoPadre = B.IdProductoHijo
                                        INNER JOIN T_Productos P ON P.IdProducto = B.IdProductoPadre
                                    )

                                    SELECT *
                                    FROM JerarquiaPadres
                                    ORDER BY Nivel; ";

                            var resultado3 = await connection.QueryAsync(sql);

                            int cantidadRegistros3 = resultado3.Count();

                            if (cantidadRegistros3 > 0)
                            {
                                contador = 0;
                                CantidadSaleRedondeadaTemp = 0;

                                foreach (var item in resultado3)
                                {
                                    decimal Cantidad1 = item.Cantidad;
                                    decimal CantidadVendida = contador == 0 ? detalle.Cantidad : CantidadSaleRedondeadaTemp;

                                    decimal CantidadSale = (decimal)CantidadVendida / Cantidad1;

                                    decimal CantidadSaleRedondeada = Math.Round(CantidadSale, 2);

                                    int IdProductoPadreSale = item.IdProductoPadre;
                                    string Documento = "SI-" + idSalida;

                                    SalidaInventarioEntitie salidaInventarioEntitie2 = new SalidaInventarioEntitie();

                                    salidaInventarioEntitie2.IdTipoSalida = salidaInventarioEntitie.IdTipoSalida;
                                    salidaInventarioEntitie2.IdProveedor = salidaInventarioEntitie.IdProveedor;
                                    salidaInventarioEntitie2.OrdenCompra = salidaInventarioEntitie.OrdenCompra;
                                    salidaInventarioEntitie2.NumFactura = salidaInventarioEntitie.OrdenCompra;
                                    salidaInventarioEntitie2.Comentario = Documento + " " + salidaInventarioEntitie.Comentario;

                                    var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = detalle.FechaVencimiento,
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Total = 0
                                    };

                                    salidaInventarioEntitie2.detalleSalidaInventarios.Add(nuevoDetalle);
                                    var resp = await CreateUpdateAuxiliar(salidaInventarioEntitie2, IdUser);

                                    if (resp != null)
                                    {
                                        if (resp.Flag == true)
                                        {
                                            Correcto++;
                                        }
                                        else
                                        {
                                            Error++;
                                        }
                                    }
                                    else
                                    {
                                        Error++;
                                    }

                                    contador++;
                                    CantidadSaleRedondeadaTemp = CantidadSaleRedondeada;
                                }
                            }
                        }
                        else
                        {
                            sql = $@" WITH JerarquiaPadres AS (
                                        SELECT
                                            A.IdProductoPadre,
                                            A.IdProductoHijo,
                                            P.Nombre AS NombrePadre,
                                            P.Sku AS SkuPadre,
                                            P.CodigoBarras AS CodigoBarrasPadre,
                                            ISNULL(A.Cantidad, 1) AS Cantidad,
                                            1 AS Nivel
                                        FROM T_ConversionesProducto A
                                        INNER JOIN T_Productos P ON P.IdProducto = A.IdProductoPadre
                                        WHERE A.IdProductoHijo = {detalle.IdProducto}

                                        UNION ALL

                                        SELECT
                                            B.IdProductoPadre,
                                            jp.IdProductoPadre AS IdProductoHijo,
                                            P.Nombre AS NombrePadre,
                                            P.Sku AS SkuPadre,
                                            P.CodigoBarras AS CodigoBarrasPadre,
                                            ISNULL(B.Cantidad, 1) AS Cantidad,
                                            jp.Nivel + 1
                                        FROM JerarquiaPadres jp
                                        INNER JOIN T_ConversionesProducto B ON jp.IdProductoPadre = B.IdProductoHijo
                                        INNER JOIN T_Productos P ON P.IdProducto = B.IdProductoPadre
                                    )

                                    SELECT *
                                    FROM JerarquiaPadres
                                    ORDER BY Nivel; ";

                            var resultado2 = await connection.QueryAsync(sql);

                            int cantidadRegistros2 = resultado2.Count();

                            if (cantidadRegistros2 > 0)
                            {
                                contador = 0;
                                CantidadSaleRedondeadaTemp = 0;

                                foreach (var item in resultado2)
                                {
                                    decimal Cantidad1 = item.Cantidad;
                                    decimal CantidadVendida = contador == 0 ? detalle.Cantidad : CantidadSaleRedondeadaTemp;

                                    decimal CantidadSale = (decimal)CantidadVendida / Cantidad1;

                                    decimal CantidadSaleRedondeada = Math.Round(CantidadSale, 2);

                                    int IdProductoPadreSale = item.IdProductoPadre;
                                    string Documento = "SI-" + idSalida;

                                    SalidaInventarioEntitie salidaInventarioEntitie2 = new SalidaInventarioEntitie();

                                    salidaInventarioEntitie2.IdTipoSalida = salidaInventarioEntitie.IdTipoSalida;
                                    salidaInventarioEntitie2.IdProveedor = salidaInventarioEntitie.IdProveedor;
                                    salidaInventarioEntitie2.OrdenCompra = salidaInventarioEntitie.OrdenCompra;
                                    salidaInventarioEntitie2.NumFactura = salidaInventarioEntitie.OrdenCompra;
                                    salidaInventarioEntitie2.Comentario = Documento + " " + salidaInventarioEntitie.Comentario;

                                    var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = detalle.FechaVencimiento,
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Total = 0
                                    };

                                    salidaInventarioEntitie2.detalleSalidaInventarios.Add(nuevoDetalle);
                                    var resp = await CreateUpdateAuxiliar(salidaInventarioEntitie2, IdUser);

                                    if (resp != null)
                                    {
                                        if (resp.Flag == true)
                                        {
                                            Correcto++;
                                        }
                                        else
                                        {
                                            Error++;
                                        }
                                    }
                                    else
                                    {
                                        Error++;
                                    }

                                    contador++;
                                    CantidadSaleRedondeadaTemp = CantidadSaleRedondeada;
                                }
                            }
                        }
                    }

                    response.Flag = true;
                    response.Message = $"Salida creada correctamente, al momento de realizar salidas de inventario por podructos padres o hijo Correctos: {Correcto} y Errores: {Error} ";
                    return response;
                }
                catch (Exception ex)
                {
                    if (!committed)
                    {
                        await transaction.RollbackAsync();
                    }

                    response.Flag = false;
                    response.Message = "Error: " + ex.Message;
                    return response;
                }
            }
        }

        public async Task<Response<dynamic>> CreateUpdateAuxiliar(SalidaInventarioEntitie salidaInventarioEntitie, int IdUser)
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

                    sql = @"INSERT INTO T_SalidasInventario (IdTipoSalida, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoSalida, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    int idSalida = await connection.ExecuteScalarAsync<int>(sql,
                    new
                    {
                        salidaInventarioEntitie.IdTipoSalida,
                        salidaInventarioEntitie.IdProveedor,
                        salidaInventarioEntitie.OrdenCompra,
                        salidaInventarioEntitie.NumFactura,
                        salidaInventarioEntitie.Comentario,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    },
                    transaction);

                    sql = @"INSERT INTO T_DetalleSalidasInventario (
                            IdSalidasInventario, IdProducto, CodigoLote, FechaVencimiento,
                            Cantidad, CostoUnitario, CreationDate, IdUserAction)
                            VALUES (@IdSalidasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @CreationDate, @IdUserAction);"
                    ;

                    foreach (var detalle in salidaInventarioEntitie.detalleSalidaInventarios)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdSalidasInventario = idSalida,
                                detalle.IdProducto,
                                detalle.CodigoLote,
                                detalle.FechaVencimiento,
                                detalle.Cantidad,
                                detalle.CostoUnitario,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();
                   
                    response.Flag = true;
                    response.Message = "Salida creada correctamente";
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

                    string sql = $@" SELECT A.IdSalidasInventario
                                      ,A.IdTipoSalida
	                                  ,C.Nombre TipoEntrada
                                      ,A.IdProveedor
	                                  ,D.NumeroDocumento
	                                  ,D.NombreRazonSocial
                                      ,A.OrdenCompra
                                      ,A.NumFactura
                                      ,A.Comentario
                                      ,A.CreationDate
                                      ,A.UpdateDate
                                      ,A.IdUserAction
	                                  ,B.IdProducto
	                                  ,E.Sku
	                                  ,E.CodigoBarras
	                                  ,E.Nombre
                                      ,B.CodigoLote
                                      ,B.FechaVencimiento
                                      ,B.Cantidad
                                      ,B.CostoUnitario
                                 FROM T_SalidasInventario A
                                  INNER JOIN T_DetalleSalidasInventario B ON A.IdSalidasInventario = B.IdSalidasInventario
                                  INNER JOIN T_TipoSalida C ON C.IdTipoSalida = A.IdTipoSalida
                                  INNER JOIN T_Proveedores D ON D.IdProveedor = A.IdProveedor
                                  INNER JOIN T_Productos E ON E.IdProducto = B.IdProducto 
                                  WHERE A.IdSalidasInventario = {Id} ";

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

        public async Task<Response<dynamic>> UpdateFechaVencimiento(int IdSalida, DateTime FechaVencimiento, int IdUser)
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

                    sql = @$" UPDATE T_DetalleSalidasInventario SET 
                                  FechaVencimiento = @FechaVencimiento,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdDetalleSalidasInventario = @IdSalida";

                    var parametros = new
                    {
                        IdSalida,
                        FechaVencimiento,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Fecha vencimiento actualizada correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Se presento un error al intentar actualizar fecha vencimiento";
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
