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
using static Entity.BEPedidoPlato;

namespace UI
{
    public partial class frmCargarPedido : Form
    {
        BEReserva oBEReserva;
        BLLReserva oBLLReserva;
        BEPlato oBEPlato;
        BLLPlato oBLLPlato;
        List<BEPedido> listaPedidos;
        BEPedido oBEPedido;
        BLLPedido oBLLPedido;
        public frmCargarPedido()
        {
            InitializeComponent();
            oBEReserva = new BEReserva();
            oBLLReserva = new BLLReserva();
            oBEPlato = new BEPlato();
            oBLLPlato = new BLLPlato();
            listaPedidos = new List<BEPedido>();
            oBEPedido = new BEPedido();
            oBLLPedido = new BLLPedido();
        }

        private void frmCargarPedido_Load(object sender, EventArgs e)
        {
            CargarReservasHoy();
            CargarCategorias();
        }
        private void CargarReservasHoy()
        {
            try
            {
                // Obtener la fecha "hoy" (si querés usar la zona Argentina explícitamente, ver comentario abajo)
                DateTime hoy = DateTime.Today;

                // Intento usar la BLL de reservas
                List<BEReserva> listaReservas = null;


                listaReservas = oBLLReserva.ListarPorFecha(hoy);


                // Preparar lista para mostrar (Descripción)
                var items = listaReservas
                    .OrderBy(r => r.FechaReserva)
                    .Select(r => new
                    {
                        r.Id,
                        Descripcion = $"Mesa {r.Mesa.NumeroMesa}"
                    })
                    .ToList();

                // Enlazar al ComboBox
                cmbReservas.DisplayMember = "Descripcion";
                cmbReservas.ValueMember = "Id";
                cmbReservas.DataSource = items;

                if (!items.Any())
                {
                    cmbReservas.DataSource = null;
                    cmbReservas.Items.Clear();
                    cmbReservas.Items.Add("No hay reservas para hoy");
                    cmbReservas.SelectedIndex = 0;
                }
                else
                {
                    cmbReservas.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarCategorias()
        {
            cmbCategoria.DataSource = Enum.GetValues(typeof(BEPlato.CategoriasPlato));
            cmbCategoria.SelectedIndex = -1;
        }


        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem == null)
                return;

            BEPlato.CategoriasPlato categoriaSeleccionada =
                (BEPlato.CategoriasPlato)cmbCategoria.SelectedItem;

            CargarPlatosPorCategoria(categoriaSeleccionada);
        }
        private void CargarPlatosPorCategoria(BEPlato.CategoriasPlato categoria)
        {
            dgvPlatos.Rows.Clear();
            dgvPlatos.Columns.Clear();

            dgvPlatos.Columns.Add("Id", "ID");
            dgvPlatos.Columns.Add("Nombre", "Nombre");
            dgvPlatos.Columns.Add("PrecioVenta", "Precio Venta");
            dgvPlatos.Columns.Add("Activo", "Activo");

            BLLPlato bllPlato = new BLLPlato();
            var listaPlatos = bllPlato.ListarTodo();

            var filtrados = listaPlatos
                .Where(p => p.Categoria == categoria)
                .ToList();

            foreach (var plato in filtrados)
            {
                dgvPlatos.Rows.Add(plato.Id, plato.Nombre,
                    plato.PrecioVenta.ToString("C2"),
                    plato.Activo ? "Sí" : "No");

                // Guardamos el objeto en la fila
                dgvPlatos.Rows[dgvPlatos.Rows.Count - 1].Tag = plato;
            }
        }

        private void cmbReservas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbReservas.SelectedValue == null || cmbReservas.SelectedValue.ToString() == "0")
                    return;

                int reservaId = Convert.ToInt32(cmbReservas.SelectedValue);

                // Obtener la reserva completa desde la BLL
                BEReserva reserva = oBLLReserva.ListarObjetoPorId(new BEReserva { Id = reservaId });
                if (reserva == null) return;

                // Mostrar datos de reserva
                txtReserva.Text = reserva.NumeroReserva.ToString();
                txtCliente.Text = reserva.Cliente?.Nombre ?? "";
                txtMesa.Text = reserva.Mesa?.NumeroMesa.ToString() ?? "";

                // Traer pedido existente para esta reserva (si ya existe)
                oBEPedido = oBLLPedido.ListarTodo(reservaId); // Ahora devuelve BEPedido completo
                if (oBEPedido == null)
                {
                    // Si no existe, creamos uno nuevo
                    oBEPedido = new BEPedido
                    {
                        Reserva = reserva,
                        Cliente = reserva.Cliente,
                        ListaPlatos = new List<BEPedidoPlato>(),
                        Estado = BEPedido.EstadoPedido.Abierto
                    };
                }

