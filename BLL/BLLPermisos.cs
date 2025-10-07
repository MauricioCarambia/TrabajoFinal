using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;
using Entity;

namespace BLL
{
    public class BLLPermisos
    {
        private MPPPermisos mapper = new MPPPermisos();

        public List<BEPermisos> ListarTodo() => mapper.ObtenerTodos();

        public void GuardarPermiso(BEPermisos permiso)
        {
            if (permiso.Id == 0)
                mapper.Agregar(permiso);
            else
                mapper.Modificar(permiso);
        }

        public void EliminarPermiso(int id) => mapper.Eliminar(id);
    }
}
