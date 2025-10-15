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
    public partial class frmClientes : Form
    {
        Regex nwRegex;
        BECliente oBECliente;
        BLLCliente oBLLCliente;
        public frmClientes()
        {
            InitializeComponent();
            oBECliente = new BECliente();
            oBLLCliente = new BLLCliente();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ValidarDatosCliente debe devolver un objeto BECliente con los datos del formulario
                BECliente oBECliente = ValidarDatos();

                if (oBECliente != null)
                {
                    if (oBECliente.IdCliente == 0)
                    {
                        // Guardar nueva Cliente
                        oBLLCliente.Guardar(oBECliente);

                        // Volvemos a obtener la Cliente desde la capa de negocio (por si se asigna ID o se modifica algo)
                        oBECliente = oBLLCliente.ListarObjetoPorId(oBECliente);

                        // Refrescamos la grilla o listado de Clientes
                        CargarClientes();

                        // Limpiamos los campos del formulario
                        LimpiarCampos();

                        MessageBox.Show("Se ha creado correctamente la Cliente", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception("Error: No se puede dar de alta una Cliente que ya existe!");
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
                if (dgvClientes.Rows.Count > 0)
                {

                    oBECliente = ValidarDatos();
                    if (oBECliente != null)
                    {
                        if (oBECliente.IdCliente > 0)
                        {

                            oBLLCliente.Guardar(oBECliente);
                            CargarClientes();
                            LimpiarCampos();
                            MessageBox.Show("Se ha modificado correctamente la Cliente!", "Confirmación:", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else { throw new Exception("Error: no se puede modificar una Cliente que todavía no está registrada!"); }
                    }


                }
                else { throw new Exception("Error: Primero tiene que existir al Menos una Cliente para poder modificarlo!"); }
            }
            catch (CryptographicException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void CargarClientes()
        {
            dgvClientes.DataSource = null;
            if (oBLLCliente.ListarTodo() != null)
            {
                dgvClientes.DataSource = oBLLCliente.ListarTodo();
                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            }
            else { dgvClientes.DataSource = null; }
            LimpiarCampos();
        }
        private void frmClientes_Load(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarClientes();
        }
        private void LimpiarCampos()
        {
            txtIDCliente.Text = "";
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";


        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.Rows.Count > 0)
                {

                    oBECliente = ValidarDatos();
                    if (oBECliente != null)
                    {
                        if (oBECliente.IdCliente > 0)
                        {


                            oBLLCliente.Eliminar(oBECliente);
                            CargarClientes();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private BECliente ValidarDatos()
        {
            try
            {
                // Verificar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("Error: El nombre del cliente no puede estar vacío.");

                // Verificar que el DNI no esté vacío
                if (string.IsNullOrWhiteSpace(txtDNI.Text))
                    throw new Exception("Error: El DNI no puede estar vacío.");

                // Verificar que el teléfono no esté vacío
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                    throw new Exception("Error: El teléfono no puede estar vacío.");

                // Validar que el DNI sea numérico
                if (!long.TryParse(txtDNI.Text.Trim(), out long dni))
                    throw new Exception("Error: El DNI debe ser un valor numérico.");

                // Validar que el teléfono sea numérico
                if (!long.TryParse(txtTelefono.Text.Trim(), out long telefono))
                    throw new Exception("Error: El teléfono debe ser un valor numérico.");

                // Validar que el DNI tenga una longitud válida (por ejemplo, 7 u 8 dígitos)
                if (txtDNI.Text.Trim().Length < 7 || txtDNI.Text.Trim().Length > 8)
                    throw new Exception("Error: El DNI debe tener entre 7 y 8 dígitos.");

                // Crear el objeto Cliente
                BECliente oBECliente = new BECliente();

                // Si el campo de ID tiene valor, se asigna
                oBECliente.IdCliente = string.IsNullOrWhiteSpace(txtIDCliente.Text) ? 0 : int.Parse(txtIDCliente.Text.Trim());
                oBECliente.Nombre = txtNombre.Text.Trim();
                oBECliente.DNI = txtDNI.Text.Trim();
                oBECliente.Telefono = txtTelefono.Text.Trim();

                return oBECliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.Rows.Count > 0)
            {
                oBECliente = (BECliente)dgvClientes.CurrentRow.DataBoundItem;
                if (oBECliente != null)
                {
                    txtIDCliente.Text = oBECliente.IdCliente.ToString().Trim();
                    txtNombre.Text = oBECliente.Nombre.ToString().Trim();
                    txtDNI.Text = oBECliente.DNI.ToString().Trim();
                    txtTelefono.Text = oBECliente.Telefono.ToString().Trim();


                }
                else
                {
                    dgvClientes.DataSource = null;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarDatosCliente();
                
            }
            catch (XmlException ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void ValidarDatosCliente()
        {
            try
            {
                if (txtDNI.Text.Length > 0)
                {
                    // Validar que el DNI tenga entre 6 y 8 dígitos
                    nwRegex = new Regex(@"^[0-9]{6,8}$");

                    if (nwRegex.IsMatch(txtDNI.Text.Trim()))
                    {
                        oBECliente = new BECliente();
                        oBECliente.DNI = txtDNI.Text.Trim();

                        var clienteEncontrado = oBLLCliente.ListarObjeto(oBECliente);

                        if (clienteEncontrado != null)
                        {
                            txtIDCliente.Text = clienteEncontrado.IdCliente.ToString();
                            txtNombre.Text = clienteEncontrado.Nombre;
                            txtTelefono.Text = clienteEncontrado.Telefono;
                        }
                        else
                        {
                            LimpiarCampos();
                            MessageBox.Show("No se encontró ningún cliente con ese DNI.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El DNI ingresado no es válido. Debe tener entre 6 y 8 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un DNI para realizar la búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar cliente: " + ex.Message);
            }
        }
    } 

}
