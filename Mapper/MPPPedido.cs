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

        public BEPedido ListarPorReserva(int reservaId)
        {
            if (!CrearXML()) return null;

            BDXML = XDocument.Load(ruta);

            // Buscar el pedido por ReservaId
            var pedidoXml = BDXML.Root.Element("Pedidos")?
                .Elements("pedido")
                .FirstOrDefault(p => (int)p.Element("ReservaId") == reservaId);

            if (pedidoXml == null) return null;

            BEPedido pedido = new BEPedido
            {
                Id = (int)pedidoXml.Attribute("Id"),
                Fecha = DateTime.Parse(pedidoXml.Element("Fecha").Value.Trim()),
                Estado = (BEPedido.EstadoPedido)Enum.Parse(typeof(BEPedido.EstadoPedido), pedidoXml.Element("Estado").Value.Trim()),
                Cliente = new BECliente
                {
                    IdCliente = int.Parse(pedidoXml.Element("ClienteId").Value.Trim())
                    // Nombre si lo querés, cargarlo desde BD Cliente
                },
                Reserva = new BEReserva
                {
                    Id = int.Parse(pedidoXml.Element("ReservaId").Value.Trim())
                    // NumeroReserva si lo querés, cargarlo desde BD Reserva
                },
                ListaPlatos = new List<BEPedidoPlato>()
            };

            var platosXml = pedidoXml.Element("PedidoPlatos")?.Elements("pedidoPlato");
            if (platosXml != null)
            {
                MPPPlato mapperPlato = new MPPPlato();
                foreach (var ppXml in platosXml)
                {
                    int platoId = int.Parse(ppXml.Element("PlatoId").Value.Trim());
                    BEPlato plato = mapperPlato.ListarObjetoPorId(new BEPlato { Id = platoId });

                    BEPedidoPlato pedidoPlato = new BEPedidoPlato
                    {
                        Plato = plato,
                        Cantidad = int.Parse(ppXml.Element("Cantidad").Value.Trim()),
                        Estado = (BEPedidoPlato.EstadoPlato)Enum.Parse(typeof(BEPedidoPlato.EstadoPlato), ppXml.Element("Estado").Value.Trim())
                    };
                    pedido.ListaPlatos.Add(pedidoPlato);
                }
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
        public void DescontarStockInsumos(BEPedido pedido)
        {
            if (pedido == null || pedido.ListaPlatos == null) return;
            if (!File.Exists("BD.xml")) throw new Exception("No se encontró el archivo XML de insumos.");

            XDocument xml = XDocument.Load("BD.xml");

            foreach (var pedidoPlato in pedido.ListaPlatos)
            {
                if (pedidoPlato.Plato?.ListaInsumos == null) continue;

                foreach (var platoInsumo in pedidoPlato.Plato.ListaInsumos)
                {
                    int idInsumo = platoInsumo.Insumo.Id;
                    decimal cantidadUsada = platoInsumo.Cantidad * pedidoPlato.Cantidad;

                    // Buscar insumo en XML
                    var insumoXml = xml.Root.Element("Insumos")?
                                        .Elements("insumo")
                                        .FirstOrDefault(i => (int)i.Attribute("Id") == idInsumo);

                    if (insumoXml != null)
                    {
                        decimal cantidadActual = decimal.Parse(insumoXml.Element("Cantidad").Value.Trim());
                        cantidadActual -= cantidadUsada;

                        if (cantidadActual < 0) cantidadActual = 0; // opcional: no permitir negativo
                        insumoXml.Element("Cantidad").SetValue(cantidadActual.ToString("0.##"));
                    }
                }
            }

            xml.Save("BD.xml");
        }
    }
}

