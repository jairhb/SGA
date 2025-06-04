using System.ComponentModel.DataAnnotations;

namespace Inscripciones.Common.DTOs
{
    public class InscripcionDTO
    {
        public int Id { get; set; }  //  Esta propiedad es la que falta

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreEstudiante { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string CorreoEstudiante { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un programa")]
        public int ProgramaId { get; set; }

        public string NombrePrograma { get; set; } = string.Empty; // Opcional
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;
    }
}



