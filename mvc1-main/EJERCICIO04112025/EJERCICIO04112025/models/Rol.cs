using System.ComponentModel.DataAnnotations;

namespace EJERCICIO04112025.models
{
    public class Rol
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public int tipo { get; set; }
        [Required]
        public string name { get; set; } = string.Empty;
        [Required]
        public bool estado { get; set; }

    }
}
