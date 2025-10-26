using Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Entity.BEInsumo;

namespace Mapper
{
    public class MPPInsumo
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        public bool CrearXML()
        {
            try
            {
                // Si no existe el archivo XML lo crea:
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Insumos")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    // Si existe el XML, verifico que exista el elemento <Insumos>
                    BDXML = XDocument.Load(ruta);
                    XElement insumos = BDXML.Root.Element("Insumos");

                    if (insumos != null)
                    {
                        return true;
                    }
                    else
                    {
                        // Si no existe el elemento <Insumos>, lo creo
                        insumos = new XElement("Insumos");
                        BDXML.Root.Add(insumos);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public bool Eliminar(BEInsumo oBEInsumo)
        {
            try
            {
                // Verifico que exista el XML
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);

                    // Verifico que el XML se haya cargado correctamente
                    if (BDXML != null)
                    {
                        // Busco el Insumo por su Id
                        var buscarInsumo = from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                           where insumo.Attribute("Id").Value.Trim() == oBEInsumo.Id.ToString().Trim()
                                           select insumo;

                        // Verifico que exista el insumo
                        if (buscarInsumo.Any())
                        {
                            var insumoEncontrado = buscarInsumo.First();

                            // Si el insumo está vinculado a algún proveedor o compra, podrías validar antes de eliminar.
                            // Por ahora lo eliminamos directamente.

                            insumoEncontrado.Remove();
                            BDXML.Save(ruta);
                            return true;
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró el Insumo que intenta eliminar.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar los datos del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }



        public bool Guardar(BEInsumo oBEInsumo)
        {
            try
            {
                // Verifico si está creado el XML
                if (CrearXML())
                {
                    // Cargo la ruta del XML
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        // Verifico si es un alta (nuevo insumo)
                        if (oBEInsumo.Id == 0)
                        {
                            // Verifico que no exista un insumo con el mismo nombre
                            if (!VerificarExistenciaObjeto(oBEInsumo))
                            {
                                // Obtengo el siguiente Id disponible
                                int nuevoId = ObtenerUltimoId() + 1;
                                oBEInsumo.Id = nuevoId;

                                // Agrego el nuevo insumo al XML
                                BDXML.Root.Element("Insumos").Add(new XElement("insumo",
                                    new XAttribute("Id", oBEInsumo.Id.ToString().Trim()),
                                    new XElement("Nombre", oBEInsumo.Nombre.Trim()),
                                    new XElement("UnidadMedida", oBEInsumo.UnidadMedida.ToString()),
                                    new XElement("Cantidad", oBEInsumo.Cantidad.ToString().Trim()),
                                    new XElement("Precio", oBEInsumo.Precio.ToString("0.00").Trim())
                                ));

                                BDXML.Save(ruta);
                                return true;
                            }
                            else
                            {
                                throw new Exception("Error: No se puede dar de alta un Insumo que ya existe.");
                            }
                        }
                        // Si ya existe, lo modifico
                        else
                        {
                            // Busco el insumo en el XML
                            var buscarInsumo = from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                               where insumo.Attribute("Id").Value.Trim() == oBEInsumo.Id.ToString().Trim()
                                               select insumo;

                            if (buscarInsumo.Any())
                            {
                                foreach (XElement insumoModificado in buscarInsumo)
                                {
                                    insumoModificado.Element("Nombre").Value = oBEInsumo.Nombre.Trim();
                                    insumoModificado.Element("UnidadMedida").Value = oBEInsumo.UnidadMedida.ToString();
                                    insumoModificado.Element("Cantidad").Value = oBEInsumo.Cantidad.ToString().Trim();
                                    insumoModificado.Element("Precio").Value = oBEInsumo.Precio.ToString("0.00").Trim();
                                }

                                BDXML.Save(ruta);
                                return true;
                            }
                            else
                            {
                                throw new Exception("Error: No se pudo recuperar los datos del Insumo con el Id brindado.");
                            }
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar los datos del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo acceder o crear el XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }
        public BEInsumo ListarObjetoPorId(int idInsumo)
        {
            try
            {
                // Cargar XML
                XDocument xml = XDocument.Load(ruta); // rutaInsumos es la ruta a tu XML de insumos

                // Buscar el insumo por Id
                var elemento = xml.Root.Elements("insumo")
                    .FirstOrDefault(x => (int)x.Attribute("Id") == idInsumo);

                if (elemento == null)
                    return null;

                // Mapear a BEInsumo
                BEInsumo insumo = new BEInsumo
                {
                    Id = (int)elemento.Attribute("Id"),
                    Nombre = (string)elemento.Element("Nombre"),
                    UnidadMedida = Enum.Parse<UnidadesMedida>((string)elemento.Element("UnidadMedida")),
                    Cantidad = decimal.Parse((string)elemento.Element("Cantidad")), // usar decimal si es necesario
                    Precio = decimal.Parse((string)elemento.Element("Precio"))
                };

                return insumo;
            }
            catch
            {
                return null; // En caso de error
            }
        }
       
        public BEInsumo ListarObjeto(BEInsumo oBEInsumo)
        {
            try
            {
                // Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);

                    if (BDXML != null)
                    {
                        // Busco el Insumo por ID o por Nombre
                        var buscarInsumo = from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                           where insumo.Element("Nombre").Value.Trim() == oBEInsumo.Nombre.Trim() ||
                                                 insumo.Attribute("Id").Value.Trim() == oBEInsumo.Id.ToString().Trim()
                                           select insumo;

                        // Verifico que exista el insumo:
                        if (buscarInsumo.Any())
                        {
                            var insumoEncontrado = buscarInsumo.First();

                            if (insumoEncontrado != null)
                            {
                                // Recupero los datos principales del insumo
                                BEInsumo insumo = new BEInsumo
                                {
                                    Id = int.Parse(insumoEncontrado.Attribute("Id").Value.Trim()),
                                    Nombre = insumoEncontrado.Element("Nombre").Value.Trim(),
                                    UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), insumoEncontrado.Element("UnidadMedida").Value.Trim()),
                                    Cantidad = decimal.Parse(insumoEncontrado.Element("Cantidad").Value.Trim()),
                                    Precio = decimal.Parse(insumoEncontrado.Element("Precio").Value.Trim())
                                };

                                return insumo;
                            }
                            else
                            {
                                throw new Exception("Error: No existe el Insumo con el Id brindado!");
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró el Insumo con el nombre brindado!");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML!");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML!");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BEInsumo> ListarTodo()
        {
            try
            {
                // Verifico que el XML exista:
                if (CrearXML() == true)
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);

                    if (BDXML != null)
                    {
                        // Recupero la lista de insumos y sus datos básicos:
                        var listaInsumos = (from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                            select new BEInsumo
                                            {
                                                Id = int.Parse(insumo.Attribute("Id").Value.Trim()),
                                                Nombre = insumo.Element("Nombre").Value.Trim(),
                                                UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), insumo.Element("UnidadMedida").Value.Trim()),
                                                Cantidad = decimal.Parse(insumo.Element("Cantidad").Value.Trim()),
                                                Precio = decimal.Parse(insumo.Element("Precio").Value.Trim())
                                            }).ToList();

                        return listaInsumos;
                    }
                    else
                    {
                        throw new Exception("Error: No se pudo recuperar los datos del XML!");
                    }
                }
                else
                {
                    throw new Exception("Error: No se pudo recuperar el XML!");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public int ObtenerUltimoId()
        {
            try
            {
                // Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    // Cargo la información del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        // Busco todos los Id de insumos
                        var pId = from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                  select int.Parse(insumo.Attribute("Id").Value.Trim());

                        // Si existen insumos, devuelvo el máximo Id
                        if (pId.Any())
                        {
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        // Si no hay insumos, devuelvo 0
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML!");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML!");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public bool VerificarExistenciaObjeto(BEInsumo oBEInsumo)
        {
            try
            {
                // Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    // Cargo la información del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEInsumo.Id >= 0)
                        {
                            // Busco si existe al menos un Insumo con el mismo Nombre (ignorando mayúsculas/minúsculas)
                            var buscarInsumo = from insumo in BDXML.Root.Element("Insumos").Descendants("insumo")
                                               where insumo.Element("Nombre").Value.Trim().ToLower() == oBEInsumo.Nombre.Trim().ToLower()
                                               select insumo;

                            // Si existe al menos uno, devuelvo true
                            if (buscarInsumo.Any())
                            {
                                return true;
                            }
                            // Si no, devuelvo false
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo recuperar el Id del Insumo!");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML!");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML!");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public void DescontarStock(int idInsumo, decimal cantidadADescontar)
        {
            if (!CrearXML()) return;

            XDocument xml = XDocument.Load(ruta);

            var insumo = xml.Descendants("insumo")
                            .FirstOrDefault(i => (int)i.Attribute("Id") == idInsumo);

            if (insumo != null)
            {
                // Parseamos el stock actual usando la cultura es-AR
                decimal stockActual = decimal.Parse(insumo.Element("Cantidad").Value, new CultureInfo("es-AR"));

                // Ajustamos el stock: si cantidadADescontar es positivo, resta; si es negativo, suma
                decimal nuevoStock = stockActual - cantidadADescontar;

                // Evitamos stock negativo
                if (nuevoStock < 0) nuevoStock = 0;

                // Guardamos con 2 decimales y coma como separador decimal
                insumo.Element("Cantidad").Value = nuevoStock.ToString("0.##", new CultureInfo("es-AR"));

                xml.Save(ruta);
            }
        }

    }
}
