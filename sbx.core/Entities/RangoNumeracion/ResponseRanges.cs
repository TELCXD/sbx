using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.RangoNumeracion
{
    public class ResponseRanges
    {
        public List<ResponseRangesItems> data { get; set; } = new List<ResponseRangesItems>();
    }

    public class ResponseRangesItems
    {
        public int Id { get; set; }
        public string document { get; set; } = string.Empty;
        public string prefix { get; set; } = string.Empty;
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public string current { get; set; } = string.Empty;
        public string resolution_number { get; set; } = string.Empty;
        public string start_date { get; set; } = string.Empty;
        public string end_date { get; set; } = string.Empty;
        public string technical_key { get; set; } = string.Empty;
        public bool is_expired { get; set; }
        public int is_active { get; set; }
        public string deleted_at { get; set; } = string.Empty;
        public string created_at { get; set; } = string.Empty;
        public string updated_at { get; set; } = string.Empty;
    }
}
