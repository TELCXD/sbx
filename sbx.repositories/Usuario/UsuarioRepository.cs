using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.usuario;
using System;

namespace sbx.repositories.Usuario
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(usuarioEntitie UsuarioEntitie, int IdUser)
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

                    if (UsuarioEntitie.IdUser > 0)
                    {
                        sql = @$" UPDATE T_User SET 
                                  IdIdentificationType = @IdIdentificationType,
                                  IdentificationNumber = NULLIF(@IdentificationNumber, ''),
                                  Name = @Name,
                                  LastName = @LastName,
                                  BirthDate = @BirthDate,
                                  IdCountry = @IdCountry,
                                  IdDepartament = @IdDepartament, 
                                  IdCity = @IdCity,
                                  TelephoneNumber = @TelephoneNumber, 
                                  Email = @Email,
                                  IdRole = @IdRole,
                                  UserName = @UserName,
                                  Password = @Password,
                                  DatePassword = @DatePassword,
                                  Active = @Active,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdCliente = @IdCliente ";

                        var parametros = new
                        {
                            IdIdentificationType = UsuarioEntitie.IdIdentificationType,
                            IdentificationNumber = UsuarioEntitie.IdentificationNumber,
                            Name = UsuarioEntitie.Name,
                            LastName = UsuarioEntitie.LastName,
                            BirthDate = UsuarioEntitie.BirthDate,
                            IdCountry = UsuarioEntitie.IdCountry,
                            IdDepartament = UsuarioEntitie.IdDepartament,
                            IdCity = UsuarioEntitie.IdCity,
                            TelephoneNumber = UsuarioEntitie.TelephoneNumber,
                            Email = UsuarioEntitie.Email,
                            IdRole = UsuarioEntitie.IdRole,
                            UserName = UsuarioEntitie.UserName,
                            Password = UsuarioEntitie.Password,
                            DatePassword = UsuarioEntitie.DatePassword,
                            Active = UsuarioEntitie.Active,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Usuario actualizado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar usuario";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_User (IdIdentificationType,IdentificationNumber,Name,LastName,
                                  BirthDate,IdCountry,IdDepartament,IdCity,TelephoneNumber, Email, IdRole,UserName, Password,DatePassword,Active,CreationDate,IdUserAction)
                                  VALUES(@IdIdentificationType,@IdentificationNumber,@Name,@LastName,
                                  @BirthDate,@IdCountry,@IdDepartament,@IdCity,@TelephoneNumber,@Email,@IdRole,@UserName,@Password,@DatePassword,@Active,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdIdentificationType = UsuarioEntitie.IdIdentificationType,
                            IdentificationNumber = UsuarioEntitie.IdentificationNumber,
                            Name = UsuarioEntitie.Name,
                            LastName = UsuarioEntitie.LastName,
                            BirthDate = UsuarioEntitie.BirthDate,
                            IdCountry = UsuarioEntitie.IdCountry,
                            IdDepartament = UsuarioEntitie.IdDepartament,
                            IdCity = UsuarioEntitie.IdCity,
                            TelephoneNumber = UsuarioEntitie.TelephoneNumber,
                            Email = UsuarioEntitie.Email,
                            IdRole = UsuarioEntitie.IdRole,
                            UserName = UsuarioEntitie.UserName,
                            Password = UsuarioEntitie.Password,
                            DatePassword = UsuarioEntitie.DatePassword,
                            Active = UsuarioEntitie.Active,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Usuario creado correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear usuario";
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
    }
}
