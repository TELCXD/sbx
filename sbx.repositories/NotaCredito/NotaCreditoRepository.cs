using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Entities.NotaCredito;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.NotaCredito;
using sbx.repositories.EntradaInventario;

namespace sbx.repositories.NotaCredito
{
    public class NotaCreditoRepository : INotaCredito
    {
        private readonly string _connectionString;

        public NotaCreditoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Create(NotaCreditoEntitie notaCredito, int IdUser)
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

                    sql = @$" INSERT INTO T_NotaCredito (Prefijo,Consecutivo,IdVenta,Motivo,Estado,CreationDate, IdUserAction)
                              VALUES(@Prefijo,
                              (SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM T_NotaCredito WHERE Prefijo = @Prefijo),
                               @IdVenta, @Motivo,@Estado,@CreationDate,@IdUserAction);
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var parametros = new
                    {
                        notaCredito.Prefijo,
                        notaCredito.Consecutivo,
                        notaCredito.IdVenta,
                        notaCredito.Motivo,
                        notaCredito.Estado,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idNotaCredito = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_NotaCreditoDetalle (
                            IdNotaCredito, IdDetalleVenta, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, Descuento,NombreTributo, Impuesto,CreationDate, IdUserAction)
                            VALUES (@IdNotaCredito, @IdDetalleVenta, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @Descuento,@NombreTributo, @Impuesto, @CreationDate, @IdUserAction)";

                    foreach (var detalle in notaCredito.detalleNotaCredito)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdNotaCredito = idNotaCredito,
                                detalle.IdDetalleVenta,
                                detalle.IdProducto,
                                detalle.Sku,
                                detalle.CodigoBarras,
                                detalle.NombreProducto,
                                detalle.Cantidad,
                                detalle.UnidadMedida,
                                detalle.PrecioUnitario,
                                detalle.Descuento,
                                detalle.NombreTributo,
                                detalle.Impuesto,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    sql = @$" UPDATE T_Ventas SET Estado = 'ANULADA', UpdateDate = @UpdateDate, IdUserActionNotaCredito = @IdUserAction WHERE IdVenta = @IdVenta ";

                    var parametrosVenta = new
                    {
                        notaCredito.IdVenta,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametrosVenta, transaction);

                    await transaction.CommitAsync();
                    committed = true;

                    int Error = 0;
                    int Correcto = 0;
                    int contador = 0;
                    decimal CantidadSaleRedondeadaTemp = 0;

                    sql = " SELECT Consecutivo FROM T_NotaCredito WHERE IdNotaCredito = " + idNotaCredito;
                    int consecutivo = connection.QuerySingle<int>(sql);

                    foreach (var detalle in notaCredito.detalleNotaCredito)
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

                                string Documento = notaCredito.Prefijo + consecutivo;

                                EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();
                                EntradaInventarioRepository entradaInventarioRepository = new EntradaInventarioRepository(_connectionString);

                                entradasInventarioEntitie2.IdTipoEntrada = Convert.ToInt32(3);
                                entradasInventarioEntitie2.IdProveedor = 1;
                                entradasInventarioEntitie2.OrdenCompra = "";
                                entradasInventarioEntitie2.NumFactura = "";
                                entradasInventarioEntitie2.Comentario = $"{Documento} Entrada por nota credito de producto padre";

                                var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                {
                                    IdProducto = IdProductoHijoSale,
                                    CodigoLote = "",
                                    FechaVencimiento = DateTime.Parse("1900-01-01"),
                                    Cantidad = CantidadSaleRedondeada,
                                    CostoUnitario = 0,
                                    Descuento = 0,
                                    Impuesto = 0,
                                    Total = 0
                                };

                                entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                var resp = await entradaInventarioRepository.CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                                    string Documento = notaCredito.Prefijo + consecutivo;

                                    EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();
                                    EntradaInventarioRepository entradaInventarioRepository = new EntradaInventarioRepository(_connectionString);

                                    entradasInventarioEntitie2.IdTipoEntrada = Convert.ToInt32(3);
                                    entradasInventarioEntitie2.IdProveedor = 1;
                                    entradasInventarioEntitie2.OrdenCompra = "";
                                    entradasInventarioEntitie2.NumFactura = "";
                                    entradasInventarioEntitie2.Comentario = $"{Documento} Entrada por nota credito de producto Hijo";

                                    var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = DateTime.Parse("1900-01-01"),
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Descuento = 0,
                                        Impuesto = 0,
                                        Total = 0
                                    };

                                    entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                    var resp = await entradaInventarioRepository.CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                                    string Documento = notaCredito.Prefijo + consecutivo;

                                    EntradasInventarioEntitie entradasInventarioEntitie2 = new EntradasInventarioEntitie();
                                    EntradaInventarioRepository entradaInventarioRepository = new EntradaInventarioRepository(_connectionString);

                                    entradasInventarioEntitie2.IdTipoEntrada = Convert.ToInt32(3);
                                    entradasInventarioEntitie2.IdProveedor = 1;
                                    entradasInventarioEntitie2.OrdenCompra = "";
                                    entradasInventarioEntitie2.NumFactura = "";
                                    entradasInventarioEntitie2.Comentario = $"{Documento} Entrada por nota credito de producto padre";

                                    var nuevoDetalle = new DetalleEntradasInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        CodigoLote = "",
                                        FechaVencimiento = DateTime.Parse("1900-01-01"),
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Descuento = 0,
                                        Impuesto = 0,
                                        Total = 0
                                    };

