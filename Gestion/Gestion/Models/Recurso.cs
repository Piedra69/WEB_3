using System.ComponentModel.DataAnnotations;

namespace Gestion.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe especificar un CursoId")]
        public int CursoId { get; set; }
        [Required(ErrorMessage = "Debe especificar un nombre para el recurso")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe especificar un UserId")]
        public int UserId { get; set; }
        [Required]
        public Curso Curso { get; set; } = null!;
        [Required]
        public User User { get; set; } = null!;
    }
}
