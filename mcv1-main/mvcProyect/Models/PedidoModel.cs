using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace mvcProyect.Models
{
    public class PedidoModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fecha del Pedido")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Debe ingresar la fecha del pedido")]
        public DateTime FechaPedido { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        public int ClienteId { get; set; }

        [Display(Name = "Estado del Pedido")]
        [Required(ErrorMessage = "Debe ingresar el estado del pedido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El estado debe tener entre 3 y 50 caracteres")]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "Monto Total")]
        [Required(ErrorMessage = "Debe ingresar el monto")]
        [Range(0.01, 1000000, ErrorMessage = "El monto debe estar entre 0.01 y 1,000,000")]
        public decimal MontoDecimal { get; set; }

        // Propiedades de navegación
        public ClienteModel? Cliente { get; set; }
        public ICollection<DetallePedidoModel>? DetallePedidos { get; set; }

        [Display(Name = "Total Detalles")]
        public decimal TotalDetalles => DetallePedidos?.Sum(d => d.Subtotal) ?? MontoDecimal;
    }
}