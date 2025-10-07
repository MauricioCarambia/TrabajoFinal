using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;
using Entity;

namespace BLL
{
    public class BLLRoles
    {
        private readonly MPPRoles mapper = new MPPRoles();

        public List<BERoles> ListarTodo()
        {
            return mapper.ObtenerTodos();
        }

        public void GuardarRol(BERoles r)
        {
            if (r.Id == 0)
                mapper.Agregar(r);
            else
                mapper.Modificar(r);
        }

        public void EliminarRol(int id)
        {
            mapper.Eliminar(id);
        }
    }
}
