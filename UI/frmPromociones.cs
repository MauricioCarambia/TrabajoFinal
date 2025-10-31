using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace UI
{
    public partial class frmPromociones : Form
    {
        private BLLPromociones oBLLPromociones;

        public frmPromociones()
        {
            InitializeComponent();
            oBLLPromociones = new BLLPromociones();
        }

        private void frmPromociones_Load(object sender, EventArgs e)
        {
            CargarTipos();
            CargarPromociones();
        }

        // Cargar ComboBox de tipos y estados
        private void CargarTipos()
        {
            cmbTipoDescuento.DataSource = Enum.GetValues(typeof(BEPromociones.TipoPromocion));
            cmbTipoDescuento.SelectedIndex = 0;

            cmbEstadoPromocion.Items.Clear();
            cmbEstadoPromocion.Items.Add("Activa");
            cmbEstadoPromocion.Items.Add("Inactiva");
            cmbEstadoPromocion.SelectedIndex = 0;
        }

        // Cargar DataGridView con todas las promociones
        private void CargarPromociones()
        {
            dgvPromociones.DataSource = null;
            var lista = oBLLPromociones.ListarTodo();

            if (lista != null && lista.Any())
            {
                // Convertimos la lista de MetodosPago a string para mostrar
                var listaParaGrilla = lista.Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Tipo,
                    p.ValorDescuento,
                    p.MontoMinimo,
                    p.Activa,
                    MetodosPago = string.Join(", ", p.MetodosPago) // <-- aquí convertimos la lista a string
                }).ToList();

                dgvPromociones.DataSource = listaParaGrilla;
                // Hacer invisible la columna Id
                if (dgvPromociones.Columns["Id"] != null)
                    dgvPromociones.Columns["Id"].Visible = false;
            }
        }

        // Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ValidarDatosPromocion debe devolver un objeto BEPromociones con los datos del formulario
                BEPromociones oBEPromociones = ValidarDatosPromocion();

                if (oBEPromociones != null)
                {
                    if (oBEPromociones.Id == 0)
                    {
                        // Guardar nueva promoción
                        oBLLPromociones.Guardar(oBEPromociones);

                        // Volvemos a obtener la promoción desde la capa de negocio (por si se asigna ID o se modifica algo)
                        oBEPromociones = oBLLPromociones.ListarObjeto(oBEPromociones);

                        // Refrescamos la grilla o listado de promociones
                        CargarPromociones();

                        // Limpiamos los campos del formulario
                        LimpiarCampos();

                        MessageBox.Show("✅ Se ha creado correctamente la promoción", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception("Error: No se puede dar de alta una promoción que ya existe!");
                    }
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private BEPromociones ValidarDatosPromocion()
        {
            try
            {
                // Validar nombre de promoción
                if (string.IsNullOrWhiteSpace(txtNombrePromocion.Text))
                    throw new Exception("Debe ingresar un nombre para la promoción.");

                // Validar tipo de descuento
                if (cmbTipoDescuento.SelectedIndex == -1)
                    throw new Exception("Debe seleccionar un tipo de descuento.");

                // Validar valor de descuento
                if (!decimal.TryParse(txtValor.Text, out decimal valorDescuento))
                    throw new Exception("Debe ingresar un valor de descuento válido.");

                // Validar monto mínimo
                if (!decimal.TryParse(txtMontoMinimo.Text, out decimal montoMinimo))
                    throw new Exception("Debe ingresar un monto mínimo válido.");

                // Validar métodos de pago seleccionados
                List<string> metodosPago = new List<string>();
                if (chkEfectivo.Checked) metodosPago.Add("Efectivo");
                if (chkTarjetaCredito.Checked) metodosPago.Add("TarjetaCredito");
                if (chkTarjetaDebito.Checked) metodosPago.Add("TarjetaDebito");
                if (chkMercadoPago.Checked) metodosPago.Add("MercadoPago");

                if (metodosPago.Count == 0)
                    throw new Exception("Debe seleccionar al menos un método de pago.");

                // Construir objeto BEPromociones
                BEPromociones promo = new BEPromociones
                {
                    Id = 0, // Se asigna automáticamente al guardar
                    Nombre = txtNombrePromocion.Text.Trim(),
                    Tipo = (BEPromociones.TipoPromocion)Enum.Parse(
                               typeof(BEPromociones.TipoPromocion),
                               cmbTipoDescuento.SelectedItem.ToString()
                           ),
                    ValorDescuento = valorDescuento,
                    MontoMinimo = montoMinimo,
                    Activa = cmbEstadoPromocion.SelectedItem?.ToString() == "Activa",
                    MetodosPago = metodosPago
                };

                return promo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Error en los datos de la promoción:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Limpiar campos
        private void LimpiarCampos()
        {
            txtNombrePromocion.Clear();
            txtValor.Clear();
            txtMontoMinimo.Clear();
            chkEfectivo.Checked = chkTarjetaCredito.Checked = chkTarjetaDebito.Checked = chkMercadoPago.Checked = false;
            cmbTipoDescuento.SelectedIndex = 0;
            cmbEstadoPromocion.SelectedIndex = 0;
        }

        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPromociones.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una promoción para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener Id de la promoción seleccionada
                if (!int.TryParse(dgvPromociones.CurrentRow.Cells["Id"].Value.ToString(), out int idPromocion))
                {
                    MessageBox.Show("No se pudo obtener el Id de la promoción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmación del usuario
                var confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta promoción?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    // Crear objeto con Id para eliminar
                    var promoEliminar = new BEPromociones { Id = idPromocion };

                    // Llamamos al BLL para eliminar
                    oBLLPromociones.Eliminar(promoEliminar);

                    MessageBox.Show("✅ Promoción eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPromociones();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al eliminar la promoción:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvPromociones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Evitar cabecera
            {
                var fila = dgvPromociones.Rows[e.RowIndex];
                if (fila.Cells["Id"].Value != null)
                {
                    // Obtener Id de la promoción
                    int id = Convert.ToInt32(fila.Cells["Id"].Value);

                    // Traer la promoción completa desde la BLL
                    var promo = oBLLPromociones.ListarTodo().FirstOrDefault(p => p.Id == id);
                    if (promo != null)
                    {
                        txtNombrePromocion.Text = promo.Nombre;
                        cmbTipoDescuento.SelectedItem = promo.Tipo;
                        txtValor.Text = promo.ValorDescuento.ToString();
                        txtMontoMinimo.Text = promo.MontoMinimo.ToString();
                        cmbEstadoPromocion.SelectedItem = promo.Activa ? "Activa" : "Inactiva";

                        // Marcar métodos de pago
                        chkEfectivo.Checked = promo.MetodosPago.Contains("Efectivo");
                        chkTarjetaCredito.Checked = promo.MetodosPago.Contains("TarjetaCredito");
                        chkTarjetaDebito.Checked = promo.MetodosPago.Contains("TarjetaDebito");
                        chkMercadoPago.Checked = promo.MetodosPago.Contains("MercadoPago");
                    }
                }
            }
        }

        private List<string> ObtenerMetodosPago()
        {
            var metodos = new List<string>();
            if (chkEfectivo.Checked) metodos.Add("Efectivo");
            if (chkTarjetaCredito.Checked) metodos.Add("TarjetaCredito");
            if (chkTarjetaDebito.Checked) metodos.Add("TarjetaDebito");
            if (chkMercadoPago.Checked) metodos.Add("MercadoPago");
            return metodos;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPromociones.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una promoción para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPromocion = Convert.ToInt32(dgvPromociones.CurrentRow.Cells["Id"].Value);

                var promo = new BEPromociones
                {
                    Id = idPromocion,
                    Nombre = txtNombrePromocion.Text.Trim(),
                    Tipo = (BEPromociones.TipoPromocion)Enum.Parse(typeof(BEPromociones.TipoPromocion), cmbTipoDescuento.SelectedItem.ToString()),
                    ValorDescuento = decimal.Parse(txtValor.Text),
                    MontoMinimo = decimal.Parse(txtMontoMinimo.Text),
                    Activa = cmbEstadoPromocion.SelectedItem?.ToString() == "Activa",
                    MetodosPago = ObtenerMetodosPago()
                };

                // Guardar/modificar en el XML
                oBLLPromociones.Guardar(promo);

                MessageBox.Show("✅ Promoción modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPromociones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al modificar la promoción:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
