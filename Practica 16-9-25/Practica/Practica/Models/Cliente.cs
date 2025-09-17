namespace Practica.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}