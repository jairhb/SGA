namespace Inscripciones.Common.DTOs
{
    public class InscripcionDto
    {
        public int ProgramaId { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public string CorreoEstudiante { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
    }
}

//Para transferir entre microservicios mediante json
