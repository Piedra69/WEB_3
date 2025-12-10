using System.Collections.Generic;
using Gestion.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
    }
}
