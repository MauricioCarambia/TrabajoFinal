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
    public partial class frmProveedores : Form
    {
        BEProveedor oBEProveedor;
        BLLProveedor oBLLProveedor;
        Regex nwRegex;
        public frmProveedores()
        {

            InitializeComponent();
            oBEProveedor = new BEProveedor();
            oBLLProveedor = new BLLProveedor();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                oBEProveedor = ValidarDatos();
                if (oBEProveedor != null)
                {
                    if (oBEProveedor.Id == 0)
                    {
                        oBLLProveedor.Guardar(oBEProveedor);
                        MessageBox.Show("Se ha creado correctamente el Proveedor!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarProveedores();
                    }
                    else { throw new Exception("Error: No se puede dar el alta a un Proveedor que ya existe!"); }
                }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public BEProveedor ValidarDatos()
        {
            try
            {
                // --- Validar nombre ---
                if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
                    throw new Exception("Error: El Nombre del Proveedor no puede ser nulo!");

                nwRegex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!nwRegex.IsMatch(txtNombreProveedor.Text.Trim()))
                    throw new Exception("Error: El nombre del Proveedor solo toma palabras sin caracteres especiales!");

                // --- Validar CUIL ---
                if (string.IsNullOrWhiteSpace(txtCUILProveedor.Text))
                    throw new Exception("Error: El CUIL del Proveedor no puede ser nulo!");

                string cuil = txtCUILProveedor.Text.Trim();
                nwRegex = new Regex(@"^(20|23|24|25|26|27|30|33|34)\d{8}\d$");
                if (!nwRegex.IsMatch(cuil))
                    throw new Exception("Error: El CUIL del Proveedor no es válido!");

                // --- Validar domicilio ---
                if (string.IsNullOrWhiteSpace(txtDomicilioProveedor.Text))
                    throw new Exception("Error: El Domicilio del Proveedor no puede ser nulo!");

                nwRegex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!nwRegex.IsMatch(txtDomicilioProveedor.Text.Trim()))
                    throw new Exception("Error: El Domicilio del Proveedor solo toma palabras y números sin caracteres especiales!");

                // --- Validar email ---
                if (string.IsNullOrWhiteSpace(txtEmailProveedor.Text))
                    throw new Exception("Error: El Email del Proveedor no puede ser nulo!");

                nwRegex = new Regex(@"^\w+[\w.]*@[\w.]+\.[a-zA-Z]{2,}(?:\.[a-zA-Z]{2,3})?$");
                if (!nwRegex.IsMatch(txtEmailProveedor.Text.Trim()))
                    throw new Exception("Error: El Email del Proveedor no es válido!");

                // --- Validar teléfono ---
                if (string.IsNullOrWhiteSpace(txtTelefonoProveedor.Text))
                    throw new Exception("Error: El Teléfono del Proveedor no puede ser nulo!");

                // Acepta números, espacios, guiones y paréntesis
                nwRegex = new Regex(@"^[0-9\-\+\(\) ]{6,20}$");
                if (!nwRegex.IsMatch(txtTelefonoProveedor.Text.Trim()))
                    throw new Exception("Error: El Teléfono del Proveedor no es válido! Solo se permiten números, espacios, guiones o paréntesis.");

                // --- Si todo está OK, crear el objeto ---
                oBEProveedor = new BEProveedor
                {
                    Id = string.IsNullOrWhiteSpace(txtIdProveedor.Text) ? 0 : int.Parse(txtIdProveedor.Text.Trim()),
                    Nombre = txtNombreProveedor.Text.Trim(),
                    CUIL = long.Parse(cuil),
                    Domicilio = txtDomicilioProveedor.Text.Trim(),
                    Email = txtEmailProveedor.Text.Trim(),
                    Telefono = txtTelefonoProveedor.Text.Trim()
                };

                return oBEProveedor;
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.Rows.Count > 0)
                {
                    oBEProveedor = ValidarDatos();
                    if (oBEProveedor != null)
                    {
                        if (oBEProveedor.Id > 0)
                        {
                            oBLLProveedor.Guardar(oBEProveedor);
                            MessageBox.Show("Se ha modificado correctamente al Proveedor!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarProveedores();
                        }
                        else { throw new Exception("Error: No se puede modificar un Proveedor que todavia no lo creo!"); }
                    }
                }
                else { throw new Exception("Error: Primero tiene que crear al menos un Proveedor para poder modificarlo!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.Rows.Count > 0)
                {
                    oBEProveedor = ValidarDatos();
                    if (oBEProveedor != null)
                    {
                        if (oBEProveedor.Id > 0)
                        {
                            oBLLProveedor.Eliminar(oBEProveedor);
                            MessageBox.Show("Se ha eliminado correctamente al Proveedor!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarProveedores();
                        }
                        else { throw new Exception("Error: Primero tiene que crear al Proveedor para poder Eliminarlo!"); }
                    }
                }
                else { throw new Exception("Error: Primero tiene que crear al menos un Proveedor para poder Eliminarlo!"); }
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LimpiarCampos()
        {
            txtIdProveedor.Text = string.Empty.Trim();
            txtNombreProveedor.Text = string.Empty.Trim();
            txtCUILProveedor.Text = string.Empty.Trim();
            txtDomicilioProveedor.Text = string.Empty.Trim();
            txtEmailProveedor.Text = string.Empty.Trim();
            txtTelefonoProveedor.Text = string.Empty.Trim();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = oBLLProveedor.ListarTodo();
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvProveedores.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProveedores.Rows.Count > 0)
            {
                oBEProveedor = (BEProveedor)dgvProveedores.CurrentRow.DataBoundItem;
                if (oBEProveedor != null)
                {
                    txtIdProveedor.Text = oBEProveedor.Id.ToString().Trim();
                    txtNombreProveedor.Text = oBEProveedor.Nombre.Trim();
                    txtDomicilioProveedor.Text = oBEProveedor.Domicilio.Trim();
                    txtEmailProveedor.Text = oBEProveedor.Email.Trim();
                    txtTelefonoProveedor.Text = oBEProveedor.Telefono.Trim();
                    txtCUILProveedor.Text = oBEProveedor.CUIL.ToString().Trim();

                }
            }
            else { }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
