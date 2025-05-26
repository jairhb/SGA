using System.ComponentModel.DataAnnotations;

namespace InscripcionesWeb.DTOs
{
    public class InscripcionDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un programa")]
        public int IdPrograma { get; set; }

        public string NombrePrograma { get; set; } = string.Empty; // opcional
    }
}
