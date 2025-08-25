using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Vendedor;

namespace sbx
{
    public partial class Vendedor : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVendedor _IVendedor;
        private AgregarVendedor? _AgregarVendedor;
        private readonly IParametros _IParametros;
        public Vendedor(IServiceProvider serviceProvider, IVendedor vendedor, IParametros iParametros)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IVendedor = vendedor;
            _IParametros = iParametros;
        }

        private dynamic? _Permisos;
        private int Id_Vendedor = 0;
        string BuscarPor = "";
        string ModoRedondeo = "N/A";
        string MultiploRendondeo = "50";
        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Vendedor_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;

            BuscarPor = "";
            ModoRedondeo = "N/A";
            MultiploRendondeo = "50";

            var DataParametros = await _IParametros.List("");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    foreach (var itemParametros in DataParametros.Data)
                    {
                        switch (itemParametros.Nombre)
                        {
                            case "Tipo filtro producto":
                                BuscarPor = itemParametros.Value;
                                break;
                            case "Modo Redondeo":
                                ModoRedondeo = itemParametros.Value;
                                break;
                            case "Multiplo Rendondeo":
                                MultiploRendondeo = itemParametros.Value;
                                break;
                            default:
                                break;
                        }
                    }

                    cbx_tipo_filtro.Text = BuscarPor;
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
                        case "vendedores":
                            btn_agregar.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar.Enabled = item.ToUpdate == 1 ? true : false;
                            btn_eliminar.Enabled = item.ToDelete == 1 ? true : false;
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
                            item.Estado == true ? "Activo" : "Inactivo");
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
                        if (Id_Vendedor > 1)
                        {
                            if (_Permisos != null)
                            {
                                _AgregarVendedor = _serviceProvider.GetRequiredService<AgregarVendedor>();
                                _AgregarVendedor.Permisos = _Permisos;
                                _AgregarVendedor.Id_Vendedor = Id_Vendedor;
                                _AgregarVendedor.FormClosed += (s, args) => _AgregarVendedor = null;
                                _AgregarVendedor.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No es posible editar este vendedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_vendedor.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar vendedor seleccionado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_vendedor.SelectedRows.Count > 0)
                    {
                        var row = dtg_vendedor.SelectedRows[0];
                        if (row.Cells["cl_IdVendedor"].Value != null)
                        {
                            Id_Vendedor = Convert.ToInt32(row.Cells["cl_IdVendedor"].Value);

                            if (Id_Vendedor > 1)
                            {
                                var resp = await _IVendedor.Eliminar(Id_Vendedor);

                                if (resp != null)
                                {
                                    if (resp.Flag == true)
                                    {
                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        await ConsultaVendedor();
                                    }
                                    else
                                    {
                                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No es posible eliminar este vendedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
