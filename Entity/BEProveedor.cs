using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEProveedor
    {
      

        public int Id { get; set; }
        public long CUIL { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<BEProveedorInsumo> ListaInsumos { get; set; } = new List<BEProveedorInsumo>();
    }
}
