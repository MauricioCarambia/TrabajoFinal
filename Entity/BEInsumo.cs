using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEInsumo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; } 
        public decimal StockActual { get; set; }

        public List<BEProveedorInsumo> ListaProveedores { get; set; } = new List<BEProveedorInsumo>();
    }
}
