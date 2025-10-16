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
        public UnidadesMedida UnidadMedida { get; set; } 
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }

        public enum UnidadesMedida
        {
            Kg,
            G,
            Lt,
            Ml,
            U
        }

        public List<BEProveedorInsumo> ListaProveedores { get; set; } = new List<BEProveedorInsumo>();
    }
}
