using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.Gasto
{
    public class GastoEntitie
    {
        public int IdGasto { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string Detalle { get; set; }
        public decimal ValorGasto { get; set; }
    }
}
