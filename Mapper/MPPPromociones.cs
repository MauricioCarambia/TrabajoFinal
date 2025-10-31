using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Mapper
{
    public class MPPPromociones
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        public bool CrearXML()
        {
            try
            {
                //Si no existe el archivo XML lo crea:
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Promociones")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //Si Existe el XML Verifico que exista el Elemento Proveedores
                    BDXML = XDocument.Load(ruta);
                    XElement promocion = BDXML.Root.Element("Promociones");
                    if (promocion != null) { return true; }
                    //Si no Existe el Elemento Proovedores pero si el XML creo el Elemento
                    else
                    {
                        promocion = new XElement("Promociones");
                        BDXML.Root.Add(promocion);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Eliminar(BEPromociones oBEPromociones)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML.Root == null)
                    throw new XmlException("Error: El XML no tiene raíz.");

                var nodoPromociones = BDXML.Root.Element("Promociones");
                if (nodoPromociones == null)
                    return false;

                // Buscar la promoción por Id
                var promoEliminar = nodoPromociones.Descendants("promocion")
                    .FirstOrDefault(p =>
                        p.Attribute("Id") != null &&
                        int.TryParse(p.Attribute("Id").Value, out int pid) &&
                        pid == oBEPromociones.Id);

                if (promoEliminar != null)
                {
                    promoEliminar.Remove();
                    BDXML.Save(ruta);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la promoción del XML.", ex);
            }
        }

        public void Guardar(BEPromociones oBEPromociones)
        {
            try
            {
                // Verifico que el XML esté creado
                if (CrearXML() == true)
                {
                    // Cargo el XML existente
                    BDXML = XDocument.Load(ruta);

                    if (BDXML != null)
                    {
                        // Verifico que el objeto Promoción no sea nulo
                        if (oBEPromociones != null)
                        {
                            // Si es una promoción nueva (sin Id)
                            if (oBEPromociones.Id == 0)
                            {
                                // Verifico si ya existe una promoción con el mismo nombre
                                if (!VerificarExistenciaObjeto(oBEPromociones))
                                {
                                    // Obtener nuevo Id autoincremental
                                    int nuevoId = ObtenerUltimoId() + 1;
                                    oBEPromociones.Id = nuevoId;

                                    // Crear nuevo nodo de promoción
                                    XElement nuevaPromo = new XElement("promocion",
                                        new XAttribute("Id", oBEPromociones.Id.ToString().Trim()),
                                        new XElement("Nombre", oBEPromociones.Nombre.Trim()),
                                        new XElement("Tipo", oBEPromociones.Tipo.ToString().Trim()),
                                        new XElement("ValorDescuento", oBEPromociones.ValorDescuento),
                                        new XElement("MontoMinimo", oBEPromociones.MontoMinimo),
                                        new XElement("Activa", oBEPromociones.Activa.ToString().ToLower())
                                    );

                                    // Agregar métodos de pago
                                    XElement metodosPagoElement = new XElement("MetodosPago");
                                    foreach (var metodo in oBEPromociones.MetodosPago)
                                        metodosPagoElement.Add(new XElement("Metodo", metodo.Trim()));

                                    nuevaPromo.Add(metodosPagoElement);

                                    // Agregar al nodo Promociones
                                    BDXML.Root.Element("Promociones").Add(nuevaPromo);

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: Ya existe una promoción con ese nombre.");
                                }
                            }
                            // Modificación de promoción existente
                            else
                            {
                                var buscarPromo = from promo in BDXML.Root.Element("Promociones").Descendants("promocion")
                                                  where promo.Attribute("Id").Value.Trim() == oBEPromociones.Id.ToString().Trim()
                                                  select promo;

                                if (buscarPromo.Any())
                                {
                                    foreach (XElement promoModificada in buscarPromo)
                                    {
                                        promoModificada.Element("Nombre").Value = oBEPromociones.Nombre.Trim();
                                        promoModificada.Element("Tipo").Value = oBEPromociones.Tipo.ToString().Trim();
                                        promoModificada.Element("ValorDescuento").Value = oBEPromociones.ValorDescuento.ToString();
                                        promoModificada.Element("MontoMinimo").Value = oBEPromociones.MontoMinimo.ToString();
                                        promoModificada.Element("Activa").Value = oBEPromociones.Activa.ToString().ToLower();

                                        // Actualizar métodos de pago
                                        promoModificada.Element("MetodosPago")?.Remove();
                                        XElement metodosPagoElement = new XElement("MetodosPago");
                                        foreach (var metodo in oBEPromociones.MetodosPago)
                                            metodosPagoElement.Add(new XElement("Metodo", metodo.Trim()));
                                        promoModificada.Add(metodosPagoElement);
                                    }

                                    BDXML.Save(ruta);
                                }
                                else
                                {
                                    throw new Exception("Error: No se encontró la promoción que se intenta modificar.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo obtener los datos de la promoción.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar o crear el XML.");
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }


        public BEPromociones ListarObjeto(BEPromociones oBEPromociones)
        {
            try
            {
                // Verifico que exista el XML
                if (CrearXML())
                {
                    // Cargo el XML
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        // Busco la promoción por Id o Nombre
                        var buscarPromocion = from promo in BDXML.Root.Element("Promociones").Descendants("promocion")
                                              where promo.Attribute("Id").Value.Trim() == oBEPromociones.Id.ToString().Trim()
                                                 || promo.Element("Nombre").Value.Trim().Equals(oBEPromociones.Nombre, StringComparison.OrdinalIgnoreCase)
                                              select promo;

                        // Verifico que exista la promoción
                        if (buscarPromocion.Any())
                        {
                            var promoEncontrada = buscarPromocion.First();

                            // Recupero lista de métodos de pago si existen
                            var metodosPagoXML = promoEncontrada.Element("MetodosPago")?.Descendants("Metodo");
                            List<string> metodosPago = new List<string>();

                            if (metodosPagoXML != null && metodosPagoXML.Any())
                            {
                                foreach (var metodo in metodosPagoXML)
                                {
                                    metodosPago.Add(metodo.Value.Trim());
                                }
                            }

                            // Creo el objeto BEPromociones
                            BEPromociones oPromo = new BEPromociones
                            {
                                Id = int.Parse(promoEncontrada.Attribute("Id").Value.Trim()),
                                Nombre = promoEncontrada.Element("Nombre").Value.Trim(),
                                Tipo = (BEPromociones.TipoPromocion)Enum.Parse(typeof(BEPromociones.TipoPromocion),
                                                                             promoEncontrada.Element("Tipo").Value.Trim()),
                                ValorDescuento = decimal.Parse(promoEncontrada.Element("ValorDescuento").Value.Trim()),
                                MontoMinimo = decimal.Parse(promoEncontrada.Element("MontoMinimo").Value.Trim()),
                                Activa = bool.Parse(promoEncontrada.Element("Activa").Value.Trim()),
                                MetodosPago = metodosPago
                            };

                            return oPromo;
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró la promoción con el Id o Nombre brindado.");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo cargar el archivo XML.");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo crear o acceder al XML.");
                }
            }
            catch (XmlException ex)
            {
                throw new XmlException("Error en el XML: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar promoción: " + ex.Message, ex);
            }
        }
        public List<BEPromociones> ListarTodo()
        {
            try
            {
                // Crear o cargar XML
                if (!CrearXML())
                    throw new Exception("Error: No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);
                XElement nodoPromociones = BDXML.Root.Element("Promociones");

                // Si no existe nodoPromociones, devolvemos lista vacía
                if (nodoPromociones == null)
                    return new List<BEPromociones>();

                // Seleccionamos todas las promociones
                var listaPromociones = nodoPromociones.Elements("promocion")
                    .Select(p => new BEPromociones
                    {
                        Id = int.TryParse(p.Attribute("Id")?.Value, out int id) ? id : 0,
                        Nombre = p.Element("Nombre")?.Value.Trim() ?? "",
                        Tipo = p.Element("Tipo") != null
                               ? (BEPromociones.TipoPromocion)Enum.Parse(
                                   typeof(BEPromociones.TipoPromocion),
                                   p.Element("Tipo").Value.Trim())
                               : BEPromociones.TipoPromocion.Porcentaje,
                        ValorDescuento = p.Element("ValorDescuento") != null
                                        && decimal.TryParse(p.Element("ValorDescuento").Value.Trim(), out decimal val) ? val : 0,
                        MontoMinimo = p.Element("MontoMinimo") != null
                                      && decimal.TryParse(p.Element("MontoMinimo").Value.Trim(), out decimal min) ? min : 0,
                        Activa = p.Element("Activa") != null
                                && bool.TryParse(p.Element("Activa").Value.Trim(), out bool activa) ? activa : false,
                        MetodosPago = p.Element("MetodosPago") != null
                                      ? p.Element("MetodosPago").Elements("Metodo")
                                         .Select(m => m.Value.Trim()).ToList()
                                      : new List<string>()
                    })
                    .ToList();

                return listaPromociones;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar todas las promociones: " + ex.Message);
            }
        }
        public int ObtenerUltimoId()
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pId = from promocion in BDXML.Root.Element("Promociones").Descendants("promocion")
                                  select int.Parse(promocion.Attribute("Id").Value.Trim());
                        //Verifico si encontro un Id de promocion:
                        if (pId.Any())
                        {
                            //Selecciono el Valor Máximo de Id de promocion:
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no existe un Id de promocion, devuevlo 0:
                        else { return 0; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }


        public bool VerificarExistenciaObjeto(BEPromociones oBEPromociones)
        {
            try
            {
                // Verifico que exista el XML:
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEPromociones != null)
                        {
                            // Busco si existe una promoción con el mismo nombre o tipo y fechas iguales
                            var buscarPromocion = from promo in BDXML.Root.Element("Promociones").Descendants("promocion")
                                                  where promo.Element("Nombre").Value.Trim().Equals(oBEPromociones.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)
                                                  select promo;

                            // Si existe al menos una promoción con ese nombre, devuelvo true
                            return buscarPromocion.Any();
                        }
                        else
                        {
                            throw new Exception("Error: No se pudo recuperar la información de la promoción!");
                        }
                    }
                    else
                    {
                        throw new XmlException("Error: No se pudo recuperar la información del XML!");
                    }
                }
                else
                {
                    throw new XmlException("Error: No se pudo recuperar el XML!");
                }
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
