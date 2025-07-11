using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.TipoEntrada;
using System.Globalization;

namespace sbx
{
    public partial class Entradas : Form
    {
        private dynamic? _Permisos;
        private readonly ITipoEntrada _ITipoEntrada;
        private AgregaDetalleEntrada? _AgregaDetalleEntrada;
        private readonly IServiceProvider _serviceProvider;
        private Buscador? _Buscador;
        private readonly IProveedor _IProveedor;
        EntradasInventarioEntitie entradasInventarioEntitie = new EntradasInventarioEntitie();
        private readonly IProducto _IProducto;
        private readonly IEntradaInventario _IEntradaInventario;
        private int _Id_Entrada;

        public Entradas(ITipoEntrada tipoEntrada, IServiceProvider serviceProvider, IProveedor proveedor, IProducto iProducto, IEntradaInventario entradaInventario)
        {
            InitializeComponent();
            _ITipoEntrada = tipoEntrada;
            _serviceProvider = serviceProvider;
            _IProveedor = proveedor;
            _IProducto = iProducto;
            _IEntradaInventario = entradaInventario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Entrada
        {
            get => _Id_Entrada;
            set => _Id_Entrada = value;
        }

        private async void Entradas_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "entradas":
                            btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_add_producto.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_busca_pv.Enabled = item.ToRead == 1 ? true : false;
                            btn_quitar.Enabled = item.ToUpdate == 1 ? true : false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay informacion de permisos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task CargaDatosIniciales()
        {
            var resp = await _ITipoEntrada.ListTipoEntrada();
            cbx_tipo_entrada.DataSource = resp.Data;
            cbx_tipo_entrada.ValueMember = "IdTipoEntrada";
            cbx_tipo_entrada.DisplayMember = "Nombre";
            cbx_tipo_entrada.SelectedIndex = 0;

            if (Id_Entrada > 0) 
            {
                panel3.Enabled = false;
                panel1.Enabled = false;
                panel2.Enabled = false;

                var DataEntrada = await _IEntradaInventario.List(Id_Entrada);
                if (DataEntrada.Data != null)
                {
                    if (DataEntrada.Data.Count > 0)
                    {
                        cbx_tipo_entrada.SelectedValue = DataEntrada.Data[0].IdTipoEntrada;
                        txt_orden_compra.Text = DataEntrada.Data[0].OrdenCompra.ToString();
                        txt_num_factura.Text = DataEntrada.Data[0].NumFactura.ToString();
                        txt_documento_proveedor.Text = DataEntrada.Data[0].NumeroDocumento.ToString();
                        lbl_nombre_proveedor.Text = DataEntrada.Data[0].NombreRazonSocial.ToString();
                        txt_comentario.Text = DataEntrada.Data[0].Comentario.ToString();

                        decimal Total = 0;
                        decimal Subtotal = 0;
                        decimal Descuento = 0;
                        decimal Impuesto = 0;
                        decimal SubtotalLinea = 0;
                        decimal DescuentoLinea = 0;
                        decimal ImpuestoLinea = 0;

                        foreach (var item in DataEntrada.Data)
                        {
                            Subtotal += Convert.ToDecimal(item.CostoUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                            SubtotalLinea = Convert.ToDecimal(item.CostoUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                            Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                            DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")));
                            ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Iva, new CultureInfo("es-CO")));
                            Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Iva, new CultureInfo("es-CO")));
                            decimal TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;
                            dtg_detalle_entrada.Rows.Add(
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.Nombre,
                                item.CodigoLote,
                                item.FechaVencimiento,
                                item.Cantidad.ToString(new CultureInfo("es-CO")),
                                item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                                item.Descuento.ToString(new CultureInfo("es-CO")),
                                item.Iva.ToString(new CultureInfo("es-CO")),
                                TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                        }

                        Total = (Subtotal - Descuento) + Impuesto;

                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    }
                }
            }
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaDetalleEntrada = _serviceProvider.GetRequiredService<AgregaDetalleEntrada>();
                _AgregaDetalleEntrada.Permisos = _Permisos;
                _AgregaDetalleEntrada.Enviar_Detalle += _ObtenerDetalleEntrada;
                _AgregaDetalleEntrada.FormClosed += (s, args) => _AgregaDetalleEntrada = null;
                _AgregaDetalleEntrada.ShowDialog();
            }
        }

