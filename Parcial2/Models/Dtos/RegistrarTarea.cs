using System.ComponentModel.DataAnnotations;

namespace Parcial2.Models.Dtos
{
    public class RegistrarTarea
    {
        [Required, MaxLength(300)]
        public string Titulo { get; set; } = string.Empty;
    
        [Required, MaxLength(800)]
        public string Descripion { get; set; } = string.Empty;
        public int Estado {  get; set; }
        
    }
    public class UpdateTarea
    {
        public int Id { get; set; }
        [Required, MaxLength(300)]
        public string Titulo { get; set; } = string.Empty;

        [Required, MaxLength(800)]
        public int Descripion { get; set; } 

    }
}

