using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UI
{
    public partial class frmRestore : Form
    {
        BEBitacora oBEBitacora;
        BEUsuario oBEUsuario;
        BLLBackUp oBLLGestorBD;
        BLLBitacora oBLLBitacora;
        BLLUsuario oBLLUsuario;
        public frmRestore(BEUsuario oBEUsuarioLogueado)
        {
            oBEBitacora = new BEBitacora();
            oBEUsuario = new BEUsuario();
            oBLLGestorBD = new BLLBackUp();
            oBLLBitacora = new BLLBitacora();
            oBLLUsuario = new BLLUsuario();
            InitializeComponent();
            this.oBEUsuario = oBEUsuarioLogueado;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRestore_Load(object sender, EventArgs e)
        {
            ListarBackupsEnTreeView();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeVwRestore.Nodes.Count > 0)
                {
                    if (treeVwRestore.SelectedNode != null)
                    {
                        if (treeVwRestore.SelectedNode.IsSelected == true)
                        {
                            oBEBitacora = new BEBitacora();
                            oBEBitacora.oBEUsuario = oBEUsuario;
                            oBEBitacora.Id = 0;
                            oBEBitacora.FechaRegistro = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Trim());
                            oBEBitacora.Detalle = "restore";
                            string nombreBackup = treeVwRestore.SelectedNode.Text;
                            oBLLGestorBD.CrearRestore(nombreBackup);
                            oBLLBitacora.Guardar(oBEBitacora);

                            MessageBox.Show($"Felicidades! Se ha restaurado correctamente el Backup: {nombreBackup}!", "Felicidades:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else { throw new Exception("Error: Primero tiene que seleccionar un Backup!"); }
                }
                else { throw new Exception("Error: Primero tiene que existir al menos un Backup para poder realizar un Restore!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void ListarBackupsEnTreeView()
        {
            try
            {
                treeVwRestore.Nodes.Clear();
                List<string> listaNombreBackups = oBLLGestorBD.ListarBackups();
                if (listaNombreBackups != null && listaNombreBackups.Count > 0)
                {
                    foreach (string nombreEspecificoBackup in listaNombreBackups)
                    {
                        treeVwRestore.Nodes.Add(new TreeNode(nombreEspecificoBackup));
                    }
                }
                else { treeVwRestore.Nodes.Clear(); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


    }
}
