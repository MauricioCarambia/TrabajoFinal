using BLL;
using BLL.Composite;
using Entity;
using Entity.Composite;

namespace UI
{
    public partial class frmMDI : Form
    {
        BEUsuario oBEUsuario;
        BLLUsuario oBLLUsuario;
        BLLPermiso oBLLPermiso;
        //BERol oBERol;
        BLLRol oBLLRol;
        List<BEPermiso> listaPermisos;
        private MenuStrip oMenuStrip;
        public frmMDI(BEUsuario oBEUsuarioLogueado)
        {
            InitializeComponent();
            oBLLPermiso = new BLLPermiso();
            oBLLUsuario = new BLLUsuario();
            oBLLRol = new BLLRol();
            this.oBEUsuario = oBEUsuarioLogueado;
            listaPermisos = new List<BEPermiso>();
            listaPermisos = oBLLUsuario.ListarTodosLosPermisosDelUsuario(oBEUsuario);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        private void registrarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void verInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsumos frm = new frmInsumos();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }




        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmABMUsuarios frm = new frmABMUsuarios();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void permisosRolesUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionUsuarios frm = new frmGestionUsuarios(menuStrip1);
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void frmMDI_Load(object sender, EventArgs e)
        {
            oMenuStrip = this.menuStrip1;
            OcultarTodosLosItemsDelMenu();
            if (listaPermisos != null) { MostrarItemsSegunPermisos(listaPermisos); }
        }


        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin fmLogin = new frmLogin();
            fmLogin.Show();
        }
        public void OcultarTodosLosItemsDelMenu()
        {
            if (this.menuStrip1 != null)
            {
                //Recorro todos los elementos del menú principal:
                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        //Pongo a todos los items ocultos:
                        menuItem.Visible = false;
                        //Si tiene subitems, se llama de vuelta al método para ocultar los subitems:
                        if (menuItem.DropDownItems.Count > 0) { OcultarItemsDelSubMenu(menuItem.DropDownItems); }
                    }
                }
            }
        }
        private void OcultarItemsDelSubMenu(ToolStripItemCollection items)
        {
            //Recorro todos los elementos del submenú:
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem subMenuItem)
                {
                    //Pongo invisible el elemento del subitem:
                    subMenuItem.Visible = false;
                    //Si el elemento tiene submenús, llamar recursivamente a este método
                    //if (subMenuItem.DropDownItems.Count > 0) { OcultarItemsDelSubMenu(subMenuItem.DropDownItems); }
                }
            }
        }
        private void MostrarItemsSegunPermisos(List<BEPermiso> permisosUsuario)
        {
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    //Si el menú principal tiene subitems, Verifico los permisos:
                    if (menuItem.DropDownItems.Count > 0) { MostrarSubItemsSegunPermisos(menuItem, permisosUsuario); }
                    if (TienePermiso(menuItem.Text, permisosUsuario)) { menuItem.Visible = true; }
                }
            }
        }
        private void MostrarSubItemsSegunPermisos(ToolStripMenuItem menuItem, List<BEPermiso> permisosUsuario)
        {
            bool algunSubItemVisible = false;

            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    //Meotodo recursioa para verificar subitems:
                    if (subMenuItem.DropDownItems.Count > 0) { MostrarSubItemsSegunPermisos(subMenuItem, permisosUsuario); }

                    //Verifico si el subitem tiene permisos:
                    if (TienePermiso(subMenuItem.Text, permisosUsuario))
                    {
                        subMenuItem.Visible = true;
                        algunSubItemVisible = true;
                    }
                }
            }
            //Si algún subitem es visible, el item también debe ser visible:
            if (algunSubItemVisible) { menuItem.Visible = true; }
        }
        private bool TienePermiso(string nombreItem, List<BEPermiso> permisosUsuario)
        {
            //Verifico si algún permiso en la lista de permisos del usuario coincide con el nombre del ítem (ignorando mayúsculas y minúsculas):
            return permisosUsuario.Any(permiso => permiso.Nombre.Equals(nombreItem, StringComparison.OrdinalIgnoreCase));
        }

        private void verBitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBitacora frm = new frmBitacora();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void hacerBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackup frm = new frmBackup(oBEUsuario);
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void haverRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRestore frm = new frmRestore(oBEUsuario);
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void registrarMesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMesas frm = new frmMesas();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void cobrarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCocina frm = new frmCocina();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hacerReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReserva frm = new frmReserva(this);
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void cargarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargarPedido frm = new frmCargarPedido();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void registrarProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProveedores frm = new frmProveedores();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void insumoProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsumoProveedor frm = new frmInsumoProveedor();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void platosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlatos frm = new frmPlatos();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }
    }
}
