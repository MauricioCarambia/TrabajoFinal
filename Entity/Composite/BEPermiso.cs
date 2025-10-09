using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Composite
{
    public class BEPermiso : BEComposite
    {

      

        public BEPermiso(int pId, string pNombre) : base(pId, pNombre) { }

    

        public override void Agregar(BEComposite oBEComponente)
        {
            try { throw new Exception("Error: No se puede agregar un permiso o rol a un permiso!"); }
            catch (Exception ex) { throw ex; }
        }

        public override IList<BEComposite> ObtenerHijos()
        {
            try { throw new Exception("Error: No se puede listar los permisos de un permiso en si!"); }
            catch (Exception ex) { throw ex; }
        }

     
    }
}
