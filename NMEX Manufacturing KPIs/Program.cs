using Microsoft.AspNetCore.Identity;
using NMEX_Manufacturing_KPIs.Services;
using NMEX_Manufacturing_KPIs.Models.Module_User;
using DapperLoginCRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


var builder = WebApplication.CreateBuilder(args);

//Configurar authorize de forma global
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IServicioUsuario, ServicioUsuario>();
builder.Services.AddTransient<IRepositorioInventory, RepositorioInventory>();
builder.Services.AddTransient<IRepositorioNetwork, RepositorioNetwork>();
builder.Services.AddTransient<IRepositorioAgingHardware, RepositorioAgingHardware>();
builder.Services.AddTransient<IRepositorioAgingSoftware, RepositorioAgingSoftware>();
builder.Services.AddTransient<IRepositorioSecurity, RepositorioSecurity>();
builder.Services.AddTransient<IRepositorioUsers, RepositorioUsers>();
builder.Services.AddAutoMapper(typeof(Program));//Configuracion de AutoMapper
builder.Services.AddHttpContextAccessor();
//Identity
builder.Services.AddTransient<SignInManager<Users>>();
builder.Services.AddTransient<IUserStore<Users>, UsersStore>();
builder.Services.AddIdentityCore<Users>(opciones =>
{
    opciones.Password.RequiredLength = 0;
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
}).AddErrorDescriber<MensajesDeErrorIdentity>();

//Configuracion de la Cookie
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/users/login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
