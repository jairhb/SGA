using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using InscripcionesService.Data;
using Inscripciones.Common.Interfaces;
using InscripcionesService.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("OracleDb");

// Registrar el contexto con Oracle
builder.Services.AddDbContext<InscripcionesDbContext>(options =>
    options.UseOracle(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Probar conexión (opcional)
try
{
    using var conn = new OracleConnection(connectionString);
    conn.Open();
    Console.WriteLine("✅ Conexión a Oracle exitosa.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al conectar a Oracle: {ex.Message}");
}

app.Run();
