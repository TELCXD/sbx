using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.PrecioProducto
{
    public class PrecioProductoEntitie
    {
        public int IdListaPrecio { get; set; }
        public int IdProducto { get; set;}
        public decimal Precio { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int IdUserAction { get; set; }
    }
}
