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
    public class MPPBitacora
    {
        readonly string ruta = ("Bitacora.Xml");
        XDocument BDXML;

       

        public bool CrearXML()
        {
            try
            {
                //Verifico si existe el XML:
                if (!File.Exists(ruta))
                {
                    //Si no Existe, lo creo y agrego el Element Bitacora:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Bitacoras")));
                    BDXML.Save(ruta);
                    return true;
                }
                //En caso de que Exista el XML:
                else
                {
                    BDXML = XDocument.Load(ruta);
                    //Verifico que el XML se cargo correctamente:
                    if (BDXML != null)
                    {
                        XElement bitacoras = BDXML.Root.Element("Bitacoras");
                        //Si existe el Element Bitacoras devuelvo true:
                        if (bitacoras != null) { return true; }
                        //Si no creo el Elemento:
                        else
                        {
                            bitacoras = new XElement("Bitacoras");
                            BDXML.Root.Add(bitacoras);
                            BDXML.Save(ruta);
                            return true;
                        }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Eliminar(BEBitacora objeto) { throw new NotImplementedException(); }

        public bool Guardar(BEBitacora oBEBitacora)
        {
            try
            {
                if (CrearXML() == true)
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEBitacora != null)
                        {
                            if (oBEBitacora.Id == 0)
                            {
                                if (VerificarExistenciaObjeto(oBEBitacora) == false)
                                {
                                    int pId = ObtenerUltimoId() + 1;
                                    oBEBitacora.Id = pId;
                                    BDXML.Root.Element("Bitacoras").Add(new XElement("bitacora",
                                        new XAttribute("Id", oBEBitacora.Id.ToString().Trim()),
                                        new XElement("Detalle", oBEBitacora.Detalle.Trim()),
                                        new XElement("Fecha_Registro", oBEBitacora.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss").Trim()),
                                        new XElement("usuario",
                                        new XAttribute("Id", oBEBitacora.oBEUsuario.Id.ToString().Trim()))));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Eror: No se puede dar el alta a una Bitacora que ya existe!"); }
                            }
                            else { throw new Exception("Error: Ya existe la Bitacora!"); }
                        }
                        else { throw new Exception("Error: No se pudo obtener la información de la Bitacora!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public BEBitacora ListarObjeto(BEBitacora objeto)
        {
            throw new NotImplementedException();
        }

        public List<BEBitacora> ListarTodo()
        {
            try
            {
                if (CrearXML() == true)
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        List<BEBitacora> listaBitacoras = new List<BEBitacora>();
                        var buscarBitacorias = from bitacora in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                               select new BEBitacora
                                               {
                                                   Id = int.Parse(bitacora.Attribute("Id").Value.Trim()),
                                                   FechaRegistro = DateTime.Parse(bitacora.Element("Fecha_Registro").Value.Trim()),
                                                   Detalle = bitacora.Element("Detalle").Value.Trim(),
                                                   oBEUsuario = new BEUsuario
                                                   {
                                                       Id = int.Parse(bitacora.Element("usuario").Attribute("Id").Value.Trim()),
                                                   }
                                               };
                        if (buscarBitacorias != null)
                        {
                            listaBitacoras = buscarBitacorias.ToList();
                            return listaBitacoras;
                        }
                        else { return listaBitacoras = null; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BEBitacora> ListarTodoPorTipo(bool pTipo)
        {
            try
            {
                //Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Si pTipo es True, devuelvo solo backups
                        if (pTipo == true)
                        {
                            //Creo una nueva lista de Bitacoras:
                            List<BEBitacora> listaBitacoras = new List<BEBitacora>();
                            var buscarBitacoriasBackups = from bitacora in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                                          where bitacora.Element("Detalle").Value == "backup"
                                                          select new BEBitacora
                                                          {
                                                              Id = int.Parse(bitacora.Attribute("Id").Value.Trim()),
                                                              FechaRegistro = DateTime.Parse(bitacora.Element("Fecha_Registro").Value.Trim()),
                                                              Detalle = bitacora.Element("Detalle").Value.Trim(),
                                                              oBEUsuario = new BEUsuario
                                                              {
                                                                  Id = int.Parse(bitacora.Element("usuario").Attribute("Id").Value.Trim()),
                                                              }
                                                          };
                            //Si encuentra al menos un Backup:
                            if (buscarBitacoriasBackups != null)
                            {
                                listaBitacoras = buscarBitacoriasBackups.ToList();
                                return listaBitacoras;
                            }
                            //Si no, devuelvo una lista nula:
                            else { return listaBitacoras = null; }
                        }
                        //Si es False, devuelvo solo Restores:
                        else
                        {
                            List<BEBitacora> listaBitacoras = new List<BEBitacora>();
                            var buscarBitacoriasRestores = from bitacora in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                                           where bitacora.Element("Detalle").Value == "restore"
                                                           select new BEBitacora
                                                           {
                                                               Id = int.Parse(bitacora.Attribute("Id").Value.Trim()),
                                                               FechaRegistro = DateTime.Parse(bitacora.Element("Fecha_Registro").Value.Trim()),
                                                               Detalle = bitacora.Element("Detalle").Value.Trim(),
                                                               oBEUsuario = new BEUsuario
                                                               {
                                                                   Id = int.Parse(bitacora.Element("usuario").Attribute("Id").Value.Trim()),
                                                               }
                                                           };
                            //Si encuentra al menos un Backup:
                            if (buscarBitacoriasRestores != null)
                            {
                                listaBitacoras = buscarBitacoriasRestores.ToList();
                                return listaBitacoras;
                            }
                            //Si no, devuelvo una lista nula:
                            else { return listaBitacoras = null; }
                        }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public int ObtenerUltimoId()
        {
            try
            {
                //Verifico la Existencia del XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //obtengo los Ids:
                        var pId = from bitacora in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                  select int.Parse(bitacora.Attribute("Id").Value.Trim());
                        //Verifico si encontro al menos Id:
                        if (pId.Any())
                        {
                            //Asigno el Id con el valor máximo:
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no existe ningun Id, devuelvo 0:
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

        public bool VerificarExistenciaObjeto(BEBitacora oBEBitacora)
        {
            try
            {
                //Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que la bitacora no sea nula:
                        if (oBEBitacora != null)
                        {
                            //Busco si se cumple la condición de que haya una bitacora con la misma fecha de registro:
                            var buscarBitacora = from bitacora in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                                 where bitacora.Element("Fecha_Registro").Value.Trim() == oBEBitacora.FechaRegistro.ToString().Trim()
                                                 select bitacora;
                            //Si encontró al menos una devuelvo True:
                            if (buscarBitacora.Count() > 0) { return true; }
                            //Si no encontró devuelvo False:
                            else { return false; }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información de la Bitacora brindada!"); }
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
