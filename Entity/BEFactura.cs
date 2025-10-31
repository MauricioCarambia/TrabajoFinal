using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEFactura
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public BECliente Cliente { get; set; }
        public List<BEPedidoPlato> DetallePlatos { get; set; } = new List<BEPedidoPlato>();
        public BEPromociones PromocionAplicada { get; set; }
        public decimal DescuentoAplicado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
       

        // ✅ Nueva propiedad para vincular el pedido
        public BEPedido Pedido { get; set; }
        public BEFactura()
        {
            Fecha = DateTime.Now;
        }
    }
}
