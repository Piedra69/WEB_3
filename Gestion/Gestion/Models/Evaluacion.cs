using System.ComponentModel.DataAnnotations;

namespace Gestion.Models
{
    public class Evaluacion
    {
        public int Id { get; set; } 
        [Required]
        public int CursoId { get; set; }
        [Required]
        public int UserId { get; set; }
        public Curso? Curso { get; set; }  
        public User? User { get; set; }   
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20")]
        public double Nota { get; set; }
    }
}
