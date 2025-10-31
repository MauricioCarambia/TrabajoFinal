using Entity;
using Entity.Composite;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLMesa
    {
        private MPPMesa oMPPMesa = new MPPMesa();

        public bool CrearXML() { throw new NotImplementedException(); }

        public void Eliminar(BEMesa oBEMesa) {  oMPPMesa.Eliminar(oBEMesa); }

        public void Guardar(BEMesa oBEMesa) { oMPPMesa.Guardar(oBEMesa); }

        public BEMesa ListarObjeto(BEMesa BEMesa) { return oMPPMesa.ListarObjeto(BEMesa); }

        public BEMesa ListarObjetoPorId(BEMesa BEMesa) { return oMPPMesa.ListarObjetoPorIdMesa(BEMesa); }
        public BEMesa ListarObjetoPorNumeroMesa(BEMesa BEMesa) { return oMPPMesa.ListarObjetoPorNumeroMesa(BEMesa); }

        public List<BEMesa> ListarTodo() { return oMPPMesa.ListarTodo(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEMesa objeto) { throw new NotImplementedException(); }

        public void ActualizarEstadoMesa(int idMesa, BEMesa.EstadoMesa nuevoEstado)
        {
            if (idMesa <= 0)
                throw new ArgumentException("El Id de mesa no es válido.");

            oMPPMesa.ActualizarEstadoMesa(idMesa, nuevoEstado);
        }



    }
}
