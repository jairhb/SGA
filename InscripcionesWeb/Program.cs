using InscripcionesWeb.DTOs;
using Inscripciones.Common.Interfaces;
using Inscripciones.Common.Services;
using InscripcionesWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// Servicios MVC
// ------------------------------
builder.Services.AddControllersWithViews();

// ------------------------------
// Configuración de URLs de APIs
// ------------------------------
var apisSection = builder.Configuration.GetSection("Apis");
builder.Services.Configure<ApiUrls>(apisSection);
var apiUrls = apisSection.Get<ApiUrls>();

// ------------------------------
// Configuración de HTTP Clients para microservicios
// ------------------------------
builder.Services.AddHttpClient<IProgramaService, ProgramaService>(client =>
{
    client.BaseAddress = new Uri(apiUrls.Programas);
});

builder.Services.AddHttpClient<IPagoService, PagoService>(client =>
{
    client.BaseAddress = new Uri(apiUrls.Pagos);
});

builder.Services.AddHttpClient("InscripcionesApi", client =>
{
    client.BaseAddress = new Uri(apiUrls.Inscripciones);
});


builder.Services.AddHttpClient<IProgramaService, ProgramaService>(client =>
{
    client.BaseAddress = new Uri(apiUrls.Programas);
});



// ------------------------------
// Configuración del servicio de correo
// ------------------------------
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// ------------------------------
// Construcción de la aplicación
// ------------------------------
var app = builder.Build();

// ------------------------------
// Middleware
// ------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// ------------------------------
// Rutas MVC por defecto
// ------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




