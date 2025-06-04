using Microsoft.AspNetCore.Mvc;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace InscripcionesService.Controllers
{   //api para la inscripcion de aspirantes
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
        public async Task<IActionResult> Crear([FromBody] InscripcionDTO inscripcion)
        {
            Console.WriteLine("📩 Petición POST recibida en /api/inscripciones");

            if (inscripcion == null)
            {
                Console.WriteLine("⚠️ El objeto de inscripción es nulo.");
                return BadRequest("El objeto enviado es nulo.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠️ ModelState inválido.");
                return BadRequest(ModelState);
            }

            try
            {
                Console.WriteLine($"🔍 Registrando inscripción para: {inscripcion.NombreEstudiante} al programa ID: {inscripcion.ProgramaId}");

                int inscripcionId = await _inscripcionService.CrearInscripcionAsync(inscripcion);

                Console.WriteLine($"✅ Inscripción registrada exitosamente. ID generado: {inscripcionId}");

                // ✅ Retornar un JSON con clave 'id' para que el frontend pueda deserializarlo fácilmente
                return Ok(new { id = inscripcionId });
            }
            catch (OracleException ex)
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









