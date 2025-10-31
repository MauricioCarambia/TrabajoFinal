using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var cobros = BDXML.Root.Element("Cobros")?.Elements("Cobro");

            if (cobros != null && cobros.Any())
                return cobros.Max(c => (int)c.Attribute("Id")) + 1;

            return 1; // Si no hay cobros, el primer Id será 1
        }

        public void Guardar(BECobro cobro)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            XDocument xml = XDocument.Load(ruta);
            XElement root = xml.Element("Root").Element("Cobros");
            if (root == null)
            {
                root = new XElement("Cobros");
                xml.Element("Root").Add(root);
            }

            // Obtener el último ID de cobro
            int nuevoId = ObtenerUltimoIdCobro() + 1;

            // Asegurarse de que no exista un cobro con ese ID
            while (ExisteCobroPorId(nuevoId))
            {
                nuevoId++;
            }

            cobro.IdCobro = nuevoId;

            XElement nodoCobro = new XElement("Cobro",
                new XAttribute("IdCobro", cobro.IdCobro),
                new XElement("Fecha", cobro.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("PedidoId", cobro.Pedido.Id),
                new XElement("FacturaId", cobro.Factura.IdFactura),
                new XElement("Promocion", cobro.Promocion?.Nombre ?? "Sin promoción"),
                new XElement("Total", cobro.Total)
            );

            root.Add(nodoCobro);
            xml.Save(ruta);
        }

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
                    return lista; // Si no hay cobros, devuelve lista vacía

                foreach (var c in root.Elements("Cobro"))
                {
                    try
                    {
                        // Lectura segura de atributos y elementos
                        int idCobro = (int?)c.Attribute("IdCobro") ?? 0;
                        DateTime fecha = DateTime.TryParse((string)c.Element("Fecha"), out DateTime f) ? f : DateTime.MinValue;
                        decimal total = decimal.TryParse((string)c.Element("Total"), out decimal t) ? t : 0m;

                        int idFactura = (int?)c.Element("FacturaId") ?? 0;
                        int idPedido = (int?)c.Element("PedidoId") ?? 0;
                        string promoNombre = (string)c.Element("Promocion") ?? "Sin promoción";

                        // Construcción del objeto
                        BECobro cobro = new BECobro
                        {
                            IdCobro = idCobro,
                            Fecha = fecha,
                            Total = total,
                            Factura = new BEFactura { IdFactura = idFactura },
                            Pedido = new BEPedido { Id = idPedido },
                            Promocion = new BEPromociones { Nombre = promoNombre }
                        };

                        lista.Add(cobro);
                    }
                    catch (Exception ex)
                    {
                        // Si un nodo falla, se omite pero se continúa con los demás
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

        public bool ExisteCobroPorId(int idCobro)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var cobros = BDXML.Root.Element("Cobros")?.Elements("Cobro");

            if (cobros == null)
                return false;

            return cobros.Any(c => (int)c.Attribute("Id") == idCobro);
        }
    }
}
    
