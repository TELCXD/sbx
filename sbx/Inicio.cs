﻿using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Backup;
using sbx.core.Interfaces.Gastos;
using sbx.core.Interfaces.Parametros;
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
        private Login? _Login;
        private readonly IParametros _IParametros;
        private readonly IBackup _IBackup;
        private Vendedor? _Vendedor;
        private Dashboard? _Dashboard;
        private Gastos? _Gastos;
        private ReporteGeneral? _ReporteGeneral;

        public Inicio(IServiceProvider serviceProvider, ITienda iTienda, IParametros iParametros, IBackup backup)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ITienda = iTienda;
            _IParametros = iParametros;
            _IBackup = backup;
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
                if (_Permisos.Count > 0)
                {
                    lbl_usuario.Text += _Permisos[0].IdUser + " - " + _Permisos[0].UserName;

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
                            case "reportes":
                                if (item.ToRead == 1)
                                {
                                    btn_reportes.Visible = true;
                                    btn_reporte_general.Visible = true;
                                }
                                break;
                            case "vendedores":
                                if (item.ToRead == 1)
                                {
                                    btn_vendedor.Visible = true;
                                }
                                break;
                            case "Gastos":
                                if (item.ToRead == 1)
                                {
                                    btn_gastos.Visible = true;
                                }
                                break;
                            default:
                                break;
                        }
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

        private async void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                var DataParametros = await _IParametros.List("Ruta backup");

                if (DataParametros.Data != null)
                {
                    if (DataParametros.Data.Count > 0)
                    {
                        string Ruta_backup = DataParametros.Data[0].Value;
                        string NombreCopiaSeguridad = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + " sbx_development.bak";
                        var resp = await _IBackup.Create(Ruta_backup, NombreCopiaSeguridad);
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

                _Login = _serviceProvider.GetRequiredService<Login>();
                _Login.FormClosed += (s, args) => _Login = null;
                _Login.Show();
                this.Hide();
            }
            else
            {
                e.Cancel = true;
            }
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

        private async void btn_caja_Click(object sender, EventArgs e)
        {
            if (_Caja != null && !_Caja.IsDisposed)
            {
                _Caja.BringToFront();
                _Caja.WindowState = FormWindowState.Normal;
                return;
            }

            var DataTienda = await _ITienda.List();
            if (DataTienda.Data != null)
            {
                if (DataTienda.Data.Count > 0)
                {
                    _Caja = _serviceProvider.GetRequiredService<Caja>();
                    _Caja.Permisos = _Permisos;
                    _Caja.FormClosed += (s, args) => _Caja = null;
                    _Caja.Show();
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

        private void btn_reportes_Click(object sender, EventArgs e)
        {
            if (_Dashboard != null && !_Dashboard.IsDisposed)
            {
                _Dashboard.BringToFront();
                _Dashboard.WindowState = FormWindowState.Normal;
                return;
            }

            _Dashboard = _serviceProvider.GetRequiredService<Dashboard>();
            _Dashboard.Permisos = _Permisos;
            _Dashboard.FormClosed += (s, args) => _Dashboard = null;
            _Dashboard.Show();
        }

        private void btn_vendedor_Click(object sender, EventArgs e)
        {
            if (_Vendedor != null && !_Vendedor.IsDisposed)
            {
                _Vendedor.BringToFront();
                _Vendedor.WindowState = FormWindowState.Normal;
                return;
            }

            _Vendedor = _serviceProvider.GetRequiredService<Vendedor>();
            _Vendedor.Permisos = _Permisos;
            _Vendedor.FormClosed += (s, args) => _Vendedor = null;
            _Vendedor.Show();
        }

        private void btn_gastos_Click(object sender, EventArgs e)
        {
            if (_Gastos != null && !_Gastos.IsDisposed)
            {
                _Gastos.BringToFront();
                _Gastos.WindowState = FormWindowState.Normal;
                return;
            }

            _Gastos = _serviceProvider.GetRequiredService<Gastos>();
            _Gastos.Permisos = _Permisos;
            _Gastos.FormClosed += (s, args) => _Gastos = null;
            _Gastos.Show();
        }

        private void btn_reporte_general_Click(object sender, EventArgs e)
        {
            if (_ReporteGeneral != null && !_ReporteGeneral.IsDisposed)
            {
                _ReporteGeneral.BringToFront();
                _ReporteGeneral.WindowState = FormWindowState.Normal;
                return;
            }

            _ReporteGeneral = _serviceProvider.GetRequiredService<ReporteGeneral>();
            _ReporteGeneral.Permisos = _Permisos;
            _ReporteGeneral.FormClosed += (s, args) => _ReporteGeneral = null;
            _ReporteGeneral.Show();
        }
    }
}
