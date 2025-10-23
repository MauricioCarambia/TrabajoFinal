using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPPlatoInsumo
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        #region Crear XML
        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(
                        new XElement("Datos",
                            new XElement("Platos"),
                            new XElement("PlatoInsumos")
                        )
                    );
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML.Root.Element("PlatoInsumos") == null)
                    {
                        BDXML.Root.Add(new XElement("PlatoInsumos"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear/verificar el XML: " + ex.Message);
            }
        }
        #endregion

        #region Guardar
        public void Guardar(BEPlatoInsumo oBEPlatoInsumo)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);

                // Asegurarse que exista <PlatoInsumos>
                XElement platoInsumosElement = BDXML.Root.Element("PlatoInsumos");
                if (platoInsumosElement == null)
                {
                    platoInsumosElement = new XElement("PlatoInsumos");
                    BDXML.Root.Add(platoInsumosElement);
                }

                // Eliminar si ya existe un insumo para ese plato + insumo (para actualizar)
                var existente = platoInsumosElement.Elements("PlatoInsumo")
                    .FirstOrDefault(x =>
                        (int)x.Element("IdPlato") == oBEPlatoInsumo.Plato.Id &&
                        (int)x.Element("IdInsumo") == oBEPlatoInsumo.Insumo.Id);

                existente?.Remove();
                decimal costoProporcional = oBEPlatoInsumo.Insumo.Precio * oBEPlatoInsumo.Cantidad;
                // Crear nuevo nodo PlatoInsumo
                XElement nuevo = new XElement("PlatoInsumo",
                    new XElement("IdPlato", oBEPlatoInsumo.Plato.Id),
                    new XElement("IdInsumo", oBEPlatoInsumo.Insumo.Id),
                    new XElement("Cantidad", oBEPlatoInsumo.Cantidad.ToString("F2")),
                    new XElement("CostoUnitario", oBEPlatoInsumo.CostoUnitario.ToString("F2"))
                );

                platoInsumosElement.Add(nuevo);
                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar PlatoInsumo: " + ex.Message);
            }
        }

        #endregion

        #region Eliminar
        public void Eliminar(int idPlato)
        {
            try
            {
                if (!CrearXML()) throw new Exception("No se pudo acceder al XML.");
                BDXML = XDocument.Load(ruta);

                XElement platoInsumosElement = BDXML.Root.Element("PlatoInsumos");

                // Buscar todos los nodos de ese plato
                var elementos = platoInsumosElement.Elements("PlatoInsumo")
                    .Where(x => (int)x.Element("IdPlato") == idPlato)
                    .ToList();

                foreach (var elem in elementos)
                {
                    elem.Remove();
                }

                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar insumos del plato: " + ex.Message);
            }
        }

        #endregion

        #region ListarTodo
        public List<BEPlatoInsumo> ListarTodo()
        {
            try
            {
                if (!CrearXML()) throw new Exception("No se pudo acceder al XML.");
                BDXML = XDocument.Load(ruta);

                XElement platoInsumosElement = BDXML.Root.Element("PlatoInsumos");
                List<BEPlatoInsumo> lista = platoInsumosElement.Elements("PlatoInsumo").Select(x => new BEPlatoInsumo
                {
                    Id = (int)x.Attribute("Id"),
                    Plato = new BEPlato { Id = int.Parse(x.Element("IdPlato").Value) },
                    Insumo = new BEInsumo { Id = int.Parse(x.Element("IdInsumo").Value) },
                    Cantidad = decimal.Parse(x.Element("Cantidad").Value),
                    CostoUnitario = decimal.Parse(x.Element("CostoUnitario").Value)
                }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar PlatoInsumos: " + ex.Message);
            }
        }
        #endregion

        #region ListarPorPlato
        public List<BEPlatoInsumo> ListarPorPlato(int idPlato)
        {
            return ListarTodo().Where(pi => pi.Plato.Id == idPlato).ToList();
        }
        #endregion

        #region VerificarExistencia
        public bool VerificarExistencia(BEPlatoInsumo oBEPlatoInsumo)
        {
            return ListarTodo().Any(pi =>
                pi.Plato.Id == oBEPlatoInsumo.Plato.Id &&
                pi.Insumo.Id == oBEPlatoInsumo.Insumo.Id
            );
        }
        #endregion

        #region ObtenerUltimoId
        public int ObtenerUltimoId()
        {
            var lista = ListarTodo();
            return lista.Any() ? lista.Max(x => x.Id) : 0;
        }
        #endregion
    }
}
