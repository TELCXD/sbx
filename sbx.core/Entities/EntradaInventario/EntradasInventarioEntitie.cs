using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.EntradaInventario
{
    public class EntradasInventarioEntitie
    {
        public int IdEntradasInventario { get; set; }
        public int IdTipoEntrada { get; set; }
        public int IdProveedor { get; set; }
        public string? OrdenCompra { get; set; }
        public string? NumFactura { get; set; }
        public string? Comentario { get; set; }
        public List<DetalleEntradasInventarioEntitie> detalleEntradasInventarios { get; set; } = new List<DetalleEntradasInventarioEntitie>();
    }

    public class DetalleEntradasInventarioEntitie
    {
        public int IdDetalleEntradasInventario { get; set; }
        public int IdProducto { get; set; }
        public string? Sku { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoLote { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string TipoProducto { get; set; }
    }
}
