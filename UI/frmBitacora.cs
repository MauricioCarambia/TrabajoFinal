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
    public partial class frmBitacora : Form
    {
        BEBitacora oBEBitacora;
        BEUsuario oBEUsuario;
        BLLBitacora oBLLBitacora;
        BLLUsuario oBLLUsuario;
        public frmBitacora()
        {
            InitializeComponent();
            oBEBitacora = new BEBitacora();
            oBEUsuario = new BEUsuario();
            oBLLBitacora = new BLLBitacora();
            oBLLUsuario = new BLLUsuario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
           
            CargarDGVBitacoras();
           
            //txtBxBitacoraUsuario.Text = string.Empty.Trim();
        }
        
        private void CargarDGVBitacoras()
        {
            try
            {
                dgvBitacora.DataSource = null;
                if (oBLLBitacora.ListarTodo() != null)
                {
                    dgvBitacora.DataSource = oBLLBitacora.ListarTodo();
                    dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dgvBitacora.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    dgvBitacora.Columns[0].HeaderText = "Fecha Registro";
                    dgvBitacora.Columns[2].HeaderText = "Id Usuario";
                }
                else { }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
