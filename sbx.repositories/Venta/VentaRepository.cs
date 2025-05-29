using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Venta;
using sbx.core.Interfaces.Venta;

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

                try
                {                   
                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    sql = @$" INSERT INTO T_Ventas (Prefijo,Consecutivo,IdCliente,IdVendedor,IdMetodoPago,CreationDate, IdUserAction)
                                  VALUES(@Prefijo,
                                        (SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM T_Ventas WHERE Prefijo = @Prefijo),
                                        @IdCliente,@IdVendedor,@IdMetodoPago,@CreationDate,@IdUserAction);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var parametros = new
                    {
                        ventaEntitie.Prefijo,
                        ventaEntitie.IdCliente,
                        ventaEntitie.IdVendedor,
                        ventaEntitie.IdMetodoPago,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idVenta = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_DetalleVenta (
                            IdVenta, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, PrecioUnitario, Descuento, Impuesto)
                            VALUES (@IdVenta, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @PrecioUnitario, @Descuento, @Impuesto)";

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
                                detalle.PrecioUnitario,
                                detalle.Descuento,
                                detalle.Impuesto
                            },
                            transaction
                        );
                    }

                    sql = @"INSERT INTO T_PagosVenta (
                            IdVenta, IdMetodoPago, Monto, Referencia, IdBanco)
                            VALUES (@IdVenta, @IdMetodoPago, @Monto, @Referencia, @IdBanco)";

                    foreach (var detallePago in ventaEntitie.pagosVenta)
                    {
                        await connection.ExecuteAsync(
                            sql,
                            new
                            {
                                IdVenta = idVenta,
                                detallePago.IdMetodoPago,
                                detallePago.Monto,
                                detallePago.Referencia,
                                detallePago.IdBanco
                            },
                            transaction
                        );
                    }

                    await transaction.CommitAsync();

                    response.Flag = true;
                    response.Message = "Venta creada correctamente";
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

                    string sql = @" ";

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

        public Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro)
        {
            throw new NotImplementedException();
        }
    }
}
