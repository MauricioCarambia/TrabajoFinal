using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPedido
    {
        public int Id { get; set; }
        public BECliente Cliente { get; set; }      // Cliente que realizó el pedido
        public BEReserva Reserva { get; set; }      // Reserva asociada (si aplica)
        public List<BEPedidoPlato> ListaPlatos { get; set; } = new List<BEPedidoPlato>();
        public EstadoPedido Estado { get; set; } = EstadoPedido.Abierto;
        public DateTime Fecha { get; set; }

        public enum EstadoPedido
        {
            Abierto,
            Cerrado
        }

        // Precio total calculado automáticamente
        public decimal Total => ListaPlatos.Sum(p => p.Subtotal);
    }
}
