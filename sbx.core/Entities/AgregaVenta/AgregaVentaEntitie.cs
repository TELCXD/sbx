namespace sbx.core.Entities.AgregaVenta
{
    public class AgregaVentaEntitie
    {
        public int IdListaPrecio { get; set; }
        public int IdVendedor { get; set; }
        public string? IdenticacionVendedor { get; set; }
        public string? NombreVendedor { get; set; }
        public int IdCliente { get; set; }
        public string? IdenticacionCliente { get; set; }
        public string? NombreCliente { get; set; }
        public int IdMetodoPago { get; set; }
        public string? NombreMetodoPago { get; set; }
        public int IdBanco { get; set; }
        public string? NombreBanco { get; set; }
    }
}
