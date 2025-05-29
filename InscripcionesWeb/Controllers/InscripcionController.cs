using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inscripciones.Common.Interfaces;
using Inscripciones.Common.DTOs;
using InscripcionesWeb.DTOs;
using InscripcionesWeb.Services;

namespace InscripcionesWeb.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly IProgramaService _programaService;
        private readonly IEmailService _emailService;

        public InscripcionesController(IProgramaService programaService, IEmailService emailService)
        {
            _programaService = programaService;
            _emailService = emailService;
        }

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

            var inscripcionApi = new InscripcionDto
            {
                ProgramaId = inscripcionForm.IdPrograma,
                NombreEstudiante = inscripcionForm.Nombre,
                CorreoEstudiante = inscripcionForm.Correo,
                FechaInscripcion = DateTime.UtcNow
            };

            try
            {
                using var client = new HttpClient();
                var response = await client.PostAsJsonAsync("http://inscripciones-service:8080/api/inscripciones", inscripcionApi);

                if (response.IsSuccessStatusCode)
                {
                    // Recuperar el nombre del programa para el correo
                    var programas = await _programaService.ObtenerProgramas();
                    var programaSeleccionado = programas.FirstOrDefault(p => p.Id == inscripcionForm.IdPrograma);
                    inscripcionForm.NombrePrograma = programaSeleccionado?.Nombre ?? "";

                    try
                    {
                        await _emailService.EnviarCorreoAsync(inscripcionForm);
                        Console.WriteLine("📧 Correo de confirmación enviado.");
                    }
                    catch (Exception exCorreo)
                    {
                        Console.WriteLine("❌ Error al enviar correo: " + exCorreo.Message);
                    }

                    return RedirectToAction("Gracias");
                }
                else
                {
                    ViewBag.Error = "Error al registrar la inscripción. El servidor devolvió un estado: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Excepción al registrar la inscripción: " + ex.Message;
            }

            var programasError = await _programaService.ObtenerProgramas();
            ViewBag.Programas = new SelectList(programasError, "Id", "Nombre");

            return View(inscripcionForm);
        }

        public IActionResult Gracias()
        {
            ViewData["Title"] = "Inscripción Exitosa";
            return View();
        }
    }
}









