using System.ComponentModel.DataAnnotations;

namespace mvcProyect.Models
{
    public class ErrorViewModel
    {
        [Key] 
        public int Id { get; set; }

        [Display(Name = "ID de la Solicitud")]
        public string? RequestId { get; set; }

        [Display(Name = "Mostrar ID de Solicitud")]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        [Display(Name = "Mensaje de Error")]
        [StringLength(500, ErrorMessage = "El mensaje de error no puede superar los 500 caracteres")]
        public string? Mensaje { get; set; }

        [Display(Name = "Ruta donde ocurrió el error")]
        [StringLength(250, ErrorMessage = "La ruta no puede superar los 250 caracteres")]
        public string? Ruta { get; set; }

        [Display(Name = "Código de Error")]
        public int? Codigo { get; set; }
    }
}
