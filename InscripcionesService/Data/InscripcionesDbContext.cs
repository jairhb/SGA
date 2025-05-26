using Microsoft.EntityFrameworkCore;
using InscripcionesService.Models;

namespace InscripcionesService.Data
{
    public class InscripcionesDbContext : DbContext
    {
        public InscripcionesDbContext(DbContextOptions<InscripcionesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inscripcion> Inscripciones { get; set; }

        // No es necesario sobrescribir OnModelCreating si ya usas DataAnnotations
    }
}

