
namespace sbx.core.Entities.Permiso
{
    public class PermisosEntitie
    {
        public int IdUserMenu { get; set; }
        public int IdMenu { get; set; }
        public int IdUser { get; set; }
        public int ToRead { get; set; }
        public int ToCreate { get; set; }
        public int ToUpdate { get; set; }
        public int ToDelete { get; set; }
        public int Active { get; set; }
        public int IdUserAction { get; set; }
    }
}
