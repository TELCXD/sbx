using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.PrecioCliente;

namespace sbx
{
    public partial class ListaPrecios : Form
    {
        private dynamic? _Permisos;
        private readonly IListaPrecios _IListaPrecios;
        private AgregaListaPrecios? _AgregaListaPrecios;
        private readonly IServiceProvider _serviceProvider;
        int IdListaPrecio = 0;

        public ListaPrecios(IListaPrecios listaPrecios, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IListaPrecios = listaPrecios;
            _serviceProvider = serviceProvider;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void ListaPrecios_Load(object sender, EventArgs e)
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
                        case "listaPrecios":
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
                _AgregaListaPrecios = _serviceProvider.GetRequiredService<AgregaListaPrecios>();
                _AgregaListaPrecios.Permisos = _Permisos;
                _AgregaListaPrecios.Id_lista_precios = 0;
                _AgregaListaPrecios.FormClosed += (s, args) => _AgregaListaPrecios = null;
                _AgregaListaPrecios.ShowDialog();
            }
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await Consulta();
        }

        private async Task Consulta()
        {
            var resp = await _IListaPrecios.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text);

            dtg_lista_precios.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    dtg_lista_precios.Rows.Clear();
                    foreach (var item in resp.Data)
                    {
                        dtg_lista_precios.Rows.Add(
                            item.IdListaPrecio,
                            item.NombreLista,
                            item.NombreTipoCliente,
                            item.FechaInicio,
                            item.FechaFin);
                    }
                }
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_lista_precios.Rows.Count > 0)
            {
                if (dtg_lista_precios.SelectedRows.Count > 0)
                {
                    var row = dtg_lista_precios.SelectedRows[0];
                    if (row.Cells["cl_id_lista_precio"].Value != null)
                    {
                        IdListaPrecio = Convert.ToInt32(row.Cells["cl_id_lista_precio"].Value);

                        if (IdListaPrecio > 1)
                        {
                            if (_Permisos != null)
                            {
                                _AgregaListaPrecios = _serviceProvider.GetRequiredService<AgregaListaPrecios>();
                                _AgregaListaPrecios.Permisos = _Permisos;
                                _AgregaListaPrecios.Id_lista_precios = IdListaPrecio;
                                _AgregaListaPrecios.FormClosed += (s, args) => _AgregaListaPrecios = null;
                                _AgregaListaPrecios.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lista de precio no puede ser editada", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dtg_lista_precios.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar lista de precio seleccionada?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_lista_precios.SelectedRows.Count > 0)
                    {
                        var row = dtg_lista_precios.SelectedRows[0];
                        if (row.Cells["cl_id_lista_precio"].Value != null)
                        {
                            IdListaPrecio = Convert.ToInt32(row.Cells["cl_id_lista_precio"].Value);

                            if (IdListaPrecio > 1)
                            {
                                var resp = await _IListaPrecios.Eliminar(IdListaPrecio);

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
                                MessageBox.Show("Lista de precio no puede ser eliminada", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
