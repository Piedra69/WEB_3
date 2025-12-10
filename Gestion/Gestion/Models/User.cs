using System.ComponentModel.DataAnnotations;

namespace Gestion.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Debe especificar un RolId")]
        public int RolId { get; set; }
        public Rol? Rol { get; set; }
    }
}
