
using Entity;
using Entity.Composite;
using Mapper;

namespace BLL
{
    public class BLLUsuario
    {
        private MPPUsuarios oMPPUsuario = new MPPUsuarios();

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEUsuario oBEUsuario) { return oMPPUsuario.Eliminar(oBEUsuario); }

        public bool Guardar(BEUsuario oBEUsuario) { return oMPPUsuario.Guardar(oBEUsuario); }

        public BEUsuario ListarObjeto(BEUsuario oBEUsuario) { return oMPPUsuario.ListarObjeto(oBEUsuario); }

        public BEUsuario ListarObjetoPorId(BEUsuario oBEUsuario) { return oMPPUsuario.ListarObjetoPorId(oBEUsuario); }

        public List<BEUsuario> ListarTodo() { return oMPPUsuario.ListarTodo(); }

        public int ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEUsuario objeto) { throw new NotImplementedException(); }

       

        public string EncriptarPassword(string pPassword) { return oMPPUsuario.EncriptarPassword(pPassword); }

        public string DesencriptarPassword(string pPassword) { return oMPPUsuario.DesencriptarPassword(pPassword); }

       


        public bool AsociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol) { return oMPPUsuario.AsociarUsuarioARol(oBEUsuario, oBERol); }

        public bool DesasociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol) { return oMPPUsuario.DesasociarUsuarioARol(oBEUsuario, oBERol); }

        public bool AsociarPermisoAUsuario(BEUsuario oBEUsario, BEPermiso oBEPermiso) { return oMPPUsuario.AsociarPermisoAUsuario(oBEUsario, oBEPermiso); }

        public bool DesasociarPermisoAUsuario(BEUsuario oBEUsario, BEPermiso oBEPermiso) { return oMPPUsuario.DesasociarPermisoAUsuario(oBEUsario, oBEPermiso); }

        public bool Login(BEUsuario oBEUsuario) { return oMPPUsuario.Login(oBEUsuario); }

        public bool AsociarUsuarioARolJerarquico(BEUsuario oBEUsuario, BERol oBERol, BERol oBERolPadre = null) { return oMPPUsuario.AsociarUsuarioARolJerarquico(oBEUsuario, oBERol, oBERolPadre); }

        public BEUsuario ListarObjetoJerarquico(BEUsuario oBEUsuario) { return oMPPUsuario.ListarObjetoJerarquico(oBEUsuario); }

        public bool DesasoriarUnRolDentroOtroRol(BEUsuario oBEUsuario, BERol oBERol, BERol oBERolPadre) { return oMPPUsuario.DesasoriarUnRolDentroOtroRol(oBEUsuario, oBERol, oBERolPadre); }

        public List<BEPermiso> ListarTodosLosPermisosDelUsuario(BEUsuario oBEusuario) { return oMPPUsuario.ListarTodosLosPermisosDelUsuario(oBEusuario); }

    }
}
