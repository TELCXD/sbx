using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.ListaPrecios;
using sbx.core.Interfaces.ListaPrecios;

namespace sbx.repositories.ListaPrecios
{
    public class ListaPreciosRepository: IListaPrecios
    {
        private readonly string _connectionString;

        public ListaPreciosRepository(string connectionString)
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

                    string sql = @"SELECT 
                                   A.IdListaPrecio
                                  ,A.NombreLista
                                  ,A.IdTipoCliente
                                  ,B.Nombre NombreTipoCliente
                                  ,A.FechaInicio
                                  ,A.FechaFin
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  FROM T_ListasPrecios A
                                  INNER JOIN T_TipoCliente B ON A.IdTipoCliente = B.IdTipoCliente ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.NombreLista LIKE @Filtro ";
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
                                    Where = $"WHERE A.NombreLista = @Filtro ";
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
                                    Where = $"WHERE A.NombreLista LIKE @Filtro ";
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

        public async Task<Response<dynamic>> CreateUpdate(ListaPreciosEntitie listaPreciosEntitie, int IdUser)
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

                    var parametros = new
                    {
                        listaPreciosEntitie.IdListaPrecio,
                        listaPreciosEntitie.NombreLista,
                        listaPreciosEntitie.IdTipoCliente,
                        listaPreciosEntitie.FechaInicio,
                        listaPreciosEntitie.FechaFin,
                        CreationDate = FechaActual,
                        UpdateDate = FechaActual,
                        IdUserAction = IdUser
                    };

                    if (listaPreciosEntitie.IdListaPrecio > 0)
                    {
                        sql = @$" UPDATE T_ListasPrecios SET 
                                  NombreLista = @NombreLista,
                                  IdTipoCliente = @IdTipoCliente,
                                  FechaInicio = @FechaInicio,
                                  FechaFin = @FechaFin,
                                  UpdateDate = @UpdateDate, 
                                  IdUserAction = @IdUserAction
                                  WHERE IdListaPrecio = @IdListaPrecio";

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Lista precios actualizada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar lista de precios";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_ListasPrecios (NombreLista,IdTipoCliente,FechaInicio,FechaFin,
                                  CreationDate, IdUserAction)
                                  VALUES(@NombreLista,@IdTipoCliente,@FechaInicio,@FechaFin,@CreationDate,@IdUserAction); 
                                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int idLista = await connection.ExecuteScalarAsync<int>(sql, parametros);

                        if (idLista > 0)
                        {
                            response.Flag = true;
                            response.Message = "Lista precios creada correctamente";
                            response.Data = idLista;
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear lista de precios";
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
                                   A.IdListaPrecio
                                  ,A.NombreLista
                                  ,A.IdTipoCliente
                                  ,B.Nombre NombreTipoCliente
                                  ,A.FechaInicio
                                  ,A.FechaFin
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction
                                  FROM T_ListasPrecios A
                                  INNER JOIN T_TipoCliente B ON A.IdTipoCliente = B.IdTipoCliente ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdListaPrecio = {Id}";
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

        public async Task<Response<dynamic>> Eliminar(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT COUNT(1) FROM T_PreciosProducto WHERE IdListaPrecio = {Id};
                                    SELECT COUNT(1) FROM T_Ventas_Suspendidas WHERE IdListaPrecio = {Id};
                                    SELECT COUNT(1) FROM T_Cotizacion WHERE IdListaPrecio = {Id}";

                    using var multi = await connection.QueryMultipleAsync(sql);

                    var PreciosProductoCount = await multi.ReadSingleAsync<int>();
                    var VentasSuspendidasCount = await multi.ReadSingleAsync<int>();
                    var CotizacionCount = await multi.ReadSingleAsync<int>();

                    string Mensaje = "";

                    if (PreciosProductoCount > 0 || VentasSuspendidasCount > 0 || CotizacionCount > 0)
                    {
                        Mensaje = "No es posible eliminar la lista de precios debido a que se encuentra en uso en los siguientes módulos: ";

                        if (PreciosProductoCount > 0)
                        {
                            Mensaje += " precios de producto,";
                        }

                        if (VentasSuspendidasCount > 0)
                        {
                            Mensaje += " ventas suspendidas,";
                        }

                        if (CotizacionCount > 0)
                        {
                            Mensaje += " Cotizaciones,";
                        }
                    }
                    else
                    {
                        sql = $"DELETE FROM T_ListasPrecios WHERE IdListaPrecio = {Id}";

                        var rowsAffected = await connection.ExecuteAsync(sql);

                        if (rowsAffected > 0)
                        {
                            Mensaje = "Se elimino correctamente la lista de precios";
                            response.Flag = true;
                        }
                        else
                        {
                            Mensaje = "Se presento un error al intentar eliminar la lista de precios";
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
