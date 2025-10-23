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
                    // Crear XML desde cero con estructura raíz
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Pedidos"),
                            new XElement("PedidoPlatos")  
                        )
                    );
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    BDXML = XDocument.Load(ruta);

                    // Verificar nodo <Pedidos>
                    XElement pedidos = BDXML.Root.Element("Pedidos");
                    if (pedidos == null)
                    {
                        pedidos = new XElement("Pedidos");
                        BDXML.Root.Add(pedidos);
                    }

                    BDXML.Save(ruta);
                    return true;
                }
            }
            catch (XmlException ex)
            {
                throw new XmlException("Error al procesar el XML de Pedidos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear/verificar el XML de Pedidos: " + ex.Message, ex);
            }
        }

        public void Guardar(BEPedido oBEPedido)
        {
            try
            {
                if (oBEPedido == null)
                    throw new ArgumentNullException(nameof(oBEPedido));

                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);

                // Asegurarse de que existan los nodos
                XElement pedidosElement = BDXML.Root.Element("Pedidos");
                if (pedidosElement == null)
                {
                    pedidosElement = new XElement("Pedidos");
                    BDXML.Root.Add(pedidosElement);
                }

                XElement pedidoPlatosElement = BDXML.Root.Element("PedidoPlatos");
                if (pedidoPlatosElement == null)
                {
                    pedidoPlatosElement = new XElement("PedidoPlatos");
                    BDXML.Root.Add(pedidoPlatosElement);
                }

                // Inicializar lista de platos
                if (oBEPedido.ListaPlatos == null)
                    oBEPedido.ListaPlatos = new List<BEPedidoPlato>();

                // Pedido nuevo o existente
                XElement pedidoNode;
                if (oBEPedido.Id > 0)
                {
                    // Modificar pedido existente
                    pedidoNode = pedidosElement.Elements("pedido")
                        .FirstOrDefault(p => (int)p.Attribute("Id") == oBEPedido.Id);

                    if (pedidoNode != null)
                    {
                        pedidoNode.Element("ClienteId")?.SetValue(oBEPedido.Cliente?.IdCliente ?? 0);
                        pedidoNode.Element("Fecha")?.SetValue(oBEPedido.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                        pedidoNode.Element("Estado")?.SetValue(oBEPedido.Estado.ToString());

                        // Borrar platos existentes
                        var platosExistentes = pedidoPlatosElement.Elements("pedidoPlato")
                            .Where(x => (int)x.Element("PedidoId") == oBEPedido.Id)
                            .ToList();
                        foreach (var pp in platosExistentes)
                            pp.Remove();
                    }
                }
                else
                {
                    // Nuevo pedido → generar Id
                    oBEPedido.Id = ObtenerUltimoId() + 1;
                    pedidoNode = new XElement("pedido",
                        new XAttribute("Id", oBEPedido.Id),
                        new XElement("ReservaId", oBEPedido.Reserva?.Id ?? 0),
                        new XElement("ClienteId", oBEPedido.Cliente?.IdCliente ?? 0),
                        new XElement("Fecha", oBEPedido.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                        new XElement("Estado", oBEPedido.Estado.ToString())
                    );
                    pedidosElement.Add(pedidoNode);
                }

                // Guardar los platos
                foreach (var plato in oBEPedido.ListaPlatos)
                {
                    XElement nuevoPlato = new XElement("pedidoPlato",
                        new XAttribute("Id", GenerarIdPedidoPlato(pedidoPlatosElement)),
                        new XElement("PedidoId", oBEPedido.Id),
                        new XElement("PlatoId", plato.Plato.Id),
                        new XElement("Cantidad", plato.Cantidad),
                        new XElement("Estado", plato.Estado.ToString())
                    );
                    pedidoPlatosElement.Add(nuevoPlato);
                }

                BDXML.Save(ruta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar/modificar el pedido: " + ex.Message, ex);
            }
        }

        private int GenerarIdPedidoPlato(XElement pedidoPlatosElement)
        {
            return pedidoPlatosElement.Elements("pedidoPlato").Any()
                ? pedidoPlatosElement.Elements("pedidoPlato").Max(x => (int)x.Attribute("Id")) + 1
                : 1;
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

                // Tomamos todos los Id de <pedido> dentro de <Pedidos>
                var ids = BDXML.Root.Element("Pedidos")?.Elements("pedido")
                            .Select(p => (int)p.Attribute("Id")) ?? Enumerable.Empty<int>();

                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de pedido: " + ex.Message);
            }
        }

        public bool VerificarExistenciaObjeto(BEPedido oBEPedido)
        {
            try
            {
                if (oBEPedido == null)
                    throw new ArgumentNullException(nameof(oBEPedido), "El objeto BEPedido no puede ser nulo.");

                if (!CrearXML())
                    throw new Exception("No se pudo crear o acceder al archivo XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("No se pudo cargar el archivo XML.");

                // Buscamos por ReservaId dentro de <Pedidos> para no duplicar pedidos para la misma reserva
                var existe = BDXML.Root.Element("Pedidos")?
                                .Elements("pedido")
                                .Any(p => (int)p.Element("ReservaId") == oBEPedido.Reserva?.Id)
                             ?? false;

                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del pedido: " + ex.Message);
            }
        }
    }
}

