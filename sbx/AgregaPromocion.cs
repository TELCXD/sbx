using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.Promociones;
using sbx.core.Entities.PromocionProducto;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Promociones;
using sbx.core.Interfaces.PromocionProducto;
using sbx.core.Interfaces.TipoPromocion;
using System.Globalization;

namespace sbx
{
    public partial class AgregaPromocion : Form
    {
        char decimalSeparator = ',';
        private dynamic? _Permisos;
        private readonly ITipoPromocion _ITipoPromocion;
        private int _Id_promocion;
        private readonly IPromociones _IPromociones;
        private readonly IPromocionProducto _IPromocionProducto;
        private int _IdPromocion;
        PromocionesEntitie promocionesEntitie = new PromocionesEntitie();
        private readonly IServiceProvider _serviceProvider;
        private AddProductoPromocion? _AddProductoPromocion;
        PromocionProductoEntitie promocionProductoEntitie = new PromocionProductoEntitie();
        private readonly IProducto _IProducto;

        public AgregaPromocion(ITipoPromocion tipoPromocion, IPromociones promociones, IPromocionProducto promocionProducto, IServiceProvider serviceProvider, IProducto producto)
        {
            InitializeComponent();
            _ITipoPromocion = tipoPromocion;
            _IPromociones = promociones;
            _IPromocionProducto = promocionProducto;
            _serviceProvider = serviceProvider;
            _IProducto = producto;
        }

        private async void AgregaPromocion_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_promocion
        {
            get => _Id_promocion;
            set => _Id_promocion = value;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "promociones":
                            btn_add_promocion.Enabled = item.ToCreate == 1 ? true : false;
                            btn_agregar_producto.Enabled = item.ToCreate == 1 ? true : false;
                            btn_quitar_producto.Enabled = item.ToCreate == 1 ? true : false;
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
            var resp = await _ITipoPromocion.ListTipoPromocion();
            cbx_tipo_promocion.DataSource = resp.Data;
            cbx_tipo_promocion.ValueMember = "IdTipoPromocion";
            cbx_tipo_promocion.DisplayMember = "Nombre";
            cbx_tipo_promocion.SelectedIndex = 0;

            if (Id_promocion > 0)
            {
                resp = await _IPromociones.List(Id_promocion);

                if (resp.Data != null)
                {
                    txt_nombre_promocion.Text = resp.Data[0].NombrePromocion;
                    cbx_tipo_promocion.SelectedValue = resp.Data[0].IdTipoPromocion;
                    txt_porcj_desc.Text = resp.Data[0].Porcentaje.ToString("N2", new CultureInfo("es-CO"));
                    dtp_fecha_inicio.Value = resp.Data[0].FechaInicio;
                    dtp_fecha_fin.Value = resp.Data[0].FechaFin;

                    resp = await _IPromocionProducto.List(Id_promocion);

                    dtg_producto.Rows.Clear();

                    if (resp.Data != null)
                    {
                        if (resp.Data.Count > 0)
                        {
                            foreach (var item in resp.Data)
                            {
                                dtg_producto.Rows.Add(
                                item.IdProducto,
                                item.Sku,
                                item.CodigoBarras,
                                item.Nombre);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txt_porcj_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_add_promocion_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_nombre_promocion.Text.Trim() != "")
            {
                if (txt_porcj_desc.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txt_porcj_desc.Text, new CultureInfo("es-CO")) > 0)
                    {
                        DateTime fechaIni = dtp_fecha_inicio.Value.Date;
                        DateTime fechaFin = dtp_fecha_fin.Value.Date;

                        if (fechaIni <= fechaFin)
                        {
                            promocionesEntitie.IdPromocion = Id_promocion;
                            promocionesEntitie.NombrePromocion = txt_nombre_promocion.Text.Trim();
                            promocionesEntitie.Porcentaje = Convert.ToDecimal(txt_porcj_desc.Text, new CultureInfo("es-CO"));
                            promocionesEntitie.IdTipoPromocion = Convert.ToInt32(cbx_tipo_promocion.SelectedValue);
                            promocionesEntitie.FechaInicio = dtp_fecha_inicio.Value;
                            promocionesEntitie.FechaFin = dtp_fecha_fin.Value;
                            var resp = await _IPromociones.CreateUpdate(promocionesEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                            if (resp != null)
                            {
                                if (resp.Flag == true && resp.Data != null)
                                {
                                    Id_promocion = Convert.ToInt32(resp.Data);

                                    MessageBox.Show($"Se creo promocion correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se obtuvo respuesta de creacion o actualizacion de promocion", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            errorProvider1.SetError(dtp_fecha_inicio, "Fecha inicio debe ser menor a fecha fin");
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(txt_porcj_desc, "Descuento debe ser mayor a cero (0)");
                    }
                }
                else
                {
                    errorProvider1.SetError(txt_porcj_desc, "Debe ingresar porcentaje descuento");
                }
            }
            else
            {
                errorProvider1.SetError(txt_nombre_promocion, "Debe ingresar nombre de promocion");
            }
        }

        private void btn_agregar_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                if (Id_promocion > 0)
                {
                    _AddProductoPromocion = _serviceProvider.GetRequiredService<AddProductoPromocion>();
                    _AddProductoPromocion.Permisos = _Permisos;
                    _AddProductoPromocion.Enviar_producto += _ObtenerProducto;
                    _AddProductoPromocion.FormClosed += (s, args) => _AddProductoPromocion = null;
                    _AddProductoPromocion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No existe promocion para agregar productos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void _ObtenerProducto(int IdProducto)
        {
            var DataProducto = await _IProducto.List(IdProducto);

            if (DataProducto.Data != null)
            {
                promocionProductoEntitie.IdPromocion = Id_promocion;
                promocionProductoEntitie.IdProducto = IdProducto;
                var resp = await _IPromocionProducto.CreateUpdate(promocionProductoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        resp = await _IPromocionProducto.List(Id_promocion);

                        dtg_producto.Rows.Clear();

                        if (resp.Data != null)
                        {
                            if (resp.Data.Count > 0)
                            {
                                foreach (var item in resp.Data)
                                {
                                    dtg_producto.Rows.Add(
                                    item.IdProducto,
                                    item.Sku,
                                    item.CodigoBarras,
                                    item.Nombre);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private async void btn_quitar_producto_Click(object sender, EventArgs e)
        {
            if (dtg_producto.Rows.Count > 0)
            {
                if (dtg_producto.SelectedRows.Count > 0)
                {
                    var row = dtg_producto.SelectedRows[0];
                    if (row.Cells["cl_idProducto"].Value != null)
                    {
                        int IdProducto = Convert.ToInt32(row.Cells["cl_idProducto"].Value);

                        promocionProductoEntitie.IdPromocion = Id_promocion;
                        promocionProductoEntitie.IdProducto = IdProducto;

                        DialogResult result = MessageBox.Show("¿Está seguro de eliminar este registro?",
                                     "Confirmar eliminación",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            var resp = await _IPromocionProducto.RemoveProducto(promocionProductoEntitie);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    resp = await _IPromocionProducto.List(Id_promocion);

                                    dtg_producto.Rows.Clear();

                                    if (resp.Data != null)
                                    {
                                        if (resp.Data.Count > 0)
                                        {
                                            foreach (var item in resp.Data)
                                            {
                                                dtg_producto.Rows.Add(
                                                item.IdProducto,
                                                item.Sku,
                                                item.CodigoBarras,
                                                item.Nombre);
                                            }
                                        }
                                    }
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
                        MessageBox.Show("No se encontro id de producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para quitar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
