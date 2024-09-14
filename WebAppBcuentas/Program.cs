using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.Configuration;
using WebAppBcuentas.Areas.EstadoCuentas.Interfaces;
using WebAppBcuentas.Areas.EstadoCuentas.Services;
using WebAppBcuentas.Automapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppBcuentas.Areas.Transacciones.Interface;
using WebAppBcuentas.Areas.Transacciones.Services;
using WebAppBcuentas.Areas.Cliente.Interfaces;
using WebAppBcuentas.Areas.Cliente.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();  // Agregar soporte para TempData

builder.Services.AddScoped<IEstadoCuenta, SEstadoCuentas>();
builder.Services.AddScoped<ITransaccion,STransaccion>();
builder.Services.AddScoped<ICliente, SCliente>();

builder.Services.AddAutoMapper(typeof(CoreMapper));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "tu_issuer", // Cambia esto por tu emisor
        ValidAudience = "tu_audience", // Cambia esto por tu audiencia
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta")) // Llave secreta
    };
    

});

builder.Services.AddHttpClient("API", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlApi"));
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddMvcCore().AddRazorRuntimeCompilation();

builder.Services.AddMvc()
    .AddRazorOptions(options =>
    {
        // Añade o asegúrate de que las rutas a las áreas estén configuradas
        options.AreaViewLocationFormats.Clear();
        options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
        options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
        options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
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
//app.UseAuthorization();

// Configurar uso de áreas (si usas áreas)

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
     name: "area",
     areaName:"EstadoCuenta",
     pattern: "{controller=EstadoCuenta}/{action=Index}/{id?}"
   );
    endpoints.MapControllerRoute(
      name: "area",
      pattern: "{area:exists}/{controller=EstadoCuenta}/{action=Index}/{id?}"
    );
  
});

app.MapRazorPages();

app.Run();
