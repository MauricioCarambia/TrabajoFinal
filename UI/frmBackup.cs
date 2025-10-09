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
    public partial class frmBackup : Form
    {
        BEBitacora oBEBitacora;
        BEUsuario oBEUsuario;
        BLLBackUp oBLLGestorBD;
        BLLBitacora oBLLBitacora;
        BLLUsuario oBLLUsuario;
        public frmBackup(BEUsuario oBEUsuarioLogueado)
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

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                oBEBitacora = new BEBitacora();
                oBEBitacora.oBEUsuario = oBEUsuario;
                oBEBitacora.Id = 0;
                oBEBitacora.FechaRegistro = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Trim());
                oBEBitacora.Detalle = "backup";
                oBLLBitacora.Guardar(oBEBitacora);
                oBLLGestorBD.CrearBackup();
                MessageBox.Show("Se ha generado el Backup correctamente!", "Felicidades:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarDGVBackups()
        {
            dgvBackUp.DataSource = null;
            if (oBLLBitacora.ListarTodoPorTipo(true) != null)
            {
                dgvBackUp.DataSource = oBLLBitacora.ListarTodoPorTipo(true);
                dgvBackUp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvBackUp.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                dgvBackUp.Columns[2].HeaderText = "Id Usuario:";
            }
        }
      

        private void frmBackup_Load(object sender, EventArgs e)
        {
            CargarDGVBackups();
        }
    }
}
