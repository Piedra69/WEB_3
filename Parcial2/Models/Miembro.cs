namespace Parcial2.Models
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;

        public int rol {  get; set; }
        public ICollection<Tarea> Tareas { get; set; }
    }
}
