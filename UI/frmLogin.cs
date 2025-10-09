using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UI
{
    public partial class frmLogin : Form
    {
        BEUsuario oBEUsuario;
        BLLUsuario oBLLUsuario;
        Regex nwRegex;
        int intento;
        public frmLogin()
        {
            InitializeComponent();
            oBEUsuario = new BEUsuario();
            oBLLUsuario = new BLLUsuario();

            intento = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                oBEUsuario = ValidarDatos();
                if (oBEUsuario != null)
                {
                    oBEUsuario = oBLLUsuario.ListarObjeto(oBEUsuario);
                    if (oBEUsuario.Activo == true)
                    {
                        if (oBEUsuario.Bloqueado == false)
                        {
                            string txtBoxPasswordEncriptado = oBLLUsuario.EncriptarPassword(txtPassword.Text.Trim());
                            if (oBEUsuario.Password == txtBoxPasswordEncriptado)
                            {
                                oBEUsuario.Password = txtPassword.Text.Trim();
                                if (oBLLUsuario.Login(oBEUsuario) == true)
                                {
                                    intento = 0;
                                    oBEUsuario = oBLLUsuario.ListarObjeto(oBEUsuario);
                                    frmMDI fmMenu = new frmMDI(oBEUsuario);
                                    fmMenu.Show();
                                    this.Hide();
                                }
                                else { throw new Exception("Error: Usuario y/o Contraseña incorrecta, por favor intente nuevamente!"); }
                            }
                            else
                            {
                                intento++;
                                if (intento == 3)
                                {
                                    oBEUsuario.Activo = false;
                                    oBEUsuario.Password = txtPassword.Text.Trim();
                                    oBLLUsuario.Guardar(oBEUsuario);
                                    MessageBox.Show($"Usuario: {oBEUsuario.Usuario} Se ha Desactivado porque se ha ingresado muchas veces Contraseñas incorrectas! Por favor, comunicarse con el Administrador para reactivar el Usuario!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else { throw new Exception("Error: Usuario y/o Contraseña incorrecta, por favor intente nuevamente!"); }
                            }
                        }
                        else { throw new Exception($"Error: El Usuario {oBEUsuario.Usuario} se encuentra Bloqueado! Por favor, comunicarse con el Administrador asi lo Desbloquea!"); }
                    }
                    else { throw new Exception($"Error: El Usuario: {oBEUsuario.Usuario} No se encuentra activo! Por favor, comuniquese con el Administrador asi lo Activa nuevamente!"); }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frmLogin_Load(object sender, EventArgs e)
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
                            oBEUsuario.Usuario = txtUsuario.Text.Trim();
                            oBEUsuario.Password = txtPassword.Text.Trim();
                            return oBEUsuario;
                        }
                        else { throw new Exception("Error: El Password no puede ser nulo!"); }
                    }
                    else { throw new Exception("Error: El nombre usuario solo acepta palabras y números sin caraceteres especiales únicamente!"); }
                }
                else { throw new Exception("Error: El nombre de Usuario no puede ser nulo!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return oBEUsuario = null;
        }

        private void LimpiarCampos()
        {
            txtUsuario.Text = string.Empty.Trim();
            txtPassword.Text = string.Empty.Trim();
            //ckBxLoginMostrarPassword.Checked = false;
        }

        //private void ckBxLoginMostrarPassword_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ckBxLoginMostrarPassword.Checked == true) { txtPassword.UseSystemPasswordChar = false; }
        //    else { txtPassword.UseSystemPasswordChar = true; }
        //}
    }
}
