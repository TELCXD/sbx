using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.CrendencialesApi
{
    public class CredencialesApiEntitie
    {
        public int IdCredencialesApi { get; set; }
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string Passwords { get; set; }
    }
}
