using System.ComponentModel.DataAnnotations;

namespace WAMVC.Models
{
    public class DetallePedidoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo IdPedido es obligatorio")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo IdProducto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }

        public PedidoModel Pedido { get; set; }
        public ProductoModel Producto { get; set; }
    }
}
