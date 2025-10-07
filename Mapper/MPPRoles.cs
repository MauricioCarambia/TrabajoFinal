using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entity;

namespace Mapper
{
    public class MPPRoles
    {
        private readonly string archivoXml = "roles.xml";

        private void GuardarXml(List<BERoles> lista)
        {
            XElement root = new XElement("Roles",
                lista.Select(r => new XElement("Rol",
                    new XElement("Id", r.Id),
                    new XElement("Nombre", r.NombreRol)
                ))
            );
            root.Save(archivoXml);
        }

        public List<BERoles> ObtenerTodos()
        {
            if (!File.Exists(archivoXml))
                return new List<BERoles>();

            XElement root = XElement.Load(archivoXml);
            return root.Elements("Rol")
                .Select(x => new BERoles
                {
                    Id = int.TryParse(x.Element("Id")?.Value, out int id) ? id : 0,
                    NombreRol = x.Element("Nombre")?.Value ?? string.Empty
                })
                .ToList();
        }

        public void Agregar(BERoles rol)
        {
            var lista = ObtenerTodos();
            rol.Id = lista.Count == 0 ? 1 : lista.Max(x => x.Id) + 1;
            lista.Add(rol);
            GuardarXml(lista);
        }

        public void Modificar(BERoles rol)
        {
            var lista = ObtenerTodos();
            var existente = lista.FirstOrDefault(x => x.Id == rol.Id);
            if (existente != null)
            {
                existente.NombreRol = rol.NombreRol;
                GuardarXml(lista);
            }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            lista.RemoveAll(x => x.Id == id);
            GuardarXml(lista);
        }
    }
}
