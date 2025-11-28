namespace Gestion.Models
{
    public class Evaluacion
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public int UserId { get; set; } 

        public double Nota { get; set; }
    }
}
