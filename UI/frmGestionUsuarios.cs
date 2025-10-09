using BLL;
using BLL.Composite;
using Entity;
using Entity.Composite;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.DataFormats;

namespace UI
{
    public partial class frmGestionUsuarios : Form
    {
        BEUsuario oBEUsuario;
        BERol oBERol;
        BERol oBERol2;
        BEPermiso oBEPermiso;
        BLLRol oBLLRol;
        BLLPermiso oBLLPermiso;
        BLLUsuario oBLLUsuario;
        Regex nwRegex;
        private MenuStrip menuStrip;


        public frmGestionUsuarios(MenuStrip menuStrip)
        {
            InitializeComponent();
            oBEUsuario = new BEUsuario();
            oBLLUsuario = new BLLUsuario();
            oBLLRol = new BLLRol();
            oBLLPermiso = new BLLPermiso();
            cmbMenu.SelectedIndexChanged += cmbMenu_SelectedIndexChanged;
            this.menuStrip = menuStrip;
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            //var frmMDI = this.MdiParent as frmMDI; // o el nombre de tu MDI
            //if (frmMDI != null)
            //    this.menuStrip = frmMDI.MainMenuStrip;
            LimpiarCampos();
        }



        private void btnAltaRol_Click(object sender, EventArgs e)
        {
            try
            {
                oBERol = ValidarDatos();
                if (oBERol != null)
                {
                    oBLLRol.Guardar(oBERol);
                    LimpiarCampos();
                    MessageBox.Show("Se ha creado correctamente el Rol!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            try
            {

                if (treeVwRoles.Nodes.Count > 0 && treeVwRoles.Nodes[0].Nodes.Count > 0)
                {
                    oBERol = ValidarDatos();
                    if (oBERol != null)
                    {
                        oBLLRol.Guardar(oBERol);
                        LimpiarCampos();
                        MessageBox.Show("Se ha modificado correctamente el Rol!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else { throw new Exception("Error: Para modificar un Rol primero tiene que existir al Menos uno!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeVwRoles.Nodes.Count > 0 && treeVwRoles.Nodes[0].Nodes.Count > 0)
                {
                    oBERol = ValidarDatos();
                    if (oBERol != null)
                    {
                        oBLLRol.Eliminar(oBERol);
                        LimpiarCampos();
                        MessageBox.Show("Se ha eliminado correctamente el Rol!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else { throw new Exception("Error: Para Eliminar un Rol primero tiene que existir al Menos uno!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAltaPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                oBEPermiso = ValidarDatosPermiso();
                if (oBEPermiso != null)
                {
                    oBLLPermiso.Guardar(oBEPermiso);
                    LimpiarCampos();
                    MessageBox.Show("Se ha creado correctamente el Permiso!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeVwPermisos.Nodes.Count > 0 && treeVwPermisos.Nodes[0].Nodes.Count > 0)
                {
                    oBEPermiso = ValidarDatosPermiso();
                    if (oBEPermiso != null)
                    {
                        //Tengo que ver si elimina si esta asociado un rol con un permiso particular:
                        oBLLPermiso.Eliminar(oBEPermiso);
                        LimpiarCampos();
                        MessageBox.Show("Se ha eliminado correctamente el Permiso!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else { throw new Exception("Error: Para Eliminar un Permiso primero tiene que existir al Menos uno!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarPermisoRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDPermiso.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0)
                    {
                        oBERol = ValidarDatos();
                        oBEPermiso = ValidarDatosPermiso();
                        if (oBEPermiso != null && oBERol != null)
                        {
                            oBLLRol.AsociarRolaPermiso(oBERol, oBEPermiso);
                            LimpiarCampos();
                            MessageBox.Show("Se ha Asociado correctamente el Permiso al Rol!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar un Rol!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar un Permiso!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarPermisoRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDPermiso.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0)
                    {
                        oBERol = ValidarDatos();
                        oBEPermiso = ValidarDatosPermiso();
                        oBLLRol.DesasociarRolaPermiso(oBERol, oBEPermiso);
                        LimpiarCampos();
                        MessageBox.Show("Se ha Desasociado correctamente el Permiso al Rol!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar un Rol!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar un Permiso!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarRolUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0)
                    {
                        oBERol = ValidarDatos();
                        oBLLUsuario.AsociarUsuarioARol(oBEUsuario, oBERol);
                        LimpiarCampos();
                        MessageBox.Show("Se ha Asociado correctamente el Rol al Usuario!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar un Rol!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar a un Usuario!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarRolUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0)
                    {
                        oBERol = ValidarDatos();
                        oBLLUsuario.DesasociarUsuarioARol(oBEUsuario, oBERol);
                        LimpiarCampos();
                        MessageBox.Show("Se ha Desasociado correctamente el Rol al Usuario!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar un Rol!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar a un Usuario!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarRolesUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0 && txtIDRoles.Text.Length > 0)
                    {
                        if (oBEUsuario != null)
                        {
                            oBERol = ValidarDatos();
                            oBERol2 = ValidarDatosRol2();
                            if (oBERol.Nombre == oBERol2.Nombre) { throw new Exception("Error: Se esta asignado el Mismo Rol dos veces!"); }
                            else
                            {
                                oBLLUsuario.AsociarUsuarioARolJerarquico(oBEUsuario, oBERol, oBERol2);
                                MessageBox.Show($"Se ha Asociado correctamente el Rol: \"{oBERol2.Nombre}\" al Rol: \"{oBERol.Nombre}\"!", "Éxito:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                            }
                        }
                        else { throw new Exception("Error: Primero tiene que seleccionar a un Usuario!"); }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar los dos Roles!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar al Usuario que le va a asignar los roles correspondiente!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarRolesUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDRol.Text.Length > 0 && txtIDRoles.Text.Length > 0)
                    {
                        oBERol = ValidarDatos();
                        oBERol2 = ValidarDatosRol2();
                        if (oBERol.Nombre == oBERol2.Nombre) { throw new Exception("Error: No se pueden asociar dos Roles iguales al mismo Usuario, por lo que no se va a poder Eliminar!"); }
                        else
                        {
                            if (txtIDUsuario.Text.Length > 0)
                            {
                                oBLLUsuario.DesasoriarUnRolDentroOtroRol(oBEUsuario, oBERol2, oBERol);
                                MessageBox.Show($"Se ha deasociado correctamente el Rol: \"{oBERol2.Nombre}\" del Rol: \"{oBERol.Nombre}\"!", "Éxito:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                            }
                            else { throw new Exception("Error: Primero tiene que seleccionar al Usuario!"); }
                        }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar los dos Roles!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar al Usuario que le va a asignar los roles correspondiente!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarPermisoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDPermiso.Text.Length > 0)
                    {
                        oBEPermiso = ValidarDatosPermiso();
                        if (oBEPermiso != null)
                        {
                            oBLLUsuario.AsociarPermisoAUsuario(oBEUsuario, oBEPermiso);
                            LimpiarCampos();
                            MessageBox.Show("Se ha Asociado correctamente el Permiso al Usuario!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar a un Permiso!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar a un Usuario!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarPermisoSuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIDUsuario.Text.Length > 0)
                {
                    if (txtIDPermiso.Text.Length > 0)
                    {
                        oBEPermiso = ValidarDatosPermiso();
                        if (oBEPermiso != null)
                        {
                            oBLLUsuario.DesasociarPermisoAUsuario(oBEUsuario, oBEPermiso);
                            LimpiarCampos();
                            MessageBox.Show("Se ha Desasociado correctamente el Permiso al Usuario!", "Felicidades!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar a un Permiso!"); }
                }
                else { throw new Exception("Error: Primero tiene que seleccionar a un Usuario!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LimpiarCampos()
        {
            oBERol = null;
            CargarComboBoxSubMenus();
            CargarComboBoxRoles();
            CargarTreeViewUsuarios();
            CargarTreeViewRoles();
            CargarTreeViewPermisos();
            CargarTreeViewRolPermisos();
            CargarTreeViewUsuarioRolPermisos();
            txtIDUsuario.Text = string.Empty.Trim();
            txtNombre.Text = string.Empty.Trim();
            txtPassword.Text = string.Empty.Trim();
            txtIDRol.Text = string.Empty.Trim();
            txtNombreRol.Text = string.Empty.Trim();
            txtIDPermiso.Text = string.Empty.Trim();
            txtNombrePermiso.Text = string.Empty.Trim();
            chbActivo.Checked = false;
            chbBloqueado.Checked = false;
            chbDescifrar.Checked = false;
            treeVwUsuarioPermisosRoles.Nodes.Clear();
        }

        private BERol ValidarDatos()
        {
            try
            {
                if (txtNombreRol.Text.Length > 0)
                {
                    nwRegex = new Regex("^[a-z ]+$");
                    if (nwRegex.IsMatch(txtNombreRol.Text.Trim()))
                    {
                        if (txtIDRol.Text.Length > 0)
                        {
                            int pId = int.Parse(txtIDRol.Text.Trim());
                            oBERol = new BERol(pId, txtNombreRol.Text.Trim());
                            return oBERol;
                        }
                        else
                        {
                            oBERol = new BERol(0, txtNombreRol.Text.Trim());
                            return oBERol;
                        }
                    }
                    else { throw new Exception("Error: El nombre del rol solo acepta palabras sin mayúsculas, ni números, ni caracteres especialess!"); }
                }
                else { throw new Exception("Error: No se puede asignar un nombre nulo al Rol!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        private BERol ValidarDatosRol2()
        {
            try
            {
                if (txtNombreRoles.Text.Length > 0)
                {
                    nwRegex = new Regex("^[a-z ]+$");
                    if (nwRegex.IsMatch(txtNombreRoles.Text.Trim()))
                    {
                        if (txtIDRoles.Text.Length > 0)
                        {
                            int pId = int.Parse(txtIDRoles.Text.Trim());
                            oBERol2 = new BERol(pId, txtNombreRoles.Text.Trim());
                            return oBERol2;
                        }
                        else { throw new Exception("Error: Tiene que seleccionar el Rol de la Lista que se despliegua del ComboBox!"); }
                    }
                    else { throw new Exception("Error: El nombre del rol solo acepta palabras sin mayúsculas, ni números, ni caracteres especialess!"); }
                }
                else { throw new Exception("Error: No se puede asignar un nombre nulo al Rol!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        private BEPermiso ValidarDatosPermiso()
        {
            try
            {
                if (txtNombrePermiso.Text.Length > 0)
                {
                    if (txtIDPermiso.Text.Length > 0)
                    {
                        int pId = int.Parse(txtIDPermiso.Text.Trim());
                        oBEPermiso = new BEPermiso(pId, txtNombrePermiso.Text.Trim());
                        return oBEPermiso;
                    }
                    else
                    {
                        oBEPermiso = new BEPermiso(0, txtNombrePermiso.Text.Trim());
                        return oBEPermiso;
                    }
                }
                else { throw new Exception("Error: No se puede asignar un nombre nulo al Permiso!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }
        private void CargarTreeViewUsuarios()
        {
            treeVwUsuarios.Nodes.Clear();
            TreeNode nodoRaiz = new TreeNode("Usuarios");
            treeVwUsuarios.Nodes.Add(nodoRaiz);
            List<BEUsuario> listaUsuarios = oBLLUsuario.ListarTodo();
            if (listaUsuarios != null)
            {
                foreach (BEUsuario usuario in listaUsuarios)
                {
                    TreeNode nodo = new TreeNode(usuario.Id + ", " + usuario.Usuario);
                    nodoRaiz.Nodes.Add(nodo);
                }
            }
            treeVwUsuarios.ExpandAll();
        }

        private void treeVwUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Text == "Usuarios")
                {
                }
                else
                {
                    if (e.Node.Parent != null && e.Node.Parent.Text == "Usuarios")
                    {
                        string usuario = e.Node.Text;
                        string[] fraccionar = usuario.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (fraccionar.Length == 2)
                        {
                            string idUsuario = fraccionar[0].Trim();
                            string nombreUsuario = fraccionar[1].Trim();
                            oBEUsuario.Id = int.Parse(idUsuario);
                            oBEUsuario.Usuario = nombreUsuario;
                            oBEUsuario = oBLLUsuario.ListarObjeto(oBEUsuario);
                            txtIDUsuario.Text = oBEUsuario.Id.ToString().Trim();
                            txtNombre.Text = oBEUsuario.Usuario.Trim();
                            txtPassword.Text = oBEUsuario.Password.Trim();
                            chbActivo.Checked = oBEUsuario.Activo;
                            chbBloqueado.Checked = oBEUsuario.Bloqueado;
                            CargarTreeViewUsuarioRolPermisos();
                        }
                        else { throw new Exception("Error: No se pudo recuperar el usuario y su Id!"); }
                    }
                    else { throw new Exception("Error: No existe Usuarios registrados!"); }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarTreeViewRoles()
        {
            //Limpio el Treeview:
            treeVwRoles.Nodes.Clear();
            //Creo un nodo raíz "Roles":
            TreeNode nodoRiz = new TreeNode("Roles");
            treeVwRoles.Nodes.Add(nodoRiz);
            List<BERol> listaRoles = oBLLRol.ListarTodo();
            if (listaRoles != null)
            {
                var rolesCategoria1 = listaRoles.ToList();
                //Agrego a cada Rol de categoría 1 al nodo Raíz:
                foreach (BERol rol in rolesCategoria1)
                {
                    TreeNode nodoRol = new TreeNode(rol.Nombre);
                    nodoRiz.Nodes.Add(nodoRol);
                    ArmarTreeViewRoles(rol, nodoRol);
                }
            }
            treeVwRoles.ExpandAll();
        }
        private void ArmarTreeViewRoles(BEComposite oBEComposite, TreeNode nodo)

        {
            if (oBEComposite.ObtenerHijos() != null)
            {
                foreach (BEComposite hijo in oBEComposite.ObtenerHijos())
                {
                    TreeNode nuevoNode = new TreeNode(hijo.Nombre);
                    nodo.Nodes.Add(nuevoNode);

                    if (hijo is BERol)
                    {
                        ArmarTreeViewRoles(hijo, nuevoNode);
                    }
                }
            }
        }

        private void treeVwRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Parent != null && e.Node.Parent.Text == "Roles")
                {
                    string rol = e.Node.Text;
                    oBERol = new BERol(0, rol);
                    oBERol = oBLLRol.ListarObjeto(oBERol);
                    CargarTreeViewRolPermisos();
                    txtIDRol.Text = oBERol.Id.ToString().Trim();
                    txtNombreRol.Text = oBERol.Nombre.Trim();
                }
                else
                {
                    treeVwPermisosPorRol.Nodes.Clear();
                    treeVwUsuarioPermisosRoles.Nodes.Clear();
                    txtIDRol.Text = string.Empty.Trim();
                    txtNombreRol.Text = string.Empty.Trim();
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarTreeViewPermisos()
        {
            try
            {
                //Recupero la lista de permisos:
                List<BEPermiso> listaPermisos = oBLLPermiso.ListarTodo();
                //Verifico que no este vacia:
                if (listaPermisos.Count > 0)
                {
                    treeVwPermisos.Nodes.Clear();
                    //Cargo el MenuStrip:
                    foreach (ToolStripMenuItem menuItem in menuStrip.Items)
                    {
                        TreeNode nodoMenuItem = new TreeNode(menuItem.Text);
                        // Cargo los subitems del menú y verifico si esta de alta en algun permisos:
                        if (CargarSubItems(menuItem, nodoMenuItem, listaPermisos)) { treeVwPermisos.Nodes.Add(nodoMenuItem); }
                    }
                    treeVwPermisos.ExpandAll();
                }
                else { treeVwPermisos.Nodes.Clear(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool CargarSubItems(ToolStripMenuItem menuItem, TreeNode nodoPadre, List<BEPermiso> listaPermisos)
        {
            bool existePermiso = false;

            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    TreeNode nodoSubItem = new TreeNode(subMenuItem.Text);
                    //Verifico si el subitem tiene permisos o si alguno de sus subitems tiene permisos:
                    if (listaPermisos.Any(p => p.Nombre == subMenuItem.Text) || CargarSubItems(subMenuItem, nodoSubItem, listaPermisos))
                    {
                        nodoPadre.Nodes.Add(nodoSubItem);
                        existePermiso = true;
                    }
                }
            }
            //Verifico si el nodo padre tiene permisos directos:
            if (listaPermisos.Any(p => p.Nombre == menuItem.Text)) { existePermiso = true; }
            return existePermiso;
        }

        private void treeVwPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode nodoSeleccionado = e.Node;

                if (nodoSeleccionado != null)
                {
                    if (nodoSeleccionado.Parent == null)
                    {
                        SelectComboBoxItem(cmbMenu, nodoSeleccionado.Text);
                    }
                    else if (nodoSeleccionado.Parent.Parent == null)
                    {
                        //Busca y selecciona el elemento en el ComboBox para el item:
                        SelectComboBoxItem(cmbMenu, nodoSeleccionado.Parent.Text);
                        //Busca y selecciona el elemento en el ComboBox para el subItem:
                        SelectComboBoxItem(cmbItem, nodoSeleccionado.Text);
                    }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void SelectComboBoxItem(ComboBox comboBox, string text)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            MessageBox.Show($"El elemento '{text}' no se encuentra en el ComboBox.", "Elemento no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void CargarTreeViewRolPermisos()
        {
            treeVwPermisosPorRol.Nodes.Clear();
            if (oBERol != null)
            {
                oBERol = oBLLRol.ListarObjeto(oBERol);
                TreeNode nodoRaiz = new TreeNode(oBERol.Nombre.Trim());
                treeVwPermisosPorRol.Nodes.Add(nodoRaiz);
                CargarNodosHijos(nodoRaiz, oBERol.ObtenerHijos());
                treeVwPermisosPorRol.ExpandAll();
            }
            else { treeVwPermisosPorRol.Nodes.Clear(); }
        }

        private void CargarNodosHijos(TreeNode nodoPadre, IList<BEComposite> hijosComposite)
        {
            if (hijosComposite != null)
            {
                foreach (var hijo in hijosComposite)
                {
                    if (hijo is BERol rolHijo)
                    {
                        TreeNode nodoHijo = new TreeNode(rolHijo.Nombre);
                        nodoPadre.Nodes.Add(nodoHijo);
                        CargarNodosHijos(nodoHijo, rolHijo.ObtenerHijos());
                    }
                    else if (hijo is BEPermiso permisoIndividual)
                    {
                        //Busco el padre del subitem en el MenuStrip:
                        foreach (ToolStripMenuItem item in menuStrip.Items)
                        {
                            //Verifico si el item es un ToolStripMenuItem y no un ToolStripSeparator:
                            if (item is ToolStripMenuItem)
                            {
                                foreach (var subItem in item.DropDownItems)
                                {
                                    // Verifico si el subItem es un ToolStripMenuItem y no un ToolStripSeparator:
                                    if (subItem is ToolStripMenuItem menuItem && menuItem.Text == permisoIndividual.Nombre)
                                    {
                                        TreeNode nodoItem = nodoPadre.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == item.Text);
                                        if (nodoItem == null)
                                        {
                                            nodoItem = new TreeNode(item.Text);
                                            nodoPadre.Nodes.Add(nodoItem);
                                        }
                                        TreeNode nodoPermiso = new TreeNode(permisoIndividual.Nombre);
                                        nodoItem.Nodes.Add(nodoPermiso);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeVwPermisosRol_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Parent != null)
                {
                    //Verifico el Nodo: 
                    if (e.Node.Parent.Parent == null)
                    {
                        //Nodo padre es el nodo raíz del rol principal:
                        string rolNombre = e.Node.Text;
                        for (int i = 0; i < cmbMenu.Items.Count; i++)
                        {
                            if (cmbMenu.Items[i] is ToolStripMenuItem menuItem && menuItem.Text == rolNombre)
                            {
                                cmbMenu.SelectedIndex = i;
                                break;
                            }
                        }
                        //Limpia y Selecciona la primera opción del ComboBox de permisos:
                        if (cmbItem.Items.Count > 0)
                        {
                            cmbItem.SelectedIndex = 0;
                        }
                        txtIDPermiso.Text = string.Empty;
                        txtNombrePermiso.Text = string.Empty;
                    }
                    //Verifico si el nodo seleccionado es un permiso:
                    else if (e.Node.Parent.Parent != null)
                    {
                        string permisoNombre = e.Node.Text;
                        oBEPermiso = new BEPermiso(0, permisoNombre);
                        oBEPermiso = oBLLPermiso.ListarObjeto(oBEPermiso);
                        txtIDPermiso.Text = oBEPermiso.Id.ToString().Trim();
                        txtNombrePermiso.Text = oBEPermiso.Nombre.Trim();
                        //Obtengo el rol asociado al permiso:
                        string rolNombreAsociado = e.Node.Parent.Text;
                        //Busca y selecciona el rol asociado en el ComboBox:
                        for (int i = 0; i < cmbMenu.Items.Count; i++)
                        {
                            if (cmbMenu.Items[i] is ToolStripMenuItem menuItem && menuItem.Text == rolNombreAsociado)
                            {
                                cmbMenu.SelectedIndex = i;
                                break;
                            }
                        }
                        //Actualiza el ComboBox de permisos después de seleccionar el rol:
                        cmbMenu_SelectedIndexChanged(cmbMenu, EventArgs.Empty);

                        //Busca y selecciona el permiso en el ComboBox:
                        for (int i = 0; i < cmbItem.Items.Count; i++)
                        {
                            if (cmbItem.Items[i] is ToolStripMenuItem menuItem && menuItem.Text == permisoNombre)
                            {
                                cmbItem.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarTreeViewUsuarioRolPermisos()
        {
            treeVwUsuarioPermisosRoles.Nodes.Clear();
            if (oBEUsuario != null && oBEUsuario.Id > 0)
            {
                TreeNode nodoRaiz = new TreeNode(oBEUsuario.Usuario.Trim());
                treeVwUsuarioPermisosRoles.Nodes.Add(nodoRaiz);
                //Obtengo la jerarquía de roles y permisos del usuario:
                oBEUsuario = oBLLUsuario.ListarObjetoJerarquico(oBEUsuario);
                IList<BERol> rolesUsuario = oBEUsuario.listaRoles;
                IList<BEPermiso> permisosUsuario = oBEUsuario.listaPermisos;
                if (rolesUsuario != null)
                {
                    foreach (var rolUsuario in rolesUsuario)
                    {
                        //Obtengo el rol con su jerarquía completa:
                        TreeNode nodoRol = new TreeNode(rolUsuario.Nombre.Trim());
                        nodoRaiz.Nodes.Add(nodoRol);
                        List<BEComposite> subLista = rolUsuario.listaPermisos;
                        AgregarRolesYPermisosAlTreeView(subLista, nodoRol);
                    }
                }
                if (permisosUsuario != null)
                {
                    foreach (var permisoUsuario in permisosUsuario)
                    {
                        AgregarPermisoAlTreeView(menuStrip, permisoUsuario.Nombre, nodoRaiz);
                    }
                }
                //treeVwUsuarioPermisosRoles.ExpandAll();
            }
            else
            {
                treeVwUsuarioPermisosRoles.Nodes.Clear();
            }
        }

        private void AgregarRolesYPermisosAlTreeView(List<BEComposite> lista, TreeNode nodoPadre)
        {
            //Añado los permisos y subroles del rol:
            if (lista != null)
            {
                foreach (var componente in lista)
                {
                    if (componente is BERol subRol)
                    {
                        List<BEComposite> subLista = subRol.listaPermisos;
                        if (subLista != null)
                        {
                            //Verifico si el subRol ya existe bajo el nodoPadre:
                            TreeNode nodoSubRolExistente = MetodoNodos(nodoPadre, subRol.Nombre);
                            if (nodoSubRolExistente == null)
                            {
                                TreeNode nodoSubRol = new TreeNode(subRol.Nombre);
                                nodoPadre.Nodes.Add(nodoSubRol);
                                //Método recursivo para añadir subroles y permisos:
                                AgregarRolesYPermisosAlTreeView(subLista, nodoSubRol);
                            }
                            else
                            {
                                //Continuo con el nodo existente:
                                AgregarRolesYPermisosAlTreeView(subLista, nodoSubRolExistente);
                            }
                        }
                    }
                    else if (componente is BEPermiso permiso)
                    {
                        AgregarPermisoAlTreeView(menuStrip, permiso.Nombre, nodoPadre);
                    }
                }
            }
        }

        private void AgregarPermisoAlTreeView(MenuStrip menuStrip, string permisoNombre, TreeNode nodoPadre)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                //Verifico si el item es un ToolStripMenuItem y no un ToolStripSeparator:
                if (item is ToolStripMenuItem)
                {
                    foreach (var subItem in item.DropDownItems)
                    {
                        if (subItem is ToolStripMenuItem menuItem && menuItem.Text == permisoNombre)
                        {
                            //Agrego el item padre si no existe en el TreeView:
                            TreeNode nodoItemPadre = MetodoNodos(nodoPadre, item.Text);
                            //Agrego el subitem al nodo padre encontrado o creado:
                            TreeNode nodoPermiso = new TreeNode(menuItem.Text);
                            nodoItemPadre.Nodes.Add(nodoPermiso);
                            return;
                        }
                    }
                }
            }
        }

        private TreeNode MetodoNodos(TreeNode nodoPadre, string textoNodo)
        {
            foreach (TreeNode nodo in nodoPadre.Nodes) { if (nodo.Text == textoNodo) { return nodo; } }
            TreeNode nuevoNodo = new TreeNode(textoNodo);
            nodoPadre.Nodes.Add(nuevoNodo);
            return nuevoNodo;
        }

        private void treeVwRolesPermisosUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode nodoSeleccionado = e.Node;
                if (nodoSeleccionado != null)
                {
                    string textoNodoSeleccionado = nodoSeleccionado.Text;
                    oBERol = new BERol(0, textoNodoSeleccionado);
                    oBEPermiso = new BEPermiso(0, textoNodoSeleccionado);
                    //En el caso de que sea el Nodo principal es el Usuario:
                    if (nodoSeleccionado.Parent == null)
                    {
                        string nombreUsuario = nodoSeleccionado.Text;
                        oBEUsuario = oBLLUsuario.ListarObjeto(oBEUsuario);
                        if (oBEUsuario != null)
                        {
                            txtIDUsuario.Text = oBEUsuario.Id.ToString().Trim();
                            txtNombre.Text = oBEUsuario.Usuario.Trim();
                            txtPassword.Text = oBEUsuario.Password.Trim();
                            chbActivo.Checked = oBEUsuario.Activo;
                            chbBloqueado.Checked = oBEUsuario.Bloqueado;
                            txtIDRol.Text = string.Empty;
                            txtNombreRol.Text = string.Empty;
                            txtIDPermiso.Text = string.Empty;
                            txtNombrePermiso.Text = string.Empty;
                            if (cmbItem.SelectedIndex > 0) { SelectComboBoxItem(cmbItem, cmbItem.Items[0].ToString()); }
                            if (cmbMenu.SelectedIndex > 0) { SelectComboBoxItem(cmbMenu, cmbMenu.Items[0].ToString()); }
                        }
                    }
                    //En Caso que el Nodo sea un Rol:
                    else if (oBLLRol.VerificarExistenciaObjeto(oBERol) == true)
                    {
                        oBERol = oBLLRol.ListarObjeto(oBERol);
                        txtIDRol.Text = oBERol.Id.ToString().Trim();
                        txtNombreRol.Text = oBERol.Nombre.Trim();
                        txtIDPermiso.Text = string.Empty;
                        txtNombrePermiso.Text = string.Empty;
                        if (cmbMenu.SelectedIndex > 0)
                        {
                            SelectComboBoxItem(cmbItem, cmbItem.Items[0].ToString());
                            SelectComboBoxItem(cmbMenu, cmbMenu.Items[0].ToString());
                        }
                    }
                    //En caso que el Permiso este asociado de forma independiente:
                    else if (oBLLPermiso.VerificarExistenciaObjeto(oBEPermiso) == true)
                    {
                        oBEPermiso = oBLLPermiso.ListarObjeto(oBEPermiso);
                        txtIDPermiso.Text = oBEPermiso.Id.ToString().Trim();
                        txtNombrePermiso.Text = oBEPermiso.Nombre.Trim();
                        txtIDRol.Text = string.Empty;
                        txtNombreRol.Text = string.Empty;
                        SelectComboBoxItem(cmbMenu, nodoSeleccionado.Parent.Text);
                        SelectComboBoxItem(cmbItem, nodoSeleccionado.Text);
                        if (nodoSeleccionado.Parent.Parent != null)
                        {
                            oBERol = new BERol(0, nodoSeleccionado.Parent.Parent.Text);
                            //En Caso que el Nodo sea un Rol:
                            if (oBLLRol.VerificarExistenciaObjeto(oBERol) == true)
                            {
                                oBERol = oBLLRol.ListarObjeto(oBERol);
                                txtIDRol.Text = oBERol.Id.ToString().Trim();
                                txtNombreRol.Text = oBERol.Nombre.Trim();
                            }
                        }
                    }
                    else
                    {
                        SelectComboBoxItem(cmbMenu, textoNodoSeleccionado);
                        SelectComboBoxItem(cmbItem, cmbItem.Items[0].ToString());
                        if (nodoSeleccionado.Parent.Parent != null)
                        {
                            if (nodoSeleccionado.Parent.Parent.Text == oBEUsuario.Usuario)
                            {
                                oBERol = new BERol(0, nodoSeleccionado.Parent.Text);
                                //En Caso que el Nodo sea un Rol:
                                if (oBLLRol.VerificarExistenciaObjeto(oBERol) == true)
                                {
                                    oBERol = oBLLRol.ListarObjeto(oBERol);
                                    txtIDRol.Text = oBERol.Id.ToString().Trim();
                                    txtNombreRol.Text = oBERol.Nombre.Trim();
                                }
                            }
                            else if (nodoSeleccionado.Parent != null)
                            {
                                oBERol = new BERol(0, nodoSeleccionado.Parent.Parent.Text);
                                //En Caso que el Nodo sea un Rol:
                                if (oBLLRol.VerificarExistenciaObjeto(oBERol) == true)
                                {
                                    oBERol = oBLLRol.ListarObjeto(oBERol);
                                    txtIDRol.Text = oBERol.Id.ToString().Trim();
                                    txtNombreRol.Text = oBERol.Nombre.Trim();
                                }
                                else
                                {
                                    txtIDRol.Text = string.Empty;
                                    txtNombreRol.Text = string.Empty;
                                    txtIDPermiso.Text = string.Empty;
                                    txtNombrePermiso.Text = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarComboBoxSubMenus()
        {
            List<object> listaSubMenus = new List<object>();
            //Agrego un string vacío al principio:
            listaSubMenus.Add(new ToolStripMenuItem { Text = "" });

            var frmMDI = this.MdiParent as frmMDI;
            if (frmMDI != null && frmMDI.MainMenuStrip != null)
            {
                MenuStrip subMenu = frmMDI.MainMenuStrip;
                foreach (var item in subMenu.Items)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        //Anulo la carga de las opciones porque está maximizada la ventana:
                        if (!string.IsNullOrEmpty(menuItem.Text) && menuItem.Text != "System.Windows.Forms.ToolStripMenuItem" && menuItem.Text != "&Cerrar"
                             && menuItem.Text != "&Restaurar" && menuItem.Text != "Mi&nimizar")
                        {
                            listaSubMenus.Add(menuItem);
                        }
                    }
                }
            }
            cmbMenu.DataSource = listaSubMenus;
            cmbMenu.DisplayMember = "Text";
        }

        private void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbItem.DataSource = null;
            if (cmbMenu.SelectedItem is ToolStripMenuItem subMenuSeleccionado)
            {
                if (subMenuSeleccionado.Text == "")
                {
                    txtNombrePermiso.Text = string.Empty;
                }
                else
                {
                    List<ToolStripMenuItem> listaPermisos = new List<ToolStripMenuItem>();
                    //Agrego un elemento con texto vacío al principio de la lista del combo Box de los subItems:
                    listaPermisos.Add(new ToolStripMenuItem(""));

                    foreach (var permiso in subMenuSeleccionado.DropDownItems)
                    {
                        if (permiso is ToolStripMenuItem menuItem && !string.IsNullOrEmpty(menuItem.Text) && menuItem.Text != "System.Windows.Forms.ToolStripMenuItem")
                        {
                            listaPermisos.Add(menuItem);
                        }
                    }
                    txtIDPermiso.Text = string.Empty;
                    txtNombrePermiso.Text = subMenuSeleccionado.Text;
                    cmbItem.DataSource = listaPermisos;
                    cmbItem.DisplayMember = "Text";
                }
            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItem.SelectedItem is ToolStripMenuItem selectedItem)
            {
                //Cargo los datos del item en los textboxes:
                string itemText = selectedItem.Text;
                oBEPermiso = new BEPermiso(0, itemText.Trim());
                if (oBLLPermiso.VerificarExistenciaObjeto(oBEPermiso))
                {
                    oBEPermiso = oBLLPermiso.ListarObjeto(oBEPermiso);
                    txtIDPermiso.Text = oBEPermiso.Id.ToString().Trim();
                    txtNombrePermiso.Text = oBEPermiso.Nombre.Trim();
                }
                else
                {
                    txtIDPermiso.Text = string.Empty.Trim();
                    txtNombrePermiso.Text = itemText.Trim();
                }

                if (selectedItem.DropDownItems.Count > 0)
                {
                    //Si el item tiene subitems, agregarlos al combobox de subitems:
                    List<ToolStripMenuItem> subItemsList = new List<ToolStripMenuItem>();
                    subItemsList.Add(new ToolStripMenuItem("")); // Agregar un string vacío como primer elemento
                    foreach (ToolStripMenuItem subItem in selectedItem.DropDownItems)
                    {
                        subItemsList.Add(subItem);
                    }
                }
            }
        }
        private void CargarComboBoxRoles()
        {
            List<BERol> listaRoles = new List<BERol>();
            listaRoles.Add(new BERol(-1, ""));
            listaRoles.AddRange(oBLLRol.ListarTodo());
            if (listaRoles.Count > 0)
            {
                cmbRoles.DataSource = null;
                cmbRoles.DataSource = listaRoles;
                cmbRoles.DisplayMember = "Nombre";
            }
            else { cmbRoles.DataSource = null; }
        }

        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRoles.SelectedIndex == -1) { }
                else if (cmbRoles.Items.Count > 0)
                {
                    if (cmbRoles.SelectedItem is BERol selectedRol)
                    {
                        string textoSeleccionado = selectedRol.Nombre;
                        if (textoSeleccionado != "")
                        {
                            oBERol2 = oBLLRol.ListarObjeto(new BERol(0, textoSeleccionado));
                            txtIDRoles.Text = oBERol2.Id.ToString().Trim();
                            txtNombreRoles.Text = oBERol2.Nombre.Trim();
                        }
                        else
                        {
                            txtIDRoles.Text = string.Empty.Trim();
                            txtNombreRoles.Text = string.Empty.Trim();
                        }
                    }
                }
                else { throw new Exception("Error: No Existen Roles para asociar!"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void chbDescifrar_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length > 0)
            {
                if (chbDescifrar.Checked == true)
                {
                    string password = txtPassword.Text.Trim();
                    password = oBLLUsuario.DesencriptarPassword(password);
                    txtPassword.Text = password;
                }
                else
                {
                    string password = txtPassword.Text.Trim();
                    password = oBLLUsuario.EncriptarPassword(password);
                    txtPassword.Text = password;
                }
            }
            else { }
        }
    }
}
