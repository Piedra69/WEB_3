using Microsoft.EntityFrameworkCore;
using mvcProyect.Models;

namespace mvcProyect.Data
{
    public class ArtesaniasDBContext : DbContext
    {
        public ArtesaniasDBContext(DbContextOptions<ArtesaniasDBContext> options)
            : base(options)
        { }

        public DbSet<ProductoModel> Productos { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<DetallePedidoModel> DetallePedidos { get; set; }
        public DbSet<HomeModel> HomeModels { get; set; } // opcional si necesitas scaffolding para Home
        public DbSet<ErrorViewModel> ErrorViewModels { get; set; } // opcional

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Pedido ↔ Cliente (Many-to-One)
            modelBuilder.Entity<PedidoModel>()
                .HasOne(p => p.Cliente) // Un Pedido tiene un Cliente
                .WithMany(c => c.Pedidos) // Un Cliente tiene muchos Pedidos
                .HasForeignKey(p => p.ClienteId) // Clave foránea en PedidoModel
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Pedido ↔ DetallePedido (One-to-Many)
            modelBuilder.Entity<PedidoModel>()
                .HasMany(p => p.DetallePedidos) // Un Pedido tiene muchos DetallePedidos
                .WithOne(d => d.Pedido) // Un DetallePedido pertenece a un Pedido
                .HasForeignKey(d => d.PedidoId) // Clave foránea en DetallePedidoModel
                .OnDelete(DeleteBehavior.Cascade);

            // Relación DetallePedido ↔ Producto (Many-to-One)
            modelBuilder.Entity<DetallePedidoModel>()
                .HasOne(d => d.Producto) // Un DetallePedido tiene un Producto
                .WithMany(p => p.DetallePedidos) // Un Producto tiene muchos DetallePedidos
                .HasForeignKey(d => d.ProductoId) // Clave foránea en DetallePedidoModel
                .OnDelete(DeleteBehavior.Restrict); // Para no borrar productos si hay detalle

            // Configuraciones opcionales adicionales
            modelBuilder.Entity<ProductoModel>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetallePedidoModel>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PedidoModel>()
                .Property(p => p.MontoDecimal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
