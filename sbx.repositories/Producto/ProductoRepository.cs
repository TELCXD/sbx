using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Producto;
using sbx.core.Interfaces.Producto;

namespace sbx.repositories.Producto
{
    public class ProductoRepository: IProducto
    {
        private readonly string _connectionString;

        public ProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(ProductoEntitie productoEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (productoEntitie.IdProducto > 0)
                    {
                        sql = @$" UPDATE T_Productos SET 
                                  Sku = NULLIF(@Sku,''),
                                  CodigoBarras = NULLIF(@CodigoBarras, ''),
                                  Nombre = @Nombre, 
                                  CostoBase = @CostoBase,
                                  PrecioBase = @PrecioBase, 
                                  EsInventariable = @EsInventariable,
                                  Impuesto = @Impuesto, 
                                  IdCategoria = @IdCategoria, 
                                  IdMarca = @IdMarca,
                                  IdUnidadMedida = @IdUnidadMedida,
                                  Idtribute = @Idtribute,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdProducto = @IdProducto";

                        var parametros = new
                        {
                            productoEntitie.IdProducto,
                            productoEntitie.Sku,
                            productoEntitie.CodigoBarras,
                            productoEntitie.Nombre,
                            productoEntitie.CostoBase,
                            productoEntitie.PrecioBase,
                            productoEntitie.EsInventariable,
                            productoEntitie.Impuesto,
                            productoEntitie.IdCategoria,
                            productoEntitie.IdMarca,
                            productoEntitie.IdUnidadMedida,
                            productoEntitie.Idtribute,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Producto actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar producto";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Productos (Sku,CodigoBarras,Nombre,
                                  CostoBase,PrecioBase,EsInventariable,Impuesto,IdCategoria,IdMarca,IdUnidadMedida,Idtribute,CreationDate, IdUserAction)
                                  VALUES(NULLIF(@Sku,''),NULLIF(@CodigoBarras, ''),@Nombre,
                                  @CostoBase,@PrecioBase,@EsInventariable,@Impuesto,@IdCategoria,@IdMarca,@IdUnidadMedida,@Idtribute,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            productoEntitie.Sku,
                            productoEntitie.CodigoBarras,
                            productoEntitie.Nombre,
                            productoEntitie.CostoBase,
                            productoEntitie.PrecioBase,
                            productoEntitie.EsInventariable,
                            productoEntitie.Impuesto,
                            productoEntitie.IdCategoria,
                            productoEntitie.IdMarca,
                            productoEntitie.IdUnidadMedida,
                            productoEntitie.Idtribute,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Producto creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Producto";
                        }
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

        public async Task<Response<dynamic>> List(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,ISNULL((SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

									     SELECT
	                                     dnc.IdProducto, 
                                         'Entrada por Nota credito' AS TipoMovimiento,
	                                     dnc.Cantidad
                                         FROM T_NotaCreditoDetalle dnc
                                         INNER JOIN T_NotaCredito nc ON nc.IdNotaCredito = dnc.IdNotaCredito

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_DetalleVenta dvt
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto),0) Stock  
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdProducto = {Id}";
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

        public async Task<bool> ExisteCodigoBarras(string codigoBarras, int Id_Producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Productos WHERE CodigoBarras = @CodigoBarras AND IdProducto != @Id_Producto ";

                    return connection.ExecuteScalar<int>(sql, new { CodigoBarras = codigoBarras, Id_Producto }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteNombre(string nombre, int Id_Producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Productos WHERE Nombre = @Nombre AND IdProducto != @Id_Producto ";

                    return connection.ExecuteScalar<int>(sql, new { Nombre = nombre, Id_Producto }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteSku(string sku, int Id_Producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Productos WHERE Sku = @Sku AND IdProducto != @Id_Producto ";

                    return connection.ExecuteScalar<int>(sql, new { Sku = sku, Id_Producto }) > 0;
                }
                catch (Exception)
                {
                    return false;
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

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,ISNULL((SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_Ventas vt INNER JOIN  T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta 
										 WHERE vt.Estado = 'FACTURADA'
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto),0) Stock                                 
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(A.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " ORDER BY REPLACE(A.Nombre, ' ', '') ";

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

        public async Task<Response<dynamic>> ListSku(string sku)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,(SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_DetalleVenta dvt
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto) Stock  
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";

                    if (!string.IsNullOrEmpty(sku))
                    {
                        Where = $"WHERE A.Sku = '{sku}'";
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

        public async Task<Response<dynamic>> ListCodigoBarras(string CodigoBarras)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,(SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_DetalleVenta dvt
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto) Stock  
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";

                    if (!string.IsNullOrEmpty(CodigoBarras))
                    {
                        Where = $"WHERE A.CodigoBarras = '{CodigoBarras}'";
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

        public async Task<Response<dynamic>> BuscarProductoPadre(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,ISNULL((SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_Ventas vt INNER JOIN  T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta 
										 WHERE vt.Estado = 'FACTURADA'
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto),0) Stock,
                                         E.IdProductoPadre
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida 
                                  LEFT JOIN T_ConversionesProducto E ON E.IdProductoPadre = A.IdProducto
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(A.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " AND E.IdProductoPadre IS NULL " + " ORDER BY REPLACE(A.Nombre, ' ', '') ";

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

        public async Task<Response<dynamic>> BuscarProductoHijo(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  ,A.EsInventariable
                                  ,A.Impuesto
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
                                  ,A.Idtribute
                                  ,E.Nombre NombreTributo
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  ,ISNULL((SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_Ventas vt INNER JOIN  T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta 
										 WHERE vt.Estado = 'FACTURADA'
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto),0) Stock,
                                         E.IdProductoHijo,
										 F.IdProductoPadre
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida 
                                  LEFT JOIN T_ConversionesProducto E ON E.IdProductoHijo = A.IdProducto
								  LEFT JOIN T_ConversionesProducto F ON F.IdProductoPadre = A.IdProducto 
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(A.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " AND E.IdProductoHijo IS NULL AND F.IdProductoPadre IS NULL " + " ORDER BY REPLACE(A.Nombre, ' ', '') ";

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

        public async Task<Response<dynamic>> BuscarExportarExcel(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdProducto
                                  ,A.Sku
                                  ,A.CodigoBarras
                                  ,A.Nombre
                                  ,A.CostoBase
                                  ,A.PrecioBase
                                  --,A.EsInventariable
                                  ,A.Impuesto
								  ,B.Nombre NombreCategoria
								  ,C.Nombre NombreMarca
								  ,D.Nombre NombreUnidadMedida
                                  ,E.Nombre NombreTributo
                                  ,ISNULL((SELECT 
                                    SUM(CASE WHEN 
                                    R.TipoMovimiento = 'Salida' OR R.TipoMovimiento = 'Salida por Venta' 
                                    THEN (R.Cantidad * -1) ELSE R.Cantidad END ) Stock
                                    FROM
                                        (
                                         SELECT
	                                     e.IdProducto, 
                                         'Entrada' AS TipoMovimiento,
	                                     e.Cantidad
                                         FROM T_DetalleEntradasInventario e
                                         INNER JOIN T_EntradasInventario ei ON ei.IdEntradasInventario = e.IdEntradasInventario

                                         UNION ALL

                                         SELECT
	                                     s.IdProducto, 
                                         'Salida' AS TipoMovimiento,
	                                     s.Cantidad
                                         FROM T_DetalleSalidasInventario s
                                         INNER JOIN T_SalidasInventario si ON si.IdSalidasInventario = s.IdSalidasInventario

	                                     UNION ALL

	                                     SELECT
	                                     dvt.IdProducto,
	                                     'Salida por Venta' AS TipoMovimiento,
	                                     dvt.Cantidad
	                                     FROM T_Ventas vt INNER JOIN  T_DetalleVenta dvt ON vt.IdVenta = dvt.IdVenta 
										 WHERE vt.Estado = 'FACTURADA'
                                         ) R
	                                     WHERE R.IdProducto = A.IdProducto),0) Stock                                 
                                  FROM T_Productos A
								  INNER JOIN T_Categorias B ON A.IdCategoria = B.IdCategoria
								  INNER JOIN T_Marcas C ON A.IdMarca = C.IdMarca
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida 
                                  INNER JOIN T_Tributes E ON A.Idtribute = E.Idtribute ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
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
                                    Where = $"WHERE REPLACE(A.Nombre, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto = @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku = @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras = @Filtro ";
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
                                        Where += $" REPLACE(A.Nombre, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Id":
                                    Where = $"WHERE A.IdProducto LIKE @Filtro ";
                                    break;
                                case "Sku":
                                    Where = $"WHERE A.Sku LIKE @Filtro ";
                                    break;
                                case "Codigo barras":
                                    Where = $"WHERE A.CodigoBarras LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " ORDER BY A.IdProducto DESC ";

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

        public async Task<Response<dynamic>> Eliminar(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT COUNT(1) FROM T_PreciosProducto WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_PreciosCliente WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_PromocionesProductos WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_DetalleEntradasInventario WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_DetalleSalidasInventario WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_DetalleVenta WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_NotaCreditoDetalle WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_DetalleVenta_Suspendidas WHERE IdProducto = {Id};
                                    SELECT COUNT(1) FROM T_ConversionesProducto WHERE IdProductoHijo = {Id};
                                    SELECT COUNT(1) FROM T_ConversionesProducto WHERE IdProductoPadre = {Id};
                                    SELECT COUNT(1) FROM T_DetalleCotizacion WHERE IdProducto = {Id}; ";

                    using var multi = await connection.QueryMultipleAsync(sql);

                    var PreciosProductoCount = await multi.ReadSingleAsync<int>();
                    var PreciosClienteCount = await multi.ReadSingleAsync<int>();
                    var PromocionesProductosCount = await multi.ReadSingleAsync<int>();
                    var DetalleEntradasCount = await multi.ReadSingleAsync<int>();
                    var DetalleSalidasCount = await multi.ReadSingleAsync<int>();
                    var DetalleVentaCount = await multi.ReadSingleAsync<int>();
                    var NotaCreditoDetalleCount = await multi.ReadSingleAsync<int>();
                    var DetalleVenta_SuspendidasCount = await multi.ReadSingleAsync<int>();
                    var ConversionesProductoHijoCount = await multi.ReadSingleAsync<int>();
                    var ConversionesProductoPadreCount = await multi.ReadSingleAsync<int>();
                    var DetalleCotizacionPadreCount = await multi.ReadSingleAsync<int>();

                    string Mensaje = "";

                    if (PreciosProductoCount > 0 || PreciosClienteCount > 0 || PromocionesProductosCount > 0
                        || DetalleEntradasCount > 0 || DetalleSalidasCount > 0 || DetalleVentaCount > 0 || NotaCreditoDetalleCount > 0
                        || DetalleVenta_SuspendidasCount > 0 || ConversionesProductoHijoCount > 0 ||
                        ConversionesProductoPadreCount > 0 || DetalleCotizacionPadreCount > 0) 
                    {
                        Mensaje = "No es posible eliminar el producto debido a que se encuentra en uso en los siguientes módulos: ";
                        
                        if (PreciosProductoCount > 0) 
                        {
                            Mensaje += " precios de producto,";
                        }

                        if (PreciosClienteCount > 0)
                        {
                            Mensaje += " precios de cliente,";
                        }

                        if (PromocionesProductosCount > 0)
                        {
                            Mensaje += " promociones de producto,";
                        }

                        if (DetalleEntradasCount > 0)
                        {
                            Mensaje += " Entradas de producto,";
                        }

                        if (DetalleSalidasCount > 0)
                        {
                            Mensaje += " Salidas de producto,";
                        }

                        if (DetalleVentaCount > 0)
                        {
                            Mensaje += " Ventas,";
                        }

                        if (NotaCreditoDetalleCount > 0)
                        {
                            Mensaje += " Nota credito,";
                        }

                        if (DetalleVenta_SuspendidasCount > 0)
                        {
                            Mensaje += " Venta suspendida,";
                        }

                        if (ConversionesProductoHijoCount > 0)
                        {
                            Mensaje += " Conversion de producto Hijo,";
                        }

                        if (ConversionesProductoPadreCount > 0)
                        {
                            Mensaje += " Conversion de producto padre,";
                        }

                        if (DetalleCotizacionPadreCount > 0)
                        {
                            Mensaje += " Cotizaciones";
                        }
                    }
                    else
                    {
                        sql = $"DELETE FROM T_Productos WHERE IdProducto = {Id}";

                        var rowsAffected = await connection.ExecuteAsync(sql);

                        if (rowsAffected > 0)
                        {
                            Mensaje = "Se elimino correctamente el producto";
                            response.Flag = true;
                        }
                        else
                        {
                            Mensaje = "Se presento un error al intentar eliminar el producto";
                            response.Flag = false;
                        }
                    }

                    response.Message = Mensaje;
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
