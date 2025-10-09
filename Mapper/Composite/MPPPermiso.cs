using Entity.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Mapper.Composite
{
    public class MPPPermiso 
    {

        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        #region "Métodos Permiso"
        public bool CrearXML()
        {
            try
            {
                //Verifico que exista el XML:
                if (!(File.Exists(ruta)))
                {
                    //Si no existe, lo creo:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Permisos")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Permisos
                    BDXML = XDocument.Load(ruta);
                    XElement permisos = BDXML.Root.Element("Permisos");
                    //Si existe, devuelvo true:
                    if (permisos != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        permisos = new XElement("Permisos");
                        BDXML.Root.Add(permisos);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Eliminar(BEPermiso oBEPermiso)
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
                        //Verifico que el Permiso no sea nulo:
                        if (oBEPermiso != null)
                        {
                            //Busco el Permiso:
                            var buscarPermiso = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                where permiso.Attribute("Id").Value == oBEPermiso.Id.ToString().Trim()
                                                select permiso;
                            //Verifico que lo encuentre:
                            if (buscarPermiso.Any())
                            {
                                var permisoEncontrado = buscarPermiso.First();
                                if (permisoEncontrado != null)
                                {
                                    var buscarPermisoUsuario = from permisoUsuarios in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                               where permisoUsuarios.Element("Id_Permiso").Value.Trim() == permisoEncontrado.Attribute("Id").Value.ToString().Trim()
                                                               select permisoUsuarios;
                                    if (!buscarPermisoUsuario.Any())
                                    {
                                        var buscarPermisoEnRoles = from permisoRoles in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                                   where permisoRoles.Element("Id_Permiso").Value.Trim() == permisoEncontrado.Attribute("Id").Value.Trim()
                                                                   select permisoRoles;
                                        if (!buscarPermisoEnRoles.Any())
                                        {
                                            permisoEncontrado.Remove();
                                            BDXML.Save(ruta);
                                            return true;
                                        }
                                        else
                                        {
                                            var elementosParaEliminar = buscarPermisoEnRoles.ToList();
                                            foreach (var permisoEnRol in elementosParaEliminar)
                                            {
                                                permisoEnRol.Remove();
                                            }
                                            permisoEncontrado.Remove();
                                            BDXML.Save(ruta);
                                            return true;
                                        }
                                    }
                                    else { throw new Exception("Error: No se puede Eliminar un Permiso que esta Asociado directamente al menos a un Usuario!"); }
                                }
                                else { throw new Exception("Error: No se pudo recuperar el Permiso para eliminarlo!"); }
                            }
                            else { throw new Exception("Error: No se encontro el Permiso indicado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar los datos del Permiso brindado!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("ERror: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Guardar(BEPermiso oBEPermiso)
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
                        //Verifico que el permiso no sea nulo:
                        if (oBEPermiso != null)
                        {
                            //Verifico que no exista el Permiso:
                            if (VerificarExistenciaObjeto(oBEPermiso) == false)
                            {
                                //En caso de un nuevo Permiso:
                                if (oBEPermiso.Id == 0)
                                {
                                    //Obtengo el Id Máximo:
                                    int pId = ObtenerUltimoId() + 1;
                                    oBEPermiso.Id = pId;
                                    BDXML.Root.Element("Permisos").Add(new XElement("permiso",
                                        new XAttribute("Id", oBEPermiso.Id.ToString().Trim()),
                                        new XElement("Nombre", oBEPermiso.Nombre.Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: EL Permiso ya existe!"); }
                            }
                            // En caso de que se tenga que modificar:
                            else
                            {
                                if (VerificarExistenciaObjeto(oBEPermiso) == false)
                                {
                                    //Busco al permiso:
                                    var buscarPermiso = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                        where permiso.Attribute("Id").Value == oBEPermiso.Id.ToString().Trim()
                                                        select permiso;
                                    if (buscarPermiso.Any())
                                    {
                                        foreach (XElement permisoEncontrado in buscarPermiso)
                                        {
                                            permisoEncontrado.Element("Nombre").Value = oBEPermiso.Nombre.Trim();
                                        }
                                        BDXML.Save(ruta);
                                        return true;
                                    }
                                    else { throw new Exception("Error: No se encontró el permiso indicado!"); }
                                }
                                else { throw new Exception("Error: El Nombre del Perfil ya existe!"); }
                            }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información que brindó del Permiso!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public BEPermiso ListarObjeto(BEPermiso oBEPermiso)
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
                        //Verifico que el Permiso no sea nulo:
                        if (oBEPermiso != null)
                        {
                            var buscarPermiso = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                where permiso.Element("Nombre").Value == oBEPermiso.Nombre.Trim()
                                                select permiso;
                            if (buscarPermiso.Any())
                            {
                                var permisoEncontrado = buscarPermiso.First();
                                if (permisoEncontrado != null)
                                {
                                    BEPermiso permiso = new BEPermiso(
                                        int.Parse(permisoEncontrado.Attribute("Id").Value),
                                        permisoEncontrado.Element("Nombre").Value.Trim());
                                    return permiso;
                                }
                                else { throw new Exception("Error: Al intentar obtener la información del Permiso!"); }
                            }
                            else { throw new Exception("Error: No se pudo encontrar al Permiso indicado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Permiso indicado!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BEPermiso> ListarTodo()
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
                        List<BEPermiso> listaPermiso = new List<BEPermiso>();
                        var lista = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                    select new BEPermiso(
                                        int.Parse(permiso.Attribute("Id").Value),
                                        permiso.Element("Nombre").Value
                                        );
                        if (lista != null)
                        {
                            listaPermiso = lista.ToList();
                            return listaPermiso;
                        }
                        else { return listaPermiso = null; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public int ObtenerUltimoId()
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
                        var pId = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                  select int.Parse(permiso.Attribute("Id").Value);
                        if (pId.Any())
                        {
                            //Retorna el Id Máximo que Encuentra en el XML:
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no existe ninguna Persona, devuelve 0:
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

        public bool VerificarExistenciaObjeto(BEPermiso oBEPermiso)
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
                        //Verifico que el Permiso no sea nulo:
                        if (oBEPermiso != null)
                        {
                            var buscarPermiso = from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                where permiso.Element("Nombre").Value == oBEPermiso.Nombre.Trim()
                                                select permiso;
                            if (buscarPermiso.Count() > 0) { return true; }
                            else { return false; }
                        }
                        else { throw new Exception("Error: No se puede brindar un Permiso nulo!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        #endregion
    }
}
