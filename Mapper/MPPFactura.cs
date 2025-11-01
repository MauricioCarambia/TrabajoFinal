using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace Mapper
{
    public class MPPFactura
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;
        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    // Crear archivo XML con nodo raíz "Facturas"
                    BDXML = new XDocument(
                        new XElement("Root",
                            new XElement("Facturas")
                        )
                    );
                    BDXML.Save(ruta);
                }
                else
                {
                    // Si el archivo ya existe, se asegura que tenga el nodo "Facturas"
                    BDXML = XDocument.Load(ruta);

                    if (BDXML.Root.Element("Facturas") == null)
                        BDXML.Root.Add(new XElement("Facturas"));

                    BDXML.Save(ruta);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o validar el XML de Facturas: " + ex.Message);
            }
        }

        public void Guardar(BEFactura factura)
        {
            XDocument xml;
            if (!File.Exists(ruta))
            {
                xml = new XDocument(new XElement("Root", new XElement("Facturas")));
                xml.Save(ruta);
            }

            xml = XDocument.Load(ruta);
            XElement root = xml.Element("Root")?.Element("Facturas");
            if (root == null)
            {
                root = new XElement("Facturas");
                xml.Element("Root").Add(root);
            }

            factura.IdFactura = ObtenerUltimoIdFactura() + 1;
            factura.NumeroFactura = ObtenerProximoNumeroFactura();
            factura.SubTotal = factura.DetallePlatos?.Sum(p => p.Plato.PrecioVenta * p.Cantidad) ?? 0;

            var cultura = new CultureInfo("es-AR"); // ✅ formato con coma decimal

            XElement nodoFactura = new XElement("Factura",
                new XAttribute("IdFactura", factura.IdFactura),
                new XElement("NumeroFactura", factura.NumeroFactura ?? "0"),
                new XElement("TipoFactura", "A"),
                new XElement("Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),

                new XElement("Cliente",
                    new XElement("Nombre", factura.Cliente?.Nombre ?? "Sin nombre"),
                    new XElement("DNI", factura.Cliente?.DNI ?? "Sin DNI")
                ),

                // ✅ guardamos con coma decimal
                new XElement("Subtotal", factura.SubTotal.ToString(cultura)),
                new XElement("Descuento", factura.DescuentoAplicado.ToString(cultura)),
                new XElement("Total", factura.Total.ToString(cultura)),

                new XElement("Promocion",
                    new XElement("Id", factura.PromocionAplicada?.Id ?? 0),
                    new XElement("Nombre", factura.PromocionAplicada?.Nombre ?? "Sin promoción")
                ),

                new XElement("Pedido",
                    new XElement("Id", factura.Pedido?.Id ?? 0)
                ),

                new XElement("Detalle",
                    from p in factura.DetallePlatos ?? new List<BEPedidoPlato>()
                    select new XElement("Plato",
                        new XElement("Nombre", p.Plato?.Nombre ?? "Desconocido"),
                        new XElement("Cantidad", p.Cantidad),
                        new XElement("PrecioUnitario", (p.Plato?.PrecioVenta ?? 0).ToString(cultura)), // ✅ coma
                        new XElement("Total", ((p.Plato?.PrecioVenta ?? 0) * p.Cantidad).ToString(cultura)) // ✅ coma
                    )
                )
            );

            root.Add(nodoFactura);
            xml.Save(ruta);
        }

        public string ObtenerProximoNumeroFactura()
        {
            if (!File.Exists(ruta))
            {
                // Si no existe el archivo, primer número
                return "0000001";
            }

            XDocument xml = XDocument.Load(ruta);
            var numeros = xml.Descendants("NumeroFactura")
                             .Select(x => x.Value)
                             .Where(v => !string.IsNullOrWhiteSpace(v))
                             .Select(v =>
                             {
                                 if (int.TryParse(v, out int num)) return num;
                                 return 0;
                             }).ToList();

            int proximo = (numeros.Count > 0 ? numeros.Max() : 0) + 1;

            // Devuelve con formato de 7 dígitos (ejemplo: 0000001)
            return proximo.ToString("D7");
        }
        public List<BEFactura> ListarTodo()
        {
            List<BEFactura> lista = new List<BEFactura>();

            if (!CrearXML())
                return lista;

            var cultura = new CultureInfo("es-AR"); // ✅ leer con coma decimal

            foreach (var f in BDXML.Descendants("Factura"))
            {
                try
                {
                    var clienteElem = f.Element("Cliente");
                    BECliente cliente = new BECliente
                    {
                        Nombre = clienteElem?.Element("Nombre")?.Value ?? "Desconocido",
                        DNI = clienteElem?.Element("DNI")?.Value ?? "Sin DNI"
                    };

                    BEFactura oBE = new BEFactura
                    {
                        IdFactura = int.TryParse(f.Attribute("IdFactura")?.Value, out int idFact) ? idFact : 0,
                        NumeroFactura = f.Element("NumeroFactura")?.Value ?? "0000000",
                        Fecha = DateTime.TryParse(f.Element("Fecha")?.Value, out DateTime fecha) ? fecha : DateTime.MinValue,
                        Cliente = cliente,
                        Total = decimal.TryParse(f.Element("Total")?.Value, NumberStyles.Any, cultura, out decimal total) ? total : 0,
                        DescuentoAplicado = decimal.TryParse(f.Element("Descuento")?.Value, NumberStyles.Any, cultura, out decimal desc) ? desc : 0,
                        PromocionAplicada = new BEPromociones
                        {
                            Nombre = f.Element("Promocion")?.Element("Nombre")?.Value ?? "Sin promoción"
                        }
                    };

                    lista.Add(oBE);
                }
                catch { continue; }
            }

            return lista;
        }


        public int ObtenerUltimoIdFactura()
        {
            try
            {
                // Verificar que exista el XML
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML de facturas!");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo cargar el XML de facturas!");

                // Obtener todos los IdFactura válidos
                var ids = BDXML.Root
                               ?.Element("Facturas")
                               ?.Descendants("Factura")
                               .Select(f =>
                               {
                                   var attr = f.Attribute("IdFactura");
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
        public bool VerificarFacturaExistente(int numeroFactura)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var facturas = BDXML.Root.Element("Facturas")?.Elements("Factura");

            if (facturas == null)
                return false;

            return facturas.Any(f => (int)f.Element("NumeroFactura") == numeroFactura);
        }
        public BEFactura ListarPorPedido(int idPedido)
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML de facturas.");

            XDocument xml = XDocument.Load(ruta);
            XElement root = xml.Element("Root")?.Element("Facturas");
            if (root == null)
                return null;

            XElement nodoFactura = root.Elements("Factura")
                                       .FirstOrDefault(f => (int?)f.Element("Pedido")?.Element("Id") == idPedido);

            if (nodoFactura == null)
                return null;

            var cultura = CultureInfo.GetCultureInfo("es-AR"); // Coma decimal

            // Construir BEFactura
            BEFactura factura = new BEFactura
            {
                IdFactura = (int?)nodoFactura.Attribute("IdFactura") ?? 0,
                NumeroFactura = (string)nodoFactura.Element("NumeroFactura") ?? "0000000",
                Fecha = DateTime.TryParse((string)nodoFactura.Element("Fecha"), out DateTime f) ? f : DateTime.MinValue,
                SubTotal = decimal.TryParse((string)nodoFactura.Element("Subtotal"), NumberStyles.Any, cultura, out decimal sub) ? sub : 0m,
                DescuentoAplicado = decimal.TryParse((string)nodoFactura.Element("Descuento"), NumberStyles.Any, cultura, out decimal desc) ? desc : 0m,
                Total = decimal.TryParse((string)nodoFactura.Element("Total"), NumberStyles.Any, cultura, out decimal tot) ? tot : 0m,
                Cliente = new BECliente
                {
                    Nombre = (string)nodoFactura.Element("Cliente")?.Element("Nombre") ?? "Sin nombre",
                    DNI = (string)nodoFactura.Element("Cliente")?.Element("DNI") ?? "Sin DNI"
                },
                Pedido = new BEPedido
                {
                    Id = (int?)nodoFactura.Element("Pedido")?.Element("Id") ?? 0
                },
                PromocionAplicada = new BEPromociones
                {
                    Nombre = (string)nodoFactura.Element("Promocion")?.Element("Nombre") ?? "Sin promoción"
                },
                DetallePlatos = nodoFactura.Element("Detalle")?
                                    .Elements("Plato")
                                    .Select(p => new BEPedidoPlato
                                    {
                                        Plato = new BEPlato
                                        {
                                            Nombre = (string)p.Element("Nombre") ?? "Desconocido",
                                            PrecioVenta = decimal.TryParse((string)p.Element("PrecioUnitario"), NumberStyles.Any, cultura, out decimal pu) ? pu : 0m
                                        },
                                        Cantidad = int.TryParse((string)p.Element("Cantidad"), out int c) ? c : 0
                                    }).ToList() ?? new List<BEPedidoPlato>()
            };

            return factura;
        }

        public BEFactura ListarObjetoPorId(int idFactura)
        {
            try
            {
                if (!CrearXML())
                    return null;

                XDocument xml = XDocument.Load(ruta);
                XElement root = xml.Element("Root")?.Element("Facturas");

                if (root == null)
                    return null;

                var f = root.Elements("Factura")
                            .FirstOrDefault(x => (int?)x.Attribute("IdFactura") == idFactura);

                if (f == null)
                    return null;

                BEFactura factura = new BEFactura
                {
                    IdFactura = (int?)f.Attribute("IdFactura") ?? 0,
                    NumeroFactura = (string)f.Element("NumeroFactura") ?? "Sin numero",
                    Fecha = DateTime.TryParse((string)f.Element("Fecha"), out DateTime fecha) ? fecha : DateTime.MinValue,
                    SubTotal = decimal.TryParse((string)f.Element("Subtotal"), NumberStyles.Any, new CultureInfo("es-AR"), out decimal sub) ? sub : 0m,
                    DescuentoAplicado = decimal.TryParse((string)f.Element("Descuento"), NumberStyles.Any, new CultureInfo("es-AR"), out decimal desc) ? desc : 0m,
                    Total = decimal.TryParse((string)f.Element("Total"), NumberStyles.Any, new CultureInfo("es-AR"), out decimal tot) ? tot : 0m,
                    PromocionAplicada = new BEPromociones
                    {
                        Nombre = (string)f.Element("Promocion")?.Element("Nombre") ?? "Sin promoción"
                    },
                    Pedido = new BEPedido
                    {
                        Id = (int?)f.Element("Pedido")?.Element("Id") ?? 0
                    },
                    Cliente = new BECliente
                    {
                        Nombre = (string)f.Element("Cliente")?.Element("Nombre") ?? "Desconocido",
                        DNI = (string)f.Element("Cliente")?.Element("DNI") ?? "Sin DNI"
                    }
                };

                // Opcional: cargar detalle de platos si existe
                var detalle = f.Element("Detalle");
                if (detalle != null)
                {
                    factura.DetallePlatos = new List<BEPedidoPlato>();
                    foreach (var p in detalle.Elements("Plato"))
                    {
                        factura.DetallePlatos.Add(new BEPedidoPlato
                        {
                            Plato = new BEPlato
                            {
                                Nombre = (string)p.Element("Nombre") ?? "Sin nombre",
                                PrecioVenta = decimal.TryParse((string)p.Element("PrecioUnitario"), NumberStyles.Any, new CultureInfo("es-AR"), out decimal pu) ? pu : 0m
                            },
                            Cantidad = int.TryParse((string)p.Element("Cantidad"), NumberStyles.Any, new CultureInfo("es-AR"), out int cant) ? cant : 0
                        });
                    }
                }

                return factura;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la factura: " + ex.Message);
            }
        }


        public void GenerarFacturaPDF(BEFactura factura)
        {
            try
            {
                string nroFactura = factura.NumeroFactura;
                string nombreArchivo = $"Factura_{nroFactura}.pdf";
                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

                iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();
               

                doc.Add(new Paragraph($"Factura N° {nroFactura}") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph(" "));

                // Datos del cliente
                doc.Add(new Paragraph($"Cliente: {factura.Cliente.Nombre}"));
                doc.Add(new Paragraph($"DNI: {factura.Cliente.DNI}"));
                doc.Add(new Paragraph($"Teléfono: {factura.Cliente.Telefono ?? "Sin Teléfono"}"));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Fecha: {factura.Fecha:dd/MM/yyyy HH:mm}"));
                doc.Add(new Paragraph(" "));

                // Tabla detalle
                PdfPTable tabla = new PdfPTable(4) { WidthPercentage = 100 };
                tabla.AddCell("Plato");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Precio Unitario");
                tabla.AddCell("Subtotal");

                var cultura = new CultureInfo("es-AR"); // ✅ coma decimal

                foreach (var p in factura.DetallePlatos)
                {
                    tabla.AddCell(p.Plato.Nombre);
                    tabla.AddCell(p.Cantidad.ToString());
                    tabla.AddCell(p.Plato.PrecioVenta.ToString("N2", cultura));
                    tabla.AddCell((p.Plato.PrecioVenta * p.Cantidad).ToString("N2", cultura));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));

                doc.Add(new Paragraph($"Subtotal: ${factura.SubTotal.ToString("N2", cultura)}"));
                if (factura.Total < factura.SubTotal)
                    doc.Add(new Paragraph($"Descuento aplicado: ${(factura.SubTotal - factura.Total).ToString("N2", cultura)}"));
                doc.Add(new Paragraph($"Total a cobrar: ${factura.Total.ToString("N2", cultura)}"));

                doc.Close();

               
            }
            catch (Exception ex)
            {
                { throw ex; }
            }
        }
    }
}
