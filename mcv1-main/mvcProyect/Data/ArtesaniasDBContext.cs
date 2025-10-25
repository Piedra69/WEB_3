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
        public DbSet<HomeModel> HomeModels { get; set; } // opcional
        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
        // opcional
        public DbSet<Usuario> UsuarioModels { get; set; }

        // ✅ NUEVA TABLA DE USUARIOS
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Pedido ↔ Cliente (Many-to-One)
            modelBuilder.Entity<PedidoModel>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Pedido ↔ DetallePedido (One-to-Many)
            modelBuilder.Entity<PedidoModel>()
                .HasMany(p => p.DetallePedidos)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación DetallePedido ↔ Producto (Many-to-One)
            modelBuilder.Entity<DetallePedidoModel>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuraciones de tipo decimal
            modelBuilder.Entity<ProductoModel>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetallePedidoModel>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PedidoModel>()
                .Property(p => p.MontoDecimal)
                .HasColumnType("decimal(18,2)");

            // ✅ CONFIGURACIÓN OPCIONAL PARA USUARIOS
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.NombreCompleto); // OJO: No tienes ID, por lo que usamos NombreCompleto como PK
                entity.Property(u => u.NombreCompleto)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Rol)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.FechaRegistro)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
