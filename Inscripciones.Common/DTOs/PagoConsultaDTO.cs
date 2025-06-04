using System;
namespace Inscripciones.Common.DTOs;

    public class PagoConsultaDTO
{
    public int Id { get; set; }
    public int InscripcionId { get; set; }
    public decimal Monto { get; set; }
    public DateTime? FechaLimite { get; set; }
    public string Estado { get; set; } = string.Empty;

    // Propiedades necesarias para el EmailService
    public string CorreoEstudiante { get; set; } = string.Empty;
    public string NombreEstudiante { get; set; } = string.Empty;
    public string NombrePrograma { get; set; } = string.Empty;
    public DateTime? FechaPago { get; set; }
}


