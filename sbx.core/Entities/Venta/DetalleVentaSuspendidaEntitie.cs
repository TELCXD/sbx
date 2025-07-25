﻿
namespace sbx.core.Entities.Venta
{
    public class DetalleVentaSuspendidaEntitie
    {
        public int IdDetalleVenta_Suspendidas { get; set; }
        public int IdVenta_Suspendidas { get; set; }
        public int IdProducto { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal CostoUnitario { get; set; }
    }
}
