using System.ComponentModel.DataAnnotations;

namespace Gestion.Models.DTOs
{
    public class RolDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }
    }
}