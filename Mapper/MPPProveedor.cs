using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Entity.BEInsumo;

namespace Mapper
{
    public class MPPProveedor
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
                        new XElement("Proveedores")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //Si Existe el XML Verifico que exista el Elemento Proveedores
                    BDXML = XDocument.Load(ruta);
                    XElement proveedores = BDXML.Root.Element("Proveedores");
                    if (proveedores != null) { return true; }
                    //Si no Existe el Elemento Proovedores pero si el XML creo el Elemento
                    else
                    {
                        proveedores = new XElement("Proveedores");
                        BDXML.Root.Add(proveedores);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        

        public bool Eliminar(BEProveedor oBEProveedor)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    BDXML = XDocument.Load(ruta);
                    //Verifico que el XML se cargo correctamente:
                    if (BDXML != null)
                    {
                        //Busco el Proveedor:
                        var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                              where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                              select proveedor;
                        //Verifico que exista el Proveedor que se busco:
                        if (buscarProveedor.Any())
                        {
                            var proveedorEncontrado = buscarProveedor.First();
                            //Si tiene productos no se puede ELiminar:
                            if (proveedorEncontrado.Element("Insumos") == null)
                            {
                                //Si esta por lo menos en una Solicitud de Cotizacion tampoco se puede eliminar:
                                var buscarSolicitudDeCotizacionConPRoveedor = from solicitudesCotizaciones in BDXML.Root.Element("Solicitudes_Cotizaciones").Descendants("solicitud_cotizacion")
                                                                              where solicitudesCotizaciones.Element("proveedor").Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                                                              select solicitudesCotizaciones;
                                if (buscarSolicitudDeCotizacionConPRoveedor.Any()) { throw new Exception("Error: No se puede eliminar un Proveedor que tiene asociado una Solicitud de Cotización!"); }
                                else
                                {
                                    proveedorEncontrado.Remove();
                                    BDXML.Save(ruta);
                                    return true;
                        }
                    }
                    else { throw new Exception("Error: No se puede Eliminar un Proveedor que tiene asociados Productos!"); }
                }
                        else { throw new Exception("Error: No se encontro el Proveedor que trata de Eliminar!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar los datos del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Guardar(BEProveedor oBEProveedor)
        {
            try
            {
                if (!CrearXML())
                    throw new XmlException("Error: No se pudo recuperar el XML!");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new XmlException("Error: No se pudo recuperar los datos del XML!");

                // --- ALTA ---
                if (oBEProveedor.Id == 0)
                {
                    if (VerificarExistenciaObjeto(oBEProveedor))
                        throw new Exception("Error: El proveedor ya existe!");

                    int nuevoId = ObtenerUltimoId() + 1;
                    oBEProveedor.Id = nuevoId;

                    XElement nuevoProveedor = new XElement("proveedor",
                        new XAttribute("Id", oBEProveedor.Id.ToString().Trim()),
                        new XElement("Nombre", oBEProveedor.Nombre.Trim()),
                        new XElement("CUIL", oBEProveedor.CUIL.ToString().Trim()),
                        new XElement("Domicilio", oBEProveedor.Domicilio.Trim()),
                        new XElement("Email", oBEProveedor.Email.Trim()),
                        new XElement("Telefono", oBEProveedor.Telefono.Trim())
                    );

                    // --- Agregar lista de insumos ---
                    XElement insumosElement = new XElement("Insumos");
                    foreach (var insumo in oBEProveedor.ListaInsumos)
                    {
                        XElement nuevoInsumo = new XElement("insumo",
                            new XAttribute("Id", insumo.Id.ToString().Trim()),
                            new XElement("Cantidad", insumo.Cantidad),
                            new XElement("Precio", insumo.Precio)
                        );
                        insumosElement.Add(nuevoInsumo);
                    }

                    nuevoProveedor.Add(insumosElement);
                    BDXML.Root.Element("Proveedores").Add(nuevoProveedor);
                    BDXML.Save(ruta);
                    return true;
                }

                // --- MODIFICACIÓN ---
                else
                {
                    var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                          where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                          select proveedor;

                    if (!buscarProveedor.Any())
                        throw new Exception("Error: No se pudo recuperar el proveedor con el Id brindado!");

                    XElement proveedorModificado = buscarProveedor.First();

                    proveedorModificado.Element("Nombre").Value = oBEProveedor.Nombre.Trim();
                    proveedorModificado.Element("Domicilio").Value = oBEProveedor.Domicilio.Trim();
                    proveedorModificado.Element("Email").Value = oBEProveedor.Email.Trim();
                    proveedorModificado.Element("Telefono").Value = oBEProveedor.Telefono.Trim();

                    // Actualizar lista de insumos
                    XElement insumosElement = new XElement("Insumos");
                    foreach (var insumo in oBEProveedor.ListaInsumos)
                    {
                        XElement nuevoInsumo = new XElement("insumo",
                            new XAttribute("Id", insumo.Id.ToString().Trim()),
                            new XElement("Cantidad", insumo.Cantidad),
                            new XElement("Precio", insumo.Precio)
                        );
                        insumosElement.Add(nuevoInsumo);
                    }
                    proveedorModificado.Element("Insumos")?.Remove();
                    proveedorModificado.Add(insumosElement);

                    BDXML.Save(ruta);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BEProveedor ListarObjeto(BEProveedor oBEProveedor)
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
                        // Busco el proveedor por CUIL o Id
                        var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                              where proveedor.Element("CUIL").Value.Trim() == oBEProveedor.CUIL.ToString().Trim()
                                                 || proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                              select proveedor;

                        // Verifico que exista el proveedor
                        if (buscarProveedor.Any())
                        {
                            var proveedorEncontrado = buscarProveedor.First();

                            // Recupero la lista de insumos asociados al proveedor
                            var ListaInsumos = proveedorEncontrado.Element("Insumos")?.Descendants("insumo");

                            // Creo el objeto proveedor base
                            BEProveedor oProveedor = new BEProveedor
                            {
                                Id = int.Parse(proveedorEncontrado.Attribute("Id").Value.Trim()),
                                Nombre = proveedorEncontrado.Element("Nombre").Value.Trim(),
                                CUIL = long.Parse(proveedorEncontrado.Element("CUIL").Value.Trim()),
                                Domicilio = proveedorEncontrado.Element("Domicilio").Value.Trim(),
                                Email = proveedorEncontrado.Element("Email").Value.Trim(),
                                Telefono = proveedorEncontrado.Element("Telefono").Value.Trim(),
                                ListaInsumos = new List<BEProveedorInsumo>() 
                            };

                            // Si no tiene insumos, devuelvo el proveedor vacío
                            if (ListaInsumos == null || !ListaInsumos.Any())
                                return oProveedor;

                            // Si tiene insumos asociados
                            foreach (var insumo in ListaInsumos)
                            {
                                // Busco el insumo global por Id
                                var insumoGlobal = (from ig in BDXML.Root.Element("Insumos").Descendants("insumo")
                                                    where ig.Attribute("Id").Value.Trim() == insumo.Attribute("Id").Value.Trim()
                                                    select ig).FirstOrDefault();

                                if (insumoGlobal != null)
                                {
                                    BEInsumo oBEInsumo = new BEInsumo
                                    {
                                        Id = int.Parse(insumoGlobal.Attribute("Id").Value.Trim()),
                                        Nombre = insumoGlobal.Element("Nombre").Value.Trim(),
                                        UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), insumoGlobal.Element("UnidadMedida").Value.Trim()),
                                        Cantidad = int.Parse(insumoGlobal.Element("Cantidad").Value.Trim()),
                                        Precio = decimal.Parse(insumoGlobal.Element("Precio").Value.Trim())
                                    };

                                    // ✅ En lugar de agregar el insumo directo, agregamos un BEProveedorInsumo
                                    oProveedor.ListaInsumos.Add(new BEProveedorInsumo
                                    {
                                        Proveedor = oProveedor,
                                        Insumo = oBEInsumo
                                    });
                                }
                            }

                            return oProveedor;
                        }
                        else
                        {
                            throw new Exception("Error: No se encontró el Proveedor con el CUIL o Id brindado.");
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
                throw new Exception("Error al listar proveedor: " + ex.Message, ex);
            }
        }



        public List<BEProveedor> ListarTodo()
        {
            try
            {
                if (!CrearXML())
                    throw new Exception("Error: No se pudo crear o acceder al XML.");

                BDXML = XDocument.Load(ruta);
                if (BDXML == null)
                    throw new Exception("Error: No se pudo cargar el archivo XML.");

                // Recupero la lista global de insumos
                var insumosGlobales = BDXML.Root.Element("Insumos")?
                    .Descendants("insumo")
                    .ToDictionary(
                        p => int.Parse(p.Attribute("Id").Value.Trim()),
                        p => new BEInsumo
                        {
                            Id = int.Parse(p.Attribute("Id").Value.Trim()),
                            Nombre = p.Element("Nombre").Value.Trim(),
                            UnidadMedida = (UnidadesMedida)Enum.Parse(typeof(UnidadesMedida), p.Element("UnidadMedida").Value.Trim()),
                            Cantidad = decimal.Parse(p.Element("Cantidad").Value.Trim()),
                            Precio = decimal.Parse(p.Element("Precio").Value.Trim())
                        }
                    );

                if (insumosGlobales == null)
                    throw new Exception("Error: No se encontró la lista de insumos en el XML.");

                // Recupero la lista de proveedores
                var listaProveedores = (
                    from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                    select new BEProveedor
                    {
                        Id = int.Parse(proveedor.Attribute("Id").Value.Trim()),
                        Nombre = proveedor.Element("Nombre").Value.Trim(),
                        CUIL = long.Parse(proveedor.Element("CUIL").Value.Trim()),
                        Domicilio = proveedor.Element("Domicilio").Value.Trim(),
                        Email = proveedor.Element("Email").Value.Trim(),
                        Telefono = proveedor.Element("Telefono").Value.Trim(),
                        ListaInsumos = new List<BEProveedorInsumo>()
                    }).ToList();

                // Combino los insumos asociados a cada proveedor
                foreach (var proveedor in listaProveedores)
                {
                    var nodoProveedor = BDXML.Root.Element("Proveedores")
                        .Descendants("proveedor")
                        .FirstOrDefault(p => p.Attribute("Id").Value == proveedor.Id.ToString());

                    if (nodoProveedor?.Element("Insumos") != null)
                    {
                        foreach (var insumo in nodoProveedor.Element("Insumos").Elements("insumo"))
                        {
                            int idInsumo = int.Parse(insumo.Attribute("Id").Value.Trim());

                            if (insumosGlobales.TryGetValue(idInsumo, out BEInsumo insumoGlobal))
                            {
                                var proveedorInsumo = new BEProveedorInsumo
                                {
                                    Proveedor = proveedor,
                                    Insumo = insumoGlobal
                                };

                                proveedor.ListaInsumos.Add(proveedorInsumo);
                            }
                        }
                    }
                }

                return listaProveedores;
            }
            catch (XmlException ex)
            {
                throw new XmlException("Error en el XML: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar todos los proveedores: " + ex.Message, ex);
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
                        var pId = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                  select int.Parse(proveedor.Attribute("Id").Value.Trim());
                        //Verifico si encontro un Id de Proveedores:
                        if (pId.Any())
                        {
                            //Selecciono el Valor Máximo de Id de Proveedores:
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no existe un Id de Proveedor, devuevlo 0:
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

        public bool VerificarExistenciaObjeto(BEProveedor oBEProveedor)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la información del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEProveedor.Id >= 0)
                        {
                            //Busco a ver si existe al menos un  Proveedor con el CUIL brindado:
                            var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                                  where proveedor.Element("CUIL").Value.Trim() == oBEProveedor.CUIL.ToString().Trim()
                                                  select proveedor;
                            //Si existe al menos un, devuelvo true:
                            if (buscarProveedor.Count() > 0) { return true; }
                            //Si no, devuelvo false:
                            else { return false; }
                        }
                        else { throw new Exception("Error: No se pudo recuperar el Id del Proveedor!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }


        //public bool CrearXMLStockInsumosProveedor()
        //{
        //    try
        //    {
        //        //Verifico que exista el XML:
        //        if (!(File.Exists(ruta)))
        //        {
        //            //Si no existe, lo creo:
        //            BDXML = new XDocument(new XElement("Root",
        //            new XElement("Stocks_Insumos_Proveedores")));
        //            BDXML.Save(ruta);
        //            return true;
        //        }
        //        else
        //        {
        //            //En caso que exista el XML, verifico que exista el Elemento Stocks_Productos_Proveedores:
        //            BDXML = XDocument.Load(ruta);
        //            XElement stockInsumosProveedor = BDXML.Root.Element("Stocks_Insumos_Proveedores");
        //            //Si existe, devuelvo true:
        //            if (stockInsumosProveedor != null) { return true; }
        //            //Si no, lo creo:
        //            else
        //            {
        //                stockInsumosProveedor = new XElement("Stocks_Insumos_Proveedores");
        //                BDXML.Root.Add(stockInsumosProveedor);
        //                BDXML.Save(ruta);
        //                return true;
        //            }
        //        }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public bool CrearXMLPrecioProductoProveedor()
        //{
        //    try
        //    {
        //        //Verifico que exista el XML:
        //        if (!(File.Exists(ruta)))
        //        {
        //            //Si no existe, lo creo:
        //            BDXML = new XDocument(new XElement("Root",
        //            new XElement("Precios_Insumos_Proveedores")));
        //            BDXML.Save(ruta);
        //            return true;
        //        }
        //        else
        //        {
        //            //En caso que exista el XML, verifico que exista el Elemento Precio_Producto_Proveedor:
        //            BDXML = XDocument.Load(ruta);
        //            XElement precioInsumoProveedor = BDXML.Root.Element("Precios_Insumos_Proveedores");
        //            //Si existe, devuelvo true:
        //            if (precioInsumoProveedor != null) { return true; }
        //            //Si no, lo creo:
        //            else
        //            {
        //                precioInsumoProveedor = new XElement("Precios_Insumos_Proveedores");
        //                BDXML.Root.Add(precioInsumoProveedor);
        //                BDXML.Save(ruta);
        //                return true;
        //            }
        //        }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public bool AsociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor)
        //{
        //    try
        //    {
        //        //Verifico que existe el XML:
        //        if (CrearXML() == true && CrearXMLPrecioProductoProveedor() == true && CrearXMLStockProductoProveedor() == true)
        //        {
        //            //Verifico que se haya cargado la info del XML:
        //            BDXML = XDocument.Load(ruta);
        //            if (BDXML != null)
        //            {
        //                //Verifico que el Proveedor exista:
        //                if (oBEProveedor.Id > 0)
        //                {
        //                    //Busco al Proveedor:
        //                    var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
        //                                          where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
        //                                          select proveedor;
        //                    //Verifico si encontro el Proveedor:
        //                    if (buscarProveedor.Any())
        //                    {
        //                        //Verifico que el Producto en el Elemento Productos Exista:
        //                        if (oBEProducto.Id > 0)
        //                        {
        //                            var buscarProductoGlobal = from producto in BDXML.Root.Element("Productos").Descendants("producto")
        //                                                       where producto.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                       select producto;
        //                            if (buscarProductoGlobal.Any())
        //                            {
        //                                var proveedorEncontrado = buscarProveedor.First();
        //                                //Actualizo los datos del Producto Global asociando la Cantidad y el Stock del Producto proveedor:
        //                                var insumoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoProveedor = insumoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                // Si el Stock del Producto del proveedor no existe en el Producto lo asocio:
        //                                if (stockProductoProveedor == null)
        //                                {
        //                                    insumoGlobalEncontrado.Add(new XElement("stock_producto_proveedor",
        //                                        new XAttribute("Id", oBEStockProductoProveedor.Id.ToString().Trim())));
        //                                    BDXML.Save(ruta);
        //                                }
        //                                else { throw new Exception("Error: El Producto del Proveedor ya se encuentra asociado al Proveedor!"); }
        //                                // Si el Precio del Producto del proveedor no existe en el Producto lo asocio:
        //                                var precioProductoProveedor = insumoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (precioProductoProveedor == null)
        //                                {
        //                                    insumoGlobalEncontrado.Add(new XElement("precio_producto_proveedor",
        //                                        new XAttribute("Id", oBEPrecioProductoProveedor.Id.ToString().Trim())));
        //                                    BDXML.Save(ruta);
        //                                }
        //                                else { throw new Exception("Error: El Producto del Proveedor ya se encuentra asociado al Proveedor!"); }
        //                                if (proveedorEncontrado != null)
        //                                {
        //                                    //En caso de que no tenga ningun producto el Proveedor:
        //                                    var productosPorProveedor = proveedorEncontrado.Element("Productos");
        //                                    if (productosPorProveedor == null)
        //                                    {
        //                                        proveedorEncontrado.Add(new XElement("Productos"));
        //                                        BDXML.Save(ruta);
        //                                        productosPorProveedor = proveedorEncontrado.Element("Productos");
        //                                    }
        //                                    var buscarPorductoProveedor = from productoProveedor in productosPorProveedor.Descendants("producto")
        //                                                                  where productoProveedor.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                                  select productoProveedor;

        //                                    if (buscarPorductoProveedor.Any()) { throw new Exception("Error: El Producto ya está asociado al Proveedor!"); }
        //                                    //En caso de que ya exista la lista del productos:
        //                                    else
        //                                    {
        //                                        productosPorProveedor.Add(new XElement("producto",
        //                                            new XAttribute("Id", oBEProducto.Id.ToString().Trim())));
        //                                        BDXML.Save(ruta);
        //                                        return true;
        //                                    }
        //                                }
        //                                else { throw new Exception("Error: Surgio un problema al recuperar los datos del Proveedor como el detalle del Producto!"); }
        //                            }
        //                            else { throw new Exception("Error: No se pudo recuperar la información del XML del precio ni stock del Producto del Proveedor!"); }
        //                        }
        //                        else { throw new Exception("Error: No se pudo recuperar los datos que brindo del Producto, como su cantidad ni precio!"); }
        //                    }
        //                    else { throw new Exception("Error: No se pudo recuperar los datos del Proveedor que brindo!"); }
        //                }
        //                else { throw new Exception("Error: Primero tiene que seleccionar al Proveedor!"); }
        //            }
        //            else { throw new Exception("Error: No se pudo recuperar los datos del XML!"); }
        //        }
        //        else { throw new Exception("Error: No se pudo recuperar el XML!"); }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public bool VerificarProductoEnProveedor(BEProveedor oBEProveedor, BEProducto oBEProducto)
        //{
        //    try
        //    {
        //        //Verifico que exista el XML:
        //        if (CrearXML() == true)
        //        {
        //            //Verifico que se cargue la info del XML:
        //            BDXML = XDocument.Load(ruta);
        //            if (BDXML != null)
        //            {
        //                //Verifico que este inicializado el Proveedor:
        //                if (oBEProveedor.Id > 0)
        //                {
        //                    //Busco al Proveedor:
        //                    var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
        //                                          where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
        //                                          select proveedor;
        //                    //Si encuentro al Proveedor:
        //                    if (buscarProveedor.Any())
        //                    {
        //                        var proveedorEncontrado = buscarProveedor.First();
        //                        if (proveedorEncontrado != null)
        //                        {
        //                            var productosEnProveedor = proveedorEncontrado.Element("Productos");
        //                            if (productosEnProveedor != null)
        //                            {
        //                                var productosEncontradosEnProvedor = productosEnProveedor.Descendants("producto");
        //                                if (productosEncontradosEnProvedor.Count() > 0)
        //                                {
        //                                    var buscarProductoEnProveedor = from productoProveedor in proveedorEncontrado.Element("Productos").Descendants("producto")
        //                                                                    where productoProveedor.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                                    select productoProveedor;
        //                                    //En caso de que exista el Producto en la lista del Proveedor devuelvo true:
        //                                    if (buscarProductoEnProveedor.Count() > 0) { return true; }
        //                                    //En caso de que no exista el Producto en la lista del Proveedor devuelvo false:
        //                                    else { return false; }
        //                                }
        //                                else { return false; }
        //                            }
        //                            else { return false; }
        //                        }
        //                        else { throw new Exception("Error: No se pudo recuperar los datos del Proveedor!"); }
        //                    }
        //                    else { throw new Exception("Error: No se pudo recuperar la información del Proveedor!"); }
        //                }
        //                else { throw new Exception("Error: Primero tiene que seleccionar al Proveedor!"); }
        //            }
        //            else { throw new XmlException("Error: No se pudo recuperar los datos del XML!"); }
        //        }
        //        else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public bool DesasociarProveedorProducto(BEProveedor oBEProveedor, BEProducto oBEProducto, BEStockProductoProveedor oBEStockProductoProveedor, BEPrecioProductoProveedor oBEPrecioProductoProveedor)
        //{
        //    try
        //    {
        //        //Verifico que existe el XML:
        //        if (CrearXML() == true && CrearXMLPrecioProductoProveedor() == true && CrearXMLStockProductoProveedor() == true)
        //        {
        //            //Verifico que se haya cargado la info del XML:
        //            BDXML = XDocument.Load(ruta);
        //            if (BDXML != null)
        //            {
        //                //Verifico que el Proveedor exista:
        //                if (oBEProveedor.Id > 0)
        //                {
        //                    //Busco al Proveedor:
        //                    var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
        //                                          where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
        //                                          select proveedor;
        //                    //Verifico si encontro el Proveedor:
        //                    if (buscarProveedor.Any())
        //                    {
        //                        //Verifico que el Producto en el Elemento Productos Exista:
        //                        if (oBEProducto.Id > 0)
        //                        {
        //                            var buscarProductoGlobal = from producto in BDXML.Root.Element("Productos").Descendants("producto")
        //                                                       where producto.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                       select producto;
        //                            if (buscarProductoGlobal.Any())
        //                            {
        //                                var proveedorEncontrado = buscarProveedor.First();
        //                                //Actualizo los datos del Producto Global asociando la Cantidad y el Stock del Producto proveedor:
        //                                var insumoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoGlobalProveedor = insumoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                var precioProductoGlobalProveedor = insumoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (insumoGlobalEncontrado != null && stockProductoGlobalProveedor != null && precioProductoGlobalProveedor != null)
        //                                {
        //                                    var productoEnProveedor = from productoProveedor in proveedorEncontrado.Element("Productos").Descendants("producto")
        //                                                              where productoProveedor.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                              select productoProveedor;
        //                                    if (productoEnProveedor != null)
        //                                    {
        //                                        var productoEnProveedorEncontrado = productoEnProveedor.First();
        //                                        var precioProductoProveedor = from precioProducto in BDXML.Root.Element("Precios_Productos_Proveedores").Descendants("precio_producto_proveedor")
        //                                                                      where precioProducto.Attribute("Id").Value.Trim() == oBEPrecioProductoProveedor.Id.ToString().Trim()
        //                                                                      select precioProducto;
        //                                        var stockProductoProveedor = from stockProducto in BDXML.Root.Element("Stocks_Productos_Proveedores").Descendants("stock_producto_proveedor")
        //                                                                     where stockProducto.Attribute("Id").Value.Trim() == oBEStockProductoProveedor.Id.ToString().Trim()
        //                                                                     select stockProducto;
        //                                        if (precioProductoProveedor != null && stockProductoProveedor != null)
        //                                        {
        //                                            var buscarSolicitudConProveedor = from solicitudProveedor in BDXML.Root.Element("Solicitudes_Cotizaciones").Descendants("solicitud_cotizacion")
        //                                                                              where solicitudProveedor.Element("proveedor").Attribute("Id").Value == oBEProveedor.Id.ToString().Trim()
        //                                                                              select solicitudProveedor;
        //                                            if (buscarSolicitudConProveedor.Any())
        //                                            {
        //                                                var solicitudProveedorEncontrada = buscarSolicitudConProveedor.First();
        //                                                var buscarProductoEnSolicitud = from productoEnSolicitud in BDXML.Root.Element("Cantidades_Productos_Solicitudes").Descendants("cantidad_producto_solicitud")
        //                                                                                where productoEnSolicitud.Element("Id_Producto").Value == oBEProducto.Id.ToString().Trim()
        //                                                                                && productoEnSolicitud.Element("Id_Solicitud_Cotizacion").Value == solicitudProveedorEncontrada.Attribute("Id").Value.Trim()
        //                                                                                select productoEnSolicitud;
        //                                                if (!buscarProductoEnSolicitud.Any())
        //                                                {
        //                                                    stockProductoGlobalProveedor.Remove();
        //                                                    precioProductoGlobalProveedor.Remove();
        //                                                    productoEnProveedorEncontrado.Remove();
        //                                                    precioProductoProveedor.Remove();
        //                                                    stockProductoProveedor.Remove();
        //                                                    BDXML.Save(ruta);
        //                                                    return true;
        //                                                }
        //                                                else { throw new Exception("Error: No se puede desasoriar un Producto de un Proveedor que se encuentra en al menos una Solicitud de Cotizacion!"); }
        //                                            }
        //                                            else
        //                                            {
        //                                                stockProductoGlobalProveedor.Remove();
        //                                                precioProductoGlobalProveedor.Remove();
        //                                                productoEnProveedorEncontrado.Remove();
        //                                                precioProductoProveedor.Remove();
        //                                                stockProductoProveedor.Remove();
        //                                                BDXML.Save(ruta);
        //                                                return true;
        //                                            }
        //                                        }
        //                                        else { throw new Exception("Error: No se pudo recuperar el precio ni el stock del Producto del Proveedor!"); }
        //                                    }
        //                                    else { throw new Exception("Error: No se pudo recuperar el Producto en el proveedor!"); }
        //                                }
        //                                else { throw new Exception("Error: No se pudo recuperar los datos del Producto!"); }
        //                            }
        //                            else { throw new Exception("Error: No se pudo recuperar la información del XML del precio ni stock del Producto del Proveedor!"); }
        //                        }
        //                        else { throw new Exception("Error: No se pudo recuperar los datos que brindo del Producto, como su cantidad ni precio!"); }
        //                    }
        //                    else { throw new Exception("Error: No se pudo recuperar los datos del Proveedor que brindo!"); }
        //                }
        //                else { throw new Exception("Error: Primero tiene que seleccionar al Proveedor!"); }
        //            }
        //            else { throw new Exception("Error: No se pudo recuperar los datos del XML!"); }
        //        }
        //        else { throw new Exception("Error: No se pudo recuperar el XML!"); }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public bool RestarStockProveedorPorEntrega(BEProveedor oBEProovedor, BEProducto oBEProducto)
        //{
        //    try
        //    {
        //        //Verifico que exista el XML:
        //        if (CrearXML() == true)
        //        {
        //            //Verifico que se cargue la info del XML:
        //            BDXML = XDocument.Load(ruta);
        //            if (BDXML != null)
        //            {
        //                //Busco al Proveedor:
        //                var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
        //                                      where proveedor.Attribute("Id").Value.Trim() == oBEProovedor.Id.ToString().Trim()
        //                                      select proveedor;
        //                //Verifico si lo encuentra:
        //                if (buscarProveedor.Any())
        //                {
        //                    XElement proveedorElement = buscarProveedor.First();
        //                    XElement productosProveedorElement = proveedorElement.Element("Productos");
        //                    //Verifico que tenga el Elemento Productos creado:
        //                    if (productosProveedorElement != null)
        //                    {
        //                        //Busco al Producto del Proveedor:
        //                        var buscarProductoProveedor = from productoProveedor in productosProveedorElement.Elements("producto")
        //                                                      where productoProveedor.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
        //                                                      select productoProveedor;
        //                        //Verifico si lo encuentra:
        //                        if (buscarProductoProveedor.Any())
        //                        {
        //                            XElement productoProveedorElement = buscarProductoProveedor.First();
        //                            int cantidadStock = int.Parse(productoProveedorElement.Element("Cantidad").Value.Trim());
        //                            productoProveedorElement.Element("Cantidad").Value = cantidadStock.ToString().Trim();
        //                            BDXML.Save(ruta);
        //                            return true;
        //                        }
        //                        else { throw new Exception($"Error: No se encontró el producto con Id {oBEProducto.Id} en el proveedor con Id {oBEProovedor.Id}!"); }
        //                    }
        //                    else { throw new Exception($"Error: El proveedor con Id {oBEProovedor.Id} no tiene productos asociados."); }
        //                }
        //                else { throw new Exception($"Error: No se encontró el proveedor con Id {oBEProovedor.Id}."); }
        //            }
        //            else { throw new XmlException("Error: No se pudo recuperar los datos del XML!"); }
        //        }
        //        else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        //public List<BEProducto> ListarProductosDelProveedor(BEProveedor oBEProveedor)
        //{
        //    try
        //    {
        //        // Verifico que exista el XML
        //        if (CrearXML())
        //        {
        //            // Recupero la información del XML
        //            BDXML = XDocument.Load(ruta);

        //            if (BDXML != null)
        //            {
        //                // Busco el Proveedor
        //                var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
        //                                      where proveedor.Element("CUIL").Value.Trim() == oBEProveedor.CUIL.ToString().Trim() ||
        //                                            proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
        //                                      select proveedor;

        //                // Verifico que exista el proveedor
        //                if (buscarProveedor.Any())
        //                {
        //                    var proveedorEncontrado = buscarProveedor.First();
        //                    var listaProductos = proveedorEncontrado.Element("Productos")?.Descendants("producto");

        //                    // Lista para almacenar los productos encontrados
        //                    List<BEProducto> productosProveedor = new List<BEProducto>();

        //                    if (listaProductos != null)
        //                    {
        //                        foreach (var producto in listaProductos)
        //                        {
        //                            var buscarProductoGlobal = from productoGlobal in BDXML.Root.Element("Productos").Descendants("producto")
        //                                                       where productoGlobal.Attribute("Id").Value.Trim() == producto.Attribute("Id").Value.Trim()
        //                                                       select productoGlobal;

        //                            if (buscarProductoGlobal.Any())
        //                            {
        //                                var insumoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoProveedor = insumoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                var precioProductoProveedor = insumoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (stockProductoProveedor != null && precioProductoProveedor != null)
        //                                {
        //                                    var stockId = insumoGlobalEncontrado.Element("stock_producto_proveedor")?.Attribute("Id")?.Value.Trim();
        //                                    var precioId = insumoGlobalEncontrado.Element("precio_producto_proveedor")?.Attribute("Id")?.Value.Trim();

        //                                    var stockProductoGlobal = from stockProducto in BDXML.Root.Element("Stocks_Productos_Proveedores").Descendants("stock_producto_proveedor")
        //                                                              where stockProducto.Attribute("Id").Value.Trim() == stockId
        //                                                              select stockProducto;

        //                                    var precioProductoGlobal = from precioProducto in BDXML.Root.Element("Precios_Productos_Proveedores").Descendants("precio_producto_proveedor")
        //                                                               where precioProducto.Attribute("Id").Value.Trim() == precioId
        //                                                               select precioProducto;

        //                                    if (stockProductoGlobal.Any() && precioProductoGlobal.Any())
        //                                    {
        //                                        var stockProductoEncontrado = stockProductoGlobal.First();
        //                                        var precioProductoEncontrado = precioProductoGlobal.First();

        //                                        BEProducto oBEProducto = new BEProducto
        //                                        {
        //                                            Id = long.Parse(insumoGlobalEncontrado.Attribute("Id").Value.Trim()),
        //                                            Nombre = insumoGlobalEncontrado.Element("Nombre").Value.Trim(),
        //                                            Marca = insumoGlobalEncontrado.Element("Marca").Value.Trim(),
        //                                            Anio = Convert.ToInt32(insumoGlobalEncontrado.Element("Anio").Value.Trim()),
        //                                            Modelo = Convert.ToInt32(insumoGlobalEncontrado.Element("Modelo").Value.Trim()),
        //                                            oBEPrecioProductoProveedor = new BEPrecioProductoProveedor
        //                                            {
        //                                                Id = long.Parse(precioProductoEncontrado.Attribute("Id").Value.Trim()),
        //                                                Precio = double.Parse(precioProductoEncontrado.Element("Precio").Value.Trim())
        //                                            },
        //                                            oBEStockProductoProveedor = new BEStockProductoProveedor
        //                                            {
        //                                                Id = long.Parse(stockProductoEncontrado.Attribute("Id").Value.Trim()),
        //                                                Cantidad = Convert.ToInt32(stockProductoEncontrado.Element("Cantidad").Value.Trim())
        //                                            }
        //                                        };

        //                                        productosProveedor.Add(oBEProducto);
        //                                    }
        //                                }
        //                                else { return productosProveedor; }
        //                            }
        //                        }
        //                    }
        //                    return productosProveedor;
        //                }
        //                else { throw new Exception("Error: No se encontró el Proveedor con el CUIL brindado."); }
        //            }
        //            else { throw new XmlException("Error: No se pudo recuperar la información del XML."); }
        //        }
        //        else { throw new XmlException("Error: No se pudo recuperar el XML."); }
        //    }
        //    catch (XmlException ex) { throw ex; }
        //    catch (Exception ex) { throw ex; }
        //    finally { }
        //}

        

        public int ContarTotalProveedores()
        {
            try
            {
                //Verifico que se exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        int cantidadProveedores = 0;
                        var buscarProveedores = BDXML.Root.Element("Proveedores").Descendants("proveedor");
                        if (buscarProveedores.Any())
                        {
                            foreach (var proveedor in buscarProveedores)
                            {
                                cantidadProveedores++;
                            }
                            return cantidadProveedores;
                        }
                        return cantidadProveedores;
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
    }
}
