namespace Gestion.Models.DTOs
{
    public class EvaluacionDto
    {
        public int Id { get; set; }       
        public int CursoId { get; set; }
        public int UserId { get; set; }   
        public double Nota { get; set; }
    }
}
