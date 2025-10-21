using BLL;
using Entity;
using Mapper;
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
    public partial class frmInsumoProveedor : Form
    {
        BLLProveedor oBLLProveedor;
        BEProveedor oBEProveedor;
        BEInsumo oBEInsumo;
        BLLInsumo oBLLInsumo;
        BEProveedorInsumo oBEProveedorInsumo;
        BLLProveedorInsumo oBLLProveedorInsumo;
        public frmInsumoProveedor()
        {
            InitializeComponent();
            oBLLProveedor = new BLLProveedor();
            oBEProveedor = new BEProveedor();
            oBEInsumo = new BEInsumo();
            oBLLInsumo = new BLLInsumo();
            oBLLProveedorInsumo = new BLLProveedorInsumo();
            oBEProveedorInsumo = new BEProveedorInsumo();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedIndex <= 0)
            {
                LimpiarCampos();               // Limpia los campos del proveedor
                dgvProveedorInsumo.DataSource = null;  // Vacía el DataGridView
                                                       // Limpia también los campos de insumo
                txtIdInsumo.Clear();
                txtNombreInsumo.Clear();
                txtUnidadMedida.Clear();
                txtCantidad.Clear();
                txtPrecio.Clear();
                return;
            }

            BEProveedor proveedorSeleccionado = cmbProveedores.SelectedItem as BEProveedor;

            if (proveedorSeleccionado != null)
            {
                txtIdProveedor.Text = proveedorSeleccionado.Id.ToString();
                txtNombreProveedor.Text = proveedorSeleccionado.Nombre;
                txtCUILProveedor.Text = proveedorSeleccionado.CUIL.ToString();
                txtDomicilioProveedor.Text = proveedorSeleccionado.Domicilio;
                txtEmailProveedor.Text = proveedorSeleccionado.Email;
                txtTelefonoProveedor.Text = proveedorSeleccionado.Telefono;

                CargarProveedorInsumo(proveedorSeleccionado.Id);

            }
        }

        private void LimpiarCampos()
        {
            txtIdProveedor.Clear();
            txtNombreProveedor.Clear();
            txtCUILProveedor.Clear();
            txtDomicilioProveedor.Clear();
            txtEmailProveedor.Clear();
            txtTelefonoProveedor.Clear();
            cmbProveedores.SelectedIndex = 0; // Selecciona la opción "vacía"
            txtIdInsumo.Clear();
            txtNombreInsumo.Clear();
            txtUnidadMedida.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
        }

        private void frmInsumoProveedor_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarInsumos();
            LimpiarCampos();
        }
        private void CargarProveedores()
        {
            try
            {
                List<BEProveedor> listaProveedores = oBLLProveedor.ListarTodo();

                // Insertamos una opción "vacía" al inicio
                BEProveedor opcionInicial = new BEProveedor
                {
                    Id = 0,
                    Nombre = "Seleccione un proveedor"
                };

                listaProveedores.Insert(0, opcionInicial);

                cmbProveedores.DataSource = listaProveedores;
                cmbProveedores.DisplayMember = "Nombre";
                cmbProveedores.ValueMember = "Id";
                cmbProveedores.SelectedIndex = 0; // para que arranque mostrando la opción vacía
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
            }
        }
        private void CargarInsumos()
        {
            try
            {
                List<BEInsumo> listaInsumos = oBLLInsumo.ListarTodo();

                dgvInsumos.DataSource = null;
                dgvInsumos.AutoGenerateColumns = true;
                dgvInsumos.DataSource = listaInsumos;

                // Opcional: ajustar columnas
                dgvInsumos.Columns["Id"].HeaderText = "ID";
                dgvInsumos.Columns["Nombre"].HeaderText = "Nombre";
                dgvInsumos.Columns["UnidadMedida"].HeaderText = "Unidad de Medida";
                dgvInsumos.Columns["Cantidad"].HeaderText = "Cantidad";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los insumos: " + ex.Message);
            }
        }



        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvInsumos_SelectionChanged(object sender, EventArgs e)
        {
            // Si no hay filas, no hacemos nada
            if (dgvInsumos.Rows.Count == 0 || dgvInsumos.CurrentRow == null)
            {
                LimpiarCampos();
                return;
            }

            // Obtenemos el insumo seleccionado
            BEInsumo insumoSeleccionado = dgvInsumos.CurrentRow.DataBoundItem as BEInsumo;

            if (insumoSeleccionado != null)
            {
                txtIdInsumo.Text = insumoSeleccionado.Id.ToString();
                txtNombreInsumo.Text = insumoSeleccionado.Nombre;
                txtUnidadMedida.Text = insumoSeleccionado.UnidadMedida.ToString();
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void btnVincularInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                // Validamos los datos
                if (!ValidarDatos())
                    return;

                BEProveedor proveedorSeleccionado = cmbProveedores.SelectedItem as BEProveedor;
                BEInsumo insumoSeleccionado = dgvInsumos.CurrentRow.DataBoundItem as BEInsumo;

                decimal cantidad = decimal.Parse(txtCantidad.Text.Trim());
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());

                BEProveedorInsumo nuevoVinculo = new BEProveedorInsumo
                {
                    Proveedor = new BEProveedor { Id = proveedorSeleccionado.Id },
                    Insumo = new BEInsumo { Id = insumoSeleccionado.Id },
                    Cantidad = cantidad,
                    Precio = precio,
                    Fecha = DateTime.Now
                };

                bool resultado = oBLLProveedorInsumo.Guardar(nuevoVinculo);

                if (resultado)
                {
                    MessageBox.Show("Insumo vinculado correctamente al proveedor.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProveedorInsumo(proveedorSeleccionado.Id);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo vincular el insumo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool ValidarDatos()
        {

            if (cmbProveedores.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un proveedor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dgvInsumos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un insumo de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Complete la cantidad y el precio de venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text.Trim(), out _) || !decimal.TryParse(txtPrecio.Text.Trim(), out _))
            {
                MessageBox.Show("La cantidad y el precio deben ser valores numéricos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void CargarProveedorInsumo(int proveedorId)
        {
            try
            {

                var lista = oBLLProveedorInsumo.ListarPorProveedor(proveedorId);

                dgvProveedorInsumo.DataSource = null;
                dgvProveedorInsumo.Columns.Clear();

                dgvProveedorInsumo.Columns.Add("ProveedorId", "ProveedorId");
                dgvProveedorInsumo.Columns.Add("InsumoId", "InsumoId");
                dgvProveedorInsumo.Columns.Add("Id", "ID");
                dgvProveedorInsumo.Columns.Add("InsumoNombre", "Insumo");
                dgvProveedorInsumo.Columns.Add("UnidadMedida", "Unidad de Medida");
                dgvProveedorInsumo.Columns.Add("Cantidad", "Cantidad");
                dgvProveedorInsumo.Columns.Add("Precio", "Precio");

                dgvProveedorInsumo.Columns["ProveedorId"].Visible = false;
                dgvProveedorInsumo.Columns["InsumoId"].Visible = false;

                foreach (var item in lista)
                {
                    dgvProveedorInsumo.Rows.Add(
                        item.Proveedor.Id,
                        item.Insumo.Id,
                        item.Id,                  // Aquí sí usás el Id real del proveedorInsumo
                        item.Insumo.Nombre,
                        item.Insumo.UnidadMedida.ToString(),
                        item.Cantidad,
                        item.Precio
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los insumos del proveedor: " + ex.Message);
            }
        }

        private void dgvProveedorInsumo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedorInsumo.CurrentRow == null) return;

                txtNombreInsumo.Text = dgvProveedorInsumo.CurrentRow.Cells["InsumoNombre"].Value.ToString();
                txtUnidadMedida.Text = dgvProveedorInsumo.CurrentRow.Cells["UnidadMedida"].Value.ToString();
                txtCantidad.Text = dgvProveedorInsumo.CurrentRow.Cells["Cantidad"].Value.ToString();
                txtPrecio.Text = dgvProveedorInsumo.CurrentRow.Cells["Precio"].Value.ToString();

                // Guardamos también los Id ocultos para usar en modificar
                txtIdProveedor.Text = dgvProveedorInsumo.CurrentRow.Cells["ProveedorId"].Value.ToString();
                txtIdInsumo.Text = dgvProveedorInsumo.CurrentRow.Cells["InsumoId"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar insumo: " + ex.Message);
            }
        }

        private void btnModificarInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                // Primero validamos los datos
                if (!ValidarDatos())
                    return;

                // Obtengo los objetos seleccionados
                BEProveedor proveedorSeleccionado = cmbProveedores.SelectedItem as BEProveedor;
                BEInsumo insumoSeleccionado = dgvInsumos.CurrentRow.DataBoundItem as BEInsumo;

                if (proveedorSeleccionado == null || insumoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un proveedor y un insumo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parseo cantidad y precio
                decimal cantidad = decimal.Parse(txtCantidad.Text.Trim());
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());

                // Creo el objeto BEProveedorInsumo
                BEProveedorInsumo vinculo = new BEProveedorInsumo
                {
                    Proveedor = proveedorSeleccionado,
                    Insumo = insumoSeleccionado,
                    Cantidad = cantidad,
                    Precio = precio
                };

                // Si hay fila seleccionada en dgvProveedorInsumo, tomamos el Id para modificar
                if (dgvProveedorInsumo.CurrentRow != null &&
                    dgvProveedorInsumo.CurrentRow.Cells["Id"].Value != null &&
                    int.TryParse(dgvProveedorInsumo.CurrentRow.Cells["Id"].Value.ToString(), out int idSeleccionado))
                {
                    vinculo.Id = idSeleccionado;
                }
                else
                {
                    vinculo.Id = 0; // Nuevo registro
                }

                // Llamo al método unificado Guardar
                bool resultado = oBLLProveedorInsumo.Guardar(vinculo);

                if (resultado)
                {
                    string mensaje = vinculo.Id > 0 ? "Vínculo modificado correctamente." : "Insumo vinculado correctamente al proveedor.";
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargo el DataGridView del proveedor
                    CargarProveedorInsumo(proveedorSeleccionado.Id);

                }
                else
                {
                    MessageBox.Show("No se pudo guardar el vínculo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al vincular/modificar insumo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesvincularInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedorInsumo.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un insumo para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Recupero el Id del proveedor y del insumo de la fila seleccionada
                int proveedorId = int.Parse(dgvProveedorInsumo.CurrentRow.Cells["ProveedorId"].Value.ToString());
                int insumoId = int.Parse(dgvProveedorInsumo.CurrentRow.Cells["InsumoId"].Value.ToString());

                // Confirmación
                DialogResult result = MessageBox.Show("¿Desea eliminar este vínculo de insumo con el proveedor?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Creo un objeto BEProveedorInsumo solo con los Ids
                    BEProveedorInsumo vinculoEliminar = new BEProveedorInsumo
                    {
                        Proveedor = new BEProveedor { Id = proveedorId },
                        Insumo = new BEInsumo { Id = insumoId }
                    };

                    // Llamo al BLL para eliminar
                    bool eliminado = oBLLProveedorInsumo.Eliminar(vinculoEliminar);

                    if (eliminado)
                    {
                        MessageBox.Show("Vínculo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Recargo el DataGridView
                        CargarProveedorInsumo(proveedorId);

                        // Limpio campos de insumo
                        txtIdInsumo.Clear();
                        txtNombreInsumo.Clear();
                        txtUnidadMedida.Clear();
                        txtCantidad.Clear();
                        txtPrecio.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el vínculo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar vínculo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
