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
using static Entity.BEInsumo;

namespace UI
{
    public partial class frmInsumos : Form
    {
        BEInsumo oBEInsumo;
        BLLInsumo oBLLInsumo;
        Regex nwRegex;
        public frmInsumos()
        {
            oBLLInsumo = new BLLInsumo();
            oBEInsumo = new BEInsumo();
            InitializeComponent();
        }

        private void frmInsumos_Load(object sender, EventArgs e)
        {
            CargarInsumos();
            CargarComboUnidadMedida();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                oBEInsumo = ValidarDatos();
                if (oBEInsumo != null)
                {
                    if (oBEInsumo.Id == 0)
                    {
                        oBLLInsumo.Guardar(oBEInsumo);
                        MessageBox.Show("Se ha creado correctamente el Insumo!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarInsumos();
                    }
                    else
                    {
                        throw new Exception("Error: No se puede dar de alta un Insumo que ya existe!");
                    }
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public BEInsumo ValidarDatos()
        {
            try
            {
                // --- Validar nombre ---
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("Error: El Nombre del Insumo no puede ser nulo!");

                nwRegex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!nwRegex.IsMatch(txtNombre.Text.Trim()))
                    throw new Exception("Error: El nombre del Insumo solo puede contener letras, números y espacios sin caracteres especiales!");

               
                // --- Validar cantidad ---
                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                    throw new Exception("Error: La Cantidad no puede ser nula!");

                if (!decimal.TryParse(txtCantidad.Text.Trim(), out decimal cantidad) || cantidad < 0)
                    throw new Exception("Error: La Cantidad debe ser un número mayor o igual a 0!");

                // --- Validar precio ---
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                    throw new Exception("Error: El Precio no puede ser nulo!");

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
                    throw new Exception("Error: El Precio debe ser un número mayor o igual a 0!");

                // --- Si todo está OK, crear el objeto ---
                oBEInsumo = new BEInsumo
                {
                    Id = string.IsNullOrWhiteSpace(txtIdInsumo.Text) ? 0 : int.Parse(txtIdInsumo.Text.Trim()),
                    Nombre = txtNombre.Text.Trim(),
                    UnidadMedida = (UnidadesMedida)cmbUnidadMedida.SelectedItem,
                    Cantidad = cantidad,
                    Precio = precio
                };

                return oBEInsumo;
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
                if (dgvInsumos.Rows.Count > 0)
                {
                    oBEInsumo = ValidarDatos();
                    if (oBEInsumo != null)
                    {
                        if (oBEInsumo.Id > 0)
                        {
                            oBLLInsumo.Guardar(oBEInsumo);
                            MessageBox.Show("Se ha modificado correctamente el Insumo!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarInsumos();
                        }
                        else { throw new Exception("Error: No se puede modificar un Insumo que todavía no fue creado!"); }
                    }
                }
                else { throw new Exception("Error: Primero debe crear al menos un Insumo para poder modificarlo!"); }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInsumos.Rows.Count > 0)
                {
                    oBEInsumo = ValidarDatos();
                    if (oBEInsumo != null)
                    {
                        if (oBEInsumo.Id > 0)
                        {
                            oBLLInsumo.Eliminar(oBEInsumo);
                            MessageBox.Show("Se ha eliminado correctamente el Insumo!", "Información:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarInsumos();
                        }
                        else
                        {
                            throw new Exception("Error: Primero tiene que crear el Insumo para poder eliminarlo!");
                        }
                    }
                }
                else
                {
                    throw new Exception("Error: Primero tiene que crear al menos un Insumo para poder eliminarlo!");
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarInsumos()
        {
            dgvInsumos.DataSource = null;
            dgvInsumos.DataSource = oBLLInsumo.ListarTodo();
            dgvInsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void LimpiarCampos()
        {
            txtIdInsumo.Text = string.Empty.Trim();
            txtNombre.Text = string.Empty.Trim();
            txtCantidad.Text = string.Empty.Trim();
            txtPrecio.Text = string.Empty.Trim();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void dgvInsumos_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvInsumos.Rows.Count > 0)
            {
                oBEInsumo = (BEInsumo)dgvInsumos.CurrentRow.DataBoundItem;
                if (oBEInsumo != null)
                {
                    txtIdInsumo.Text = oBEInsumo.Id.ToString();
                    txtNombre.Text = oBEInsumo.Nombre;
                    txtCantidad.Text = oBEInsumo.Cantidad.ToString("F2");
                    txtPrecio.Text = oBEInsumo.Precio.ToString("F2");

                    // Asignar la unidad al ComboBox
                    cmbUnidadMedida.SelectedItem = oBEInsumo.UnidadMedida;
                }
            }
        }

        private void CargarComboUnidadMedida()
        {
            cmbUnidadMedida.DataSource = Enum.GetValues(typeof(UnidadesMedida));
            cmbUnidadMedida.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cmbUnidadesMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUnidadMedida.SelectedItem != null)
            {
                // Obtener el valor seleccionado del enum
                UnidadesMedida unidadSeleccionada = (UnidadesMedida)cmbUnidadMedida.SelectedItem;

                
            }
        }
    }
}
