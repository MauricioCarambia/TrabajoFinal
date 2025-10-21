using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEProveedorInsumo
    {
        public int Id { get; set; }
        public BEInsumo Insumo { get; set; }
        public BEProveedor Proveedor { get; set; }

        public decimal Precio { get; set; }     // Precio unitario al que el proveedor vende
        public decimal Cantidad { get; set; }         // Cantidad que comprás al proveedor
        public DateTime Fecha { get; set; }
    }
}
