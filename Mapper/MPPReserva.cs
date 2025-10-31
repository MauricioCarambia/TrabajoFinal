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
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML?.Root == null)
                    throw new XmlException("Error: XML vacío o mal formado.");

                XElement reservasRoot = BDXML.Root.Element("Reservas");
                if (reservasRoot == null)
                    throw new Exception("No existen reservas en el XML.");

                // Buscar la reserva por Id
                var reservaXml = reservasRoot.Elements("reserva")
                    .FirstOrDefault(r => r.Attribute("Id")?.Value.Trim() == oBEReserva.Id.ToString());

                if (reservaXml == null)
                    throw new Exception("No se encontró la reserva con el Id especificado.");

                // Eliminar la reserva del XML
                reservaXml.Remove();
                BDXML.Save(ruta);

                // Fin del Mapper: aquí **no se toca la mesa**
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

                        // Crear nuevo elemento reserva con Estado
                        XElement nuevaReserva = new XElement("reserva",
                            new XAttribute("Id", oBEReserva.Id),
                            new XElement("NumeroReserva", oBEReserva.NumeroReserva),
                            new XElement("FechaReserva", oBEReserva.FechaReserva.ToString("yyyy-MM-dd")),
                            new XElement("Cliente",
                                new XAttribute("Id", oBEReserva.Cliente.Id),
                                new XElement("Nombre", oBEReserva.Cliente.Nombre)
                            ),
                            new XElement("CantidadPersonas", oBEReserva.CantidadPersonas),
                            new XElement("Mesa",
                                new XAttribute("IdMesa", oBEReserva.Mesa.IdMesa),
                                new XElement("NumeroMesa", oBEReserva.Mesa.NumeroMesa)
                            ),
                            new XElement("Estado", oBEReserva.Estado.ToString()) // Nuevo elemento Estado
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
                                reservaModificada.Element("CantidadPersonas").Value = oBEReserva.CantidadPersonas.ToString();
                                reservaModificada.Element("Mesa").Element("NumeroMesa").Value = oBEReserva.Mesa.NumeroMesa.ToString();

                                // Guardar estado de la reserva
                                if (reservaModificada.Element("Estado") != null)
                                    reservaModificada.Element("Estado").Value = oBEReserva.Estado.ToString();
                                else
                                    reservaModificada.Add(new XElement("Estado", oBEReserva.Estado.ToString()));
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

        //public List<BEPedido> ListarPedidosPorFecha(DateTime fecha)
        //{
        //    try
        //    {
                
        //        if (!File.Exists(ruta))
        //            return new List<BEPedido>();

        //        XDocument xml = XDocument.Load(ruta);

        //        var reservasXml = xml.Root.Element("Reservas")?.Elements("reserva") ?? Enumerable.Empty<XElement>();
        //        var pedidosXmlAll = xml.Root.Element("Pedidos")?.Elements("pedido") ?? Enumerable.Empty<XElement>();
        //        var platosXmlAll = xml.Root.Element("Platos")?.Elements("Plato") ?? Enumerable.Empty<XElement>();

        //        // Filtrar reservas del día
        //        var reservasDia = reservasXml
        //            .Where(r => DateTime.TryParse(r.Element("FechaReserva")?.Value.Trim(), out DateTime f) && f.Date == fecha.Date)
        //            .ToList();

        //        List<BEPedido> listaPedidos = new List<BEPedido>();

        //        foreach (var resXml in reservasDia)
        //        {
        //            int reservaId = int.TryParse(resXml.Attribute("Id")?.Value.Trim(), out int rid) ? rid : 0;

        //            // Leer estado de la reserva
        //            var estadoReserva = Enum.TryParse(resXml.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva estRes)
        //                                ? estRes
        //                                : BEReserva.EstadoReserva.Abierta;

        //            // Buscar pedidos asociados a esa reserva
        //            var pedidosXml = pedidosXmlAll
        //                .Where(p => int.TryParse(p.Element("ReservaId")?.Value.Trim(), out int rid2) && rid2 == reservaId)
        //                .ToList();

        //            foreach (var pedXml in pedidosXml)
        //            {
        //                BEPedido pedido = new BEPedido
        //                {
        //                    Id = int.TryParse(pedXml.Attribute("Id")?.Value.Trim(), out int pid) ? pid : 0,
        //                    Estado = Enum.TryParse(pedXml.Element("Estado")?.Value.Trim(), out BEPedido.EstadoPedido est) ? est : BEPedido.EstadoPedido.Abierto,
        //                    Reserva = new BEReserva
        //                    {
        //                        Id = reservaId,
        //                        NumeroReserva = resXml.Element("NumeroReserva")?.Value.Trim() ?? "",
        //                        FechaReserva = DateTime.TryParse(resXml.Element("FechaReserva")?.Value.Trim(), out DateTime f2) ? f2 : DateTime.Today,
        //                        CantidadPersonas = int.TryParse(resXml.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 1,
        //                        Estado = estadoReserva,
        //                        Cliente = new BECliente
        //                        {
        //                            Id = int.TryParse(resXml.Element("Cliente")?.Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0,
        //                            Nombre = resXml.Element("Cliente")?.Element("Nombre")?.Value.Trim() ?? ""
        //                        },
        //                        Mesa = new BEMesa
        //                        {
        //                            IdMesa = int.TryParse(resXml.Element("Mesa")?.Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
        //                            NumeroMesa = int.TryParse(resXml.Element("Mesa")?.Element("NumeroMesa")?.Value.Trim(), out int mn) ? mn : 0
        //                        }
        //                    },
        //                    ListaPlatos = new List<BEPedidoPlato>()
        //                };

        //                // Cargar los platos del pedido
        //                var platosXml = pedXml.Element("PedidoPlatos")?.Elements("pedidoPlato") ?? Enumerable.Empty<XElement>();
        //                foreach (var pl in platosXml)
        //                {
        //                    int platoId = int.TryParse(pl.Element("PlatoId")?.Value.Trim(), out int platoPid) ? platoPid : 0;

        //                    var platoInfoXml = platosXmlAll.FirstOrDefault(x => int.TryParse(x.Attribute("Id")?.Value, out int pxid) && pxid == platoId);

        //                    string nombre = platoInfoXml?.Element("Nombre")?.Value.Trim() ?? "";
        //                    decimal precio = platoInfoXml != null && decimal.TryParse(platoInfoXml.Element("PrecioVenta")?.Value.Trim(), out decimal pv) ? pv : 0;

        //                    pedido.ListaPlatos.Add(new BEPedidoPlato
        //                    {
        //                        Id = int.TryParse(pl.Attribute("Id")?.Value.Trim(), out int plid) ? plid : 0,
        //                        Cantidad = int.TryParse(pl.Element("Cantidad")?.Value.Trim(), out int c) ? c : 1,
        //                        Estado = Enum.TryParse(pl.Element("Estado")?.Value.Trim(), out BEPedidoPlato.EstadoPlato estPl) ? estPl : BEPedidoPlato.EstadoPlato.Pendiente,
        //                        Plato = new BEPlato
        //                        {
        //                            Id = platoId,
        //                            Nombre = nombre,
        //                            PrecioVenta = precio
        //                        }
        //                    });
        //                }

        //                listaPedidos.Add(pedido);
        //            }
        //        }

        //        return listaPedidos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al listar los pedidos por fecha: " + ex.Message, ex);
        //    }
        //}
        public BEReserva ListarObjeto(BEReserva oBEReserva)
        {
            try
            {
                if (oBEReserva == null)
                    throw new ArgumentNullException(nameof(oBEReserva), "Error: No se proporcionó una reserva válida.");

                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML de reservas.");

                BDXML = XDocument.Load(ruta) ?? throw new XmlException("Error: No se pudo cargar la información del XML.");

                XElement reservasRoot = BDXML.Root.Element("Reservas");
                if (reservasRoot == null)
                    throw new Exception("Error: No existen reservas en el XML.");

                var buscarReserva = from res in reservasRoot.Descendants("reserva")
                                    where (oBEReserva.Id != 0 && res.Attribute("Id")?.Value.Trim() == oBEReserva.Id.ToString()) ||
                                          (!string.IsNullOrEmpty(oBEReserva.NumeroReserva) && res.Element("NumeroReserva")?.Value.Trim() == oBEReserva.NumeroReserva)
                                    select res;

                if (!buscarReserva.Any())
                    throw new Exception("Error: No se encontró ninguna reserva con los datos proporcionados.");

                var reservaEncontrada = buscarReserva.First();

                // Construir el objeto BEReserva con validaciones de null
                BEReserva reserva = new BEReserva
                {
                    Id = int.TryParse(reservaEncontrada.Attribute("Id")?.Value.Trim(), out int rid) ? rid : 0,
                    NumeroReserva = reservaEncontrada.Element("NumeroReserva")?.Value.Trim() ?? "",
                    FechaReserva = DateTime.TryParse(reservaEncontrada.Element("FechaReserva")?.Value.Trim(), out DateTime fecha) ? fecha : DateTime.MinValue,
                    CantidadPersonas = int.TryParse(reservaEncontrada.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 1,
                    Estado = Enum.TryParse(reservaEncontrada.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva est) ? est : BEReserva.EstadoReserva.Abierta,
                    Cliente = new BECliente
                    {
                        Id = int.TryParse(reservaEncontrada.Element("Cliente")?.Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0
                    },
                    Mesa = new BEMesa
                    {
                        IdMesa = int.TryParse(reservaEncontrada.Element("Mesa")?.Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
                        NumeroMesa = int.TryParse(reservaEncontrada.Element("Mesa")?.Element("NumeroMesa")?.Value.Trim(), out int mn) ? mn : 0,
                        Capacidad = int.TryParse(reservaEncontrada.Element("Mesa")?.Element("Capacidad")?.Value.Trim(), out int cap) ? cap : 0,
                        Estado = Enum.TryParse(reservaEncontrada.Element("Mesa")?.Element("Estado")?.Value.Trim(), out BEMesa.EstadoMesa em) ? em : BEMesa.EstadoMesa.Libre
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
                if (oBEReserva == null)
                    throw new ArgumentNullException(nameof(oBEReserva), "Error: No se proporcionó una reserva válida.");

                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                BDXML = XDocument.Load(ruta) ?? throw new XmlException("Error: No se pudo cargar el XML de reservas.");

                // Validar nodo <Reservas>
                var nodoReservas = BDXML.Root.Element("Reservas");
                if (nodoReservas == null)
                    throw new XmlException("Error: No se encontró el nodo <Reservas> en el XML.");

                // Buscar la reserva por Id
                var buscarReserva = nodoReservas.Descendants("reserva")
                    .FirstOrDefault(r => int.TryParse(r.Attribute("Id")?.Value.Trim(), out int rid) && rid == oBEReserva.Id);

                if (buscarReserva == null)
                    throw new Exception("Error: No se encontró la reserva con el Id proporcionado.");

                // Construir objeto BEReserva con validaciones de null y TryParse
                BEReserva reserva = new BEReserva
                {
                    Id = int.TryParse(buscarReserva.Attribute("Id")?.Value.Trim(), out int rid2) ? rid2 : 0,
                    NumeroReserva = buscarReserva.Element("NumeroReserva")?.Value.Trim() ?? "",
                    FechaReserva = DateTime.TryParse(buscarReserva.Element("FechaReserva")?.Value.Trim(), out DateTime fecha) ? fecha : DateTime.MinValue,
                    CantidadPersonas = int.TryParse(buscarReserva.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 1,
                    Estado = Enum.TryParse(buscarReserva.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva estado) ? estado : BEReserva.EstadoReserva.Abierta,
                    Cliente = new BECliente
                    {
                        Id = int.TryParse(buscarReserva.Element("Cliente")?.Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0
                    },
                    Mesa = new BEMesa
                    {
                        IdMesa = int.TryParse(buscarReserva.Element("Mesa")?.Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
                        NumeroMesa = int.TryParse(buscarReserva.Element("Mesa")?.Element("NumeroMesa")?.Value.Trim(), out int mn) ? mn : 0
                    }
                };

                return reserva;
            }
            catch (XmlException) { throw; }
            catch (Exception) { throw; }
        }

        public List<BEReserva> ListarPorFecha(DateTime fecha)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                BDXML = XDocument.Load(ruta) ?? throw new XmlException("Error: No se pudo cargar el XML de reservas.");

                var reservasRoot = BDXML.Root.Element("Reservas");
                if (reservasRoot == null)
                    return new List<BEReserva>();

                List<BEReserva> listaReservas = (from r in reservasRoot.Elements("reserva")
                                                 let fechaStr = r.Element("FechaReserva")?.Value.Trim() ?? ""
                                                 let fechaR = DateTime.TryParse(fechaStr, out DateTime fr) ? fr : DateTime.MinValue
                                                 where fechaR.Date == fecha.Date
                                                 select new BEReserva
                                                 {
                                                     Id = int.TryParse(r.Attribute("Id")?.Value.Trim(), out int id) ? id : 0,
                                                     NumeroReserva = r.Element("NumeroReserva")?.Value.Trim() ?? "",
                                                     FechaReserva = fechaR,
                                                     CantidadPersonas = int.TryParse(r.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 0,
                                                     Estado = Enum.TryParse(r.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva est) ? est : BEReserva.EstadoReserva.Abierta,

                                                     Cliente = r.Element("Cliente") != null
                                                        ? new BECliente
                                                        {
                                                            Id = int.TryParse(r.Element("Cliente").Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0,
                                                            Nombre = r.Element("Cliente").Element("Nombre")?.Value.Trim() ?? ""
                                                        }
                                                        : new BECliente(),

                                                     Mesa = r.Element("Mesa") != null
                                                        ? new BEMesa
                                                        {
                                                            IdMesa = int.TryParse(r.Element("Mesa").Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
                                                            NumeroMesa = int.TryParse(r.Element("Mesa").Element("NumeroMesa")?.Value.Trim(), out int num) ? num : 0
                                                        }
                                                        : new BEMesa()
                                                 }).ToList();

                return listaReservas;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public List<BEReserva> ListarTodo()
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar o crear el XML de reservas.");

                BDXML = XDocument.Load(ruta) ?? throw new XmlException("Error: No se pudo cargar el XML de reservas.");

                var reservasRoot = BDXML.Root.Element("Reservas");
                if (reservasRoot == null)
                    return new List<BEReserva>();

                List<BEReserva> listaReservas = (from r in reservasRoot.Elements("reserva")
                                                 let fechaReservaStr = r.Element("FechaReserva")?.Value.Trim() ?? ""
                                                 select new BEReserva
                                                 {
                                                     Id = int.TryParse(r.Attribute("Id")?.Value.Trim(), out int id) ? id : 0,
                                                     NumeroReserva = r.Element("NumeroReserva")?.Value.Trim() ?? "",
                                                     FechaReserva = DateTime.TryParse(fechaReservaStr, out DateTime fecha) ? fecha : DateTime.MinValue,
                                                     CantidadPersonas = int.TryParse(r.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 0,

                                                     Estado = Enum.TryParse(r.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva est)
                                                              ? est
                                                              : BEReserva.EstadoReserva.Abierta, // Valor por defecto

                                                     Cliente = r.Element("Cliente") != null
                                                        ? new BECliente
                                                        {
                                                            Id = int.TryParse(r.Element("Cliente").Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0,
                                                            Nombre = r.Element("Cliente").Element("Nombre")?.Value.Trim() ?? ""
                                                        }
                                                        : new BECliente(),

                                                     Mesa = r.Element("Mesa") != null
                                                        ? new BEMesa
                                                        {
                                                            IdMesa = int.TryParse(r.Element("Mesa").Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
                                                            NumeroMesa = int.TryParse(r.Element("Mesa").Element("NumeroMesa")?.Value.Trim(), out int num) ? num : 0
                                                        }
                                                        : new BEMesa()
                                                 }).ToList();

                return listaReservas;
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw new Exception("Error al listar todas las reservas: " + ex.Message, ex); }
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
