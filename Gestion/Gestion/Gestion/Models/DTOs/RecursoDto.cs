namespace Gestion.Models.DTOs
{
    public class RecursoDto
    {
        public int Id { get; set; }      
        public int CursoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int UserId { get; set; }    
    }
}
