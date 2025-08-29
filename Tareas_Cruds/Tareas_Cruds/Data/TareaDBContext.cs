using Microsoft.EntityFrameworkCore;
using Tareas_Cruds.Models;
using Tareas_Cruds.Models;

namespace Tareas_Cruds.Data
{
    public class TareaDbContext : DbContext
    {
        public TareaDbContext(DbContextOptions<TareaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
    }
}
