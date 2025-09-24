using System.ComponentModel.DataAnnotations;

namespace Practica.Models
{
    public class Cliente
    {

            public int ClienteId { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
            [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
            public string Email { get; set; }

            public ICollection<Pedido> Pedidos { get; set; }
        }
    }
