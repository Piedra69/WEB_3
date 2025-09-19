using System.ComponentModel.DataAnnotations;

namespace Practica.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [StringLength(20)]
        public string Rol { get; set; }  // Valores: "admin", "cliente", "empleado"
    }
}