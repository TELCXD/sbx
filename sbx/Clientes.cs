using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Cliente;

namespace sbx
{
    public partial class Clientes : Form
    {
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICliente _ICliente;
        private AgregarCliente? _AgregarCliente;
        private int Id_Cliente = 0;
        private PreciosClientes _PreciosClientes;

        public Clientes(IServiceProvider serviceProvider, ICliente cliente)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ICliente = cliente;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "clientes":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            //btn_eliminar.Enabled = item.ToUpdate == 1 ? true : false;
                            break;
                        case "preciosClientes":
                            btn_mejor_precio.Enabled = item.ToCreate == 1 ? true : false;
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaCliente();
        }

        private async Task ConsultaCliente()
        {
            errorProvider1.Clear();

            var resp = await _ICliente.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_cliente.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_cliente.Rows.Clear();
                    foreach (var item in resp.Data)
                    {
                        dtg_cliente.Rows.Add(
                            item.IdCliente,
                            item.IdentificationType,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.TipoCliente,
                            item.Direccion,
                            item.Telefono,
                            item.Email,
                            item.Estado);
                    }
                }
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarCliente = _serviceProvider.GetRequiredService<AgregarCliente>();
                _AgregarCliente.Permisos = _Permisos;
                _AgregarCliente.Id_Cliente = 0;
                _AgregarCliente.FormClosed += (s, args) => _AgregarCliente = null;
                _AgregarCliente.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_cliente.Rows.Count > 0)
            {
                if (dtg_cliente.SelectedRows.Count > 0)
                {
                    var row = dtg_cliente.SelectedRows[0];
                    if (row.Cells["cl_IdCliente"].Value != null)
                    {
                        Id_Cliente = Convert.ToInt32(row.Cells["cl_IdCliente"].Value);
                        if (_Permisos != null)
                        {
                            _AgregarCliente = _serviceProvider.GetRequiredService<AgregarCliente>();
                            _AgregarCliente.Permisos = _Permisos;
                            _AgregarCliente.Id_Cliente = Id_Cliente;
                            _AgregarCliente.FormClosed += (s, args) => _AgregarCliente = null;
                            _AgregarCliente.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_mejor_precio_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _PreciosClientes = _serviceProvider.GetRequiredService<PreciosClientes>();
                _PreciosClientes.Permisos = _Permisos;
                _PreciosClientes.FormClosed += (s, args) => _PreciosClientes = null;
                _PreciosClientes.ShowDialog();
            }
        }
    }
}
