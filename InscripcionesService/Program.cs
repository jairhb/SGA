using Microsoft.EntityFrameworkCore;
using InscripcionesService.Data;
using Inscripciones.Common.Interfaces; // <- Para IInscripcionService
using InscripcionesService.Services;   // <- Para InscripcionService

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión Oracle
var connectionString = builder.Configuration.GetConnectionString("OracleDb");

builder.Services.AddDbContext<InscripcionesDbContext>(options =>
    options.UseOracle(connectionString));

// Servicios adicionales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInscripcionService, InscripcionService>(); // Registro correcto

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

app.Run();
