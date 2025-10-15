using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEMesa
    {
        

        public int IdMesa { get; set; }
        public int Capacidad { get; set; }
        public int NumeroMesa { get; set; }
        public EstadoMesa Estado { get; set; }
        public enum EstadoMesa
        {
            Libre,
            Ocupada,
            Reservada
        }
        
    }
}
