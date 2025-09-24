using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practica.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Pedido")]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser válido.")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [MinLength(1, ErrorMessage = "El pedido debe contener al menos un producto.")]
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        // Extra: propiedad calculada (no se guarda en DB si usas [NotMapped])
        [Display(Name = "Monto Total")]
        public decimal MontoTotal => Detalles?.Sum(d => d.Cantidad * d.PrecioUnitario) ?? 0;
    }
}
