using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Venta;
using System.Globalization;

namespace sbx
{
    public partial class AgregaProductoGrupo : Form
    {
        private int _Id_Producto_grupo;
        private Buscador? _Buscador;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProducto _IProducto;
        private readonly IVenta _IVenta;
        private dynamic? _Permisos;
        string BuscarPor = "";
        private readonly IParametros _IParametros;
        int Id_producto_grupo = 0;
        int Id_producto_indiv = 0;
        char decimalSeparator = ',';
        private decimal CantidadInicial = 0;

        public AgregaProductoGrupo(IServiceProvider serviceProvider, IProducto producto, IParametros iParametros, IVenta venta)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProducto = producto;
            _IParametros = iParametros;
            _IVenta = venta;
        }

        public int Id_Producto
        {
            get => _Id_Producto_grupo;
            set => _Id_Producto_grupo = value;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void AgregaProductoGrupo_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            var DataParametros = await _IParametros.List("");

            BuscarPor = "Id";

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    foreach (var itemParametros in DataParametros.Data)
                    {
                        switch (itemParametros.Nombre)
                        {
                            case "Buscar en venta por":
                                BuscarPor = itemParametros.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            cbx_busca_por.Text = BuscarPor;


            if (Id_Producto > 0)
            {
                _Buscador_EnviaId(Id_Producto);
            }
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
                            btn_busca_pr.Enabled = item.ToRead == 1 ? true : false;
                            btn_busca_producto.Enabled = item.ToRead == 1 ? true : false;
                            btn_quitar1.Enabled = item.ToDelete == 1 ? true : false;
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

        private void btn_busca_pr_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "busca_producto_tipo_grupo";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            panel2.Enabled = false;
            Id_producto_grupo = 0;

            var resp = await _IProducto.List(id);
            if (resp.Data != null)
            {
                Id_producto_grupo = id;

                txt_producto_grupo.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                lbl_nombre_producto.Text = resp.Data[0].Nombre;
                decimal Precio = Convert.ToDecimal(resp.Data[0].PrecioBase, new CultureInfo("es-CO"));
                lbl_precio_unitario.Text = Precio.ToString("N2", new CultureInfo("es-CO"));
                decimal Costo = Convert.ToDecimal(resp.Data[0].CostoBase, new CultureInfo("es-CO"));
                lbl_costo_unitario.Text = Costo.ToString("N2", new CultureInfo("es-CO"));

                panel2.Enabled = true;

                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                dtg_prd_individual.Rows.Clear();

                if (respData.Data != null)
                {
                    if (respData.Data.Count > 0)
                    {
                        foreach (var item in respData.Data)
                        {
                            dtg_prd_individual.Rows.Add(
                                item.IdProductoIndividual,
                                item.Sku,
                                item.CodigoBarras,
                                item.Nombre,
                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                        }

                        if (dtg_prd_individual.Rows.Count > 0)
                        {
                            decimal totalCantidad = 0;
                            decimal totalCostos = 0;
                            decimal totalPrecios = 0;

                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                            {
                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                totalCostos += CostoUnitario * Cantidad;
                                totalPrecios += PrecioUnitario * Cantidad;
                                totalCantidad += Cantidad;
                            }

                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                        }
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btn_busca_producto_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "busca_producto_tipo_individual";
            _Buscador.EnviaId += _Buscador_EnviaId_Individual;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId_Individual(int id)
        {
            if (!string.IsNullOrEmpty(txt_producto_grupo.Text))
            {
                Id_producto_indiv = 0;

                var resp = await _IProducto.List(id);
                if (resp.Data != null)
                {
                    if (resp.Data.Count > 0)
                    {
                        Id_producto_indiv = id;

                        var Exist = await _IProducto.ExistePrdIndiv(Id_producto_grupo, Id_producto_indiv);

                        if (!Exist)
                        {
                            //var DataParametros = await _IParametros.List("Validar stock para venta");

                            //if (DataParametros.Data != null)
                            //{
                            //    string ValidaStock = DataParametros.Data[0].Value;
                            //    if (ValidaStock == "SI")
                            //    {
                            //        decimal CantidadEquivalenteVentaTemp = 0;
                            //        decimal CantidadConversionTemp = 0;
                            //        decimal Stock = resp.Data[0].Stock;
                            //        decimal StockInicial = resp.Data[0].Stock;

                            //        //Identificar si tiene productos padre en listay restar del stock
                            //        var ProductosPadre = await _IVenta.IdentificaProductoPadreNivel1(resp.Data[0].IdProducto);
                            //        if (ProductosPadre != null)
                            //        {
                            //            if (ProductosPadre.Data.Count > 0)
                            //            {
                            //                foreach (var item in ProductosPadre.Data)
                            //                {
                            //                    CantidadEquivalenteVentaTemp = 0;

                            //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                            //                    {
                            //                        if (Convert.ToInt32(item.IdProductoPadre) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                            //                        {
                            //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                            //                            decimal CantidadEquivalenteVenta = CantidadParaVenta * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                            //                            if (CantidadConversionTemp > 0)
                            //                            {
                            //                                CantidadEquivalenteVenta *= CantidadConversionTemp;
                            //                            }

                            //                            Stock -= CantidadEquivalenteVenta;

                            //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                            //                        }
                            //                    }

                            //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                            //                }
                            //            }
                            //        }

                            //        CantidadEquivalenteVentaTemp = 0;
                            //        CantidadConversionTemp = 0;

                            //        //Identificar si tiene productos hijo en lista y restas del stock
                            //        var ProductosHijo = await _IVenta.IdentificaProductoHijoNivel(resp.Data[0].IdProducto);
                            //        if (ProductosHijo != null)
                            //        {
                            //            if (ProductosHijo.Data.Count > 0)
                            //            {
                            //                foreach (var item in ProductosHijo.Data)
                            //                {
                            //                    CantidadEquivalenteVentaTemp = 0;

                            //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                            //                    {
                            //                        if (Convert.ToInt32(item.IdProductoHijo) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                            //                        {
                            //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                            //                            decimal CantidadEquivalenteVenta = CantidadParaVenta / Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                            //                            if (CantidadConversionTemp > 0)
                            //                            {
                            //                                CantidadEquivalenteVenta /= CantidadConversionTemp;
                            //                            }

                            //                            Stock -= CantidadEquivalenteVenta;

                            //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                            //                        }
                            //                    }

                            //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                            //                }
                            //            }
                            //        }

                            //        decimal CantParaVenta = 0;

                            //        //Identificar si hay mas productos del mismo en lista y restar del stock
                            //        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                            //        {
                            //            if (Convert.ToInt32(resp.Data[0].IdProducto) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                            //            {
                            //                CantParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));

                            //                Stock -= CantParaVenta;
                            //            }
                            //        }

                            //        if (Stock >= (StockInicial > 0 && StockInicial < 1 ? StockInicial : 1))
                            //        {
                            //            resp.Data[0].CantidadF = StockInicial > 0 && StockInicial < 1 ? StockInicial : 1;

                            //            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                            //            if (respG != null)
                            //            {
                            //                if (respG.Flag == true)
                            //                {
                            //                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                            //                    dtg_prd_individual.Rows.Clear();

                            //                    if (respData.Data != null)
                            //                    {
                            //                        if (respData.Data.Count > 0)
                            //                        {
                            //                            foreach (var item in respData.Data)
                            //                            {
                            //                                dtg_prd_individual.Rows.Add(
                            //                                    item.IdProductoIndividual,
                            //                                    item.Sku,
                            //                                    item.CodigoBarras,
                            //                                    item.Nombre,
                            //                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                            //                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                            //                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                            //                            }

                            //                            if (dtg_prd_individual.Rows.Count > 0)
                            //                            {
                            //                                decimal totalCantidad = 0;
                            //                                decimal totalCostos = 0;
                            //                                decimal totalPrecios = 0;

                            //                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                            //                                {
                            //                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                            //                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                            //                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                            //                                    totalCostos += CostoUnitario * Cantidad;
                            //                                    totalPrecios += PrecioUnitario * Cantidad;
                            //                                    totalCantidad += Cantidad;
                            //                                }

                            //                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                            //                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                            //                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                            //                            }
                            //                        }
                            //                    }

                            //                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                }
                            //                else
                            //                {
                            //                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //                }
                            //            }
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                            //        if (respG != null)
                            //        {
                            //            if (respG.Flag == true)
                            //            {
                            //                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                            //                dtg_prd_individual.Rows.Clear();

                            //                if (respData.Data != null)
                            //                {
                            //                    if (respData.Data.Count > 0)
                            //                    {
                            //                        foreach (var item in respData.Data)
                            //                        {
                            //                            dtg_prd_individual.Rows.Add(
                            //                                item.IdProductoIndividual,
                            //                                item.Sku,
                            //                                item.CodigoBarras,
                            //                                item.Nombre,
                            //                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                            //                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                            //                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                            //                        }

                            //                        if (dtg_prd_individual.Rows.Count > 0)
                            //                        {
                            //                            decimal totalCantidad = 0;
                            //                            decimal totalCostos = 0;
                            //                            decimal totalPrecios = 0;

                            //                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                            //                            {
                            //                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                            //                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                            //                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                            //                                totalCostos += CostoUnitario * Cantidad;
                            //                                totalPrecios += PrecioUnitario * Cantidad;
                            //                                totalCantidad += Cantidad;
                            //                            }

                            //                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                            //                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                            //                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                            //                        }
                            //                    }
                            //                }

                            //                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            }
                            //            else
                            //            {
                            //                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //            }
                            //        }
                            //    }
                            //}

                            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                            if (respG != null)
                            {
                                if (respG.Flag == true)
                                {
                                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                    dtg_prd_individual.Rows.Clear();

                                    if (respData.Data != null)
                                    {
                                        if (respData.Data.Count > 0)
                                        {
                                            foreach (var item in respData.Data)
                                            {
                                                dtg_prd_individual.Rows.Add(
                                                    item.IdProductoIndividual,
                                                    item.Sku,
                                                    item.CodigoBarras,
                                                    item.Nombre,
                                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                            }

                                            if (dtg_prd_individual.Rows.Count > 0)
                                            {
                                                decimal totalCantidad = 0;
                                                decimal totalCostos = 0;
                                                decimal totalPrecios = 0;

                                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                                {
                                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                                    totalCostos += CostoUnitario * Cantidad;
                                                    totalPrecios += PrecioUnitario * Cantidad;
                                                    totalCantidad += Cantidad;
                                                }

                                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                                            }
                                        }
                                    }

                                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El producto de tipo individual ya existe en la lista, por favor seleccione un producto diferente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto tipo grupo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void txt_buscar_producto_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();

            if (!string.IsNullOrEmpty(txt_producto_grupo.Text))
            {
                Id_producto_indiv = 0;

                if (cbx_busca_por.Text == "Id")
                {
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        //Enter
                        if (e.KeyChar == (char)13)
                        {
                            bool esEnteroValido = int.TryParse(txt_buscar_producto.Text, out int resultado);

                            if (esEnteroValido)
                            {
                                var DataProducto = await _IProducto.List(Convert.ToInt32(txt_buscar_producto.Text));

                                if (DataProducto.Data != null)
                                {
                                    if (DataProducto.Data.Count > 0)
                                    {
                                        int IdProduct = DataProducto.Data[0].IdProducto;

                                        Id_producto_indiv = IdProduct;

                                        var Exist = await _IProducto.ExistePrdIndiv(Id_producto_grupo, Id_producto_indiv);

                                        if (!Exist)
                                        {
                                            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                            if (respG != null)
                                            {
                                                if (respG.Flag == true)
                                                {
                                                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                                    dtg_prd_individual.Rows.Clear();

                                                    if (respData.Data != null)
                                                    {
                                                        if (respData.Data.Count > 0)
                                                        {
                                                            foreach (var item in respData.Data)
                                                            {
                                                                dtg_prd_individual.Rows.Add(
                                                                    item.IdProductoIndividual,
                                                                    item.Sku,
                                                                    item.CodigoBarras,
                                                                    item.Nombre,
                                                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                                            }

                                                            if (dtg_prd_individual.Rows.Count > 0)
                                                            {
                                                                decimal totalCantidad = 0;
                                                                decimal totalCostos = 0;
                                                                decimal totalPrecios = 0;

                                                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                                                {
                                                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                                                    totalCostos += CostoUnitario * Cantidad;
                                                                    totalPrecios += PrecioUnitario * Cantidad;
                                                                    totalCantidad += Cantidad;
                                                                }

                                                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                                                txt_buscar_producto.Text = "";
                                                            }
                                                        }
                                                    }

                                                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                {
                                                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                            }

                                            //var DataParametros = await _IParametros.List("Validar stock para venta");

                                            //if (DataParametros.Data != null)
                                            //{
                                            //    string ValidaStock = DataParametros.Data[0].Value;
                                            //    if (ValidaStock == "SI")
                                            //    {
                                            //        decimal CantidadEquivalenteVentaTemp = 0;
                                            //        decimal CantidadConversionTemp = 0;
                                            //        decimal Stock = DataProducto.Data[0].Stock;
                                            //        decimal StockInicial = DataProducto.Data[0].Stock;

                                            //        //Identificar si tiene productos padre en listay restar del stock
                                            //        var ProductosPadre = await _IVenta.IdentificaProductoPadreNivel1(DataProducto.Data[0].IdProducto);
                                            //        if (ProductosPadre != null)
                                            //        {
                                            //            if (ProductosPadre.Data.Count > 0)
                                            //            {
                                            //                foreach (var item in ProductosPadre.Data)
                                            //                {
                                            //                    CantidadEquivalenteVentaTemp = 0;

                                            //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                            //                    {
                                            //                        if (Convert.ToInt32(item.IdProductoPadre) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                            //                        {
                                            //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                            //                            decimal CantidadEquivalenteVenta = CantidadParaVenta * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                            //                            if (CantidadConversionTemp > 0)
                                            //                            {
                                            //                                CantidadEquivalenteVenta *= CantidadConversionTemp;
                                            //                            }

                                            //                            Stock -= CantidadEquivalenteVenta;

                                            //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                            //                        }
                                            //                    }

                                            //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                            //                }
                                            //            }
                                            //        }

                                            //        CantidadEquivalenteVentaTemp = 0;
                                            //        CantidadConversionTemp = 0;

                                            //        //Identificar si tiene productos hijo en lista y restas del stock
                                            //        var ProductosHijo = await _IVenta.IdentificaProductoHijoNivel(DataProducto.Data[0].IdProducto);
                                            //        if (ProductosHijo != null)
                                            //        {
                                            //            if (ProductosHijo.Data.Count > 0)
                                            //            {
                                            //                foreach (var item in ProductosHijo.Data)
                                            //                {
                                            //                    CantidadEquivalenteVentaTemp = 0;

                                            //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                            //                    {
                                            //                        if (Convert.ToInt32(item.IdProductoHijo) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                            //                        {
                                            //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                            //                            decimal CantidadEquivalenteVenta = CantidadParaVenta / Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                            //                            if (CantidadConversionTemp > 0)
                                            //                            {
                                            //                                CantidadEquivalenteVenta /= CantidadConversionTemp;
                                            //                            }

                                            //                            Stock -= CantidadEquivalenteVenta;

                                            //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                            //                        }
                                            //                    }

                                            //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                            //                }
                                            //            }
                                            //        }

                                            //        decimal CantParaVenta = 0;

                                            //        //Identificar si hay mas productos del mismo en lista y restar del stock
                                            //        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                            //        {
                                            //            if (Convert.ToInt32(DataProducto.Data[0].IdProducto) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                            //            {
                                            //                CantParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));

                                            //                Stock -= CantParaVenta;
                                            //            }
                                            //        }

                                            //        if (Stock >= (StockInicial > 0 && StockInicial < 1 ? StockInicial : 1))
                                            //        {
                                            //            DataProducto.Data[0].CantidadF = StockInicial > 0 && StockInicial < 1 ? StockInicial : 1;

                                            //            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                            //            if (respG != null)
                                            //            {
                                            //                if (respG.Flag == true)
                                            //                {
                                            //                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                            //                    dtg_prd_individual.Rows.Clear();

                                            //                    if (respData.Data != null)
                                            //                    {
                                            //                        if (respData.Data.Count > 0)
                                            //                        {
                                            //                            foreach (var item in respData.Data)
                                            //                            {
                                            //                                dtg_prd_individual.Rows.Add(
                                            //                                    item.IdProductoIndividual,
                                            //                                    item.Sku,
                                            //                                    item.CodigoBarras,
                                            //                                    item.Nombre,
                                            //                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                            //                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                            //                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                            //                            }

                                            //                            if (dtg_prd_individual.Rows.Count > 0)
                                            //                            {
                                            //                                decimal totalCantidad = 0;
                                            //                                decimal totalCostos = 0;
                                            //                                decimal totalPrecios = 0;

                                            //                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                            //                                {
                                            //                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                            //                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                            //                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                            //                                    totalCostos += CostoUnitario * Cantidad;
                                            //                                    totalPrecios += PrecioUnitario * Cantidad;
                                            //                                    totalCantidad += Cantidad;
                                            //                                }

                                            //                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                            //                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                            //                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                            //                                txt_buscar_producto.Text = "";
                                            //                            }
                                            //                        }
                                            //                    }

                                            //                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //                }
                                            //                else
                                            //                {
                                            //                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //                }
                                            //            }
                                            //        }
                                            //        else
                                            //        {
                                            //            MessageBox.Show("Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                            //        if (respG != null)
                                            //        {
                                            //            if (respG.Flag == true)
                                            //            {
                                            //                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                            //                dtg_prd_individual.Rows.Clear();

                                            //                if (respData.Data != null)
                                            //                {
                                            //                    if (respData.Data.Count > 0)
                                            //                    {
                                            //                        foreach (var item in respData.Data)
                                            //                        {
                                            //                            dtg_prd_individual.Rows.Add(
                                            //                                item.IdProductoIndividual,
                                            //                                item.Sku,
                                            //                                item.CodigoBarras,
                                            //                                item.Nombre,
                                            //                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                            //                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                            //                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                            //                        }

                                            //                        if (dtg_prd_individual.Rows.Count > 0)
                                            //                        {
                                            //                            decimal totalCantidad = 0;
                                            //                            decimal totalCostos = 0;
                                            //                            decimal totalPrecios = 0;

                                            //                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                            //                            {
                                            //                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                            //                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                            //                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                            //                                totalCostos += CostoUnitario * Cantidad;
                                            //                                totalPrecios += PrecioUnitario * Cantidad;
                                            //                                totalCantidad += Cantidad;
                                            //                            }

                                            //                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                            //                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                            //                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                            //                            txt_buscar_producto.Text = "";
                                            //                        }
                                            //                    }
                                            //                }

                                            //                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //            }
                                            //            else
                                            //            {
                                            //                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //            }
                                            //        }
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            MessageBox.Show("El producto de tipo individual ya existe en la lista, por favor seleccione un producto diferente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                    }
                                }
                                else
                                {
                                    errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                }
                            }
                        }
                    }
                }
                else if (cbx_busca_por.Text == "Sku" || cbx_busca_por.Text == "Codigo barras")
                {
                    //Enter
                    if (e.KeyChar == (char)13)
                    {
                        if (cbx_busca_por.Text == "Sku")
                        {
                            var DataProducto = await _IProducto.ListSku(txt_buscar_producto.Text);

                            if (DataProducto.Data != null)
                            {
                                if (DataProducto.Data.Count > 0)
                                {
                                    int IdProduct = DataProducto.Data[0].IdProducto;

                                    Id_producto_indiv = IdProduct;

                                    var Exist = await _IProducto.ExistePrdIndiv(Id_producto_grupo, Id_producto_indiv);

                                    if (!Exist)
                                    {
                                        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        if (respG != null)
                                        {
                                            if (respG.Flag == true)
                                            {
                                                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                                dtg_prd_individual.Rows.Clear();

                                                if (respData.Data != null)
                                                {
                                                    if (respData.Data.Count > 0)
                                                    {
                                                        foreach (var item in respData.Data)
                                                        {
                                                            dtg_prd_individual.Rows.Add(
                                                                item.IdProductoIndividual,
                                                                item.Sku,
                                                                item.CodigoBarras,
                                                                item.Nombre,
                                                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                                        }

                                                        if (dtg_prd_individual.Rows.Count > 0)
                                                        {
                                                            decimal totalCantidad = 0;
                                                            decimal totalCostos = 0;
                                                            decimal totalPrecios = 0;

                                                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                                            {
                                                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                                                totalCostos += CostoUnitario * Cantidad;
                                                                totalPrecios += PrecioUnitario * Cantidad;
                                                                totalCantidad += Cantidad;
                                                            }

                                                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                                            txt_buscar_producto.Text = "";
                                                        }
                                                    }
                                                }

                                                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }

                                        //var DataParametros = await _IParametros.List("Validar stock para venta");

                                        //if (DataParametros.Data != null)
                                        //{
                                        //    string ValidaStock = DataParametros.Data[0].Value;
                                        //    if (ValidaStock == "SI")
                                        //    {
                                        //        decimal CantidadEquivalenteVentaTemp = 0;
                                        //        decimal CantidadConversionTemp = 0;
                                        //        decimal Stock = DataProducto.Data[0].Stock;
                                        //        decimal StockInicial = DataProducto.Data[0].Stock;

                                        //        //Identificar si tiene productos padre en listay restar del stock
                                        //        var ProductosPadre = await _IVenta.IdentificaProductoPadreNivel1(DataProducto.Data[0].IdProducto);
                                        //        if (ProductosPadre != null)
                                        //        {
                                        //            if (ProductosPadre.Data.Count > 0)
                                        //            {
                                        //                foreach (var item in ProductosPadre.Data)
                                        //                {
                                        //                    CantidadEquivalenteVentaTemp = 0;

                                        //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //                    {
                                        //                        if (Convert.ToInt32(item.IdProductoPadre) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //                        {
                                        //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                            decimal CantidadEquivalenteVenta = CantidadParaVenta * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                        //                            if (CantidadConversionTemp > 0)
                                        //                            {
                                        //                                CantidadEquivalenteVenta *= CantidadConversionTemp;
                                        //                            }

                                        //                            Stock -= CantidadEquivalenteVenta;

                                        //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                        //                        }
                                        //                    }

                                        //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        //                }
                                        //            }
                                        //        }

                                        //        CantidadEquivalenteVentaTemp = 0;
                                        //        CantidadConversionTemp = 0;

                                        //        //Identificar si tiene productos hijo en lista y restas del stock
                                        //        var ProductosHijo = await _IVenta.IdentificaProductoHijoNivel(DataProducto.Data[0].IdProducto);
                                        //        if (ProductosHijo != null)
                                        //        {
                                        //            if (ProductosHijo.Data.Count > 0)
                                        //            {
                                        //                foreach (var item in ProductosHijo.Data)
                                        //                {
                                        //                    CantidadEquivalenteVentaTemp = 0;

                                        //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //                    {
                                        //                        if (Convert.ToInt32(item.IdProductoHijo) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //                        {
                                        //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                            decimal CantidadEquivalenteVenta = CantidadParaVenta / Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                        //                            if (CantidadConversionTemp > 0)
                                        //                            {
                                        //                                CantidadEquivalenteVenta /= CantidadConversionTemp;
                                        //                            }

                                        //                            Stock -= CantidadEquivalenteVenta;

                                        //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                        //                        }
                                        //                    }

                                        //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        //                }
                                        //            }
                                        //        }

                                        //        decimal CantParaVenta = 0;

                                        //        //Identificar si hay mas productos del mismo en lista y restar del stock
                                        //        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //        {
                                        //            if (Convert.ToInt32(DataProducto.Data[0].IdProducto) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //            {
                                        //                CantParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));

                                        //                Stock -= CantParaVenta;
                                        //            }
                                        //        }

                                        //        if (Stock >= (StockInicial > 0 && StockInicial < 1 ? StockInicial : 1))
                                        //        {
                                        //            DataProducto.Data[0].CantidadF = StockInicial > 0 && StockInicial < 1 ? StockInicial : 1;

                                        //            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        //            if (respG != null)
                                        //            {
                                        //                if (respG.Flag == true)
                                        //                {
                                        //                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                        //                    dtg_prd_individual.Rows.Clear();

                                        //                    if (respData.Data != null)
                                        //                    {
                                        //                        if (respData.Data.Count > 0)
                                        //                        {
                                        //                            foreach (var item in respData.Data)
                                        //                            {
                                        //                                dtg_prd_individual.Rows.Add(
                                        //                                    item.IdProductoIndividual,
                                        //                                    item.Sku,
                                        //                                    item.CodigoBarras,
                                        //                                    item.Nombre,
                                        //                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                        //                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                        //                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                        //                            }

                                        //                            if (dtg_prd_individual.Rows.Count > 0)
                                        //                            {
                                        //                                decimal totalCantidad = 0;
                                        //                                decimal totalCostos = 0;
                                        //                                decimal totalPrecios = 0;

                                        //                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                        //                                {
                                        //                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                                    totalCostos += CostoUnitario * Cantidad;
                                        //                                    totalPrecios += PrecioUnitario * Cantidad;
                                        //                                    totalCantidad += Cantidad;
                                        //                                }

                                        //                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                        //                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                        //                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                        //                                txt_buscar_producto.Text = "";
                                        //                            }
                                        //                        }
                                        //                    }

                                        //                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //                }
                                        //                else
                                        //                {
                                        //                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            MessageBox.Show("Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        //        if (respG != null)
                                        //        {
                                        //            if (respG.Flag == true)
                                        //            {
                                        //                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                        //                dtg_prd_individual.Rows.Clear();

                                        //                if (respData.Data != null)
                                        //                {
                                        //                    if (respData.Data.Count > 0)
                                        //                    {
                                        //                        foreach (var item in respData.Data)
                                        //                        {
                                        //                            dtg_prd_individual.Rows.Add(
                                        //                                item.IdProductoIndividual,
                                        //                                item.Sku,
                                        //                                item.CodigoBarras,
                                        //                                item.Nombre,
                                        //                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                        //                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                        //                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                        //                        }

                                        //                        if (dtg_prd_individual.Rows.Count > 0)
                                        //                        {
                                        //                            decimal totalCantidad = 0;
                                        //                            decimal totalCostos = 0;
                                        //                            decimal totalPrecios = 0;

                                        //                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                        //                            {
                                        //                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                                totalCostos += CostoUnitario * Cantidad;
                                        //                                totalPrecios += PrecioUnitario * Cantidad;
                                        //                                totalCantidad += Cantidad;
                                        //                            }

                                        //                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                        //                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                        //                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                        //                            txt_buscar_producto.Text = "";
                                        //                        }
                                        //                    }
                                        //                }

                                        //                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //            }
                                        //            else
                                        //            {
                                        //                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        MessageBox.Show("El producto de tipo individual ya existe en la lista, por favor seleccione un producto diferente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                }
                            }
                            else
                            {
                                errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                            }
                        }
                        else if (cbx_busca_por.Text == "Codigo barras")
                        {
                            var DataProducto = await _IProducto.ListCodigoBarras(txt_buscar_producto.Text);

                            if (DataProducto.Data != null)
                            {
                                if (DataProducto.Data.Count > 0)
                                {
                                    int IdProduct = DataProducto.Data[0].IdProducto;

                                    Id_producto_indiv = IdProduct;

                                    var Exist = await _IProducto.ExistePrdIndiv(Id_producto_grupo, Id_producto_indiv);

                                    if (!Exist)
                                    {
                                        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        if (respG != null)
                                        {
                                            if (respG.Flag == true)
                                            {
                                                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                                dtg_prd_individual.Rows.Clear();

                                                if (respData.Data != null)
                                                {
                                                    if (respData.Data.Count > 0)
                                                    {
                                                        foreach (var item in respData.Data)
                                                        {
                                                            dtg_prd_individual.Rows.Add(
                                                                item.IdProductoIndividual,
                                                                item.Sku,
                                                                item.CodigoBarras,
                                                                item.Nombre,
                                                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                                        }

                                                        if (dtg_prd_individual.Rows.Count > 0)
                                                        {
                                                            decimal totalCantidad = 0;
                                                            decimal totalCostos = 0;
                                                            decimal totalPrecios = 0;

                                                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                                            {
                                                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                                                totalCostos += CostoUnitario * Cantidad;
                                                                totalPrecios += PrecioUnitario * Cantidad;
                                                                totalCantidad += Cantidad;
                                                            }

                                                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                                            txt_buscar_producto.Text = "";
                                                        }
                                                    }
                                                }

                                                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }

                                        //var DataParametros = await _IParametros.List("Validar stock para venta");

                                        //if (DataParametros.Data != null)
                                        //{
                                        //    string ValidaStock = DataParametros.Data[0].Value;
                                        //    if (ValidaStock == "SI")
                                        //    {
                                        //        decimal CantidadEquivalenteVentaTemp = 0;
                                        //        decimal CantidadConversionTemp = 0;
                                        //        decimal Stock = DataProducto.Data[0].Stock;
                                        //        decimal StockInicial = DataProducto.Data[0].Stock;

                                        //        //Identificar si tiene productos padre en listay restar del stock
                                        //        var ProductosPadre = await _IVenta.IdentificaProductoPadreNivel1(DataProducto.Data[0].IdProducto);
                                        //        if (ProductosPadre != null)
                                        //        {
                                        //            if (ProductosPadre.Data.Count > 0)
                                        //            {
                                        //                foreach (var item in ProductosPadre.Data)
                                        //                {
                                        //                    CantidadEquivalenteVentaTemp = 0;

                                        //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //                    {
                                        //                        if (Convert.ToInt32(item.IdProductoPadre) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //                        {
                                        //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                            decimal CantidadEquivalenteVenta = CantidadParaVenta * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                        //                            if (CantidadConversionTemp > 0)
                                        //                            {
                                        //                                CantidadEquivalenteVenta *= CantidadConversionTemp;
                                        //                            }

                                        //                            Stock -= CantidadEquivalenteVenta;

                                        //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                        //                        }
                                        //                    }

                                        //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        //                }
                                        //            }
                                        //        }

                                        //        CantidadEquivalenteVentaTemp = 0;
                                        //        CantidadConversionTemp = 0;

                                        //        //Identificar si tiene productos hijo en lista y restas del stock
                                        //        var ProductosHijo = await _IVenta.IdentificaProductoHijoNivel(DataProducto.Data[0].IdProducto);
                                        //        if (ProductosHijo != null)
                                        //        {
                                        //            if (ProductosHijo.Data.Count > 0)
                                        //            {
                                        //                foreach (var item in ProductosHijo.Data)
                                        //                {
                                        //                    CantidadEquivalenteVentaTemp = 0;

                                        //                    foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //                    {
                                        //                        if (Convert.ToInt32(item.IdProductoHijo) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //                        {
                                        //                            decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                            decimal CantidadEquivalenteVenta = CantidadParaVenta / Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                                        //                            if (CantidadConversionTemp > 0)
                                        //                            {
                                        //                                CantidadEquivalenteVenta /= CantidadConversionTemp;
                                        //                            }

                                        //                            Stock -= CantidadEquivalenteVenta;

                                        //                            CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                                        //                        }
                                        //                    }

                                        //                    CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                                        //                }
                                        //            }
                                        //        }

                                        //        decimal CantParaVenta = 0;

                                        //        //Identificar si hay mas productos del mismo en lista y restar del stock
                                        //        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                                        //        {
                                        //            if (Convert.ToInt32(DataProducto.Data[0].IdProducto) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                                        //            {
                                        //                CantParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));

                                        //                Stock -= CantParaVenta;
                                        //            }
                                        //        }

                                        //        if (Stock >= (StockInicial > 0 && StockInicial < 1 ? StockInicial : 1))
                                        //        {
                                        //            DataProducto.Data[0].CantidadF = StockInicial > 0 && StockInicial < 1 ? StockInicial : 1;

                                        //            var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        //            if (respG != null)
                                        //            {
                                        //                if (respG.Flag == true)
                                        //                {
                                        //                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                        //                    dtg_prd_individual.Rows.Clear();

                                        //                    if (respData.Data != null)
                                        //                    {
                                        //                        if (respData.Data.Count > 0)
                                        //                        {
                                        //                            foreach (var item in respData.Data)
                                        //                            {
                                        //                                dtg_prd_individual.Rows.Add(
                                        //                                    item.IdProductoIndividual,
                                        //                                    item.Sku,
                                        //                                    item.CodigoBarras,
                                        //                                    item.Nombre,
                                        //                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                        //                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                        //                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                        //                            }

                                        //                            if (dtg_prd_individual.Rows.Count > 0)
                                        //                            {
                                        //                                decimal totalCantidad = 0;
                                        //                                decimal totalCostos = 0;
                                        //                                decimal totalPrecios = 0;

                                        //                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                        //                                {
                                        //                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                                    totalCostos += CostoUnitario * Cantidad;
                                        //                                    totalPrecios += PrecioUnitario * Cantidad;
                                        //                                    totalCantidad += Cantidad;
                                        //                                }

                                        //                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                        //                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                        //                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                        //                                txt_buscar_producto.Text = "";
                                        //                            }
                                        //                        }
                                        //                    }

                                        //                    //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //                }
                                        //                else
                                        //                {
                                        //                    MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            MessageBox.Show("Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        var respG = await _IProducto.AgregaProdIndiviToProdGrupo(Id_producto_grupo, Id_producto_indiv, 1, Convert.ToInt32(_Permisos?[0]?.IdUser));

                                        //        if (respG != null)
                                        //        {
                                        //            if (respG.Flag == true)
                                        //            {
                                        //                var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                        //                dtg_prd_individual.Rows.Clear();

                                        //                if (respData.Data != null)
                                        //                {
                                        //                    if (respData.Data.Count > 0)
                                        //                    {
                                        //                        foreach (var item in respData.Data)
                                        //                        {
                                        //                            dtg_prd_individual.Rows.Add(
                                        //                                item.IdProductoIndividual,
                                        //                                item.Sku,
                                        //                                item.CodigoBarras,
                                        //                                item.Nombre,
                                        //                                item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                        //                                item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                        //                                item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                        //                        }

                                        //                        if (dtg_prd_individual.Rows.Count > 0)
                                        //                        {
                                        //                            decimal totalCantidad = 0;
                                        //                            decimal totalCostos = 0;
                                        //                            decimal totalPrecios = 0;

                                        //                            foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                        //                            {
                                        //                                decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                        //                                decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                        //                                totalCostos += CostoUnitario * Cantidad;
                                        //                                totalPrecios += PrecioUnitario * Cantidad;
                                        //                                totalCantidad += Cantidad;
                                        //                            }

                                        //                            lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                        //                            lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                        //                            lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));

                                        //                            txt_buscar_producto.Text = "";
                                        //                        }
                                        //                    }
                                        //                }

                                        //                //MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //            }
                                        //            else
                                        //            {
                                        //                MessageBox.Show(respG.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        MessageBox.Show("El producto de tipo individual ya existe en la lista, por favor seleccione un producto diferente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                                }
                            }
                            else
                            {
                                errorProvider1.SetError(txt_buscar_producto, $"{cbx_busca_por.Text} no encontrado");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto tipo grupo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_quitar1_Click(object sender, EventArgs e)
        {
            if (dtg_prd_individual.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea quitar el producto seleccionado del listado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_prd_individual.SelectedRows.Count > 0)
                    {
                        var row = dtg_prd_individual.SelectedRows[0];
                        if (row.Cells["cl_id_producto"].Value != null)
                        {
                            Id_producto_indiv = Convert.ToInt32(row.Cells["cl_id_producto"].Value);

                            var resp = await _IProducto.EliminarPrdIndvXPrdGrp(Id_producto_grupo, Id_producto_indiv);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    var respData = await _IProducto.BuscarXProdGrupo(Id_producto_grupo);

                                    dtg_prd_individual.Rows.Clear();

                                    if (respData.Data != null)
                                    {
                                        if (respData.Data.Count > 0)
                                        {
                                            foreach (var item in respData.Data)
                                            {
                                                dtg_prd_individual.Rows.Add(
                                                    item.IdProductoIndividual,
                                                    item.Sku,
                                                    item.CodigoBarras,
                                                    item.Nombre,
                                                    item.Cantidad.ToString("N2", new CultureInfo("es-CO")),
                                                    item.CostoBase.ToString("N2", new CultureInfo("es-CO")),
                                                    item.PrecioBase.ToString("N2", new CultureInfo("es-CO")));
                                            }

                                            txt_buscar_producto.Text = "";

                                            if (dtg_prd_individual.Rows.Count > 0)
                                            {
                                                decimal totalCantidad = 0;
                                                decimal totalCostos = 0;
                                                decimal totalPrecios = 0;

                                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                                {
                                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                                    totalCostos += CostoUnitario * Cantidad;
                                                    totalPrecios += PrecioUnitario * Cantidad;
                                                    totalCantidad += Cantidad;
                                                }

                                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
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
            else
            {
                MessageBox.Show("No hay datos para Quitar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtg_prd_individual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void dtg_prd_individual_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Remover eventos anteriores para evitar duplicados
            e.Control.KeyPress -= new KeyPressEventHandler(dtg_prd_individual_KeyPress);

            if (dtg_prd_individual.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dtg_prd_individual_KeyPress);
            }
        }

        private async void dtg_prd_individual_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var celda = dtg_prd_individual[e.ColumnIndex, e.RowIndex];

                if (celda.Value == null || string.IsNullOrWhiteSpace(celda.Value.ToString()) || Convert.ToDecimal(celda.Value, new CultureInfo("es-CO")) <= 0)
                {
                    celda.Value = CantidadInicial.ToString(new CultureInfo("es-CO"));
                }

                var celdaIdProducto = dtg_prd_individual[0, e.RowIndex];
                var CantidadPrdParaPaquete = dtg_prd_individual[4, e.RowIndex];

                var DataProducto = await _IProducto.List(Convert.ToInt32(celdaIdProducto.Value));
                if (DataProducto.Data != null)
                {
                    if (DataProducto.Data.Count > 0)
                    {
                        //var DataParametros = await _IParametros.List("Validar stock para venta");

                        //if (DataParametros.Data != null)
                        //{
                        //    if (DataParametros.Data.Count > 0)
                        //    {
                        //        string ValidaStock = DataParametros.Data[0].Value;
                        //        if (ValidaStock == "SI")
                        //        {
                        //            if (Convert.ToDecimal(celda.Value, new CultureInfo("es-CO")) <= 0)
                        //            {
                        //                celda.Value = CantidadInicial.ToString(new CultureInfo("es-CO"));
                        //            }

                        //            decimal CantidadEquivalenteVentaTemp = 0;
                        //            decimal CantidadConversionTemp = 0;
                        //            decimal Stock = DataProducto.Data[0].Stock;

                        //            //Identificar si tiene productos padre en listay restar del stock
                        //            var ProductosPadre = await _IVenta.IdentificaProductoPadreNivel1(DataProducto.Data[0].IdProducto);
                        //            if (ProductosPadre != null)
                        //            {
                        //                if (ProductosPadre.Data.Count > 0)
                        //                {
                        //                    foreach (var item in ProductosPadre.Data)
                        //                    {
                        //                        CantidadEquivalenteVentaTemp = 0;

                        //                        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                        //                        {
                        //                            if (Convert.ToInt32(item.IdProductoPadre) == Convert.ToInt32(row.Cells["cl_idProducto"].Value))
                        //                            {
                        //                                decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                        //                                decimal CantidadEquivalenteVenta = CantidadParaVenta * Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                        //                                if (CantidadConversionTemp > 0)
                        //                                {
                        //                                    CantidadEquivalenteVenta *= CantidadConversionTemp;
                        //                                }

                        //                                Stock -= CantidadEquivalenteVenta;

                        //                                CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                        //                            }
                        //                        }

                        //                        CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                        //                    }
                        //                }
                        //            }

                        //            CantidadEquivalenteVentaTemp = 0;
                        //            CantidadConversionTemp = 0;

                        //            //Identificar si tiene productos hijo en lista y restas del stock
                        //            var ProductosHijo = await _IVenta.IdentificaProductoHijoNivel(DataProducto.Data[0].IdProducto);
                        //            if (ProductosHijo != null)
                        //            {
                        //                if (ProductosHijo.Data.Count > 0)
                        //                {
                        //                    foreach (var item in ProductosHijo.Data)
                        //                    {
                        //                        CantidadEquivalenteVentaTemp = 0;

                        //                        foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                        //                        {
                        //                            if (Convert.ToInt32(item.IdProductoHijo) == Convert.ToInt32(row.Cells["cl_idProducto"].Value))
                        //                            {
                        //                                decimal CantidadParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                        //                                decimal CantidadEquivalenteVenta = CantidadParaVenta / Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));

                        //                                if (CantidadConversionTemp > 0)
                        //                                {
                        //                                    CantidadEquivalenteVenta /= CantidadConversionTemp;
                        //                                }

                        //                                Stock -= CantidadEquivalenteVenta;

                        //                                CantidadEquivalenteVentaTemp = CantidadEquivalenteVenta;
                        //                            }
                        //                        }

                        //                        CantidadConversionTemp = Convert.ToDecimal(item.Cantidad, new CultureInfo("es-CO"));
                        //                    }
                        //                }
                        //            }

                        //            decimal CantParaVenta = 0;

                        //            //Identificar si hay mas productos del mismo en lista y restar del stock
                        //            foreach (DataGridViewRow row in dtg_prd_individual.Rows)
                        //            {
                        //                if (Convert.ToInt32(DataProducto.Data[0].IdProducto) == Convert.ToInt32(row.Cells["cl_id_producto"].Value))
                        //                {
                        //                    CantParaVenta = Convert.ToDecimal(row.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));

                        //                    Stock -= CantParaVenta;
                        //                }
                        //            }

                        //            if (Stock >= 0)
                        //            {
                        //                var respUp = await _IProducto.UpdateCantidadProdIndiviToProdGrupo(Id_producto_grupo, Convert.ToInt32(celdaIdProducto.Value), Convert.ToDecimal(CantidadPrdParaPaquete.Value), Convert.ToInt32(_Permisos?[0]?.IdUser));

                        //                if (respUp != null)
                        //                {
                        //                    if (respUp.Flag == true)
                        //                    {
                        //                        decimal totalCantidad = 0;
                        //                        decimal totalCostos = 0;
                        //                        decimal totalPrecios = 0;

                        //                        foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                        //                        {
                        //                            decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                        //                            decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                        //                            decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                        //                            totalCostos += CostoUnitario * Cantidad;
                        //                            totalPrecios += PrecioUnitario * Cantidad;
                        //                            totalCantidad += Cantidad;
                        //                        }

                        //                        lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                        //                        lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                        //                        lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                MessageBox.Show("Producto sin Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        //                celda.Value = CantidadInicial.ToString(new CultureInfo("es-CO"));
                        //            }
                        //        }
                        //        else
                        //        {
                        //            var respUp = await _IProducto.UpdateCantidadProdIndiviToProdGrupo(Id_producto_grupo, Convert.ToInt32(celdaIdProducto.Value), Convert.ToDecimal(CantidadPrdParaPaquete.Value), Convert.ToInt32(_Permisos?[0]?.IdUser));

                        //            if (respUp != null)
                        //            {
                        //                if (respUp.Flag == true)
                        //                {
                        //                    decimal totalCantidad = 0;
                        //                    decimal totalCostos = 0;
                        //                    decimal totalPrecios = 0;

                        //                    foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                        //                    {
                        //                        decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                        //                        decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                        //                        decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                        //                        totalCostos += CostoUnitario * Cantidad;
                        //                        totalPrecios += PrecioUnitario * Cantidad;
                        //                        totalCantidad += Cantidad;
                        //                    }

                        //                    lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                        //                    lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                        //                    lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        var respUp = await _IProducto.UpdateCantidadProdIndiviToProdGrupo(Id_producto_grupo, Convert.ToInt32(celdaIdProducto.Value), Convert.ToDecimal(CantidadPrdParaPaquete.Value), Convert.ToInt32(_Permisos?[0]?.IdUser));

                        if (respUp != null)
                        {
                            if (respUp.Flag == true)
                            {
                                decimal totalCantidad = 0;
                                decimal totalCostos = 0;
                                decimal totalPrecios = 0;

                                foreach (DataGridViewRow fila in dtg_prd_individual.Rows)
                                {
                                    decimal CostoUnitario = Convert.ToDecimal(fila.Cells["cl_costo_unitario"].Value, new CultureInfo("es-CO"));
                                    decimal PrecioUnitario = Convert.ToDecimal(fila.Cells["cl_precio_unitario"].Value, new CultureInfo("es-CO"));
                                    decimal Cantidad = Convert.ToDecimal(fila.Cells["cl_cantidad"].Value, new CultureInfo("es-CO"));
                                    totalCostos += CostoUnitario * Cantidad;
                                    totalPrecios += PrecioUnitario * Cantidad;
                                    totalCantidad += Cantidad;
                                }

                                lbl_total_costos.Text = totalCostos.ToString("N2", new CultureInfo("es-CO"));
                                lbl_total_precios.Text = totalPrecios.ToString("N2", new CultureInfo("es-CO"));
                                lbl_total_cantidad.Text = totalCantidad.ToString("N2", new CultureInfo("es-CO"));
                            }
                        }
                    }
                }
            }
        }

        private void dtg_prd_individual_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var celda = dtg_prd_individual[e.ColumnIndex, e.RowIndex];

                CantidadInicial = Convert.ToDecimal(celda.Value, new CultureInfo("es-CO"));
            }
        }
    }
}
