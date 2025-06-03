using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Caja;
using sbx.core.Interfaces.Caja;

namespace sbx.repositories.Caja
{
    public class CajaRepository : ICaja
    {
        private readonly string _connectionString;

        public CajaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(CajaEntitie cajaEntitie)
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

                    if (cajaEntitie.IdApertura_Cierre_caja > 0)
                    {
                        sql = @$" UPDATE T_AperturaCierreCaja SET 
                                  FechaHoraCierre = @FechaActual,
                                  MontoFinalDeclarado = @MontoFinalDeclarado,
                                  VentasTotales = @VentasTotales, 
                                  Diferencia = @Diferencia,
                                  Estado = @Estado
                                  WHERE IdApertura_Cierre_caja = @IdApertura_Cierre_caja";

                        var parametros = new
                        {
                            cajaEntitie.IdApertura_Cierre_caja,
                            FechaActual,
                            cajaEntitie.MontoFinalDeclarado,
                            cajaEntitie.VentasTotales,
                            cajaEntitie.Diferencia,
                            cajaEntitie.Estado
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Caja cerrada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar cerrar caja";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_AperturaCierreCaja (IdUserAction,FechaHoraApertura,MontoInicialDeclarado,Estado)
                                  VALUES(@IdUserAction, @FechaActual, @MontoInicialDeclarado, @Estado) ";

                        var parametros = new
                        {
                            cajaEntitie.IdUserAction,
                            FechaActual,
                            cajaEntitie.MontoInicialDeclarado,
                            cajaEntitie.Estado
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Caja abierta correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar abrir Caja";
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

                    string sql = @"SELECT 
                                   IdApertura_Cierre_caja,
                                   IdUserAction,
                                   FechaHoraApertura,
                                   MontoInicialDeclarado,
                                   FechaHoraCierre,
                                   MontoFinalDeclarado,
                                   VentasTotales,
                                   Diferencia,
                                   Estado
                                   FROM T_AperturaCierreCaja ";

                    string Where = "";

                    if (IdUser > 0)
                    {
                        Where = $"WHERE IdUserAction = {IdUser}";
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro, DateTime Finicio, DateTime Ffin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();


                try
                {
                    await connection.OpenAsync();

                    DateTime FechaIni = Convert.ToDateTime(Finicio.ToString("yyyy-MM-dd"));
                    DateTime FechaFn = Convert.ToDateTime(Ffin.ToString("yyyy-MM-dd"));

                    string sql = @"SELECT 
                                   A.IdApertura_Cierre_caja,
                                   A.IdUserAction,
                                   B.UserName,
                                   CONCAT(A.IdUserAction,'-',B.UserName) Usuario,
                                   A.FechaHoraApertura,
                                   A.MontoInicialDeclarado,
                                   A.FechaHoraCierre,
                                   A.MontoFinalDeclarado,
                                   A.VentasTotales,
                                   A.Diferencia,
                                   A.Estado
                                   FROM T_AperturaCierreCaja A 
                                   INNER JOIN T_User B ON A.IdUserAction = B.IdUser 
                                   WHERE (A.FechaHoraApertura BETWEEN CONVERT(DATETIME,@FechaIni+'00:00:00',120) AND CONVERT(DATETIME,@FechaFn+'23:59:59',120)) ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre usuario":
                                    Where = $"AND B.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"AND A.IdUserAction LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato + "%";
                            break;
                        case "Igual a":
                            switch (campoFiltro)
                            {
                                case "Nombre usuario":
                                    Where = $"AND B.UserName = @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"AND A.IdUserAction = @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato;
                            break;
                        case "Contiene":
                            switch (campoFiltro)
                            {
                                case "Nombre usuario":
                                    Where = $"AND B.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"AND A.IdUserAction LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " ORDER BY A.FechaHoraApertura DESC ";

                    dynamic resultado = await connection.QueryAsync(sql, new { Filtro, FechaIni, FechaFn });

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

        public async Task<Response<dynamic>> EstadoCaja(int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT
                                    IdApertura_Cierre_caja,
                                    IdUserAction,
                                    FechaHoraApertura,
                                    MontoInicialDeclarado,
                                    FechaHoraCierre,
                                    MontoFinalDeclarado,
                                    VentasTotales,
                                    Diferencia,
                                    Estado
                                    FROM T_AperturaCierreCaja
                                    WHERE IdUserAction = {IdUser} 
                                    AND IdApertura_Cierre_caja = (SELECT MAX(IdApertura_Cierre_caja) FROM T_AperturaCierreCaja WHERE IdUserAction = {IdUser}) ";

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
