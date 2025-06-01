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
                                  Iva = @Iva, 
                                  IdCategoria = @IdCategoria, 
                                  IdMarca = @IdMarca,
                                  IdUnidadMedida = @IdUnidadMedida,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdProducto = @IdProducto";

                        var parametros = new
                        {
                            IdProducto = productoEntitie.IdProducto,
                            Sku = productoEntitie.Sku,
                            CodigoBarras = productoEntitie.CodigoBarras,
                            Nombre = productoEntitie.Nombre,
                            CostoBase = productoEntitie.CostoBase,
                            PrecioBase = productoEntitie.PrecioBase,
                            EsInventariable = productoEntitie.EsInventariable,
                            Iva = productoEntitie.Iva,
                            IdCategoria = productoEntitie.IdCategoria,
                            IdMarca = productoEntitie.IdMarca,
                            IdUnidadMedida = productoEntitie.IdUnidadMedida,
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
                                  CostoBase,PrecioBase,EsInventariable,Iva,IdCategoria,IdMarca,IdUnidadMedida,CreationDate, IdUserAction)
                                  VALUES(NULLIF(@Sku,''),NULLIF(@CodigoBarras, ''),@Nombre,
                                  @CostoBase,@PrecioBase,@EsInventariable,@Iva,@IdCategoria,@IdMarca,@IdUnidadMedida,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            Sku = productoEntitie.Sku,
                            CodigoBarras = productoEntitie.CodigoBarras,
                            Nombre = productoEntitie.Nombre,
                            CostoBase = productoEntitie.CostoBase,
                            PrecioBase = productoEntitie.PrecioBase,
                            EsInventariable = productoEntitie.EsInventariable,
                            Iva = productoEntitie.Iva,
                            IdCategoria = productoEntitie.IdCategoria,
                            IdMarca = productoEntitie.IdMarca,
                            IdUnidadMedida = productoEntitie.IdUnidadMedida,
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
                                  ,A.Iva
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
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
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida ";

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
                                  ,A.Iva
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
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
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.Nombre LIKE @Filtro ";
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
                                    Where = $"WHERE A.Nombre = @Filtro ";
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
                                    Where = $"WHERE A.Nombre LIKE @Filtro ";
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

                    sql += Where;

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
                                  ,A.Iva
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
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
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida ";

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
                                  ,A.Iva
                                  ,A.IdCategoria
								  ,B.Nombre NombreCategoria
                                  ,A.IdMarca
								  ,C.Nombre NombreMarca
                                  ,A.UpdateDate
                                  ,A.IdUnidadMedida
								  ,D.Nombre NombreUnidadMedida
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
								  INNER JOIN T_UnidadMedida D ON A.IdUnidadMedida = D.IdUnidadMedida ";

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
    }
}
