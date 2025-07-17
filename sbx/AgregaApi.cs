using sbx.core.Entities.CrendencialesApi;
using sbx.core.Interfaces.CredencialesApi;

namespace sbx
{
    public partial class AgregaApi : Form
    {
        private readonly ICredencialesApi _ICredencialesApi;
        private int _Id_credencialesApi;
        private dynamic? _Permisos;

        public AgregaApi(ICredencialesApi iCredencialesApi)
        {
            InitializeComponent();
            _ICredencialesApi = iCredencialesApi;
        }

        public int Id_credencialesApi
        {
            get => _Id_credencialesApi;
            set => _Id_credencialesApi = value;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private async void AgregaApi_Load(object sender, EventArgs e)
        {
            if (Id_credencialesApi > 0)
            {
                var resp = await _ICredencialesApi.ListId(Id_credencialesApi);

                if (resp.Data != null)
                {
                    txt_url_api.Text = resp.Data[0].url_api;
                    txt_descripcion.Text = resp.Data[0].Descripcion;
                    txt_variable1.Text = resp.Data[0].Variable1;
                    txt_variable2.Text = resp.Data[0].Variable2;
                    txt_variable3.Text = resp.Data[0].Variable3;
                    txt_variable4.Text = resp.Data[0].Variable4;
                    txt_variable5.Text = resp.Data[0].Variable5;
                    txt_variable6.Text = resp.Data[0].Variable6;
                    txt_variable7.Text = resp.Data[0].Variable7;
                    txt_variable8.Text = resp.Data[0].Variable8;
                    txt_variable9.Text = resp.Data[0].Variable9;
                    txt_variable10.Text = resp.Data[0].Variable10;
                    txt_variable11.Text = resp.Data[0].Variable11;
                    txt_variable12.Text = resp.Data[0].Variable12;
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            ValidaPermisos();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "credencialesApi":
                            btn_guardar.Enabled = item.ToCreate == 1 ? true : false;
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

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            bool Valido = true;

            if (txt_url_api.Text == "")
            {
                errorProvider1.SetError(txt_url_api, "Debe ingresar url de api");
                Valido = false;
            }

            if (Valido == true)
            {
                var Datos = new CredencialesApiEntitie
                {
                    IdCredencialesApi = Id_credencialesApi,
                    url_api = txt_url_api.Text,
                    Descripcion = txt_descripcion.Text,
                    Variable1 = txt_variable1.Text,
                    Variable2 = txt_variable2.Text,
                    Variable3 = txt_variable3.Text,
                    Variable4 = txt_variable4.Text,
                    Variable5 = txt_variable5.Text,
                    Variable6 = txt_variable6.Text,
                    Variable7 = txt_variable7.Text,
                    Variable8 = txt_variable8.Text,
                    Variable9 = txt_variable9.Text,
                    Variable10 = txt_variable10.Text,
                    Variable11 = txt_variable11.Text,
                    Variable12 = txt_variable12.Text,
                };

                var resp = await _ICredencialesApi.CreateUpdate(Datos, Convert.ToInt32(_Permisos?[0]?.IdUser));

                if (resp != null)
                {
                    if (resp.Flag == true)
                    {
                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
