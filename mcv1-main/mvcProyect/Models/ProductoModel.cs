using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvcProyect.Models
{
    public class ProductoModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "Debe ingresar el nombre del producto")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 150 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción del Producto")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 500 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage = "Debe ingresar el precio")]
        [Range(0.01, 100000, ErrorMessage = "El precio debe estar entre 0.01 y 100,000")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock Disponible")]
        [Required(ErrorMessage = "Debe ingresar el stock")]
        [Range(0, 10000, ErrorMessage = "El stock debe estar entre 0 y 10,000")]
        public int Stock { get; set; }

        public ICollection<DetallePedidoModel>? DetallePedidos { get; set; }
    }
}
