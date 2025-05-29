using InscripcionesWeb.DTOs;
using Inscripciones.Common.Interfaces;
using InscripcionesWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Controladores y vistas
builder.Services.AddControllersWithViews();

// Cliente HTTP para ProgramasService
builder.Services.AddHttpClient<IProgramaService, ProgramaService>(client =>
{
    client.BaseAddress = new Uri("http://programas-service:8080"); // Ajustado para entorno Docker
});

// Cliente HTTP para inscripciones 
builder.Services.AddHttpClient("InscripcionesApi", client =>
{
    client.BaseAddress = new Uri("http://inscripciones-service:8080/");
});

// Servicio de Email
builder.Services.AddScoped<IEmailService, EmailService>();

// Configuración de EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




