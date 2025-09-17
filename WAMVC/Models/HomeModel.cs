using System.ComponentModel.DataAnnotations;

namespace WAMVC.Models
{
    public class HomeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(500, ErrorMessage = "El mensaje no puede tener más de 500 caracteres")]
        public string? Mensaje { get; set; }

        [Required(ErrorMessage = "El destinatario es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string? Destinatario { get; set; }
    }
}
