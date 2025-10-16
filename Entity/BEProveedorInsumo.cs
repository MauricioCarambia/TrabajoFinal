using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEProveedorInsumo
    {
        public int IdProveedor { get; set; }
        public int IdInsumo { get; set; }
        public decimal PrecioCompra { get; set; } // Precio por unidad (kg, litro, etc.)
        public int CantidadPorUnidad { get; set; } // Ejemplo: bolsa de 5 kg

        public BEProveedor Proveedor { get; set; }
        public BEInsumo Insumo { get; set; }
    }
}
