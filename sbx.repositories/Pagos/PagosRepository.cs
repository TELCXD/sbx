using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Pagos;
using sbx.core.Interfaces.Pago;

namespace sbx.repositories.Pagos
{
    public class PagosRepository : IPagosEfectivo
    {
        private readonly string _connectionString;

        public PagosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(PagosEfectivoEntitie pagosEfectivoEntitie, int IdUser)
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

                    if (pagosEfectivoEntitie.IdPago > 0)
                    {
                        sql = @$" UPDATE T_Pagos SET                                   
                                  ValorPago = @ValorPago, 
                                  Descripcion = @Descripcion, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdPago = @IdPago";

                        var parametros = new
                        {
                            pagosEfectivoEntitie.IdPago,
                            ValorPago = pagosEfectivoEntitie.ValorPago,
                            Descripcion = pagosEfectivoEntitie.DescripcionPago,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Pago actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar pago";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Pagos (ValorPago,Descripcion,CreationDate, IdUserAction) VALUES(@ValorPago,@Descripcion,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            ValorPago = pagosEfectivoEntitie.ValorPago,
                            Descripcion = pagosEfectivoEntitie.DescripcionPago,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Pago creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear pago";
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

        public async Task<Response<dynamic>> List(int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $"SELECT IdPago, ValorPago, Descripcion, CreationDate, UpdateDate, IdUserAction FROM T_Pagos WHERE IdUserAction = {IdUser} ";

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
