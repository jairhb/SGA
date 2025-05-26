using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using Oracle.ManagedDataAccess.Client; // Necesario para usar OracleCommand

var builder = WebApplication.CreateBuilder(args);

// ✅ Cadena de conexión corregida
var connectionString = "User Id=USUARIOS_DB;Password=usuarios123;Data Source=localhost:1521/XEPDB1";

// Registrar UsuariosDbContext con Oracle
builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseOracle(connectionString));

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"Cadena de conexión: {connectionString}");

// 🧪 Consulta directa para verificar conexión y contar usuarios
try
{
    using (var connection = new OracleConnection(connectionString))
    {
        connection.Open();
        using (var command = new OracleCommand("SELECT COUNT(*) FROM USUARIOS", connection))
        {
            var count = command.ExecuteScalar();
            Console.WriteLine($"👥 Total de usuarios: {count}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al consultar la base de datos: {ex.Message}");
}

app.Run();