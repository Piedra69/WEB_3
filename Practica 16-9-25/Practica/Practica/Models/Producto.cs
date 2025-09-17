namespace Practica.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public ICollection<DetallePedido> DetallesPedido { get; set; }
    }

}
