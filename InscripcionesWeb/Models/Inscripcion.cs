using System.ComponentModel.DataAnnotations;

namespace InscripcionesWeb.Models
{
    public class Inscripcion
    {
        [Key]
        public int Id { get; set; }
        public int ProgramaId { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public string CorreoEstudiante { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
    }
}

