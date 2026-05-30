using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.AgregaVenta
{
    public class AgregaVariosMediosPago
    {
        public int IdMetodoPago { get; set; }
        public decimal valor { get; set; }
        public string Referencia { get; set; }
        public int IdBanco { get; set; }
    }
}
