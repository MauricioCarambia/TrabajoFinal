namespace UI
{
    public partial class frmMDI : Form
    {
        public frmMDI()
        {
            InitializeComponent();
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

        private void registrarPlatoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registrarPlatoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPlatos frm = new frmPlatos();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void verPlatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmABMUsuarios frm = new frmABMUsuarios();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void permisosRolesUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionUsuarios frm = new frmGestionUsuarios();
            frm.MdiParent = this;   // le decís que el padre es el MDI actual
            frm.Show();
        }

        private void frmMDI_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
