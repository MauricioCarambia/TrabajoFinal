using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPedido
    {
        private MPPPedido oMPPPedido = new MPPPedido();

        public bool CrearXML() { throw new NotImplementedException(); }

        //public void Eliminar(BEPedido oBEPedido) { oMPPPedido.Eliminar(oBEPedido); }

        public void Guardar(BEPedido oBEPedido) { oMPPPedido.Guardar(oBEPedido); }

        //public BEPedido ListarObjeto(BEPedido oBEPedido) { return oMPPPedido.ListarObjeto(oBEPedido); }

        public BEPedido ListarObjetoPorId(BEPedido oBEPedido) { return oMPPPedido.ListarObjetoPorId(oBEPedido); }
        public void DescontarStockInsumos(BEPedido oBEPedido)
        {
            oMPPPedido.DescontarStockInsumos(oBEPedido);
        }

        public BEPedido ListarTodo(int reservaId)
        {
            return oMPPPedido.ListarPorReserva(reservaId);
        }
        //public List<BEPedido> ListarTodo() { return oMPPPedido.ListarPorReserva(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPedido objeto) { throw new NotImplementedException(); }
    }
}
