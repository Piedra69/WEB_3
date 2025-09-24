using System.ComponentModel.DataAnnotations;

namespace Practica.Models
{
    public class ErrorViewModel
    {
        [Required(ErrorMessage = "El RequestId es obligatorio.")]
        [StringLength(100, ErrorMessage = "El RequestId no puede superar los 100 caracteres.")]
        public string? RequestId { get; set; }

        // Propiedad calculada: devuelve true si RequestId tiene contenido
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Extra: puedes guardar el mensaje de error
        [StringLength(500, ErrorMessage = "El mensaje de error no puede superar los 500 caracteres.")]
        public string? MensajeError { get; set; }

        // Extra: código de estado HTTP (ej. 404, 500)
        [Range(100, 599, ErrorMessage = "El código de estado debe estar entre 100 y 599.")]
        public int? StatusCode { get; set; }
    }
}
