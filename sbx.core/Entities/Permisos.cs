using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities
{
    public class Permisos
    {
        public string? MenuUrl { get; set; }
        public int ToRead { get; set; }
        public int ToCreate { get; set; }
        public int ToUpdate { get; set; }
        public int ToDelete { get; set; }
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public int IdRole { get; set; }
        public string NameRole { get; set; }
    }
}
