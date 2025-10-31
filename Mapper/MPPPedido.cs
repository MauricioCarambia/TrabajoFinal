using Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPPedido
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Reservas"),
                            new XElement("Pedidos")
                        )
                    );
                    BDXML.Save(ruta);
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML.Root.Element("Reservas") == null)
                        BDXML.Root.Add(new XElement("Reservas"));
                    if (BDXML.Root.Element("Pedidos") == null)
                        BDXML.Root.Add(new XElement("Pedidos"));
                    BDXML.Save(ruta);
                }
                return true;
            }
            catch { throw; }
        }
        public void EliminarPlato(int pedidoId, int platoId, int cantidadAEliminar)
        {
            if (cantidadAEliminar <= 0) return; // nada que eliminar

            XDocument xml = XDocument.Load(ruta);

            var pedidoXml = xml.Root.Element("Pedidos")
                                    .Elements("pedido")
                                    .FirstOrDefault(p => (int)p.Attribute("Id") == pedidoId);

            if (pedidoXml != null)
            {
                var platoXml = pedidoXml.Element("PedidoPlatos")
                                        .Elements("pedidoPlato")
                                        .FirstOrDefault(pp => (int)pp.Attribute("Id") == platoId);

                if (platoXml != null)
                {
                    int cantidadActual = int.Parse(platoXml.Element("Cantidad").Value);
                    int nuevaCantidad = cantidadActual - cantidadAEliminar;

                    if (nuevaCantidad <= 0)
                    {
                        // eliminar plato si se elimina toda la cantidad
                        platoXml.Remove();
                    }
                    else
                    {
                        // actualizar cantidad en XML
                        platoXml.SetElementValue("Cantidad", nuevaCantidad);
                    }

                    xml.Save(ruta);
                }
            }
        }

        private int ObtenerUltimoId()
        {
            if (!File.Exists(ruta)) return 0;

            XDocument doc = XDocument.Load(ruta);
            var ids = doc.Descendants("pedido")
                         .Attributes("Id")
                         .Select(a => int.TryParse(a.Value, out int id) ? id : 0);
            return ids.Any() ? ids.Max() : 0;
        }

        // Guardar original
        //public void Guardar(BEPedido oBEPedido)
        //{
        //    try
        //    {
        //        if (oBEPedido == null) throw new ArgumentNullException(nameof(oBEPedido));
        //        if (oBEPedido.Reserva == null) throw new ArgumentException("El pedido debe tener reserva asignada.");

        //        CrearXML(); // asegura que BDXML esté cargado y tenga la estructura
        //        XElement pedidosElement = BDXML.Root.Element("Pedidos");

        //        // Buscar pedido existente por ReservaId
        //        XElement pedidoExistente = pedidosElement.Elements("pedido")
        //            .FirstOrDefault(p => ((int?)p.Element("ReservaId") ?? 0) == oBEPedido.Reserva.Id);

        //        if (pedidoExistente == null)
        //        {
        //            // Nuevo pedido
        //            int nextPedidoId = ObtenerUltimoId() + 1;

        //            pedidoExistente = new XElement("pedido",
        //                new XAttribute("Id", nextPedidoId),
        //                new XElement("ReservaId", oBEPedido.Reserva.Id),
        //                new XElement("Estado", oBEPedido.Estado.ToString()),
        //                new XElement("PedidoPlatos")
        //            );
        //            pedidosElement.Add(pedidoExistente);
        //            oBEPedido.Id = nextPedidoId;
        //        }
        //        else
        //        {
        //            // Actualizar pedido existente
        //            pedidoExistente.SetElementValue("Estado", oBEPedido.Estado.ToString());
        //            if (pedidoExistente.Element("PedidoPlatos") == null)
        //                pedidoExistente.Add(new XElement("PedidoPlatos"));
        //        }

        //        XElement pedidoPlatosElement = pedidoExistente.Element("PedidoPlatos");

        //        // Agregar o actualizar platos, acumulando cantidades si ya existen
        //        foreach (var plato in oBEPedido.ListaPlatos)
        //        {
        //            var platoXml = pedidoPlatosElement.Elements("pedidoPlato")
        //                .FirstOrDefault(pp => (int)pp.Element("PlatoId") == plato.Plato.Id);

        //            if (platoXml != null)
        //            {
        //                // Acumular cantidad si el plato ya existe
        //                int cantidadExistente = int.Parse(platoXml.Element("Cantidad")?.Value ?? "0");
        //                platoXml.SetElementValue("Cantidad", cantidadExistente + plato.Cantidad);
        //                platoXml.SetElementValue("Estado", plato.Estado.ToString());
        //            }
        //            else
        //            {
        //                // Nuevo plato
        //                int nextPlatoId = pedidoPlatosElement.Elements("pedidoPlato")
        //                                        .Select(pp => (int?)pp.Attribute("Id") ?? 0)
        //                                        .DefaultIfEmpty(0).Max() + 1;

        //                XElement nuevoPlato = new XElement("pedidoPlato",
        //                    new XAttribute("Id", nextPlatoId),
        //                    new XElement("PlatoId", plato.Plato?.Id ?? 0),
        //                    new XElement("Cantidad", plato.Cantidad),
        //                    new XElement("Estado", plato.Estado.ToString())
        //                );
        //                pedidoPlatosElement.Add(nuevoPlato);
        //            }
        //        }

        //        BDXML.Save(ruta);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al guardar el pedido: " + ex.Message, ex);
        //    }
        //}

        public void Guardar(BEPedido oBEPedido)
        {
            try
            {
                if (oBEPedido == null) throw new ArgumentNullException(nameof(oBEPedido));
                if (oBEPedido.Reserva == null) throw new ArgumentException("El pedido debe tener reserva asignada.");

                CrearXML();
                XElement pedidosElement = BDXML.Root.Element("Pedidos");

                XElement pedidoExistente = pedidosElement.Elements("Pedido")
                    .FirstOrDefault(p => ((int?)p.Element("ReservaId") ?? 0) == oBEPedido.Reserva.Id);

                if (pedidoExistente == null)
                {
                    int nextPedidoId = pedidosElement.Elements("Pedido")
                                            .Select(p => (int?)p.Attribute("Id") ?? 0)
                                            .DefaultIfEmpty(0).Max() + 1;

                    pedidoExistente = new XElement("Pedido",
                        new XAttribute("Id", nextPedidoId),
                        new XElement("ReservaId", oBEPedido.Reserva.Id),
                        new XElement("Estado", oBEPedido.Estado.ToString()),
                        new XElement("Total", 0),
                        new XElement("PedidoPlatos")
                    );
                    pedidosElement.Add(pedidoExistente);
                    oBEPedido.Id = nextPedidoId;
                }
                else
                {
                    pedidoExistente.SetElementValue("Estado", oBEPedido.Estado.ToString());
                    if (pedidoExistente.Element("PedidoPlatos") == null)
                        pedidoExistente.Add(new XElement("PedidoPlatos"));
                }

                XElement pedidoPlatosElement = pedidoExistente.Element("PedidoPlatos");
                var culture = new CultureInfo("es-AR"); // decimales con coma

                // 🔹 Total existente (para sumar los nuevos platos)
                decimal totalPedido = 0;
                var subtotalesExistentes = pedidoPlatosElement.Elements("PedidoPlato")
                    .Select(pp => decimal.Parse(pp.Element("Subtotal")?.Value ?? "0", culture));

                totalPedido += subtotalesExistentes.Sum();

                // 🔹 Recorrer los platos nuevos
                foreach (var plato in oBEPedido.ListaPlatos)
                {
                    decimal subtotal = plato.Cantidad * plato.Plato.PrecioVenta;

                    var platoXml = pedidoPlatosElement.Elements("PedidoPlato")
                        .FirstOrDefault(pp =>
                            (int)pp.Element("PlatoId") == plato.Plato.Id &&
                            pp.Element("Estado")?.Value == plato.Estado.ToString()
                        );

                    if (platoXml != null)
                    {
                        int cantidadExistente = int.Parse(platoXml.Element("Cantidad")?.Value ?? "0");
                        decimal subtotalExistente = decimal.Parse(platoXml.Element("Subtotal")?.Value ?? "0", culture);

                        platoXml.SetElementValue("Cantidad", cantidadExistente + plato.Cantidad);
                        platoXml.SetElementValue("Subtotal", (subtotalExistente + subtotal).ToString("0.00", culture));
                    }
                    else
                    {
                        int nextPlatoId = pedidoPlatosElement.Elements("PedidoPlato")
                                                .Select(pp => (int?)pp.Attribute("Id") ?? 0)
                                                .DefaultIfEmpty(0).Max() + 1;

                        XElement nuevoPlato = new XElement("PedidoPlato",
                            new XAttribute("Id", nextPlatoId),
                            new XElement("PlatoId", plato.Plato?.Id ?? 0),
                            new XElement("Cantidad", plato.Cantidad),
                            new XElement("Estado", plato.Estado.ToString()),
                            new XElement("Subtotal", subtotal.ToString("0.00", culture))
                        );
                        pedidoPlatosElement.Add(nuevoPlato);
                    }

                    totalPedido += subtotal; // sumar al total general
                }

                pedidoExistente.SetElementValue("Total", totalPedido.ToString("0.00", culture));

                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el pedido: " + ex.Message, ex);
            }
        }

        public BEPedido ListarObjetoPorId(int pedidoId)
        {
            if (!CrearXML()) throw new Exception("No se pudo crear o cargar el XML.");
            if (BDXML == null) throw new Exception("El XML no está cargado.");

            var pedXml = BDXML.Root.Element("Pedidos")
                .Elements("Pedido")
                .FirstOrDefault(p => int.Parse(p.Attribute("Id").Value) == pedidoId);
            if (pedXml == null) return null;

            int reservaId = int.Parse(pedXml.Element("ReservaId").Value);

            var resXml = BDXML.Root.Element("Reservas")
                .Elements("reserva")
                .FirstOrDefault(r => int.Parse(r.Attribute("Id").Value) == reservaId);
            if (resXml == null) return null;

            var pedido = new BEPedido
            {
                Id = pedidoId,
                Estado = Enum.TryParse(pedXml.Element("Estado")?.Value, out BEPedido.EstadoPedido est) ? est : BEPedido.EstadoPedido.Abierto,
                Reserva = new BEReserva
                {
                    Id = reservaId,
                    NumeroReserva = resXml.Element("NumeroReserva")?.Value ?? "",
                    FechaReserva = DateTime.TryParse(resXml.Element("FechaReserva")?.Value, out DateTime fecha) ? fecha : DateTime.Today,
                    Cliente = new BECliente
                    {
                        Id = int.Parse(resXml.Element("Cliente")?.Attribute("IdCliente")?.Value ?? "0"),
                        Nombre = resXml.Element("Cliente")?.Element("Nombre")?.Value ?? "",
                        DNI = resXml.Element("Cliente")?.Element("DNI")?.Value ?? "",
                        Telefono = resXml.Element("Cliente")?.Element("Telefono")?.Value ?? ""
                    },
                    Mesa = new BEMesa
                    {
                        IdMesa = int.Parse(resXml.Element("Mesa")?.Attribute("IdMesa")?.Value ?? "0"),
                        NumeroMesa = int.Parse(resXml.Element("Mesa")?.Element("NumeroMesa")?.Value ?? "0")
                    }
                },
                ListaPlatos = new List<BEPedidoPlato>()
            };

            // ✅ Leer el total del XML (nuevo)
            decimal total = 0;
            var totalElem = pedXml.Element("Total");
            if (totalElem != null)
            {
                decimal.TryParse(totalElem.Value, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("es-AR"), out total);
            }
            pedido.Total = total;

            var platosXml = pedXml.Element("PedidoPlatos")?.Elements("PedidoPlato");
            if (platosXml != null)
            {
                MPPPlato mppPlato = new MPPPlato();
                var culture = new System.Globalization.CultureInfo("es-AR");

                foreach (var pl in platosXml)
                {
                    int platoId = int.Parse(pl.Element("PlatoId")?.Value ?? "0");
                    BEPlato platoCompleto = mppPlato.ListarObjetoPorId(new BEPlato { Id = platoId })
                        ?? new BEPlato { Id = platoId, Nombre = "Sin nombre", PrecioVenta = 0 };

                    int cantidad = int.Parse(pl.Element("Cantidad")?.Value ?? "0");

                    pedido.ListaPlatos.Add(new BEPedidoPlato
                    {
                        Id = int.Parse(pl.Attribute("Id")?.Value ?? "0"),
                        Cantidad = cantidad,
                        Estado = Enum.TryParse(pl.Element("Estado")?.Value, out BEPedidoPlato.EstadoPlato estado) ? estado : BEPedidoPlato.EstadoPlato.Pendiente,
                        Plato = platoCompleto
                    });
                }
            }

            return pedido;
        }


        public List<BEPedido> ListarTodo()
        {
            List<BEPedido> listaPedidos = new List<BEPedido>();

            if (!File.Exists(ruta)) return listaPedidos;

            BDXML = XDocument.Load(ruta);
            var pedidosXml = BDXML.Root.Element("Pedidos")?.Elements("pedido");
            if (pedidosXml == null) return listaPedidos;

            MPPCliente mppCliente = new MPPCliente();
            MPPReserva mppReserva = new MPPReserva();
            MPPMesa mppMesa = new MPPMesa();
            MPPPlato mppPlato = new MPPPlato();

            foreach (var pedidoXml in pedidosXml)
            {
                int reservaId = int.TryParse(pedidoXml.Element("ReservaId")?.Value, out int rid) ? rid : 0;
                int clienteId = int.TryParse(pedidoXml.Element("ClienteId")?.Value, out int cid) ? cid : 0;

                // Cargar objetos completos
                BECliente cliente = mppCliente.ListarObjetoPorIdCliente(new BECliente { Id = clienteId }) ?? new BECliente { Id = clienteId, Nombre = "Sin cliente" };
                BEReserva reserva = mppReserva.ListarObjetoPorIdReserva(new BEReserva { Id = reservaId }) ?? new BEReserva { Id = reservaId, NumeroReserva = "-", Mesa = new BEMesa { NumeroMesa = 0 } };

                BEPedido pedido = new BEPedido
                {
                    Id = int.TryParse(pedidoXml.Attribute("Id")?.Value, out int pid) ? pid : 0,
                    Reserva = reserva,
                    Cliente = cliente,
                    Fecha = DateTime.TryParse(pedidoXml.Element("Fecha")?.Value, out DateTime f) ? f : DateTime.Now,
                    Estado = Enum.TryParse(pedidoXml.Element("Estado")?.Value, out BEPedido.EstadoPedido est) ? est : BEPedido.EstadoPedido.Abierto,
                    ListaPlatos = new List<BEPedidoPlato>()
                };

                // Cargar los platos
                var platosXml = pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato");
                if (platosXml != null)
                {
                    foreach (var pp in platosXml)
                    {
                        BEPlato plato = mppPlato.ListarObjetoPorId(new BEPlato { Id = int.Parse(pp.Element("PlatoId")?.Value ?? "0") });
                        pedido.ListaPlatos.Add(new BEPedidoPlato
                        {
                            Plato = plato,
                            Cantidad = int.TryParse(pp.Element("Cantidad")?.Value, out int cant) ? cant : 0,
                            Estado = Enum.TryParse(pp.Element("Estado")?.Value, out BEPedidoPlato.EstadoPlato ep) ? ep : BEPedidoPlato.EstadoPlato.Pendiente
                        });
                    }
                }

                listaPedidos.Add(pedido);
            }

            return listaPedidos;
        }
        public List<BEPedido> ListarPedidosPorFecha(DateTime fecha)
        {
            try
            {
                if (!File.Exists(ruta))
                    return new List<BEPedido>();

                XDocument xml = XDocument.Load(ruta);

                var reservasXml = xml.Root.Element("Reservas")?.Elements("reserva") ?? Enumerable.Empty<XElement>();
                var pedidosXmlAll = xml.Root.Element("Pedidos")?.Elements("Pedido") ?? Enumerable.Empty<XElement>();
                var platosXmlAll = xml.Root.Element("Platos")?.Elements("Plato") ?? Enumerable.Empty<XElement>();

                // Filtrar reservas del día
                var reservasDia = reservasXml
                    .Where(r => DateTime.TryParse(r.Element("FechaReserva")?.Value.Trim(), out DateTime f) && f.Date == fecha.Date)
                    .ToList();

                List<BEPedido> listaPedidos = new List<BEPedido>();

                var culture = new System.Globalization.CultureInfo("es-AR"); // Para parseo de decimales con coma

                foreach (var resXml in reservasDia)
                {
                    int reservaId = int.TryParse(resXml.Attribute("Id")?.Value.Trim(), out int rid) ? rid : 0;

                    var estadoReserva = Enum.TryParse(resXml.Element("Estado")?.Value.Trim(), out BEReserva.EstadoReserva estRes)
                                        ? estRes
                                        : BEReserva.EstadoReserva.Abierta;

                    // Buscar pedidos asociados a esa reserva
                    var pedidosXml = pedidosXmlAll
                        .Where(p => int.TryParse(p.Element("ReservaId")?.Value.Trim(), out int rid2) && rid2 == reservaId)
                        .ToList();

                    foreach (var pedXml in pedidosXml)
                    {
                        BEPedido pedido = new BEPedido
                        {
                            Id = int.TryParse(pedXml.Attribute("Id")?.Value.Trim(), out int pid) ? pid : 0,
                            Estado = Enum.TryParse(pedXml.Element("Estado")?.Value.Trim(), out BEPedido.EstadoPedido est) ? est : BEPedido.EstadoPedido.Abierto,
                            Total = decimal.TryParse(pedXml.Element("Total")?.Value.Trim(), NumberStyles.Any, culture, out decimal total) ? total : 0,
                            Reserva = new BEReserva
                            {
                                Id = reservaId,
                                NumeroReserva = resXml.Element("NumeroReserva")?.Value.Trim() ?? "",
                                FechaReserva = DateTime.TryParse(resXml.Element("FechaReserva")?.Value.Trim(), out DateTime f2) ? f2 : DateTime.Today,
                                CantidadPersonas = int.TryParse(resXml.Element("CantidadPersonas")?.Value.Trim(), out int cp) ? cp : 1,
                                Estado = estadoReserva,
                                Cliente = new BECliente
                                {
                                    Id = int.TryParse(resXml.Element("Cliente")?.Attribute("Id")?.Value.Trim(), out int cid) ? cid : 0,
                                    Nombre = resXml.Element("Cliente")?.Element("Nombre")?.Value.Trim() ?? ""
                                },
                                Mesa = new BEMesa
                                {
                                    IdMesa = int.TryParse(resXml.Element("Mesa")?.Attribute("IdMesa")?.Value.Trim(), out int mid) ? mid : 0,
                                    NumeroMesa = int.TryParse(resXml.Element("Mesa")?.Element("NumeroMesa")?.Value.Trim(), out int mn) ? mn : 0
                                }
                            },
                            ListaPlatos = new List<BEPedidoPlato>()
                        };

                        // Cargar los platos del pedido
                        var platosXml = pedXml.Element("PedidoPlatos")?.Elements("PedidoPlato") ?? Enumerable.Empty<XElement>();
                        foreach (var pl in platosXml)
                        {
                            int platoId = int.TryParse(pl.Element("PlatoId")?.Value.Trim(), out int platoPid) ? platoPid : 0;

                            var platoInfoXml = platosXmlAll.FirstOrDefault(x => int.TryParse(x.Attribute("Id")?.Value, out int pxid) && pxid == platoId);

                            string nombre = platoInfoXml?.Element("Nombre")?.Value.Trim() ?? "";
                            decimal precio = platoInfoXml != null && decimal.TryParse(platoInfoXml.Element("PrecioVenta")?.Value.Trim(), NumberStyles.Any, culture, out decimal pv) ? pv : 0;
                            decimal subtotal = decimal.TryParse(pl.Element("Subtotal")?.Value.Trim(), NumberStyles.Any, culture, out decimal st) ? st : precio * (int.Parse(pl.Element("Cantidad")?.Value ?? "1"));

                            pedido.ListaPlatos.Add(new BEPedidoPlato
                            {
                                Id = int.TryParse(pl.Attribute("Id")?.Value.Trim(), out int plid) ? plid : 0,
                                Cantidad = int.TryParse(pl.Element("Cantidad")?.Value.Trim(), out int c) ? c : 1,
                                Estado = Enum.TryParse(pl.Element("Estado")?.Value.Trim(), out BEPedidoPlato.EstadoPlato estPl) ? estPl : BEPedidoPlato.EstadoPlato.Pendiente,
                                Plato = new BEPlato
                                {
                                    Id = platoId,
                                    Nombre = nombre,
                                    PrecioVenta = precio
                                }
                            });
                        }

                        listaPedidos.Add(pedido);
                    }
                }

                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los pedidos por fecha: " + ex.Message, ex);
            }
        }

        public BEPedido ListarPorReserva(int reservaId)
        {
            if (!File.Exists(ruta))
                return null;

            XDocument xml = XDocument.Load(ruta);

            // Tomar todos los pedidos que tengan la misma reserva
            var pedidosXml = xml.Root.Elements("pedido")
                .Where(p => int.Parse(p.Element("ReservaId")?.Value ?? "0") == reservaId)
                .ToList();

            if (!pedidosXml.Any())
                return null;

            BEPedido pedidoUnificado = new BEPedido
            {
                Id = pedidosXml.First().Attribute("Id") != null ? int.Parse(pedidosXml.First().Attribute("Id").Value) : 0,
                Reserva = new BEReserva { Id = reservaId },
                Cliente = new BECliente { Id = int.Parse(pedidosXml.First().Element("ClienteId")?.Value ?? "0") },
                ListaPlatos = new List<BEPedidoPlato>(),
                Estado = Enum.TryParse(pedidosXml.First().Element("Estado")?.Value, out BEPedido.EstadoPedido estado) ? estado : BEPedido.EstadoPedido.Abierto
            };

            MPPPlato mppPlato = new MPPPlato();

            foreach (var pedidoXml in pedidosXml)
            {
                var platosXml = pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato");
                if (platosXml == null) continue;

                foreach (var pp in platosXml)
                {
                    int platoId = int.Parse(pp.Element("PlatoId")?.Value ?? "0");
                    BEPlato plato = mppPlato.ListarObjetoPorId(new BEPlato { Id = platoId });

                    int cantidad = int.Parse(pp.Element("Cantidad")?.Value ?? "0");
                    BEPedidoPlato.EstadoPlato estadoPlato = Enum.TryParse(pp.Element("Estado")?.Value, out BEPedidoPlato.EstadoPlato ep) ? ep : BEPedidoPlato.EstadoPlato.Pendiente;

                    // Verifico si ya existe el plato en la lista unificada
                    var existente = pedidoUnificado.ListaPlatos.FirstOrDefault(p => p.Plato.Id == plato.Id);
                    if (existente != null)
                    {
                        existente.Cantidad += cantidad; // acumulo cantidades
                    }
                    else
                    {
                        pedidoUnificado.ListaPlatos.Add(new BEPedidoPlato
                        {
                            Plato = plato,
                            Cantidad = cantidad,
                            Estado = estadoPlato
                        });
                    }
                }
            }

            return pedidoUnificado;
        }

        public List<BEPedidoPlato> ObtenerPlatosPorEstado(string estado)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var lista = new List<BEPedidoPlato>();

            foreach (var pedido in BDXML.Descendants("Pedido"))
            {
                int idPedido = int.Parse(pedido.Attribute("Id").Value);

                var platos = from p in pedido.Descendants("PedidoPlato")
                             where string.Equals((string)p.Element("Estado"), estado, StringComparison.OrdinalIgnoreCase)
                             select new BEPedidoPlato
                             {
                                 Id = int.Parse(p.Attribute("Id").Value),
                                 IdPedido = idPedido,
                                 Plato = new BEPlato
                                 {
                                     Id = int.Parse(p.Element("PlatoId").Value)
                                 },
                                 Cantidad = int.Parse(p.Element("Cantidad").Value),
                                 Estado = Enum.Parse<BEPedidoPlato.EstadoPlato>(p.Element("Estado").Value),
                                 //Subtotal = decimal.Parse(p.Element("Subtotal").Value)
                             };

                lista.AddRange(platos);
            }

            return lista;
        }


        public bool VerificarExistenciaObjeto(BEPedido oBEPedido)
        {
            if (!CrearXML()) return false;

            BDXML = XDocument.Load(ruta);
            return BDXML.Root.Element("Pedidos")?
                   .Elements("pedido")
                   .Any(p => (int)p.Element("ReservaId") == oBEPedido.Reserva?.Id)
                   ?? false;
        }

        public bool VerificarStockPedido(BEPedido pedido, out List<string> errores)
        {
            errores = new List<string>();
            if (!File.Exists("BD.xml"))
            {
                errores.Add("No se encontró el archivo XML de stock de insumos.");
                return false;
            }

            XDocument xml = XDocument.Load(ruta);

            foreach (var plato in pedido.ListaPlatos)
            {
                var insumosPlatoXml = xml.Root.Element("PlatoInsumos")?
                                        .Elements("PlatoInsumo")
                                        .Where(p => (int)p.Element("IdPlato") == plato.Plato.Id);

                if (insumosPlatoXml == null) continue;

                foreach (var piXml in insumosPlatoXml)
                {
                    int idInsumo = (int)piXml.Element("IdInsumo");
                    decimal cantidadNecesaria = decimal.Parse(piXml.Element("Cantidad").Value) * plato.Cantidad;

                    var insumoXml = xml.Root.Element("Insumos")?
                                        .Elements("insumo")
                                        .FirstOrDefault(i => (int)i.Attribute("Id") == idInsumo);

                    decimal stockDisponible = 0;
                    string nombreInsumo = $"Id {idInsumo}";
                    if (insumoXml != null)
                    {
                        nombreInsumo = insumoXml.Element("Nombre")?.Value ?? nombreInsumo;
                        stockDisponible = decimal.Parse(insumoXml.Element("Cantidad").Value.Trim());
                    }

                    if (cantidadNecesaria > stockDisponible)
                    {
                       errores.Add($"No hay stock suficiente de {nombreInsumo}.\nNecesario: {cantidadNecesaria}.\nDisponible: {stockDisponible}\npara el plato: {plato.Plato.Nombre}");
    
                    }
                }
            }

            return !errores.Any();
        }

        public void DescontarStockInsumos(BEPedido pedido)
        {
            if (pedido == null || pedido.ListaPlatos == null) return;
            if (!File.Exists("BD.xml")) throw new Exception("No se encontró el archivo XML de insumos.");

            XDocument xml = XDocument.Load("BD.xml");

            foreach (var pedidoPlato in pedido.ListaPlatos)
            {
                // Solo descontar si el plato está Confirmado
                if (pedidoPlato.Estado != BEPedidoPlato.EstadoPlato.Cargado)
                    continue;

                if (pedidoPlato.Plato?.ListaInsumos == null) continue;

                foreach (var platoInsumo in pedidoPlato.Plato.ListaInsumos)
                {
                    int idInsumo = platoInsumo.Insumo.Id;
                    decimal cantidadUsada = platoInsumo.Cantidad * pedidoPlato.Cantidad;

                    var insumoXml = xml.Root.Element("Insumos")?
                                        .Elements("insumo")
                                        .FirstOrDefault(i => (int)i.Attribute("Id") == idInsumo);

                    if (insumoXml != null)
                    {
                        decimal cantidadActual = decimal.Parse(insumoXml.Element("Cantidad").Value.Trim());
                        cantidadActual -= cantidadUsada;
                        if (cantidadActual < 0) cantidadActual = 0; // opcional

                        insumoXml.Element("Cantidad").SetValue(cantidadActual.ToString("0.##"));
                    }
                }

                // Opcional: marcar en XML que este plato ya descontó stock
                var pedidoXml = xml.Root.Element("Pedidos")?
                                  .Elements("pedido")
                                  .FirstOrDefault(p => (int)p.Attribute("Id") == pedido.Id);

                var pedidoPlatoXml = pedidoXml?.Element("PedidoPlatos")?
                                       .Elements("pedidoPlato")
                                       .FirstOrDefault(pp => (int)pp.Element("PlatoId") == pedidoPlato.Plato.Id);

                if (pedidoPlatoXml != null)
                    pedidoPlatoXml.SetElementValue("Estado", pedidoPlato.Estado.ToString());
            }

            xml.Save("BD.xml");
        }
        public void ReponerStockInsumos(BEPedidoPlato pedidoPlato)
        {
            if (pedidoPlato?.Plato?.ListaInsumos == null || !File.Exists("BD.xml"))
                return;

            XDocument xml = XDocument.Load("BD.xml");

            foreach (var platoInsumo in pedidoPlato.Plato.ListaInsumos)
            {
                int idInsumo = platoInsumo.Insumo.Id;
                decimal cantidadReponer = platoInsumo.Cantidad * pedidoPlato.Cantidad;

                // Buscar insumo en XML
                var insumoXml = xml.Root.Element("Insumos")?
                                    .Elements("insumo")
                                    .FirstOrDefault(i => (int)i.Attribute("Id") == idInsumo);

                if (insumoXml != null)
                {
                    decimal cantidadActual = decimal.Parse(insumoXml.Element("Cantidad").Value.Trim());
                    cantidadActual += cantidadReponer;
                    insumoXml.Element("Cantidad").SetValue(cantidadActual.ToString("0.##"));
                }
            }

            xml.Save("BD.xml");
        }
        public void CambiarEstadoPlato(int idPedido, int idPedidoPlato, BEPedidoPlato.EstadoPlato nuevoEstado)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML.");

            BDXML = XDocument.Load(ruta);

            // Buscar el pedido correspondiente
            var pedidoXml = BDXML.Descendants("Pedido")
                                 .FirstOrDefault(p => int.Parse(p.Attribute("Id")?.Value ?? "0") == idPedido);

            if (pedidoXml == null)
                throw new Exception($"No se encontró el pedido con Id {idPedido}.");

            var pedidoPlatos = pedidoXml.Element("PedidoPlatos");
            if (pedidoPlatos == null)
                throw new Exception("No se encontró el nodo PedidoPlatos dentro del pedido.");

            // Buscar el plato original por Id
            var platoOriginal = pedidoPlatos.Elements("PedidoPlato")
                                            .FirstOrDefault(p => int.Parse(p.Attribute("Id")?.Value ?? "0") == idPedidoPlato);

            if (platoOriginal == null)
                throw new Exception($"No se encontró el plato con Id {idPedidoPlato} dentro del pedido {idPedido}.");

            int cantidadOriginal = int.Parse(platoOriginal.Element("Cantidad")?.Value ?? "0");
            int platoId = int.Parse(platoOriginal.Element("PlatoId")?.Value ?? "0");
            string estadoOriginal = platoOriginal.Element("Estado")?.Value ?? "";

            // Si el estado ya es el mismo, no hacer nada
            if (estadoOriginal.Equals(nuevoEstado.ToString(), StringComparison.OrdinalIgnoreCase))
                return;

            // Buscar si ya existe un plato con el mismo PlatoId y el nuevo estado dentro del mismo pedido
            var platoDestino = pedidoPlatos.Elements("PedidoPlato")
                                           .FirstOrDefault(p =>
                                               int.Parse(p.Element("PlatoId")?.Value ?? "0") == platoId &&
                                               string.Equals(p.Element("Estado")?.Value ?? "", nuevoEstado.ToString(), StringComparison.OrdinalIgnoreCase)
                                           );

            if (platoDestino != null)
            {
                // Sumar cantidades al plato existente
                int cantidadDestino = int.Parse(platoDestino.Element("Cantidad")?.Value ?? "0");
                platoDestino.SetElementValue("Cantidad", cantidadDestino + cantidadOriginal);

                // Actualizar subtotal si existe
                if (platoDestino.Element("Subtotal") != null)
                {
                    decimal precioUnitario = decimal.Parse(platoDestino.Element("Subtotal")?.Value ?? "0") / cantidadDestino;
                    platoDestino.SetElementValue("Subtotal", precioUnitario * (cantidadDestino + cantidadOriginal));
                }

                // Eliminar el plato original
                platoOriginal.Remove();
            }
            else
            {
                // Cambiar estado del plato original
                platoOriginal.SetElementValue("Estado", nuevoEstado.ToString());
            }

            BDXML.Save(ruta);
        }
        public void EnviarPlatosACocina(int pedidoId)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML.");

            BDXML = XDocument.Load(ruta);

            // Buscar el pedido correcto
            var pedido = BDXML.Descendants("Pedido")
                              .FirstOrDefault(p => int.Parse(p.Attribute("Id").Value) == pedidoId);

            if (pedido == null)
                throw new Exception($"No se encontró el pedido con ID {pedidoId}.");

            var pedidoPlatosElement = pedido.Element("PedidoPlatos");
            if (pedidoPlatosElement == null)
                throw new Exception("No se encontró el nodo PedidoPlatos dentro del pedido.");

            // Buscar todos los platos confirmados
            var platosConfirmados = pedidoPlatosElement.Elements("PedidoPlato")
                                                       .Where(p => (string)p.Element("Estado") == "Confirmado")
                                                       .ToList();

            foreach (var platoConfirmado in platosConfirmados)
            {
                int platoId = int.Parse(platoConfirmado.Element("PlatoId").Value);
                int cantidadConfirmada = int.Parse(platoConfirmado.Element("Cantidad").Value);

                // Buscar si ya existe un plato pendiente con el mismo ID
                var platoPendiente = pedidoPlatosElement.Elements("PedidoPlato")
                                                       .FirstOrDefault(p =>
                                                           int.Parse(p.Element("PlatoId").Value) == platoId &&
                                                           (string)p.Element("Estado") == "Pendiente");

                if (platoPendiente != null)
                {
                    // ✅ Si existe un plato pendiente igual → acumular cantidad
                    int cantidadExistente = int.Parse(platoPendiente.Element("Cantidad").Value);
                    platoPendiente.SetElementValue("Cantidad", cantidadExistente + cantidadConfirmada);

                    // 🔹 Eliminar el plato confirmado original
                    platoConfirmado.Remove();
                }
                else
                {
                    // ✅ Si no hay uno pendiente, cambiar estado a "Pendiente"
                    platoConfirmado.SetElementValue("Estado", "Pendiente");
                }
            }

            BDXML.Save(ruta);
        }
        public void CambiarEstadoPedido(int idPedido, BEPedido.EstadoPedido nuevoEstado)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var pedido = BDXML.Descendants("pedido")
                              .FirstOrDefault(p => (int)p.Attribute("Id") == idPedido);

            if (pedido == null)
                throw new Exception($"No se encontró el pedido con ID {idPedido}.");

            pedido.SetElementValue("Estado", nuevoEstado.ToString());
            BDXML.Save(ruta);
        }
        public void EnviarPedidoACobranza(int pedidoId)
        {
            if (!CrearXML())
                throw new Exception("No se pudo cargar el XML.");

            BDXML = XDocument.Load(ruta);

            // Buscar el pedido por Id
            var pedidoXml = BDXML.Descendants("Pedido")
                                 .FirstOrDefault(p => int.Parse(p.Attribute("Id").Value) == pedidoId);

            if (pedidoXml == null)
                throw new Exception($"No se encontró el pedido con Id {pedidoId}.");

            // Verificar que todos los platos estén entregados
            var todosEntregados = pedidoXml.Descendants("PedidoPlato")
                                           .All(pl => pl.Element("Estado")?.Value == "Entregado");

            if (!todosEntregados)
                throw new Exception("No se puede mandar a cobranza. Todos los platos deben estar entregados.");

            // Cambiar estado del pedido a Cerrado
            pedidoXml.SetElementValue("Estado", "Cerrado");

            // Cambiar estado de la reserva asociada a Cerrada
            int reservaId = int.Parse(pedidoXml.Element("ReservaId").Value);
            var reservaXml = BDXML.Descendants("reserva")
                                  .FirstOrDefault(r => int.Parse(r.Attribute("Id").Value) == reservaId);

            if (reservaXml != null)
                reservaXml.SetElementValue("Estado", "Cerrada");

            // Guardar XML
            BDXML.Save(ruta);
        }

        //public void EnviarPlatosACocina(int pedidoId)
        //{
        //    if (!CrearXML())
        //        throw new Exception("No se pudo cargar el XML.");

        //    BDXML = XDocument.Load(ruta);

        //    var pedido = BDXML.Descendants("pedido")
        //                      .FirstOrDefault(p => int.Parse(p.Attribute("Id").Value) == pedidoId);

        //    if (pedido == null)
        //        throw new Exception($"No se encontró el pedido con ID {pedidoId}.");

        //    // Buscar todos los platos confirmados y pasarlos a Pendiente
        //    var platosConfirmados = pedido.Element("PedidoPlatos")
        //                                  .Elements("pedidoPlato")
        //                                  .Where(p => (string)p.Element("Estado") == "Confirmado")
        //                                  .ToList();

        //    foreach (var plato in platosConfirmados)
        //    {
        //        plato.Element("Estado").Value = "Pendiente";
        //    }

        //    BDXML.Save(ruta);
        //}
    }
}

