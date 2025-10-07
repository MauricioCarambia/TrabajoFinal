
using Entity;
using DAL;

namespace BLL
{
    public class BLLUsuarios
    {
        private readonly DALUsuarios usuarioDAL = new DALUsuarios();

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
