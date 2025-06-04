using System;
using System.Threading.Tasks;
using Inscripciones.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Inscripciones.Common.Interfaces;

namespace PagosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        // Endpoint de prueba para verificar que el servicio está activo
        [HttpGet]
        public IActionResult Ping()
        {
            Console.WriteLine("📡 Ping recibido en PagosService");
            return Ok("PagosService activo ✅");
        }


        [HttpGet("por-inscripcion/{inscripcionId}")]
        public async Task<ActionResult<PagoConsultaDTO>> ObtenerPorInscripcion(int inscripcionId)
        {
         var pago = await _pagoService.ObtenerPagoPorInscripcionId(inscripcionId);

        if (pago == null)
        {
        return NotFound();
        }

        return Ok(pago);
        }

        // Endpoint para registrar un pago
        [HttpPost]
        public async Task<IActionResult> RegistrarPago([FromBody] PagoDTO pago)
        {
            Console.WriteLine("💳 Petición POST recibida en /api/pagos");

            if (pago == null)
            {
                Console.WriteLine("⚠️ Objeto de pago nulo.");
                return BadRequest("Los datos del pago no pueden ser nulos.");
            }

            try
            {
                await _pagoService.RegistrarPagoAsync(pago);
                Console.WriteLine($"✅ Pago registrado correctamente para inscripción ID: {pago.InscripcionId}, monto: {pago.Monto}");
                return Ok(new { mensaje = "Pago registrado correctamente ✅" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al registrar el pago: {ex.Message}");
                return StatusCode(500, $"❌ Error interno: {ex.Message}");
            }
        }
    }
}
