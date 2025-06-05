using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Permiso;
using sbx.core.Interfaces.Permisos;

namespace sbx.repositories.Permiso
{
    public class PermisoRepository : IPermisos
    {
        private readonly string _connectionString;

        public PermisoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(List<PermisosEntitie> permisosEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";
                    int Correcto = 0;
                    int Error = 0;

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    foreach (var item in permisosEntitie)
                    {
                        if (item.IdUserMenu > 0)
                        {
                            sql = @$" UPDATE TR_User_Menu SET 
                                  IdMenu = @IdMenu,
                                  IdUser = @IdUser,
                                  ToRead = @ToRead,
                                  ToCreate = @ToCreate,
                                  ToUpdate = @ToUpdate,
                                  ToDelete = @ToDelete, 
                                  Active = @Active,
                                  UpdateDate = @UpdateDate,
                                  IdUserAction = @IdUserAction 
                                  WHERE IdUserMenu = @IdUserMenu";

                            var parametros = new
                            {
                                IdUserMenu = item.IdUserMenu,
                                IdMenu = item.IdMenu,
                                IdUser = item.IdUser,
                                ToRead = item.ToRead,
                                ToCreate = item.ToCreate,
                                ToUpdate = item.ToUpdate,
                                ToDelete = item.ToDelete,
                                Active = item.Active,
                                UpdateDate = FechaActual,
                                IdUserAction = IdUser
                            };

                            int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                            if (FilasAfectadas > 0)
                            {
                                Correcto++;
                            }
                            else
                            {
                                Error++;
                            }
                        }
                        else
                        {
                            sql = @$" INSERT INTO T_Cliente (IdMenu,IdUser,ToRead,ToCreate,ToUpdate,ToDelete,Active)
                                  VALUES(@IdMenu,@IdUser,@ToRead,@ToCreate,@ToUpdate,@ToDelete,@Active) ";

                            var parametros = new
                            {
                                IdUserMenu = item.IdUserMenu,
                                IdMenu = item.IdMenu,
                                IdUser = item.IdUser,
                                ToRead = item.ToRead,
                                ToCreate = item.ToCreate,
                                ToUpdate = item.ToUpdate,
                                ToDelete = item.ToDelete,
                                Active = item.Active,
                                CreationDate = FechaActual,
                                IdUserAction = IdUser
                            };

                            int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                            if (FilasAfectadas > 0)
                            {
                                Correcto++;
                            }
                            else
                            {
                                Error++;
                            }
                        }
                    }

                    response.Flag = true;
                    response.Message = $"Proceso finalizado, Correctos: {Correcto} - Errores: {Error}";
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
                                   A.IdUserMenu
                                  ,A.IdMenu
                                  ,B.MenuName
                                  ,A.IdUser
                                  ,C.UserName
                                  ,A.ToRead
                                  ,A.ToCreate
                                  ,A.ToUpdate
                                  ,A.ToDelete
                                  ,A.Active
                                  ,A.CreationDate
                                  ,A.UpdateDate
                                  ,A.IdUserAction 
                                  FROM TR_User_Menu A
                                  INNER JOIN T_Menu B ON B.IdMenu = A.IdMenu
                                  INNER JOIN T_User C ON C.IdUser = A.IdUser ";

                    string Where = "";

                    if (IdUser > 0)
                    {
                        Where = $"WHERE A.IdUser = {IdUser}";
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
    }
}
