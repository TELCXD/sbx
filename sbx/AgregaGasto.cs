using sbx.core.Entities.Gasto;
using sbx.core.Interfaces.Categoria;
using sbx.core.Interfaces.Gastos;
using sbx.core.Interfaces.Marca;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.UnidadMedida;
using System.Globalization;

namespace sbx
{
    public partial class AgregaGasto : Form
    {
        private readonly IGastos _IGastos;
        char decimalSeparator = ',';
        private int _Id_Gasto;
        private dynamic? _Permisos;

        public AgregaGasto(IGastos gastos)
        {
            InitializeComponent();
            _IGastos = gastos;
        }

        public int Id_Gasto
        {
            get => _Id_Gasto;
            set => _Id_Gasto = value;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void AgregaGasto_Load(object sender, EventArgs e)
        {
            await CargaDatosIniciales();
            ValidaPermisos();
            cbx_categoria.SelectedIndex = 0;
        }

        private async Task CargaDatosIniciales()
        {          
            if (Id_Gasto > 0)
            {
                var resp = await _IGastos.List(Id_Gasto);

                if (resp.Data != null)
                {
                    cbx_categoria.Text = resp.Data[0].Categoria.ToString();
                    cbx_subcategoria.Text = resp.Data[0].Subcategoria.ToString();
                    cbx_detalle.Text = resp.Data[0].Detalle.ToString();
                    txt_valor.Text = resp.Data[0].ValorGasto.ToString("N2", new CultureInfo("es-CO"));                
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                        case "Gastos":
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

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_valor.Text)) { errorProvider1.SetError(txt_valor, "Ingrese un valor numerico"); valido++; }

            if (valido > 0) { return; } else { valido = 0; }

            if (Convert.ToDecimal(txt_valor.Text, new CultureInfo("es-CO")) <= 0) { errorProvider1.SetError(txt_valor, "El valor debe ser mayor a cero"); valido++; }

            if (valido > 0) { return; }

            var Datos = new GastoEntitie
            {
                IdGasto = Id_Gasto,
                Categoria = cbx_categoria.Text.Trim(),
                Subcategoria = cbx_subcategoria.Text.Trim(),
                Detalle = cbx_detalle.Text.Trim(),
                ValorGasto = Convert.ToDecimal(txt_valor.Text, new CultureInfo("es-CO"))
            };

            var resp = await _IGastos.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

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

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void cbx_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> opciones = new List<string>();

            switch (cbx_categoria.Text)
            {
                case "Gastos operativos":
                    opciones = new List<string> { "Servicios públicos", "Alquiler o hipoteca", "Mantenimiento y reparaciones" };
                    break;
                case "Gastos de personal":
                    opciones = new List<string> { "Salarios", "Prestaciones sociales", "Bonificaciones", "Capacitación" };
                    break;
                case "Gastos administrativos":
                    opciones = new List<string> { "Papelería", "Software o licencias", "Honorarios" };
                    break;
                case "Gastos de ventas y marketing":
                    opciones = new List<string> { "Publicidad", "Diseño gráfico", "Obsequios" };
                    break;
                case "Gastos financieros":
                    opciones = new List<string> { "Intereses bancarios", "Comisiones", "Costos de transferencias" };
                    break;
                case "Gastos misceláneos":
                    opciones = new List<string> { "Gastos menores no recurrentes" };
                    break;
                default:
                    break;
            }

            cbx_subcategoria.DataSource = opciones;
            cbx_subcategoria.SelectedIndex = 0;
        }

        private void cbx_subcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> opciones = new List<string>();

            switch (cbx_subcategoria.Text)
            {
                case "Servicios públicos":
                    opciones = new List<string> { "Agua", "Luz", "Internet", "Teléfono" };
                    break;
                case "Alquiler o hipoteca":
                    opciones = new List<string> { "Local", "Oficina", "Bodega" };
                    break;
                case "Mantenimiento y reparaciones":
                    opciones = new List<string> { "Equipos", "Local","Oficina","Bodega", "Software", };
                    break;
                case "Salarios":
                    opciones = new List<string> { "Salarios"};
                    break;
                case "Prestaciones sociales":
                    opciones = new List<string> { "Seguridad social", "Salud", "Pensión", "Primas", "Cesantías" };
                    break;
                case "Bonificaciones":
                    opciones = new List<string> { "Bonificaciones"};
                    break;
                case "Capacitación":
                    opciones = new List<string> { "Capacitación" };
                    break;
                case "Papelería":
                    opciones = new List<string> { "Papelería"};
                    break;
                case "Software o licencias":
                    opciones = new List<string> { "Antivirus", "Office","POS" };
                    break;
                case "Honorarios":
                    opciones = new List<string> { "Contador", "Asesorías" };
                    break;
                case "Publicidad":
                    opciones = new List<string> { "Redes sociales", "Google Ads", "Volantes" };
                    break;
                case "Diseño gráfico":
                    opciones = new List<string> { "Diseño gráfico", "Creación de contenido" };
                    break;
                case "Obsequios":
                    opciones = new List<string> { "Clientes", "Proveedores" };
                    break;
                case "Intereses bancarios":
                    opciones = new List<string> { "Intereses bancarios" };
                    break;
                case "Comisiones":
                    opciones = new List<string> { "Comisiones" };
                    break;
                case "Costos de transferencias":
                    opciones = new List<string> { "Costos de transferencias" };
                    break;
                case "Gastos menores no recurrentes":
                    opciones = new List<string> { "Eventos", "Regalos", "Imprevistos" };
                    break;
                default:
                    break;
            }

            cbx_detalle.DataSource = opciones;
            cbx_detalle.SelectedIndex = 0;
        }
    }
}
