using Microsoft.EntityFrameworkCore;
using Tareas_Cruds.Models;
namespace Tareas_Cruds.Data
{
    public class TareaDBContext : DbContext
    {
        public TareaDBContext(DbContextOptions<TareaDBContext> options) : base(options)
        {
        }
        public DbSet<Tarea> Tareas {  get; set; }
        protected TareaDBContext() 
        { 
        }


    } 
}

