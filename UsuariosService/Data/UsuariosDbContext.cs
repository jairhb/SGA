using Microsoft.EntityFrameworkCore;
using UsuariosService.Models;

namespace UsuariosService.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}