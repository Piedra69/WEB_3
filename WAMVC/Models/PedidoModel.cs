using System.ComponentModel.DataAnnotations;

namespace WAMVC.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio")]
        [Range(0.01, 1000000, ErrorMessage = "El monto total debe ser mayor que 0")]
        public decimal MontoTotal { get; set; }

        public ClienteModel Cliente { get; set; } // Un pedido pertenece a un solo cliente.

        public ICollection<DetallePedidoModel> DetallePedidos { get; set; } // Un pedido puede tener muchos detalles.
    }
}
