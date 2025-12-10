using System.ComponentModel.DataAnnotations;

namespace Gestion.Models
{
    public class Curso
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]   
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
