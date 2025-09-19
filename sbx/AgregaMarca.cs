using sbx.core.Interfaces.Marca;

namespace sbx
{
    public partial class AgregaMarca : Form
    {
        private readonly IMarca _IMarca;
        private int _Id_Marca;

        public AgregaMarca(IMarca marca)
        {
            InitializeComponent();
            _IMarca = marca;
        }

        public int Id_marca
        {
            get => _Id_Marca;
            set => _Id_Marca = value;
        }
        private async void AgregaMarca_Load(object sender, EventArgs e)
        {
            if (Id_marca > 0)
            {
                var resp = await _IMarca.ListMarcaId(Id_marca);

                if (resp.Data != null)
                {
                    txt_id.Text = resp.Data[0].IdMarca.ToString();
                    txt_nombre.Text = resp.Data[0].Nombre;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            int valido = 0;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txt_nombre.Text)) { errorProvider1.SetError(txt_nombre, "Debe Ingresar un nombre"); valido++; }
            
            if (valido > 0) { return; } else { valido = 0; }         

            if (valido == 0)
            {
                var Exist = await _IMarca.ExisteNombre(txt_nombre.Text.Trim(), Id_marca);
                if (Exist) { errorProvider1.SetError(txt_nombre, "Nombre ya existe"); valido++; }

                if (valido > 0) { return; }

                var resp = await _IMarca.CreateUpdate(txt_nombre.Text.Trim(), Id_marca);

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
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
