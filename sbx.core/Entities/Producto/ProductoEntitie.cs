using System.ComponentModel.DataAnnotations;

namespace sbx.core.Entities.Producto
{
    public class ProductoEntitie
    {
        public int IdProducto { get; set; }
        public string Sku { get; set; }
        public string CodigoBarras { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio")]
        public decimal CostoBase { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal PrecioBase { get; set; }
        public int EsInventariable { get; set; }
        [Required(ErrorMessage = "El Impuesto es obligatorio")]
        public decimal Impuesto { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdUnidadMedida { get; set; }
        public int Idtribute { get; set; }
        public string TipoProducto { get; set; }
    }
}
