using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.Marca;

namespace sbx
{
    public partial class Marcas : Form
    {
        private readonly IMarca _IMarca;
        private AgregaMarca? _AgregaMarca;
        private readonly IServiceProvider _serviceProvider;

        public Marcas(IMarca marca, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IMarca = marca;
            _serviceProvider = serviceProvider;
        }

        private int Id_Marca = 0;

        private void Marcas_Load(object sender, EventArgs e)
        {

        }

        private async Task Consulta()
        {
            errorProvider1.Clear();
            txt_buscar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            var resp = await _IMarca.BuscaMarca(txt_buscar.Text);

            dtg_marca.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_marca.Rows.Add(
                            item.IdMarca,
                            item.Nombre);
                    }
                }
            }
            txt_buscar.Enabled = true;
            txt_buscar.Focus();
            this.Cursor = Cursors.Default;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            _AgregaMarca = _serviceProvider.GetRequiredService<AgregaMarca>();
            _AgregaMarca.Id_marca = 0;
            _AgregaMarca.FormClosed += (s, args) => _AgregaMarca = null;
            _AgregaMarca.ShowDialog();
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_marca.Rows.Count > 0)
            {
                if (dtg_marca.SelectedRows.Count > 0)
                {
                    var row = dtg_marca.SelectedRows[0];
                    if (row.Cells["cl_idMarca"].Value != null)
                    {
                        Id_Marca = Convert.ToInt32(row.Cells["cl_idMarca"].Value);

                        if (Id_Marca > 1)
                        {
                            _AgregaMarca = _serviceProvider.GetRequiredService<AgregaMarca>();
                            _AgregaMarca.Id_marca = Id_Marca;
                            _AgregaMarca.FormClosed += (s, args) => _AgregaMarca = null;
                            _AgregaMarca.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("La marca por defecto N/a no puede ser modificada", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dtg_marca.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar la marca seleccionada?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_marca.SelectedRows.Count > 0)
                    {
                        var row = dtg_marca.SelectedRows[0];
                        if (row.Cells["cl_idMarca"].Value != null)
                        {
                            Id_Marca = Convert.ToInt32(row.Cells["cl_idMarca"].Value);

                            if (Id_Marca > 1)
                            {
                                var resp = await _IMarca.Eliminar(Id_Marca);

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
                            else
                            {
                                MessageBox.Show("La marca por defecto N/a no puede ser eliminada", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Consulta();
        }

        private async void txt_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter
            if (e.KeyChar == (char)13)
            {
                await Consulta();
            }
        }
    }
}
