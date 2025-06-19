using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Vendedor;
using sbx.core.Interfaces.Vendedor;

namespace sbx.repositories.Vendedor
{
    public class VendedorRepository: IVendedor
    {
        private readonly string _connectionString;

        public VendedorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(VendedorEntitie vendedorEntitie, int IdUser)
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

                    if (vendedorEntitie.IdVendedor > 0)
                    {
                        sql = @$" UPDATE T_Vendedor SET 
                                  IdIdentificationType = @IdIdentificationType,
                                  NumeroDocumento = NULLIF(@NumeroDocumento, ''),
                                  Nombre = @Nombre,
                                  Apellido = @Apellido,
                                  Direccion = @Direccion,
                                  Telefono = @Telefono, 
                                  Email = @Email,
                                  Estado = @Estado, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdVendedor = @IdVendedor";

                        var parametros = new
                        {
                            IdVendedor = vendedorEntitie.IdVendedor,
                            IdIdentificationType = vendedorEntitie.IdIdentificationType,
                            NumeroDocumento = vendedorEntitie.NumeroDocumento,
                            Nombre = vendedorEntitie.Nombre,
                            Apellido = vendedorEntitie.Apellido,
                            Direccion = vendedorEntitie.Direccion,
                            Telefono = vendedorEntitie.Telefono,
                            Email = vendedorEntitie.Email,
                            Estado = vendedorEntitie.Estado,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Vendedor actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar Vendedor";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Vendedor (IdIdentificationType,NumeroDocumento,Nombre,Apellido,
                                  Direccion,Telefono,Email,Estado,CreationDate, IdUserAction)
                                  VALUES(@IdIdentificationType,NULLIF(@NumeroDocumento,''),@Nombre,@Apellido,@Direccion,
                                  @Telefono,@Email,@Estado,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdIdentificationType = vendedorEntitie.IdIdentificationType,
                            NumeroDocumento = vendedorEntitie.NumeroDocumento,
                            Nombre = vendedorEntitie.Nombre,
                            Apellido = vendedorEntitie.Apellido,
                            Direccion = vendedorEntitie.Direccion,
                            Telefono = vendedorEntitie.Telefono,
                            Email = vendedorEntitie.Email,
                            Estado = vendedorEntitie.Estado,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Vendedor creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Vendedor";
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
                                   A.IdVendedor
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.Nombre
                                  ,A.Apellido
                                  ,CONCAT(A.Nombre, ' ', A.Apellido) AS NombreCompleto
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Vendedor A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdVendedor = {Id}";
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

        public async Task<Response<dynamic>> ListActivos(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdVendedor
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.Nombre
                                  ,A.Apellido
                                  ,CONCAT(A.Nombre, ' ', A.Apellido) AS NombreCompleto
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Vendedor A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  WHERE A.Estado = 1 ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $" AND A.IdVendedor = {Id}";
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

        public async Task<bool> ExisteNumeroDocumento(string numeroDocumento, int Id_Cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Vendedor WHERE NumeroDocumento = @NumeroDocumento AND IdVendedor != @IdVendedor ";

                    return connection.ExecuteScalar<int>(sql, new { NumeroDocumento = numeroDocumento, Id_Cliente }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteTelefono(string telefono, int IdVendedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Vendedor WHERE Telefono = @Telefono AND IdVendedor != @IdVendedor ";

                    return connection.ExecuteScalar<int>(sql, new { Telefono = telefono, IdVendedor }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteEmail(string email, int IdVendedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Vendedor WHERE Email = @Email AND IdVendedor != @IdVendedor ";

                    return connection.ExecuteScalar<int>(sql, new { Email = email, IdVendedor }) > 0;
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
                                   A.IdVendedor
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.Nombre
                                  ,A.Apellido
                                  ,CONCAT(A.Nombre,' ', A.Apellido) NombreCompletoVendedor
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Vendedor A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE CONCAT(A.Nombre,' ',A.Apellido) LIKE @Filtro ";
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE A.NumeroDocumento LIKE @Filtro ";
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
                                    Where = $"WHERE CONCAT(A.Nombre,' ',A.Apellido) = @Filtro ";
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE A.NumeroDocumento = @Filtro ";
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
                                    Where = $"WHERE CONCAT(A.Nombre,' ',A.Apellido) LIKE @Filtro ";
                                    break;
                                case "Num Doc":
                                    Where = $"WHERE A.NumeroDocumento LIKE @Filtro ";
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
    }
}
