using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Gasto;
using sbx.core.Interfaces.Gastos;

namespace sbx.repositories.Gastos
{
    public class GastosRepository: IGastos
    {
        private readonly string _connectionString;

        public GastosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(GastoEntitie GastoEntitie, int IdUser)
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

                    if (GastoEntitie.IdGasto > 0)
                    {
                        sql = @$" UPDATE T_Gastos SET 
                                  Categoria = @Categoria, 
                                  Subcategoria = @Subcategoria,
                                  Detalle = @Detalle, 
                                  ValorGasto = @ValorGasto,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdGasto = @IdGasto";

                        var parametros = new
                        {
                            IdGasto = GastoEntitie.IdGasto,
                            Categoria = GastoEntitie.Categoria,
                            Subcategoria = GastoEntitie.Subcategoria,
                            Detalle = GastoEntitie.Detalle,
                            ValorGasto = GastoEntitie.ValorGasto,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Gasto actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar Gasto";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Gastos (Categoria,Subcategoria,Detalle,ValorGasto,CreationDate, IdUserAction)
                                  VALUES(@Categoria,@Subcategoria,@Detalle,@ValorGasto,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            Categoria = GastoEntitie.Categoria,
                            Subcategoria = GastoEntitie.Subcategoria,
                            Detalle = GastoEntitie.Detalle,
                            ValorGasto = GastoEntitie.ValorGasto,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Gasto creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Gasto";
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

        public async Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdGasto
                                  ,A.Categoria
                                  ,A.Subcategoria
                                  ,A.Detalle
                                  ,A.ValorGasto      
                                  ,A.CreationDate   
                                  ,A.UpdateDate   
                                  ,A.IdUserAction
                                  ,B.UserName
                                  ,CONCAT(A.IdUserAction,'-',B.UserName) Usuario 
                                  FROM T_Gastos A 
                                  INNER JOIN T_User B ON A.IdUserAction = B.IdUser ";

                    string Where = "";
                    string Filtro = "";

                    string[] DatoSeparado = dato.Split('+');

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Categoria":
                                    Where = $"WHERE REPLACE(A.Categoria, ' ', '') LIKE REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Subcategoria":
                                    Where = $"WHERE A.Subcategoria LIKE @Filtro ";
                                    break;
                                case "Detalle":
                                    Where = $"WHERE A.Detalle LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato + "%";
                            break;
                        case "Igual a":
                            switch (campoFiltro)
                            {
                                case "Categoria":
                                    Where = $"WHERE REPLACE(A.Categoria, ' ', '') = REPLACE(@Filtro, ' ', '') ";
                                    break;
                                case "Subcategoria":
                                    Where = $"WHERE A.Subcategoria = @Filtro ";
                                    break;
                                case "Detalle":
                                    Where = $"WHERE A.Detalle = @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = dato;
                            break;
                        case "Contiene":
                            switch (campoFiltro)
                            {
                                case "Categoria":
                                    Where = $"WHERE  ";
                                    int contador = 0;
                                    foreach (string parte in DatoSeparado)
                                    {
                                        if (contador == 0) 
                                        {
                                            Where += $" REPLACE(A.Categoria, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                        }
                                        else if (contador == 1)
                                        {
                                            Where += $" REPLACE(A.Subcategoria, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                        }
                                        else if (contador == 2)
                                        {
                                            Where += $" REPLACE(A.Detalle, ' ', '') LIKE REPLACE('%{parte}%', ' ', '') AND ";
                                        }

                                        contador++;
                                    }

                                    string conversion = Where.Substring(0, Where.Length - 4);
                                    Where = conversion;
                                    break;
                                case "Subcategoria":
                                    Where = $"WHERE A.Subcategoria LIKE @Filtro ";
                                    break;
                                case "Detalle":
                                    Where = $"WHERE A.Detalle LIKE @Filtro ";
                                    break;
                                default:
                                    break;
                            }

                            Filtro = "%" + dato + "%";
                            break;
                        default:
                            break;
                    }

                    sql += Where + " ORDER BY REPLACE(A.Categoria, ' ', '') ";

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

                    string sql = $"DELETE FROM T_Gastos WHERE IdGasto = {Id}";

                    string Mensaje = "";

                    var rowsAffected = await connection.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        Mensaje = "Se elimino correctamente el gasto";
                        response.Flag = true;
                    }
                    else
                    {
                        Mensaje = "Se presento un error al intentar eliminar el gasto";
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

        public async Task<Response<dynamic>> List(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdGasto
                                  ,A.Categoria
                                  ,A.Subcategoria
                                  ,A.Detalle
                                  ,A.ValorGasto      
                                  ,A.CreationDate   
                                  ,A.UpdateDate   
                                  ,A.IdUserAction
                                  ,B.UserName
                                  ,CONCAT(A.IdUserAction, '-',B.UserName) Usuario 
                                  FROM T_Gastos A 
                                  INNER JOIN T_User B ON A.IdUserAction = B.IdUser ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdGasto = {Id}";
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

        public async Task<Response<dynamic>> BuscarReporte(DateTime FechaInicio, DateTime FechaFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                DateTime FechaIni = Convert.ToDateTime(FechaInicio.ToString("yyyy-MM-dd"));
                DateTime FechaFn = Convert.ToDateTime(FechaFin.ToString("yyyy-MM-dd"));

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                    Categoria,
                                    Subcategoria,
                                    Detalle,
                                    SUM(ValorGasto) ValorGasto,
                                    CreationDate,
                                    UpdateDate,
                                    IdUserAction
                                    FROM T_Gastos
                                    WHERE 
                                    (CreationDate BETWEEN CONVERT(DATETIME,@FechaIni+' 00:00:00',120) 
									AND CONVERT(DATETIME,@FechaFn+' 23:59:59',120)) 
									GROUP BY Categoria, Subcategoria, Detalle, CreationDate, UpdateDate,IdUserAction  
									ORDER BY Categoria ";

                    dynamic resultado = await connection.QueryAsync(sql, new { FechaIni, FechaFn });

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
