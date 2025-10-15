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
    public class MPPCliente
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
                    new XElement("Clientes")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Usuarios
                    BDXML = XDocument.Load(ruta);
                    XElement cliente = BDXML.Root.Element("Clientes");
                    //Si existe, devuelvo true:
                    if (cliente != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        cliente = new XElement("Clientes");
                        BDXML.Root.Add(cliente);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public void Eliminar(BECliente oBECliente)
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
                        // Busco al cliente por su Id:
                        var buscarCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                            where cliente.Attribute("IdCliente").Value.Trim() == oBECliente.IdCliente.ToString().Trim()
                                            select cliente;

                        // Verifico si se encontró el cliente:
                        if (buscarCliente.Any())
                        {
                            // Obtengo el primer cliente encontrado y lo elimino:
                            var clienteEncontrado = buscarCliente.First();
                            clienteEncontrado.Remove();

                            // Guardo los cambios en el XML:
                            BDXML.Save(ruta);
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró el cliente con el Id especificado.");
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


        public void Guardar(BECliente oBECliente)
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
                        // Verificar que el objeto Cliente no sea nulo
                        if (oBECliente != null)
                        {
                            // Si es un cliente nuevo (sin Id)
                            if (oBECliente.IdCliente == 0)
                            {
                                // Verificar si ya existe un cliente con el mismo DNI
                                if (VerificarExistenciaObjeto(oBECliente) == false)
                                {
                                    // Obtener nuevo Id autoincremental
                                    int ultimoIdCliente = ObtenerUltimoIdCliente() + 1;
                                    oBECliente.IdCliente = ultimoIdCliente;

                                    // Agregar nuevo cliente al XML
                                    BDXML.Root.Element("Clientes").Add(new XElement("cliente",
                                        new XAttribute("IdCliente", oBECliente.IdCliente.ToString().Trim()),
                                        new XElement("Nombre", oBECliente.Nombre.Trim()),
                                        new XElement("DNI", oBECliente.DNI.Trim()),
                                        new XElement("Telefono", oBECliente.Telefono.Trim())
                                    ));

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: Ya existe un cliente con ese DNI.");
                                }
                            }
                            // En caso de que el cliente ya exista, se modifica
                            else
                            {
                                var buscarCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                                    where cliente.Attribute("IdCliente").Value.Trim() == oBECliente.IdCliente.ToString().Trim()
                                                    select cliente;

                                if (buscarCliente.Any())
                                {
                                    foreach (XElement clienteModificado in buscarCliente)
                                    {
                                        clienteModificado.Element("Nombre").Value = oBECliente.Nombre.Trim();
                                        clienteModificado.Element("DNI").Value = oBECliente.DNI.Trim();
                                        clienteModificado.Element("Telefono").Value = oBECliente.Telefono.Trim();
                                    }

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: No se encontró el cliente que se intenta modificar.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos del cliente.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar o crear el XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BECliente ListarObjeto(BECliente oBECliente)
        {
            try
            {
                if (oBECliente == null)
                    throw new ArgumentNullException(nameof(oBECliente), "Error: No se proporcionó un cliente válido.");

                // Verifico la existencia del XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML!");

                // Cargo el XML
                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo recuperar la información del XML!");

                // Busco el cliente por IdCliente
                var buscarCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                    where cliente.Element("DNI").Value.Trim() == oBECliente.DNI
                                    select cliente;

                if (!buscarCliente.Any())
                    throw new Exception("Error: No se encontró ningún cliente con el Id brindado!");

                var clienteEncontrado = buscarCliente.First();

                // Construyo el objeto Cliente
                BECliente ocliente = new BECliente
                {
                    IdCliente = int.Parse(clienteEncontrado.Attribute("IdCliente").Value.Trim()),
                    Nombre = clienteEncontrado.Element("Nombre").Value.Trim(),
                    DNI = clienteEncontrado.Element("DNI").Value.Trim(),
                    Telefono = clienteEncontrado.Element("Telefono").Value.Trim()
                };

                return ocliente;
            }
            catch (XmlException) { throw; }
            catch (Exception) { throw; }
        }



        public BECliente ListarObjetoPorIdCliente(BECliente oBECliente)
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true) // Se asume que CrearXML() también valida el XML de clientes
                {
                    // Cargo el XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBECliente != null)
                        {
                            // Busco el cliente por IdCliente
                            var buscarCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                                where int.Parse(cliente.Attribute("IdCliente").Value.Trim()) == oBECliente.IdCliente
                                                select cliente;

                            if (buscarCliente.Any())
                            {
                                var clienteEncontrado = buscarCliente.First();

                                BECliente cliente = new BECliente
                                {
                                    IdCliente = int.Parse(clienteEncontrado.Attribute("IdCliente").Value.Trim()),
                                    Nombre = clienteEncontrado.Element("Nombre").Value.Trim(),
                                    DNI = clienteEncontrado.Element("DNI").Value.Trim(),
                                    Telefono = clienteEncontrado.Element("Telefono").Value.Trim()
                                };

                                return cliente;
                            }
                            else
                            {
                                throw new Exception("Error: No se encontró ningún cliente con los datos brindados!");
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos del cliente!");
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



        public List<BECliente> ListarTodo()
        {
            try
            {
                // Verifico la existencia del XML:
                if (CrearXML() == true) // Se asume que CrearXML() también valida el XML de clientes
                {
                    // Cargo la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        List<BECliente> listaClientes = new List<BECliente>();

                        var lista = from cliente in BDXML.Root.Element("Clientes").Elements("cliente")
                                    select new BECliente
                                    {
                                        IdCliente = int.Parse(cliente.Attribute("IdCliente").Value),
                                        Nombre = cliente.Element("Nombre").Value.Trim(),
                                        DNI = cliente.Element("DNI").Value.Trim(),
                                        Telefono = cliente.Element("Telefono").Value.Trim()
                                    };

                        listaClientes = lista.ToList();
                        return listaClientes;
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



        public int ObtenerUltimoIdCliente()
        {
            try
            {
                // Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    // Verifico que se carguen los datos:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pIdCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                         select int.Parse(cliente.Attribute("IdCliente").Value.Trim());

                        // Verifico si encontró algún IdCliente:
                        if (pIdCliente.Any())
                        {
                            // Busco el IdCliente más alto:
                            int ultimoIdCliente = pIdCliente.Max();
                            return ultimoIdCliente;
                        }
                        // Si no encontró, devuelvo 0:
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: no se pudo recuperar la información del XML!");
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

        public bool VerificarExistenciaObjeto(BECliente oBECliente)
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
                        if (oBECliente != null)
                        {
                            // Busco la existencia del cliente por DNI (o cualquier campo único)
                            var buscarCliente = from cliente in BDXML.Root.Element("Clientes").Descendants("cliente")
                                                where cliente.Element("DNI").Value.Trim() == oBECliente.DNI
                                                select cliente;

                            return buscarCliente.Any();
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener la información del cliente");
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

    }
}
