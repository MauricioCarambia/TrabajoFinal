using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Entity.BEInsumo;

namespace Mapper
{
    public class MPPProveedorInsumo
    {
        readonly string ruta = "BD.xml";
        XDocument BDXML;

        public bool CrearXML()
        {
            try
            {
                // Si no existe el archivo XML, lo creo con las secciones necesarias
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Proveedores"),
                            new XElement("Insumos"),
                            new XElement("ProveedorInsumos") // sección para los vínculos
                        )
                    );
                    BDXML.Save(ruta);
                }
                else
                {
                    // Si existe, lo cargo
                    BDXML = XDocument.Load(ruta);

                    // Me aseguro de que exista la sección ProveedorInsumos
                    if (BDXML.Root.Element("ProveedorInsumos") == null)
                    {
                        BDXML.Root.Add(new XElement("ProveedorInsumos"));
                        BDXML.Save(ruta);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o cargar el XML: " + ex.Message);
            }
        }

        public bool Eliminar(BEProveedorInsumo oBEProveedorInsumo)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo acceder al XML.");

                BDXML = XDocument.Load(ruta);

                var seccion = BDXML.Root.Element("ProveedorInsumos");
                if (seccion == null) return false;

                var elemento = seccion.Elements("proveedorInsumo")
                    .FirstOrDefault(pi =>
                        (int)pi.Element("Proveedor") == oBEProveedorInsumo.Proveedor.Id &&
                        (int)pi.Element("Insumo") == oBEProveedorInsumo.Insumo.Id
                    );

                if (elemento != null)
                {
                    elemento.Remove();
                    BDXML.Save(ruta);
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        public bool Guardar(BEProveedorInsumo oBEProveedorInsumo)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");

                BDXML = XDocument.Load(ruta);

                XElement seccion = BDXML.Root.Element("ProveedorInsumos");
                if (seccion == null)
                {
                    seccion = new XElement("ProveedorInsumos");
                    BDXML.Root.Add(seccion);
                }

                // --- MODIFICAR EXISTENTE ---
                if (oBEProveedorInsumo.Id > 0)
                {
                    var nodo = seccion.Elements("proveedorInsumo")
                                .FirstOrDefault(pi => int.Parse(pi.Attribute("Id").Value.Trim()) == oBEProveedorInsumo.Id);

                    if (nodo == null)
                        throw new Exception("No se encontró el vínculo proveedor-insumo.");

                    nodo.Element("Cantidad").Value = oBEProveedorInsumo.Cantidad.ToString("0.00");
                    nodo.Element("Precio").Value = oBEProveedorInsumo.Precio.ToString("0.00");

                    BDXML.Save(ruta);
                    return true;
                }
                // --- NUEVO REGISTRO ---
                else
                {
                    // Verificar existencia de vínculo
                    bool existe = seccion.Elements("proveedorInsumo")
                                    .Any(pi =>
                                        int.Parse(pi.Element("Proveedor").Value.Trim()) == oBEProveedorInsumo.Proveedor.Id &&
                                        int.Parse(pi.Element("Insumo").Value.Trim()) == oBEProveedorInsumo.Insumo.Id
                                    );

                    if (existe)
                        throw new Exception("El proveedor ya tiene este insumo vinculado");

                    int nuevoId = 1;
                    if (seccion.Elements("proveedorInsumo").Any())
                        nuevoId = seccion.Elements("proveedorInsumo").Max(pi => int.Parse(pi.Attribute("Id").Value.Trim())) + 1;

                    XElement nuevoProveedorInsumo = new XElement("proveedorInsumo",
                        new XAttribute("Id", nuevoId),
                        new XElement("Proveedor", oBEProveedorInsumo.Proveedor.Id),
                        new XElement("Insumo", oBEProveedorInsumo.Insumo.Id),
                        new XElement("Cantidad", oBEProveedorInsumo.Cantidad.ToString("0.00")),
                        new XElement("Precio", oBEProveedorInsumo.Precio.ToString("0.00"))
                    );

                    seccion.Add(nuevoProveedorInsumo);
                    BDXML.Save(ruta);

                    // Asignamos el Id recién creado al objeto
                    oBEProveedorInsumo.Id = nuevoId;

                    return true;
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }




        public BEProveedorInsumo ListarObjeto(BEProveedorInsumo oBEProveedorInsumo)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML.Root == null)
                    throw new XmlException("Error: No se pudo recuperar la información del XML.");

                XElement seccionProveedorInsumos = BDXML.Root.Element("ProveedorInsumos");
                if (seccionProveedorInsumos == null)
                    throw new Exception("No existe la sección <ProveedorInsumos> en el XML.");

                // Busco la relación proveedor-insumo
                var relacion = seccionProveedorInsumos.Elements("proveedorInsumo")
                    .FirstOrDefault(pi =>
                        int.Parse(pi.Element("Proveedor").Value.Trim()) == oBEProveedorInsumo.Proveedor.Id &&
                        int.Parse(pi.Element("Insumo").Value.Trim()) == oBEProveedorInsumo.Insumo.Id
                    );

                if (relacion == null)
                    throw new Exception("No se encontró el vínculo Insumo-Proveedor especificado.");

                // Recupero los datos del insumo desde <Insumos>
                XElement insumoXML = BDXML.Root.Element("Insumos")
                    ?.Elements("insumo")
                    .FirstOrDefault(ins => int.Parse(ins.Attribute("Id").Value.Trim()) == oBEProveedorInsumo.Insumo.Id);

