using Microsoft.EntityFrameworkCore;
using ProgramasService.Models;

namespace ProgramasService.Data
{
    public class ProgramasDbContext : DbContext
    {
        public ProgramasDbContext(DbContextOptions<ProgramasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Programa> Programas { get; set; }
    }
}
