using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;


namespace Inscripciones.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(InscripcionDTO inscripcion)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Sistema de Inscripciones", _config["EmailSettings:Remitente"]));
            mensaje.To.Add(MailboxAddress.Parse(inscripcion.CorreoEstudiante));
            mensaje.Subject = "Inscripción exitosa";

            mensaje.Body = new TextPart("plain")
            {
                Text = $"Hola {inscripcion.NombreEstudiante}, tu inscripción al programa {inscripcion.NombrePrograma} fue registrada correctamente."
            };

            await EnviarAsync(mensaje);
        }

        public async Task EnviarCorreoConfirmacionPagoAsync(PagoConsultaDTO pago)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Sistema de Pagos", _config["EmailSettings:Remitente"]));
            mensaje.To.Add(MailboxAddress.Parse(pago.CorreoEstudiante));
            mensaje.Subject = "Confirmación de Pago";

            mensaje.Body = new TextPart("html")
            {
                Text = GenerarContenidoCorreoPago(pago)
            };

            await EnviarAsync(mensaje);
        }

        private async Task EnviarAsync(MimeMessage mensaje)
        {
            using var smtp = new SmtpClient();
            int puerto = int.Parse(_config["EmailSettings:SmtpPort"]);
            await smtp.ConnectAsync(_config["EmailSettings:SmtpHost"], puerto, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Usuario"], _config["EmailSettings:Clave"]);
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);
        }

        private string GenerarContenidoCorreoPago(PagoConsultaDTO pago)
        {
            return $@"
                <html>
                    <body>
                        <h2>Confirmación de Pago</h2>
                        <p>Hola <strong>{pago.NombreEstudiante}</strong>,</p>
                        <p>Tu pago ha sido registrado exitosamente para el programa <strong>{pago.NombrePrograma}</strong>.</p>
                        <ul>
                            <li><strong>Monto:</strong> {pago.Monto:C}</li>
                            <li><strong>Fecha de Pago:</strong> {pago.FechaPago:dd/MM/yyyy}</li>
                            <li><strong>Estado:</strong> {pago.Estado}</li>
                        </ul>
                        <p>Gracias por tu confianza.</p>
                        <p><em>Dirección de Admisiones y Registro</em></p>
                    </body>
                </html>";
        }
    }
}






