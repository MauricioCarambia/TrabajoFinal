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
    public class BLLRol 
    {

        MPPRol oMPPRol;

        #region "Constructor"

        public BLLRol() { oMPPRol = new MPPRol(); }

        #endregion

        #region "Métodos"

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BERol oBERol) { return oMPPRol.Eliminar(oBERol); }

        public bool Guardar(BERol oBERol) { return oMPPRol.Guardar(oBERol); }

        public BERol ListarObjeto(BERol oBERol) { return oMPPRol.ListarObjeto(oBERol); }

        public List<BERol> ListarTodo() { return oMPPRol.ListarTodo(); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BERol oBERol) { return oMPPRol.VerificarExistenciaObjeto(oBERol); }

        public bool AsociarRolaPermiso(BERol oBERol, BEPermiso oBEPermiso) { return oMPPRol.AsociarRolaPermiso(oBERol, oBEPermiso); }

        public bool DesasociarRolaPermiso(BERol oBERol, BEPermiso oBEPermiso) { return oMPPRol.DesasociarRolaPermiso(oBERol, oBEPermiso); }

        //public List<BEPermiso> ListarPermisosDeRolPorUsuario(BEUsuario oBEUsuario) { return oMPPRol.ListarPermisosDeRolPorUsuario(oBEUsuario); }

        //public bool AsociarRolaRol(BERol oBERol, BERol oBERol2) { return oMPPRol.AsociarRolaRol(oBERol, oBERol2); }

        //public bool VerificarExistenciaRolEnRol(BERol oBERol, BERol oBERol2) { return oMPPRol.VerificarExistenciaRolEnRol(oBERol, oBERol2); }

        #endregion

    }
}
