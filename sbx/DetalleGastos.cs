using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Gastos;
using sbx.core.Interfaces.Parametros;
using System.Globalization;

namespace sbx
{
    public partial class DetalleGastos : Form
    {
        private readonly IGastos _IGastos;
        private readonly IParametros _IParametros;
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private AgregaGasto? _AgregaGasto;

        public DetalleGastos(IGastos gastos, IParametros parametros, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IGastos = gastos;
            _IParametros = parametros;
            _serviceProvider = serviceProvider;
        }

        private int Id_Gasto = 0;

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void DetalleGastos_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            cbx_campo_filtro.SelectedIndex = 0;

            var DataParametros = await _IParametros.List("Tipo filtro producto");

            if (DataParametros.Data != null)
            {
                if (DataParametros.Data.Count > 0)
                {
                    string BuscarPor = DataParametros.Data[0].Value;
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
                        case "Gastos":
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Consulta();
        }

        private async Task Consulta()
        {
            btn_buscar.Enabled = false;
            txt_buscar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            decimal Total = 0;

            var resp = await _IGastos.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_gastos.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_gastos.Rows.Add(
                            item.IdGasto,
                            item.Categoria,
                            item.Subcategoria,
                            item.Detalle,
                            item.ValorGasto.ToString("N2", new CultureInfo("es-CO")),
                            item.CreationDate,
                            item.Usuario);

                        Total += item.ValorGasto;
                    }
                }
            }

            lbl_total_gastos.Text = Total.ToString("N2", new CultureInfo("es-CO"));

            btn_buscar.Enabled = true;
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await Consulta();
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_gastos.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar el gasto seleccionado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_gastos.SelectedRows.Count > 0)
                    {
                        var row = dtg_gastos.SelectedRows[0];
                        if (row.Cells["cl_idGasto"].Value != null)
                        {
                            Id_Gasto = Convert.ToInt32(row.Cells["cl_idGasto"].Value);
                            var resp = await _IGastos.Eliminar(Id_Gasto);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await Consulta();
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
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_gastos.Rows.Count > 0)
            {
                if (dtg_gastos.SelectedRows.Count > 0)
                {
                    var row = dtg_gastos.SelectedRows[0];
                    if (row.Cells["cl_idGasto"].Value != null)
                    {
                        Id_Gasto = Convert.ToInt32(row.Cells["cl_idGasto"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaGasto = _serviceProvider.GetRequiredService<AgregaGasto>();
                            _AgregaGasto.Permisos = _Permisos;
                            _AgregaGasto.Id_Gasto = Id_Gasto;
                            _AgregaGasto.FormClosed += (s, args) => _AgregaGasto = null;
                            _AgregaGasto.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para Editar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaGasto = _serviceProvider.GetRequiredService<AgregaGasto>();
                _AgregaGasto.Permisos = _Permisos;
                _AgregaGasto.Id_Gasto = 0;
                _AgregaGasto.FormClosed += (s, args) => _AgregaGasto = null;
                _AgregaGasto.ShowDialog();
            }
        }
    }
}
