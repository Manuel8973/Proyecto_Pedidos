namespace Proyecto_pedidos.Models
{
    public class pedido
    {
        public int Cliente { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
