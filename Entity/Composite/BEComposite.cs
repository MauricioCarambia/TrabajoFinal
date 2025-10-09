using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Composite
{
    public abstract class BEComposite 
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        

     

        public BEComposite(int pId, string pNombre)
        {
            Id = pId;
            Nombre = pNombre;
        }

      

        public abstract void Agregar(BEComposite oBEComposite);

        public abstract IList<BEComposite> ObtenerHijos();

   

    }
}
