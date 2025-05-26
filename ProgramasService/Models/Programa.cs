using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProgramasService.Models;

namespace ProgramasService.Models
{
    [Table("PROGRAMAS")]
    public class Programa
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NOMBRE")]
        public string Nombre { get; set; } = string.Empty;

        [Column("DESCRIPCION")]
        public string? Descripcion { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime? FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public DateTime? FechaFin { get; set; }
    }
}