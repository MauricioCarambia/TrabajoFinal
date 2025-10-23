using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPPlato
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    // Crear XML desde cero con ambos nodos
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Platos"),
                            new XElement("PlatoInsumos")
                        )
                    );
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    // Cargar XML existente
                    BDXML = XDocument.Load(ruta);

                    // Verificar nodo <Platos>
                    XElement platos = BDXML.Root.Element("Platos");
                    if (platos == null)
                    {
                        platos = new XElement("Platos");
                        BDXML.Root.Add(platos);
                    }

                    // Verificar nodo <PlatoInsumos>
                    XElement insumos = BDXML.Root.Element("PlatoInsumos");
                    if (insumos == null)
                    {
                        insumos = new XElement("PlatoInsumos");
                        BDXML.Root.Add(insumos);
                    }

                    BDXML.Save(ruta);
                    return true;
                }
            }
            catch (XmlException ex)
            {
                throw new XmlException("Error al procesar el XML de Platos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear/verificar el XML de Platos: " + ex.Message, ex);
            }
        }

        public void Eliminar(BEPlato oBEPlato)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                // Cargo el XML de Platos
                BDXML = XDocument.Load(ruta);

                if (BDXML == null)
                    throw new XmlException("No se pudo recuperar la información del XML de Platos.");

                // Busco el Plato por su Id
                XElement platoEliminar = BDXML.Root.Element("Platos")
                                                .Elements("Plato")
                                                .FirstOrDefault(p => (int)p.Attribute("Id") == oBEPlato.Id);

                if (platoEliminar != null)
                {
                    // Lo elimino del XML
                    platoEliminar.Remove();

                    // Guardar cambios
                    BDXML.Save(ruta);

                    // Opcional: eliminar los registros relacionados en PlatoInsumos.xml
                    string rutaInsumos = "PlatoInsumos.xml";
                    if (File.Exists(rutaInsumos))
                    {
                        XDocument xmlInsumos = XDocument.Load(rutaInsumos);
                        var insumosEliminar = xmlInsumos.Root.Elements("PlatoInsumo")
                                                            .Where(x => (int)x.Attribute("IdPlato") == oBEPlato.Id)
                                                            .ToList();
                        foreach (var item in insumosEliminar)
                            item.Remove();

                        xmlInsumos.Save(rutaInsumos);
                    }
                }
                else
                {
                    throw new Exception($"No se encontró el plato con Id = {oBEPlato.Id}");
                }
            }
            catch (XmlException ex)
            {
                throw new XmlException("Error al procesar el XML: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el plato: " + ex.Message, ex);
            }
        }

        public void Guardar(BEPlato oBEPlato)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("No se pudo cargar el archivo XML.");

                // Verificar o crear nodo <Platos>
                XElement platosElement = BDXML.Root.Element("Platos");
                if (platosElement == null)
                {
                    platosElement = new XElement("Platos");
                    BDXML.Root.Add(platosElement);
                }

                // Verificar o crear nodo <PlatoInsumos>
                XElement insumosElement = BDXML.Root.Element("PlatoInsumos");
                if (insumosElement == null)
                {
                    insumosElement = new XElement("PlatoInsumos");
                    BDXML.Root.Add(insumosElement);
                }

                // Generar Id si es nuevo
                if (oBEPlato.Id == 0)
                {
                    int nuevoId = platosElement.Elements("Plato").Any()
                        ? platosElement.Elements("Plato").Max(x => (int)x.Attribute("Id")) + 1
                        : 1;
                    oBEPlato.Id = nuevoId;
                }
                else
                {
                    // Eliminar plato existente
                    var platoExistente = platosElement.Elements("Plato")
                        .FirstOrDefault(x => (int)x.Attribute("Id") == oBEPlato.Id);
                    platoExistente?.Remove();

                    // Eliminar insumos existentes
                    var insumosExistentes = insumosElement.Elements("PlatoInsumo")
                        .Where(x => (int)x.Element("IdPlato") == oBEPlato.Id)
                        .ToList();
                    foreach (var i in insumosExistentes)
                        i.Remove();
                }

                // Guardar Plato
                XElement nuevoPlato = new XElement("Plato",
                    new XAttribute("Id", oBEPlato.Id),
                    new XElement("Nombre", oBEPlato.Nombre),
                    new XElement("Categoria", oBEPlato.Categoria.ToString()),
                     new XElement("PorcentajeGanancia", oBEPlato.PorcentajeGanancia.ToString()),
                    new XElement("PrecioCosto", oBEPlato.PrecioCosto.ToString("F2")),
                    new XElement("PrecioVenta", oBEPlato.PrecioVenta.ToString("F2")),
                    new XElement("Activo", oBEPlato.Activo)
                );
                platosElement.Add(nuevoPlato);

                // Guardar insumos del plato (con <IdPlato> y <IdInsumo> como elementos)
                foreach (var insumo in oBEPlato.ListaInsumos)
                {
                    XElement nuevoInsumo = new XElement("PlatoInsumo",
                        new XElement("IdPlato", oBEPlato.Id),
                        new XElement("IdInsumo", insumo.Insumo.Id),
                        new XElement("Cantidad", insumo.Cantidad.ToString("F2")),
                        new XElement("CostoUnitario", insumo.CostoUnitario.ToString("F2"))
                    );
                    insumosElement.Add(nuevoInsumo);
                }

                // Guardar XML
                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el plato: " + ex.Message);
            }
        }

        public BEPlato ListarObjeto(BEPlato oBEPlato)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("No se pudo cargar el archivo XML.");

                if (oBEPlato == null)
                    throw new Exception("Error: No se pudo obtener la información del plato!");

                // 1️⃣ Buscar el plato en <Platos>
                var platoXml = BDXML.Root.Element("Platos")?
                    .Elements("Plato")
                    .FirstOrDefault(p => (int)p.Attribute("Id") == oBEPlato.Id);

                if (platoXml == null)
                    throw new Exception("Error: No se encontró ningún plato con los datos brindados!");

                // 2️⃣ Crear el objeto BEPlato
                BEPlato plato = new BEPlato
                {
                    Id = int.Parse(platoXml.Attribute("Id").Value),
                    Nombre = platoXml.Element("Nombre").Value.Trim(),
                    Categoria = (BEPlato.CategoriasPlato)Enum.Parse(typeof(BEPlato.CategoriasPlato), platoXml.Element("Categoria").Value.Trim()),
                    PorcentajeGanancia = decimal.Parse(platoXml.Element("PorcentajeGanancia").Value.Trim()),
                    PrecioCosto = decimal.Parse(platoXml.Element("PrecioCosto").Value.Trim()),
                    PrecioVenta = decimal.Parse(platoXml.Element("PrecioVenta").Value.Trim()),
                    Activo = bool.Parse(platoXml.Element("Activo").Value.Trim()),
                    ListaInsumos = new List<BEPlatoInsumo>()
                };

                // 3️⃣ Buscar insumos en <PlatoInsumos> filtrando por IdPlato
                var insumosXml = BDXML.Root.Element("PlatoInsumos")?
                    .Elements("PlatoInsumo")
                    .Where(pi => (int)pi.Element("IdPlato") == plato.Id);

                if (insumosXml != null)
                {
                    foreach (var i in insumosXml)
                    {
                        BEPlatoInsumo platoInsumo = new BEPlatoInsumo
                        {
                            Insumo = new BEInsumo
                            {
                                Id = int.Parse(i.Element("IdInsumo").Value.Trim())
                                // Opcional: si querés nombre y unidad, necesitarías cargarlo de los insumos disponibles
                            },
                            Cantidad = decimal.Parse(i.Element("Cantidad").Value.Trim()),
                            CostoUnitario = decimal.Parse(i.Element("CostoUnitario").Value.Trim())
                        };

                        plato.ListaInsumos.Add(platoInsumo);
                    }
                }

                return plato;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw new Exception("Error al listar el plato: " + ex.Message); }
        }

        public BEPlato ListarObjetoPorId(BEPlato oBEPlato)
        {
            if (!CrearXML()) return null;

            BDXML = XDocument.Load(ruta);
            var platoXml = BDXML.Root.Element("Platos")?
                .Elements("Plato")
                .FirstOrDefault(p => (int)p.Attribute("Id") == oBEPlato.Id);

            if (platoXml == null) return null;

            BEPlato plato = new BEPlato
            {
                Id = (int)platoXml.Attribute("Id"),
                Nombre = platoXml.Element("Nombre").Value.Trim(),
                Categoria = (BEPlato.CategoriasPlato)Enum.Parse(typeof(BEPlato.CategoriasPlato), platoXml.Element("Categoria").Value.Trim()),
                PorcentajeGanancia = decimal.Parse(platoXml.Element("PorcentajeGanancia").Value.Trim()),
                PrecioCosto = decimal.Parse(platoXml.Element("PrecioCosto").Value.Trim()),
                PrecioVenta = decimal.Parse(platoXml.Element("PrecioVenta").Value.Trim()),
                Activo = bool.Parse(platoXml.Element("Activo").Value.Trim()),
                ListaInsumos = new List<BEPlatoInsumo>()
            };

            var insumosXml = BDXML.Root.Element("PlatoInsumos")?
                .Elements("PlatoInsumo")
                .Where(pi => (int)pi.Element("IdPlato") == plato.Id);

            if (insumosXml != null)
            {
                foreach (var i in insumosXml)
                {
                    plato.ListaInsumos.Add(new BEPlatoInsumo
                    {
                        Insumo = new BEInsumo
                        {
                            Id = int.Parse(i.Element("IdInsumo").Value.Trim())
                            // No completamos nombre ni unidad aquí
                        },
                        Cantidad = decimal.Parse(i.Element("Cantidad").Value.Trim()),
                        CostoUnitario = decimal.Parse(i.Element("CostoUnitario").Value.Trim())
                    });
                }
            }

            return plato;
        }


        public List<BEPlato> ListarTodo()
        {
            if (!CrearXML()) return new List<BEPlato>();

            BDXML = XDocument.Load(ruta);
            var platosXml = BDXML.Root.Element("Platos")?.Elements("Plato");
            var insumosXml = BDXML.Root.Element("PlatoInsumos")?.Elements("PlatoInsumo");

            if (platosXml == null) return new List<BEPlato>();

            List<BEPlato> listaPlatos = new List<BEPlato>();

            foreach (var platoXml in platosXml)
            {
                int idPlato = (int)platoXml.Attribute("Id");
                BEPlato plato = new BEPlato
                {
                    Id = idPlato,
                    Nombre = platoXml.Element("Nombre").Value.Trim(),
                    Categoria = (BEPlato.CategoriasPlato)Enum.Parse(typeof(BEPlato.CategoriasPlato), platoXml.Element("Categoria").Value.Trim()),
                    PorcentajeGanancia = decimal.Parse(platoXml.Element("PorcentajeGanancia").Value.Trim()),
                    PrecioCosto = decimal.Parse(platoXml.Element("PrecioCosto").Value.Trim()),
                    PrecioVenta = decimal.Parse(platoXml.Element("PrecioVenta").Value.Trim()),
                    Activo = bool.Parse(platoXml.Element("Activo").Value.Trim()),
                    ListaInsumos = new List<BEPlatoInsumo>()
                };

                // Agregamos solo Id y Cantidad del insumo
                var insumosDelPlato = insumosXml?.Where(pi => (int)pi.Element("IdPlato") == idPlato);
                if (insumosDelPlato != null)
                {
                    foreach (var i in insumosDelPlato)
                    {
                        plato.ListaInsumos.Add(new BEPlatoInsumo
                        {
                            Insumo = new BEInsumo
                            {
                                Id = int.Parse(i.Element("IdInsumo").Value.Trim())
                            },
                            Cantidad = decimal.Parse(i.Element("Cantidad").Value.Trim()),
                            CostoUnitario = decimal.Parse(i.Element("CostoUnitario").Value.Trim())
                        });
                    }
                }

                listaPlatos.Add(plato);
            }

            return listaPlatos;
        }

        public int ObtenerUltimoId()
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("No se pudo cargar el archivo XML.");

                // Tomamos todos los Id de <Plato> dentro de <Platos>
                var ids = BDXML.Root.Element("Platos")?.Elements("Plato")
                            .Select(p => (int)p.Attribute("Id")) ?? Enumerable.Empty<int>();

                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de plato: " + ex.Message);
            }
        }

        public bool VerificarExistenciaObjeto(BEPlato oBEPlato)
        {
            try
            {
                if (oBEPlato == null)
                    throw new ArgumentNullException(nameof(oBEPlato), "El objeto BEPlato no puede ser nulo.");

                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("No se pudo cargar el archivo XML.");

                // Buscamos por nombre dentro de <Platos>
                var existe = BDXML.Root.Element("Platos")?
                                .Elements("Plato")
                                .Any(p => string.Equals(p.Element("Nombre")?.Value.Trim(),
                                                        oBEPlato.Nombre.Trim(),
                                                        StringComparison.OrdinalIgnoreCase))
                             ?? false;

                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del plato: " + ex.Message);
            }
        }


    }
}
