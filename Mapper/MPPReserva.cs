using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Entity.BEMesa;

namespace Mapper
{
    public class MPPReserva
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
                    new XElement("Reservas")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Usuarios
                    BDXML = XDocument.Load(ruta);
                    XElement reserva = BDXML.Root.Element("Reservas");
                    //Si existe, devuelvo true:
                    if (reserva != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        reserva = new XElement("Reservas");
                        BDXML.Root.Add(reserva);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public void Eliminar(BEReserva oBEReserva)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);

                    if (BDXML != null)
                    {
                        // Busco la reserva por su Id
                        var buscarReserva = from reserva in BDXML.Root.Element("Reservas").Descendants("reserva")
                                            where reserva.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString().Trim()
                                            select reserva;

                        if (buscarReserva.Any())
                        {
                            var reservaEncontrada = buscarReserva.First();
                            reservaEncontrada.Remove();
                            BDXML.Save(ruta);
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró la reserva con el Id especificado.");
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



        public void Guardar(BEReserva oBEReserva)
        {
            try
            {
                if (oBEReserva == null)
                    throw new ArgumentNullException(nameof(oBEReserva), "Error: No se proporcionó una reserva válida.");

                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML == null)
                        throw new XmlException("Error: No se pudo cargar el XML.");

                    // Si es una reserva nueva
                    if (oBEReserva.Id == 0)
                    {
                        // Generar nuevo Id autoincremental
                        int ultimoId = 0;
                        var reservasExistentes = BDXML.Root.Element("Reservas").Descendants("reserva");
                        if (reservasExistentes.Any())
                            ultimoId = reservasExistentes.Max(r => (int)r.Attribute("Id"));

                        oBEReserva.Id = ultimoId + 1;

                        // Generar número de reserva único (ej: R-20251015-001)
                        string fechaStr = oBEReserva.FechaReserva.ToString("yyyyMMdd");
                        int contador = reservasExistentes.Count() + 1;
                        oBEReserva.NumeroReserva = $"R-{fechaStr}-{contador:D3}";

                        // Crear nuevo elemento reserva
                        XElement nuevaReserva = new XElement("reserva",
                            new XAttribute("Id", oBEReserva.Id),
                            new XElement("NumeroReserva", oBEReserva.NumeroReserva),
                            new XElement("FechaReserva", oBEReserva.FechaReserva.ToString("yyyy-MM-dd")),
                            new XElement("Cliente",
                                new XAttribute("IdCliente", oBEReserva.Cliente.IdCliente),
                                new XElement("Nombre", oBEReserva.Cliente.Nombre)
                            ),
                            new XElement("CantidadPersonas", oBEReserva.CantidadPersonas),
                            new XElement("Mesa",
                                new XAttribute("IdMesa", oBEReserva.Mesa.IdMesa),
                                new XElement("NumeroMesa", oBEReserva.Mesa.NumeroMesa)
                            )
                        );

                        BDXML.Root.Element("Reservas").Add(nuevaReserva);
                        BDXML.Save(ruta);
                    }
                    else
                    {
                        // Modificar reserva existente
                        var buscarReserva = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                            where r.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString()
                                            select r;

                        if (buscarReserva.Any())
                        {
                            foreach (XElement reservaModificada in buscarReserva)
                            {
                                reservaModificada.Element("FechaReserva").Value = oBEReserva.FechaReserva.ToString("yyyy-MM-dd");
                                reservaModificada.Element("Cliente").Element("Nombre").Value = oBEReserva.Cliente.Nombre;
                                reservaModificada.Element("CantidadPersonas").Value = oBEReserva.CantidadPersonas.ToString();
                                reservaModificada.Element("Mesa").Element("NumeroMesa").Value = oBEReserva.Mesa.NumeroMesa.ToString();
                            }

                            BDXML.Save(ruta);
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró la reserva que se intenta modificar.");
                        }
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo crear o acceder al XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }



        public BEReserva ListarObjeto(BEReserva oBEReserva)
        {
            try
            {
                if (oBEReserva == null)
                    throw new ArgumentNullException(nameof(oBEReserva), "Error: No se proporcionó una reserva válida.");

                // Verifico la existencia del XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML de reservas.");

                // Cargo el XML
                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo recuperar la información del XML.");

                // Busco la reserva por Id o NúmeroReserva
                var buscarReserva = from res in BDXML.Root.Element("Reservas").Descendants("reserva")
                                    where (oBEReserva.Id != 0 && res.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString()) ||
                                          (!string.IsNullOrEmpty(oBEReserva.NumeroReserva) && res.Element("NumeroReserva").Value.Trim() == oBEReserva.NumeroReserva)
                                    select res;

                if (!buscarReserva.Any())
                    throw new Exception("Error: No se encontró ninguna reserva con los datos proporcionados.");

                var reservaEncontrada = buscarReserva.First();

                // Construyo el objeto Reserva
                BEReserva reserva = new BEReserva
                {
                    Id = int.Parse(reservaEncontrada.Attribute("Id").Value.Trim()),
                    NumeroReserva = reservaEncontrada.Element("NumeroReserva").Value.Trim(),
                    FechaReserva = DateTime.Parse(reservaEncontrada.Element("FechaReserva").Value.Trim()),
                    CantidadPersonas = int.Parse(reservaEncontrada.Element("CantidadPersonas").Value.Trim()),
                    Cliente = new BECliente
                    {
                        IdCliente = int.Parse(reservaEncontrada.Element("Cliente").Attribute("IdCliente").Value.Trim()),
                        Nombre = reservaEncontrada.Element("Cliente").Element("Nombre").Value.Trim(),
                        DNI = reservaEncontrada.Element("Cliente").Element("DNI").Value.Trim(),
                        Telefono = reservaEncontrada.Element("Cliente").Element("Telefono").Value.Trim()
                    },
                    Mesa = new BEMesa
                    {
                        IdMesa = int.Parse(reservaEncontrada.Element("Mesa").Attribute("IdMesa").Value.Trim()),
                        NumeroMesa = int.Parse(reservaEncontrada.Element("Mesa").Element("NumeroMesa").Value.Trim()), // Convertir a int
                        Capacidad = int.Parse(reservaEncontrada.Element("Mesa").Element("Capacidad").Value.Trim()), // Convertir a int
                        Estado = (EstadoMesa)Enum.Parse(typeof(EstadoMesa), reservaEncontrada.Element("Mesa").Element("Estado").Value.Trim())
                    }
                };

                return reserva;
            }
            catch (XmlException) { throw; }
            catch (Exception) { throw; }
        }


        public BEReserva ListarObjetoPorIdReserva(BEReserva oBEReserva)
        {
            try
            {
                if (CrearXML() == false)
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo cargar el XML de reservas.");

                if (oBEReserva == null)
                    throw new ArgumentNullException(nameof(oBEReserva), "Error: No se proporcionó una reserva válida.");

                // Busco la reserva por Id
                var buscarReserva = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                    where int.Parse(r.Attribute("Id").Value.Trim()) == oBEReserva.Id
                                    select r;

                if (!buscarReserva.Any())
                    throw new Exception("Error: No se encontró la reserva con el Id proporcionado.");

                var reservaEncontrada = buscarReserva.First();

                // Construyo el objeto BEReserva
                BEReserva reserva = new BEReserva
                {
                    Id = int.Parse(reservaEncontrada.Attribute("Id").Value.Trim()),
                    NumeroReserva = reservaEncontrada.Element("NumeroReserva").Value.Trim(),
                    FechaReserva = DateTime.Parse(reservaEncontrada.Element("FechaReserva").Value.Trim()),
                    CantidadPersonas = int.Parse(reservaEncontrada.Element("CantidadPersonas").Value.Trim()),
                    Cliente = new BECliente
                    {
                        IdCliente = int.Parse(reservaEncontrada.Element("Cliente").Attribute("IdCliente").Value.Trim()),
                        Nombre = reservaEncontrada.Element("Cliente").Element("Nombre").Value.Trim()
                        // Podés agregar DNI y Teléfono si lo guardás
                    },
                    Mesa = new BEMesa
                    {
                        IdMesa = int.Parse(reservaEncontrada.Element("Mesa").Attribute("IdMesa").Value.Trim()),
                        NumeroMesa = int.Parse(reservaEncontrada.Element("Mesa").Element("NumeroMesa").Value.Trim()),
                    }
                };

                return reserva;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }



        public List<BEReserva> ListarTodo()
        {
            try
            {
                // Verifico la existencia del XML de reservas
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo cargar el XML de reservas.");

                List<BEReserva> listaReservas = new List<BEReserva>();

                var lista = from r in BDXML.Root.Element("Reservas").Elements("reserva")
                            select new BEReserva
                            {
                                Id = int.Parse(r.Attribute("Id").Value.Trim()),
                                NumeroReserva = r.Element("NumeroReserva").Value.Trim(),
                                FechaReserva = DateTime.Parse(r.Element("FechaReserva").Value.Trim()),
                                CantidadPersonas = int.Parse(r.Element("CantidadPersonas").Value.Trim()),
                                Cliente = new BECliente
                                {
                                    IdCliente = int.Parse(r.Element("Cliente").Attribute("IdCliente").Value.Trim()),
                                    Nombre = r.Element("Cliente").Element("Nombre").Value.Trim()
                                },
                                Mesa = new BEMesa
                                {
                                    IdMesa = int.Parse(r.Element("Mesa").Attribute("IdMesa").Value.Trim()),
                                    NumeroMesa = int.Parse(r.Element("Mesa").Element("NumeroMesa").Value.Trim())
                                }
                            };

                listaReservas = lista.ToList();
                return listaReservas;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public int ObtenerUltimoIdReserva()
        {
            try
            {
                // Verifico que exista el XML de reservas
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                // Cargo el XML
                BDXML = XDocument.Load(ruta);
                if (BDXML != null)
                {
                    var pIdReserva = from reserva in BDXML.Root.Element("Reservas").Descendants("reserva")
                                     select int.Parse(reserva.Attribute("Id").Value.Trim());

                    // Verifico si encontró algún Id
                    if (pIdReserva.Any())
                    {
                        // Busco el Id más alto
                        return pIdReserva.Max();
                    }
                    else
                    {
                        return 0; // Si no hay reservas
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar la información del XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public bool VerificarExistenciaObjeto(BEReserva oBEReserva)
        {
            try
            {
                // Verifico la existencia del XML de reservas
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                // Cargo el XML
                BDXML = XDocument.Load(ruta);
                if (BDXML != null)
                {
                    if (oBEReserva != null)
                    {
                        // Busco si ya existe la reserva por Número de Reserva
                        var buscarReserva = from reserva in BDXML.Root.Element("Reservas").Descendants("reserva")
                                            where reserva.Element("NumeroReserva").Value.Trim() == oBEReserva.NumeroReserva
                                            select reserva;

                        return buscarReserva.Any();
                    }
                    else
                    {
                        throw new Exception("Error: No se pudo obtener la información de la reserva.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar la información del XML de reservas.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

    }
}
