using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.NotaCredito;
using sbx.core.Interfaces.NotaCredito;
using sbx.core.Interfaces.Venta;
using System.Globalization;

namespace sbx
{
    public partial class AgregarNotaCredito : Form
    {
        private readonly IVenta _IVenta;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly INotaCredito _INotaCredito;
        int IdFactura = 0;
        private dynamic? _Permisos;
        decimal TotalParaDevolucion = 0;
        char decimalSeparator = ',';

        public AgregarNotaCredito(IVenta venta, IServiceProvider serviceProvider, INotaCredito notaCredito)
        {
            InitializeComponent();
            _IVenta = venta;
            _serviceProvider = serviceProvider;
            _INotaCredito = notaCredito;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void AgregarNotaCredito_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "notaCredito":
                            btn_busca_factura.Enabled = item.ToRead == 1 ? true : false;
                            btn_devolucion.Enabled = item.ToCreate == 1 ? true : false;
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

        private void btn_busca_factura_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Busca_factura";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            IdFactura = id;
            var resp = await _IVenta.List(id);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_ventas.Rows.Clear();

                    txt_busca_factura.Text = resp.Data[0].Factura;

                    if (resp.Data != null)
                    {
                        if (resp.Data.Count > 0)
                        {
                            lbl_factura.Text = resp.Data[0].Factura;
                            lbl_cliente.Text = resp.Data[0].NumeroDocumento + " - " + resp.Data[0].NombreRazonSocial;
                            lbl_vendedor.Text = resp.Data[0].NumeroDocumentoVendedor + " - " + resp.Data[0].NombreCompletoVendedor;
                            lbl_medio_pago.Text = resp.Data[0].NombreMetodoPago;
                            lbl_referencia.Text = resp.Data[0].Referencia;
                            lbl_banco.Text = resp.Data[0].NombreBanco;
                            lbl_usuario.Text = resp.Data[0].IdUserActionFactura + " - " + resp.Data[0].UserNameFactura;

                            decimal Subtotal = 0;
                            decimal cantidadTotal = 0;
                            decimal DescuentoLinea;
                            decimal Descuento = 0;
                            decimal Impuesto = 0;
                            decimal ImpuestoLinea;
                            decimal SubtotalLinea;
                            decimal Total = 0;
                            decimal TotalLinea;

                            foreach (var item in resp.Data)
                            {
                                cantidadTotal += Convert.ToDecimal(item.Cantidad);
                                Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                                SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                                Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                TotalLinea = (SubtotalLinea - DescuentoLinea) + ImpuestoLinea;

                                dtg_ventas.Rows.Add(
                                    item.IdDetalleVenta,
                                    false,
                                    item.IdProducto,
                                    item.Sku,
                                    item.CodigoBarras,
                                    item.NombreProducto,
                                    item.UnidadMedida,
                                    item.PrecioUnitario.ToString("N2", new CultureInfo("es-CO")),
                                    Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")),
                                    0,
                                    Convert.ToDecimal(item.Descuento, new CultureInfo("es-CO")),
                                    Convert.ToDecimal(item.Impuesto, new CultureInfo("es-CO")),
                                    TotalLinea.ToString("N2", new CultureInfo("es-CO")));
                            }

                            Total += (Subtotal - Descuento) + Impuesto;

                            lbl_cantidadProductos.Text = cantidadTotal.ToString(new CultureInfo("es-CO"));
                            lbl_subtotal.Text = Subtotal.ToString("N2", new CultureInfo("es-CO"));
                            lbl_descuento.Text = Descuento.ToString("N2", new CultureInfo("es-CO"));
                            lbl_impuesto.Text = Impuesto.ToString("N2", new CultureInfo("es-CO"));
                            lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                        }
                        else
                        {
                            MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private async void btn_devolucion_parcial_Click(object sender, EventArgs e)
        {
            int Contador = 0;
            bool TodoProductTieneCantDevo = true;

            foreach (DataGridViewRow fila in dtg_ventas.Rows)
            {
                if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) > 0)
                {
                    Contador++;
                }

                if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value) && Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")) <= 0)
                {
                    TodoProductTieneCantDevo = false;
                }
            }

            if (Contador > 0 && TodoProductTieneCantDevo)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de realizar devolucion?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    NotaCreditoEntitie notaCreditoEntitie = new NotaCreditoEntitie
                    {
                        IdVenta = IdFactura,
                        Motivo = txt_motivo_devolucion.Text
                    };

                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                        {
                            DetalleNotaCredito detalleNotaCredito = new DetalleNotaCredito
                            {
                                IdDetalleVenta = Convert.ToInt32(fila.Cells["cl_id_detalle_venta"].Value),
                                IdProducto = Convert.ToInt32(fila.Cells["cl_idProducto"].Value),
                                Sku = fila.Cells["cl_sku"].Value.ToString() ?? "",
                                CodigoBarras = fila.Cells["cl_codigo_barras"].Value.ToString() ?? "",
                                NombreProducto = fila.Cells["cl_nombre"].Value.ToString() ?? "",
                                Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO")),
                                PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")),
                                UnidadMedida = fila.Cells["cl_unidadMedida"].Value.ToString() ?? "",
                                Descuento = Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")),
                                Impuesto = Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO"))
                            };

                            notaCreditoEntitie.detalleNotaCredito.Add(detalleNotaCredito);
                        }
                    }

