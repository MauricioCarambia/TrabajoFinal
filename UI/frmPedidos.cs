using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class frmPedidos : Form
    {
        BLLPedido oBLLPedido;
        BLLReserva oBLLReserva;
        BLLCliente oBLLCliente;

        public frmPedidos()
        {
            InitializeComponent();
            oBLLPedido = new BLLPedido();
            oBLLReserva = new BLLReserva();
            oBLLCliente = new BLLCliente();
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
        }

        private void frmPedidos_Load(object sender, EventArgs e)
        {
            CargarPedidosHoy();
        }

        private void CargarPedidosHoy()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                var pedidos = oBLLPedido.ListarPedidosPorFecha(hoy);

                if (pedidos == null || pedidos.Count == 0)
                {
                    dgvPedidos.DataSource = null;
                    LimpiarDetalle();
                    return;
                }

                dgvPedidos.DataSource = pedidos.Select(p => new
                {
                    PedidoId = p.Id,
                    NumeroReserva = p.Reserva.NumeroReserva,
                    Cliente = p.Reserva.Cliente.Nombre,
                    Mesa = p.Reserva.Mesa.NumeroMesa,
                    Total = p.ListaPlatos.Sum(pl => pl.Plato.PrecioVenta * pl.Cantidad).ToString("0.00"),
                    Estado = p.Estado.ToString()
                }).ToList();

                dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPedidos.MultiSelect = false;
                dgvPedidos.ReadOnly = true;
                dgvPedidos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pedidos: " + ex.Message);
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow == null) return;

            int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["PedidoId"].Value);
            var pedido = oBLLPedido.ListarObjetoPorId(pedidoId);

            if (pedido == null)
            {
                LimpiarDetalle();
                return;
            }

            // Mostrar detalles en el segundo DGV y los textbox
            MostrarDetallePedido(pedido);
            txtReserva.Text = pedido.Reserva.NumeroReserva;
            txtCliente.Text = pedido.Reserva.Cliente.Nombre;
            txtMesa.Text = pedido.Reserva.Mesa.NumeroMesa.ToString();
        }

        private void MostrarDetallePedido(BEPedido pedido)
        {
            dgvDetallePedido.DataSource = null;

            if (pedido == null || pedido.ListaPlatos == null || !pedido.ListaPlatos.Any())
                return;

            var listaParaDGV = pedido.ListaPlatos.Select(p => new
            {
                Id = p.Id,
                IdPedido = pedido.Id,
                Plato = p.Plato?.Nombre ?? "Sin nombre",
                Cantidad = p.Cantidad,
                PrecioVenta = p.Plato?.PrecioVenta.ToString("0.00") ?? "0.00",
                Subtotal = p.Subtotal.ToString("0.00"), // ✅ Calculado al vuelo
                Estado = p.Estado.ToString()
            }).ToList();

            dgvDetallePedido.DataSource = listaParaDGV;

            if (dgvDetallePedido.Columns.Contains("Id")) dgvDetallePedido.Columns["Id"].Visible = false;
            if (dgvDetallePedido.Columns.Contains("IdPedido")) dgvDetallePedido.Columns["IdPedido"].Visible = false;

            dgvDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvDetallePedido.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            dgvDetallePedido.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvDetallePedido.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvDetallePedido.DefaultCellStyle.SelectionForeColor = Color.White;

            txtTotal.Text = pedido.ListaPlatos.Sum(p => p.Subtotal).ToString("0.00"); // ✅ Total actualizado
        }

        private void LimpiarDetalle()
        {
            txtReserva.Clear();
            txtCliente.Clear();
            txtMesa.Clear();
            txtTotal.Clear();
            dgvDetallePedido.DataSource = null;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminarPlato_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetallePedido.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un plato para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["PedidoId"].Value);
                var pedido = oBLLPedido.ListarObjetoPorId(pedidoId);

                int idPlato = Convert.ToInt32(dgvDetallePedido.CurrentRow.Cells["Id"].Value);

                var platoEliminar = pedido.ListaPlatos
                    .FirstOrDefault(p => p.Id == idPlato &&
                        (p.Estado == BEPedidoPlato.EstadoPlato.Confirmado || p.Estado == BEPedidoPlato.EstadoPlato.Pendiente));

                if (platoEliminar == null)
                {
                    MessageBox.Show("No se puede eliminar este plato porque está en preparación o terminado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cantidad > platoEliminar.Cantidad)
                {
                    MessageBox.Show($"No se puede eliminar {cantidad} unidad(es). Solo hay {platoEliminar.Cantidad} disponibles.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    $"¿Está seguro que desea eliminar {cantidad} unidad(es) del plato '{platoEliminar.Plato.Nombre}'?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    oBLLPedido.EliminarPlato(pedido, platoEliminar, cantidad);

                    var pedidoActualizado = oBLLPedido.ListarObjetoPorId(pedidoId);
                    MostrarDetallePedido(pedidoActualizado);
                    txtTotal.Text = pedidoActualizado.Total.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarPedidosHoy();
        }

        private void btnEnviarCocina_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedidos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un pedido para enviar a cocina.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["PedidoId"].Value);
                oBLLPedido.EnviarPlatosACocina(pedidoId);

                CargarPedidosHoy();
                MessageBox.Show("Los platos confirmados se enviaron a cocina correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar a cocina: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEntregado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetallePedido.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un plato para marcar como entregado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPlato = Convert.ToInt32(dgvDetallePedido.CurrentRow.Cells["Id"].Value);
                int idPedido = Convert.ToInt32(dgvDetallePedido.CurrentRow.Cells["IdPedido"].Value);
                string estadoActual = dgvDetallePedido.CurrentRow.Cells["Estado"].Value.ToString();

                if (estadoActual != BEPedidoPlato.EstadoPlato.Terminado.ToString())
                {
                    MessageBox.Show("Solo se pueden marcar como entregados los platos que estén terminados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                oBLLPedido.CambiarEstadoPlato(idPedido, idPlato, BEPedidoPlato.EstadoPlato.Entregado);

                MessageBox.Show("El plato ha sido marcado como entregado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar
                CargarPedidosHoy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar el estado del plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEnviarCobranza_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedidos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un pedido para mandar a cobranza.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["PedidoId"].Value);

                // Llamamos al método de BLL que hace todo
                oBLLPedido.EnviarPedidoACobranza(pedidoId);

                MessageBox.Show("El pedido ha sido cerrado, enviado a cobranza y la reserva se ha cerrado.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar lista de pedidos y detalle
                CargarPedidosHoy();
                dgvDetallePedido.DataSource = null;
                LimpiarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el pedido a cobranza: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
