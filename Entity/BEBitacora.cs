using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEBitacora
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string Detalle { get; set; }

        public BEUsuario oBEUsuario { get; set; }



        public BEBitacora() { }

        public BEBitacora(int pId, DateTime pFechaRegistro, string pDetalle)
        {
            Id = pId;
            FechaRegistro = pFechaRegistro;
            Detalle = pDetalle;
        }
    }
}
