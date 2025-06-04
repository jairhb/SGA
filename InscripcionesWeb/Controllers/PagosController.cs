using Microsoft.AspNetCore.Mvc;
using Inscripciones.Common.Interfaces;
using Inscripciones.Common.DTOs;
using System;
using System.Threading.Tasks;

namespace InscripcionesWeb.Controllers
{
    public class PagosController : Controller
    {
        private readonly IPagoService _pagoService;
        private readonly IEmailService _emailService;

        public PagosController(IPagoService pagoService, IEmailService emailService)
        {
            _pagoService = pagoService;
            _emailService = emailService;
        }

        // GET: Muestra los detalles del pago por inscripción
        [HttpGet]
        public async Task<IActionResult> Detalle(int inscripcionId)
        {
            // Verifica si ya existe un pago
            var pago = await _pagoService.ObtenerPagoPorInscripcionId(inscripcionId);

            if (pago == null)
            {
                // Crear automáticamente
                var nuevoPago = new PagoDTO
                {
                    InscripcionId = inscripcionId,
                    Monto = 150000,
                    FechaPago = DateTime.UtcNow,
                    Estado = "pendiente"
                };

                await _pagoService.RegistrarPagoAsync(nuevoPago);

                // Marcar en TempData para mostrar alerta
                TempData["PagoGenerado"] = true;

                // Volver a consultar el pago recién creado
                pago = await _pagoService.ObtenerPagoPorInscripcionId(inscripcionId);
            }

            if (pago == null)
            {
                ViewBag.Error = $"Ocurrió un error al crear o consultar el pago para la inscripción #{inscripcionId}.";
                return View("Error");
            }

            return View("Detalle", pago);
        }

        // POST: Simula la confirmación del pago (por ejemplo, desde un botón)
        [HttpPost]
        public async Task<IActionResult> ConfirmarPago(int inscripcionId)
        {
            var nuevoPago = new PagoDTO
            {
                InscripcionId = inscripcionId,
                Monto = 150000,
                FechaPago = DateTime.UtcNow,
                Estado = "pagado"
            };

            await _pagoService.RegistrarPagoAsync(nuevoPago);

            var pagoRegistrado = await _pagoService.ObtenerPagoPorInscripcionId(inscripcionId);

            if (pagoRegistrado == null)
            {
                ViewBag.Error = "El pago no se pudo registrar correctamente.";
                return View("Error");
            }

            // (Opcional) Enviar notificación
            // await _emailService.EnviarCorreoConfirmacionPagoAsync(pagoRegistrado);

            return View("ConfirmacionPago", pagoRegistrado); // Vista clara de confirmación
        }

        // GET: Mensaje de gracias opcional
        [HttpGet]
        public IActionResult Gracias()
        {
            ViewBag.Mensaje = "Gracias por tu inscripción. Puedes realizar el pago cuando estés listo.";
            return View();
        }
    }
}