        private void btn_busca_pv_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Entradas_busca_proveedor";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProveedor.List(id);
            if (resp.Data != null)
            {
                entradasInventarioEntitie.IdProveedor = resp.Data[0].IdProveedor;
                txt_documento_proveedor.Text = resp.Data[0].NumeroDocumento;
                lbl_nombre_proveedor.Text = resp.Data[0].NombreRazonSocial;
            }
        }

        private void _ObtenerDetalleEntrada(DetalleEntradasInventarioEntitie detalleEntradasInv)
        {
            var nuevoDetalle = new DetalleEntradasInventarioEntitie
            {
                IdProducto = detalleEntradasInv.IdProducto,
                Sku = detalleEntradasInv.Sku,
                CodigoBarras = detalleEntradasInv.CodigoBarras,
                Nombre = detalleEntradasInv.Nombre,
                CodigoLote = detalleEntradasInv.CodigoLote,
                FechaVencimiento = detalleEntradasInv.FechaVencimiento,
                Cantidad = detalleEntradasInv.Cantidad,
                CostoUnitario = detalleEntradasInv.CostoUnitario,
                Descuento = detalleEntradasInv.Descuento,
                Iva = detalleEntradasInv.Iva,
                Total = detalleEntradasInv.Total
            };

            bool Existe = false;

            foreach (DataGridViewRow fila in dtg_detalle_entrada.Rows)
            {
                if (Convert.ToInt32(fila.Cells["cl_id_producto"].Value) == nuevoDetalle.IdProducto) 
                {
                    Existe = true;
                }
            }

            if (!Existe) 
            {
                entradasInventarioEntitie.detalleEntradasInventarios.Add(nuevoDetalle);
                dtg_detalle_entrada.Rows.Clear();
                foreach (var item in entradasInventarioEntitie.detalleEntradasInventarios)
                {
                    dtg_detalle_entrada.Rows.Add(
                        item.IdProducto,
                        item.Sku,
                        item.CodigoBarras,
                        item.Nombre,
                        item.CodigoLote,
                        item.FechaVencimiento,
                        item.Cantidad.ToString().Replace('.', ','),
                        item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                        item.Descuento.ToString().Replace('.', ','),
                        item.Iva.ToString().Replace('.', ','),
                        item.Total.ToString("N2", new CultureInfo("es-CO"))
                        );
                }

                if (entradasInventarioEntitie.detalleEntradasInventarios.Count > 0)
                {
                    decimal Total = entradasInventarioEntitie.detalleEntradasInventarios.Sum(d => d.Total);
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    lbl_total.Text = "_";
                }
            }
            else
            {
                MessageBox.Show("Este producto ya está en la lista, no se puede agregar nuevamente.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_entrada.Rows.Count > 0)
            {
                entradasInventarioEntitie = new EntradasInventarioEntitie();

                entradasInventarioEntitie.IdTipoEntrada = Convert.ToInt32(cbx_tipo_entrada.SelectedValue);
                entradasInventarioEntitie.OrdenCompra = txt_orden_compra.Text;
                entradasInventarioEntitie.NumFactura = txt_num_factura.Text;
                entradasInventarioEntitie.Comentario = txt_comentario.Text;
                if (txt_documento_proveedor.Text != "") 
                {
                    var respProvee = await _IProveedor.ListNumeroDocumento(txt_documento_proveedor.Text);
                    if (respProvee.Data != null)
                    {
                        entradasInventarioEntitie.IdProveedor = respProvee.Data[0].IdProveedor;
                    }
                }
                else
                {
                    entradasInventarioEntitie.IdProveedor = 1;
                }

                foreach (DataGridViewRow fila in dtg_detalle_entrada.Rows)
                {
                    var nuevoDetalle = new DetalleEntradasInventarioEntitie
                    {
                        IdProducto = Convert.ToInt32(fila.Cells["cl_id_producto"].Value),
                        CodigoLote = fila.Cells["cl_lote"].Value.ToString(),
                        FechaVencimiento = Convert.ToDateTime(fila.Cells["cl_fecha_vencimiento"].Value),
                        Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO")),
                        CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO")),
                        Descuento = Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")),
                        Iva = Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO"))
                    };

                    entradasInventarioEntitie.detalleEntradasInventarios.Add(nuevoDetalle);
                }

                var resp = await _IEntradaInventario.CreateUpdate(entradasInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para guardar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_quitar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_entrada.Rows.Count > 0)
            {
                if (dtg_detalle_entrada.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de quitar producto de la lista?",
                             "Confirmar cancelacion",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var row = dtg_detalle_entrada.SelectedRows[0];
                        if (row.Cells["cl_id_producto"].Value != null)
                        {
                            int Id_Producto = Convert.ToInt32(row.Cells["cl_id_producto"].Value);
                            dtg_detalle_entrada.Rows.RemoveAt(row.Index);

                            var item = entradasInventarioEntitie.detalleEntradasInventarios
                            .FirstOrDefault(d => d.IdProducto == Id_Producto);

                            if (item != null)
                            {
                                entradasInventarioEntitie.detalleEntradasInventarios.Remove(item);
                            }

                            decimal Total = 0;
                            decimal Subtotal = 0;
                            decimal Descuento = 0;
                            decimal Impuesto = 0;
                            decimal SubtotalLinea = 0;
                            decimal DescuentoLinea = 0;

                            foreach (DataGridViewRow fila in dtg_detalle_entrada.Rows)
                            {
                                Subtotal += Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                SubtotalLinea = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                                DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                                Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                            }
                            Total = (Subtotal - Descuento) + Impuesto;

                            lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay productos para quitar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay productos para quitar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            return ValorDescuento;
        }
    }
}
