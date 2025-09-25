using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Practica.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
        [Range(1, int.MaxValue, ErrorMessage = "ClienteId inválido.")]
        public int ClienteId { get; set; }

        // Navegaciones: no validarlas y evitar nulls
        [ValidateNever]
        public Cliente? Cliente { get; set; }

        [ValidateNever]
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        [Display(Name = "Monto Total")]
        public decimal MontoTotal => Detalles?.Sum(d => d.Cantidad * d.PrecioUnitario) ?? 0m;
    }
}
