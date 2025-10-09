using Entity.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Entity;

namespace Mapper.Composite
{
    public class MPPRol 
    {

        readonly string ruta = ("BD.Xml");
        XDocument BDXML;

        #region "Métodos"

        #region "Métodos Rol"

        public bool CrearXML()
        {
            try
            {
                //Si no existe el XML lo creo::
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Roles")));
                    BDXML.Save(ruta);
                    return true;
                }
                //Si existe verifico que exista el elemento Roles:
                else
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        XElement roles = BDXML.Root.Element("Roles");
                        //verifico si existe el Elemento Roles:
                        if (roles == null)
                        {
                            roles = new XElement("Roles");
                            BDXML.Root.Add(roles);
                            BDXML.Save(ruta);
                            return true;
                        }
                        //Si existe devuelvo true:
                        else { return true; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Eliminar(BERol oBERol)
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
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //Busco al Rol:
                            var buscarRol = from rol in BDXML.Root.Element("Roles").Descendants("rol")
                                            where rol.Attribute("Id").Value.Trim() == oBERol.Id.ToString().Trim()
                                            select rol;
                            if (buscarRol.Any())
                            {
                                var rolEncontrado = buscarRol.First();
                                if (rolEncontrado != null)
                                {
                                    var buscarRolEnUsuario = from rolUsuarios in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                             where rolUsuarios.Element("Id_Rol_Padre").Value.Trim() == rolEncontrado.Attribute("Id").Value.Trim()
                                                             select rolUsuarios;
                                    if (!buscarRolEnUsuario.Any())
                                    {
                                        var buscarPermisosEnRol = from permisosRol in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                                  where permisosRol.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                                  select permisosRol;
                                        if (buscarPermisosEnRol.Any())
                                        {
                                            foreach (var permiso in buscarPermisosEnRol)
                                            {
                                                permiso.Remove();
                                                break;
                                            }
                                            rolEncontrado.Remove();
                                            BDXML.Save(ruta);
                                            return true;
                                        }
                                        else
                                        {
                                            rolEncontrado.Remove();
                                            BDXML.Save(ruta);
                                            return true;
                                        }
                                    }
                                    else { throw new Exception("Error: No se puede Eliminar un Rol que está asociado al menos a un Usuario!"); }
                                }
                                else { throw new Exception("Error: No se pudo recuperar la información del Rol especificado!"); }
                            }
                            else { throw new Exception("Error: No se encontró el Rol indicado!"); }
                        }
                        else { throw new Exception("Error: No se puede brindar un Rol nulo!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Guardar(BERol oBERol)
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
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //En caso de que sea un nuevo Rol:
                            if (oBERol.Id == 0)
                            {
                                //Verifico que no exista el Rol:
                                if (VerificarExistenciaObjeto(oBERol) == false)
                                {
                                    //Asigno un nuevo Id:
                                    int pId = ObtenerUltimoId() + 1;
                                    oBERol.Id = pId;
                                    BDXML.Root.Element("Roles").Add(new XElement("rol",
                                        new XAttribute("Id", oBERol.Id.ToString().Trim()),
                                        new XElement("Nombre", oBERol.Nombre.Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se puede dar el alta una Rol que ya existe!"); }
                            }
                            //En caso de que exista, se modifica:
                            else
                            {
                                if (VerificarExistenciaObjeto(oBERol) == false)
                                {
                                    //Busco al permiso:
                                    var buscarRol = from rol in BDXML.Root.Element("Roles").Descendants("rol")
                                                    where rol.Attribute("Id").Value.Trim() == oBERol.Id.ToString().Trim()
                                                    select rol;
                                    if (buscarRol.Any())
                                    {

                                        foreach (XElement rolEncontrado in buscarRol)
                                        {
                                            rolEncontrado.Element("Nombre").Value = oBERol.Nombre.Trim();
                                        }
                                        BDXML.Save(ruta);
                                        return true;
                                    }
                                    else { throw new Exception("No se encontro el Rol indicado!"); }
                                }
                                else { throw new Exception("Error: El nombre del Rol ya existe!"); }
                            }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Rol!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public BERol ListarObjeto(BERol oBERol)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    if (CrearXMLRolPermiso() == true && CrearXMLRolaRol() == true)
                    {
                        //Verifico que se cargue la info del XML:
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            //Verifico que Rol no sea nulo:
                            if (oBERol != null)
                            {
                                //Busco al rol:
                                var buscarRol = from rol in BDXML.Root.Element("Roles").Descendants("rol")
                                                where rol.Element("Nombre").Value == oBERol.Nombre.Trim()
                                                select rol;
                                if (buscarRol.Any())
                                {
                                    var rolEncontrado = buscarRol.First();
                                    if (rolEncontrado != null)
                                    {
                                        //Creo un nuevo objeto BERol con los datos encontrados:
                                        BERol rol = new BERol(
                                            int.Parse(rolEncontrado.Attribute("Id").Value),
                                            rolEncontrado.Element("Nombre").Value.Trim()
                                        );

                                        //Obtengo los roles categoría 2 asociados al rol categoría 1:
                                        var rolesEnRol = from rolRol in BDXML.Root.Element("Roles_Roles").Descendants("rol_rol")
                                                         where rolRol.Element("Id_Rol").Value.Trim() == rolEncontrado.Attribute("Id").Value.Trim()
                                                         select rolRol;
                                        if (rolesEnRol.Any())
                                        {
                                            foreach (var rol2 in rolesEnRol)
                                            {
                                                var buscarRol2 = (from rolGeneral2 in BDXML.Root.Element("Roles").Descendants("rol")
                                                                  where rolGeneral2.Attribute("Id").Value.Trim() == rol2.Element("Id_Rol_2").Value.Trim()
                                                                  select rolGeneral2).First();
                                                if (buscarRol2 != null)
                                                {
                                                    BERol rolEnRol2 = new BERol
                                                    (
                                                        int.Parse(buscarRol2.Attribute("Id").Value),
                                                        buscarRol2.Element("Nombre").Value.Trim()
                                                    );

                                                    var permisosRol2 = from permisosEnRol in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                                       where permisosEnRol.Element("Id_Rol").Value == buscarRol2.Attribute("Id").Value
                                                                       select permisosEnRol.Element("Id_Permiso").Value;
                                                    foreach (var permisoId in permisosRol2)
                                                    {
                                                        var permisoEnRol1 = (from permisoEnRol in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                                             where permisoEnRol.Element("Id_Rol").Value == rolEncontrado.Attribute("Id").Value
                                                                             && permisoEnRol.Element("Id_Permiso").Value == permisoId
                                                                             select permisoEnRol).FirstOrDefault();

                                                        if (permisoEnRol1 != null)
                                                        {
                                                            var permisoNombre = (from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                                 where permiso.Attribute("Id").Value == permisoId
                                                                                 select permiso.Element("Nombre").Value).FirstOrDefault();
                                                            if (permisoNombre != null)
                                                            {
                                                                BEPermiso oBEPermiso = new BEPermiso(int.Parse(permisoId), permisoNombre);
                                                                rolEnRol2.Agregar(oBEPermiso);
                                                            }
                                                        }
                                                    }
                                                    rol.listaPermisos.Add(rolEnRol2);
                                                }
                                            }
                                        }
                                        var buscarPermisosEnRol = from permisosRol in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                                  where permisosRol.Element("Id_Rol").Value.Trim() == rolEncontrado.Attribute("Id").Value.Trim()
                                                                  select permisosRol;
                                        if (buscarPermisosEnRol.Any())
                                        {
                                            foreach (var permisoEncontrado in buscarPermisosEnRol)
                                            {
                                                var permisoNombre = (from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                     where permiso.Attribute("Id").Value == permisoEncontrado.Element("Id_Permiso").Value.Trim()
                                                                     select permiso.Element("Nombre").Value).FirstOrDefault();
                                                if (permisoNombre != null)
                                                {
                                                    BEPermiso permiso = new BEPermiso(int.Parse(permisoEncontrado.Element("Id_Permiso").Value.Trim()), permisoNombre);
                                                    rol.listaPermisos.Add(permiso);
                                                }
                                            }
                                            return rol;
                                        }
                                        else { return rol; }
                                    }
                                    else { throw new Exception("Error: No se pudo mostrar el Rol indicado!"); }
                                }
                                else { throw new Exception("Error: No se encontró el rol indicado!"); }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Rol!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información de Rol_Permisos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BERol> ListarTodo()
        {
            try
            {
                //Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    //Verifico la existencia del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Creo una nueva lista de roles:
                        List<BERol> listaRoles = new List<BERol>();
                        //Busco a los roles:
                        var lista = from roles in BDXML.Root.Element("Roles").Elements("rol")
                                    select new BERol
                                    (
                                        int.Parse(roles.Attribute("Id").Value),
                                        roles.Element("Nombre").Value
                                    );
                        listaRoles = lista.ToList();
                        return listaRoles;
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
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pId = from rol in BDXML.Root.Element("Roles").Descendants("rol")
                                  select int.Parse(rol.Attribute("Id").Value);
                        //Verifico si encontro algun Id:
                        if (pId.Any())
                        {
                            int ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no encontro, devuelvo 0:
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

        public bool VerificarExistenciaObjeto(BERol oBERol)
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
                        var buscarRol = from rol in BDXML.Root.Element("Roles").Descendants("rol")
                                        where rol.Element("Nombre").Value == oBERol.Nombre.Trim()
                                        select rol;
                        //Si encontro al menos un rol con el nombre:
                        if (buscarRol.Count() > 0) { return true; }
                        //Si no encontro devuelvo false:
                        else { return false; }
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

        #region "Métodos Rol-Rol"

        public bool CrearXMLRolaRol()
        {
            try
            {
                //Si no existe el XML lo creo::
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Roles_Roles")));
                    BDXML.Save(ruta);
                    return true;
                }
                //Si existe verifico que exista el elemento Roles:
                else
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        XElement roles = BDXML.Root.Element("Roles_Roles");
                        //verifico si existe el Elemento Roles:
                        if (roles == null)
                        {
                            roles = new XElement("Roles_Roles");
                            BDXML.Root.Add(roles);
                            BDXML.Save(ruta);
                            return true;
                        }
                        //Si existe devuelvo true:
                        else { return true; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificarExistenciaRolEnRol(BERol oBERol, BERol oBERol2)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXMLRolaRol() == true && CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //Verifico que el Permiso no sea nulo:
                            if (oBERol2 != null)
                            {
                                var buscarRolenRol = from rolRol in BDXML.Root.Element("Roles_Roles").Descendants("rol_rol")
                                                     where rolRol.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                     && rolRol.Element("Id_Rol_2").Value.Trim() == oBERol2.Id.ToString().Trim()
                                                     select rolRol;
                                if (buscarRolenRol.Count() > 0) { return true; }
                                else { return false; }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Rol brindado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Rol brindado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool AsociarRolaRol(BERol oBERol, BERol oBERol2)
        {
            try
            {
                if (CrearXML() == true && CrearXMLRolaRol() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBERol != null && oBERol2 != null)
                        {
                            //Verifico que no existe la asociacion entre los roles:
                            if (VerificarExistenciaRolEnRol(oBERol, oBERol2) == false)
                            {
                                long pId = ObtenerUltimoId() + 1;
                                BDXML.Root.Element("Roles_Roles").Add(new XElement("rol_rol",
                                    new XAttribute("Id", pId.ToString().Trim()),
                                    new XElement("Id_Rol", oBERol.Id.ToString().Trim()),
                                    new XElement("Id_Rol_2", oBERol2.Id.ToString().Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: Los Roles ya Estan Asociados!"); }
                        }
                        else { throw new Exception("Error: No puede brindar roles nulos!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        #endregion

        #region "Métodos Rol-Permisos"
        public bool CrearXMLRolPermiso()
        {
            try
            {
                //Si no existe el XML lo creo:
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Rol_Permisos")));
                    BDXML.Save(ruta);
                    return true;
                }
                //Si existe verifico que exista el elemento Rol_Permiso:
                else
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        XElement rolesPermisos = BDXML.Root.Element("Rol_Permisos");
                        //verifico si existe el Elemento Rol_Permiso:
                        if (rolesPermisos == null)
                        {
                            rolesPermisos = new XElement("Rol_Permisos");
                            BDXML.Root.Add(rolesPermisos);
                            BDXML.Save(ruta);
                            return true;
                        }
                        //Si existe devuelvo true:
                        else { return true; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificarExistenciaPermisoEnRol(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXMLRolPermiso() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //Verifico que el Permiso no sea nulo:
                            if (oBEPermiso != null)
                            {
                                var buscarRolPermiso = from rolPermiso in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                       where rolPermiso.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                       && rolPermiso.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                                       select rolPermiso;
                                if (buscarRolPermiso.Count() > 0) { return true; }
                                else { return false; }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Permiso brindado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Rol brindado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public long ObtenerUltimoIdRolRol()
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true && CrearXMLRolaRol() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pId = from rol in BDXML.Root.Element("Roles_Roles").Descendants("rol_rol")
                                  select int.Parse(rol.Attribute("Id").Value);
                        //Verifico si encontro algun Id:
                        if (pId.Any())
                        {
                            long ultimoId = pId.Max();
                            return ultimoId;
                        }
                        //Si no encontro, devuelvo 0:
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

        public bool AsociarRolaPermiso(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXMLRolPermiso() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //Verifico que el Permiso no sea nulo:
                            if (oBEPermiso != null)
                            {
                                if (VerificarExistenciaPermisoEnRol(oBERol, oBEPermiso) == false)
                                {
                                    long pId = ObtenerUltimoIdRolRol() + 1;
                                    BDXML.Root.Element("Rol_Permisos").Add(new XElement("rol_permiso",
                                        new XAttribute("Id", pId),
                                        new XElement("Id_Rol", oBERol.Id.ToString().Trim()),
                                        new XElement("Id_Permiso", oBEPermiso.Id.ToString().Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: Ya esta asociado el Permiso al Rol indicado!"); }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Permiso brindado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Rol brindado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool DesasociarRolaPermiso(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXMLRolPermiso() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Rol no sea nulo:
                        if (oBERol != null)
                        {
                            //Verifico que el Permiso no sea nulo:
                            if (oBEPermiso != null)
                            {

                                var buscarRolPermiso = from rolPermiso in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                       where rolPermiso.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                       && rolPermiso.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                                       select rolPermiso;
                                if (buscarRolPermiso.Any())
                                {
                                    var rolPermisoEncontrado = buscarRolPermiso.First();
                                    rolPermisoEncontrado.Remove();
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se pudo encontrar el Permiso en el Rol indicado!"); }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Permiso brindado!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Rol brindado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<BEPermiso> ListarPermisosDeRolPorUsuario(BEUsuario oBEUsuario)
        {
            try
            {
                //Verifico la existencia del XML:
                if (CrearXMLRolPermiso() == true && CrearXML() == true)
                {
                    //Verifico la carga del info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        List<BEPermiso> listaPermisos = new List<BEPermiso>();
                        BDXML = XDocument.Load(ruta);
                        var permisosRol = from permisosEnRol in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                          where permisosEnRol.Element("Id_Rol").Value == oBEUsuario.Id.ToString()
                                          select permisosEnRol.Element("Id_Permiso").Value;
                        foreach (var permisoId in permisosRol)
                        {
                            var permisoNombre = (from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                 where permiso.Attribute("Id").Value == permisoId
                                                 select permiso.Element("Nombre").Value).FirstOrDefault();

                            if (!string.IsNullOrEmpty(permisoNombre))
                            {
                                listaPermisos.Add(new BEPermiso(int.Parse(permisoId), permisoNombre));
                            }
                        }
                        return listaPermisos;
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

        #endregion

    }
}
