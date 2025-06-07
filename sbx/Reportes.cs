using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sbx
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            cbx_client_venta.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        private void cbx_client_venta_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();

            switch (cbx_client_venta.Text)
            {
                case "":
                    break;
                default:
                    break;
            }

            if (cbx_client_venta.Text == "Resumen - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
            }

            if (cbx_client_venta.Text == "Resumen - Ganancias y perdidas")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Prefijo-Consecutivo", "Nombre producto", "Id", "Sku", "Codigo barras", "Nombre Cliente", "Num Doc Cliente", "Nombre usuario", "Id usuario" });
            }

            cbx_campo_filtro.SelectedIndex = 0;
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {

        }
    }
}
