
using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.RangoNumeracion;

namespace sbx
{
    public partial class Ajustes : Form
    {
        private dynamic? _Permisos;
        private readonly IRangoNumeracion _IRangoNumeracion;
        private readonly IServiceProvider _serviceProvider;
        private AgregaRna? _AgregaRna;
        private int Id_RangoNumeracion = 0;

        public Ajustes(IServiceProvider serviceProvider, IRangoNumeracion rangoNumeracion)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IRangoNumeracion = rangoNumeracion;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void Ajustes_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await ConsultaRangosNumeracion();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "ajustes":
                            btn_agregar_ra.Enabled = item.ToCreate == 1 ? true : false;
                            btn_editar_ra.Enabled = item.ToUpdate == 1 ? true : false;
                            //btn_eliminar_ra.Enabled = item.ToDelete == 1 ? true : false;
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

        private async Task ConsultaRangosNumeracion()
        {
            var resp = await _IRangoNumeracion.List(0);

            if (resp.Data != null)
            {
                dtg_rangos_numeracion.Rows.Clear();
                foreach (var item in resp.Data)
                {
                    dtg_rangos_numeracion.Rows.Add(
                        item.Id_RangoNumeracion,
                        item.Active == true ? "Active" : "Inactivo",
                        item.Id_TipoDocumentoRangoNumeracion == 1 ? "Factura Electrónica de Venta"
                        :item.Id_TipoDocumentoRangoNumeracion == 2 ? "Nota de Crédito" 
                        :item.Id_TipoDocumentoRangoNumeracion == 3 ? "Factura": "",
                        item.Prefijo,
                        item.NumeroDesde,
                        item.NumeroHasta,
                        item.NumeroAutorizacion,
                        item.FechaVencimiento,
                        "0");
                }
            }
        }

        private void btn_agregar_ra_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaRna = _serviceProvider.GetRequiredService<AgregaRna>();
                _AgregaRna.Permisos = _Permisos;
                _AgregaRna.Id_RangoNumeracion = 0;
                _AgregaRna.FormClosed += (s, args) => _AgregaRna = null;
                _AgregaRna.ShowDialog();
            }
        }

        private async void btn_buscar_ra_Click(object sender, EventArgs e)
        {
            await ConsultaRangosNumeracion();
        }

        private void btn_editar_ra_Click(object sender, EventArgs e)
        {
            if (dtg_rangos_numeracion.Rows.Count > 0) 
            {
                if (dtg_rangos_numeracion.SelectedRows.Count > 0)
                {
                    var row = dtg_rangos_numeracion.SelectedRows[0];
                    if (row.Cells["cl_Nro"].Value != null)
                    {
                        Id_RangoNumeracion = Convert.ToInt32(row.Cells["cl_Nro"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaRna = _serviceProvider.GetRequiredService<AgregaRna>();
                            _AgregaRna.Permisos = _Permisos;
                            _AgregaRna.Id_RangoNumeracion = Id_RangoNumeracion;
                            _AgregaRna.FormClosed += (s, args) => _AgregaRna = null;
                            _AgregaRna.ShowDialog();
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
