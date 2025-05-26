using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using InscripcionesWeb.DTOs;

namespace InscripcionesWeb.Services
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(InscripcionDTO inscripcion);
    }

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
            mensaje.To.Add(MailboxAddress.Parse(inscripcion.Correo));
            mensaje.Subject = "Inscripción exitosa";

            mensaje.Body = new TextPart("plain")
            {
                Text = $"Hola {inscripcion.Nombre}, tu inscripción al programa {inscripcion.NombrePrograma} fue registrada."
            };

            using var smtp = new SmtpClient();
            int puerto = int.Parse(_config["EmailSettings:SmtpPort"]);
            await smtp.ConnectAsync(_config["EmailSettings:SmtpHost"], puerto, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Usuario"], _config["EmailSettings:Clave"]);
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);
        }
    }
}

