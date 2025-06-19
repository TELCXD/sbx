using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.AgrupacionProducto;
using sbx.core.Interfaces.ConversionProducto;
using sbx.core.Interfaces.Producto;
using System.Globalization;

namespace sbx
{
    public partial class AddConversionProducto : Form
    {
        char decimalSeparator = ',';
        private dynamic? _Permisos;
        private Int64 _Id_ConversionesProducto = 0;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private readonly IConversionProducto _IConversionProducto;
        ConversionProductoEntitie conversionProductoEntitie = new ConversionProductoEntitie();

        public AddConversionProducto(IServiceProvider serviceProvider, IProducto iProducto, IConversionProducto conversionProducto)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = iProducto;
            _IConversionProducto = conversionProducto;
        }

        private async void AddAgrupacionProducto_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            conversionProductoEntitie.Llave = Id_ConversionesProducto;
            await CargaDatosIniciales();
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public Int64 Id_ConversionesProducto
        {
            get => _Id_ConversionesProducto;
            set => _Id_ConversionesProducto = value;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "conversionProducto":
                            btn_guardar_conversion.Enabled = item.ToCreate == 1 ? true : false;
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
            if (Id_ConversionesProducto > 0)
            {
                var resp = await _IConversionProducto.List(Id_ConversionesProducto);

                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        conversionProductoEntitie.IdProductoPadre = Convert.ToInt32(resp.Data[0].IdProductoPadre);
                        string id_sku_codigobarras = resp.Data[0].IdProductoPadre + " - " + resp.Data[0].SkuPadre + " - " + resp.Data[0].CodigoBarraasPadre;
                        txt_producto_padre.Text = id_sku_codigobarras;
                        lbl_nombre_producto_padre.Text = resp.Data[0].NombrePadre;
                        btn_busca_producto_padre.Enabled = false;

                        conversionProductoEntitie.IdProductoHijo = Convert.ToInt32(resp.Data[0].IdProductoHijo);
                        string id_sku_codigobarrashijo = resp.Data[0].IdProductoHijo + " - " + resp.Data[0].SkuHijo + " - " + resp.Data[0].CodigoBarrasHijo;
                        txt_producto_hijo.Text = id_sku_codigobarrashijo;
                        lbl_nombre_producto_hijo.Text = resp.Data[0].NombreHijo;
                        btn_busca_producto_hijo.Enabled = false;

                        decimal cantidad = Convert.ToDecimal(resp.Data[0].Cantidad, new CultureInfo("es-CO"));

                        txt_cantidad.Text = cantidad.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_guardar_agrupacion_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txt_producto_padre.Text.Trim() != "" && txt_producto_hijo.Text.Trim() != "" && txt_cantidad.Text.Trim() != "") 
            {
                conversionProductoEntitie.Cantidad = Convert.ToDecimal(txt_cantidad.Text, new CultureInfo("es-CO"));

                var resp = await _IConversionProducto.CreateUpdate(conversionProductoEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                if (txt_producto_padre.Text.Trim() == "") { errorProvider1.SetError(txt_producto_padre, "Debe seleccionar el producto padre"); }
                if (txt_producto_hijo.Text.Trim() == "") { errorProvider1.SetError(txt_producto_hijo, "Debe seleccionar el producto hijo"); }
                if (txt_cantidad.Text.Trim() == "") { errorProvider1.SetError(txt_cantidad, "Debe ingresar cantidad de producto hijo"); }
            }
        }

        private void btn_busca_producto_padre_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Add_ConversionProductoPadre";
            _Buscador.EnviaId += _Buscador_EnviaId_padre;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private void btn_busca_producto_hijo_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "Add_ConversionProductoHijo";
            _Buscador.EnviaId += _Buscador_EnviaId_hijo;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId_padre(int id)
        {
            errorProvider1.Clear();
            var DataProducto = await _IProducto.List(id);
            if (DataProducto.Data != null)
            {
                if (DataProducto.Data.Count > 0)
                {
                    conversionProductoEntitie.IdProductoPadre = Convert.ToInt32(DataProducto.Data[0].IdProducto);

                    string id_sku_codigobarras = DataProducto.Data[0].IdProducto + " - " + DataProducto.Data[0].Sku + " - " + DataProducto.Data[0].CodigoBarras;
                    txt_producto_padre.Text = id_sku_codigobarras;

                    lbl_nombre_producto_padre.Text = DataProducto.Data[0].Nombre;
                }
            }
        }

        private async void _Buscador_EnviaId_hijo(int id)
        {
            errorProvider1.Clear();
            var DataProducto = await _IProducto.List(id);
            if (DataProducto.Data != null)
            {
                if (DataProducto.Data.Count > 0)
                {
                    conversionProductoEntitie.IdProductoHijo = Convert.ToInt32(DataProducto.Data[0].IdProducto);

                    string id_sku_codigobarras = DataProducto.Data[0].IdProducto + " - " + DataProducto.Data[0].Sku + " - " + DataProducto.Data[0].CodigoBarras;
                    txt_producto_hijo.Text = id_sku_codigobarras;

                    lbl_nombre_producto_hijo.Text = DataProducto.Data[0].Nombre;
                }
            }
        }
    }
}
