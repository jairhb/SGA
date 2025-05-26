using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosService.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public required string Nombre { get; set; }

        [Column("EMAIL")]
        public required string Correo { get; set; }
    }
}