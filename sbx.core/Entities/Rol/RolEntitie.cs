
namespace sbx.core.Entities.Rol
{
    public class RolEntitie
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
        public int IdProfile { get; set; }
        public int Active { get; set; }
        public int IdUserAction { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
