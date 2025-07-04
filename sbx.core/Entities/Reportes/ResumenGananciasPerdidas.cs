using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.Reportes
{
    public class ResumenGananciasPerdidas
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal VentaNetaFinal { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal GananciaBruta { get; set; }
    }
}
