using sbx.core.Interfaces.Reportes;

namespace sbx
{
    public partial class Reportes : Form
    {
        private readonly IReportes _IReportes;

        public Reportes(IReportes reportes)
        {
            InitializeComponent();
            _IReportes = reportes;
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            cbx_tipo_reporte.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void cbx_client_venta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();

            switch (cbx_tipo_reporte.Text)
            {
                case "":
                    break;
                default:
                    break;
            }

            if (cbx_tipo_reporte.Text == "Resumen - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
            }

            if (cbx_tipo_reporte.Text == "Detallado -  Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
            }

            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            var resp = await _IReportes.Buscar(txt_buscar.Text,cbx_campo_filtro.Text,cbx_tipo_filtro.Text,cbx_tipo_reporte.Text,dtp_fecha_inicio.Value,dtp_fecha_fin.Value);
            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {

                }
            }
        }
    }
}
