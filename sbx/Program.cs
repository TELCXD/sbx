using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sbx.core.Interfaces;
using sbx.core.Interfaces.ActividadEconomica;
using sbx.core.Interfaces.Backup;
using sbx.core.Interfaces.Banco;
using sbx.core.Interfaces.Caja;
using sbx.core.Interfaces.Categoria;
using sbx.core.Interfaces.Ciudad;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.CodigoPostal;
using sbx.core.Interfaces.Departamento;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.IdentificationType;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.Marca;
using sbx.core.Interfaces.MedioPago;
using sbx.core.Interfaces.NotaCredito;
using sbx.core.Interfaces.Pago;
using sbx.core.Interfaces.Pais;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Permisos;
using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.PrecioProducto;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.Promociones;
using sbx.core.Interfaces.PromocionProducto;
using sbx.core.Interfaces.Proveedor;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Reportes;
using sbx.core.Interfaces.ResponsabilidadTributaria;
using sbx.core.Interfaces.Rol;
using sbx.core.Interfaces.SalidaInventario;
using sbx.core.Interfaces.Tienda;
using sbx.core.Interfaces.TipoCliente;
using sbx.core.Interfaces.TipoContribuyente;
using sbx.core.Interfaces.TipoDocumentoRangoNumeracion;
using sbx.core.Interfaces.TipoEntrada;
using sbx.core.Interfaces.TipoPromocion;
using sbx.core.Interfaces.TipoResponsabilidad;
using sbx.core.Interfaces.TipoSalida;
using sbx.core.Interfaces.UnidadMedida;
using sbx.core.Interfaces.Usuario;
using sbx.core.Interfaces.Vendedor;
using sbx.core.Interfaces.Venta;
using sbx.repositories.ActividadEconomica;
using sbx.repositories.Backup;
using sbx.repositories.Banco;
using sbx.repositories.Caja;
using sbx.repositories.Categorias;
using sbx.repositories.Ciudad;
using sbx.repositories.Cliente;
using sbx.repositories.CodigoPostal;
using sbx.repositories.Departamento;
using sbx.repositories.EntradaInventario;
using sbx.repositories.IdentificationType;
using sbx.repositories.ListaPrecios;
using sbx.repositories.LoginRepository;
using sbx.repositories.Marca;
using sbx.repositories.MedioPago;
using sbx.repositories.NotaCredito;
using sbx.repositories.Pagos;
using sbx.repositories.Pais;
using sbx.repositories.Parametros;
using sbx.repositories.Permiso;
using sbx.repositories.PrecioCliente;
using sbx.repositories.PrecioProducto;
using sbx.repositories.Producto;
using sbx.repositories.Promociones;
using sbx.repositories.PromocionProducto;
using sbx.repositories.Proveedor;
using sbx.repositories.RangoNumeracion;
using sbx.repositories.Reportes;
using sbx.repositories.ResponsabilidadTributaria;
using sbx.repositories.Rol;
using sbx.repositories.SalidaInventario;
using sbx.repositories.TiendaRepository;
using sbx.repositories.TipoCliente;
using sbx.repositories.TipoContribuyente;
using sbx.repositories.TipoDocumentoRangoNumeracion;
using sbx.repositories.TipoEntrada;
using sbx.repositories.TipoPromocion;
using sbx.repositories.TipoResponsabilidad;
using sbx.repositories.TipoSalida;
using sbx.repositories.UnidadMedida;
using sbx.repositories.Usuario;
using sbx.repositories.Vendedor;
using sbx.repositories.Venta;
using System.Configuration;

