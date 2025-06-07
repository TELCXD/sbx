using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Proveedor;

namespace sbx
{
    public partial class Proveedores : Form
    {
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProveedor _IProveedor;
        private AgregaProveedor? _AgregaProveedor;
        private int Id_Proveedor = 0;

        public Proveedores(IServiceProvider serviceProvider, IProveedor proveedor)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IProveedor = proveedor;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Proveedores_Load(object sender, EventArgs e)
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
                        case "proveedor":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            //btn_eliminar.Enabled = item.ToUpdate == 1 ? true : false;
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
            await ConsultaProveedor();
        }

        private async Task ConsultaProveedor()
        {
            errorProvider1.Clear();

            var resp = await _IProveedor.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_proveedor.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_proveedor.Rows.Clear();
                    foreach (var item in resp.Data)
                    {
                        dtg_proveedor.Rows.Add(
                            item.IdProveedor,
                            item.IdentificationType,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
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
                _AgregaProveedor = _serviceProvider.GetRequiredService<AgregaProveedor>();
                _AgregaProveedor.Permisos = _Permisos;
                _AgregaProveedor.Id_Proveedor = 0;
                _AgregaProveedor.FormClosed += (s, args) => _AgregaProveedor = null;
                _AgregaProveedor.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_proveedor.Rows.Count > 0)
            {
                if (dtg_proveedor.SelectedRows.Count > 0)
                {
                    var row = dtg_proveedor.SelectedRows[0];
                    if (row.Cells["cl_IdProveedor"].Value != null)
                    {
                        Id_Proveedor = Convert.ToInt32(row.Cells["cl_IdProveedor"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaProveedor = _serviceProvider.GetRequiredService<AgregaProveedor>();
                            _AgregaProveedor.Permisos = _Permisos;
                            _AgregaProveedor.Id_Proveedor = Id_Proveedor;
                            _AgregaProveedor.FormClosed += (s, args) => _AgregaProveedor = null;
                            _AgregaProveedor.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
