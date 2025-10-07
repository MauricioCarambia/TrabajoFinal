using Entity;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPUsuarios
    {
        private readonly string archivoXml = "usuarios.xml";

        // Guarda la lista completa de usuarios en el XML
        private void GuardarXml(List<Usuarios> lista)
        {
            XElement root = new XElement("Usuarios",
                lista.Select(u => new XElement("Usuario",
                    new XElement("Id", u.Id),
                    new XElement("Nombre", u.NombreUsuario),
                    new XElement("Contraseña", u.Contrasenia)
                ))
            );
            root.Save(archivoXml);
        }

        // Devuelve todos los usuarios almacenados
        public List<Usuarios> ObtenerTodos()
        {
            if (!File.Exists(archivoXml))
                return new List<Usuarios>();

            XElement root = XElement.Load(archivoXml);
            return root.Elements("Usuario")
                .Select(x => new Usuarios
                {
                    Id = int.Parse(x.Element("Id").Value),
                    NombreUsuario = x.Element("Nombre").Value,
                    Contrasenia = x.Element("Contraseña").Value
                })
                .ToList();
        }

        // Guarda un nuevo usuario
        public void Agregar(Usuarios usuario)
        {
            var lista = ObtenerTodos();
            usuario.Id = lista.Count == 0 ? 1 : lista.Max(x => x.Id) + 1;
            lista.Add(usuario);
            GuardarXml(lista);
        }

        // Modifica un usuario existente
        public void Modificar(Usuarios usuario)
        {
            var lista = ObtenerTodos();
            var existente = lista.FirstOrDefault(x => x.Id == usuario.Id);
            if (existente != null)
            {
                existente.NombreUsuario = usuario.NombreUsuario;
                existente.Contrasenia = usuario.Contrasenia;
                GuardarXml(lista);
            }
        }

        // Elimina un usuario
        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            lista.RemoveAll(x => x.Id == id);
            GuardarXml(lista);
        }
    }
}
