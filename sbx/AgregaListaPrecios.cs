using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.ListaPrecios;
using sbx.core.Entities.PrecioProducto;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.PrecioProducto;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.TipoCliente;
using System.Globalization;

namespace sbx
{
    public partial class AgregaListaPrecios : Form
    {
        private dynamic? _Permisos;
        private int _IdListaPrecio;
        private readonly IServiceProvider _serviceProvider;
        private int _Id_lista_precios;
        private readonly ITipoCliente _ITipoCliente;
        private readonly IListaPrecios _IListaPrecios;
        private readonly IPrecioProducto _IPrecioProducto;
        private AddProductoListaPrecio? _AddProductoListaPrecio;
        private readonly IProducto _IProducto;
        ListaPreciosEntitie listaPreciosEntitie = new ListaPreciosEntitie();
        PrecioProductoEntitie precioProductoEntitie = new PrecioProductoEntitie();

        public AgregaListaPrecios(IServiceProvider serviceProvider, ITipoCliente iTipoCliente, IListaPrecios listaPrecios, IPrecioProducto precioProducto, IProducto iProducto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ITipoCliente = iTipoCliente;
            _IListaPrecios = listaPrecios;
            _IPrecioProducto = precioProducto;
            _IProducto = iProducto;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int Id_lista_precios
        {
            get => _Id_lista_precios;
            set => _Id_lista_precios = value;
        }

        private async void AgregaListaPrecios_Load(object sender, EventArgs e)
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
                        case "listaPrecios":
                            btn_add_precio_cliente.Enabled = item.ToCreate == 1 ? true : false;
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
            var resp = await _ITipoCliente.ListTipoCliente();
            cbx_tipo_cliente.DataSource = resp.Data;
            cbx_tipo_cliente.ValueMember = "IdTipoCliente";
            cbx_tipo_cliente.DisplayMember = "Nombre";
            cbx_tipo_cliente.SelectedIndex = 0;

            if (Id_lista_precios > 0)
            {
                resp = await _IListaPrecios.List(Id_lista_precios);

                if (resp.Data != null)
                {
                    txt_nombre_lista_precios.Text = resp.Data[0].NombreLista;
                    cbx_tipo_cliente.SelectedValue = resp.Data[0].IdTipoCliente;
                    dtp_fecha_inicio.Value = resp.Data[0].FechaInicio;
                    dtp_fecha_fin.Value = resp.Data[0].FechaFin;

                    resp = await _IPrecioProducto.List(Id_lista_precios);

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
                               item.Nombre,
                               item.Precio.ToString("N2", new CultureInfo("es-CO")));
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

        private async void btn_add_precio_cliente_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_nombre_lista_precios.Text.Trim() != "")
            {
                DateTime fechaIni = dtp_fecha_inicio.Value;
                DateTime fechaFin = dtp_fecha_fin.Value;

                if (fechaIni <= fechaFin)
                {
                    listaPreciosEntitie.IdListaPrecio = Id_lista_precios;
                    listaPreciosEntitie.NombreLista = txt_nombre_lista_precios.Text.Trim();
                    listaPreciosEntitie.IdTipoCliente = Convert.ToInt32(cbx_tipo_cliente.SelectedValue);
                    listaPreciosEntitie.FechaInicio = dtp_fecha_inicio.Value;
                    listaPreciosEntitie.FechaFin = dtp_fecha_fin.Value;

                    var resp = await _IListaPrecios.CreateUpdate(listaPreciosEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                    if (resp != null)
                    {
                        if (resp.Flag == true && resp.Data != null)
                        {
                            Id_lista_precios = Convert.ToInt32(resp.Data);

                            MessageBox.Show($"Se creo lista correctamente", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("No se obtuvo respuesta de creacion o actualizacion de lista de precios", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    errorProvider1.SetError(dtp_fecha_inicio, "Fecha inicio debe ser menor a fecha fin");
                }
            }
            else
            {
                errorProvider1.SetError(txt_nombre_lista_precios, "Debe ingresar nombre lista de precios");
            }
        }

        private void btn_agregar_producto_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                if (Id_lista_precios > 0) 
                {
                    _AddProductoListaPrecio = _serviceProvider.GetRequiredService<AddProductoListaPrecio>();
                    _AddProductoListaPrecio.Permisos = _Permisos;
                    _AddProductoListaPrecio.Enviar_producto += _ObtenerProducto;
                    _AddProductoListaPrecio.FormClosed += (s, args) => _AddProductoListaPrecio = null;
                    _AddProductoListaPrecio.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No existe lista de precios para agregar productos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        precioProductoEntitie.IdListaPrecio = Id_lista_precios;
                        precioProductoEntitie.IdProducto = IdProducto;

                        DialogResult result = MessageBox.Show("¿Está seguro de eliminar este registro?",
                                     "Confirmar eliminación",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            var resp = await _IPrecioProducto.RemoveProducto(precioProductoEntitie);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    resp = await _IPrecioProducto.List(Id_lista_precios);

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
                                                item.Nombre,
                                                item.Precio.ToString("N2", new CultureInfo("es-CO")));
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

        private async void _ObtenerProducto(int IdProducto, decimal Precio)
        {
            var DataProducto = await _IProducto.List(IdProducto);

            if (DataProducto.Data != null)
            {
                precioProductoEntitie.IdListaPrecio = Id_lista_precios;
                precioProductoEntitie.IdProducto = IdProducto;
                precioProductoEntitie.Precio = Convert.ToDecimal(Precio, new CultureInfo("es-CO"));

                var resp = await _IPrecioProducto.CreateUpdate(precioProductoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        resp = await _IPrecioProducto.List(Id_lista_precios);

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
                                    item.Nombre,
                                    item.Precio.ToString("N2", new CultureInfo("es-CO")));
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
    }
}
