
using Entity;
using Mapper;

namespace BLL
{
    public class BLLUsuarios
    {
        private readonly MPPUsuarios usuarioDAL = new MPPUsuarios();

        public List<Usuarios> ObtenerUsuarios()
        {
            return usuarioDAL.ObtenerTodos();
        }

        public void AgregarUsuario(Usuarios usuario)
        {
            // Acá podrías agregar validaciones (por ejemplo, que no haya duplicados)
            usuarioDAL.Agregar(usuario);
        }

        public void ModificarUsuario(Usuarios usuario)
        {
            usuarioDAL.Modificar(usuario);
        }

        public void EliminarUsuario(int id)
        {
            usuarioDAL.Eliminar(id);
        }
    }
}
