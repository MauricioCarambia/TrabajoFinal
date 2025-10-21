using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedorInsumo
    {


        public MPPProveedorInsumo oMPPProveedorInsumo = new MPPProveedorInsumo(); 

       

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEProveedorInsumo oBEProveedorInsumo) { return oMPPProveedorInsumo.Eliminar(oBEProveedorInsumo); }

        public bool Guardar(BEProveedorInsumo oBEProveedorInsumo) { return oMPPProveedorInsumo.Guardar(oBEProveedorInsumo); }

        public BEProveedorInsumo ListarObjeto(BEProveedorInsumo oBEProveedorInsumo) { return oMPPProveedorInsumo.ListarObjeto(oBEProveedorInsumo); }

        public List<BEProveedorInsumo> ListarTodo() { return oMPPProveedorInsumo.ListarTodo(); }
        public List<BEProveedorInsumo> ListarPorProveedor(int proveedorId) { return oMPPProveedorInsumo.ListarPorProveedor(proveedorId); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEProveedorInsumo objeto) { throw new NotImplementedException(); }
    }
}
