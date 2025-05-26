using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InscripcionesService.Models
{
    [Table("INSCRIPCIONES", Schema = "INSCRIPCIONES_DB")]  // tabla y esquema exactos
    public class Inscripcion
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PROGRAMA_ID")]
        public int ProgramaId { get; set; }

        [Column("NOMBRE_ESTUDIANTE")]
        public string NombreEstudiante { get; set; } = string.Empty;

        [Column("CORREO_ESTUDIANTE")]
        public string CorreoEstudiante { get; set; } = string.Empty;

        [Column("FECHA_INSCRIPCION")]
        public DateTime? FechaInscripcion { get; set; }
    }
}



