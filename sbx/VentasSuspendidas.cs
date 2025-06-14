using sbx.core.Interfaces.Venta;

namespace sbx
{
    public partial class VentasSuspendidas : Form
    {
        public delegate void EnviarId(int id);
        public event EnviarId EnviaId;
        private readonly IVenta _IVenta;
        int fila = 0;
        int Id = 0;
        private int Id_Venta_suspendida = 0;

        public VentasSuspendidas(IVenta venta)
        {
            InitializeComponent();
            _IVenta = venta;
        }

        private async void VentasSuspendidas_Load(object sender, EventArgs e)
        {
            await ConsultaVentasSuspendidas();
        }

        private async Task ConsultaVentasSuspendidas()
        {
            dtg_ventas_suspendidas.Rows.Clear();
            int contador = 1;

            var resp = await _IVenta.ListVentasSuspendidas();

            dtg_ventas_suspendidas.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_ventas_suspendidas.Rows.Add(
                            item.IdVenta_Suspendidas,
                            contador,
                            item.CreationDate,
                            item.NumeroDocumento + " - " + item.NombreRazonSocial);

                        contador++;
                    }
                }
            }
        }

        private void btn_reactivar_Click(object sender, EventArgs e)
        {
            if (dtg_ventas_suspendidas.Rows.Count > 0)
            {
                if (dtg_ventas_suspendidas.SelectedRows.Count > 0)
                {
                    var row = dtg_ventas_suspendidas.SelectedRows[0];
                    if (row.Cells["cl_idVenta_suspendida"].Value != null)
                    {
                        Id_Venta_suspendida = Convert.ToInt32(row.Cells["cl_idVenta_suspendida"].Value);
                        EnviaId(Id_Venta_suspendida);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtg_ventas_suspendidas_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_ventas_suspendidas.Rows.Count > 0)
            {
                fila = dtg_ventas_suspendidas.CurrentRow.Index;
                Id = Convert.ToInt32(dtg_ventas_suspendidas[0, fila].Value);
                EnviaId(Id);
                this.Close();
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dtg_ventas_suspendidas.Rows.Count > 0)
            {
                if (dtg_ventas_suspendidas.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar venta suspendida?",
                       "Confirmar",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var row = dtg_ventas_suspendidas.SelectedRows[0];
                        if (row.Cells["cl_idVenta_suspendida"].Value != null)
                        {
                            Id_Venta_suspendida = Convert.ToInt32(row.Cells["cl_idVenta_suspendida"].Value);
                            var resp = await _IVenta.EliminarVentasSuspendidas(Id_Venta_suspendida);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await ConsultaVentasSuspendidas();
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
                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
