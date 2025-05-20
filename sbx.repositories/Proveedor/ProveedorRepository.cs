using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Proveedor;
using sbx.core.Interfaces.Proveedor;

namespace sbx.repositories.Proveedor
{
    public class ProveedorRepository: IProveedor
    {
        private readonly string _connectionString;

        public ProveedorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(ProveedorEntitie proveedorEntitie, int IdUser)
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

                    if (proveedorEntitie.IdProveedor > 0)
                    {
                        sql = @$" UPDATE T_Proveedores SET 
                                  IdIdentificationType = @IdIdentificationType,
                                  NumeroDocumento = NULLIF(@NumeroDocumento, ''),
                                  NombreRazonSocial = NULLIF(@NombreRazonSocial, ''), 
                                  Direccion = @Direccion,
                                  Telefono = @Telefono, 
                                  Email = @Email,
                                  Estado = @Estado, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdProveedor = @IdProveedor";

                        var parametros = new
                        {
                            IdProveedor = proveedorEntitie.IdProveedor,
                            IdIdentificationType = proveedorEntitie.IdIdentificationType,
                            NumeroDocumento = proveedorEntitie.NumeroDocumento,
                            NombreRazonSocial = proveedorEntitie.NombreRazonSocial,
                            Direccion = proveedorEntitie.Direccion,
                            Telefono = proveedorEntitie.Telefono,
                            Email = proveedorEntitie.Email,
                            Estado = proveedorEntitie.Estado,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Proveedor actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar proveedor";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Proveedores (IdIdentificationType,NumeroDocumento,NombreRazonSocial,
                                  Direccion,Telefono,Email,Estado,CreationDate, IdUserAction)
                                  VALUES(@IdIdentificationType,NULLIF(@NumeroDocumento,''),NULLIF(@NombreRazonSocial, ''),@Direccion,
                                  @Telefono,@Email,@Estado,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdIdentificationType = proveedorEntitie.IdIdentificationType,
                            NumeroDocumento = proveedorEntitie.NumeroDocumento,
                            NombreRazonSocial = proveedorEntitie.NombreRazonSocial,
                            Direccion = proveedorEntitie.Direccion,
                            Telefono = proveedorEntitie.Telefono,
                            Email = proveedorEntitie.Email,
                            Estado = proveedorEntitie.Estado,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Proveedor creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Proveedor";
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
                                   A.IdProveedor
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Proveedores A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdProveedor = {Id}";
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

        public async Task<bool> ExisteNumeroDocumento(string numeroDocumento, int Id_Proveedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Proveedores WHERE NumeroDocumento = @NumeroDocumento AND IdProveedor != @Id_Proveedor ";

                    return connection.ExecuteScalar<int>(sql, new { NumeroDocumento = numeroDocumento, Id_Proveedor }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteNombreRazonSocial(string nombreRazonSocial, int Id_Proveedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Proveedores WHERE NombreRazonSocial = @NombreRazonSocial AND IdProveedor != @Id_Proveedor ";

                    return connection.ExecuteScalar<int>(sql, new { NombreRazonSocial = nombreRazonSocial, Id_Proveedor }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteTelefono(string telefono, int Id_Proveedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Proveedores WHERE Telefono = @Telefono AND IdProveedor != @Id_Proveedor ";

                    return connection.ExecuteScalar<int>(sql, new { Telefono = telefono, Id_Proveedor }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteEmail(string email, int Id_Proveedor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Proveedores WHERE Email = @Email AND IdProveedor != @Id_Proveedor ";

                    return connection.ExecuteScalar<int>(sql, new { Email = email, Id_Proveedor }) > 0;
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
                                   A.IdProveedor
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Proveedores A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre":
                                    Where = $"WHERE A.NombreRazonSocial LIKE @Filtro ";
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
                                    Where = $"WHERE A.NombreRazonSocial = @Filtro ";
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
                                    Where = $"WHERE A.NombreRazonSocial LIKE @Filtro ";
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
