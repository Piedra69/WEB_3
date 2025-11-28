using System.Data;

namespace Gestion.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        public int CursoId { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
