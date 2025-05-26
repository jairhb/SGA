using Microsoft.AspNetCore.Mvc;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;

namespace InscripcionesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionesController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] InscripcionDto inscripcion)
        {
            Console.WriteLine("📩 Petición POST recibida en /api/inscripciones");

            if (inscripcion == null)
            {
                Console.WriteLine("⚠️ El objeto de inscripción es nulo.");
                return BadRequest("Los datos de inscripción no son válidos.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠️ ModelState inválido.");
                return BadRequest(ModelState);
            }

            try
            {
                Console.WriteLine($"🔍 Registrando inscripción para: {inscripcion.NombreEstudiante}, ProgramaId: {inscripcion.ProgramaId}");

                await _inscripcionService.CrearInscripcionAsync(inscripcion);

                Console.WriteLine("✅ Inscripción registrada exitosamente.");
                return Ok(new { mensaje = "Inscripción registrada correctamente" });
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                Console.WriteLine($"❌ OracleException: {ex.Message}");
                return StatusCode(500, "Error en la base de datos Oracle.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error general: {ex.Message}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}