                // Ahora dgvPedidos mostrará los platos que ya tiene
                ActualizarDgvPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarAgregarPlato(out int cantidad)
        {
            cantidad = 0;

            try
            {
                if (dgvPlatos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (oBEPedido == null)
                {
                    MessageBox.Show("Seleccione una reserva/mesa primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarAgregarPlato(out int cantidad))
                    return;

                BEPlato platoSeleccionado = dgvPlatos.CurrentRow?.Tag as BEPlato;
                if (platoSeleccionado == null) return;

                // Verificar si el plato ya existe
                var platoExistente = oBEPedido.ListaPlatos.FirstOrDefault(p => p.Plato.Id == platoSeleccionado.Id);

                if (platoExistente != null)
                {
                    // Sumar cantidad
                    platoExistente.Cantidad += cantidad;
                }
                else
                {
                    // Agregar nuevo plato
                    oBEPedido.ListaPlatos.Add(new BEPedidoPlato
                    {
                        Plato = platoSeleccionado,
                        Cantidad = cantidad,
                        Estado = EstadoPlato.Pendiente
                    });
                }

                // Actualizar la vista
                ActualizarDgvPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarDgvPedidos()
        {
            try
            {
                dgvPedidos.Rows.Clear();
                dgvPedidos.Columns.Clear();

                // Crear columnas
                dgvPedidos.Columns.Add("Plato", "Plato");
                dgvPedidos.Columns.Add("Cantidad", "Cantidad");
                dgvPedidos.Columns.Add("Subtotal", "Subtotal");
                dgvPedidos.Columns.Add("Estado", "Estado"); // Nueva columna para el estado

                // Llenar filas con la info de cada plato
                foreach (var platoPedido in oBEPedido.ListaPlatos)
                {
                    decimal subtotal = platoPedido.Cantidad * platoPedido.Plato.PrecioVenta;

                    dgvPedidos.Rows.Add(
                        platoPedido.Plato.Nombre,
                        platoPedido.Cantidad.ToString("N2"),
                        subtotal.ToString("C2"),
                        platoPedido.Estado.ToString() // Mostrar el estado del plato
                    );
                }

                // Calcular total del pedido
                txtTotal.Text = oBEPedido.ListaPlatos.Sum(p => p.Cantidad * p.Plato.PrecioVenta).ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedidos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un plato para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el plato seleccionado desde la fila del DataGridView
                string nombrePlato = dgvPedidos.CurrentRow.Cells["Plato"].Value?.ToString();
                if (string.IsNullOrEmpty(nombrePlato)) return;

                // Buscar el objeto BEPedidoPlato en la lista del pedido
                var platoSeleccionado = oBEPedido.ListaPlatos.FirstOrDefault(p => p.Plato.Nombre == nombrePlato);
                if (platoSeleccionado == null) return;

                // Validar cantidad a eliminar
                if (!int.TryParse(txtCantidad.Text, out int cantidadAEliminar) || cantidadAEliminar <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cantidadAEliminar >= platoSeleccionado.Cantidad)
                {
                    // Si la cantidad a eliminar es mayor o igual a la existente, eliminar el plato completo
                    oBEPedido.ListaPlatos.Remove(platoSeleccionado);
                }
                else
                {
                    // Si es menor, solo restamos la cantidad
                    platoSeleccionado.Cantidad -= cantidadAEliminar;
                }

                // Actualizar DataGridView y total
                ActualizarDgvPedidos();

                MessageBox.Show("Cantidad eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        public void LimpiarCampos()
        {
            cmbReservas.SelectedIndex = -1;
            txtReserva.Clear();
            txtCliente.Clear();
            txtMesa.Clear();
            cmbCategoria.SelectedIndex = -1;
            dgvPlatos.Rows.Clear();
            dgvPlatos.Columns.Clear();
            dgvPedidos.Rows.Clear();
            dgvPedidos.Columns.Clear();
            txtTotal.Clear();
            txtCantidad.Clear();
            oBEPedido = null;
            listaPedidos.Clear();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Validar antes de continuar
                if (!ValidarConfirmarPedido())
                    return;

                // 🔹 Actualizar datos del pedido
                //oBEPedido.Estado = BEPedido.EstadoPedido.Cerrado;
                oBEPedido.Fecha = DateTime.Now;

                // 🔹 Asegurar estados de los platos
                foreach (var item in oBEPedido.ListaPlatos)
                {
                    if (item.Estado == default)
                        item.Estado = BEPedidoPlato.EstadoPlato.Pendiente;
                }
                oBLLPedido.DescontarStockInsumos(oBEPedido);
                // 🔹 Guardar pedido (nuevo o existente)
                oBLLPedido.Guardar(oBEPedido);

                // 🔹 Confirmar al usuario
                MessageBox.Show("Pedido confirmado y enviado a cocina.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Refrescar vista y limpiar
                ActualizarDgvPedidos();
                oBEPedido = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al confirmar el pedido: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarConfirmarPedido()
        {
            if (oBEPedido == null)
            {
                MessageBox.Show("No hay un pedido activo para confirmar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (oBEPedido.ListaPlatos == null || oBEPedido.ListaPlatos.Count == 0)
            {
                MessageBox.Show("El pedido no contiene platos.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (oBEPedido.Reserva == null)
            {
                MessageBox.Show("El pedido no tiene una reserva asociada.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (oBEPedido.Cliente == null)
            {
                MessageBox.Show("El pedido no tiene un cliente asignado.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

