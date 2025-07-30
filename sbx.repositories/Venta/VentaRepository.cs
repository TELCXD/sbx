using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.Venta;
using sbx.repositories.SalidaInventario;

namespace sbx.repositories.Venta
{
    public class VentaRepository : IVenta
    {
        private readonly string _connectionString;

        public VentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Create(VentaEntitie ventaEntitie, int IdUser)
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

                    sql = @$" INSERT INTO T_Ventas (Prefijo,Consecutivo,IdCliente,IdVendedor,IdMetodoPago,Estado,CreationDate, IdUserAction)
                            VALUES(@Prefijo,
                            (SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM T_Ventas WHERE Prefijo = @Prefijo),
                            @IdCliente,@IdVendedor,@IdMetodoPago,@Estado,@CreationDate,@IdUserAction);
                            SELECT CAST(SCOPE_IDENTITY() AS INT); ";

                    var parametros = new
                    {
                        ventaEntitie.Prefijo,
                        ventaEntitie.Consecutivo,
                        ventaEntitie.IdCliente,
                        ventaEntitie.IdVendedor,
                        ventaEntitie.IdMetodoPago,
                        ventaEntitie.Estado,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idVenta = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_DetalleVenta (
                            IdVenta, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, CostoUnitario, Descuento, Impuesto, NombreTributo,CreationDate, IdUserAction)
                            VALUES (@IdVenta, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @CostoUnitario, @Descuento, @Impuesto, @NombreTributo, @CreationDate, @IdUserAction)";

                    foreach (var detalle in ventaEntitie.detalleVentas)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta = idVenta,
                                detalle.IdProducto,
                                detalle.Sku,
                                detalle.CodigoBarras,
                                detalle.NombreProducto,
                                detalle.Cantidad,
                                detalle.UnidadMedida,
                                detalle.PrecioUnitario,
                                detalle.CostoUnitario,
                                detalle.Descuento,
                                detalle.Impuesto,
                                detalle.NombreTributo,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    sql = @"INSERT INTO T_PagosVenta (
                            IdVenta, IdMetodoPago, Recibido, Monto, Referencia, IdBanco, CreationDate, IdUserAction)
                            VALUES (@IdVenta, @IdMetodoPago, @Recibido, @Monto, @Referencia, @IdBanco, @CreationDate, @IdUserAction)";

                    foreach (var detallePago in ventaEntitie.pagosVenta)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta = idVenta,
                                detallePago.IdMetodoPago,
                                detallePago.Recibido,
                                detallePago.Monto,
                                detallePago.Referencia,
                                detallePago.IdBanco,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();
                    committed = true;

                    int Error = 0;
                    int Correcto = 0;
                    int contador = 0;
                    decimal CantidadSaleRedondeadaTemp = 0;

                    string Factura = await ListFactura(idVenta);

                    foreach (var detalle in ventaEntitie.detalleVentas)
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

                                SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();
                                SalidaInventarioRepository inventarioRepository = new SalidaInventarioRepository(_connectionString);

                                salidaInventarioEntitie.IdTipoSalida = Convert.ToInt32(4);
                                salidaInventarioEntitie.OrdenCompra = "";
                                salidaInventarioEntitie.NumFactura = "";
                                salidaInventarioEntitie.Comentario = $"{Factura} Salida por venta de producto padre";

                                var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                {
                                    IdProducto = IdProductoHijoSale,
                                    Sku = item.SkuHijo,
                                    CodigoBarras = item.CodigoBarrasHijo,
                                    Nombre = item.NombreHijo,
                                    CodigoLote = "",
                                    FechaVencimiento = DateTime.Parse("1900-01-01"),
                                    Cantidad = CantidadSaleRedondeada,
                                    CostoUnitario = 0,
                                    Total = 0
                                };

                                salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);
                                var resp = await inventarioRepository.CreateUpdateAuxiliar(salidaInventarioEntitie, IdUser);

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

                                    SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();
                                    SalidaInventarioRepository inventarioRepository = new SalidaInventarioRepository(_connectionString);

                                    salidaInventarioEntitie.IdTipoSalida = Convert.ToInt32(4);
                                    salidaInventarioEntitie.OrdenCompra = "";
                                    salidaInventarioEntitie.NumFactura = "";
                                    salidaInventarioEntitie.Comentario = $"{Factura} Salida por venta de producto hijo";

