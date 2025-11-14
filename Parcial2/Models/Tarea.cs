using System.ComponentModel.DataAnnotations;

namespace Parcial2.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;
        [Required, MaxLength(300)]
        public string Descripion { get; set; } = string.Empty;
    
        public DateOnly FechaCreacion { get; set; }
      
        public int Estado { get; set; } 
        public int ResponsableId { get; set; }
    

        public Miembro Miembro {  get; set; }


    }

}
