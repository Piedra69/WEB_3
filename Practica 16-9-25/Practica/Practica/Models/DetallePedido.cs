using System.ComponentModel.DataAnnotations;

namespace Practica.Models
{
    public class DetallePedido
    {
        [Key]
        public int DetallePedidoId { get; set; }

        [Required(ErrorMessage = "El campo PedidoId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un pedido válido.")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "El campo ProductoId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto válido.")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, 1000, ErrorMessage = "La cantidad debe ser al menos 1 y como máximo 1000.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }

        // Campo calculado (opcional, no mapeado a DB si lo marcas con [NotMapped])
        [Display(Name = "Subtotal")]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