namespace sbx
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SbxConnectionString"].ConnectionString;

            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<ILogin>(provider =>
                    new LoginRepository(connectionString));
                services.AddTransient<Login>();

                services.AddTransient<ITienda>(provider =>
                   new TiendaRepository(connectionString));

                services.AddTransient<Tienda>();

                services.AddTransient<Inicio>();

                services.AddTransient<Ajustes>();

                services.AddTransient<IRangoNumeracion>(provider =>
                new RangoNumeracionRepository(connectionString));

                services.AddTransient<Ajustes>();

                services.AddTransient<ITipoDocumentoRangoNumeracion>(provider =>
                new TipoDocumentoRangoNumeracionRepository(connectionString));

                services.AddTransient<AgregaRna>();

                services.AddTransient<Productos>();

                services.AddTransient<AgregarProducto>();

                services.AddTransient<Proveedores>();

                services.AddTransient<AgregaProveedor>();

                services.AddTransient<Clientes>();

                services.AddTransient<AgregarCliente>();

                services.AddTransient<Inventario>();

                services.AddTransient<Entradas>();

                services.AddTransient<AgregaDetalleEntrada>();

                services.AddTransient<Buscador>();

                services.AddTransient<Salidas>();

                services.AddTransient<AgregaDetalleSalida>();

                services.AddTransient<PreciosClientes>();

                services.AddTransient<AgregaPreciosCliente>();

                services.AddTransient<ListaPrecios>();

                services.AddTransient<AgregaListaPrecios>();
                
                services.AddTransient<AddProductoListaPrecio>();

                services.AddTransient<Promociones>();

                services.AddTransient<AgregaPromocion>();

                services.AddTransient<AddProductoPromocion>();

                services.AddTransient<AgregarVentas>();

                services.AddTransient<Ventas>();

                services.AddTransient<DetalleVenta>();

                services.AddTransient<Caja>();

                services.AddTransient<AddApertura>();

                services.AddTransient<AddCierre>();

                services.AddTransient<AddUsuario>();

                services.AddTransient<AgregarNotaCredito>();

                services.AddTransient<DetalleProdDevo>();

                services.AddTransient<AddPagosEfectivo>();

                services.AddTransient<Reportes>();

                services.AddTransient<IIdentificationType>(provider =>
                   new IdentificationTypeRepository(connectionString));

                services.AddTransient<ITipoResponsabilidad>(provider =>
                   new TipoResponsabilidadRepository(connectionString));

                services.AddTransient<IResponsabilidadTributaria>(provider =>
                   new ResponsabilidadTributariaRepository(connectionString));

                services.AddTransient<ITipoContribuyente>(provider =>
                   new TipoContribuyenteRepository(connectionString));

                services.AddTransient<IPais>(provider =>
                   new PaisRepository(connectionString));

                services.AddTransient<IDepartamento>(provider =>
                  new DepartamentoRepository(connectionString));

                services.AddTransient<ICiudad>(provider =>
                  new CiudadRepository(connectionString));

                services.AddTransient<ICodigoPostal>(provider =>
                  new CodigoPostalRepository(connectionString));

                services.AddTransient<ICodigoPostal>(provider =>
                  new CodigoPostalRepository(connectionString));

                services.AddTransient<IActividadEconomica>(provider =>
                  new ActividadEconomicaRepository(connectionString));

                services.AddTransient<IProducto>(provider =>
                 new ProductoRepository(connectionString));

                services.AddTransient<ICategoria>(provider =>
                  new CategoriaRepository(connectionString));

                services.AddTransient<IMarca>(provider =>
                  new MarcaRepository(connectionString));

                services.AddTransient<IUnidadMedida>(provider =>
                  new UnidadMedidaRepository(connectionString));

                services.AddTransient<IProveedor>(provider =>
                  new ProveedorRepository(connectionString));

                services.AddTransient<ICliente>(provider =>
                 new ClienteRepository(connectionString));

                services.AddTransient<ITipoCliente>(provider =>
                  new TipoClienteRepository(connectionString));

                services.AddTransient<ITipoEntrada>(provider =>
                  new TipoEntradaRepository(connectionString));

                services.AddTransient<IEntradaInventario>(provider =>
                  new EntradaInventarioRepository(connectionString));

                services.AddTransient<ITipoSalida>(provider =>
                 new TipoSalidaRepository(connectionString));

                services.AddTransient<ISalidaInventario>(provider =>
                  new SalidaInventarioRepository(connectionString));

                services.AddTransient<IPrecioCliente>(provider =>
                 new PrecioClienteRepository(connectionString));

                services.AddTransient<IListaPrecios>(provider =>
                 new ListaPreciosRepository(connectionString));

                services.AddTransient<IPrecioProducto>(provider =>
                 new PrecioProductoRepository(connectionString));

                services.AddTransient<IPromociones>(provider =>
                 new PromocionesRepository(connectionString));

                services.AddTransient<IPromocionProducto>(provider =>
                 new PromocionProductoRepository(connectionString));

                services.AddTransient<ITipoPromocion>(provider =>
                 new TipoPromocionRepository(connectionString));

                services.AddTransient<IMedioPago>(provider =>
                new MedioPagoRepository(connectionString));

                services.AddTransient<IVendedor>(provider =>
                 new VendedorRepository(connectionString));

                services.AddTransient<IBanco>(provider =>
                 new BancoRepository(connectionString));

                services.AddTransient<IVenta>(provider =>
                new VentaRepository(connectionString));

                services.AddTransient<IParametros>(provider =>
                new ParametrosRepository(connectionString));

                services.AddTransient<ICaja>(provider =>
                new CajaRepository(connectionString));

                services.AddTransient<IUsuario>(provider =>
                new UsuarioRepository(connectionString));

                services.AddTransient<IRol>(provider =>
                new RolRepository(connectionString));

                services.AddTransient<IPermisos>(provider =>
                new PermisoRepository(connectionString));

                services.AddTransient<INotaCredito>(provider =>
                new NotaCreditoRepository(connectionString));

                services.AddTransient<IPagosEfectivo>(provider =>
                new PagosRepository(connectionString));

                services.AddTransient<IBackup>(provider =>
                new BackupRepository(connectionString));

                services.AddTransient<IReportes>(provider =>
                new ReportesRepository(connectionString));
            })
            .Build();

            ApplicationConfiguration.Initialize();
            var loginForm = host.Services.GetRequiredService<Login>();
            Application.Run(loginForm);
        }
    }
}