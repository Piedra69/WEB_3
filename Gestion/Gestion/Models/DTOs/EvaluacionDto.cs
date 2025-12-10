using System.ComponentModel.DataAnnotations;

namespace Gestion.Models.DTOs
{
    public class EvaluacionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe proporcionar un CursoId")]
       
        public int CursoId { get; set; }

        [Required(ErrorMessage = "Debe proporcionar un UserId")]
     
        public int UserId { get; set; }

 
        public double Nota { get; set; }
    }
}
