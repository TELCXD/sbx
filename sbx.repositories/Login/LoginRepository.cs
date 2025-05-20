using Microsoft.Data.SqlClient;
using Dapper;
using sbx.core.Interfaces;
using Isopoh.Cryptography.Argon2;
using sbx.core.Entities;

namespace sbx.repositories.LoginRepository
{
    public class LoginRepository: ILogin
    {
        private readonly string _connectionString;
        
        public LoginRepository(string connectionString) 
        {
            _connectionString = connectionString;
        }

        #region validar credenciales
        public async Task<Response<dynamic>> ValidarUsuario(string nombreUsuario, string contrasena)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "SELECT IdUser, Password, Active, IdRole FROM T_User WHERE UserName = @NombreUsuario";
                    var parametros = new { NombreUsuario = nombreUsuario };

                    dynamic resultado = await connection.QueryAsync(sql, parametros);

                    int IdUser = 0;
                    bool Activo = false;
                    int IdRole = 0;

                    if (resultado != null)
                    {
                        if (resultado.Count > 0) 
                        {
                            IdUser = resultado[0].IdUser;
                            if (IdUser > 0)
                            {
                                Activo = resultado[0].Active;
                                if (Activo)
                                {
                                    string password = resultado[0].Password;
                                    bool passwordValidate = true;
                                    passwordValidate = Argon2.Verify(password, contrasena);
                                    if (passwordValidate)
                                    {
                                        IdRole = resultado[0].IdRole;
                                        sql = @$"SELECT B.MenuURL, A.ToRead, A.ToCreate, A.ToUpdate, A.ToDelete  
                                            FROM TR_Role_Menu A 
                                                INNER JOIN T_Menu B
                                                    ON A.IdMenu= B.IdMenu
	                                            INNER JOIN T_Role C 
		                                            ON A.IdRole = C.IdRole
                                            WHERE A.IdRole= {IdRole} AND B.Active = 1 AND C.Active = 1
                                            AND (A.ToUpdate=1 OR A.ToCreate=1 OR A.ToRead=1 OR A.ToDelete=1)";

                                        var permisos = await connection.QueryAsync<Permisos>(sql);

                                        foreach (var item in permisos)
                                        {
                                            item.IdUser = IdUser;
                                            item.UserName = nombreUsuario;
                                        }

                                        response.Flag = true;
                                        response.Message = "Credenciales correctas";
                                        response.Data = permisos;
                                    }
                                    else
                                    {
                                        response.Flag = false;
                                        response.Message = "Contraseña incorrecta";
                                    }
                                }
                                else
                                {
                                    response.Flag = false;
                                    response.Message = "Usuario inactivo";
                                }
                            }
                            else
                            {
                                response.Flag = false;
                                response.Message = "Usuario no existe";
                            }
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Usuario no existe";
                        }
                    }
                    else
                    {
                        response.Flag = false;
                        response.Message = "Sin respuesta de datos";
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Flag = false;
                    response.Message = "Error: "+ex.Message;
                    return response;
                }
            }
        }
        #endregion
    }
}
