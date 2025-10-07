using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entity;

namespace Mapper
{
    public class MPPPermisos
    {
        private readonly string archivoXml = "permisos.xml";

        // Guardar lista completa en XML
        private void GuardarXml(List<BEPermisos> lista)
        {
            XElement root = new XElement("Permisos",
                lista.Select(p => new XElement("Permiso",
                    new XElement("Id", p.Id),
                    new XElement("Nombre", p.Nombre),
                    new XElement("Menu", p.Menu),
                    new XElement("Item", p.Item)
                ))
            );
            root.Save(archivoXml);
        }

        // Obtener todos los permisos
        public List<BEPermisos> ObtenerTodos()
        {
            if (!File.Exists(archivoXml))
                return new List<BEPermisos>();

            XElement root = XElement.Load(archivoXml);
            return root.Elements("Permiso")
                       .Select(x => new BEPermisos
                       {
                           Id = int.Parse(x.Element("Id")?.Value ?? "0"),
                           Nombre = x.Element("Nombre")?.Value ?? "",
                           Menu = x.Element("Menu")?.Value ?? "",
                           Item = x.Element("Item")?.Value ?? ""
                       }).ToList();
        }

        // Agregar un permiso nuevo
        public void Agregar(BEPermisos permiso)
        {
            var lista = ObtenerTodos();
            permiso.Id = lista.Count == 0 ? 1 : lista.Max(p => p.Id) + 1;
            lista.Add(permiso);
            GuardarXml(lista);
        }

        // Modificar un permiso existente
        public void Modificar(BEPermisos permiso)
        {
            var lista = ObtenerTodos();
            var existente = lista.FirstOrDefault(p => p.Id == permiso.Id);
            if (existente != null)
            {
                existente.Nombre = permiso.Nombre;
                existente.Menu = permiso.Menu;
                existente.Item = permiso.Item;
                GuardarXml(lista);
            }
        }

        // Eliminar un permiso por ID
        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            lista.RemoveAll(p => p.Id == id);
            GuardarXml(lista);
        }
    }
}
