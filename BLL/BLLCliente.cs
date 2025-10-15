using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        private MPPCliente oMPPCliente = new MPPCliente();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BECliente oBECliente) { oMPPCliente.Eliminar(oBECliente); }

        public void Guardar(BECliente oBECliente) { oMPPCliente.Guardar(oBECliente); }

        public BECliente ListarObjeto(BECliente BECliente) { return oMPPCliente.ListarObjeto(BECliente); }

        public BECliente ListarObjetoPorId(BECliente BECliente) { return oMPPCliente.ListarObjetoPorIdCliente(BECliente); }

        public List<BECliente> ListarTodo() { return oMPPCliente.ListarTodo(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BECliente objeto) { throw new NotImplementedException(); }
    }
}
