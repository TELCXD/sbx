using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;
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

                bool committed = false;

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
                            Cantidad, CostoUnitario, Descuento, Impuesto, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction);";

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
                                detalle.Impuesto,
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

                    foreach (var detalle in entradasInventarioEntitie.detalleEntradasInventarios)
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
                                string Documento = "EI-" + idEntrada;

                                EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();

                                entradasInventarioEntitie2.IdTipoEntrada = entradasInventarioEntitie.IdTipoEntrada;
                                entradasInventarioEntitie2.IdProveedor = entradasInventarioEntitie.IdProveedor;
                                entradasInventarioEntitie2.OrdenCompra = entradasInventarioEntitie.OrdenCompra;
                                entradasInventarioEntitie2.NumFactura = entradasInventarioEntitie.NumFactura;
                                entradasInventarioEntitie2.Comentario = Documento + " " + entradasInventarioEntitie.Comentario;

                                var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                {
                                    IdProducto = IdProductoHijoSale,
                                    CodigoLote = "",
                                    FechaVencimiento = detalle.FechaVencimiento,
                                    Cantidad = CantidadSaleRedondeada,
                                    CostoUnitario = 0,
                                    Descuento = 0,
                                    Impuesto = 0,
                                    Total = 0
                                };

                                entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                var resp = await CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                                    string Documento = "EI-" + idEntrada;

                                    EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();

                                    entradasInventarioEntitie2.IdTipoEntrada = entradasInventarioEntitie.IdTipoEntrada;
                                    entradasInventarioEntitie2.IdProveedor = entradasInventarioEntitie.IdProveedor;
                                    entradasInventarioEntitie2.OrdenCompra = entradasInventarioEntitie.OrdenCompra;
                                    entradasInventarioEntitie2.NumFactura = entradasInventarioEntitie.NumFactura;
                                    entradasInventarioEntitie2.Comentario = Documento + " " + entradasInventarioEntitie.Comentario;

                                    var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = detalle.FechaVencimiento,
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Descuento = 0,
                                        Impuesto = 0,
                                        Total = 0
                                    };

                                    entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                    var resp = await CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                                    string Documento = "EI-" + idEntrada;

                                    EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();

                                    entradasInventarioEntitie2.IdTipoEntrada = entradasInventarioEntitie.IdTipoEntrada;
                                    entradasInventarioEntitie2.IdProveedor = entradasInventarioEntitie.IdProveedor;
                                    entradasInventarioEntitie2.OrdenCompra = entradasInventarioEntitie.OrdenCompra;
                                    entradasInventarioEntitie2.NumFactura = entradasInventarioEntitie.NumFactura;
                                    entradasInventarioEntitie2.Comentario = Documento + " " + entradasInventarioEntitie.Comentario;

                                    var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = detalle.FechaVencimiento,
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Descuento = 0,
                                        Impuesto = 0,
                                        Total = 0
                                    };

                                    entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                    var resp = await CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                    if (Correcto > 0 || Error > 0) 
                    {
                        response.Message = $"Entrada creada correctamente, al momento de realizar entradas de inventario por podructos padres o hijo Correctos: {Correcto} y Errores: {Error}";
                    }
                    else
                    {
                        response.Message = $"Entrada creada correctamente";
                    }
                    
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string tipo, DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @" SELECT Fecha,IdUserAction,UserName ,Usuario , IdDocumento,Documento, TipoMovimiento, Cantidad, Tipo, IdProducto, Nombre, Sku, 
                                    CodigoBarras, CodigoLote, FechaVencimiento,Costo,Valor,Descuento,Impuesto,Total,Comentario 
                                    FROM
                                    (
                                    SELECT 
                                        e.IdEntradasInventario IdDocumento,
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
                                        e.FechaVencimiento,
										0 Costo,
										e.CostoUnitario Valor,
										e.Descuento,
										e.Impuesto,
                                        ei.Comentario,
										(e.Cantidad * e.CostoUnitario) * (1 - ISNULL(e.Descuento, 0) / 100) Total
                                    FROM T_DetalleEntradasInventario e
                                    INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario
                                    INNER JOIN T_TipoEntrada te ON te.IdTipoEntrada = ei.IdTipoEntrada
                                    INNER JOIN T_Productos p ON p.IdProducto = e.IdProducto
									INNER JOIN T_User usr ON usr.IdUser = e.IdUserAction

                                    UNION ALL

                                    SELECT 
                                        s.IdSalidasInventario IdDocumento,
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
                                        s.FechaVencimiento,
										0 Costo,
										s.CostoUnitario Valor,
										0 Descuento,
										0 Impuesto,
                                        si.Comentario,
										(s.Cantidad * s.CostoUnitario) Total
                                    FROM T_DetalleSalidasInventario s
                                    INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario
                                    INNER JOIN T_TipoSalida ts ON ts.IdTipoSalida = si.IdTipoSalida
                                    INNER JOIN T_Productos p ON p.IdProducto = s.IdProducto
									INNER JOIN T_User usr ON usr.IdUser = s.IdUserAction

                                    UNION ALL

	                                SELECT
                                        vt.IdVenta IdDocumento,
	                                    dvt.CreationDate AS Fecha,
										dvt.IdUserAction,
										usr.UserName,
										CONCAT(dvt.IdUserAction,'-',usr.UserName) Usuario,
										ISNULL(vt.NumberFacturaDIAN,CONCAT(vt.Prefijo,vt.Consecutivo)) Documento,
	                                    'Salida por Venta' AS TipoMovimiento,
	                                    dvt.Cantidad,
	                                    'Ventas' AS Tipo,
	                                    dvt.IdProducto,
	                                    dvt.NombreProducto AS Nombre,
	                                    dvt.Sku,
	                                    dvt.CodigoBarras,
	                                    '' AS CodigoLote,
	                                    '' AS FechaVencimiento,
										dvt.CostoUnitario Costo,
										dvt.PrecioUnitario Valor,
										dvt.Descuento,
										dvt.Impuesto,
                                        '' AS Comentario,
										(dvt.Cantidad * dvt.PrecioUnitario) * (1 - ISNULL(dvt.Descuento, 0) / 100) Total
	                                FROM T_Ventas vt 
									INNER JOIN T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta
									INNER JOIN T_User usr ON usr.IdUser = dvt.IdUserAction

									UNION ALL

									SELECT
                                        nc.IdNotaCredito IdDocumento,
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
	                                    '' AS FechaVencimiento,
										0 Costo,
										ncd.PrecioUnitario,
										ncd.Descuento,
										ncd.Impuesto,
                                        nc.Motivo AS Comentario,
										(ncd.Cantidad * ncd.PrecioUnitario) * (1 - ISNULL(ncd.Descuento, 0) / 100) Total
									FROM T_NotaCredito nc INNER JOIN T_NotaCreditoDetalle ncd ON nc.IdNotaCredito  = ncd.IdNotaCredito
									INNER JOIN T_User usr ON usr.IdUser = ncd.IdUserAction
                                    ) R  ";

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

                    string filtroPorTipo = "";
                    if (tipo != "Todo") 
                    {
                    filtroPorTipo = $" AND TipoMovimiento = '{tipo}'";
                    }

                    string filtroFechas = @" AND (Fecha BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
									        AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) ";

                    sql += Where + filtroFechas + filtroPorTipo;
                    sql += Orderby;

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

                        sql = @" DECLARE @IdMarca INT;

                                -- Buscar si ya existe
                                SELECT @IdMarca = IdMarca FROM T_Marcas WHERE Nombre = @NombreMarca;

                                -- Si no existe, insertar
                                IF @IdMarca IS NULL
                                BEGIN
                                    INSERT INTO T_Marcas (Nombre) VALUES (@NombreMarca);
                                    SET @IdMarca = SCOPE_IDENTITY();
                                END

                                -- Retornar ID
                                SELECT @IdMarca AS IdMarca; ";

                        int idMarca = await connection.ExecuteScalarAsync<int>(sql, new { NombreMarca = (item[3].ToString()?.Trim() == "" ? "N/A" : item[3].ToString()?.Trim()) }, transaction);

                        sql = @$" INSERT INTO T_Productos (Sku,CodigoBarras,Nombre,
                                  CostoBase,PrecioBase,EsInventariable,Impuesto,IdCategoria,IdMarca,IdUnidadMedida,Idtribute,CreationDate, IdUserAction)
                                  VALUES(NULLIF(@Sku,''),NULLIF(@CodigoBarras, ''),@Nombre,
                                  @CostoBase,@PrecioBase,@EsInventariable,@Impuesto,@IdCategoria,@IdMarca,@IdUnidadMedida,@Idtribute,@CreationDate,@IdUserAction);
                                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        var parametrosProducto = new
                        {
                            Sku = item[0],
                            CodigoBarras = item[1],
                            Nombre = item[3].ToString()?.Trim() != "" ? item[2].ToString()?.Trim() + " - " + item[3].ToString()?.Trim() : item[2].ToString()?.Trim(),
                            CostoBase = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                            PrecioBase = Convert.ToDecimal(item[5], new CultureInfo("es-CO")),
                            EsInventariable = 1,
                            Impuesto = Convert.ToDecimal(item[7], new CultureInfo("es-CO")),
                            IdCategoria = 1,
                            IdMarca = idMarca,
                            IdUnidadMedida = 1,
                            Idtribute = item[8].ToString()?.Trim() == "IVA" ? 1 : item[8].ToString()?.Trim() == "INC" ? 2 : 1,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int idProducto = await connection.ExecuteScalarAsync<int>(sql, parametrosProducto, transaction);

                        decimal Cantidad = Convert.ToDecimal(item[6], new CultureInfo("es-CO"));

                        if (Cantidad > 0)
                        {
                            sql = @"INSERT INTO T_EntradasInventario (IdTipoEntrada, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoEntrada, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                            int idEntrada = await connection.ExecuteScalarAsync<int>(sql,
                            new
                            {
                                IdTipoEntrada = 2,
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
                            Cantidad, CostoUnitario, Descuento, Impuesto, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction);";

                            DateTime FechaVencimiento = new DateTime();
                            if (!string.IsNullOrEmpty(item[9].ToString()?.Trim())) 
                            {
                                FechaVencimiento = Convert.ToDateTime(item[9].ToString()?.Trim());
                                FechaVencimiento = Convert.ToDateTime(FechaVencimiento.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                FechaVencimiento = Convert.ToDateTime("1900-01-01");
                            }

                                await connection.ExecuteAsync(
                                        sql,
                                        new
                                        {
                                            IdEntradasInventario = idEntrada,
                                            idProducto,
                                            CodigoLote = "",
                                            FechaVencimiento,
                                            Cantidad = Convert.ToDecimal(item[6], new CultureInfo("es-CO")),
                                            CostoUnitario = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                                            Descuento = 0,
                                            Impuesto = 0,
                                            CreationDate = FechaActual,
                                            IdUserAction = IdUser
                                        },
                                        transaction);
                        }
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

        public async Task<Response<dynamic>> CreateUpdateAuxiliar(EntradasInventarioEntitie entradasInventarioEntitie, int IdUser)
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
                            Cantidad, CostoUnitario, Descuento, Impuesto, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction);";

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
                                detalle.Impuesto,
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

        public async Task<Response<dynamic>> List(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@" SELECT A.IdEntradasInventario
                                      ,A.IdTipoEntrada
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
                                      ,B.Descuento
                                      ,B.Impuesto
                                  FROM T_EntradasInventario A
                                  INNER JOIN T_DetalleEntradasInventario B ON A.IdEntradasInventario = B.IdEntradasInventario
                                  INNER JOIN T_TipoEntrada C ON C.IdTipoEntrada = A.IdTipoEntrada
                                  INNER JOIN T_Proveedores D ON D.IdProveedor = A.IdProveedor
                                  INNER JOIN T_Productos E ON E.IdProducto = B.IdProducto
                                  WHERE A.IdEntradasInventario = {Id} ";

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

        public async Task<Response<dynamic>> CargueMasivoEditarProductoEntradaSalidas(DataTable Datos, int IdUser)
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

                        sql = @$" UPDATE T_Productos SET Sku = NULLIF(@Sku,''),CodigoBarras = NULLIF(@CodigoBarras, ''),Nombre = @Nombre,
                                  CostoBase = @CostoBase,PrecioBase = @PrecioBase,Impuesto = @Impuesto,Idtribute = @Idtribute,UpdateDate = @UpdateDate, IdUserAction = @IdUserAction 
                                  WHERE IdProducto = @Id; ";

                        var parametrosProducto = new
                        {
                            Id = item[0],
                            Sku = item[1],
                            CodigoBarras = item[2],
                            Nombre = item[3].ToString()?.Trim(),
                            CostoBase = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                            PrecioBase = Convert.ToDecimal(item[5], new CultureInfo("es-CO")),
                            Impuesto = Convert.ToDecimal(item[6], new CultureInfo("es-CO")),
                            Idtribute = item[7].ToString()?.Trim() == "IVA" ? 1 : item[7].ToString()?.Trim() == "INC" ? 2 : 1,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametrosProducto, transaction);

                        string Movimiento = item[8].ToString()!.Trim();

                        decimal Cantidad = Convert.ToDecimal(item[9], new CultureInfo("es-CO"));

                        if (Cantidad > 0)
                        {
                            if (Movimiento == "Entrada")
                            {
                                sql = @"INSERT INTO T_EntradasInventario (IdTipoEntrada, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoEntrada, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                                int idEntrada = await connection.ExecuteScalarAsync<int>(sql,
                                new
                                {
                                    IdTipoEntrada = 2,
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
                            Cantidad, CostoUnitario, Descuento, Impuesto, CreationDate, IdUserAction)
                            VALUES (@IdEntradasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction);";

                                int IdProd = Convert.ToInt32(item[0]);

                                DateTime FechaVencimiento = new DateTime();
                                if (!string.IsNullOrEmpty(item[10].ToString()?.Trim()))
                                {
                                    FechaVencimiento = Convert.ToDateTime(item[10].ToString()?.Trim());
                                    FechaVencimiento = Convert.ToDateTime(FechaVencimiento.ToString("yyyy-MM-dd"));
                                }
                                else
                                {
                                    FechaVencimiento = Convert.ToDateTime("1900-01-01");
                                }

                                await connection.ExecuteAsync(
                                        sql,
                                        new
                                        {
                                            IdEntradasInventario = idEntrada,
                                            IdProducto = IdProd,
                                            CodigoLote = "",
                                            FechaVencimiento,
                                            Cantidad = Convert.ToDecimal(item[9], new CultureInfo("es-CO")),
                                            CostoUnitario = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                                            Descuento = 0,
                                            Impuesto = 0,
                                            CreationDate = FechaActual,
                                            IdUserAction = IdUser
                                        },
                                        transaction);
                            }
                            else if (Movimiento == "Salida")
                            {
                                sql = @"INSERT INTO T_SalidasInventario (IdTipoSalida, IdProveedor, OrdenCompra, NumFactura, Comentario, CreationDate, IdUserAction)
                            VALUES (@IdTipoSalida, NULLIF(@IdProveedor,0), @OrdenCompra, @NumFactura, @Comentario, @CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                                int idSalida = await connection.ExecuteScalarAsync<int>(sql,
                                new
                                {
                                    IdTipoSalida = 2,
                                    IdProveedor = 1,
                                    OrdenCompra = "",
                                    NumFactura = "",
                                    Comentario = "",
                                    CreationDate = FechaActual,
                                    IdUserAction = IdUser
                                },
                                transaction);

                                sql = @"INSERT INTO T_DetalleSalidasInventario (
                            IdDetalleSalidasInventario, IdProducto, CodigoLote, FechaVencimiento,
                            Cantidad, CostoUnitario, CreationDate, IdUserAction)
                            VALUES (@IdDetalleSalidasInventario, @IdProducto, @CodigoLote, @FechaVencimiento,
                                    @Cantidad, @CostoUnitario, @CreationDate, @IdUserAction);";

                                int IdProd = Convert.ToInt32(item[0]);

                                DateTime FechaVencimiento = new DateTime();
                                if (!string.IsNullOrEmpty(item[10].ToString()?.Trim()))
                                {
                                    FechaVencimiento = Convert.ToDateTime(item[10].ToString()?.Trim());
                                    FechaVencimiento = Convert.ToDateTime(FechaVencimiento.ToString("yyyy-MM-dd"));
                                }
                                else
                                {
                                    FechaVencimiento = Convert.ToDateTime("1900-01-01");
                                }

                                await connection.ExecuteAsync(
                                        sql,
                                        new
                                        {
                                            IdDetalleSalidasInventario = idSalida,
                                            IdProducto = IdProd,
                                            CodigoLote = "",
                                            FechaVencimiento,
                                            Cantidad = Convert.ToDecimal(item[9], new CultureInfo("es-CO")),
                                            CostoUnitario = Convert.ToDecimal(item[4], new CultureInfo("es-CO")),
                                            CreationDate = FechaActual,
                                            IdUserAction = IdUser
                                        },
                                        transaction);
                            }
                        }
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Message = "Edicion de producto de forma masiva realizada correctamente";
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
