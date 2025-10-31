using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BECobro
    {
        public int IdCobro { get; set; }
        public BEFactura Factura { get; set; }
        public BEPedido Pedido { get; set; }
        public BEPromociones Promocion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public BECobro()
        {
            Fecha = DateTime.Now;
        }
    }
}
