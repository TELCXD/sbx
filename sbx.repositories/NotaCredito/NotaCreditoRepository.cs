using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.NotaCredito;
using sbx.core.Interfaces.NotaCredito;

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

                try
                {
                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    sql = @$" INSERT INTO T_NotaCredito (IdVenta,Motivo,CreationDate, IdUserAction)
                                  VALUES(@IdVenta, @Motivo, @CreationDate, @IdUserAction);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    var parametros = new
                    {
                        notaCredito.IdVenta,
                        notaCredito.Motivo,
                        CreationDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    int idNotaCredito = await connection.ExecuteScalarAsync<int>(sql, parametros, transaction);

                    sql = @"INSERT INTO T_NotaCreditoDetalle (
                            IdNotaCredito, IdDetalleVenta, IdProducto, Sku, CodigoBarras, NombreProducto, Cantidad, UnidadMedida, PrecioUnitario, Descuento, Impuesto,CreationDate, IdUserAction)
                            VALUES (@IdNotaCredito, @IdDetalleVenta, @IdProducto, @Sku, @CodigoBarras,
                                    @NombreProducto, @Cantidad, @UnidadMedida, @PrecioUnitario, @Descuento, @Impuesto, @CreationDate, @IdUserAction)";

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

                    response.Flag = true;
                    response.Data = idNotaCredito;
                    response.Message = "Nota credito creada correctamente";
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

                    string sql = @"SELECT 
                                    A.Motivo,
                                    A.IdVenta,
									A.IdUserAction AS IdUserActionNotaCredito,
									CONCAT(A.IdUserAction, '-', C.UserName) AS Usuario,
                                    B.IdNotaCreditoDetalle,
                                    B.IdNotaCredito,
                                    B.IdDetalleVenta,	
                                    B.IdProducto,
                                    B.Sku,
                                    B.CodigoBarras,
                                    B.NombreProducto,
                                    B.Cantidad,
                                    B.UnidadMedida,
                                    B.PrecioUnitario,
                                    B.Descuento,
                                    B.Impuesto,
                                    B.CreationDate,
                                    B.IdUserAction AS IdUserActionNotaCreditoDetalle
                                    FROM T_NotaCredito A 
                                    INNER JOIN T_NotaCreditoDetalle B ON B.IdNotaCredito = A.IdNotaCredito
									INNER JOIN T_User C ON C.IdUser = A.IdUserAction ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE B.IdNotaCredito = {Id}";
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
