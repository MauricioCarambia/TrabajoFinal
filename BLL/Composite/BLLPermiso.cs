using Entity.Composite;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Composite;

namespace BLL.Composite
{
    public class BLLPermiso 
    {

        MPPPermiso oMPPPermiso;

        #region "Constructor"

        public BLLPermiso() { oMPPPermiso = new MPPPermiso(); }

        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEPermiso oBEPermiso) { return oMPPPermiso.Eliminar(oBEPermiso); }

        public bool Guardar(BEPermiso oBEPermiso) { return oMPPPermiso.Guardar(oBEPermiso); }

        public BEPermiso ListarObjeto(BEPermiso oBEPermiso) { return oMPPPermiso.ListarObjeto(oBEPermiso); }

        public List<BEPermiso> ListarTodo() { return oMPPPermiso.ListarTodo(); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEPermiso oBEPermiso) { return oMPPPermiso.VerificarExistenciaObjeto(oBEPermiso); }

        #endregion
    }
}
