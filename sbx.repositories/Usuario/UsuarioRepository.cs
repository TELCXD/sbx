using Dapper;
using Isopoh.Cryptography.Argon2;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.usuario;
using sbx.core.Interfaces.Usuario;

namespace sbx.repositories.Usuario
{
    public class UsuarioRepository : IUsuario
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

                    string PasswordEncriptado = "";
                    if (UsuarioEntitie.Password != "") 
                    {
                        PasswordEncriptado = Argon2.Hash(UsuarioEntitie.Password);
                    }

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
                                  Active = @Active,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdUser = @IdUser ";                        

                        var parametros = new
                        {
                            IdUser = UsuarioEntitie.IdUser,
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
                            Password = PasswordEncriptado != "" ? PasswordEncriptado: UsuarioEntitie.PasswordHash,
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
                                  BirthDate,IdCountry,IdDepartament,IdCity,TelephoneNumber, Email, IdRole,UserName, Password,Active,CreationDate,IdUserAction)
                                  VALUES(@IdIdentificationType,@IdentificationNumber,@Name,@LastName,
                                  @BirthDate,@IdCountry,@IdDepartament,@IdCity,@TelephoneNumber,@Email,@IdRole,@UserName,@Password,@Active,@CreationDate,@IdUserAction)";

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
                            Password = PasswordEncriptado,
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
                                   A.IdUser
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.IdentificationNumber
                                  ,A.Name
                                  ,A.LastName
                                  ,A.BirthDate
                                  ,A.IdCountry
                                  ,A.IdDepartament
                                  ,A.IdCity
                                  ,A.TelephoneNumber
                                  ,A.Email
                                  ,A.IdRole
                                  ,A.UserName
                                  ,A.Password
                                  ,A.Active
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_User A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType ";

                    string Where = "";

                    if (Id > 0)
                    {
                        Where = $"WHERE A.IdUser = {Id}";
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
                                   A.IdUser
                                  ,A.IdIdentificationType
                                  ,B.IdentificationType
                                  ,A.IdentificationNumber
                                  ,A.Name
                                  ,A.LastName
                                  ,CONCAT(A.Name, ' ', A.LastName) NombreCompleto
                                  ,A.BirthDate
                                  ,A.IdCountry
                                  ,A.IdDepartament
                                  ,A.IdCity
                                  ,A.TelephoneNumber
                                  ,A.Email
                                  ,A.IdRole
                                  ,C.NameRole
                                  ,A.UserName
                                  ,A.Password
                                  ,A.Active
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM T_User A
								  INNER JOIN T_IdentificationType B ON A.IdIdentificationType = B.IdIdentificationType
                                  INNER JOIN T_Role C ON C.IdRole = A.IdRole ";

                    string Where = "";
                    string Filtro = "";

                    switch (tipoFiltro)
                    {
                        case "Inicia con":
                            switch (campoFiltro)
                            {
                                case "Nombre usuario":
                                    Where = $"WHERE A.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"WHERE A.IdUser LIKE @Filtro ";
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
                                    Where = $"WHERE A.UserName = @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"WHERE A.IdUser = @Filtro ";
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
                                    Where = $"WHERE A.UserName LIKE @Filtro ";
                                    break;
                                case "Id usuario":
                                    Where = $"WHERE A.IdUser LIKE @Filtro ";
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

        public async Task<bool> ExisteNumeroDocumento(string IdentificationNumber, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_User WHERE IdentificationNumber = @IdentificationNumber AND IdUser != @IdUser ";

                    return connection.ExecuteScalar<int>(sql, new { IdentificationNumber, IdUser }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteTelefono(string TelephoneNumber, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_User WHERE TelephoneNumber = @TelephoneNumber AND IdUser != @IdUser ";

                    return connection.ExecuteScalar<int>(sql, new { TelephoneNumber, IdUser }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExisteEmail(string Email, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT COUNT(1) FROM T_User WHERE Email = @Email AND IdUser != @IdUser ";

                    return connection.ExecuteScalar<int>(sql, new { Email, IdUser }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
