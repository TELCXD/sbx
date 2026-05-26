using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using sbx.core.Interfaces;
using sbx.core.Interfaces.Categoria;
using sbx.core.Interfaces.EntradaInventario;
using sbx.core.Interfaces.Marca;
using sbx.core.Interfaces.Producto;
using sbx.core.Interfaces.SalidaInventario;
using sbx.core.Interfaces.Tributos;
using sbx.core.Interfaces.UnidadMedida;
using sbx.core.Interfaces.Banco;
using sbx.core.Interfaces.Cliente;
using sbx.core.Interfaces.ListaPrecios;
using sbx.core.Interfaces.MedioPago;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.PrecioCliente;
using sbx.core.Interfaces.PrecioProducto;
using sbx.core.Interfaces.PromocionProducto;
using sbx.core.Interfaces.RangoNumeracion;
using sbx.core.Interfaces.Vendedor;
using sbx.core.Interfaces.Venta;
using sbx.core.Interfaces.FechaVencimiento;
using sbx.repositories.Banco;
using sbx.repositories.Categorias;
using sbx.repositories.Cliente;
using sbx.repositories.EntradaInventario;
using sbx.repositories.ListaPrecios;
using sbx.repositories.LoginRepository;
using sbx.repositories.Marca;
using sbx.repositories.MedioPago;
using sbx.repositories.Parametros;
using sbx.repositories.PrecioCliente;
using sbx.repositories.PrecioProducto;
using sbx.repositories.Producto;
using sbx.repositories.PromocionProducto;
using sbx.repositories.RangoNumeracion;
using sbx.repositories.SalidaInventario;
using sbx.repositories.Tributos;
using sbx.repositories.UnidadMedida;
using sbx.repositories.Vendedor;
using sbx.repositories.Venta;
using sbx.repositories.FechaVecimiento;
using System.Collections.Concurrent;
using System.Security.Claims;
using Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "sbx_auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.LoginPath = "/login";
        options.LogoutPath = "/api/auth/logout";
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddTransient<ILogin>(_ => new LoginRepository(connectionString));
builder.Services.AddTransient<IProducto>(_ => new ProductoRepository(connectionString));
builder.Services.AddTransient<ICategoria>(_ => new CategoriaRepository(connectionString));
builder.Services.AddTransient<IMarca>(_ => new MarcaRepository(connectionString));
builder.Services.AddTransient<IUnidadMedida>(_ => new UnidadMedidaRepository(connectionString));
builder.Services.AddTransient<ITribute>(_ => new TributeRepository(connectionString));
builder.Services.AddTransient<IEntradaInventario>(_ => new EntradaInventarioRepository(connectionString));
builder.Services.AddTransient<ISalidaInventario>(_ => new SalidaInventarioRepository(connectionString));
builder.Services.AddTransient<IVenta>(_ => new VentaRepository(connectionString));
builder.Services.AddTransient<ICliente>(_ => new ClienteRepository(connectionString));
builder.Services.AddTransient<IListaPrecios>(_ => new ListaPreciosRepository(connectionString));
builder.Services.AddTransient<IVendedor>(_ => new VendedorRepository(connectionString));
builder.Services.AddTransient<IMedioPago>(_ => new MedioPagoRepository(connectionString));
builder.Services.AddTransient<IBanco>(_ => new BancoRepository(connectionString));
builder.Services.AddTransient<IPrecioProducto>(_ => new PrecioProductoRepository(connectionString));
builder.Services.AddTransient<IPrecioCliente>(_ => new PrecioClienteRepository(connectionString));
builder.Services.AddTransient<IPromocionProducto>(_ => new PromocionProductoRepository(connectionString));
builder.Services.AddTransient<IRangoNumeracion>(_ => new RangoNumeracionRepository(connectionString));
builder.Services.AddTransient<IParametros>(_ => new ParametrosRepository(connectionString));
builder.Services.AddTransient<IFechaVencimiento>(_ => new FechaVencimientoRepository(connectionString));
builder.Services.AddSingleton<LoginTokenStore>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/auth/signin/{token:guid}", async (Guid token, HttpContext httpContext, LoginTokenStore tokenStore) =>
{
    var userData = tokenStore.Consume(token);
    if (userData == null)
        return Results.Redirect("/login?error=invalid_token");

    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, userData.UserName),
        new(ClaimTypes.NameIdentifier, userData.IdUser.ToString()),
        new(ClaimTypes.Role, userData.Role),
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

    var returnUrl = string.IsNullOrEmpty(userData.ReturnUrl) ? "/" : userData.ReturnUrl;
    return Results.Redirect(returnUrl);
});

app.MapGet("/api/auth/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public class LoginTokenStore
{
    private readonly ConcurrentDictionary<Guid, UserData> _store = new();

    public Guid Add(UserData data)
    {
        var token = Guid.NewGuid();
        _store[token] = data;
        return token;
    }

    public UserData? Consume(Guid token)
    {
        _store.TryRemove(token, out var data);
        return data;
    }
}

public record UserData(int IdUser, string UserName, string Role, string? ReturnUrl);