                                    entradasInventarioEntitie2.detalleEntradasInventarios.Add(nuevoDetalle);
                                    var resp = await entradaInventarioRepository.CreateUpdateAuxiliar(entradasInventarioEntitie2, IdUser);

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
                    response.Data = idNotaCredito;
                    response.Message = $"Nota credito creada correctamente, al momento de realizar entradas de inventario por podructos padres o hijo Correctos: {Correcto} y Errores: {Error}";
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

        public async Task<Response<dynamic>> List(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    A.IdNotaCredito,
                                    ISNULL(NULLIF(A.NumberNotaCreditoDIAN, ''),CONCAT(A.Prefijo,A.Consecutivo)) NotaCredito,
                                    A.Motivo,
                                    A.IdVenta,
                                    D.Prefijo,
									D.Consecutivo,
                                    ISNULL(D.NumberFacturaDIAN,CONCAT(D.Prefijo,D.Consecutivo)) Factura,
									A.IdUserAction AS IdUserActionNotaCredito,
									CONCAT(A.IdUserAction, '-', C.UserName) AS Usuario,
                                    C.UserName,
                                    A.Estado,
                                    ISNULL(A.NumberNotaCreditoDIAN,'') NumberNotaCreditoDIAN,
                                    ISNULL(A.EstadoNotaCreditoDIAN,'') EstadoNotaCreditoDIAN,
                                    A.NotaCreditoJSON,
                                    B.IdNotaCreditoDetalle,
                                    B.IdDetalleVenta,	
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.Cantidad,
                                    B.UnidadMedida,
                                    B.PrecioUnitario,
                                    B.Descuento,
                                    B.NombreTributo,
                                    B.Impuesto,
                                    B.CreationDate,
                                    B.IdUserAction AS IdUserActionNotaCreditoDetalle
                                    FROM T_NotaCredito A 
                                    INNER JOIN T_NotaCreditoDetalle B ON B.IdNotaCredito = A.IdNotaCredito
                                    INNER JOIN T_Ventas D ON D.IdVenta = A.IdVenta
									INNER JOIN T_User C ON C.IdUser = A.IdUserAction ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdNotaCredito = {Id}";
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

        public async Task<Response<dynamic>> ActualizarDataNotaCreditoElectronica(ActualizarNotaCreditoForNotaCreditoElectronicaEntitie actualizarNotaCreditoForNotaCreditoElectronicaEntitie, int IdUser)
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

                    sql = @$" UPDATE T_NotaCredito SET NumberNotaCreditoDIAN = @NumberNotaCreditoDIAN,
                              EstadoNotaCreditoDIAN = @EstadoNotaCreditoDIAN, NotaCreditoJSON = @NotaCreditoJSON,
                              UpdateDate = @UpdateDate, IdUserAction = @IdUserAction
                              WHERE IdNotaCredito = @IdNotaCredito ";

                    var parametros = new
                    {
                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.IdNotaCredito,
                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NumberNotaCreditoDIAN,
                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.EstadoNotaCreditoDIAN,
                        actualizarNotaCreditoForNotaCreditoElectronicaEntitie.NotaCreditoJSON,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Nota credito actualiza con datos factura electronica correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Se presento un error al intentar actualiza con datos nota credito electronica";
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Flag = false;
                    response.Message = "Error: " + ex.Message;
                    response.Data = 0;
                    return response;
                }
            }
        }

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, string clientVenta, DateTime FechaInicio, DateTime FechaFin, int idUser, string RolName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string FiltroPorUsuario = "";

                    if (RolName != "Administrador")
                    {
                        FiltroPorUsuario = $" AND A.IdUserAction = {idUser} ";
                    }

                    string sql = @" SELECT 
									A.IdNotaCredito,
                                    A.IdVenta,
                                    A.Prefijo,
                                    A.Consecutivo,
                                    CONCAT(A.Prefijo,A.Consecutivo) NotaCredito,
                                    A.Estado,
                                    ISNULL(A.EstadoNotaCreditoDIAN,'') EstadoNotaCreditoDIAN,
									ISNULL(A.NumberNotaCreditoDIAN,'') NumberNotaCreditoDIAN,
									A.NotaCreditoJSON,
                                    ISNULL((SELECT IdNotaCredito FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) IdNotaCredito,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.Cantidad,
									B.UnidadMedida,
                                    B.Descuento,
                                    B.NombreTributo,
                                    B.Impuesto,                                                                                             
                                    A.CreationDate,
									G.UserName UserName,
                                    A.IdUserAction 
                                    FROM T_NotaCredito A
                                    INNER JOIN T_NotaCreditoDetalle B ON A.IdNotaCredito = B.IdNotaCredito
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    WHERE 
                                    A.CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120) " + FiltroPorUsuario;

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName LIKE @Filtro ";
                                    }
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
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) LIKE @Filtro ";
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
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto = @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName = @Filtro ";
                                    }
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
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) = @Filtro ";
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
                                    if (clientVenta == "Producto")
                                    {
                                        Where = $" AND B.NombreProducto LIKE @Filtro ";
                                    }
                                    else if (clientVenta == "Usuario")
                                    {
                                        Where = $" AND G.UserName LIKE @Filtro ";
                                    }
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
                                    Where = $" AND CONCAT(A.Prefijo,A.Consecutivo) LIKE @Filtro ";
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

                    if (resultado.Count == 0 && campoFiltro == "Prefijo-Consecutivo")
                    {
                        sql = sql.Replace("AND CONCAT(A.Prefijo,A.Consecutivo)", "AND A.NumberNotaCreditoDIAN");

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
