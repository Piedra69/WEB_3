namespace Gestion.Models.DTOs
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int RolId { get; set; }
    }
}
