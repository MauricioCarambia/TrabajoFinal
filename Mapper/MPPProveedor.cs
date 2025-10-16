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
                            //if (proveedorEncontrado.Element("Productos") == null)
                            //{
                                //Si esta por lo menos en una Solicitud de Cotizacion tampoco se puede eliminar:
                                //var buscarSolicitudDeCotizacionConPRoveedor = from solicitudesCotizaciones in BDXML.Root.Element("Solicitudes_Cotizaciones").Descendants("solicitud_cotizacion")
                                //                                              where solicitudesCotizaciones.Element("proveedor").Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                //                                              select solicitudesCotizaciones;
                                //if (buscarSolicitudDeCotizacionConPRoveedor.Any()) { throw new Exception("Error: No se puede eliminar un Proveedor que tiene asociado una Solicitud de Cotización!"); }
                                //else
                                //{
                                    proveedorEncontrado.Remove();
                                    BDXML.Save(ruta);
                                    return true;
                                //}
                            //}
                            //else { throw new Exception("Error: No se puede Eliminar un Proveedor que tiene asociados Productos!"); }
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
                //Verifico si esta creado el XML:
                if (CrearXML() == true)
                {
                    //Cargo la ruta del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico si es el alta de un nuevo Proveedor:
                        if (oBEProveedor.Id == 0)
                        {
                            //Verifico antes de darle el alta, de que no exista el Proveedor:
                            if (VerificarExistenciaObjeto(oBEProveedor) == false)
                            {
                                //Busco el id mas grande que existe en el XML y le asigno el siguiente:
                                int nuevoId = ObtenerUltimoId() + 1;
                                oBEProveedor.Id = nuevoId;
                                BDXML.Root.Element("Proveedores").Add(new XElement("proveedor",
                                    new XAttribute("Id", oBEProveedor.Id.ToString().Trim()),
                                    new XElement("Nombre", oBEProveedor.Nombre.Trim()),
                                    new XElement("CUIL", oBEProveedor.CUIL.ToString().Trim()),
                                    new XElement("Domicilio", oBEProveedor.Domicilio.Trim()),
                                    new XElement("Email", oBEProveedor.Email.Trim()),
                                    new XElement("Telefono", oBEProveedor.Telefono.Trim())));
                                //Le creo el Elemento de lista de Productos:
                                var productosElement = new XElement("Productos");
                                //foreach (var producto in oBEProveedor.listaProductos)
                                //{
                                //    XElement nuevoProducto;
                                //    nuevoProducto = new XElement("producto",
                                //        new XAttribute("Id", producto.Id.ToString().Trim()));
                                //    productosElement.Add(nuevoProducto);
                                //    BDXML.Root.Element("Proveedores").Add(productosElement);
                                //}
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede dar el alta a un Proveedor que ya existe!"); }
                        }
                        //En caso de que existe, lo modifico:
                        else
                        {
                            //Busco al Proveedor:
                            var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                                  where proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                                  select proveedor;
                            if (buscarProveedor != null)
                            {
                                foreach (XElement proveedorModificado in buscarProveedor)
                                {
                                    //Le asigno los nuevos valores correspondientes al Proveedor:
                                    proveedorModificado.Element("Nombre").Value = oBEProveedor.Nombre.Trim();
                                    proveedorModificado.Element("Domicilio").Value = oBEProveedor.Domicilio.Trim();
                                    proveedorModificado.Element("Email").Value = oBEProveedor.Email.Trim();
                                    proveedorModificado.Element("Telefono").Value = oBEProveedor.Telefono.Trim();
                                }
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se pudo recuperar los datos del Proveedor con el Id brindado!"); }
                        }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar los datos del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public BEProveedor ListarObjeto(BEProveedor oBEProveedor)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Recupero la información del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Busco el Proveedor:
                        var buscarProveedor = from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                              where proveedor.Element("CUIL").Value.Trim() == oBEProveedor.CUIL.ToString().Trim() ||
                                              proveedor.Attribute("Id").Value.Trim() == oBEProveedor.Id.ToString().Trim()
                                              select proveedor;
                        //Verifico que exista el proveedor:
                        if (buscarProveedor.Any())
                        {
                            var proveedorEncontrado = buscarProveedor.First();
                            //Si se encontró el Proveedor recupero la información:
                            if (proveedorEncontrado != null)
                            {
                                var listaProductos = proveedorEncontrado.Element("Productos")?.Descendants("producto");
                                if (listaProductos == null || !listaProductos.Any())
                                {
                                    BEProveedor proveedorSinProductos = new BEProveedor
                                    {
                                        Id = int.Parse(proveedorEncontrado.Attribute("Id").Value.Trim()),
                                        Nombre = proveedorEncontrado.Element("Nombre").Value.Trim(),
                                        CUIL = long.Parse(proveedorEncontrado.Element("CUIL").Value.Trim()),
                                        Domicilio = proveedorEncontrado.Element("Domicilio").Value.Trim(),
                                        Email = proveedorEncontrado.Element("Email").Value.Trim(),
                                        Telefono = proveedorEncontrado.Element("Telefono").Value.Trim(),
                                        //listaProductos = new List<BEProducto>()
                                    };
                                    return proveedorSinProductos;
                                }
                                //En caso de que tenga productos Asociado:
                                else
                                {
                                    BEProveedor proveedorConProductos = new BEProveedor
                                    {
                                        Id = int.Parse(proveedorEncontrado.Attribute("Id").Value.Trim()),
                                        Nombre = proveedorEncontrado.Element("Nombre").Value.Trim(),
                                        CUIL = long.Parse(proveedorEncontrado.Element("CUIL").Value.Trim()),
                                        Domicilio = proveedorEncontrado.Element("Domicilio").Value.Trim(),
                                        Email = proveedorEncontrado.Element("Email").Value.Trim(),
                                        Telefono = proveedorEncontrado.Element("Telefono").Value.Trim(),
                                        //listaProductos = new List<BEProducto>()
                                    };
                                    foreach (var producto in listaProductos)
                                    {
                                        var buscarProductoGlobal = from productoGlobal in BDXML.Root.Element("Productos").Descendants("producto")
                                                                   where productoGlobal.Attribute("Id").Value.Trim() == producto.Attribute("Id").Value.Trim()
                                                                   select productoGlobal;
                                        if (buscarProductoGlobal.Any())
                                        {
                                            var productoGlobalEncontrado = buscarProductoGlobal.First();
                                            var stockId = productoGlobalEncontrado.Element("stock_producto").Attribute("Id").Value.Trim();
                                            var precioId = productoGlobalEncontrado.Element("precio_producto").Attribute("Id").Value.Trim();
                                            //Busco el stock del producto:
                                            var stockProductoGlobal = from stockProducto in BDXML.Root.Element("Stocks_Productos").Descendants("stock_producto")
                                                                      where stockProducto.Attribute("Id").Value.Trim() == stockId
                                                                      select stockProducto;
                                            //Busco el precio del producto:
                                            var precioProductoGlobal = from precioProducto in BDXML.Root.Element("Precios_Productos").Descendants("precio_producto")
                                                                       where precioProducto.Attribute("Id").Value.Trim() == precioId
                                                                       select precioProducto;
                                            //if (stockProductoGlobal.Any() && precioProductoGlobal.Any())
                                            //{
                                            //    var stockProductoEncontrado = stockProductoGlobal.First();
                                            //    var precioProductoEncontrado = precioProductoGlobal.First();
                                            //    BEProducto oBEProducto = new BEProducto
                                            //    {
                                            //        Id = long.Parse(productoGlobalEncontrado.Attribute("Id").Value.Trim()),
                                            //        Nombre = productoGlobalEncontrado.Element("Nombre").Value.Trim(),
                                            //        Marca = productoGlobalEncontrado.Element("Marca").Value.Trim(),
                                            //        Anio = Convert.ToInt32(productoGlobalEncontrado.Element("Anio").Value.Trim()),
                                            //        Modelo = Convert.ToInt32(productoGlobalEncontrado.Element("Modelo").Value.Trim()),
                                            //        oBEPrecioProductoProveedor = new BEPrecioProductoProveedor
                                            //        (
                                            //            long.Parse(precioProductoEncontrado.Attribute("Id").Value.Trim()),
                                            //            double.Parse(precioProductoEncontrado.Element("Precio").Value.Trim())
                                            //        ),
                                            //        oBEStockProductoProveedor = new BEStockProductoProveedor
                                            //        (
                                            //            long.Parse(stockProductoEncontrado.Attribute("Id").Value.Trim()),
                                            //            Convert.ToInt32(stockProductoEncontrado.Element("Cantidad").Value.Trim())
                                            //        )
                                            //    };
                                            //    proveedorConProductos.listaProductos.Add(oBEProducto);
                                            //}
                                        }
                                    }
                                    return proveedorConProductos;
                                }
                            }
                            else { throw new Exception("Error: No existe el Proveedor con el Id brindado!"); }
                        }
                        else { throw new Exception("Error: No se encontró el Proveedor con el CUIL brindado!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BEProveedor> ListarTodo()
        {
            try
            {
                //Verifico que el XML exista:
                if (CrearXML() == true)
                {
                    //Recupero la información del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Recupero la lista global de productos:
                        //var productosGlobales = BDXML.Root.Element("Productos").Descendants("producto")
                        //    .ToDictionary(
                        //        p => int.Parse(p.Attribute("Id").Value.Trim()),
                        //        p => new BEProducto
                        //        {
                        //            Id = int.Parse(p.Attribute("Id").Value.Trim()),
                        //            Nombre = p.Element("Nombre").Value.Trim(),
                        //            Marca = p.Element("Marca").Value.Trim(),
                        //            Modelo = Convert.ToInt32(p.Element("Modelo").Value.Trim()),
                        //            Anio = Convert.ToInt32(p.Element("Anio").Value.Trim())
                        //        }
                        //    );
                        //Recupero la lista de proveedores y sus productos asociados:
                        var listaProveedores = (from proveedor in BDXML.Root.Element("Proveedores").Descendants("proveedor")
                                                select new BEProveedor
                                                {
                                                    Id = int.Parse(proveedor.Attribute("Id").Value.Trim()),
                                                    Nombre = proveedor.Element("Nombre").Value.Trim(),
                                                    CUIL = long.Parse(proveedor.Element("CUIL").Value.Trim()),
                                                    Domicilio = proveedor.Element("Domicilio").Value.Trim(),
                                                    Email = proveedor.Element("Email").Value.Trim(),
                                                    Telefono = proveedor.Element("Telefono").Value.Trim(),
                                                    //listaProductos = (proveedor.Element("Productos") != null
                                                    //    ? (from producto in proveedor.Element("Productos").Elements("producto")
                                                    //       select new BEProducto
                                                    //       {
                                                    //           Id = long.Parse(producto.Attribute("Id").Value.Trim())
                                                    //       }).ToList()
                                                    //    : new List<BEProducto>())
                                                }).ToList();
                        //Combino los datos de listaProductos con los detalles de productosGlobales:
                        foreach (var proveedor in listaProveedores)
                        {
                            //var listaProductosCombinados = new List<BEProducto>();
                            //foreach (var producto in proveedor.listaProductos)
                            //{
                            //    if (productosGlobales.TryGetValue(producto.Id, out BEProducto productoGlobal))
                            //    {
                            //        listaProductosCombinados.Add(new BEProducto
                            //        {
                            //            Id = producto.Id,
                            //            Nombre = productoGlobal.Nombre,
                            //            Marca = productoGlobal.Marca,
                            //            Modelo = productoGlobal.Modelo,
                            //            Anio = productoGlobal.Anio
                            //        });
                            //    }
                            //}
                            //proveedor.listaProductos = listaProductosCombinados;
                        }
                        return listaProveedores;
                    }
                    else { throw new Exception("Error: No se pudo recuperar los datos del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
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


        public bool CrearXMLStockProductoProveedor()
        {
            try
            {
                //Verifico que exista el XML:
                if (!(File.Exists(ruta)))
                {
                    //Si no existe, lo creo:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Stocks_Productos_Proveedores")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Stocks_Productos_Proveedores:
                    BDXML = XDocument.Load(ruta);
                    XElement stockProductoProveedor = BDXML.Root.Element("Stocks_Productos_Proveedores");
                    //Si existe, devuelvo true:
                    if (stockProductoProveedor != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        stockProductoProveedor = new XElement("Stocks_Productos_Proveedores");
                        BDXML.Root.Add(stockProductoProveedor);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool CrearXMLPrecioProductoProveedor()
        {
            try
            {
                //Verifico que exista el XML:
                if (!(File.Exists(ruta)))
                {
                    //Si no existe, lo creo:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Precios_Productos_Proveedores")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Precio_Producto_Proveedor:
                    BDXML = XDocument.Load(ruta);
                    XElement precioProductoProveedor = BDXML.Root.Element("Precios_Productos_Proveedores");
                    //Si existe, devuelvo true:
                    if (precioProductoProveedor != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        precioProductoProveedor = new XElement("Precios_Productos_Proveedores");
                        BDXML.Root.Add(precioProductoProveedor);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

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
        //                                var productoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoProveedor = productoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                // Si el Stock del Producto del proveedor no existe en el Producto lo asocio:
        //                                if (stockProductoProveedor == null)
        //                                {
        //                                    productoGlobalEncontrado.Add(new XElement("stock_producto_proveedor",
        //                                        new XAttribute("Id", oBEStockProductoProveedor.Id.ToString().Trim())));
        //                                    BDXML.Save(ruta);
        //                                }
        //                                else { throw new Exception("Error: El Producto del Proveedor ya se encuentra asociado al Proveedor!"); }
        //                                // Si el Precio del Producto del proveedor no existe en el Producto lo asocio:
        //                                var precioProductoProveedor = productoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (precioProductoProveedor == null)
        //                                {
        //                                    productoGlobalEncontrado.Add(new XElement("precio_producto_proveedor",
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
        //                                var productoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoGlobalProveedor = productoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                var precioProductoGlobalProveedor = productoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (productoGlobalEncontrado != null && stockProductoGlobalProveedor != null && precioProductoGlobalProveedor != null)
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
        //                                var productoGlobalEncontrado = buscarProductoGlobal.First();
        //                                var stockProductoProveedor = productoGlobalEncontrado.Element("stock_producto_proveedor");
        //                                var precioProductoProveedor = productoGlobalEncontrado.Element("precio_producto_proveedor");
        //                                if (stockProductoProveedor != null && precioProductoProveedor != null)
        //                                {
        //                                    var stockId = productoGlobalEncontrado.Element("stock_producto_proveedor")?.Attribute("Id")?.Value.Trim();
        //                                    var precioId = productoGlobalEncontrado.Element("precio_producto_proveedor")?.Attribute("Id")?.Value.Trim();

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
        //                                            Id = long.Parse(productoGlobalEncontrado.Attribute("Id").Value.Trim()),
        //                                            Nombre = productoGlobalEncontrado.Element("Nombre").Value.Trim(),
        //                                            Marca = productoGlobalEncontrado.Element("Marca").Value.Trim(),
        //                                            Anio = Convert.ToInt32(productoGlobalEncontrado.Element("Anio").Value.Trim()),
        //                                            Modelo = Convert.ToInt32(productoGlobalEncontrado.Element("Modelo").Value.Trim()),
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
