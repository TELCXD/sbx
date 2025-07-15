using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.RangoNumeracion;
using sbx.core.Interfaces.RangoNumeracion;

namespace sbx.repositories.RangoNumeracion
{
    public class RangoNumeracionRepository : IRangoNumeracion
    {
        private readonly string _connectionString;

        public RangoNumeracionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(RangoNumeracionEntitie rangoNumeracionEntitie, int IdUser)
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

                    if (rangoNumeracionEntitie.Id_RangoNumeracion > 0)
                    {
                        sql = @$" UPDATE T_RangoNumeracion SET 
                                  IdRangoDIAN = @Id_RangoDIAN,
                                  Id_TipoDocumentoRangoNumeracion = @Id_TipoDocumentoRangoNumeracion,
                                  Prefijo = @Prefijo,
                                  NumeroDesde = @NumeroDesde, NumeroHasta = @NumeroHasta,
                                  NumeroAutorizacion = @NumeroAutorizacion,
                                  ClaveTecnica = @ClaveTecnica,
                                  FechaExpedicion = @FechaExpedicion,
                                  FechaVencimiento = @FechaVencimiento,
                                  Vencido = @Vencido,
                                  Active = @Active, 
                                  EnUso = @EnUso, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction
                                  WHERE Id_RangoNumeracion = @Id_RangoNumeracion";

                        var parametros = new
                        {
                            rangoNumeracionEntitie.Id_RangoDIAN,
                            Id_RangoNumeracion = rangoNumeracionEntitie.Id_RangoNumeracion,
                            Id_TipoDocumentoRangoNumeracion = rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion,
                            Prefijo = rangoNumeracionEntitie.Prefijo,
                            NumeroDesde = rangoNumeracionEntitie.NumeroDesde,
                            NumeroHasta = rangoNumeracionEntitie.NumeroHasta,
                            NumeroAutorizacion = rangoNumeracionEntitie.NumeroAutorizacion,
                            rangoNumeracionEntitie.ClaveTecnica,
                            rangoNumeracionEntitie.FechaExpedicion,
                            FechaVencimiento = rangoNumeracionEntitie.FechaVencimiento,
                            rangoNumeracionEntitie.Vencido,
                            Active = rangoNumeracionEntitie.Active,
                            EnUso = rangoNumeracionEntitie.EnUso,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            if (parametros.EnUso == 1)
                            {
                                int Filas = 0;
                                if (rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion == 1 || rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion == 2)
                                {
                                    Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {parametros.Id_RangoNumeracion} AND (Id_TipoDocumentoRangoNumeracion = 1 OR Id_TipoDocumentoRangoNumeracion = 2) ");
                                }
                                else
                                {
                                    Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {parametros.Id_RangoNumeracion} AND Id_TipoDocumentoRangoNumeracion = {rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion} ");
                                }
                            }

                            response.Flag = true;
                            response.Message = "Rango numeracion actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar rango numeracion";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_RangoNumeracion (IdRangoDIAN,Id_TipoDocumentoRangoNumeracion,
                                  Prefijo,NumeroDesde,NumeroHasta,NumeroAutorizacion,ClaveTecnica,FechaExpedicion,
                                  FechaVencimiento,Vencido,Active,EnUso,CreationDate,IdUserAction)
                                  VALUES(@Id_RangoDIAN,@Id_TipoDocumentoRangoNumeracion,@Prefijo,@NumeroDesde,
                                  @NumeroHasta,@NumeroAutorizacion,@ClaveTecnica,@FechaExpedicion,
                                  @FechaVencimiento,@Vencido,@Active,@EnUso,
                                  @CreationDate,@IdUserAction); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        var parametros = new
                        {
                            rangoNumeracionEntitie.Id_RangoDIAN,
                            Id_TipoDocumentoRangoNumeracion = rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion,
                            Prefijo = rangoNumeracionEntitie.Prefijo,
                            NumeroDesde = rangoNumeracionEntitie.NumeroDesde,
                            NumeroHasta = rangoNumeracionEntitie.NumeroHasta,
                            NumeroAutorizacion = rangoNumeracionEntitie.NumeroAutorizacion,
                            rangoNumeracionEntitie.ClaveTecnica,
                            rangoNumeracionEntitie.FechaExpedicion,
                            FechaVencimiento = rangoNumeracionEntitie.FechaVencimiento,
                            rangoNumeracionEntitie.Vencido,
                            Active = rangoNumeracionEntitie.Active,
                            EnUso = rangoNumeracionEntitie.EnUso,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        //int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);
                        int idGenerado = await connection.QuerySingleAsync<int>(sql, parametros);

                        if (idGenerado > 0)
                        {
                            if (parametros.EnUso == 1) 
                            {
                                int Filas = 0;
                                if (rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion == 1 || rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion == 2)
                                {
                                    Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {idGenerado} AND (Id_TipoDocumentoRangoNumeracion = 1 OR Id_TipoDocumentoRangoNumeracion = 2) ");
                                }
                                else
                                {
                                    Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {idGenerado} AND Id_TipoDocumentoRangoNumeracion = {rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion} ");
                                }
                            }

                            response.Flag = true;
                            response.Message = "Rango numeracion creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Rango numeracion";
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
                                   Id_RangoNumeracion
                                  ,IdRangoDIAN
                                  ,Id_TipoDocumentoRangoNumeracion
                                  ,Prefijo
                                  ,ISNULL(NumeroDesde,0) NumeroDesde
                                  ,ISNULL(NumeroHasta,0) NumeroHasta
                                  ,NumeroAutorizacion
                                  ,ClaveTecnica
                                  ,FechaExpedicion
                                  ,FechaVencimiento
                                  ,Vencido
                                  ,Active
                                  ,ISNULL(EnUso, 0) EnUso
                                  ,CreationDate
                                  ,UpdateDate
                                  ,IdUserAction
                                  FROM T_RangoNumeracion ";

                    string Where = "";

                    if (Id > 0) 
                    {
                        Where = $"WHERE Id_RangoNumeracion = {Id}";
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

        public async Task<Response<dynamic>> ListEnUso(int Id, int Id_TipoDocumentoRangoNumeracion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   Id_RangoNumeracion
                                  ,IdRangoDIAN
                                  ,Id_TipoDocumentoRangoNumeracion
                                  ,Prefijo
                                  ,ISNULL(NumeroDesde,0) NumeroDesde
                                  ,ISNULL(NumeroHasta,0) NumeroHasta
                                  ,NumeroAutorizacion
                                  ,ClaveTecnica
                                  ,FechaExpedicion
                                  ,FechaVencimiento
                                  ,Vencido
                                  ,Active
                                  ,ISNULL(EnUso, 0) EnUso
                                  ,CreationDate
                                  ,UpdateDate
                                  ,IdUserAction
                                  FROM T_RangoNumeracion ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE Id_RangoNumeracion != {Id} AND EnUso = 1 ";
                        if (Id_TipoDocumentoRangoNumeracion == 1 || Id_TipoDocumentoRangoNumeracion == 2) 
                        {
                            Where += $"AND (Id_TipoDocumentoRangoNumeracion = 1 OR Id_TipoDocumentoRangoNumeracion = 2) ";
                        }
                        else
                        {
                            Where += $"AND Id_TipoDocumentoRangoNumeracion = {Id_TipoDocumentoRangoNumeracion} ";
                        }

                        sql += Where;
                    }
                    else
                    {
                        Where = $"WHERE EnUso = 1 ";
                        if (Id_TipoDocumentoRangoNumeracion == 1 || Id_TipoDocumentoRangoNumeracion == 2)
                        {
                            Where += $"AND (Id_TipoDocumentoRangoNumeracion = 1 OR Id_TipoDocumentoRangoNumeracion = 2) ";
                        }
                        else
                        {
                            Where += $"AND Id_TipoDocumentoRangoNumeracion = {Id_TipoDocumentoRangoNumeracion} ";
                        }

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

        public async Task<bool> ExisteIdRangoDIAN(string IdRango, int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_RangoNumeracion WHERE IdRangoDIAN = @IdRango AND Id_RangoNumeracion != @Id ";

                    return connection.ExecuteScalar<int>(sql, new { IdRango, Id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExistePrefijo(string prefijo, int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_RangoNumeracion WHERE Prefijo = @prefijo AND Id_RangoNumeracion != @Id ";

                    return connection.ExecuteScalar<int>(sql, new { prefijo, Id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteResolucion(string Resolucion, int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_RangoNumeracion WHERE NumeroAutorizacion = @Resolucion AND Id_RangoNumeracion != @Id ";

                    return connection.ExecuteScalar<int>(sql, new { Resolucion, Id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteClaveTecnica(string ClaveTecnica, int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_RangoNumeracion WHERE ClaveTecnica = @ClaveTecnica AND Id_RangoNumeracion != @Id ";

                    return connection.ExecuteScalar<int>(sql, new { ClaveTecnica, Id }) > 0;
                }
                catch (Exception)
                {
                    return false;
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
