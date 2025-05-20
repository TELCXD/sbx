﻿using sbx.core.Entities.Producto;
using sbx.core.Interfaces.Categoria;
using sbx.core.Interfaces.Marca;
using sbx.core.Interfaces.Producto;
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

        public AgregarProducto(ICategoria categoria, IMarca marca, IUnidadMedida unidadMedida, IProducto producto)
        {
            InitializeComponent();

            _ICategoria = categoria;
            _IMarca = marca;
            _IUnidadMedida = unidadMedida;
            _IProducto = producto;
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

            cbx_es_inventariable.SelectedIndex = 0;

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
                    cbx_es_inventariable.SelectedIndex = resp.Data[0].EsInventariable == true ? 0 : 1;
                    txt_iva.Text = resp.Data[0].Iva.ToString().Replace('.', ',');
                    cbx_categoria.SelectedValue = resp.Data[0].IdCategoria;
                    cbx_marca.SelectedValue = resp.Data[0].IdMarca;
                    cbx_unidad_medida.SelectedValue = resp.Data[0].IdUnidadMedida;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_costo.Text)) { errorProvider1.SetError(txt_costo, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_precio.Text)) { errorProvider1.SetError(txt_precio, "Ingrese un valor numerico"); valido++; }
            if (string.IsNullOrWhiteSpace(txt_iva.Text)) { errorProvider1.SetError(txt_iva, "Ingrese un valor numerico"); valido++; }

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
                Iva = Convert.ToDecimal(txt_iva.Text.Replace(',', '.')),
                IdCategoria = Convert.ToInt32(cbx_categoria.SelectedValue),
                IdMarca = Convert.ToInt32(cbx_marca.SelectedValue),
                IdUnidadMedida = Convert.ToInt32(cbx_unidad_medida.SelectedValue),
                EsInventariable = cbx_es_inventariable.Text == "SI" ? 1 : 0
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
                Exist = await _IProducto.ExisteNombre(txt_nombre.Text.Trim(), Id_Producto);
                if (Exist) { errorProvider1.SetError(txt_nombre, "Nombre ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _IProducto.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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

                    if (validationResult.MemberNames.Contains("Iva"))
                    {
                        errorProvider1.SetError(txt_iva, validationResult.ErrorMessage);
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
            if (!decimal.TryParse(txt_iva.Text, out _))
            {
                errorProvider1.SetError(txt_iva, "Ingrese un valor numerico");
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
    }
}
