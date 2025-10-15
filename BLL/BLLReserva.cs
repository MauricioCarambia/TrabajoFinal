using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Mapper;

namespace BLL
{
    public class BLLReserva
    {
        private MPPReserva oMPPReserva = new MPPReserva();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BEReserva oBEReserva) { oMPPReserva.Eliminar(oBEReserva); }

        public void Guardar(BEReserva oBEReserva) { oMPPReserva.Guardar(oBEReserva); }

        public BEReserva ListarObjeto(BEReserva BEReserva) { return oMPPReserva.ListarObjeto(BEReserva); }

        public BEReserva ListarObjetoPorId(BEReserva BEReserva) { return oMPPReserva.ListarObjetoPorIdReserva(BEReserva); }

        public List<BEReserva> ListarTodo() { return oMPPReserva.ListarTodo(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEReserva objeto) { throw new NotImplementedException(); }
    }
}
