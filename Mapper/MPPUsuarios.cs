using Entity;
using Entity.Composite;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using Seguridad;

namespace Mapper
{
    public class MPPUsuarios
    {
        readonly string ruta = ("BD.xml");
        XDocument BDXML;

        #region "Métodos"

        #region "Métodos Usuario"

        public bool CrearXML()
        {
            try
            {
                //Verifico que exista el XML:
                if (!(File.Exists(ruta)))
                {
                    //Si no existe, lo creo:
                    BDXML = new XDocument(new XElement("Root",
                    new XElement("Usuarios")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    //En caso que exista el XML, verifico que exista el Elemento Usuarios
                    BDXML = XDocument.Load(ruta);
                    XElement usuarios = BDXML.Root.Element("Usuarios");
                    //Si existe, devuelvo true:
                    if (usuarios != null) { return true; }
                    //Si no, lo creo:
                    else
                    {
                        usuarios = new XElement("Usuarios");
                        BDXML.Root.Add(usuarios);
                        BDXML.Save(ruta);
                        return true;
                    }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Eliminar(BEUsuario oBEUsuario)
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
                        //Busco al usuario:
                        var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                            where usuario.Attribute("Id").Value == oBEUsuario.Id.ToString().Trim()
                                            select usuario;
                        //Verifico que haya encontrado al Usuario:
                        if (buscarUsuario.Any())
                        {
                            var usuarioEncontrado = buscarUsuario.First();
                            var buscarRolesEnUsuario = from rolesUsuario in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                       where rolesUsuario.Element("Id_Usuario").Value == oBEUsuario.Id.ToString().Trim()
                                                       select rolesUsuario;
                            var buscarPersmisosEnUsuario = from permisosUsuario in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                           where permisosUsuario.Element("Id_Usuario").Value == oBEUsuario.Id.ToString().Trim()
                                                           select permisosUsuario;
                            if (!buscarRolesEnUsuario.Any() && !buscarPersmisosEnUsuario.Any())
                            {
                                usuarioEncontrado.Remove();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede Eliminar un usuario que tiene asociado al menos un Rol o al menos un Permiso!"); }
                        }
                        else { throw new Exception("Error: No se econtro al usuario solicitado con el Id brindado!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo cargar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Guardar(BEUsuario oBEUsuario)
        {
            try
            {
                //Verifico que se cargue el XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Usuario no sea nulo:
                        if (oBEUsuario != null)
                        {
                            //Verifico si es un Usuario Nuevo
                            if (oBEUsuario.Id == 0)
                            {
                                //Verifico si ya existe:
                                if (VerificarExistenciaObjeto(oBEUsuario) == false)
                                {
                                    //Le asigno el Id correspondiente:
                                    int ultimoId = ObtenerUltimoId() + 1;
                                    oBEUsuario.Id = ultimoId;
                                    //Encripto la Clave:
                                    string passwordEncriptado = EncriptarPassword(oBEUsuario.Password);
                                    BDXML.Root.Element("Usuarios").Add(new XElement("usuario",
                                        new XAttribute("Id", oBEUsuario.Id.ToString().Trim()),
                                        new XElement("Usuario", oBEUsuario.Usuario.Trim()),
                                        new XElement("Password", passwordEncriptado),
                                        new XElement("Activo", oBEUsuario.Activo.ToString().Trim()),
                                        new XElement("Bloqueado", oBEUsuario.Bloqueado.ToString().Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se puede dar el alta a un Usuario que ya existe!"); }
                            }
                            //En caso de que no, se modifica:
                            else
                            {
                                //Busco al Usuario:
                                var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                                    where usuario.Attribute("Id").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                    select usuario;
                                //Verifico que lo encuentre:
                                if (buscarUsuario.Any())
                                {
                                    //Actualizo los datos correspondientes:
                                    foreach (XElement usuarioModificado in buscarUsuario)
                                    {
                                        string passwordEncriptado = EncriptarPassword(oBEUsuario.Password);
                                        usuarioModificado.Element("Password").Value = passwordEncriptado;
                                        usuarioModificado.Element("Activo").Value = oBEUsuario.Activo.ToString().Trim();
                                        usuarioModificado.Element("Bloqueado").Value = oBEUsuario.Bloqueado.ToString().Trim();
                                    }
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se pudo recuperar los datos del Usuario para actualizar!"); }
                            }
                        }
                        else { throw new Exception("Error: No se puedo obtener los datos del Usuario!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEUsuario ListarObjeto(BEUsuario oBEUsuario)
        {
            try
            {
                //Verifico la Existencia del XML:
                if (CrearXML() == true)
                {
                    if (CrearXMLUsuarioRol() == true)
                    {
                        if (CrearXMLUsuarioPermiso() == true)
                        {
                            //Verifico que se cargue la info del XML:
                            BDXML = XDocument.Load(ruta);
                            if (BDXML != null)
                            {
                                //Verifico que el usuario no sea nulo:
                                if (oBEUsuario != null)
                                {
                                    //Busco al usuario:
                                    var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                                        where usuario.Element("Usuario").Value == oBEUsuario.Usuario.Trim()
                                                        select usuario;
                                    if (buscarUsuario.Any())
                                    {
                                        var usuarioEncontrado = buscarUsuario.First();
                                        if (usuarioEncontrado != null)
                                        {
                                            BEUsuario usuario = new BEUsuario
                                            {
                                                Id = int.Parse(usuarioEncontrado.Attribute("Id").Value.Trim()),
                                                Usuario = usuarioEncontrado.Element("Usuario").Value.Trim(),
                                                Password = usuarioEncontrado.Element("Password").Value,
                                                Activo = bool.Parse(usuarioEncontrado.Element("Activo").Value),
                                                Bloqueado = bool.Parse(usuarioEncontrado.Element("Bloqueado").Value)

                                            };
                                            //Busco la lista de Roles:
                                            var rolesUsuario = from roles in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                               where roles.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                               select roles;
                                            if (rolesUsuario.Any())
                                            {
                                                usuario.listaRoles = new List<BERol>();
                                                // Recorro los roles:
                                                foreach (var rol in rolesUsuario)
                                                {
                                                    //Busco el Rol en el Element Roles:
                                                    string rolId = rol.Element("Id_Rol_Padre").Value.Trim();
                                                    var buscarRolGloblal = from rolGlobal in BDXML.Root.Element("Roles").Descendants("rol")
                                                                           where rolGlobal.Attribute("Id").Value.Trim() == rolId
                                                                           select rolGlobal;
                                                    var rolGlobalEncontrado = buscarRolGloblal.First();

                                                    // Aquí debes crear un objeto BERol y agregarlo a la lista de roles del usuario
                                                    BERol rolUsuario = new BERol
                                                    (
                                                        int.Parse(rol.Element("Id_Rol_Padre").Value.Trim()),
                                                        rolGlobalEncontrado.Element("Nombre").Value.Trim()
                                                    );

                                                    // Agregar el rol a la lista de roles del usuario
                                                    usuario.listaRoles.Add(rolUsuario);

                                                }
                                            }
                                            //Busco la lista de Permisos:
                                            var permisosUsuario = from permisos in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                                  where permisos.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                                  select permisos;
                                            if (permisosUsuario.Any())
                                            {
                                                usuario.listaPermisos = new List<BEPermiso>();
                                                // Recorro los Permisos:
                                                foreach (var permiso in permisosUsuario)
                                                {
                                                    //Busco el Permiso en el Element Permisos:
                                                    string permisoId = permiso.Element("Id_Permiso").Value.Trim();
                                                    var buscarPermisoGloblal = from permisoGlobal in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                               where permisoGlobal.Attribute("Id").Value.Trim() == permisoId
                                                                               select permisoGlobal;
                                                    var permisoGlobalEncontrado = buscarPermisoGloblal.First();

                                                    // Aquí debes crear un objeto BEPermiso y agregarlo a la lista de permisos del usuario
                                                    BEPermiso permisoUsuario = new BEPermiso
                                                    (
                                                        int.Parse(permiso.Element("Id_Permiso").Value.Trim()),
                                                        permisoGlobalEncontrado.Element("Nombre").Value.Trim()
                                                    );

                                                    // Agregar el permiso a la lista de permisos del usuario
                                                    usuario.listaPermisos.Add(permisoUsuario);
                                                }
                                            }
                                            return usuario;
                                        }
                                        else { throw new Exception("Error: No se pudo listar al Usuario"); }
                                    }
                                    else { throw new Exception("Error: No se Encontró ningun Usuario con los datos brindados!"); }
                                }
                                else { throw new Exception("Error: No se pudo recuperar los datos del Usuario!"); }
                            }
                            else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la informacion del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el Elemento Usario-Roles del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEUsuario ListarObjetoPorId(BEUsuario oBEUsuario)
        {
            try
            {
                //Verifico la Existencia del XML:
                if (CrearXML() == true)
                {
                    if (CrearXMLUsuarioRol() == true)
                    {
                        if (CrearXMLUsuarioPermiso() == true)
                        {
                            //Verifico que se cargue la info del XML:
                            BDXML = XDocument.Load(ruta);
                            if (BDXML != null)
                            {
                                //Verifico que el usuario no sea nulo:
                                if (oBEUsuario != null)
                                {
                                    //Busco al usuario:
                                    var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                                        where usuario.Attribute("Id").Value == oBEUsuario.Id.ToString().Trim()
                                                        select usuario;
                                    if (buscarUsuario.Any())
                                    {
                                        var usuarioEncontrado = buscarUsuario.First();
                                        if (usuarioEncontrado != null)
                                        {
                                            BEUsuario usuario = new BEUsuario
                                            {
                                                Id = int.Parse(usuarioEncontrado.Attribute("Id").Value.Trim()),
                                                Usuario = usuarioEncontrado.Element("Usuario").Value.Trim(),
                                                Password = usuarioEncontrado.Element("Password").Value,
                                                Activo = bool.Parse(usuarioEncontrado.Element("Activo").Value),
                                                Bloqueado = bool.Parse(usuarioEncontrado.Element("Bloqueado").Value)

                                            };
                                            //Busco la lista de Roles:
                                            var rolesUsuario = from roles in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                               where roles.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                               select roles;
                                            if (rolesUsuario.Any())
                                            {
                                                usuario.listaRoles = new List<BERol>();
                                                // Recorro los roles:
                                                foreach (var rol in rolesUsuario)
                                                {
                                                    //Busco el Rol en el Element Roles:
                                                    string rolId = rol.Element("Id_Rol_Padre").Value.Trim();
                                                    var buscarRolGloblal = from rolGlobal in BDXML.Root.Element("Roles").Descendants("rol")
                                                                           where rolGlobal.Attribute("Id").Value.Trim() == rolId
                                                                           select rolGlobal;
                                                    var rolGlobalEncontrado = buscarRolGloblal.First();

                                                    // Aquí debes crear un objeto BERol y agregarlo a la lista de roles del usuario
                                                    BERol rolUsuario = new BERol
                                                    (
                                                        int.Parse(rol.Element("Id_Rol_Padre").Value.Trim()),
                                                        rolGlobalEncontrado.Element("Nombre").Value.Trim()
                                                    );

                                                    // Agregar el rol a la lista de roles del usuario
                                                    usuario.listaRoles.Add(rolUsuario);

                                                }
                                            }
                                            //Busco la lista de Permisos:
                                            var permisosUsuario = from permisos in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                                  where permisos.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                                  select permisos;
                                            if (permisosUsuario.Any())
                                            {
                                                usuario.listaPermisos = new List<BEPermiso>();
                                                // Recorro los Permisos:
                                                foreach (var permiso in permisosUsuario)
                                                {
                                                    //Busco el Permiso en el Element Permisos:
                                                    string permisoId = permiso.Element("Id_Permiso").Value.Trim();
                                                    var buscarPermisoGloblal = from permisoGlobal in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                               where permisoGlobal.Attribute("Id").Value.Trim() == permisoId
                                                                               select permisoGlobal;
                                                    var permisoGlobalEncontrado = buscarPermisoGloblal.First();

                                                    // Aquí debes crear un objeto BEPermiso y agregarlo a la lista de permisos del usuario
                                                    BEPermiso permisoUsuario = new BEPermiso
                                                    (
                                                        int.Parse(permiso.Element("Id_Permiso").Value.Trim()),
                                                        permisoGlobalEncontrado.Element("Nombre").Value.Trim()
                                                    );

                                                    // Agregar el permiso a la lista de permisos del usuario
                                                    usuario.listaPermisos.Add(permisoUsuario);
                                                }
                                            }
                                            return usuario;
                                        }
                                        else { throw new Exception("Error: No se pudo listar al Usuario"); }
                                    }
                                    else { throw new Exception("Error: No se Encontró ningun Usuario con los datos brindados!"); }
                                }
                                else { throw new Exception("Error: No se pudo recuperar los datos del Usuario!"); }
                            }
                            else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la informacion del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el Elemento Usario-Roles del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEUsuario> ListarTodo()
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
                        //Creo una nueva lista de usuarios:
                        List<BEUsuario> listaUsuarios = new List<BEUsuario>();
                        var lista = from usuarios in BDXML.Root.Element("Usuarios").Elements("usuario")
                                    select new BEUsuario
                                    {
                                        Id = int.Parse(usuarios.Attribute("Id").Value),
                                        Usuario = usuarios.Element("Usuario").Value,
                                        Password = usuarios.Element("Password").Value,
                                        Activo = bool.Parse(usuarios.Element("Activo").Value),
                                        Bloqueado = bool.Parse(usuarios.Element("Bloqueado").Value)
                                    };
                        listaUsuarios = lista.ToList();

                        return listaUsuarios;
                    }
                    else { throw new Exception("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: no se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
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
                    //Verifico que se carguen los datos:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var pID = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                  select int.Parse(usuario.Attribute("Id").Value.Trim());
                        //Verifico si encontro algun Id:
                        if (pID.Any())
                        {
                            //Busco el Id mas alto:
                            int ultimoId = pID.Max();
                            return ultimoId;
                        }
                        //Si no encontro, devuelvo 0:
                        else { return 0; }
                    }
                    else { throw new XmlException("Error: no se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificarExistenciaObjeto(BEUsuario oBEUsuario)
        {
            try
            {
                //Verifico la existencia del XML:
                if (CrearXML() == true)
                {
                    //Verifico que se cargue el XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el usuario no sea nulo:
                        if (oBEUsuario != null)
                        {
                            //Busco la existencia del usuario:
                            var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                                where usuario.Element("Usuario").Value == oBEUsuario.Usuario.ToString()
                                                select usuario;
                            //En el caso de que encontro al menos un usuario con ese atributo:
                            if (buscarUsuario.Count() > 0) { return true; }
                            //Si no encontro, devulevo false:
                            else { return false; }
                        }
                        else { throw new Exception("Error: No se pudo obtener la ifomración del Usuario"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }



        public string EncriptarPassword(string pPassword)
        {
            try
            {
                if (pPassword != null)
                {
                    if (pPassword.Length > 0)
                    {
                        string passwordEncriptado = Encriptacion.EncriptarPassword(pPassword);
                        return passwordEncriptado;
                    }
                    else { throw new Exception("Error: La contraseña no puede ser vacia!"); }
                }
                else { throw new Exception("Error: La contraseña no puede ser nula!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public string DesencriptarPassword(string pPassword)
        {
            try
            {
                if (pPassword != null)
                {
                    if (pPassword.Length > 0)
                    {
                        string passwordDesencriptado = Encriptacion.DesencriptarPassword(pPassword);
                        return passwordDesencriptado;
                    }
                    else { throw new Exception("Error: La contraseña no puede ser vacia!"); }
                }
                else { throw new Exception("Error: La contraseña no puede ser nula!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool Login(BEUsuario oBEUsuario)
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
                        bool validar = false;
                        //Cifro la contraseña brindada:
                        string passCifrada = EncriptarPassword(oBEUsuario.Password.Trim());
                        //Busco los datos del usuario:
                        var buscarUsuarioGlobal = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                                  where usuario.Element("Usuario").Value.Trim() == oBEUsuario.Usuario.Trim()
                                                  && usuario.Element("Password").Value.Trim() == passCifrada
                                                  select usuario;
                        if (buscarUsuarioGlobal.Any())
                        {
                            var usuarioEncontrado = buscarUsuarioGlobal.First();
                            if (usuarioEncontrado != null)
                            {
                                if (usuarioEncontrado.Element("Activo").Value == "True")
                                {
                                    if (usuarioEncontrado.Element("Bloqueado").Value == "False")
                                    {
                                        validar = true;
                                        return validar;
                                    }
                                    else { throw new Exception("Error: El Usuario se encuentra Bloqueado! Por favor, contactese con el Administrador para Desbloquearlo!"); }
                                }
                                else { throw new Exception("Error: El Usuario no se encuentra activo, por favor contacteste con el Administrador para que lo habilite!"); }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Usuario!"); }
                        }
                        else { throw new Exception("Error: Verifique su nombre de Usuario y/o Contraseña!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        #endregion

        #region "Métodos Usuario - Permisos"

        public bool CrearXMLUsuarioPermiso()
        {
            try
            {
                //Si no existe el XML lo creo::
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Usuario_Permisos")));
                    BDXML.Save(ruta);
                    return true;
                }
                //Si existe verifico que exista el elemento Usuario_Permisos:
                else
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        XElement usuarioPermisos = BDXML.Root.Element("Usuario_Permisos");
                        //verifico si existe el Elemento Usuario_Roles:
                        if (usuarioPermisos == null)
                        {
                            usuarioPermisos = new XElement("Usuario_Permisos");
                            BDXML.Root.Add(usuarioPermisos);
                            BDXML.Save(ruta);
                            return true;
                        }
                        //Si existe devuelvo true:
                        else { return true; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificiarUsuarioRol(BEUsuario oBEUsuario, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico que exista el Elemento Usuario_Permisos en el XML:
                    if (CrearXMLUsuarioRol() == true)
                    {
                        //Verifico que se cargue la info del XML:
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            //Verifico que el Rol y el Usuario no sean nulos!:
                            if (oBEUsuario != null && oBEPermiso != null)
                            {
                                //Busco si existe el Usuario que tenga asignado el Rol
                                var buscarUsuarioPermiso = from usuarioPermiso in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                           where usuarioPermiso.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                           && usuarioPermiso.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                                           select usuarioPermiso;
                                //Si lo encontro devuelvo True:
                                if (buscarUsuarioPermiso.Count() > 0) { return true; }
                                //Si no tiene asignado el Permiso devuelvo false:
                                else { return false; }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del Usuario y/o Rol indicados!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del Elemento Usuario-Permisos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool AsociarPermisoAUsuario(BEUsuario oBEUsario, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico la existencia del Elemento Usuario-Permisos en el XML:
                    if (CrearXMLUsuarioPermiso() == true)
                    {
                        //Vericico que se cargue la info del XML:
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            //Verifico que el Usuario y el Permiso no sean nulos:
                            if (oBEUsario != null && oBEUsario != null)
                            {
                                //Verifico si tiene asociado el Permiso al Usuario:
                                if (VerificiarUsuarioRol(oBEUsario, oBEPermiso) == false)
                                {
                                    BDXML.Root.Element("Usuario_Permisos").Add(new XElement("usuario_permiso",
                                        new XElement("Id_Usuario", oBEUsario.Id.ToString().Trim()),
                                        new XElement("Id_Permiso", oBEPermiso.Id.ToString().Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: El Permiso ya esta Asociado al Usuario!"); }
                            }
                            else { throw new Exception("Error: No se pudo obtener los datos del Usuario y/o Permiso brindado!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del Elemento Usuario-Permisos del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool DesasociarPermisoAUsuario(BEUsuario oBEUsario, BEPermiso oBEPermiso)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true)
                {
                    //Verifico la existencia del Elemento Usuario-Permisos en el XML:
                    if (CrearXMLUsuarioPermiso() == true)
                    {
                        //Vericico que se cargue la info del XML:
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            //Verifico que el Usuario y el Permiso no sean nulos:
                            if (oBEUsario != null && oBEUsario != null)
                            {
                                //Verifico si tiene asociado el Permiso al Usuario:
                                if (VerificiarUsuarioRol(oBEUsario, oBEPermiso) == true)
                                {
                                    var buscarUsuarioPermiso = from usuarioPermiso in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                               where usuarioPermiso.Element("Id_Usuario").Value.Trim() == oBEUsario.Id.ToString().Trim()
                                                               && usuarioPermiso.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                                               select usuarioPermiso;
                                    if (buscarUsuarioPermiso.Any())
                                    {
                                        var usuarioPermisoEncontrado = buscarUsuarioPermiso.First();
                                        usuarioPermisoEncontrado.Remove();
                                        BDXML.Save(ruta);
                                        return true;
                                    }
                                    else { throw new Exception("Error: No se pudo recuperar la información del Permiso en el Usuario indicado!"); }
                                }
                                else { throw new Exception("Error: El Permiso ya esta Asociado al Usuario!"); }
                            }
                            else { throw new Exception("Error: No se pudo obtener los datos del Usuario y/o Permiso brindado!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del Elemento Usuario-Permisos del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        #endregion

        #region "Métodos Usuario - Roles"

        public bool CrearXMLUsuarioRol()
        {
            try
            {
                //Si no existe el XML lo creo::
                if (!(File.Exists(ruta)))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Usuario_Roles")));
                    BDXML.Save(ruta);
                    return true;
                }
                //Si existe verifico que exista el elemento Usuario_Roles:
                else
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        XElement usuarioRoles = BDXML.Root.Element("Usuario_Roles");
                        //verifico si existe el Elemento Usuario_Roles:
                        if (usuarioRoles == null)
                        {
                            usuarioRoles = new XElement("Usuario_Roles");
                            BDXML.Root.Add(usuarioRoles);
                            BDXML.Save(ruta);
                            return true;
                        }
                        //Si existe devuelvo true:
                        else { return true; }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool VerificiarUsuarioRol(BEUsuario oBEUsuario, BERol oBERol)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXMLUsuarioRol() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Rol y el Usuario no sean nulos!:
                        if (oBEUsuario != null && oBERol != null)
                        {
                            //Busco si existe el Usuario que tenga asignado el Rol
                            var buscarUsuarioRol = from usuarioRol in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                   where usuarioRol.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                   && usuarioRol.Element("Id_Rol_Padre").Value.Trim() == oBERol.Id.ToString().Trim()
                                                   select usuarioRol;
                            if (buscarUsuarioRol.Count() > 0) { return true; }
                            var buscarUsuarioRolesPadres = from listaRolesPadres in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                           where listaRolesPadres.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                           select listaRolesPadres;
                            foreach (var rolePadre in buscarUsuarioRolesPadres)
                            {
                                var listaRolesUsuario = (from roles in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                         where roles.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                         select roles).First();
                                if (listaRolesUsuario != null)
                                {
                                    var listar = from lista in listaRolesUsuario.Descendants("Roles")
                                                 select lista;
                                    if (listar.Any())
                                    {
                                        foreach (var rol in listar.Descendants("Id_Rol"))
                                        {
                                            var id = rol.Value.Trim();
                                            if (BuscarRolEnJerarquia(rol.Parent.Parent, oBERol.Id.ToString().Trim()) == true) { return true; }
                                            else { return false; }
                                        }
                                    }
                                }
                                //Si no tiene asignado el Rol devuelvo false:
                                else { return false; }
                            }
                            return false;
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del Usuario y/o Rol indicados!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool AsociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol)
        {
            try
            {
                //Verifico la Existencia del XML:
                if (CrearXML() == true)
                {
                    if (CrearXMLUsuarioRol() == true)
                    {
                        //Verifico la existencia del XML:
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            //Verifico que el Usuario y el Rol no sean nulos:
                            if (oBEUsuario != null && oBERol != null)
                            {
                                if (VerificiarUsuarioRol(oBEUsuario, oBERol) == false)
                                {
                                    BDXML.Root.Element("Usuario_Roles").Add(new XElement("usuario_rol",
                                        new XElement("Id_Usuario", oBEUsuario.Id.ToString().Trim()),
                                        new XElement("Id_Rol_Padre", oBERol.Id.ToString().Trim())));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: El Usuario ya tiene Asociado el Rol indicado!"); }
                            }
                            else { throw new Exception("Error: No se pudo obtener la información del Usuario y/o Rol brindados!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del Usuario y/o Rol!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool DesasociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol)
        {
            try
            {
                //Verifico la Existencia del XML:
                if (CrearXMLUsuarioRol() == true)
                {
                    //Verifico la existencia del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que el Usuario y el Rol no sean nulos:
                        if (oBEUsuario != null && oBERol != null)
                        {
                            if (VerificiarUsuarioRol(oBEUsuario, oBERol) == true)
                            {
                                var buscarUsuarioRol = from usuarioRol in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                       where usuarioRol.Element("Id_Usuario").Value == oBEUsuario.Id.ToString().Trim()
                                                       && usuarioRol.Element("Id_Rol_Padre").Value == oBERol.Id.ToString().Trim()
                                                       select usuarioRol;
                                if (buscarUsuarioRol.Any())
                                {
                                    var usuarioRolEncontrado = buscarUsuarioRol.First();
                                    var verificarSiTieneOtrosRoles = usuarioRolEncontrado.Element("Roles");
                                    if (verificarSiTieneOtrosRoles == null)
                                    {
                                        usuarioRolEncontrado.Remove();
                                        BDXML.Save(ruta);
                                        return true;
                                    }
                                    else { throw new Exception("Error: El Rol que quiere eliminar tiene por lo menos un Rol asociado, por favor elimine primero el Rol Asociado asi puede eliminar este Rol!"); }
                                }
                                else { throw new Exception("Error: No se encontro ni el Usuario ni el Rol indicado!"); }
                            }
                            else { throw new Exception("Error: El Usuario ya tiene Asociado el Rol indicado!"); }
                        }
                        else { throw new Exception("Error: No se pudo obtener la información del Usuario y/o Rol brindados!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }


        #endregion

        #region "Usuario - Roles Jerarquicos"

        private bool BuscarRolEnJerarquia(XElement rolesElement, string rolId)
        {
            foreach (var rol in rolesElement.Elements("Roles"))
            {
                foreach (var idRol in rol.Elements("Id_Rol"))
                {
                    if (idRol != null && idRol.Value.Trim() == rolId) { return true; }
                }
                if (rol.Elements("Roles").Any() && BuscarRolEnJerarquia(rol, rolId)) { return true; }
            }
            return false;
        }

        public bool AsociarUsuarioARolJerarquico(BEUsuario oBEUsuario, BERol oBERolPadre, BERol oBERol)
        {
            try
            {
                // Verifico la existencia del XML
                if (CrearXML() && CrearXMLUsuarioRol())
                {
                    // Cargo la información del XML
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        //Verifico que ningno de los Roles sean nulos:
                        if (oBERolPadre != null && oBERol != null)
                        {
                            if (VerificiarUsuarioRol(oBEUsuario, oBERol) == false)
                            {
                                // Busco los Roles del Usuario:
                                var usuarioRoles = from rol in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                   where rol.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                   select rol;
                                if (usuarioRoles.Any())
                                {
                                    //Selecciono el elemento usuario_rol:
                                    var usuarioRolElemento = usuarioRoles.First();
                                    if (usuarioRolElemento != null)
                                    {
                                        //verifico que el oBERol no existe:
                                        if (BuscarRolEnJerarquia(usuarioRolElemento, oBERol.Id.ToString().Trim())) { throw new Exception($"Error: El Rol {oBERol.Nombre} ya está asociado!"); }
                                        //En caso de que el oBERolPadre sea el Rol de mayor jerarquía:
                                        if (usuarioRolElemento.Element("Id_Rol_Padre").Value.Trim() == oBERolPadre.Id.ToString().Trim())
                                        {
                                            var buscarListaRoles = from listaRoles in usuarioRolElemento.Descendants("Roles")
                                                                   select listaRoles;
                                            //SI Exisitia una jerarquia de Roles:
                                            if (buscarListaRoles.Any())
                                            {
                                                var listaRolesEncontrada = buscarListaRoles.First();
                                                if (listaRolesEncontrada != null)
                                                {
                                                    listaRolesEncontrada.Add(new XElement("Id_Rol", oBERol.Id.ToString().Trim()));
                                                    BDXML.Save(ruta);
                                                    return true;
                                                }
                                                else { throw new Exception("Error: No se pudo recuperar la lista de jerarquias de Roles!"); }
                                            }
                                            //En caso de que no Exista:
                                            else
                                            {
                                                usuarioRolElemento.Add(new XElement("Roles",
                                                    new XElement("Id_Rol", oBERol.Id.ToString().Trim())));
                                                BDXML.Save(ruta);
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            //Llamo al método recursivo para buscar y asociar el rol en la jerarquía:
                                            return AsociarRolRecursivo(usuarioRolElemento, oBERolPadre, oBERol);
                                        }
                                    }
                                    else { throw new Exception("Error: No se pudo recueperar la información!"); }
                                }
                                else { throw new Exception("Error: Primero tiene que asociar en Forma individual el Rol Principal del Usuario!"); }
                            }
                            else { throw new Exception("Error: El Rol que quiere asignar ya lo tiene el usuario!"); }
                        }
                        else { throw new Exception("Error: No se pudo obtener la información de los roles indicados."); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML."); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML."); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool DesasoriarUnRolDentroOtroRol(BEUsuario oBEUsuario, BERol oBERol, BERol oBERolPadre)
        {
            try
            {
                //Verifico que exista el XML:
                if (CrearXML() == true && CrearXMLUsuarioRol() == true)
                {
                    //Verifico que se cargue la info del XML:
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEUsuario != null)
                        {
                            if (oBERolPadre != null && oBERol != null)
                            {
                                var listarRolesUsuario = (from listaRoles in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                                          where listaRoles.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                          select listaRoles);
                                if (listarRolesUsuario.Any())
                                {
                                    foreach (var rol in listarRolesUsuario)
                                    {
                                        //En Caso de que sea el oBERolPadre sea el Rol Padre:
                                        if (oBERolPadre.Id.ToString().Trim() == rol.Element("Id_Rol_Padre").Value.Trim())
                                        {
                                            var buscarRolAEliminar = from buscarRolEliminar in rol.Elements("Roles")
                                                                     where buscarRolEliminar.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                                     select buscarRolEliminar;
                                            if (buscarRolAEliminar.Any())
                                            {
                                                var rolAEliminarENcontrado = buscarRolAEliminar.First();
                                                if (rolAEliminarENcontrado != null)
                                                {
                                                    var verificarSiTieneRoles = rolAEliminarENcontrado.Element("Roles");
                                                    if (verificarSiTieneRoles == null)
                                                    {
                                                        rolAEliminarENcontrado.Remove();
                                                        BDXML.Save(ruta);
                                                        return true;
                                                    }
                                                    else { throw new Exception("Error: El Rol que quiere eliminar tiene al menos un Rol Asociado, por lo que primero tiene que eliminar el Rol hijo!"); }
                                                }
                                                else { throw new Exception("Error: No se pudo recuperar la información del Rol a Eliminar!"); }
                                            }
                                            else { throw new Exception("Error: No se encontro el Rol a eliminar dentro de los roles asignados al Usuario!"); }
                                        }
                                        //En Caso de que el oBERolPadre no sea el Rol Padre General:
                                        else
                                        {
                                            foreach (var rolUsuario in listarRolesUsuario)
                                            {
                                                if (DesasociarRolRecursivo(rolUsuario, oBERol, oBERolPadre) == true) { return true; }
                                                else { throw new Exception("Algun error!"); }
                                            }
                                        }
                                    }
                                    return false;
                                }
                                else { throw new Exception("Error: El Usuario no tiene asignado ningun Rol!"); }
                            }
                            else { throw new Exception("Error: No se pudo recuperar la información del alguno de los roles Brindados!"); }
                        }
                        else { throw new Exception("Error: No se pudo recuperar la información del usuario!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        private bool DesasociarRolRecursivo(XElement rolPadre, BERol oBERol, BERol oBERolPadre)
        {
            try
            {
                var buscarRolPadre = from rol in rolPadre.Descendants("Id_Rol")
                                     where rol.Value.Trim() == oBERolPadre.Id.ToString().Trim()
                                     select rol;
                if (buscarRolPadre.Any())
                {
                    var padrePrincipalEncontrado = buscarRolPadre.First();
                    if (padrePrincipalEncontrado != null)
                    {
                        //Subo un nivel de los datos Encontrados:
                        var rolesPadre = padrePrincipalEncontrado.Parent;
                        var subRoles = rolesPadre.Element("Roles");
                        if (subRoles != null)
                        {
                            var subRol = from rol in rolesPadre.Descendants("Roles")
                                         select rol;
                            if (subRol.Any())
                            {
                                foreach (var rol in subRol)
                                {
                                    var buscarRol = from rolEncontrado in subRol
                                                    where rol.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                    select rol;
                                    if (buscarRol.Any())
                                    {
                                        var rolAEliminarEncontrado = buscarRol.First();
                                        if (!rolAEliminarEncontrado.Elements("Roles").Any())
                                        {
                                            rolAEliminarEncontrado.Remove();
                                            BDXML.Save(ruta);
                                            return true;
                                        }
                                        else { throw new Exception("Error: El Rol a Eliminar tiene otros Roles, por lo que primero tiene que eliminar los otros Roles!"); }
                                    }
                                }
                            }
                            return false;
                        }
                        else { throw new Exception("Error: No se encontro el Rol a Eliminar!"); }
                    }
                    else
                    {
                        // Si no se encuentra el padre, sigue buscando recursivamente
                        return DesasociarRolRecursivo(padrePrincipalEncontrado, oBERolPadre, oBERol);
                    }
                }
                else { throw new Exception($"Error: El Rol {oBERolPadre.Nombre} no se encuentra asociado al usuario. Primero asocie dicho rol para luego poder asociar el rol: {oBERol.Nombre}."); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        private bool AsociarRolRecursivo(XElement rolElemento, BERol oBERolPadre, BERol oBERol)
        {
            try
            {
                var buscarRolPadre = from rol in rolElemento.Descendants("Id_Rol")
                                     where rol.Value.Trim() == oBERolPadre.Id.ToString().Trim()
                                     select rol;
                if (buscarRolPadre.Any())
                {
                    var padrePrincipalEncontrado = buscarRolPadre.First();
                    if (padrePrincipalEncontrado != null)
                    {
                        //Subo un nivel de los datos Encontrados:
                        var rolesPadre = padrePrincipalEncontrado.Parent;
                        //Verifico si existe la listaRoles existente para añadir el nuevo rol:
                        XElement listaRolesEncontrada = rolesPadre.Element("Roles");
                        if (listaRolesEncontrada == null)
                        {
                            //Si no existe, creo uno nuevo:
                            listaRolesEncontrada = new XElement("Roles");
                            rolesPadre.Add(listaRolesEncontrada);
                        }
                        listaRolesEncontrada.Add(new XElement("Id_Rol", oBERol.Id.ToString().Trim()));
                        BDXML.Save(ruta);
                        return true;
                    }
                    else
                    {
                        // Si no se encuentra el padre, sigue buscando recursivamente
                        return AsociarRolRecursivo(padrePrincipalEncontrado, oBERolPadre, oBERol);
                    }
                }
                else { throw new Exception($"Error: El Rol {oBERolPadre.Nombre} no se encuentra asociado al usuario. Primero asocie dicho rol para luego poder asociar el rol: {oBERol.Nombre}."); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public BEUsuario ListarObjetoJerarquico(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML() && CrearXMLUsuarioRol() && CrearXMLUsuarioPermiso())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                            where usuario.Attribute("Id").Value == oBEUsuario.Id.ToString().Trim()
                                            select usuario;
                        if (buscarUsuario.Any())
                        {
                            var usuarioEncontrado = buscarUsuario.First();
                            BEUsuario usuario = new BEUsuario
                            {
                                Id = int.Parse(usuarioEncontrado.Attribute("Id").Value.Trim()),
                                Usuario = usuarioEncontrado.Element("Usuario").Value.Trim(),
                                Password = usuarioEncontrado.Element("Password").Value,
                                Activo = bool.Parse(usuarioEncontrado.Element("Activo").Value),
                                Bloqueado = bool.Parse(usuarioEncontrado.Element("Bloqueado").Value)
                            };
                            // Obtener roles jerárquicos
                            var rolesUsuario = from rol in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                               where rol.Element("Id_Usuario").Value == usuario.Id.ToString().Trim()
                                               select rol;
                            if (rolesUsuario.Any())
                            {
                                foreach (var rolPadre in rolesUsuario)
                                {
                                    var buscarRolGeneralPadre = (from rolPadreGeneral in BDXML.Root.Element("Roles").Descendants("rol")
                                                                 where rolPadreGeneral.Attribute("Id").Value.Trim() == rolPadre.Element("Id_Rol_Padre").Value.Trim()
                                                                 select rolPadreGeneral).First();
                                    BERol rol = new BERol(int.Parse(rolPadre.Element("Id_Rol_Padre").Value.Trim()), buscarRolGeneralPadre.Element("Nombre").Value.Trim());
                                    var buscarPermisos = from permisos in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                         where permisos.Element("Id_Rol").Value == rol.Id.ToString().Trim()
                                                         select permisos;
                                    if (buscarPermisos.Any())
                                    {
                                        foreach (var permiso in buscarPermisos)
                                        {
                                            var permisoNombre = (from permisoEnRol in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                 where permisoEnRol.Attribute("Id").Value == permiso.Element("Id_Permiso").Value.Trim()
                                                                 select permisoEnRol.Element("Nombre").Value).FirstOrDefault();
                                            if (permisoNombre != null)
                                            {
                                                BEPermiso oBEPermiso = new BEPermiso(int.Parse(permiso.Element("Id_Permiso").Value.Trim()), permisoNombre);
                                                rol.listaPermisos.Add(oBEPermiso);
                                            }
                                        }
                                    }
                                    AgregarSubRoles(rol, rolPadre.Element("Roles"));
                                    usuario.listaRoles.Add(rol);
                                }
                            }
                            //Busco la lista de Permisos:
                            var permisosUsuario = from permisos in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                  where permisos.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                                  select permisos;
                            if (permisosUsuario.Any())
                            {
                                usuario.listaPermisos = new List<BEPermiso>();
                                // Recorro los Permisos:
                                foreach (var permiso in permisosUsuario)
                                {
                                    //Busco el Permiso en el Element Permisos:
                                    string permisoId = permiso.Element("Id_Permiso").Value.Trim();
                                    var buscarPermisoGloblal = from permisoGlobal in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                               where permisoGlobal.Attribute("Id").Value.Trim() == permisoId
                                                               select permisoGlobal;
                                    var permisoGlobalEncontrado = buscarPermisoGloblal.First();

                                    // Aquí debes crear un objeto BEPermiso y agregarlo a la lista de permisos del usuario
                                    BEPermiso permisoUsuario = new BEPermiso
                                    (
                                        int.Parse(permiso.Element("Id_Permiso").Value.Trim()),
                                        permisoGlobalEncontrado.Element("Nombre").Value.Trim()
                                    );

                                    // Agregar el permiso a la lista de permisos del usuario
                                    usuario.listaPermisos.Add(permisoUsuario);
                                }
                            }
                            return usuario;
                        }
                        else { throw new Exception("Error: No se encontró el Usuario indicado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar los datos del Usuario!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        private void AgregarSubRoles(BERol rol, XElement rolesElement)
        {
            if (rolesElement != null)
            {
                foreach (var rolesRol in rolesElement.Elements("Id_Rol"))
                {
                    var subRolEncontrado = rolesRol.Parent;
                    int id = int.Parse(rolesRol.Value);
                    var buscarRolGeneral = (from rolGeneral in BDXML.Root.Element("Roles").Descendants("rol")
                                            where rolGeneral.Attribute("Id").Value == id.ToString().Trim()
                                            select rolGeneral).First();
                    BERol subRol = new BERol(id, buscarRolGeneral.Element("Nombre").Value.Trim());
                    var buscarPermisos = from permisos in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                         where permisos.Element("Id_Rol").Value == id.ToString().Trim()
                                         select permisos;
                    if (buscarPermisos.Any())
                    {
                        foreach (var permiso in buscarPermisos)
                        {
                            var permisoNombre = (from permisoEnRol in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                 where permisoEnRol.Attribute("Id").Value == permiso.Element("Id_Permiso").Value.Trim()
                                                 select permisoEnRol.Element("Nombre").Value).FirstOrDefault();
                            if (permisoNombre != null)
                            {
                                BEPermiso oBEPermiso = new BEPermiso(int.Parse(permiso.Element("Id_Permiso").Value.Trim()), permisoNombre);
                                subRol.listaPermisos.Add(oBEPermiso);
                            }
                        }
                    }
                    var siguienteElemento = rolesRol.ElementsAfterSelf().FirstOrDefault();
                    if (siguienteElemento != null)
                    {
                        if (siguienteElemento.Name == "Id_Rol")
                        {
                            rol.listaPermisos.Add(subRol);
                        }
                        else if (siguienteElemento.Name == "Roles")
                        {
                            AgregarSubRoles(subRol, siguienteElemento);
                            rol.listaPermisos.Add(subRol);
                        }
                    }
                    else
                    {
                        var verificarSiTieneRoles = from roles in subRolEncontrado.Descendants("Roles")
                                                    select roles;

                        if (verificarSiTieneRoles.Any()) { AgregarSubRoles(subRol, rolesElement.Element("Roles")); }
                        rol.listaPermisos.Add(subRol);
                    }
                }
            }
        }

        public List<BERol> ListarRolesDeUsuario(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML() && CrearXMLUsuarioRol() && CrearXMLUsuarioPermiso())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        var buscarUsuario = from usuario in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                            where usuario.Attribute("Id").Value == oBEUsuario.Id.ToString().Trim()
                                            select usuario;
                        if (buscarUsuario.Any())
                        {
                            List<BERol> listaRoles = new List<BERol>();
                            var usuarioEncontrado = buscarUsuario.First();
                            BEUsuario usuario = new BEUsuario
                            {
                                Id = int.Parse(usuarioEncontrado.Attribute("Id").Value.Trim()),
                                Usuario = usuarioEncontrado.Element("Usuario").Value.Trim(),
                                Password = usuarioEncontrado.Element("Password").Value,
                                Activo = bool.Parse(usuarioEncontrado.Element("Activo").Value),
                                Bloqueado = bool.Parse(usuarioEncontrado.Element("Bloqueado").Value)
                            };
                            // Obtener roles jerárquicos
                            var rolesUsuario = from rol in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                               where rol.Element("Id_Usuario").Value == usuario.Id.ToString().Trim()
                                               select rol;
                            if (rolesUsuario.Any())
                            {
                                foreach (var rolPadre in rolesUsuario)
                                {
                                    var buscarRolGeneralPadre = (from rolPadreGeneral in BDXML.Root.Element("Roles").Descendants("rol")
                                                                 where rolPadreGeneral.Attribute("Id").Value.Trim() == rolPadre.Element("Id_Rol_Padre").Value.Trim()
                                                                 select rolPadreGeneral).First();
                                    BERol rol = new BERol(int.Parse(rolPadre.Element("Id_Rol_Padre").Value.Trim()), buscarRolGeneralPadre.Element("Nombre").Value.Trim());
                                    AgregarSubRolesALista(rol, rolPadre.Element("Roles"), oBEUsuario);
                                    listaRoles = oBEUsuario.listaRoles; // Agregar rol a la lista de roles
                                }
                            }
                            return listaRoles;
                        }
                        else { throw new Exception("Error: No se encontró el Usuario indicado!"); }
                    }
                    else { throw new Exception("Error: No se pudo recuperar los datos del Usuario!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        private void AgregarSubRolesALista(BERol rol, XElement rolesElement, BEUsuario oBEUsuario)
        {
            if (rolesElement != null)
            {
                foreach (var rolesRol in rolesElement.Elements("Id_Rol"))
                {
                    int id = int.Parse(rolesRol.Value);
                    var buscarRolGeneral = (from rolGeneral in BDXML.Root.Element("Roles").Descendants("rol")
                                            where rolGeneral.Attribute("Id").Value == id.ToString().Trim()
                                            select rolGeneral).First();
                    BERol subRol = new BERol(id, buscarRolGeneral.Element("Nombre").Value.Trim());
                    var siguienteElemento = rolesRol.ElementsAfterSelf().FirstOrDefault();
                    if (siguienteElemento != null)
                    {
                        AgregarSubRolesALista(subRol, siguienteElemento, oBEUsuario);
                    }
                    if (!oBEUsuario.listaRoles.Any(r => r.Id == subRol.Id))
                    {
                        //Agrego el subrol a la lista de roles si no existe:
                        oBEUsuario.listaRoles.Add(subRol);
                    }
                }
            }
        }

        public List<BEPermiso> ListarTodosLosPermisosDelUsuario(BEUsuario oBEusuario)
        {
            try
            {
                if (CrearXML() == true && CrearXMLUsuarioPermiso() == true && CrearXMLUsuarioRol() == true)
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (oBEusuario != null)
                        {
                            if (oBEusuario.Id > 0)
                            {
                                oBEusuario.listaRoles = ListarRolesDeUsuario(oBEusuario);
                                List<BEPermiso> listaPermisos = new List<BEPermiso>();
                                var buscarPermisosUsuario = from permisosUsuario in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                                            where permisosUsuario.Element("Id_Usuario").Value.Trim() == oBEusuario.Id.ToString().Trim()
                                                            select permisosUsuario;
                                if (buscarPermisosUsuario != null)
                                {
                                    foreach (var permisosGenerales in buscarPermisosUsuario)
                                    {
                                        var idPermiso = permisosGenerales.Element("Id_Permiso").Value.Trim();
                                        var buscarDatosPermiso = (from datoPermiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                  where datoPermiso.Attribute("Id").Value.Trim() == idPermiso
                                                                  select datoPermiso).First();
                                        if (buscarDatosPermiso != null)
                                        {
                                            BEPermiso permiso = new BEPermiso(int.Parse(idPermiso), buscarDatosPermiso.Element("Nombre").Value.Trim());
                                            listaPermisos.Add(permiso);
                                        }
                                        else { throw new Exception("Error: No se pudo recuperar la información específica del Permiso!"); }
                                    }
                                }
                                if (oBEusuario.listaRoles != null)
                                {
                                    if (oBEusuario.listaRoles.Count > 0)
                                    {
                                        foreach (var rol in oBEusuario.listaRoles)
                                        {
                                            var buscarRol = from roles in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                            where roles.Element("Id_Rol").Value.Trim() == rol.Id.ToString().Trim()
                                                            select roles;
                                            if (buscarRol != null)
                                            {
                                                foreach (var permisoEnRol in buscarRol)
                                                {
                                                    var idPermiso = permisoEnRol.Element("Id_Permiso").Value.Trim();
                                                    var busarPermiso = (from permiso in BDXML.Root.Element("Permisos").Descendants("permiso")
                                                                        where permiso.Attribute("Id").Value.Trim() == idPermiso
                                                                        select permiso).First();
                                                    if (busarPermiso != null)
                                                    {
                                                        BEPermiso permisoDeRol = new BEPermiso(int.Parse(idPermiso), busarPermiso.Element("Nombre").Value.Trim());
                                                        listaPermisos.Add(permisoDeRol);
                                                    }
                                                    else { throw new Exception("Error: No se pudo recuperar la información particular del Permiso!"); }
                                                }
                                            }
                                        }
                                    }
                                }
                                return listaPermisos;
                            }
                            else { throw new Exception("Error: No se puede buscar los permisos de un Usuario nuevo!"); }
                        }
                        else { throw new Exception("Error: No se recuperar la información del Usuario que brindo!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }


        #endregion

        #endregion
    }
}
