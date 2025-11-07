using EJERCICIO04112025.models;
using Microsoft.EntityFrameworkCore;

namespace EJERCICIO04112025.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
