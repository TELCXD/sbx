using sbx.core.Interfaces.Dashboard;
using sbx.core.Interfaces.Reportes;
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
    public partial class Dashboard : Form
    {
        private readonly IDashboard _IDashboard;
        public Dashboard(IDashboard dashboard)
        {
            InitializeComponent();
            _IDashboard = dashboard;
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            await Buscar();
        }

        private async Task Buscar()
        {
            this.Cursor = Cursors.WaitCursor;

            var resp = await _IDashboard.Buscar(dtp_fecha_inicio.Value, dtp_fecha_fin.Value);

            chart1.DataSource = resp.Data;

        }
    }
}
