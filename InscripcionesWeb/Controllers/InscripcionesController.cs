using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace InscripcionesWeb.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly IProgramaService _programaService;
        private readonly IEmailService _emailService;
        private readonly IPagoService _pagoService;
        private readonly IHttpClientFactory _httpClientFactory;

        public InscripcionesController(
            IProgramaService programaService,
            IEmailService emailService,
            IPagoService pagoService,
            IHttpClientFactory httpClientFactory)
        {
            _programaService = programaService;
            _emailService = emailService;
            _pagoService = pagoService;
            _httpClientFactory = httpClientFactory;
        }
        //con este gestiono los programas de la base de datos
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var programas = await _programaService.ObtenerProgramas();
            ViewBag.Programas = new SelectList(programas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(InscripcionDTO inscripcionForm)
        {
            if (!ModelState.IsValid)
            {
                var programas = await _programaService.ObtenerProgramas();
                ViewBag.Programas = new SelectList(programas, "Id", "Nombre");
                return View(inscripcionForm);
            }

            var inscripcionApi = new InscripcionDTO
            {
                ProgramaId = inscripcionForm.ProgramaId,
                NombreEstudiante = inscripcionForm.NombreEstudiante,
                CorreoEstudiante = inscripcionForm.CorreoEstudiante,
                FechaInscripcion = DateTime.UtcNow
            };

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://inscripciones-service:8080/api/inscripciones", inscripcionApi);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("📥 Respuesta JSON del microservicio: " + jsonResponse);

                int idGenerado = 0;

                try
                {
                    var jsonDoc = JsonDocument.Parse(jsonResponse);
                    idGenerado = jsonDoc.RootElement.GetProperty("id").GetInt32();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("⚠️ Error al extraer ID del JSON: " + ex.Message);
                }

                await _emailService.EnviarCorreoAsync(new InscripcionDTO
                {
                    ProgramaId = inscripcionApi.ProgramaId,
                    NombreEstudiante = inscripcionApi.NombreEstudiante,
                    CorreoEstudiante = inscripcionApi.CorreoEstudiante,
                    NombrePrograma = inscripcionForm.NombrePrograma
                });

                // ✅ Datos para la vista de confirmación
                ViewBag.NombreEstudiante = inscripcionForm.NombreEstudiante;
                ViewBag.NombrePrograma = inscripcionForm.NombrePrograma;
                ViewBag.InscripcionId = idGenerado;

                return View("Gracias");
            }

            ViewBag.Error = "Error al registrar la inscripción.";
            return View("Error");
        }
    }
}














