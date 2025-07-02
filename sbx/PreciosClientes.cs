using Microsoft.Extensions.DependencyInjection;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.PrecioCliente;
using System.Globalization;

namespace sbx
{
    public partial class PreciosClientes : Form
    {
        private dynamic? _Permisos;
        private int Id_precios_cliente = 0;
        private readonly IPrecioCliente _IPrecioCliente;
        private AgregaPreciosCliente? _AgregaPreciosCliente;
        private readonly IServiceProvider _serviceProvider;
        private long llavePrimaria;

        public PreciosClientes(IPrecioCliente precioCliente, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _IPrecioCliente = precioCliente;
            _serviceProvider = serviceProvider;
        }

        private void PreciosClientes_Load(object sender, EventArgs e)
        {
            ValidaPermisos();

            cbx_client_producto.SelectedIndex = 0;
            cbx_campo_filtro.SelectedIndex = 0;
            cbx_tipo_filtro.SelectedIndex = 0;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "preciosClientes":
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

        private void cbx_client_producto_SelectedValueChanged(object sender, EventArgs e)
        {
            cbx_campo_filtro.Items.Clear();
            if (cbx_client_producto.Text == "Cliente")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Num Doc" });

            }
            else if (cbx_client_producto.Text == "Producto")
            {
                cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            }
            cbx_campo_filtro.SelectedIndex = 0;
        }

        private async void btn_buscar_Click(object sender, EventArgs e)
        {
            await ConsultaPrecioCliente();
        }

        private async Task ConsultaPrecioCliente()
        {
            errorProvider1.Clear();
            if (cbx_campo_filtro.Text == "Id")
            {
                bool esNumerico = int.TryParse(txt_buscar.Text, out int valor);
                if (!esNumerico)
                {
                    errorProvider1.SetError(txt_buscar, "Ingrese un valor numerico");
                    return;
                }
            }

            var resp = await _IPrecioCliente.Buscar(txt_buscar.Text, cbx_campo_filtro.Text, cbx_tipo_filtro.Text, cbx_client_producto.Text);

            dtg_cliente_precio.Rows.Clear();

            if (resp.Data != null)
            {
                if (resp.Data.Count > 0)
                {
                    foreach (var item in resp.Data)
                    {
                        dtg_cliente_precio.Rows.Add(
                            item.llavePrimaria,
                            item.IdCliente,
                            item.NumeroDocumento,
                            item.NombreRazonSocial,
                            item.TipoCliente,
                            item.IdProducto,
                            item.Sku,
                            item.CodigoBarras,
                            item.Nombre,
                            item.PrecioEspecial.ToString("N2", new CultureInfo("es-CO")),
                            item.FechaInicio,
                            item.FechaFin);
                    }
                }
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (_Permisos != null)
            {
                _AgregaPreciosCliente = _serviceProvider.GetRequiredService<AgregaPreciosCliente>();
                _AgregaPreciosCliente.Permisos = _Permisos;
                _AgregaPreciosCliente.llavePrimaria = 0;
                _AgregaPreciosCliente.FormClosed += (s, args) => _AgregaPreciosCliente = null;
                _AgregaPreciosCliente.ShowDialog();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dtg_cliente_precio.Rows.Count > 0)
            {
                if (dtg_cliente_precio.SelectedRows.Count > 0)
                {
                    var row = dtg_cliente_precio.SelectedRows[0];
                    if (row.Cells["cl_id_precio_cliente"].Value != null)
                    {
                        llavePrimaria = Convert.ToInt32(row.Cells["cl_id_precio_cliente"].Value);
                        if (_Permisos != null)
                        {
                            _AgregaPreciosCliente = _serviceProvider.GetRequiredService<AgregaPreciosCliente>();
                            _AgregaPreciosCliente.Permisos = _Permisos;
                            _AgregaPreciosCliente.llavePrimaria = llavePrimaria;
                            _AgregaPreciosCliente.FormClosed += (s, args) => _AgregaPreciosCliente = null;
                            _AgregaPreciosCliente.ShowDialog();
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
            if (dtg_cliente_precio.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar el precio del cliente seleccionado?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dtg_cliente_precio.SelectedRows.Count > 0)
                    {
                        var row = dtg_cliente_precio.SelectedRows[0];
                        if (row.Cells["cl_id_precio_cliente"].Value != null)
                        {
                            Id_precios_cliente = Convert.ToInt32(row.Cells["cl_id_precio_cliente"].Value);

                            var resp = await _IPrecioCliente.Eliminar(Id_precios_cliente);

                            if (resp != null)
                            {
                                if (resp.Flag == true)
                                {
                                    MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await ConsultaPrecioCliente();
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
                MessageBox.Show("No hay datos para Eliminar", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
