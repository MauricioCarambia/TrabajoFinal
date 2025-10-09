using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;

namespace BLL
{
    public class BLLBitacora
    {
        MPPBitacora oMMPBitacora;

        

        public BLLBitacora() { oMMPBitacora = new MPPBitacora(); }

     

        public bool CrearXML() { throw new NotImplementedException(); }

        public bool Eliminar(BEBitacora objeto) { throw new NotImplementedException(); }

        public bool Guardar(BEBitacora oBEBitacora) { return oMMPBitacora.Guardar(oBEBitacora); }

        public BEBitacora ListarObjeto(BEBitacora objeto) { throw new NotImplementedException(); }

        public List<BEBitacora> ListarTodo() { return oMMPBitacora.ListarTodo(); }

        public List<BEBitacora> ListarTodoPorTipo(bool pTipo) { return oMMPBitacora.ListarTodoPorTipo(pTipo); }

        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool VerificarExistenciaObjeto(BEBitacora objeto) { throw new NotImplementedException(); }
    }
}
