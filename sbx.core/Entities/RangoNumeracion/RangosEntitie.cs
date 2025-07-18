using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.RangoNumeracion
{
    public class RangosEntitie
    {
        public string url_api { get; set; } = string.Empty;
        public int Id { get; set; }
        public int document { get; set; }
        public string resolution_number { get; set; } = string.Empty;
        public string technical_key { get; set; } = string.Empty;   
        public int is_active { get; set; }
    }
}
