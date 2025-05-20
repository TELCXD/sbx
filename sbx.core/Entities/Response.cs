using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities
{
    public class Response<T>
    {
        public bool Flag { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
