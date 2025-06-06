using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.Pagos
{
    public class PagosEfectivoEntitie
    {
        public int IdPago { get; set; }
        public decimal ValorPago { get; set; }
        public string DescripcionPago { get; set; }
        public int IdUserAction { get; set; }
    }
}
