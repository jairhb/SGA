namespace InscripcionesWeb.DTOs
{
    public class EmailSettings
    {
        public string Remitente { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string SmtpHost { get; set; } = string.Empty;
    }
}
