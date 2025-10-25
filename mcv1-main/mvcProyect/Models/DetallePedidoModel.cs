using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcProyect.Models
{
    public class DetallePedidoModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Pedido")]
        [Required(ErrorMessage = "El pedido asociado es obligatorio")]
        public int PedidoId { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El producto es obligatorio")]
        public int ProductoId { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, 1000, ErrorMessage = "La cantidad debe estar entre 1 y 1000")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, 10000, ErrorMessage = "El precio unitario debe estar entre 0.01 y 10,000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación (nullable)
        public PedidoModel? Pedido { get; set; }
        public ProductoModel? Producto { get; set; }

        [Display(Name = "Subtotal")]
        [NotMapped] // Indica que no se mapea a la base de datos
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}