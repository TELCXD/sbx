using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Vendedor;

namespace sbx
{
    public partial class Vendedor : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVendedor _IVendedor;
        private AgregarVendedor? _AgregarVendedor;

        public Vendedor(IServiceProvider serviceProvider, IVendedor vendedor)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IVendedor = vendedor;
        }

        private dynamic? _Permisos;
        private int Id_Vendedor = 0;

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Vendedor_Load(object sender, EventArgs e)
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
                        case "vendedores":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
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

        private async Task ConsultaVendedor()
        {
            errorProvider1.Clear();

            var resp = await _IVendedor.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_vendedor.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_vendedor.Rows.Clear();
                    foreach (var item in resp.Data)
                    {
                        dtg_vendedor.Rows.Add(
                            item.IdVendedor,
                            item.IdentificationType,
                            item.NumeroDocumento,
                            item.NombreCompletoVendedor,
                            item.Direccion,
                            item.Telefono,
                            item.Email,
                            item.Estado == true ? "Activo": "Inactivo");
                    }
                }
            }
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaVendedor();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregarVendedor = _serviceProvider.GetRequiredService<AgregarVendedor>();
                _AgregarVendedor.Permisos = _Permisos;
                _AgregarVendedor.Id_Vendedor = 0;
                _AgregarVendedor.FormClosed += (s, args) => _AgregarVendedor = null;
                _AgregarVendedor.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_vendedor.Rows.Count > 0)
            {
                if (dtg_vendedor.SelectedRows.Count > 0)
                {
                    var row = dtg_vendedor.SelectedRows[0];
                    if (row.Cells["cl_IdVendedor"].Value != null)
                    {
                        Id_Vendedor = Convert.ToInt32(row.Cells["cl_IdVendedor"].Value);
                        if (_Permisos != null)
                        {
                            _AgregarVendedor = _serviceProvider.GetRequiredService<AgregarVendedor>();
                            _AgregarVendedor.Permisos = _Permisos;
                            _AgregarVendedor.Id_Vendedor = Id_Vendedor;
                            _AgregarVendedor.FormClosed += (s, args) => _AgregarVendedor = null;
                            _AgregarVendedor.ShowDialog();
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
