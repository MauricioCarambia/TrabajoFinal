using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;

namespace UI
{
    public partial class frmABMUsuarios : Form
    {
        private BLLUsuarios usuarioBLL = new BLLUsuarios();
        private int idSeleccionado = -1;
        public frmABMUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuarios u = new Usuarios
            {
                Nombre = txtUsuario.Text,
                Contrasenia = txtContrasenia.Text
            };
            usuarioBLL.AgregarUsuario(u);
            CargarUsuarios();
            LimpiarCampos();
        }
        private void CargarUsuarios()
        {
            var lista = usuarioBLL.ObtenerUsuarios();
            dgvUsuarios.DataSource = lista.ToList();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1) return;

            Usuarios u = new Usuarios
            {
                Id = idSeleccionado,
                Nombre = txtUsuario.Text,
                Contrasenia = txtContrasenia.Text
            };
            usuarioBLL.ModificarUsuario(u);
            CargarUsuarios();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1) return;
            usuarioBLL.EliminarUsuario(idSeleccionado);
            CargarUsuarios();
            LimpiarCampos();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;
            idSeleccionado = (int)dgvUsuarios.CurrentRow.Cells[0].Value;
            txtUsuario.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtContrasenia.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
        }
        private void LimpiarCampos()
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            idSeleccionado = -1;
        }
    }
}
