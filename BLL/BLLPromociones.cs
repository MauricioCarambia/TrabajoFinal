using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPromociones
    {
        private MPPPromociones oMPPPromociones = new MPPPromociones();

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEPromociones oBEPromociones) { return oMPPPromociones.Eliminar(oBEPromociones); }

        public void Guardar(BEPromociones oBEPromociones) { oMPPPromociones.Guardar(oBEPromociones); }

        public BEPromociones ListarObjeto(BEPromociones oBEPromociones) { return oMPPPromociones.ListarObjeto(oBEPromociones); }

        public List<BEPromociones> ListarTodo() { return oMPPPromociones.ListarTodo(); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPromociones oBEPromociones) { throw new NotImplementedException(); }
    }
}
