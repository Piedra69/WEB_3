using System.ComponentModel.DataAnnotations;

namespace Gestion.Models.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es obligatoria")]
        [MinLength(6, ErrorMessage = "La contrasena debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe especificar un Rol")]
        public int RolId { get; set; }
    }
}
