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
    public partial class frmMesas : Form
    {
        BEMesa oBEMesa;
        BLLMesa oBLLMesa;
        public frmMesas()
        {
            InitializeComponent();
            oBEMesa = new BEMesa();
            oBLLMesa = new BLLMesa();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ValidarDatosMesa debe devolver un objeto BEMesa con los datos del formulario
                BEMesa oBEMesa = ValidarDatosMesa();

                if (oBEMesa != null)
                {
                    if (oBEMesa.IdMesa == 0)
                    {
                        // Guardar nueva mesa
                        oBLLMesa.Guardar(oBEMesa);

                        // Volvemos a obtener la mesa desde la capa de negocio (por si se asigna ID o se modifica algo)
                        oBEMesa = oBLLMesa.ListarObjeto(oBEMesa);

                        // Refrescamos la grilla o listado de mesas
                        CargarMesas();

                        // Limpiamos los campos del formulario
                        LimpiarCampos();

                        MessageBox.Show("Se ha creado correctamente la Mesa", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception("Error: No se puede dar de alta una Mesa que ya existe!");
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMesas.Rows.Count > 0)
                {

                    oBEMesa = ValidarDatosMesa();
                    if (oBEMesa != null)
                    {
                        if (oBEMesa.IdMesa > 0)
                        {

                            oBLLMesa.Guardar(oBEMesa);
                            CargarMesas();
                            LimpiarCampos();
                            MessageBox.Show("Se ha modificado correctamente la mesa!", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else { throw new Exception("Error: no se puede modificar una Mesa que todavía no está registrada!"); }
                    }


                }
                else { throw new Exception("Error: Primero tiene que existir al Menos una mesa para poder modificarlo!"); }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarMesas()
        {
            dgvMesas.DataSource = null;
            if (oBLLMesa.ListarTodo() != null)
            {
                dgvMesas.DataSource = oBLLMesa.ListarTodo();
                dgvMesas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvMesas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            }
            else { dgvMesas.DataSource = null; }
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMesas.Rows.Count > 0)
                {

                    oBEMesa = ValidarDatosMesa();
                    if (oBEMesa != null)
                    {
                        if (oBEMesa.IdMesa > 0)
                        {


                            oBLLMesa.Eliminar(oBEMesa);
                            CargarMesas();
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

   
        private void LimpiarCampos()
        {
            txtIDMesa.Text = "";
            txtCapacidad.Text = "";
            txtNumeroMesa.Text = "";


        }

        private void frmMesas_Load(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarComboEstado();
            CargarMesas();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private BEMesa ValidarDatosMesa()
        {
            try
            {
                // Verificar que el número de mesa no esté vacío
                if (string.IsNullOrWhiteSpace(txtNumeroMesa.Text))
                    throw new Exception("Error: El número de mesa no puede estar vacío.");

                // Verificar que la capacidad no esté vacía
                if (string.IsNullOrWhiteSpace(txtCapacidad.Text))
                    throw new Exception("Error: La capacidad de la mesa no puede estar vacía.");

                // Validar que los valores sean numéricos
                if (!int.TryParse(txtNumeroMesa.Text.Trim(), out int numeroMesa))
                    throw new Exception("Error: El número de mesa debe ser un valor numérico.");

                if (!int.TryParse(txtCapacidad.Text.Trim(), out int capacidad))
                    throw new Exception("Error: La capacidad debe ser un valor numérico.");

                // Validar que el número y la capacidad sean positivos
                if (numeroMesa <= 0)
                    throw new Exception("Error: El número de mesa debe ser mayor a 0.");

                if (capacidad <= 0)
                    throw new Exception("Error: La capacidad debe ser mayor a 0.");

                // Crear el objeto Mesa
                BEMesa oBEMesa = new BEMesa();

                // Si el campo de ID tiene valor, se asigna
                oBEMesa.IdMesa = string.IsNullOrWhiteSpace(txtIDMesa.Text) ? 0 : int.Parse(txtIDMesa.Text.Trim());
                oBEMesa.NumeroMesa = numeroMesa;
                oBEMesa.Capacidad = capacidad;

                // Validar estado (si se elige desde un ComboBox)
                if (cmbEstado.SelectedItem == null)
                    throw new Exception("Error: Debe seleccionar un estado para la mesa.");

                oBEMesa.Estado = (BEMesa.EstadoMesa)cmbEstado.SelectedItem;



                return oBEMesa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CargarComboEstado()
        {
            // Limpio antes de agregar
            cmbEstado.Items.Clear();

            // Obtengo todos los valores del enum BEMesa.Estados
            foreach (var estado in Enum.GetValues(typeof(BEMesa.EstadoMesa)))
            {
                cmbEstado.Items.Add(estado);
            }

            // Opcional: seleccionar el primer item por defecto
            if (cmbEstado.Items.Count > 0)
                cmbEstado.SelectedIndex = 0;
        }
        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oBEMesa != null)
            {
                oBEMesa.Estado = (BEMesa.EstadoMesa)cmbEstado.SelectedItem;
            }
        }

        private void dgvMesas_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvMesas.Rows.Count > 0)
            {
                oBEMesa = (BEMesa)dgvMesas.CurrentRow.DataBoundItem;
                if (oBEMesa != null)
                {
                    txtIDMesa.Text = oBEMesa.IdMesa.ToString().Trim();
                    txtNumeroMesa.Text = oBEMesa.NumeroMesa.ToString().Trim();
                    txtCapacidad.Text = oBEMesa.Capacidad.ToString().Trim();
                    cmbEstado.SelectedItem = oBEMesa.Estado;


                }
                else
                {
                    dgvMesas.DataSource = null;
                }
            }

        }
    }
}
