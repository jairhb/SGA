using Inscripciones.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using Microsoft.OpenApi.Models;
using PagosService.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PagosService API", Version = "v1" });
});


// Registrar el servicio de pagos
builder.Services.AddScoped<IPagoService, PagoService>();

// Agregar configuración como singleton
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Validar conexión a Oracle (opcional)
try
{
    using var conn = new OracleConnection(builder.Configuration.GetConnectionString("OracleDb"));
    conn.Open();
    Console.WriteLine("✅ Conexión a Oracle exitosa.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al conectar a Oracle: {ex.Message}");
}

app.Run();
