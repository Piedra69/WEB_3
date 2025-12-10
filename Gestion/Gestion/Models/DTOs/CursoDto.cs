using System.ComponentModel.DataAnnotations;

namespace Gestion.Models.DTOs
{
    public class CursoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
       
        public string Titulo { get; set; } 

        [Required(ErrorMessage = "La descripción es obligatoria")]
      
        public string Descripcion { get; set; } 

        [Required(ErrorMessage = "Debe proporcionar un UserId")]
      
        public int UserId { get; set; }
    }
}
