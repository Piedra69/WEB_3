namespace Practica.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime FechaPedido { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
    }
}