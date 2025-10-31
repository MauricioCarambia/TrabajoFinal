using Entity;
using Entity.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Entity.BEMesa;

namespace Mapper
{
    public class MPPMesa
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;



        public bool CrearXML()
        {
            try
            {
                //Verifico que exista el XML:
                if (!(File.Exists(ruta)))
                {
                    //Si no existe, lo creo:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Mesas")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Usuarios
                    BDXML = XDocument.Load(ruta);
                    XElement mesa = BDXML.Root.Element("Mesas");
                    //Si existe, devuelvo true:
                    if (mesa != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        mesa = new XElement("Mesas");
                        BDXML.Root.Add(mesa);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public void Eliminar(BEMesa oBEMesa)
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
                        // Busco la mesa por su Id:
                        var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                         where mesa.Attribute("IdMesa").Value.Trim() == oBEMesa.IdMesa.ToString().Trim()
                                         select mesa;

                        // Verifico si se encontró la mesa:
                        if (buscarMesa.Any())
                        {
                            // Obtengo la mesa encontrada y la elimino:
                            var mesaEncontrada = buscarMesa.First();
                            mesaEncontrada.Remove();

                            // Guardo los cambios en el XML:
                            BDXML.Save(ruta);
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró la mesa con el Id especificado.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo crear o acceder al archivo XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public void Guardar(BEMesa oBEMesa)
        {
            try
            {
                // Verifico que se cargue el XML:
                if (CrearXML() == true)
                {
                    // Cargar el XML existente
                    BDXML = XDocument.Load(ruta);

                    if (BDXML != null)
                    {
                        // Verificar que el objeto Mesa no sea nulo
                        if (oBEMesa != null)
                        {
                            // Si es una mesa nueva (sin Id)
                            if (oBEMesa.IdMesa == 0)
                            {
                                // Verificar si ya existe una mesa con el mismo número
                                if (VerificarExistenciaObjeto(oBEMesa) == false)
                                {
                                    // Obtener nuevo Id autoincremental
                                    int ultimoIdMesa = ObtenerUltimoIdMesa() + 1;
                                    oBEMesa.IdMesa = ultimoIdMesa;

                                    // Agregar nueva mesa al XML
                                    BDXML.Root.Element("Mesas").Add(new XElement("mesa",
                                        new XAttribute("IdMesa", oBEMesa.IdMesa.ToString().Trim()),
                                        new XElement("NumeroMesa", oBEMesa.NumeroMesa.ToString().Trim()),
                                        new XElement("Capacidad", oBEMesa.Capacidad.ToString().Trim()),
                                        new XElement("Estado", oBEMesa.Estado.ToString().Trim())
                                    ));

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: Ya existe una mesa con ese número.");
                                }
                            }
                            // En caso de que la mesa ya exista, se modifica
                            else
                            {
                                var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                                 where mesa.Attribute("IdMesa").Value.Trim() == oBEMesa.IdMesa.ToString().Trim()
                                                 select mesa;

                                if (buscarMesa.Any())
                                {
                                    foreach (XElement mesaModificada in buscarMesa)
                                    {
                                        mesaModificada.Element("NumeroMesa").Value = oBEMesa.NumeroMesa.ToString().Trim();
                                        mesaModificada.Element("Capacidad").Value = oBEMesa.Capacidad.ToString().Trim();
                                        mesaModificada.Element("Estado").Value = oBEMesa.Estado.ToString().Trim();
                                    }

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: No se encontró la mesa que se intenta modificar.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos de la mesa.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }



        public BEMesa ListarObjeto(BEMesa oBEMesa)
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEMesa != null)
                        {
                            // Busco la mesa por IdMesa
                            var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                             where int.Parse(mesa.Attribute("IdMesa").Value.Trim()) == oBEMesa.IdMesa
                                             select mesa;

                            if (buscarMesa.Any())
                            {
                                var mesaEncontrada = buscarMesa.First();

                                BEMesa mesa = new BEMesa
                                {
                                    IdMesa = int.Parse(mesaEncontrada.Attribute("IdMesa").Value.Trim()),
                                    NumeroMesa = int.Parse(mesaEncontrada.Element("NumeroMesa").Value.Trim()),
                                    Capacidad = int.Parse(mesaEncontrada.Element("Capacidad").Value.Trim()),
                                    Estado = Enum.Parse<BEMesa.EstadoMesa>(mesaEncontrada.Element("Estado").Value.Trim())
                                };

                                return mesa;
                            }
                            else
                            {
                                throw new Exception("Error: No se encontró ninguna mesa con los datos brindados!");
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener la información de la mesa!");
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
        }

        public BEMesa ListarObjetoPorNumeroMesa(BEMesa oBEMesa)
        {
            try
            {
                if (CrearXML() == true)
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEMesa != null)
                        {
                            // Buscar por NúmeroMesa (no por IdMesa)
                            var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                             where int.Parse(mesa.Element("NumeroMesa").Value.Trim()) == oBEMesa.NumeroMesa
                                             select mesa;

                            if (buscarMesa.Any())
                            {
                                var mesaEncontrada = buscarMesa.First();

                                BEMesa mesa = new BEMesa
                                {
                                    IdMesa = int.Parse(mesaEncontrada.Attribute("IdMesa").Value.Trim()),
                                    NumeroMesa = int.Parse(mesaEncontrada.Element("NumeroMesa").Value.Trim()),
                                    Capacidad = int.Parse(mesaEncontrada.Element("Capacidad").Value.Trim()),
                                    Estado = Enum.Parse<BEMesa.EstadoMesa>(mesaEncontrada.Element("Estado").Value.Trim())
                                };

                                return mesa;
                            }
                            else
                            {
                                throw new Exception("Error: No se encontró ninguna mesa con el número brindado!");
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos de la mesa!");
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
        }
        public BEMesa ListarObjetoPorIdMesa(BEMesa oBEMesa)
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEMesa != null)
                        {
                            // Busco la mesa por IdMesa
                            var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                             where int.Parse(mesa.Attribute("IdMesa").Value.Trim()) == oBEMesa.IdMesa
                                             select mesa;

                            if (buscarMesa.Any())
                            {
                                var mesaEncontrada = buscarMesa.First();

                                BEMesa mesa = new BEMesa
                                {
                                    IdMesa = int.Parse(mesaEncontrada.Attribute("IdMesa").Value.Trim()),
                                    NumeroMesa = int.Parse(mesaEncontrada.Element("NumeroMesa").Value.Trim()),
                                    Capacidad = int.Parse(mesaEncontrada.Element("Capacidad").Value.Trim()),
                                    Estado = Enum.Parse<BEMesa.EstadoMesa>(mesaEncontrada.Element("Estado").Value.Trim())
                                };

                                return mesa;
                            }
                            else
                            {
                                throw new Exception("Error: No se encontró ninguna mesa con los datos brindados!");
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos de la mesa!");
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
        }


        public List<BEMesa> ListarTodo()
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    // Cargo la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        List<BEMesa> listaMesas = new List<BEMesa>();

                        var lista = from mesa in BDXML.Root.Element("Mesas").Elements("mesa")
                                    select new BEMesa
                                    {
                                        IdMesa = int.Parse(mesa.Attribute("IdMesa").Value),
                                        Capacidad = int.Parse(mesa.Element("Capacidad").Value),
                                        NumeroMesa = int.Parse(mesa.Element("NumeroMesa").Value),
                                        Estado = (EstadoMesa)Enum.Parse(typeof(EstadoMesa), mesa.Element("Estado").Value)
                                    };

                        listaMesas = lista.ToList();
                        return listaMesas;
                    }
                    else
                    {
                        throw new Exception("Error: No se pudo recuperar la información del XML!");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int ObtenerUltimoIdMesa()
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se carguen los datos:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pIdMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                  select int.Parse(mesa.Attribute("IdMesa").Value.Trim());
                        //Verifico si encontro algun IdMesa:
                        if (pIdMesa.Any())
                        {
                            //Busco el IdMesa mas alto:
                            int ultimoIdMesa = pIdMesa.Max();
                            return ultimoIdMesa;
                        }
                        //Si no encontro, devuelvo 0:
                        else { return 0; }
                    }
                    else { throw new XmlException("Error: no se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificarExistenciaObjeto(BEMesa oBEMesa)
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEMesa != null)
                        {
                            // Busco la existencia de la mesa por NumeroMesa
                            var buscarMesa = from mesa in BDXML.Root.Element("Mesas").Descendants("mesa")
                                             where int.Parse(mesa.Element("NumeroMesa").Value) == oBEMesa.NumeroMesa
                                             select mesa;

                            return buscarMesa.Any();
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener la información de la Mesa");
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
        }

        public void ActualizarEstadoMesa(int idMesa, BEMesa.EstadoMesa nuevoEstado)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML de mesas.");

            XDocument BDXML = XDocument.Load(ruta);

            var mesa = BDXML.Descendants("mesa")
                            .FirstOrDefault(m => int.Parse(m.Attribute("IdMesa").Value) == idMesa);

            if (mesa == null)
                throw new Exception($"No se encontró la mesa con Id {idMesa}.");

            mesa.Element("Estado").Value = nuevoEstado.ToString();

            BDXML.Save(ruta);
        }

    }
}
