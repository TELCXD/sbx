using sbx.core.Interfaces.FechaVencimiento;

namespace sbx
{
    public partial class ConfirmaFechaVecimiento : Form
    {
        private int _Id_producto;
        public delegate void RetornaFechaVencimiento(DateTime FechaVence);
        public event RetornaFechaVencimiento retornaFechaVencimiento;
        private readonly IFechaVencimiento _IFechaVencimiento;
        private bool cerrandoDesdeCodigo = false;
        public ConfirmaFechaVecimiento(IFechaVencimiento iFechaVencimiento)
        {
            InitializeComponent();
            _IFechaVencimiento = iFechaVencimiento;
        }

        public int Id_producto
        {
            get => _Id_producto;
            set => _Id_producto = value;
        }

        private async void ConfirmaFechaVecimiento_Load(object sender, EventArgs e)
        {
            var resp = await _IFechaVencimiento.BuscarxIdProducto(Id_producto);

            dtg_fecha_vence.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_fecha_vence.Rows.Add(
                            item.IdProducto,
                            item.FechaVencimiento);
                    }
                }
            }
        }

        private void dtg_fecha_vence_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_fecha_vence.Rows.Count > 0)
            {
                int fila = dtg_fecha_vence.CurrentRow.Index;
                DateTime FechaV = Convert.ToDateTime(dtg_fecha_vence[1, fila].Value);
                cerrandoDesdeCodigo = true;
                retornaFechaVencimiento(FechaV);
                this.Close();
            }
        }

        private void ConfirmaFechaVecimiento_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cerrandoDesdeCodigo && e.CloseReason == CloseReason.UserClosing)
            {
                DateTime fecha = new DateTime(1800, 1, 1);
                retornaFechaVencimiento(fecha);
            }
        }
    }
}
