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
    public class MPPCobro
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;
        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    // Crear archivo XML con nodo raíz "Cobros"
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Cobros")
                        )
                    );
                    BDXML.Save(ruta);
                }
                else
                {
                    // Si el archivo existe, validar que tenga el nodo "Cobros"
                    BDXML = XDocument.Load(ruta);

                    if (BDXML.Root.Element("Cobros") == null)
                        BDXML.Root.Add(new XElement("Cobros"));

                    BDXML.Save(ruta);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o validar el XML de Cobros: " + ex.Message);
            }
        }

        public int ObtenerUltimoIdCobro()
        {
            try
            {
                // Verificar que exista el XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML de cobros!");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo cargar el XML de cobros!");

                // Obtener todos los Id válidos de cobros
                var ids = BDXML.Root
                               ?.Element("Cobros")
                               ?.Descendants("Cobro")
                               .Select(c =>
                               {
                                   var attr = c.Attribute("IdCobro");
                                   return attr != null && int.TryParse(attr.Value.Trim(), out int val) ? (int?)val : null;
                               })
                               .Where(id => id.HasValue)
                               .Select(id => id.Value)
                               .ToList();

                // Si no hay IDs válidos, devolver 0
                return ids != null && ids.Any() ? ids.Max() : 0;
            }
            catch
            {
                // Relanzar la excepción manteniendo el stack trace original
                throw;
            }
        }

        public void Guardar(BECobro cobro)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            XDocument xml = XDocument.Load(ruta);
            XElement root = xml.Element("Root")?.Element("Cobros");

            if (root == null)
            {
                XElement rootGlobal = xml.Element("Root");
                if (rootGlobal == null)
                {
                    rootGlobal = new XElement("Root");
                    xml.Add(rootGlobal);
                }

                root = new XElement("Cobros");
                rootGlobal.Add(root);
            }

            // 🔹 Generar nuevo IdCobro
            int nuevoId = ObtenerUltimoIdCobro() + 1;
            while (ExisteCobroPorId(nuevoId))
                nuevoId++;

            cobro.IdCobro = nuevoId;

            // 🔹 Crear nodo XML del cobro (sin Usuario ni Cliente)
            XElement nodoCobro = new XElement("Cobro",
                new XAttribute("IdCobro", cobro.IdCobro),
                new XElement("Fecha", cobro.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("PedidoId", cobro.Pedido?.Id ?? 0),
                new XElement("FacturaId", cobro.Factura?.IdFactura ?? 0),
                new XElement("MetodoPago", cobro.MetodoPago ?? "Sin especificar"),
                new XElement("Promocion", cobro.Promocion?.Nombre ?? "Sin promoción"),

                // ✅ Guardar total con punto decimal (invariant culture)
                new XElement("Total", cobro.Total.ToString("F2", new CultureInfo("es-AR")))
            );

            root.Add(nodoCobro);
            xml.Save(ruta);
        }

        // ============================================================
        // 🔹 LISTAR TODOS LOS COBROS
        // ============================================================
        public List<BECobro> ListarTodo()
        {
            List<BECobro> lista = new List<BECobro>();

            try
            {
                if (!CrearXML())
                    return lista;

                XDocument xml = XDocument.Load(ruta);
                XElement root = xml.Element("Root")?.Element("Cobros");

                if (root == null)
                    return lista;

                foreach (var c in root.Elements("Cobro"))
                {
                    try
                    {
                        int idCobro = (int?)c.Attribute("IdCobro") ?? 0;
                        DateTime fecha = DateTime.TryParse((string)c.Element("Fecha"), out DateTime f) ? f : DateTime.MinValue;

                        // ✅ Leer total con coma decimal (formato argentino)
                        decimal total = 0m;
                        decimal.TryParse((string)c.Element("Total"), NumberStyles.Any, new CultureInfo("es-AR"), out total);

                        int idFactura = (int?)c.Element("FacturaId") ?? 0;
                        int idPedido = (int?)c.Element("PedidoId") ?? 0;

                        string metodoPago = (string)c.Element("MetodoPago") ?? "Sin especificar";
                        string promoNombre = (string)c.Element("Promocion") ?? "Sin promoción";

                        // 🔹 Construir el objeto completo
                        BECobro cobro = new BECobro
                        {
                            IdCobro = idCobro,
                            Fecha = fecha,
                            Total = total,
                            MetodoPago = metodoPago,
                            Factura = new BEFactura { IdFactura = idFactura },
                            Pedido = new BEPedido { Id = idPedido },
                            Promocion = new BEPromociones { Nombre = promoNombre }
                        };

                        lista.Add(cobro);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al leer cobro: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cobros: " + ex.Message);
            }

            return lista;
        }

        // ============================================================
        // 🔹 LISTAR COBRO POR ID
        // ============================================================
        public BECobro ListarObjetoPorId(int idCobro)
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("No se pudo crear o cargar el XML.");

                XDocument xml = XDocument.Load(ruta);
                XElement root = xml.Element("Root")?.Element("Cobros");

                if (root == null)
                    return null;

                var c = root.Elements("Cobro")
                            .FirstOrDefault(x => (int?)x.Attribute("IdCobro") == idCobro);

                if (c == null)
                    return null;

                // ✅ Parseo correcto con coma decimal
                decimal total = 0m;
                decimal.TryParse((string)c.Element("Total"), NumberStyles.Any, new CultureInfo("es-AR"), out total);

                BECobro cobro = new BECobro
                {
                    IdCobro = (int?)c.Attribute("IdCobro") ?? 0,
                    Fecha = DateTime.TryParse((string)c.Element("Fecha"), out DateTime f) ? f : DateTime.MinValue,
                    Total = total,
                    Factura = new BEFactura { IdFactura = (int?)c.Element("FacturaId") ?? 0 },
                    Pedido = new BEPedido { Id = (int?)c.Element("PedidoId") ?? 0 },
                    MetodoPago = (string)c.Element("MetodoPago") ?? "Sin especificar",
                    Promocion = new BEPromociones { Nombre = (string)c.Element("Promocion") ?? "Sin promoción" }
                };

                return cobro;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cobro: " + ex.Message);
            }
        }

        public bool ExisteCobroPorId(int idCobro)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var cobros = BDXML.Root.Element("Cobros")?.Elements("Cobro");

            if (cobros == null)
                return false;

            return cobros.Any(c => (int)c.Attribute("IdCobro") == idCobro);
        }
        public List<int> ListarPedidosCobrados()
        {
            var cobros = ListarTodo();
            return cobros.Where(c => c.Pedido != null)
                         .Select(c => c.Pedido.Id)
                         .Distinct()
                         .ToList();
        }
    }
}
    