                                    var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        Sku = item.SkuPadre,
                                        CodigoBarras = item.CodigoBarrasPadre,
                                        Nombre = item.NombrePadre,
                                        CodigoLote = "",
                                        FechaVencimiento = DateTime.Parse("1900-01-01"),
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Total = 0
                                    };

                                    salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);
                                    var resp = await inventarioRepository.CreateUpdateAuxiliar(salidaInventarioEntitie, IdUser);

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

                                    SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();
                                    SalidaInventarioRepository inventarioRepository = new SalidaInventarioRepository(_connectionString);

                                    salidaInventarioEntitie.IdTipoSalida = Convert.ToInt32(4);
                                    salidaInventarioEntitie.OrdenCompra = "";
                                    salidaInventarioEntitie.NumFactura = "";
                                    salidaInventarioEntitie.Comentario = $"{Factura} Salida por venta de producto hijo";

                                    var nuevoDetalle = new DetalleSalidaInventarioEntitie
                                    {
                                        IdProducto = IdProductoPadreSale,
                                        Sku = item.SkuPadre,
                                        CodigoBarras = item.CodigoBarrasPadre,
                                        Nombre = item.NombrePadre,
                                        CodigoLote = "",
                                        FechaVencimiento = DateTime.Parse("1900-01-01"),
                                        Cantidad = CantidadSaleRedondeada,
                                        CostoUnitario = 0,
                                        Total = 0
                                    };

                                    salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);
                                    var resp = await inventarioRepository.CreateUpdateAuxiliar(salidaInventarioEntitie, IdUser);

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
                    response.Data = idVenta;
                    response.Message = $"Venta creada correctamente, al momento de realizar salidas de inventario por podructos padres o hijo Correctos: {Correcto} y Errores: {Error} ";
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
                    response.Data = 0;
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
                                    A.IdVenta,
                                    A.Prefijo,
                                    A.Consecutivo,
                                    CONCAT(A.Prefijo,A.Consecutivo) Factura,
                                    A.CreationDate FechaFactura,
                                    A.IdUserAction IdUserActionFactura,
                                    A.IdVendedor,
                                    A.Estado,
                                    ISNULL(A.EstadoFacturaDIAN,'') EstadoFacturaDIAN,
									ISNULL(A.NumberFacturaDIAN,'') NumberFacturaDIAN,
									A.FacturaJSON,
                                    ISNULL((SELECT NT.IdNotaCredito FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) IdNotaCredito,
									ISNULL((SELECT NT.Prefijo FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) PrefijoNotaCredito,
									ISNULL((SELECT NT.Consecutivo FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) ConsecutivoNotaCredito,
                                    ISNULL((SELECT ISNULL(NULLIF(NT.NumberNotaCreditoDIAN, ''),CONCAT(NT.Prefijo,NT.Consecutivo)) FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) NotaCredito,
									ISNULL((SELECT NT.EstadoNotaCreditoDIAN FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) EstadoNotaCreditoDIAN,
									ISNULL((SELECT NT.NumberNotaCreditoDIAN FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) NumberNotaCreditoDIAN,
									ISNULL((SELECT NT.NotaCreditoJSON FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) NotaCreditoJSON,
                                    ISNULL((SELECT NT.Motivo FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), '') MotivoNotaCredito,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameFactura,
                                    B.IdDetalleVenta,
                                    B.IdProducto,
                                    P.Nombre NombreProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    P.IdUnidadMedida,
                                    B.UnidadMedida,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleFactura,
                                    B.IdUserAction IdUserActionDetalleFactura,
                                    --H.UserName UserNameDetalleFactura,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial,
                                    D.IdIdentificationType,
                                    ISNULL(D.Direccion,'') Direccion,
									ISNULL(D.Email,'') Email,
									ISNULL(D.Telefono,'') Telefono,
                                    A.IdMetodoPago,
                                    F.Nombre NombreMetodoPago,
                                    C.Referencia,
                                    C.IdBanco,
                                    E.Nombre NombreBanco,
                                    C.Recibido,
                                    C.Monto,
                                    C.CreationDate FechaPagoFactura,
                                    C.IdUserAction IdUserActionPagoFactura 
                                    --,I.UserName UserNamePagoFactura
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_PagosVenta C ON A.IdVenta = C.IdVenta
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_Bancos E ON C.IdBanco = E.IdBanco
                                    INNER JOIN T_MetodoPago F ON F.IdMetodoPago = A.IdMetodoPago
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    --INNER JOIN T_User H ON H.IdUser = B.IdUserAction
                                    --INNER JOIN T_User I ON H.IdUser = C.IdUserAction 
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor 
                                    INNER JOIN T_Productos P ON P.IdProducto = B.IdProducto ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdVenta = {Id}";
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

                    string sql = @"SELECT 
                                    A.IdVenta,
                                    A.Prefijo,
                                    A.Consecutivo,
                                    CONCAT(A.Prefijo,A.Consecutivo) Factura,
                                    A.CreationDate FechaFactura,
                                    A.IdUserAction IdUserActionFactura,
                                    A.IdVendedor,
                                    A.Estado,
                                    ISNULL(A.EstadoFacturaDIAN,'') EstadoFacturaDIAN,
									ISNULL(A.NumberFacturaDIAN,'') NumberFacturaDIAN,
									A.FacturaJSON,
                                    ISNULL((SELECT IdNotaCredito FROM T_NotaCredito NT WHERE NT.IdVenta = A.IdVenta), 0) IdNotaCredito,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameFactura,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleFactura,
                                    B.IdUserAction IdUserActionDetalleFactura,
                                    --H.UserName UserNameDetalleFactura,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial,
                                    A.IdMetodoPago,
                                    F.Nombre NombreMetodoPago,
                                    C.Referencia,
                                    C.IdBanco,
                                    E.Nombre NombreBanco,
                                    C.Monto,
                                    C.CreationDate FechaPagoFactura,
                                    C.IdUserAction IdUserActionPagoFactura 
                                    --,I.UserName UserNamePagoFactura
                                    FROM T_Ventas A
                                    INNER JOIN T_DetalleVenta B ON A.IdVenta = B.IdVenta
                                    INNER JOIN T_PagosVenta C ON A.IdVenta = C.IdVenta
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_Bancos E ON C.IdBanco = E.IdBanco
                                    INNER JOIN T_MetodoPago F ON F.IdMetodoPago = A.IdMetodoPago
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    --INNER JOIN T_User H ON H.IdUser = B.IdUserAction
                                    --INNER JOIN T_User I ON H.IdUser = C.IdUserAction 
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor
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

        public async Task<Response<dynamic>> VentasTotales(int IdUser, DateTime FechaHoraApertura)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaActual = DateTime.Now;
                FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                DateTime FechaApertura = FechaHoraApertura;
                FechaApertura = Convert.ToDateTime(FechaApertura.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT
                                    B.IdDetalleVenta,
                                    B.IdVenta,
									A.Estado,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.Cantidad,
                                    B.UnidadMedida,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate,
                                    B.IdUserAction 
                                    FROM T_Ventas A INNER JOIN T_DetalleVenta B ON B.IdVenta = A.IdVenta 
                                    WHERE 
                                    A.IdUserAction = @IdUser 
                                    AND 
									A.Estado = 'FACTURADA' 
                                    AND 
                                    A.CreationDate BETWEEN CONVERT(DATETIME,@FechaApertura,120) AND CONVERT(DATETIME,@FechaActual,120) ";

                    dynamic resultado = await connection.QueryAsync(sql, new { IdUser , FechaActual, FechaApertura });

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

        public async Task<Response<dynamic>> BuscarFactura(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();     

                try
                {
                    await connection.OpenAsync();
                   
                    string sql = @"SELECT A.IdVenta,                                
                                    ISNULL(A.NumberFacturaDIAN,CONCAT(A.Prefijo,A.Consecutivo)) Factura,
                                    A.CreationDate FechaFactura,                                                              
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial
                                    FROM T_Ventas A                                 
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Factura":
                                    Where = $"WHERE CONCAT(A.Prefijo,'-',A.Consecutivo) LIKE @Filtro ";
                                    break;
                                case "Identificacion cliente":
                                    Where = $"WHERE D.NumeroDocumento LIKE @Filtro ";
                                    break;
                                case "Nombre cliente":
                                    Where = $"WHERE D.NombreRazonSocial LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato + "%";
                            break;
                        case "Igual a":
                            switch (campoFiltro)
                            {
                                case "Factura":
                                    Where = $"WHERE CONCAT(A.Prefijo,'-',A.Consecutivo) = @Filtro ";
                                    break;
                                case "Identificacion cliente":
                                    Where = $"WHERE D.NumeroDocumento = @Filtro ";
                                    break;
                                case "Nombre cliente":
                                    Where = $"WHERE D.NombreRazonSocial = @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato;
                            break;
                        case "Contiene":
                            switch (campoFiltro)
                            {
                                case "Factura":
                                    Where = $"WHERE CONCAT(A.Prefijo,'-',A.Consecutivo) LIKE @Filtro ";
                                    break;
                                case "Identificacion cliente":
                                    Where = $"WHERE D.NumeroDocumento LIKE @Filtro ";
                                    break;
                                case "Nombre cliente":
                                    Where = $"WHERE D.NombreRazonSocial LIKE @Filtro ";
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

        public async Task<Response<dynamic>> CreateSuspendida(VentaSuspendidaEntitie ventaSuspendidaEntitie, int IdUser)
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

                    sql = @$" INSERT INTO T_Ventas_Suspendidas (IdListaPrecio,IdCliente,IdVendedor,IdMetodoPago,CreationDate, IdUserAction)
                                  VALUES(@IdListaPrecio,@IdCliente,@IdVendedor,@IdMetodoPago,@CreationDate,@IdUserAction);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var parametros = new
                    {
                        ventaSuspendidaEntitie.IdListaPrecio,
                        ventaSuspendidaEntitie.IdCliente,
                        ventaSuspendidaEntitie.IdVendedor,
                        ventaSuspendidaEntitie.IdMetodoPago,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idVenta = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_DetalleVenta_Suspendidas (
                            IdVenta_Suspendidas, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, CostoUnitario, Descuento, Impuesto,CreationDate, IdUserAction)
                            VALUES (@IdVenta_Suspendidas, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @CostoUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction)";

                    foreach (var detalle in ventaSuspendidaEntitie.detalleVentasSuspendida)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta_Suspendidas = idVenta,
                                detalle.IdProducto,
                                detalle.Sku,
                                detalle.CodigoBarras,
                                detalle.NombreProducto,
                                detalle.Cantidad,
                                detalle.UnidadMedida,
                                detalle.PrecioUnitario,
                                detalle.CostoUnitario,
                                detalle.Descuento,
                                detalle.Impuesto,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    sql = @"INSERT INTO T_PagosVenta_Suspendidas (
                            IdVenta_Suspendidas, IdMetodoPago, Recibido, Monto, Referencia, IdBanco, CreationDate, IdUserAction)
                            VALUES (@IdVenta_Suspendidas, @IdMetodoPago, @Recibido, @Monto, @Referencia, @IdBanco, @CreationDate, @IdUserAction)";

                    foreach (var detallePago in ventaSuspendidaEntitie.pagosVentaSuspendida)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta_Suspendidas = idVenta,
                                detallePago.IdMetodoPago,
                                detallePago.Recibido,
                                detallePago.Monto,
                                detallePago.Referencia,
                                detallePago.IdBanco,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Data = idVenta;
                    response.Message = "Venta suspendida correctamente";
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

        public async Task<Response<dynamic>> VentasSuspendidas(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    A.IdVenta_Suspendidas,                                   
                                    A.CreationDate FechaFactura,
                                    A.IdUserAction IdUserActionFactura,
                                    A.IdListaPrecio,
                                    A.IdVendedor,
                                    J.NumeroDocumento NumeroDocumentoVendedor,
                                    J.Nombre NombreVendedor,
                                    J.Apellido ApellidoVendedor,
                                    CONCAT(J.Nombre,' ',J.Apellido) NombreCompletoVendedor, 
                                    G.UserName UserNameFactura,
                                    B.IdDetalleVenta_Suspendidas,
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.UnidadMedida,
                                    B.NombreProducto,
                                    B.PrecioUnitario,
                                    B.CostoUnitario,
                                    B.Cantidad,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate FechaDetalleFactura,
                                    B.IdUserAction IdUserActionDetalleFactura,
                                    A.IdCliente,
                                    D.NumeroDocumento,
                                    D.NombreRazonSocial,
                                    A.IdMetodoPago,
                                    F.Nombre NombreMetodoPago,
                                    C.Referencia,
                                    C.IdBanco,
                                    E.Nombre NombreBanco,
                                    C.Recibido,
                                    C.Monto,
                                    C.CreationDate FechaPagoFactura,
                                    C.IdUserAction IdUserActionPagoFactura 
                                    FROM T_Ventas_Suspendidas A
                                    INNER JOIN T_DetalleVenta_Suspendidas B ON A.IdVenta_Suspendidas = B.IdVenta_Suspendidas
                                    INNER JOIN T_PagosVenta_Suspendidas C ON A.IdVenta_Suspendidas = C.IdVenta_Suspendidas
                                    INNER JOIN T_Cliente D ON A.IdCliente = D.IdCliente
                                    INNER JOIN T_Bancos E ON C.IdBanco = E.IdBanco
                                    INNER JOIN T_MetodoPago F ON F.IdMetodoPago = A.IdMetodoPago
                                    INNER JOIN T_User G ON G.IdUser = A.IdUserAction
                                    INNER JOIN T_Vendedor J ON J.IdVendedor = A.IdVendedor ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdVenta_Suspendidas = {Id}";
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

        public async Task<Response<dynamic>> ListVentasSuspendidas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT
                                    A.IdVenta_Suspendidas,
                                    A.CreationDate,
                                    A.IdCliente,
                                    B.NumeroDocumento,
                                    B.NombreRazonSocial
                                    FROM T_Ventas_Suspendidas A
                                    INNER JOIN T_Cliente B ON A.IdCliente = B.IdCliente ";

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

        public async Task<Response<dynamic>> EliminarVentasSuspendidas(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                await connection.OpenAsync();

                using var transaction = connection.BeginTransaction();

                try
                {
                    string sql = $@"DELETE FROM T_PagosVenta_Suspendidas WHERE IdVenta_Suspendidas = {Id} ";

                    int filasAfectadas = await connection.ExecuteAsync(sql,null, transaction);

                    if (filasAfectadas > 0)
                    {
                        sql = $@"DELETE FROM T_DetalleVenta_Suspendidas WHERE IdVenta_Suspendidas = {Id} ";

                        filasAfectadas = await connection.ExecuteAsync(sql,null, transaction);

                        if (filasAfectadas > 0)
                        {
                            sql = $@"DELETE FROM T_Ventas_Suspendidas WHERE IdVenta_Suspendidas = {Id} ";

                            filasAfectadas = await connection.ExecuteAsync(sql, null, transaction);

                            if (filasAfectadas > 0) 
                            {
                                await transaction.CommitAsync();

                                response.Flag = true;
                                response.Message = "Proceso realizado correctamente";
                            }
                        }
                    }

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

        public async Task<string> ListFactura(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = $@" SELECT                                   
                                    CONCAT(Prefijo,'-',Consecutivo) Factura
                                    FROM T_Ventas WHERE IdVenta = {Id}";

                    string Factura = await connection.QueryFirstOrDefaultAsync<string>(sql) ?? "";

                    return Factura;
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
        }

        public async Task<Response<dynamic>> IdentificaProductoPadreNivel1(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@" WITH JerarquiaPadres AS (
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
                                        WHERE A.IdProductoHijo = {IdProducto}

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

        public async Task<Response<dynamic>> IdentificaProductoHijoNivel(int IdProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@" WITH JerarquiaProductos AS (
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
                                WHERE A.IdProductoPadre = {IdProducto}

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

        public async Task<Response<dynamic>> ActualizarDataFacturaElectronica(ActualizarFacturaForFacturaElectronicaEntitie actualizarFacturaForFacturaElectronicaEntitie, int IdUser)
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

                    sql = @$" UPDATE T_Ventas SET NumberFacturaDIAN = @NumberFacturaDIAN,
                              EstadoFacturaDIAN = @EstadoFacturaDIAN, FacturaJSON = @FacturaJSON,
                              UpdateDate = @UpdateDate, IdUserAction = @IdUserAction
                              WHERE IdVenta = @IdVenta ";

                    var parametros = new
                    {
                        actualizarFacturaForFacturaElectronicaEntitie.IdVenta,
                        actualizarFacturaForFacturaElectronicaEntitie.NumberFacturaDIAN,
                        actualizarFacturaForFacturaElectronicaEntitie.EstadoFacturaDIAN,
                        actualizarFacturaForFacturaElectronicaEntitie.FacturaJSON,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                    if (FilasAfectadas > 0)
                    {
                        response.Flag = true;
                        response.Message = "Venta actualiza con datos factura electronica correctamente";
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Se presento un error al intentar actualiza con datos factura electronica";
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
    }
}
