using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UI
{
    public partial class frmABMUsuarios : Form
    {
        BEUsuario oBEUsuario;
        BLLUsuario oBLLUsuario;
        Regex nwRegex;
        public frmABMUsuarios()
        {
            InitializeComponent();
            oBEUsuario = new BEUsuario();
            oBLLUsuario = new BLLUsuario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                oBEUsuario = ValidarDatos();
                if (oBEUsuario != null)
                {
                    
                        if (oBEUsuario.Id == 0)
                        {
                            
                                oBLLUsuario.Guardar(oBEUsuario);
                                oBEUsuario = oBLLUsuario.ListarObjeto(oBEUsuario);
                        CargarUsuarios();
                        LimpiarCampos();
                                MessageBox.Show("Se ha creado correctamente al Usuario", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                           
                        }
                        else { throw new Exception("Error: No se puede dar el alta a un Usuario que ya existe!"); }
                    
                }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            if (oBLLUsuario.ListarTodo() != null)
            {
                dgvUsuarios.DataSource = oBLLUsuario.ListarTodo();
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            }
            else { dgvUsuarios.DataSource = null; }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.Rows.Count > 0)
                {
                   
                        oBEUsuario = ValidarDatos();
                        if (oBEUsuario != null)
                        {
                            if (oBEUsuario.Id > 0)
                            {
                               
                                    oBLLUsuario.Guardar(oBEUsuario);
                            CargarUsuarios();
                            LimpiarCampos();
                                    MessageBox.Show("Se ha modificado correctamente al Usuario!", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                               
                            }
                            else { throw new Exception("Error: no se puede modificar a un Usuario que todavia no esta registrado!"); }
                        }

                    
                }
                else { throw new Exception("Error: Primero tiene que existir al Menos un Usuario para poder modificarlo!"); }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.Rows.Count > 0)
                {
                    
                        oBEUsuario = ValidarDatos();
                        if (oBEUsuario != null)
                        {
                            if (oBEUsuario.Id > 0)
                            {
                                
                                  
                            oBLLUsuario.Eliminar(oBEUsuario);
                            CargarUsuarios();
                            LimpiarCampos();
                                   
                                
                               
                            }
                            else { throw new Exception("Error: Los datos del Empleado no Coinciden con el del Usuario!"); }
                        }
                    
                }
                else { throw new Exception("Error: Primero tiene que existir al menos un Usuario para poder Eliminarlo!"); }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvUsuarios.Rows.Count > 0)
            {
                oBEUsuario = (BEUsuario)dgvUsuarios.CurrentRow.DataBoundItem;
                if (oBEUsuario != null)
                {
                    txtID.Text = oBEUsuario.Id.ToString().Trim();
                    txtUsuario.Text = oBEUsuario.Usuario.Trim();
                    txtPassword.Text = oBLLUsuario.DesencriptarPassword(oBEUsuario.Password.Trim());
                    //ABMUsuarioCkBxActivo.Checked = oBEUsuario.Activo;
                    //ABMUsuarioCkBxBloqueado.Checked = oBEUsuario.Bloqueado;
                    txtPassword.Text = oBEUsuario.Password.Trim();
                    
                    
                }
                else
                {
                    dgvUsuarios.DataSource = null;
                }
            }
        }
        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmABMUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            LimpiarCampos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

        }
        private BEUsuario ValidarDatos()
        {
            try
            {
                if (txtUsuario.Text.Length > 0)
                {
                    nwRegex = new Regex("^[a-zA-Z0-9]*$");
                    if (nwRegex.IsMatch(txtUsuario.Text.Trim()))
                    {
                        if (txtPassword.Text.Length > 0)
                        {
                            if (nwRegex.IsMatch(txtPassword.Text.Trim()))
                            {
                                oBEUsuario = new BEUsuario();
                                if (txtID.Text.Length > 0) { oBEUsuario.Id = int.Parse(txtID.Text.Trim()); }
                                else { oBEUsuario.Id = 0; }
                                oBEUsuario.Usuario = txtUsuario.Text.Trim();
                                oBEUsuario.Password = txtPassword.Text.Trim();
                                //if (ABMUsuarioCkBxActivo.Checked == true) { oBEUsuario.Activo = true; }
                                //else { oBEUsuario.Activo = false; }
                                //if (ABMUsuarioCkBxBloqueado.Checked == true) { oBEUsuario.Bloqueado = true; }
                                //else { oBEUsuario.Bloqueado = false; }
                                return oBEUsuario;
                            }
                            else { throw new Exception("Error: Error: El Password del Usuario solo acepta palabras y números sin caraceteres especiales únicamente!"); }
                        }
                        else { throw new Exception("Error: El Password no puede ser nulo!"); }
                    }
                    else { throw new Exception("Error: El nombre usuario solo acepta palabras y números sin caraceteres especiales únicamente!"); }
                }
                else { throw new Exception("Error: EL Nombre de Usuario no puede ser nulo!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        private void chkEncriptar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string passwordDescifrado;
                if (oBEUsuario != null)
                {
                    if (chkEncriptar.Checked == true)
                    {
                        if (txtID.Text.Length > 0)
                        {
                            passwordDescifrado = oBLLUsuario.DesencriptarPassword(oBEUsuario.Password.Trim());
                            txtPassword.Text = passwordDescifrado;
                        }
                    }
                    else
                    {
                        if (txtID.Text.Length > 0) { txtPassword.Text = oBEUsuario.Password.Trim(); }
                    }
                }
                else { txtPassword.Text = string.Empty.Trim(); }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
