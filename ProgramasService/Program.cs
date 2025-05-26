using Microsoft.EntityFrameworkCore;
using ProgramasService.Data;
using Inscripciones.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("OracleDb");

// Registro del DbContext usando Oracle
builder.Services.AddDbContext<ProgramasDbContext>(options =>
    options.UseOracle(connectionString));

// Controladores y servicios HTTP
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Compilación del app
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();





