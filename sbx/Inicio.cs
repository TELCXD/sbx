using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Tienda;

namespace sbx
{
    public partial class Inicio : Form
    {
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITienda _ITienda;
        private Tienda? _Tienda;
        private Ajustes? _Ajustes;
        private Productos? _Productos;
        private Proveedores? _Proveedores;
        private Clientes? _Clientes;
        private Inventario? _Inventario;
        private AgregarVentas? _AgregarVentas;
        private Caja? _Caja;

        public Inicio(IServiceProvider serviceProvider, ITienda iTienda)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ITienda = iTienda;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
        }

        private void btn_ajustes_Click(object sender, EventArgs e)
        {
            if (_Ajustes != null && !_Ajustes.IsDisposed)
            {
                _Ajustes.BringToFront();
                _Ajustes.WindowState = FormWindowState.Normal;
                return;
            }

            _Ajustes = _serviceProvider.GetRequiredService<Ajustes>();
            _Ajustes.Permisos = _Permisos;
            _Ajustes.FormClosed += (s, args) => _Ajustes = null;
            _Ajustes.Show();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                lbl_usuario.Text += _Permisos[0].UserName;

                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "ventas":
                            if (item.ToRead == 1)
                            {
                                btn_venta.Visible = true;
                            }
                            break;
                        case "ajustes":
                            if (item.ToRead == 1)
                            {
                                btn_ajustes.Visible = true;
                            }
                            break;
                        case "tienda":
                            if (item.ToRead == 1)
                            {
                                btn_tienda.Visible = true;
                            }
                            break;
                        case "productos":
                            if (item.ToRead == 1)
                            {
                                btn_producto.Visible = true;
                            }
                            break;
                        case "proveedor":
                            if (item.ToRead == 1)
                            {
                                btn_proveedor.Visible = true;
                            }
                            break;
                        case "clientes":
                            if (item.ToRead == 1)
                            {
                                btn_cliente.Visible = true;
                            }
                            break;
                        case "inventario":
                            if (item.ToRead == 1)
                            {
                                btn_inventario.Visible = true;
                            }
                            break;
                        case "caja":
                            if (item.ToRead == 1)
                            {
                                btn_caja.Visible = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void btn_tienda_Click(object sender, EventArgs e)
        {
            if (_Tienda != null && !_Tienda.IsDisposed)
            {
                _Tienda.BringToFront();
                _Tienda.WindowState = FormWindowState.Normal;
                return;
            }

            _Tienda = _serviceProvider.GetRequiredService<Tienda>();
            _Tienda.Permisos = _Permisos;
            _Tienda.FormClosed += (s, args) => _Tienda = null;
            _Tienda.Show();
        }

        private void btn_producto_Click(object sender, EventArgs e)
        {
            if (_Productos != null && !_Productos.IsDisposed)
            {
                _Productos.BringToFront();
                _Productos.WindowState = FormWindowState.Normal;
                return;
            }

            _Productos = _serviceProvider.GetRequiredService<Productos>();
            _Productos.Permisos = _Permisos;
            _Productos.FormClosed += (s, args) => _Productos = null;
            _Productos.Show();
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btn_proveedor_Click(object sender, EventArgs e)
        {
            if (_Proveedores != null && !_Proveedores.IsDisposed)
            {
                _Proveedores.BringToFront();
                _Proveedores.WindowState = FormWindowState.Normal;
                return;
            }

            _Proveedores = _serviceProvider.GetRequiredService<Proveedores>();
            _Proveedores.Permisos = _Permisos;
            _Proveedores.FormClosed += (s, args) => _Proveedores = null;
            _Proveedores.Show();
        }

        private void btn_cliente_Click(object sender, EventArgs e)
        {
            if (_Clientes != null && !_Clientes.IsDisposed)
            {
                _Clientes.BringToFront();
                _Clientes.WindowState = FormWindowState.Normal;
                return;
            }

            _Clientes = _serviceProvider.GetRequiredService<Clientes>();
            _Clientes.Permisos = _Permisos;
            _Clientes.FormClosed += (s, args) => _Clientes = null;
            _Clientes.Show();
        }

        private void btn_inventario_Click(object sender, EventArgs e)
        {
            if (_Inventario != null && !_Inventario.IsDisposed)
            {
                _Inventario.BringToFront();
                _Inventario.WindowState = FormWindowState.Normal;
                return;
            }

            _Inventario = _serviceProvider.GetRequiredService<Inventario>();
            _Inventario.Permisos = _Permisos;
            _Inventario.FormClosed += (s, args) => _Inventario = null;
            _Inventario.Show();
        }

        private async void btn_venta_Click(object sender, EventArgs e)
        {
            if (_AgregarVentas != null && !_AgregarVentas.IsDisposed)
            {
                _AgregarVentas.BringToFront();
                _AgregarVentas.WindowState = FormWindowState.Normal;
                return;
            }

            var DataTienda = await _ITienda.List();
            if (DataTienda.Data != null)
            {
                if (DataTienda.Data.Count > 0)
                {
                    _AgregarVentas = _serviceProvider.GetRequiredService<AgregarVentas>();
                    _AgregarVentas.Permisos = _Permisos;
                    _AgregarVentas.FormClosed += (s, args) => _Clientes = null;
                    _AgregarVentas.Show();
                }
                else
                {
                    MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se encuentra informacion de Tienda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_caja_Click(object sender, EventArgs e)
        {
            if (_Caja != null && !_Caja.IsDisposed)
            {
                _Caja.BringToFront();
                _Caja.WindowState = FormWindowState.Normal;
                return;
            }

            _Caja = _serviceProvider.GetRequiredService<Caja>();
            _Caja.Permisos = _Permisos;
            _Caja.FormClosed += (s, args) => _Caja = null;
            _Caja.Show();
        }
    }
}
