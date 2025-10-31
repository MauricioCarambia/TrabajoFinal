using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEReserva
    {
        public int Id { get; set; }
        public string NumeroReserva { get; set; }
        public DateTime FechaReserva { get; set; } 
        public BECliente Cliente { get; set; }      
        public int CantidadPersonas { get; set; }   
        public BEMesa Mesa { get; set; }
        public EstadoReserva Estado { get; set; } = EstadoReserva.Abierta;

        public enum EstadoReserva
        {
            Abierta,
            Cerrada,
            Finalizada
        }

    }
}
