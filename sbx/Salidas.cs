using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Interfaces.FechaVencimiento;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.SalidaInventario;
using sbx.core.Interfaces.TipoSalida;
using System.Globalization;

namespace sbx
{
    public partial class Salidas : Form
    {
        private dynamic? _Permisos;
        private readonly ITipoSalida _ITipoSalida;
        private readonly IServiceProvider _serviceProvider;
        private Buscador? _Buscador;
        private readonly IProveedor _IProveedor;
        private readonly IProducto _IProducto;
        SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();
        private AgregaDetalleSalida? _AgregaDetalleSalida;
        private readonly ISalidaInventario _ISalidaInventario;
        private int _Id_Salida;
        DateTime FechaVSeleccionada = new DateTime(1900, 1, 1);
        private readonly IFechaVencimiento _IFechaVencimiento;
        private ConfirmaFechaVecimiento? _ConfirmaFechaVecimiento;

        public Salidas(ITipoSalida tipoSalida, IServiceProvider serviceProvider, IProveedor proveedor, IProducto producto, ISalidaInventario salidaInventario, IFechaVencimiento iFechaVencimiento)
        {
            InitializeComponent();
            _ITipoSalida = tipoSalida;
            _serviceProvider = serviceProvider;
            _IProducto = producto;
            _IProveedor = proveedor;
            _ISalidaInventario = salidaInventario;
            _IFechaVencimiento = iFechaVencimiento;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Salida
        {
            get => _Id_Salida;
            set => _Id_Salida = value;
        }

        private async void Salidas_Load(object sender, EventArgs e)
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
                        case "salidas":
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
            var resp = await _ITipoSalida.ListTipoSalida();
            cbx_tipo_salida.DataSource = resp.Data;
            cbx_tipo_salida.ValueMember = "IdTipoSalida";
            cbx_tipo_salida.DisplayMember = "Nombre";
            cbx_tipo_salida.SelectedIndex = 0;

            if (Id_Salida > 0)
            {
                panel3.Enabled = false;
                panel1.Enabled = false;
                panel2.Enabled = false;

                var DataSalida = await _ISalidaInventario.List(Id_Salida);
                if (DataSalida.Data != null)
                {
                    if (DataSalida.Data.Count > 0)
                    {
                        cbx_tipo_salida.SelectedValue = DataSalida.Data[0].IdTipoSalida;
                        txt_orden_compra.Text = DataSalida.Data[0].OrdenCompra.ToString();
                        txt_num_factura.Text = DataSalida.Data[0].NumFactura.ToString();
                        txt_documento_proveedor.Text = DataSalida.Data[0].NumeroDocumento.ToString();
                        lbl_nombre_proveedor.Text = DataSalida.Data[0].NombreRazonSocial.ToString();
                        txt_comentario.Text = DataSalida.Data[0].Comentario.ToString();

                        decimal Total = 0;
                        decimal Subtotal = 0;
                        decimal SubtotalLinea = 0;

                        foreach (var item in DataSalida.Data)
                        {
                            Subtotal += Convert.ToDecimal(item.CostoUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                            SubtotalLinea = Convert.ToDecimal(item.CostoUnitario, new CultureInfo("es-CO")) * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                            dtg_detalle_salida.Rows.Add(
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.Nombre,
                                item.CodigoLote,
                                item.FechaVencimiento,
                                item.Cantidad.ToString(new CultureInfo("es-CO")),
                                item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                                SubtotalLinea.ToString("N2", new CultureInfo("es-CO")));
                        }

                        Total = Subtotal;

                        lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                    }
                }
            }
        }

        private void btn_busca_pv_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Salidas_busca_proveedor";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            var resp = await _IProveedor.List(id);
            if (resp.Data != null)
            {
                salidaInventarioEntitie.IdProveedor = resp.Data[0].IdProveedor;
                txt_documento_proveedor.Text = resp.Data[0].NumeroDocumento;
                lbl_nombre_proveedor.Text = resp.Data[0].NombreRazonSocial;
            }
        }

