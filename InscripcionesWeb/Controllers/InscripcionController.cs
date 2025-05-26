using Inscripciones.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InscripcionesWeb.Services;
using InscripcionesWeb.DTOs;

namespace InscripcionesWeb.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly IProgramaService _programaService;
        private readonly IEmailService _emailService;
        private readonly InscripcionService _inscripcionService;

        public InscripcionesController(IProgramaService programaService, IEmailService emailService, InscripcionService inscripcionService)
        {
            _programaService = programaService;
            _emailService = emailService;
            _inscripcionService = inscripcionService;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            Console.WriteLine("📥 GET /Inscripciones/Crear");

            var programas = await _programaService.ObtenerProgramas();

            if (programas == null || !programas.Any())
            {
                ViewBag.Error = "No hay programas disponibles.";
                return View();
            }

            ViewData["Programas"] = new SelectList(programas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(InscripcionDTO inscripcion)
        {
            Console.WriteLine("📨 POST /Inscripciones/Crear");

            if (!ModelState.IsValid)
            {
                var programas = await _programaService.ObtenerProgramas();
                ViewData["Programas"] = new SelectList(programas, "Id", "Nombre");
                return View(inscripcion);
            }

            try
            {
                var programas = await _programaService.ObtenerProgramas();
                var programaSeleccionado = programas.FirstOrDefault(p => p.Id == inscripcion.IdPrograma);
                inscripcion.NombrePrograma = programaSeleccionado?.Nombre ?? "Programa Desconocido";

                var inscripcionParaAPI = new
                {
                    nombreEstudiante = inscripcion.Nombre,
                    correoEstudiante = inscripcion.Correo,
                    programaId = inscripcion.IdPrograma,
                    fechaInscripcion = DateTime.Now
                };

                var client = new HttpClient();
                var response = await client.PostAsJsonAsync("http://localhost:5180/api/inscripciones", inscripcionParaAPI);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("❌ Error al llamar al microservicio de inscripciones");
                    ModelState.AddModelError("", "Error al registrar la inscripción.");
                    ViewData["Programas"] = new SelectList(programas, "Id", "Nombre");
                    return View(inscripcion);
                }

                await _emailService.EnviarCorreoAsync(inscripcion);
                Console.WriteLine($"✅ Correo enviado a {inscripcion.Correo}");

                // ✅ Correcto: redirige al método Gracias de este mismo controlador
                return RedirectToAction("Gracias");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error inesperado: {ex.Message}");
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                var programas = await _programaService.ObtenerProgramas();
                ViewData["Programas"] = new SelectList(programas, "Id", "Nombre");
                return View(inscripcion);
            }
        }

        [HttpGet]
        public IActionResult Gracias()
        {
            return View();
        }
    }
}







