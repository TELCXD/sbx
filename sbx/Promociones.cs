using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.Promociones;

namespace sbx
{
    public partial class Promociones : Form
    {
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPromociones _IPromociones;
        private AgregaPromocion? _AgregaPromocion;
        int IdPromocion = 0;

        public Promociones(IServiceProvider serviceProvider, IPromociones promociones)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IPromociones = promociones;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void Promociones_Load(object sender, EventArgs e)
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
                        case "promociones":
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

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaPromocion = _serviceProvider.GetRequiredService<AgregaPromocion>();
                _AgregaPromocion.Permisos = _Permisos;
                _AgregaPromocion.Id_promocion = 0;
                _AgregaPromocion.FormClosed += (s, args) => _AgregaPromocion = null;
                _AgregaPromocion.ShowDialog();
            }
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Consulta();
        }

        private async Task Consulta()
        {
            var resp = await _IPromociones.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_promociones.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_promociones.Rows.Add(
                            item.IdPromocion,
                            item.NombrePromocion,
                            item.NombreTipoPromocion,
                            item.Porcentaje,
                            item.FechaInicio,
                            item.FechaFin);
                    }
                }
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_promociones.Rows.Count > 0)
            {
                if (dtg_promociones.SelectedRows.Count > 0)
                {
                    var row = dtg_promociones.SelectedRows[0];
                    if (row.Cells["cl_id_promocion"].Value != null)
                    {
                        IdPromocion = Convert.ToInt32(row.Cells["cl_id_promocion"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaPromocion = _serviceProvider.GetRequiredService<AgregaPromocion>();
                            _AgregaPromocion.Permisos = _Permisos;
                            _AgregaPromocion.Id_promocion = IdPromocion;
                            _AgregaPromocion.FormClosed += (s, args) => _AgregaPromocion = null;
                            _AgregaPromocion.ShowDialog();
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
            if (dtg_promociones.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar promocion seleccionada?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_promociones.SelectedRows.Count > 0)
                    {
                        var row = dtg_promociones.SelectedRows[0];
                        if (row.Cells["cl_id_promocion"].Value != null)
                        {
                            IdPromocion = Convert.ToInt32(row.Cells["cl_id_promocion"].Value);
                            var resp = await _IPromociones.Eliminar(IdPromocion);

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
    }
}