        private void btn_add_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaDetalleSalida = _serviceProvider.GetRequiredService<AgregaDetalleSalida>();
                _AgregaDetalleSalida.Permisos = _Permisos;
                _AgregaDetalleSalida.Enviar_Detalle += _ObtenerDetalleSalida;
                _AgregaDetalleSalida.FormClosed += (s, args) => _AgregaDetalleSalida = null;
                _AgregaDetalleSalida.ShowDialog();
            }
        }

        private void _ObtenerDetalleSalida(DetalleSalidaInventarioEntitie detalleSalidasInv)
        {
            var nuevoDetalle = new DetalleSalidaInventarioEntitie
            {
                IdProducto = detalleSalidasInv.IdProducto,
                Sku = detalleSalidasInv.Sku,
                CodigoBarras = detalleSalidasInv.CodigoBarras,
                Nombre = detalleSalidasInv.Nombre,
                CodigoLote = detalleSalidasInv.CodigoLote,
                FechaVencimiento = detalleSalidasInv.FechaVencimiento,
                Cantidad = detalleSalidasInv.Cantidad,
                CostoUnitario = detalleSalidasInv.CostoUnitario,
                Total = detalleSalidasInv.Total,
                TipoProducto = detalleSalidasInv.TipoProducto
            };

            bool Existe = false;

            foreach (DataGridViewRow fila in dtg_detalle_salida.Rows)
            {
                if (Convert.ToInt32(fila.Cells["cl_id_producto"].Value) == nuevoDetalle.IdProducto)
                {
                    Existe = true;
                }
            }

            if (!Existe)
            {
                salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);
                dtg_detalle_salida.Rows.Clear();
                foreach (var item in salidaInventarioEntitie.detalleSalidaInventarios)
                {
                    dtg_detalle_salida.Rows.Add(
                        item.IdProducto,
                        item.Sku,
                        item.CodigoBarras,
                        item.Nombre,
                        item.CodigoLote,
                        item.FechaVencimiento,
                        item.Cantidad.ToString().Replace('.', ','),
                        item.CostoUnitario.ToString("N2", new CultureInfo("es-CO")),
                        item.Total.ToString("N2", new CultureInfo("es-CO")),
                        item.TipoProducto
                    );
                }

                if (salidaInventarioEntitie.detalleSalidaInventarios.Count > 0)
                {
                    decimal Total = salidaInventarioEntitie.detalleSalidaInventarios.Sum(d => d.Total);
                    lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    lbl_total.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Este producto ya está en la lista, no se puede agregar nuevamente.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_salida.Rows.Count > 0)
            {
                salidaInventarioEntitie = new SalidaInventarioEntitie();

                salidaInventarioEntitie.IdTipoSalida = Convert.ToInt32(cbx_tipo_salida.SelectedValue);
                salidaInventarioEntitie.OrdenCompra = txt_orden_compra.Text;
                salidaInventarioEntitie.NumFactura = txt_num_factura.Text;
                salidaInventarioEntitie.Comentario = txt_comentario.Text;
                if (txt_documento_proveedor.Text != "")
                {
                    var respProvee = await _IProveedor.ListNumeroDocumento(txt_documento_proveedor.Text);
                    if (respProvee.Data != null)
                    {
                        salidaInventarioEntitie.IdProveedor = respProvee.Data[0].IdProveedor;
                    }
                }
                else
                {
                    salidaInventarioEntitie.IdProveedor = 1;
                }

                foreach (DataGridViewRow fila in dtg_detalle_salida.Rows)
                {
                    var nuevoDetalle = new DetalleSalidaInventarioEntitie
                    {
                        IdProducto = Convert.ToInt32(fila.Cells["cl_id_producto"].Value),
                        CodigoLote = fila.Cells["cl_lote"].Value.ToString(),
                        FechaVencimiento = Convert.ToDateTime(fila.Cells["cl_fecha_vencimiento"].Value),
                        Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO")),
                        CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO")),                      
                        TipoProducto = fila.Cells["cl_tipo_producto"].Value.ToString()!
                    };

                    salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalle);

                    //Si es producto tipo Grupo, agrega a la entrada los productos de tipo individual pertenecientes al Kit
                    if (nuevoDetalle.TipoProducto == "Grupo")
                    {
                        var respPrdIndv = await _IProducto.ListPrdGrp(nuevoDetalle.IdProducto);
                        if (respPrdIndv.Data != null)
                        {
                            if (respPrdIndv.Data.Count > 0)
                            {
                                foreach (var item in respPrdIndv.Data)
                                {
                                    //Preguntar fecha de vencimiento si aplica
                                    await ValidarFechaVencimiento(Convert.ToInt32(item.IdProductoIndividual));
                                    if (FechaVSeleccionada == new DateTime(1800, 1, 1)) { MessageBox.Show("Se deben seleccionar las fechas de vencimiento requeridas, intente nuevamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                                    var nuevoDetalleIndiv = new DetalleSalidaInventarioEntitie
                                    {
                                        IdProducto = Convert.ToInt32(item.IdProductoIndividual),
                                        CodigoLote = "",
                                        FechaVencimiento = FechaVSeleccionada,
                                        Cantidad = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")) * nuevoDetalle.Cantidad,
                                        CostoUnitario = Convert.ToDecimal(item.CostoBase, new CultureInfo("es-CO")),
                                        TipoProducto = item.TipoProducto
                                    };

                                    salidaInventarioEntitie.detalleSalidaInventarios.Add(nuevoDetalleIndiv);
                                }
                            }
                        }
                    }
                }

                var resp = await _ISalidaInventario.CreateUpdate(salidaInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

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

        public async Task ValidarFechaVencimiento(int IdPrd)
        {
            FechaVSeleccionada = new DateTime(1900, 1, 1);

            var resp = await _IFechaVencimiento.BuscarxIdProductoTieneVence(IdPrd);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 1)
                {
                    _ConfirmaFechaVecimiento = _serviceProvider.GetRequiredService<ConfirmaFechaVecimiento>();
                    _ConfirmaFechaVecimiento.Id_producto = IdPrd;
                    _ConfirmaFechaVecimiento.retornaFechaVencimiento += _RetornaFechaVencimiento;
                    _ConfirmaFechaVecimiento.FormClosed += (s, args) => _ConfirmaFechaVecimiento = null;
                    _ConfirmaFechaVecimiento.ShowDialog();
                }
                else if (resp.Data.Count == 1)
                {
                    FechaVSeleccionada = Convert.ToDateTime(resp.Data[0].FechaVencimiento);
                }
            }
        }

        public void _RetornaFechaVencimiento(DateTime fechaVenceSeleccionada)
        {
            FechaVSeleccionada = fechaVenceSeleccionada;
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

        private void btn_quitar_Click(object sender, EventArgs e)
        {
            if (dtg_detalle_salida.Rows.Count > 0)
            {
                if (dtg_detalle_salida.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de quitar producto de la lista?",
                             "Confirmar cancelacion",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var row = dtg_detalle_salida.SelectedRows[0];
                        if (row.Cells["cl_id_producto"].Value != null)
                        {
                            int Id_Producto = Convert.ToInt32(row.Cells["cl_id_producto"].Value);
                            dtg_detalle_salida.Rows.RemoveAt(row.Index);

                            var item = salidaInventarioEntitie.detalleSalidaInventarios
                            .FirstOrDefault(d => d.IdProducto == Id_Producto);

                            if (item != null)
                            {
                                salidaInventarioEntitie.detalleSalidaInventarios.Remove(item);
                            }

                            decimal Total = 0;
                            decimal Subtotal = 0;

                            foreach (DataGridViewRow fila in dtg_detalle_salida.Rows)
                            {
                                Subtotal += Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO")) * Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO")); 
                            }
                            Total = Subtotal;

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
    }
}
