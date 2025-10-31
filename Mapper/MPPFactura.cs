using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            // Cargar o crear el XML
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

             // ← agregá esta prop. string o simplemente
            // si querés mantener int también

            // Asignar Id correlativo
            factura.IdFactura = ObtenerUltimoIdFactura() + 1;
            factura.NumeroFactura = ObtenerProximoNumeroFactura();

            // Calcular subtotal si no viene
            factura.SubTotal = factura.DetallePlatos?.Sum(p => p.Plato.PrecioVenta * p.Cantidad) ?? 0;

            // Crear el nodo de factura
            XElement nodoFactura = new XElement("Factura",
                new XAttribute("IdFactura", factura.IdFactura),
                new XElement("NumeroFactura", factura.NumeroFactura),
                new XElement("TipoFactura", "A"), // siempre tipo A
                new XElement("Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),

               new XElement("Cliente",
                    new XElement("Nombre", factura.Cliente?.Nombre ?? "Sin nombre"),
                    new XElement("DNI", factura.Cliente?.DNI ?? "Sin DNI")
                ),

                new XElement("Subtotal", factura.SubTotal),
                new XElement("Descuento", factura.DescuentoAplicado),
                new XElement("Total", factura.Total),

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
                        new XElement("PrecioUnitario", p.Plato?.PrecioVenta ?? 0),
                        new XElement("Total", (p.Plato?.PrecioVenta ?? 0) * p.Cantidad)
                    )
                )
            );

            // Guardar en XML
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

            foreach (var f in BDXML.Descendants("Factura"))
            {
                try
                {
                    // 🧾 Cliente completo
                    var clienteElem = f.Element("Cliente");
                    BECliente cliente = new BECliente
                    {
                        Id = int.TryParse(clienteElem?.Attribute("Id")?.Value, out int idCliente) ? idCliente : 0,
                        Nombre = clienteElem?.Element("Nombre")?.Value ?? "Desconocido",
                        DNI = clienteElem?.Element("DNI")?.Value ?? "Sin DNI",
                        Telefono = clienteElem?.Element("Telefono")?.Value ?? "Sin Teléfono"
                    };

                    // 🧩 Pedido
                    BEPedido pedido = new BEPedido();
                    var pedidoElem = f.Element("Pedido");
                    if (pedidoElem != null)
                    {
                        int.TryParse(pedidoElem.Attribute("Id")?.Value, out int idPedido);
                        pedido.Id = idPedido;
                    }

                    // 🧮 Crear objeto factura
                    BEFactura oBE = new BEFactura
                    {
                        IdFactura = int.TryParse(f.Attribute("IdFactura")?.Value, out int idFact) ? idFact : 0,
                        NumeroFactura = f.Element("NumeroFactura")?.Value ?? "0000000",
                        Fecha = DateTime.TryParse(f.Element("Fecha")?.Value, out DateTime fecha) ? fecha : DateTime.MinValue,
                        Cliente = cliente,          // ✅ Cliente completo
                        Total = decimal.TryParse(f.Element("Total")?.Value, out decimal total) ? total : 0,
                        DescuentoAplicado = decimal.TryParse(f.Element("Descuento")?.Value, out decimal desc) ? desc : 0,
                        PromocionAplicada = new BEPromociones
                        {
                            Nombre = f.Element("Promocion")?.Value ?? "Sin promoción"
                        },
                        Pedido = pedido
                    };

                    lista.Add(oBE);
                }
                catch
                {
                    // Ignorar factura con errores y continuar
                    continue;
                }
            }

            return lista;
        }


        public int ObtenerUltimoIdFactura()
        {
            if (!CrearXML())
                throw new Exception("No se pudo crear o cargar el XML.");

            BDXML = XDocument.Load(ruta);

            var facturas = BDXML.Root.Element("Facturas")?.Elements("Factura");

            if (facturas != null && facturas.Any())
                return facturas.Max(f => (int)f.Attribute("Id")) + 1;

            return 1; // Si no hay facturas, el primer Id será 1
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

        //private void GenerarFacturaPDF(BEFactura factura)
        //{
        //    try
        //    {
        //        string nombreArchivo = $"Factura_{factura.NumeroFactura}.pdf";
        //        string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

        //        Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
        //        PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
        //        doc.Open();

        //        // ENCABEZADO
        //        var titulo = new Paragraph($"Factura {factura.TipoFactura} N° {factura.NumeroFactura}")
        //        { Alignment = Element.ALIGN_CENTER };
        //        doc.Add(titulo);
        //        doc.Add(new Paragraph(" "));
        //        doc.Add(new Paragraph($"Fecha: {factura.Fecha:dd/MM/yyyy HH:mm}"));
        //        doc.Add(new Paragraph($"Cliente: {factura.Cliente.Nombre}"));
        //        doc.Add(new Paragraph($"DNI: {factura.Cliente.DNI}"));
        //        doc.Add(new Paragraph(" "));

        //        // TABLA DETALLE
        //        PdfPTable tabla = new PdfPTable(4) { WidthPercentage = 100 };
        //        tabla.AddCell("Plato");
        //        tabla.AddCell("Cantidad");
        //        tabla.AddCell("Precio Unitario");
        //        tabla.AddCell("Subtotal");

        //        foreach (var item in factura.DetallePlatos)
        //        {
        //            tabla.AddCell(item.Plato.Nombre);
        //            tabla.AddCell(item.Cantidad.ToString());
        //            tabla.AddCell(item.Plato.PrecioVenta.ToString("0.00"));
        //            tabla.AddCell((item.Plato.PrecioVenta * item.Cantidad).ToString("0.00"));
        //        }

        //        doc.Add(tabla);
        //        doc.Add(new Paragraph(" "));

        //        // TOTALES
        //        doc.Add(new Paragraph($"Subtotal: ${factura.Subtotal:0.00}"));
        //        if (factura.Descuento > 0)
        //            doc.Add(new Paragraph($"Descuento aplicado: ${factura.Descuento:0.00}"));
        //        doc.Add(new Paragraph($"Total a cobrar: ${factura.Total:0.00}"));

        //        doc.Close();

        //        MessageBox.Show($"Factura generada correctamente:\n{ruta}", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error generando factura PDF: " + ex.Message);
        //    }
        //}
    }
}
