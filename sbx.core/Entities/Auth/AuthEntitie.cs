using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.Auth
{
    public class AuthEntitie
    {
        public string url_api { get; set; } = string.Empty;
        public string grant_type { get; set; } = string.Empty;
        public string client_id { get; set; } = string.Empty;
        public string client_secret { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string Passwords { get; set; } = string.Empty;
    }
}
