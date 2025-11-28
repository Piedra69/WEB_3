using Microsoft.EntityFrameworkCore;
using Parcial2.Models;

namespace Parcial2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarea> Tareas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
