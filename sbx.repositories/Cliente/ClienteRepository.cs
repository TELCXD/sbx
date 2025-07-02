using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Cliente;
using sbx.core.Interfaces.Cliente;

namespace sbx.repositories.Cliente
{
    public class ClienteRepository: ICliente
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(ClienteEntitie clienteEntitie, int IdUser)
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

                    if (clienteEntitie.IdCliente > 0)
                    {
                        sql = @$" UPDATE T_Cliente SET 
                                  IdIdentificationType = @IdIdentificationType,
                                  NumeroDocumento = NULLIF(@NumeroDocumento, ''),
                                  NombreRazonSocial = NULLIF(@NombreRazonSocial, ''),
                                  IdTipoCliente = @IdTipoCliente,
                                  Direccion = @Direccion,
                                  Telefono = @Telefono, 
                                  Email = @Email,
                                  Estado = @Estado, 
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdCliente = @IdCliente";

                        var parametros = new
                        {
                            IdCliente = clienteEntitie.IdCliente,
                            IdIdentificationType = clienteEntitie.IdIdentificationType,
                            NumeroDocumento = clienteEntitie.NumeroDocumento,
                            NombreRazonSocial = clienteEntitie.NombreRazonSocial,
                            IdTipoCliente = clienteEntitie.IdTipoCliente,
                            Direccion = clienteEntitie.Direccion,
                            Telefono = clienteEntitie.Telefono,
                            Email = clienteEntitie.Email,
                            Estado = clienteEntitie.Estado,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Cliente actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar cliente";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Cliente (IdIdentificationType,NumeroDocumento,NombreRazonSocial,IdTipoCliente,
                                  Direccion,Telefono,Email,Estado,CreationDate, IdUserAction)
                                  VALUES(@IdIdentificationType,NULLIF(@NumeroDocumento,''),NULLIF(@NombreRazonSocial, ''),@IdTipoCliente,@Direccion,
                                  @Telefono,@Email,@Estado,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdIdentificationType = clienteEntitie.IdIdentificationType,
                            NumeroDocumento = clienteEntitie.NumeroDocumento,
                            NombreRazonSocial = clienteEntitie.NombreRazonSocial,
                            IdTipoCliente = clienteEntitie.IdTipoCliente,
                            Direccion = clienteEntitie.Direccion,
                            Telefono = clienteEntitie.Telefono,
                            Email = clienteEntitie.Email,
                            Estado = clienteEntitie.Estado,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Cliente creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear cliente";
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
                                   A.IdCliente
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,C.IdTipoCliente
                                  ,C.Nombre
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Cliente A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdCliente = {Id}";
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

                    string sql = @"SELECT COUNT(1) FROM T_Cliente WHERE NumeroDocumento = @NumeroDocumento AND IdCliente != @Id_Cliente ";

                    return connection.ExecuteScalar<int>(sql, new { NumeroDocumento = numeroDocumento, Id_Cliente }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteNombreRazonSocial(string nombreRazonSocial, int Id_Cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Cliente WHERE NombreRazonSocial = @NombreRazonSocial AND IdCliente != @Id_Cliente ";

                    return connection.ExecuteScalar<int>(sql, new { NombreRazonSocial = nombreRazonSocial, Id_Cliente }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteTelefono(string telefono, int Id_Cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Cliente WHERE Telefono = @Telefono AND IdCliente != @Id_Cliente ";

                    return connection.ExecuteScalar<int>(sql, new { Telefono = telefono, Id_Cliente }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteEmail(string email, int Id_Cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_Cliente WHERE Email = @Email AND IdCliente != @Id_Cliente ";

                    return connection.ExecuteScalar<int>(sql, new { Email = email, Id_Cliente }) > 0;
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
                                   A.IdCliente
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,C.IdTipoCliente
                                  ,C.Nombre TipoCliente
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Cliente A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente ";

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

        public async Task<Response<dynamic>> ListNumDoc(string NumDoc)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdCliente
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,C.IdTipoCliente
                                  ,C.Nombre
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Cliente A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente 
                                  WHERE A.NumeroDocumento = @NumDoc";


                    dynamic resultado = await connection.QueryAsync(sql, new { NumDoc });

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

        public async Task<Response<dynamic>> BuscarExportar(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,C.Nombre TipoCliente
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,CASE WHEN A.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END Estado
                                  FROM T_Cliente A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente ";

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

        public async Task<Response<dynamic>> Eliminar(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT COUNT(1) FROM T_PreciosCliente WHERE IdCliente = {Id};
                                    SELECT COUNT(1) FROM T_Ventas WHERE IdCliente = {Id};
                                    SELECT COUNT(1) FROM T_Ventas_Suspendidas WHERE IdCliente = {Id};
                                    SELECT COUNT(1) FROM T_Cotizacion WHERE IdCliente = {Id}; ";

                    using var multi = await connection.QueryMultipleAsync(sql);

                    var PreciosClienteCount = await multi.ReadSingleAsync<int>();
                    var VentasCount = await multi.ReadSingleAsync<int>();
                    var Ventas_SuspendidasCount = await multi.ReadSingleAsync<int>();
                    var CotizacionCount = await multi.ReadSingleAsync<int>();

                    string Mensaje = "";

                    if (PreciosClienteCount > 0 || VentasCount > 0 || Ventas_SuspendidasCount > 0 || CotizacionCount > 0 )
                    {
                        Mensaje = "No es posible eliminar el cliente debido a que se encuentra en uso en los siguientes módulos: ";

                        if (PreciosClienteCount > 0)
                        {
                            Mensaje += " precios de cliente,";
                        }

                        if (VentasCount > 0)
                        {
                            Mensaje += " Ventas cliente,";
                        }

                        if (Ventas_SuspendidasCount > 0)
                        {
                            Mensaje += " Ventas suspendidas,";
                        }

                        if (CotizacionCount > 0)
                        {
                            Mensaje += " Cotizacion,";
                        }
                    }
                    else
                    {
                        sql = $"DELETE FROM T_Cliente WHERE IdCliente = {Id}";

                        var rowsAffected = await connection.ExecuteAsync(sql);

                        if (rowsAffected > 0)
                        {
                            Mensaje = "Se elimino correctamente el cliente";
                            response.Flag = true;
                        }
                        else
                        {
                            Mensaje = "Se presento un error al intentar eliminar el cliente";
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

        public async Task<Response<dynamic>> BuscarPrecio(string dato, string campoFiltro, string tipoFiltro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT 
                                   A.IdCliente
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.NumeroDocumento
                                  ,A.NombreRazonSocial
                                  ,C.IdTipoCliente
                                  ,C.Nombre TipoCliente
                                  ,A.Direccion
                                  ,A.Telefono
                                  ,A.Email
                                  ,A.Estado
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_Cliente A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType 
                                  INNER JOIN T_TipoCliente C ON A.IdTipoCliente = C.IdTipoCliente ";

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

                    sql += Where + " AND A.IdCliente > 1";

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
