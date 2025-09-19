using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practica.Models;

namespace Practica.Data
{
    public class PracticaContext : DbContext
    {
        public PracticaContext (DbContextOptions<PracticaContext> options)
            : base(options)
        {
        }

        public DbSet<Practica.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<Practica.Models.DetallePedido> DetallePedido { get; set; } = default!;
        public DbSet<Practica.Models.Pedido> Pedido { get; set; } = default!;
        public DbSet<Practica.Models.Producto> Producto { get; set; } = default!;
        public DbSet<Practica.Models.Usuario> Usuario { get; set; } = default!;
    }
}
