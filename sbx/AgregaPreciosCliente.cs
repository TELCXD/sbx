using Microsoft.Extensions.DependencyInjection;
using sbx.core.Entities.PrecioCliente;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.TipoCliente;
using System.Globalization;

namespace sbx
{
    public partial class AgregaPreciosCliente : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private dynamic? _Permisos;
        char decimalSeparator = ',';
        private readonly IPrecioCliente _IPrecioCliente;
        PrecioClienteEntitie precioClienteEntitie = new PrecioClienteEntitie();
        private Buscador? _Buscador;
        private readonly IProducto _IProducto;
        private readonly ICliente _ICliente;
        string busqueda = "";
        private long _llavePrimaria;

        public AgregaPreciosCliente(IServiceProvider serviceProvider, IPrecioCliente precioCliente, IProducto iProducto, ICliente cliente)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _IPrecioCliente = precioCliente;
            _IProducto = iProducto;
            _ICliente = cliente;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public long llavePrimaria
        {
            get => _llavePrimaria;
            set => _llavePrimaria = value;
        }

        private async void AgregaPreciosCliente_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
            await CargaDatosIniciales();
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
                            btn_add_precio_cliente.Enabled = item.ToCreate == 1 ? true : false;
                            btn_busca_cliente.Enabled = item.ToRead == 1 ? true : false;
                            btn_busca_producto.Enabled = item.ToRead == 1 ? true : false;
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

        private async Task CargaDatosIniciales()
        {
            dtp_fecha_inicio.Value = DateTime.Now;
            dtp_fecha_fin.Value = DateTime.Now;

            if (llavePrimaria > 0)
            {
                txt_documento_cliente.Enabled = false;
                txt_producto.Enabled=false;
                btn_busca_cliente.Enabled= false;
                btn_busca_producto.Enabled= false;

                var resp = await _IPrecioCliente.List(llavePrimaria);

                if (resp.Data != null)
                {
                    precioClienteEntitie.llavePrimaria = llavePrimaria;
                    precioClienteEntitie.IdCliente = resp.Data[0].IdCliente;
                    txt_documento_cliente.Text = resp.Data[0].NumeroDocumento;
                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                    precioClienteEntitie.IdProducto = resp.Data[0].IdProducto;
                    txt_producto.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                    lbl_nombre_producto.Text = resp.Data[0].Nombre;
                    txt_precio_especial.Text = resp.Data[0].PrecioEspecial.ToString("N2", new CultureInfo("es-CO"));
                    dtp_fecha_inicio.Value = Convert.ToDateTime(resp.Data[0].FechaInicio);
                    dtp_fecha_fin.Value = Convert.ToDateTime(resp.Data[0].FechaFin);
                }
                else
                {
                    MessageBox.Show("No hay informacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txt_precio_especial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_add_precio_cliente_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_documento_cliente.Text.Trim() != "" && txt_producto.Text.Trim() != "")
            {
                if (txt_precio_especial.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txt_precio_especial.Text, new CultureInfo("es-CO")) > 0)
                    {
                        DateTime fechaIni = dtp_fecha_inicio.Value;
                        DateTime fechaFin = dtp_fecha_fin.Value;

                        if (fechaIni <= fechaFin)
                        {
                            precioClienteEntitie.llavePrimaria = llavePrimaria;
                            precioClienteEntitie.PrecioEspecial = Convert.ToDecimal(txt_precio_especial.Text, new CultureInfo("es-CO"));
                            precioClienteEntitie.FechaInicio = fechaIni;
                            precioClienteEntitie.FechaFin = fechaFin;

                            var resp = await _IPrecioCliente.CreateUpdate(precioClienteEntitie, Convert.ToInt32(_Permisos?[0]?.IdUser));

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
                        else
                        {
                            errorProvider1.SetError(dtp_fecha_inicio, "Fecha inicio debe ser menor a fecha fin");
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(txt_precio_especial, "Precio especial debe ser mayor a cero (0)");
                    }
                }
                else
                {
                    errorProvider1.SetError(txt_precio_especial, "Debe ingresar precio especial");
                }
            }
            else
            {
                errorProvider1.SetError(txt_documento_cliente, "Se debe seleccionar cliente");
                errorProvider1.SetError(txt_producto, "Se debe seleccionar producto");
            }
        }

        private void btn_busca_cliente_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "AgregaPrecios_busca_cliente";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            busqueda = "AgregaPrecios_busca_cliente";
            _Buscador.ShowDialog();
        }

        private void btn_busca_producto_Click(object sender, EventArgs e)
        {
            _Buscador = _serviceProvider.GetRequiredService<Buscador>();
            _Buscador.Origen = "AgregaPrecios_busca_producto";
            _Buscador.EnviaId += _Buscador_EnviaId;
            _Buscador.FormClosed += (s, args) => _Buscador = null;
            busqueda = "AgregaPrecios_busca_producto";
            _Buscador.ShowDialog();
        }

        private async void _Buscador_EnviaId(int id)
        {
            if (busqueda == "AgregaPrecios_busca_cliente") 
            {
                var resp = await _ICliente.List(id);
                if (resp.Data != null)
                {
                    precioClienteEntitie.IdCliente = resp.Data[0].IdCliente;
                    txt_documento_cliente.Text = resp.Data[0].NumeroDocumento;
                    lbl_nombre_cliente.Text = resp.Data[0].NombreRazonSocial;
                }
            }
            else if(busqueda == "AgregaPrecios_busca_producto")
            {
                var resp = await _IProducto.List(id);
                if (resp.Data != null)
                {
                    precioClienteEntitie.IdProducto = resp.Data[0].IdProducto;
                    txt_producto.Text = resp.Data[0].IdProducto + " " + resp.Data[0].Sku + " " + resp.Data[0].CodigoBarras;
                    lbl_nombre_producto.Text = resp.Data[0].Nombre;
                }
            }
        }
    }
}
