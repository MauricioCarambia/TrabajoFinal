using BLL;
using Entity;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace UI
{
    public partial class frmGestionUsuarios : Form
    {
        private BLLRoles rolBLL = new BLLRoles();
        private BLLUsuarios usuarioBLL = new BLLUsuarios();
        private BLLPermisos permisoBLL = new BLLPermisos();

        private BERoles rolSeleccionado;
        private BEPermisos permisoSeleccionado;
        private Usuarios usuarioSeleccionado;


        public frmGestionUsuarios()
        {
            InitializeComponent();
            LimpiarCampos();
            CargarUsuariosTree();
            CargarRolesTree();
            CargarPermisosTree();
        }


        private void btnAltaRol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreRol.Text)) return;

            BERoles r = new BERoles
            {
                NombreRol = txtNombreRol.Text
            };

            rolBLL.GuardarRol(r);
            LimpiarCampos();
            CargarRolesTree();
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            if (rolSeleccionado != null && int.TryParse(txtIDRol.Text, out int idRol))
            {
                rolSeleccionado.Id = idRol;
                rolSeleccionado.NombreRol = txtNombreRol.Text;

                rolBLL.GuardarRol(rolSeleccionado);
                CargarRolesTree();
                LimpiarCampos();
            }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIDRol.Text, out int idRol))
            {
                var result = MessageBox.Show("¿Está seguro que desea eliminar el rol?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    rolBLL.EliminarRol(idRol);
                    LimpiarCampos();
                    CargarRolesTree();
                }
            }
        }

        private void treeRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BERoles r)
            {
                rolSeleccionado = r;
                txtIDRol.Text = r.Id.ToString();
                txtNombreRol.Text = r.NombreRol;
            }
        }

        private void CargarRolesTree()
        {
            treeRoles.Nodes.Clear();
            var roles = rolBLL.ListarTodo();
            TreeNode root = new TreeNode("Roles");
            treeRoles.Nodes.Add(root);

            foreach (var r in roles)
            {
                TreeNode nodo = new TreeNode($"{r.Id}-{r.NombreRol}");
                nodo.Tag = r;
                root.Nodes.Add(nodo);
            }

            treeRoles.ExpandAll();
        }





        private void btnAltaPermiso_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombrePermiso.Text)) return;

            BEPermisos p = new BEPermisos
            {
                Id = permisoSeleccionado?.Id ?? 0,
                Nombre = txtNombrePermiso.Text,
                Menu = cmbMenu.SelectedItem?.ToString() ?? "",
                Item = cmbItem.SelectedItem?.ToString() ?? ""
            };

            permisoBLL.GuardarPermiso(p);
            LimpiarCampos();
            CargarPermisosTree();
        }

        private void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (permisoSeleccionado != null)
            {
                permisoBLL.EliminarPermiso(permisoSeleccionado.Id);
                LimpiarCampos();
                CargarPermisosTree();
            }
        }

        private void treePermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BEPermisos p)
            {
                permisoSeleccionado = p;
                txtIDPermiso.Text = p.Id.ToString();
                txtNombrePermiso.Text = p.Nombre;
                cmbMenu.SelectedItem = p.Menu;
                cmbItem.SelectedItem = p.Item;
            }
        }

        private void CargarPermisosTree()
        {
            treePermisos.Nodes.Clear();
            var permisos = permisoBLL.ListarTodo();
            TreeNode root = new TreeNode("Permisos");
            treePermisos.Nodes.Add(root);

            foreach (var p in permisos)
            {
                TreeNode nodo = new TreeNode($"{p.Id}-{p.Nombre}");
                nodo.Tag = p;
                root.Nodes.Add(nodo);
            }

            treePermisos.ExpandAll();
        }


        #region ABM Usuarios

        private void CargarUsuariosTree()
        {
            treeUsuarios.Nodes.Clear();
            var usuarios = usuarioBLL.ObtenerUsuarios();
            TreeNode root = new TreeNode("Usuarios");
            treeUsuarios.Nodes.Add(root);

            foreach (var u in usuarios)
            {
                TreeNode nodo = new TreeNode($"{u.Id}-{u.NombreUsuario}");
                nodo.Tag = u;
                root.Nodes.Add(nodo);
            }

            treeUsuarios.ExpandAll();
        }

        private void treeUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Usuarios u)
            {
                usuarioSeleccionado = u;
                txtID.Text = u.Id.ToString();
                txtNombre.Text = u.NombreUsuario;
                txtPassword.Text = u.Contrasenia;
            }
        }

        #endregion

        private void LimpiarCampos()
        {
            txtIDRol.Clear();
            txtNombreRol.Clear();
            txtIDPermiso.Clear();
            txtNombrePermiso.Clear();
            txtID.Clear();
            txtNombre.Clear();
            txtPassword.Clear();

            rolSeleccionado = null;
            permisoSeleccionado = null;
            usuarioSeleccionado = null;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
