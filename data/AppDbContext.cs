using Microsoft.EntityFrameworkCore;
using BolsaValores.Models;

namespace BolsaValores.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}