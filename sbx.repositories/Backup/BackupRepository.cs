using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.Backup;

namespace sbx.repositories.Backup
{
    public class BackupRepository : IBackup
    {
        private readonly string _connectionString;

        public BackupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> Create(string Ruta_backup,string NombreCopiaSeguridad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string Comando = "BACKUP DATABASE [sbx_development] TO  DISK = N'" + Ruta_backup + "" + NombreCopiaSeguridad + "' WITH NOFORMAT, NOINIT,  NAME = N'sbx_development-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                    await connection.ExecuteAsync(Comando);

                    response.Flag = true;
                    response.Message = "Copia de seguridad generada correctamente";

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
