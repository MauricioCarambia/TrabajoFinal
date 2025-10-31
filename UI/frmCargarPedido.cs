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
using System.Xml.Linq;
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
        BLLInsumo oBLLInsumo;
        BLLMesa oBLLMesa;
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
            oBLLInsumo = new BLLInsumo();
            oBLLMesa = new BLLMesa();
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

                // Traigo la reserva completa
                BEReserva reserva = oBLLReserva.ListarObjetoPorId(new BEReserva { Id = reservaId });
                if (reserva == null) return;

                txtReserva.Text = reserva.NumeroReserva.ToString();
                txtCliente.Text = reserva.Cliente?.Nombre ?? "";
                txtMesa.Text = reserva.Mesa?.NumeroMesa.ToString() ?? "";

                // Verificar si ya hay un pedido activo para esta reserva
                if (oBEPedido == null || oBEPedido.Reserva == null || oBEPedido.Reserva.Id != reservaId)
                {
                    // Intentar traer pedido existente de la base (XML o DB)
                    var pedidoExistente = oBLLPedido.ListarPorReserva(reservaId);

                    if (pedidoExistente != null && pedidoExistente.ListaPlatos.Count > 0)
                    {
                        oBEPedido = pedidoExistente;
                    }
                    else
                    {
                        oBEPedido = new BEPedido
                        {
                            Reserva = reserva,
                            Cliente = reserva.Cliente,
                            ListaPlatos = new List<BEPedidoPlato>(),
                            Estado = BEPedido.EstadoPedido.Abierto
                        };
                    }
                }

                // Actualizo el DataGridView con los platos de la reserva
                ActualizarDgvPedidos();

                // Si querés, podés separar la carga de los platos de la reserva en otro dgv
                CargarPlatosReserva(reservaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reserva: " + ex.Message);
            }
        }
        private void CargarPlatosReserva(int reservaId)
        {
            try
            {
                dgvPedidos.Rows.Clear();
                dgvPedidos.Columns.Clear();

                dgvPedidos.Columns.Add("IdPlato", "ID");
                dgvPedidos.Columns["IdPlato"].Visible = false;

                dgvPedidos.Columns.Add("Nombre", "Nombre");
                dgvPedidos.Columns.Add("Cantidad", "Cantidad");
                dgvPedidos.Columns.Add("PrecioUnitario", "Precio Unitario");
                dgvPedidos.Columns.Add("Subtotal", "Subtotal");
                dgvPedidos.Columns.Add("Estado", "Estado");

                if (oBEPedido == null || oBEPedido.ListaPlatos.Count == 0)
                    return;

                foreach (var platoPedido in oBEPedido.ListaPlatos)
                {
                    decimal subtotal = platoPedido.Cantidad * platoPedido.Plato.PrecioVenta;

                    dgvPedidos.Rows.Add(
                        platoPedido.Plato.Id,
                        platoPedido.Plato.Nombre,
                        platoPedido.Cantidad,
                        platoPedido.Plato.PrecioVenta.ToString("C2"),
                        subtotal.ToString("C2"),
                        platoPedido.Estado.ToString()
                    );
                }

                dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPedidos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los platos de la reserva: " + ex.Message);
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
                // 0️⃣ Validar que el pedido no esté cerrado
                if (oBEPedido.Estado == BEPedido.EstadoPedido.Cerrado)
                {
                    MessageBox.Show("No se pueden agregar más platos a un pedido cerrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 0️⃣a Validar que la reserva no esté cerrada
                if (oBEPedido.Reserva != null && oBEPedido.Reserva.Estado == BEReserva.EstadoReserva.Cerrada)
                {
                    MessageBox.Show("No se pueden agregar platos a un pedido cuya reserva esté cerrada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1️⃣ Verificar selección de plato
                if (dgvPlatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Obtener el plato seleccionado
                DataGridViewRow fila = dgvPlatos.SelectedRows[0];
                int idPlato = Convert.ToInt32(fila.Cells["Id"].Value);
                BEPlato plato = oBLLPlato.ListarObjetoPorId(new BEPlato { Id = idPlato });

                // 2️⃣a Validar que el plato esté activo
                if (!plato.Activo)
                {
                    MessageBox.Show($"El plato '{plato.Nombre}' está inactivo y no se puede agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3️⃣ Validar cantidad ingresada
                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4️⃣ Obtener insumos del plato
                List<BEPlatoInsumo> insumosPlato = oBLLPlato.ListarInsumosPorPlato(plato.Id);

                // 5️⃣ Validar stock disponible
                foreach (var insumoPlato in insumosPlato)
                {
                    BEInsumo insumo = oBLLInsumo.ListarObjetoPorId(insumoPlato.Id);
                    decimal stockRestante = insumo.Cantidad - (insumoPlato.Cantidad * cantidad);
                    if (stockRestante < 0)
                    {
                        MessageBox.Show($"No hay suficiente stock de {insumo.Nombre}. Stock disponible: {insumo.Cantidad}, requerido: {insumoPlato.Cantidad * cantidad}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 6️⃣ Descontar stock
                foreach (var insumoPlato in insumosPlato)
                {
                    BEInsumo insumo = oBLLInsumo.ListarObjetoPorId(insumoPlato.Id);
                    insumo.Cantidad -= insumoPlato.Cantidad * cantidad;
                    oBLLInsumo.Guardar(insumo);
                }

                // 7️⃣ Verificar si ya existe un plato pendiente del mismo tipo
                var existente = oBEPedido.ListaPlatos.FirstOrDefault(p => p.Plato.Id == plato.Id && p.Estado == BEPedidoPlato.EstadoPlato.Cargado);
                if (existente != null)
                {
                    existente.Cantidad += cantidad;
                }
                else
                {
                    // 8️⃣ Agregar nuevo plato Cargado
                    oBEPedido.ListaPlatos.Add(new BEPedidoPlato
                    {
                        Plato = plato,
                        Cantidad = cantidad,
                        Estado = BEPedidoPlato.EstadoPlato.Cargado
                    });
                }

                // 9️⃣ Actualizar DataGridView
                ActualizarDgvPedidos();
                txtCantidad.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarDgvPedidos()
        {
            try
            {
                dgvPedidos.Rows.Clear();
                dgvPedidos.Columns.Clear();

                // Crear columnas
                dgvPedidos.Columns.Add("IdPlato", "IdPlato");
                dgvPedidos.Columns["IdPlato"].Visible = false; // Oculta el ID

                dgvPedidos.Columns.Add("Plato", "Plato");
                dgvPedidos.Columns.Add("Cantidad", "Cantidad");
                dgvPedidos.Columns.Add("Subtotal", "Subtotal");
                dgvPedidos.Columns.Add("Estado", "Estado");

                // Llenar filas con la info de cada plato
                foreach (var platoPedido in oBEPedido.ListaPlatos)
                {
                    decimal subtotal = platoPedido.Cantidad * platoPedido.Plato.PrecioVenta;

                    dgvPedidos.Rows.Add(
                        platoPedido.Plato.Id,                 // Id oculto
                        platoPedido.Plato.Nombre,             // Nombre del plato
                        platoPedido.Cantidad.ToString("N2"),  // Cantidad con formato
                        subtotal.ToString("C2"),              // Subtotal formateado
                        platoPedido.Estado.ToString()         // Estado
                    );
                }

                // Calcular total del pedido
                txtTotal.Text = oBEPedido.ListaPlatos
                    .Sum(p => p.Cantidad * p.Plato.PrecioVenta)
                    .ToString("C2");

                // Opcional: ajustar ancho y formato
                dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPedidos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el pedido: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedidos.CurrentRow == null) return;

                // Validar que haya una cantidad ingresada
                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidadAEliminar) || cantidadAEliminar <= 0)
                {
                    MessageBox.Show("Por favor, ingrese una cantidad válida para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCantidad.Focus();
                    return;
                }

                var platoEliminar = oBEPedido.ListaPlatos[dgvPedidos.CurrentRow.Index];

                // Solo eliminar si está pendiente
                if (platoEliminar.Estado != BEPedidoPlato.EstadoPlato.Cargado)
                {
                    MessageBox.Show("Solo se pueden eliminar platos Cargado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cantidadAEliminar >= platoEliminar.Cantidad)
                {
                    // Si la cantidad a eliminar es igual o mayor, se elimina completamente
                    oBEPedido.ListaPlatos.Remove(platoEliminar);
                }
                else
                {
                    // Si es menor, solo se descuenta la cantidad
                    platoEliminar.Cantidad -= cantidadAEliminar;
                }

                // Actualizar dgv
                ActualizarDgvPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 🔹 Verificar si el pedido está cerrado
                if (oBEPedido.Estado == BEPedido.EstadoPedido.Cerrado)
                {
                    MessageBox.Show("No se pueden agregar ni confirmar platos en un pedido cerrado.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Filtrar platos cargados
                var platosPendientes = oBEPedido.ListaPlatos
                    .Where(p => p.Estado == BEPedidoPlato.EstadoPlato.Cargado)
                    .ToList();

                if (!platosPendientes.Any())
                {
                    MessageBox.Show("No hay platos cargados para confirmar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Crear pedido temporal con los platos pendientes
                BEPedido pedidoTemporal = new BEPedido
                {
                    ListaPlatos = platosPendientes
                };

                // ✅ Confirmar pedido
                List<string> errores = oBLLPedido.ConfirmarPedido(pedidoTemporal, oBEPedido);

                if (errores.Count > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errores), "Error de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Cambiar estado de mesa a ocupada si tiene reserva
                if (oBEPedido.Reserva != null && oBEPedido.Reserva.Mesa != null)
                {
                    int idMesa = oBEPedido.Reserva.Mesa.IdMesa;
                    oBLLMesa.ActualizarEstadoMesa(idMesa, BEMesa.EstadoMesa.Ocupada);
                }

                // 🔹 Actualizar grilla de pedidos
                ActualizarDgvPedidos();

                // 🔹 Limpiar lista de platos confirmados
                foreach (var plato in platosPendientes)
                {
                    oBEPedido.ListaPlatos.Remove(plato);
                }

                dgvPedidos.DataSource = null;
                txtTotal.Clear();

                MessageBox.Show("Pedido confirmado, stock descontado y mesa marcada como ocupada.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al confirmar pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        // Método para validar pedido antes de confirmar
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

