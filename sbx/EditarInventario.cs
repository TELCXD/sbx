
namespace sbx
{
    public partial class EditarInventario : Form
    {
        private DateTime? _FechaVenceActual;
        private DateTime? _FechaVenceNueva;

        public delegate void EnviarNuevaFechaV(DateTime NuevaFechaVencimiento);
        public event EnviarNuevaFechaV NuevaFechaV;

        public DateTime? FechaVenceActual
        {
            get => _FechaVenceActual;
            set => _FechaVenceActual = value;
        }

        public DateTime? FechaVenceNueva
        {
            get => _FechaVenceNueva;
            set => _FechaVenceNueva = value;
        }

        public EditarInventario()
        {
            InitializeComponent();
        }

        private void EditarInventario_Load(object sender, EventArgs e)
        {
            chk_sin_fecha_vence.Checked = true;
            dtpkActualFechaVencimiento.Value = FechaVenceActual!.Value;
            dtpkNuevaFechaVencimiento.Value = new DateTime(1900, 01, 01);
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            NuevaFechaV(dtpkNuevaFechaVencimiento.Value);
            this.Close();
        }

        private void chk_sin_fecha_vence_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sin_fecha_vence.Checked) 
            {               
                dtpkNuevaFechaVencimiento.Value = new DateTime(1900, 01, 01);
                dtpkNuevaFechaVencimiento.Enabled = false;
            }
            else
            {
                dtpkNuevaFechaVencimiento.Value = DateTime.Now;
                dtpkNuevaFechaVencimiento.Enabled = true;
            }
        }
    }
}