                if (insumoXML == null)
                    throw new Exception("No se encontró el Insumo correspondiente en el XML.");

                // Armo el objeto BEProveedorInsumo
                BEProveedorInsumo resultado = new BEProveedorInsumo
                {
                    Id = int.Parse(relacion.Attribute("Id").Value),
                    Proveedor = new BEProveedor
                    {
                        Id = oBEProveedorInsumo.Proveedor.Id
                    },
                    Insumo = new BEInsumo
                    {
                        Id = int.Parse(insumoXML.Attribute("Id").Value),
                        Nombre = insumoXML.Element("Nombre").Value.Trim(),
                        UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), insumoXML.Element("UnidadMedida").Value.Trim())
                    },
                    Cantidad = decimal.Parse(relacion.Element("Cantidad").Value.Trim()),
                    Precio = decimal.Parse(relacion.Element("Precio").Value.Trim())
                };

                return resultado;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public List<BEProveedorInsumo> ListarPorProveedor(int proveedorId)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo cargar el archivo XML.");

                // Diccionario de insumos globales
                var insumosGlobales = BDXML.Root.Element("Insumos")?
                    .Descendants("insumo")
                    .ToDictionary(
                        i => int.Parse(i.Attribute("Id").Value.Trim()),
                        i => new BEInsumo
                        {
                            Id = int.Parse(i.Attribute("Id").Value.Trim()),
                            Nombre = i.Element("Nombre").Value.Trim(),
                            UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), i.Element("UnidadMedida").Value.Trim())
                        }
                    );

                if (insumosGlobales == null)
                    throw new Exception("Error: No se encontró la lista de insumos en el XML.");

                // Busco todos los proveedores-insumos del proveedor indicado
                var lista = (from pi in BDXML.Root.Element("ProveedorInsumos").Descendants("proveedorInsumo")
                             where int.Parse(pi.Element("Proveedor").Value.Trim()) == proveedorId
                             let insumoId = int.Parse(pi.Element("Insumo").Value.Trim())
                             where insumosGlobales.ContainsKey(insumoId)
                             select new BEProveedorInsumo
                             {
                                 Id = int.Parse(pi.Attribute("Id").Value.Trim()), // <--- asigno el Id correcto
                                 Proveedor = new BEProveedor
                                 {
                                     Id = proveedorId
                                 },
                                 Insumo = insumosGlobales[insumoId],
                                 Cantidad = decimal.Parse(pi.Element("Cantidad").Value.Trim()),
                                 Precio = decimal.Parse(pi.Element("Precio").Value.Trim())
                             }).ToList();

                return lista;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }




        public List<BEProveedorInsumo> ListarTodo()
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("Error: No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("Error: No se pudo cargar el archivo XML.");

                // Recupero la lista de insumos globales
                var insumosGlobales = BDXML.Root.Element("Insumos")?
                    .Descendants("insumo")
                    .ToDictionary(
                        p => int.Parse(p.Attribute("Id").Value.Trim()),
                        p => new BEInsumo
                        {
                            Id = int.Parse(p.Attribute("Id").Value.Trim()),
                            Nombre = p.Element("Nombre").Value.Trim(),
                            UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), p.Element("UnidadMedida").Value.Trim())
                        }
                    );

                if (insumosGlobales == null)
                    throw new Exception("Error: No se encontró la lista de insumos en el XML.");

                // Recupero la lista de relaciones proveedor-insumo
                var lista = (from relacion in BDXML.Root.Element("ProveedorInsumos").Descendants("proveedorInsumo")
                             let proveedorId = int.Parse(relacion.Element("Proveedor").Value.Trim())
                             let insumoId = int.Parse(relacion.Element("Insumo").Value.Trim())
                             where insumosGlobales.ContainsKey(insumoId)
                             select new BEProveedorInsumo
                             {
                                 Proveedor = new BEProveedor
                                 {
                                     Id = proveedorId
                                 },
                                 Insumo = insumosGlobales[insumoId],
                                 Cantidad = decimal.Parse(relacion.Element("Cantidad").Value.Trim()),
                                 Precio = decimal.Parse(relacion.Element("Precio").Value.Trim())
                             }).ToList();

                return lista;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public int ObtenerUltimoId()
        {
            try
            {
                // Verifico que exista el XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");

                // Cargo la información del XML
                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo recuperar la información del XML.");

                // Busco todos los Id de relaciones proveedor-insumo
                var listaIds = from relacion in BDXML.Root.Element("ProveedorInsumos").Descendants("proveedorInsumo")
                               select int.Parse(relacion.Attribute("Id").Value.Trim());

                // Si existen relaciones, devuelvo el máximo Id
                if (listaIds.Any())
                {
                    return listaIds.Max();
                }
                else
                {
                    // Si no hay relaciones, devuelvo 0
                    return 0;
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEProveedorInsumo oBEProveedorInsumo)
        {
            try
            {
                // Verifico que exista el XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");

                // Cargo la información del XML
                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo recuperar la información del XML.");

                // Busco si ya existe la relación proveedor-insumo
                var buscarRelacion = from relacion in BDXML.Root.Element("ProveedorInsumos").Descendants("proveedorInsumo")
                                     where relacion.Element("Proveedor").Value.Trim() == oBEProveedorInsumo.Proveedor.Id.ToString().Trim() &&
                                           relacion.Element("Insumo").Value.Trim() == oBEProveedorInsumo.Insumo.Id.ToString().Trim()
                                     select relacion;

                return buscarRelacion.Any();
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


    }
}
