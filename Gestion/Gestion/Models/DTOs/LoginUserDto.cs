using System.ComponentModel.DataAnnotations;

namespace Gestion.Models.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no valido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
