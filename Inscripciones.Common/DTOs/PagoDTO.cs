using Inscripciones.Common.Interfaces;
namespace Inscripciones.Common.DTOs
{
    public class PagoDTO
    {
        public int InscripcionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; } = "pendiente";
    }
}

