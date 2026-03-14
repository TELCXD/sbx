using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using sbx.core.Entities.EntradaInventario;
using sbx.core.Entities.Producto;
using sbx.core.Entities.SalidaInventario;
using sbx.core.Interfaces.Categoria;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.FechaVencimiento;
using sbx.core.Interfaces.Marca;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.SalidaInventario;
using sbx.core.Interfaces.Tributos;
using sbx.core.Interfaces.UnidadMedida;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace sbx
{
    public partial class AgregarProducto : Form
    {
        private dynamic? _Permisos;
        private int _Id_Producto;
        private readonly ICategoria _ICategoria;
        private readonly IMarca _IMarca;
        private readonly IUnidadMedida _IUnidadMedida;
        private readonly IProducto _IProducto;
        private readonly ITribute _ITribute;
        private Marcas? _Marcas;
        private AgregaCodigosBarras? _AgregaCodigosBarras;
        private AgregaProductoGrupo? _AgregaProductoGrupo;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFechaVencimiento _IFechaVencimiento;
        private ConfirmaFechaVecimiento? _ConfirmaFechaVecimiento;
        private readonly IEntradaInventario _IEntradaInventario;
        private readonly ISalidaInventario _ISalidaInventario;
        public AgregarProducto(ICategoria categoria, IMarca marca, IUnidadMedida unidadMedida, IProducto producto, ITribute tribute, 
            IServiceProvider iServiceProvider, IFechaVencimiento fechaVencimiento, IEntradaInventario entradaInventario, ISalidaInventario salidaInventario)
        {
            InitializeComponent();

            _ICategoria = categoria;
            _IMarca = marca;
            _IUnidadMedida = unidadMedida;
            _IProducto = producto;
            _ITribute = tribute;
            _serviceProvider = iServiceProvider;
            _IFechaVencimiento = fechaVencimiento;
            _IEntradaInventario = entradaInventario;
            _ISalidaInventario = salidaInventario;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_Producto
        {
            get => _Id_Producto;
            set => _Id_Producto = value;
        }

        char decimalSeparator = ',';
        private bool formatting = false;
        DateTime FechaVSeleccionada = new DateTime(1900, 1, 1);

        private async void AgregarProducto_Load(object sender, EventArgs e)
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
                        case "productos":
                            btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
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
            var resp = await _ICategoria.ListCategoria();
            cbx_categoria.DataSource = resp.Data;
            cbx_categoria.ValueMember = "IdCategoria";
            cbx_categoria.DisplayMember = "Nombre";
            cbx_categoria.SelectedIndex = 0;

            resp = await _IMarca.ListMarca();
            cbx_marca.DataSource = resp.Data;
            cbx_marca.ValueMember = "IdMarca";
            cbx_marca.DisplayMember = "Nombre";
            cbx_marca.SelectedIndex = 0;

            resp = await _IUnidadMedida.ListUnidadMedida();
            cbx_unidad_medida.DataSource = resp.Data;
            cbx_unidad_medida.ValueMember = "IdUnidadMedida";
            cbx_unidad_medida.DisplayMember = "Nombre";
            cbx_unidad_medida.SelectedIndex = 0;

            resp = await _ITribute.ListTribute();
            cbx_tipo_tributo.DataSource = resp.Data;
            cbx_tipo_tributo.ValueMember = "Idtribute";
            cbx_tipo_tributo.DisplayMember = "Nombre";
            cbx_tipo_tributo.SelectedIndex = 0;

            cbx_es_inventariable.SelectedIndex = 0;
            cbx_tipo_producto.SelectedIndex = 0;
            rbEntrada.Checked = true;
            txt_cantidad_E_S.Text = "0";

            if (Id_Producto > 0)
            {
                resp = await _IProducto.List(Id_Producto);

                if (resp.Data != null)
                {
                    txt_codigo_interno.Text = resp.Data[0].IdProducto.ToString();
                    txt_sku.Text = resp.Data[0].Sku;
                    txt_codigo_barras.Text = resp.Data[0].CodigoBarras;
                    txt_nombre.Text = resp.Data[0].Nombre;
                    txt_costo.Text = resp.Data[0].CostoBase.ToString("N2", new CultureInfo("es-CO"));
                    txt_precio.Text = resp.Data[0].PrecioBase.ToString("N2", new CultureInfo("es-CO"));
                    calculoPorcentajeMargen();
                    cbx_es_inventariable.SelectedIndex = resp.Data[0].EsInventariable == true ? 0 : 1;
                    txt_impuesto.Text = resp.Data[0].Impuesto.ToString(new CultureInfo("es-CO"));
                    cbx_categoria.SelectedValue = resp.Data[0].IdCategoria;
                    cbx_marca.SelectedValue = resp.Data[0].IdMarca;
                    cbx_unidad_medida.SelectedValue = resp.Data[0].IdUnidadMedida;
                    cbx_tipo_tributo.SelectedValue = resp.Data[0].Idtribute;
                    cbx_tipo_producto.SelectedIndex = resp.Data[0].TipoProducto == "Individual" ? 0 : 1;
                    txt_stock.Text = resp.Data[0].Stock.ToString("N2", new CultureInfo("es-CO"));
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (txt_codigo_interno.Text.IsNullOrEmpty())
            {
                btn_agrega_codigos_barras.Enabled = false;
            }
            else
            {
                btn_agrega_codigos_barras.Enabled = true;
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_costo.Text)) { errorProvider1.SetError(txt_costo, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_precio.Text)) { errorProvider1.SetError(txt_precio, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_impuesto.Text)) { errorProvider1.SetError(txt_impuesto, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_cantidad_E_S.Text)) { errorProvider1.SetError(txt_cantidad_E_S, "Ingrese un valor numerico"); valido++; }

            if (valido > 0) { return; } else { valido = 0; }

            if (Convert.ToDecimal(txt_precio.Text, new CultureInfo("es-CO")) <= 0) { errorProvider1.SetError(txt_precio, "El precio debe ser mayor a cero"); valido++; }

            if (valido > 0) { return; }

            var Datos = new ProductoEntitie
            {
                IdProducto = Id_Producto,
                Sku = txt_sku.Text.Trim(),
                CodigoBarras = txt_codigo_barras.Text.Trim(),
                Nombre = txt_nombre.Text.Trim(),
                CostoBase = Convert.ToDecimal(txt_costo.Text, new CultureInfo("es-CO")),
                PrecioBase = Convert.ToDecimal(txt_precio.Text, new CultureInfo("es-CO")),
                Impuesto = Convert.ToDecimal(txt_impuesto.Text, new CultureInfo("es-CO")),
                IdCategoria = Convert.ToInt32(cbx_categoria.SelectedValue),
                IdMarca = Convert.ToInt32(cbx_marca.SelectedValue),
                IdUnidadMedida = Convert.ToInt32(cbx_unidad_medida.SelectedValue),
                Idtribute = Convert.ToInt32(cbx_tipo_tributo.SelectedValue),
                EsInventariable = cbx_es_inventariable.Text == "SI" ? 1 : 0,
                TipoProducto = cbx_tipo_producto.Text
            };

            var validationContext = new ValidationContext(Datos);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            bool esValido = Validator.TryValidateObject(Datos, validationContext, validationResults, true);

            if (esValido)
            {
                var Exist = await _IProducto.ExisteSku(txt_sku.Text.Trim(), Id_Producto);
                if (Exist) { errorProvider1.SetError(txt_sku, "Sku ya existe"); valido++; }
                Exist = await _IProducto.ExisteCodigoBarras(txt_codigo_barras.Text.Trim(), Id_Producto);
                if (Exist) { errorProvider1.SetError(txt_codigo_barras, "Codigo de barras ya existe"); valido++; }
                Exist = await _IProducto.ListIdCodigoBarras(Id_Producto, txt_codigo_barras.Text.Trim());
                if (Exist) { MessageBox.Show("Codigo de barras ya existe en otro producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); valido++; }
                Exist = await _IProducto.ExisteNombre(txt_nombre.Text.Trim(), Id_Producto);
                if (Exist) { errorProvider1.SetError(txt_nombre, "Nombre ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _IProducto.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        int IdPrd = Datos.IdProducto > 0 ? Datos.IdProducto : Convert.ToInt32(resp.Data);

                        if (Convert.ToDecimal(txt_cantidad_E_S.Text, new CultureInfo("es-CO")) > 0) 
                        {
                            var respEntradaSalida = await entradaSalidaSencillaInventario(IdPrd, Convert.ToDecimal(txt_cantidad_E_S.Text, new CultureInfo("es-CO")), Datos.TipoProducto);
                        }

                        MessageBox.Show($"{resp.Message}, Id Producto: {IdPrd}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                foreach (var validationResult in validationResults)
                {
                    if (validationResult.MemberNames.Contains("Nombre"))
                    {
                        errorProvider1.SetError(txt_nombre, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("CostoBase"))
                    {
                        errorProvider1.SetError(txt_costo, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("PrecioBase"))
                    {
                        errorProvider1.SetError(txt_precio, validationResult.ErrorMessage);
                    }

                    if (validationResult.MemberNames.Contains("Impuesto"))
                    {
                        errorProvider1.SetError(txt_impuesto, validationResult.ErrorMessage);
                    }
                }
            }
        }

        private void txt_iva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_iva_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_impuesto.Text, out _))
            {
                errorProvider1.SetError(txt_impuesto, "Ingrese un valor numerico");
            }
        }

        private void txt_costo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_costo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_precio_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txt_costo_Leave(object sender, EventArgs e)
        {

        }

        private void txt_precio_Leave(object sender, EventArgs e)
        {

        }

        private void cbx_tipo_tributo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (dynamic?)cbx_tipo_tributo.SelectedItem;
            string Nombre = item!.Nombre;

            if (Nombre == "INC Bolsas")
            {
                lbl_tributo.Text = "Valor Impuesto bolsa *";
            }
            else
            {
                lbl_tributo.Text = "Impuesto (%) *";
            }
        }

        private void btn_agregaMarca_Click(object sender, EventArgs e)
        {
            _Marcas = _serviceProvider.GetRequiredService<Marcas>();
            _Marcas.FormClosed += (s, args) => _Marcas = null;
            _Marcas.ShowDialog();
        }

        private void cbx_tipo_producto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_agrega_codigos_barras_Click(object sender, EventArgs e)
        {
            _AgregaCodigosBarras = _serviceProvider.GetRequiredService<AgregaCodigosBarras>();
            _AgregaCodigosBarras.Permisos = _Permisos;
            _AgregaCodigosBarras.Id_Producto = Convert.ToInt32(txt_codigo_interno.Text);
            _AgregaCodigosBarras.FormClosed += (s, args) => _AgregaCodigosBarras = null;
            _AgregaCodigosBarras.ShowDialog();
        }

        private decimal calcula_Margen(decimal costo, decimal precioVenta)
        {
            decimal Margen = 0;
            decimal ganancia = 0;

            if (costo > 0 && precioVenta > 0)
            {

                ganancia = precioVenta - costo;
                Margen = (ganancia / precioVenta) * 100;
            }

            return Margen;
        }

        private void txt_costo_KeyUp(object sender, KeyEventArgs e)
        {
            calculoPorcentajeMargen();
        }

        private void txt_precio_KeyUp(object sender, KeyEventArgs e)
        {
            calculoPorcentajeMargen();
        }

        private void calculoPorcentajeMargen()
        {
            decimal Porcentaje = 0;

            if (string.IsNullOrEmpty(txt_costo.Text.Trim()) || string.IsNullOrEmpty(txt_precio.Text.Trim()))
                return;

            if (Convert.ToDecimal(txt_costo.Text) > 0 && Convert.ToDecimal(txt_precio.Text) > 0)
            {
                Porcentaje = calcula_Margen(Convert.ToDecimal(txt_costo.Text), Convert.ToDecimal(txt_precio.Text));
            }


            txt_margen.Text = Porcentaje.ToString("N2");
        }

        private void txt_cantidad_E_S_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_cantidad_E_S_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!decimal.TryParse(txt_cantidad_E_S.Text, out _))
            {
                errorProvider1.SetError(txt_cantidad_E_S, "Ingrese un valor numerico");
            }
        }

        private async Task<bool> entradaSalidaSencillaInventario(int idProducto, decimal cantidad, string tipoProducto)
        {
            if (rbEntrada.Checked)
            {
                EntradasInventarioEntitie entradasInventarioEntitie = new EntradasInventarioEntitie();

                entradasInventarioEntitie.IdTipoEntrada = 2;
                entradasInventarioEntitie.OrdenCompra = "";
                entradasInventarioEntitie.NumFactura = "";
                entradasInventarioEntitie.Comentario = "Entrada sencilla desde Producto";
                entradasInventarioEntitie.IdProveedor = 1;

                var nuevoDetalle = new DetalleEntradasInventarioEntitie
                {
                    IdProducto = idProducto,
                    CodigoLote = "",
                    FechaVencimiento = new DateTime(1900, 1, 1),
                    Cantidad = cantidad,
                    CostoUnitario = 0,
                    Descuento = 0,
                    Impuesto = 0,
                    TipoProducto = tipoProducto
                };

                entradasInventarioEntitie.detalleEntradasInventarios.Add(nuevoDetalle);

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
                                if (FechaVSeleccionada == new DateTime(1800, 1, 1)) { 
                                    MessageBox.Show("Se deben seleccionar las fechas de vencimiento requeridas, intente nuevamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                    return false; 
                                }

                                var nuevoDetalleIndiv = new DetalleEntradasInventarioEntitie
                                {
                                    IdProducto = Convert.ToInt32(item.IdProductoIndividual),
                                    CodigoLote = "",
                                    FechaVencimiento = FechaVSeleccionada,
                                    Cantidad = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO")) * nuevoDetalle.Cantidad,
                                    CostoUnitario = Convert.ToDecimal(item.CostoBase, new CultureInfo("es-CO")),
                                    Descuento = 0,
                                    Impuesto = 0,
                                    TipoProducto = item.TipoProducto
                                };

                                entradasInventarioEntitie.detalleEntradasInventarios.Add(nuevoDetalleIndiv);
                            }
                        }
                    }
                }

                var resp = await _IEntradaInventario.CreateUpdate(entradasInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else if (rd_Salida.Checked)
            {
                SalidaInventarioEntitie salidaInventarioEntitie = new SalidaInventarioEntitie();

                salidaInventarioEntitie.IdTipoSalida = 2;
                salidaInventarioEntitie.OrdenCompra = "";
                salidaInventarioEntitie.NumFactura = "";
                salidaInventarioEntitie.Comentario = "Salida sencilla desde Producto";
                salidaInventarioEntitie.IdProveedor = 1;

                var nuevoDetalle = new DetalleSalidaInventarioEntitie
                {
                    IdProducto = idProducto,
                    CodigoLote = "",
                    FechaVencimiento = new DateTime(1900, 1, 1),
                    Cantidad = cantidad,
                    CostoUnitario = 0,
                    TipoProducto = tipoProducto
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
                                if (FechaVSeleccionada == new DateTime(1800, 1, 1)) 
                                { 
                                    MessageBox.Show("Se deben seleccionar las fechas de vencimiento requeridas, intente nuevamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                    return false; 
                                }

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

                var resp = await _ISalidaInventario.CreateUpdate(salidaInventarioEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
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
    }
}
