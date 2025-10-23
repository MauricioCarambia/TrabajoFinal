using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPedidoPlato
    {
        public int Id { get; set; }
        public BEPlato Plato { get; set; }
        public int Cantidad { get; set; }

        public EstadoPlato Estado { get; set; } = EstadoPlato.Pendiente;
        public enum EstadoPlato
        {
            Pendiente,
            Preparacion,
            Terminado,
            Entregado
        }

        // Precio total del plato en el pedido
        public decimal Subtotal => Plato.PrecioVenta * Cantidad;
    }
}
