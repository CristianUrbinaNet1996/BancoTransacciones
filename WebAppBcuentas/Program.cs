using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.Configuration;
using WebAppBcuentas.Areas.EstadoCuentas.Interfaces;
using WebAppBcuentas.Areas.EstadoCuentas.Services;
using WebAppBcuentas.Automapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();  // Agregar soporte para TempData

builder.Services.AddScoped<IEstadoCuenta, SEstadoCuentas>();

builder.Services.AddAutoMapper(typeof(CoreMapper));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


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

app.UseAuthorization();

// Configurar uso de áreas (si usas áreas)
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Ruta específica para el área 'EstadoCuenta'
    endpoints.MapAreaControllerRoute(
        name: "estado_cuenta_route",
        areaName: "EstadoCuenta",
        pattern: "EstadoCuenta/{controller=EstadoCuenta}/{action=GetEstadoCuenta}/{idTarjeta?}");

    // Ruta predeterminada
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
