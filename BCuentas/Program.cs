using BCuentas.Helpers.Validators;
using Core.Infraestructure.Automapper;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using Core.Infraestructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus.Client;
using Prometheus.Client.AspNetCore;
using Serilog;
using System.Linq;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api BCuentas",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Cristian Urbina",
            Email = "eduardourbina119@gmail.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(CoreMapper));
var connectionString = builder.Configuration.GetConnectionString("Database");

// DBContext
builder.Services.AddDbContext<BcuentasContext>(options => options.UseSqlServer(connectionString));

// Registrar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<EstadoCuentaRequestDtoValidator>();

// Registro del UnitOfWork genérico
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWorkRepository<>));
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<ITarjetaRepository, TarjetasRepository>();
builder.Services.AddScoped<IEstadoCuentaRepository, EstadoCuentaRepository>();
builder.Services.AddScoped<IParametrosConfiguracionRepository, ParametrosConfiguracionRepository>();
builder.Services.AddScoped<ITransaccionesRepository, TransaccionesRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();    

// Configura Serilog desde appsettings.json
try
{
    Serilog.Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .CreateLogger();

    builder.Host.UseSerilog();
}
catch (Exception ex)
{
    Console.WriteLine("Error initializing Serilog: " + ex.Message);
    if (ex.InnerException != null)
    {
        Console.WriteLine("Inner exception: " + ex.InnerException.Message);
    }
}

// Configuración de HealthChecks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<BcuentasContext>()
    .AddDiskStorageHealthCheck(options => options.AddDrive("C:\\", 1024));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    // Configuración de HealthChecks
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description,
                    duration = entry.Value.Duration
                })
            });
            await context.Response.WriteAsync(result);
        }
    });

    // Exponer métricas usando Prometheus

});

app.Run();