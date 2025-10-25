using System.ComponentModel.DataAnnotations;

namespace mvcProyect.Models
{
    public class HomeModel
    {
        [Key] 
        public int Id { get; set; }

        [Display(Name = "ID de la Solicitud")]
        public string? RequestId { get; set; }

        [Display(Name = "Mostrar ID de Solicitud")]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        [Display(Name = "Mensaje de Bienvenida")]
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "El mensaje debe tener entre 5 y 200 caracteres")]
        public string Mensaje { get; set; } = string.Empty;
    }
}
