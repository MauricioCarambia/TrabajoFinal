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
                            new XElement("Pedidos")
                        )
                    );
                    BDXML.Save(ruta);
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML.Root.Element("Pedidos") == null)
                    {
                        BDXML.Root.Add(new XElement("Pedidos"));
                        BDXML.Save(ruta);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear/verificar el XML: " + ex.Message, ex);
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

        private void SetOrCreateElementValue(XElement parent, string elementName, string value)
        {
            var el = parent.Element(elementName);
            if (el == null) parent.Add(new XElement(elementName, value));
            else el.SetValue(value);
        }

        public void Guardar(BEPedido oBEPedido)
        {
            try
            {
                if (oBEPedido == null) throw new ArgumentNullException(nameof(oBEPedido));

                CrearXML();
                BDXML = XDocument.Load(ruta);

                XElement pedidosElement = BDXML.Root.Element("Pedidos");
                if (pedidosElement == null)
                {
                    pedidosElement = new XElement("Pedidos");
                    BDXML.Root.Add(pedidosElement);
                }

                if (oBEPedido.ListaPlatos == null)
                    oBEPedido.ListaPlatos = new List<BEPedidoPlato>();

                // Buscar pedido existente
                XElement pedidoExistente = pedidosElement.Elements("pedido")
                    .FirstOrDefault(p =>
                        ((int?)p.Attribute("Id") ?? 0) == oBEPedido.Id ||
                        (oBEPedido.Reserva != null && ((int?)p.Element("ReservaId") ?? 0) == oBEPedido.Reserva.Id)
                    );

                if (pedidoExistente == null)
                {
                    // Nuevo pedido
                    oBEPedido.Id = ObtenerUltimoId() + 1;
                    pedidoExistente = new XElement("pedido",
                        new XAttribute("Id", oBEPedido.Id),
                        new XElement("ReservaId", oBEPedido.Reserva?.Id ?? 0),
                        new XElement("ClienteId", oBEPedido.Cliente?.IdCliente ?? 0),
                        new XElement("Fecha", oBEPedido.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                        new XElement("Estado", oBEPedido.Estado.ToString())
                    );
                    pedidosElement.Add(pedidoExistente);
                }
                else
                {
                    // Actualizar existente
                    SetOrCreateElementValue(pedidoExistente, "ReservaId", (oBEPedido.Reserva?.Id ?? 0).ToString());
                    SetOrCreateElementValue(pedidoExistente, "ClienteId", (oBEPedido.Cliente?.IdCliente ?? 0).ToString());
                    SetOrCreateElementValue(pedidoExistente, "Fecha", oBEPedido.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                    SetOrCreateElementValue(pedidoExistente, "Estado", oBEPedido.Estado.ToString());
                    pedidoExistente.Element("PedidoPlatos")?.Remove();
                }

                // Crear nodo de platos
                XElement pedidoPlatosElement = new XElement("PedidoPlatos");
                int nextPedidoPlatoId = 1;
                var allIds = BDXML.Descendants("pedidoPlato").Attributes("Id")
                                  .Select(a => int.TryParse(a.Value, out int v) ? v : 0);
                if (allIds.Any()) nextPedidoPlatoId = allIds.Max() + 1;

                foreach (var plato in oBEPedido.ListaPlatos)
                {
                    XElement nuevoPlato = new XElement("pedidoPlato",
                        new XAttribute("Id", nextPedidoPlatoId++),
                        new XElement("PlatoId", plato.Plato?.Id ?? 0),
                        new XElement("Cantidad", plato.Cantidad),
                        new XElement("Estado", plato.Estado.ToString())
                    );
                    pedidoPlatosElement.Add(nuevoPlato);
                }

                pedidoExistente.Add(pedidoPlatosElement);
                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el pedido: " + ex.Message, ex);
            }
        }

        public BEPedido ListarObjetoPorId(BEPedido oBEPedido)
        {
            if (!CrearXML()) return null;

            BDXML = XDocument.Load(ruta);

            var pedidoXml = BDXML.Root.Element("Pedidos")?
                .Elements("pedido")
                .FirstOrDefault(p => (int)p.Attribute("Id") == oBEPedido.Id);

            if (pedidoXml == null) return null;

            BEPedido pedido = new BEPedido
            {
                Id = (int)pedidoXml.Attribute("Id"),
                Fecha = DateTime.Parse(pedidoXml.Element("Fecha").Value.Trim()),
                Estado = (BEPedido.EstadoPedido)Enum.Parse(typeof(BEPedido.EstadoPedido), pedidoXml.Element("Estado").Value.Trim()),
                Cliente = new BECliente { IdCliente = int.Parse(pedidoXml.Element("ClienteId").Value.Trim()) },
                Reserva = new BEReserva { Id = int.Parse(pedidoXml.Element("ReservaId").Value.Trim()) },
                ListaPlatos = new List<BEPedidoPlato>()
            };

            var platosXml = pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato");

            if (platosXml != null)
            {
                foreach (var pp in platosXml)
                {
                    MPPPlato mapperPlato = new MPPPlato();
                    int platoId = int.Parse(pp.Element("PlatoId").Value.Trim());
                    BEPlato plato = mapperPlato.ListarObjetoPorId(new BEPlato { Id = platoId }); // suponiendo que tienes MPPPlato

                    BEPedidoPlato pedidoPlato = new BEPedidoPlato
                    {
                        Plato = plato,
                        Cantidad = int.Parse(pp.Element("Cantidad").Value.Trim()),
                        Estado = (BEPedidoPlato.EstadoPlato)Enum.Parse(typeof(BEPedidoPlato.EstadoPlato), pp.Element("Estado").Value.Trim())
                    };
                    pedido.ListaPlatos.Add(pedidoPlato);
                }
            }

            return pedido;
        }
        public List<BEPedido> ListarTodo()
        {
            try
            {
                if (!File.Exists(ruta)) return new List<BEPedido>();

                BDXML = XDocument.Load(ruta);
                var pedidosXml = BDXML.Root.Element("Pedidos")?.Elements("pedido");

                if (pedidosXml == null) return new List<BEPedido>();

                List<BEPedido> listaPedidos = new List<BEPedido>();

                foreach (var pedidoXml in pedidosXml)
                {
                    int reservaId = int.TryParse(pedidoXml.Element("ReservaId")?.Value, out int rid) ? rid : 0;
                    int clienteId = int.TryParse(pedidoXml.Element("ClienteId")?.Value, out int cid) ? cid : 0;
                    DateTime fecha = DateTime.TryParse(pedidoXml.Element("Fecha")?.Value, out DateTime f) ? f : DateTime.Now;

                    // Crear objetos Reserva y Cliente (si tenés mappers de BLL, los usás)
                    BEReserva reserva = new BEReserva
                    {
                        Id = reservaId,
                        Cliente = new BECliente { IdCliente = clienteId },
                        // Opcional: llenar Mesa, CantidadPersonas, etc., si tuvieras info
                    };

                    BEPedido pedido = new BEPedido
                    {
                        Id = int.TryParse(pedidoXml.Attribute("Id")?.Value, out int pid) ? pid : 0,
                        Reserva = reserva,
                        Cliente = reserva.Cliente,
                        Fecha = fecha,
                        Estado = Enum.TryParse(pedidoXml.Element("Estado")?.Value, out BEPedido.EstadoPedido estado)
                                    ? estado
                                    : BEPedido.EstadoPedido.Abierto,
                        ListaPlatos = new List<BEPedidoPlato>()
                    };

                    // Leer los platos
                    var platosXml = pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato");
                    if (platosXml != null)
                    {
                        MPPPlato mapperPlato = new MPPPlato();
                        foreach (var pp in platosXml)
                        {
                            BEPlato plato = mapperPlato.ListarObjetoPorId(new BEPlato { Id = int.Parse(pp.Element("PlatoId")?.Value ?? "0") });

                            BEPedidoPlato pedidoPlato = new BEPedidoPlato
                            {
                                Plato = plato,
                                Cantidad = int.TryParse(pp.Element("Cantidad")?.Value, out int cant) ? cant : 0,
                                Estado = Enum.TryParse(pp.Element("Estado")?.Value, out BEPedidoPlato.EstadoPlato ep) ? ep : BEPedidoPlato.EstadoPlato.Pendiente
                            };
                            pedido.ListaPlatos.Add(pedidoPlato);
                        }
                    }

                    listaPedidos.Add(pedido);
                }

                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pedidos: " + ex.Message, ex);
            }
        }

        public BEPedido ListarPorReserva(int reservaId)
        {
            if (!File.Exists(ruta))
                return null;

            XDocument xml = XDocument.Load(ruta);

            var pedidoXml = xml.Root.Elements("pedido")
                .FirstOrDefault(p => int.Parse(p.Element("ReservaId")?.Value ?? "0") == reservaId);

            if (pedidoXml == null)
                return null;

            // Crear objeto pedido
            BEPedido pedido = new BEPedido
            {
                Id = int.Parse(pedidoXml.Attribute("Id")?.Value ?? "0"),
                Fecha = DateTime.Parse(pedidoXml.Element("Fecha")?.Value ?? DateTime.MinValue.ToString()),
                Estado = Enum.TryParse(pedidoXml.Element("Estado")?.Value, out BEPedido.EstadoPedido estado) ? estado : BEPedido.EstadoPedido.Abierto,
                Reserva = oBLLReserva.ListarObjetoPorId(new BEReserva { Id = reservaId }),
                Cliente = oBLLCliente.ListarObjetoPorId(new BECliente { IdCliente = int.Parse(pedidoXml.Element("ClienteId")?.Value ?? "0") }),
                ListaPlatos = new List<BEPedidoPlato>()
            };

            // Cargar platos
            foreach (var pp in pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato") ?? Enumerable.Empty<XElement>())
            {
                int platoId = int.Parse(pp.Element("PlatoId")?.Value ?? "0");
                BEPlato plato = oBLLPlato.ListarObjetoPorId(new BEPlato { Id = platoId });

                pedido.ListaPlatos.Add(new BEPedidoPlato
                {
                    Plato = plato,
                    Cantidad = int.TryParse(pp.Element("Cantidad")?.Value, out int cant) ? cant : 0,
                    Estado = Enum.TryParse(pp.Element("Estado")?.Value, out BEPedidoPlato.EstadoPlato ep) ? ep : BEPedidoPlato.EstadoPlato.Pendiente
                });
            }

            return pedido;
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
                if (pedidoPlato.Estado != BEPedidoPlato.EstadoPlato.Pendiente)
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

    }
}

