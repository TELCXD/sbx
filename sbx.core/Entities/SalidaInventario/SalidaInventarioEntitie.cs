
namespace sbx.core.Entities.SalidaInventario
{
    public class SalidaInventarioEntitie
    {
        public int IdSalidasInventario { get; set; }
        public int IdTipoSalida { get; set; }
        public int IdProveedor { get; set; }
        public string? OrdenCompra { get; set; }
        public string? NumFactura { get; set; }
        public string? Comentario { get; set; }
        public List<DetalleSalidaInventarioEntitie> detalleSalidaInventarios { get; set; } = new List<DetalleSalidaInventarioEntitie>();
    }

    public class DetalleSalidaInventarioEntitie
    {
        public int IdDetalleSalidasInventario { get; set; }
        public int IdSalidasInventario { get; set; }
        public int IdProducto { get; set; }
        public string? Sku { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoLote { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Total { get; set; }
        public string TipoProducto { get; set; }
    }
}