                    var resp = await _INotaCredito.Create(notaCreditoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                    if (resp != null)
                    {
                        if (resp.Flag == true)
                        {
                            MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                if (!TodoProductTieneCantDevo) 
                {
                    MessageBox.Show("Todos los productos seleccionados debe tener Cant.dev mayor a cero, favor verificar ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar 1 producto y total devolucion debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private void dtg_ventas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalCantidadDevo;
            decimal TotalLineaDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (dtg_ventas.Columns[e.ColumnIndex].Name == "cl_seleccionado")
            {
                dtg_ventas.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //bool isChecked = Convert.ToBoolean(dtg_ventas.Rows[e.RowIndex].Cells["cl_seleccionado"].Value);

                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    if (Convert.ToBoolean(fila.Cells["cl_seleccionado"].Value))
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value, new CultureInfo("es-CO")));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value, new CultureInfo("es-CO")));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }
                    else
                    {
                        fila.Cells["cl_cantidad_devolver"].Value = 0;
                    }
                }

                lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
            }
        }

        private void chk_marcar_todo_CheckedChanged(object sender, EventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (chk_marcar_todo.Checked)
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = true;
                    TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                    DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                    ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                    TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                    TotalParaDevolucion += TotalLineaDevo;
                }
            }
            else
            {
                foreach (DataGridViewRow fila in dtg_ventas.Rows)
                {
                    fila.Cells["cl_seleccionado"].Value = false;
                    fila.Cells["cl_cantidad_devolver"].Value = 0;
                }
            }

            lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
            lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
        }

        private void dtg_ventas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal SubtotalLineaDevo;
            decimal DescuentoLineaDevo;
            decimal ImpuestoLineaDevo;
            decimal TotalLineaDevo;
            decimal TotalCantidadDevo;

            TotalParaDevolucion = 0;
            TotalCantidadDevo = 0;

            if (e.ColumnIndex == 9)
            {
                var celda = dtg_ventas[e.ColumnIndex, e.RowIndex];

                if (celda.Value == null || string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    if (e.ColumnIndex == 9)
                    {
                        celda.Value = 0;
                    }

                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    dtg_ventas[1, e.RowIndex].Value = 1;
                   
                    foreach (DataGridViewRow fila in dtg_ventas.Rows)
                    {
                        TotalCantidadDevo += Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        SubtotalLineaDevo = Convert.ToDecimal(fila.Cells["cl_precio"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad_devolver"].Value, new CultureInfo("es-CO"));
                        DescuentoLineaDevo = CalcularDescuento(SubtotalLineaDevo, Convert.ToDecimal(fila.Cells["cl_descuento"].Value));
                        ImpuestoLineaDevo = CalcularIva(SubtotalLineaDevo - DescuentoLineaDevo, Convert.ToDecimal(fila.Cells["cl_iva"].Value));
                        TotalLineaDevo = (SubtotalLineaDevo - DescuentoLineaDevo) + ImpuestoLineaDevo;

                        TotalParaDevolucion += TotalLineaDevo;
                    }

                    lbl_cantidad_devolucion.Text = TotalCantidadDevo.ToString(new CultureInfo("es-CO"));
                    lbl_total_devolucion.Text = TotalParaDevolucion.ToString("N2", new CultureInfo("es-CO"));
                }
            }
        }

        private decimal CalcularTotal(decimal valorBase, decimal porcentajeIva, decimal porcentajeDescuento)
        {
            var descuento = CalcularDescuento(valorBase, porcentajeDescuento);
            var valor = valorBase - descuento;
            var iva = CalcularIva(valor, porcentajeIva);
            return valor + iva;
        }

        private void dtg_ventas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void dtg_ventas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Remover eventos anteriores para evitar duplicados
            e.Control.KeyPress -= new KeyPressEventHandler(dtg_ventas_KeyPress);

            if (dtg_ventas.CurrentCell.ColumnIndex == 9)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dtg_ventas_KeyPress);
            }
        }
    }
}
