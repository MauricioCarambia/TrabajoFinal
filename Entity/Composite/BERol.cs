using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Composite
{
    public class BERol : BEComposite
    {

        public List<BEComposite> listaPermisos;


        public BERol(int pId, string pNombre) : base(pId, pNombre)
        {
            listaPermisos = new List<BEComposite>();
        }

        public override void Agregar(BEComposite oBEComponente)
        {
            try
            {
                if (oBEComponente != null)
                {
                    listaPermisos.Add(oBEComponente);
                }
                else { }
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public override IList<BEComposite> ObtenerHijos()
        {
            try
            {
                return listaPermisos;
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }
    }
}
