using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using UsuariosService.Data;

var builder = WebApplication.CreateBuilder(args);

// 👉 Conexión Oracle desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("OracleDb");

// 👉 DbContext
builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseOracle(connectionString));

// 👉 Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "UsuariosService",
        Version = "v1",
        Description = "API para gestión de usuarios"
    });
});

var app = builder.Build();

// 👉 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// 👉 Prueba de conexión (opcional)
try
{
    using var connection = new OracleConnection(connectionString);
    connection.Open();
    using var cmd = new OracleCommand("SELECT COUNT(*) FROM USUARIOS", connection);
    var count = cmd.ExecuteScalar();
    Console.WriteLine($"👤 Total de usuarios: {count}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al conectar a Oracle: {ex.Message}");
}

app.Run();
