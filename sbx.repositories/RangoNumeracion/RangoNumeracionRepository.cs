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
                                  NumeroResolucion = @NumeroResolucion,
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
                            NumeroResolucion = rangoNumeracionEntitie.NumeroResolucion,
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
                                int Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {parametros.Id_RangoNumeracion} AND Id_TipoDocumentoRangoNumeracion = {rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion} ");
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
                                  Prefijo,NumeroDesde,NumeroHasta,NumeroResolucion,ClaveTecnica,FechaExpedicion,
                                  FechaVencimiento,Vencido,Active,EnUso,CreationDate,IdUserAction)
                                  VALUES(@Id_RangoDIAN,@Id_TipoDocumentoRangoNumeracion,@Prefijo,@NumeroDesde,
                                  @NumeroHasta,@NumeroResolucion,@ClaveTecnica,@FechaExpedicion,
                                  @FechaVencimiento,@Vencido,@Active,@EnUso,
                                  @CreationDate,@IdUserAction); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        var parametros = new
                        {
                            rangoNumeracionEntitie.Id_RangoDIAN,
                            Id_TipoDocumentoRangoNumeracion = rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion,
                            Prefijo = rangoNumeracionEntitie.Prefijo,
                            NumeroDesde = rangoNumeracionEntitie.NumeroDesde,
                            NumeroHasta = rangoNumeracionEntitie.NumeroHasta,
                            NumeroResolucion = rangoNumeracionEntitie.NumeroResolucion,
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
                                int Filas = await connection.ExecuteAsync($"UPDATE T_RangoNumeracion SET EnUso = 0 WHERE Id_RangoNumeracion != {idGenerado} AND Id_TipoDocumentoRangoNumeracion = {rangoNumeracionEntitie.Id_TipoDocumentoRangoNumeracion} ");
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
                                  ,NumeroResolucion
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
                        Where = $"WHERE Id_RangoNumeracion = {Id} ";
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
                                  ,NumeroResolucion
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
                        Where = $"WHERE Id_RangoNumeracion != {Id} AND EnUso = 1 AND Id_TipoDocumentoRangoNumeracion = {Id_TipoDocumentoRangoNumeracion} ";                       
                        sql += Where;
                    }
                    else
                    {
                        Where = $"WHERE EnUso = 1 AND Id_TipoDocumentoRangoNumeracion = {Id_TipoDocumentoRangoNumeracion} ";                      
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

        public async Task<Response<dynamic>> IdentificaDocumento(int IdTipoDocumento)
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
                                  ,(SELECT ISNULL(MAX(v.Consecutivo), 0) FROM T_Ventas v WHERE v.Prefijo = Prefijo) ConsecutivoActual
                                  ,NumeroResolucion
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

                    if (IdTipoDocumento > 0)
                    {
                        Where = $"WHERE EnUso = 1 AND Id_TipoDocumentoRangoNumeracion = {IdTipoDocumento}";                     
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

        public async Task<Response<dynamic>> ValidaRango(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT 
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
                                  FROM T_RangoNumeracion WHERE EnUso = 1 AND Id_RangoNumeracion = {Id} ";

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

                    string sql = $"DELETE FROM T_RangoNumeracion WHERE Id_RangoNumeracion = {Id}";

                    var rowsAffected = await connection.ExecuteAsync(sql);

                    string Mensaje = "";

                    if (rowsAffected > 0)
                    {
                        Mensaje = "Se elimino correctamente";
                        response.Flag = true;
                    }
                    else
                    {
                        Mensaje = "Se presento un error al intentar eliminar";
                        response.Flag = false;
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
