using sbx.core.Entities;

namespace sbx.core.Interfaces.Backup
{
    public interface IBackup
    {
        Task<Response<dynamic>> Create(string Ruta_backup, string NombreCopiaSeguridad);
    }
}
