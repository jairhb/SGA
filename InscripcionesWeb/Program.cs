using InscripcionesWeb.DTOs;
using Inscripciones.Common.Interfaces;
using ProgramasService.Data;
using InscripcionesWeb.Services; // Asegúrate que este namespace sea correcto

var builder = WebApplication.CreateBuilder(args);

// Controladores y vistas
builder.Services.AddControllersWithViews();

// Cliente HTTP para ProgramasService
builder.Services.AddHttpClient<IProgramaService, ProgramaService>();
builder.Services.AddHttpClient<InscripcionService>();

// Servicio de Email
builder.Services.AddScoped<EmailService>();
builder.Services.AddTransient<IEmailService, EmailService>();

// Configuración de EmailSettings desde appsettings.json
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



