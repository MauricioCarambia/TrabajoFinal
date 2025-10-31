using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class frmCocina : Form
    {
        BLLPedido oBLLPedido;
        BLLPlato oBLLPlato;
        public frmCocina()
        {
            InitializeComponent();
            oBLLPedido = new BLLPedido();
            oBLLPlato = new BLLPlato();
        }

        private void frmCocina_Load(object sender, EventArgs e)
        {
            CargarPlatosPendientesYPreparacion();
            CargarPlatosTerminados();
        }

        private void CargarPlatos(string estado)
        {
            try
            {
                // Obtener los platos según el estado (pendiente, preparación o terminado)
                var listaPlatos = oBLLPedido.ObtenerPlatosPorEstado(estado);
                var todosPlatos = oBLLPlato.ListarTodo();

                // Proyectar datos para mostrar nombre y cantidad
                var listaParaDGV = listaPlatos.Select(p =>
                {
                    var platoCompleto = todosPlatos.FirstOrDefault(x => x.Id == p.Plato.Id);

                    return new
                    {
                        Id = p.Id,
                        Plato = platoCompleto != null ? platoCompleto.Nombre : $"Plato #{p.Plato.Id}",
                        Cantidad = p.Cantidad,
                        Estado = p.Estado.ToString()
                    };
                }).ToList();

                // Mostrar en el DGV
                dgvCocina.DataSource = null;
                dgvCocina.DataSource = listaParaDGV;
                dgvCocina.Columns["Id"].Visible = false;
                dgvCocina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar platos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPlatosPendientesYPreparacion()
        {
            try
            {
                var listaPendientes = oBLLPedido.ObtenerPlatosPorEstado("Pendiente");
                var listaPreparacion = oBLLPedido.ObtenerPlatosPorEstado("Preparacion");
                var listaTotal = listaPendientes.Concat(listaPreparacion).ToList();

                var todosPlatos = oBLLPlato.ListarTodo();

                var listaParaDGV = listaTotal.Select(p =>
                {
                    var platoCompleto = todosPlatos.FirstOrDefault(x => x.Id == p.Plato.Id);
                    return new
                    {
                        Id = p.Id,
                        IdPedido = p.IdPedido, // 🔹 agregamos el Id del pedido
                        Plato = platoCompleto != null ? platoCompleto.Nombre : $"Plato #{p.Plato.Id}",
                        Cantidad = p.Cantidad,
                        Estado = p.Estado.ToString()
                    };
                }).ToList();

                dgvCocina.DataSource = null;
                dgvCocina.DataSource = listaParaDGV;

                // 🔹 Ocultamos las columnas de Id si no se quieren mostrar
                if (dgvCocina.Columns.Contains("Id"))
                    dgvCocina.Columns["Id"].Visible = false;

                //if (dgvCocina.Columns.Contains("IdPedido"))
                //    dgvCocina.Columns["IdPedido"].Visible = false;

                dgvCocina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ActualizarBotonesEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar platos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarPlatosTerminados();
            CargarPlatosPendientesYPreparacion();
            rdoPendientes.Checked = false;
            rdoPreparacion.Checked = false;
        }

        private void btnPreparacion_Click(object sender, EventArgs e)
        {
            if (dgvCocina.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un plato para cambiar a preparación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var estadoActual = dgvCocina.CurrentRow.Cells["Estado"].Value.ToString();

            if (estadoActual != BEPedidoPlato.EstadoPlato.Pendiente.ToString())
            {
                MessageBox.Show("Solo se pueden poner en preparación los platos que estén pendientes.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CambiarEstadoPlatoSeleccionado(BEPedidoPlato.EstadoPlato.Preparacion);
            CargarPlatosTerminados();
            CargarPlatosPendientesYPreparacion();
        }

        private void btnTerminado_Click(object sender, EventArgs e)
        {
            if (dgvCocina.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un plato para marcar como terminado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var estadoActual = dgvCocina.CurrentRow.Cells["Estado"].Value.ToString();

            if (estadoActual != BEPedidoPlato.EstadoPlato.Preparacion.ToString())
            {
                MessageBox.Show("Solo se pueden marcar como terminados los platos que estén en preparación.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CambiarEstadoPlatoSeleccionado(BEPedidoPlato.EstadoPlato.Terminado);
            CargarPlatosTerminados();
            CargarPlatosPendientesYPreparacion();
        }
        //private void CambiarEstadoPlatoSeleccionado(BEPedidoPlato.EstadoPlato nuevoEstado)
        //{
        //    if (dgvCocina.CurrentRow == null)
        //    {
        //        MessageBox.Show("Seleccione un plato para cambiar el estado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        int idPlato = Convert.ToInt32(dgvCocina.CurrentRow.Cells["Id"].Value);
        //        int idPedido = Convert.ToInt32(dgvCocina.CurrentRow.Cells["IdPedido"].Value); // 🔹 nuevo

        //        oBLLPedido.CambiarEstadoPlato(idPedido, idPlato, nuevoEstado); // 🔹 pasamos ambos IDs

        //        MessageBox.Show($"Estado cambiado a {nuevoEstado}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cambiar estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void CambiarEstadoPlatoSeleccionado(BEPedidoPlato.EstadoPlato nuevoEstado)
        {
            if (dgvCocina.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un plato para cambiar el estado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 🔹 Obtenemos el Id del plato y del pedido desde el DataGridView
                int idPlato = Convert.ToInt32(dgvCocina.CurrentRow.Cells["Id"].Value);
                int idPedido = Convert.ToInt32(dgvCocina.CurrentRow.Cells["IdPedido"].Value);

                // 🔹 Llamamos a la BLL para cambiar el estado, pasando ambos IDs
                oBLLPedido.CambiarEstadoPlato(idPedido, idPlato, nuevoEstado);

                MessageBox.Show($"Estado cambiado a {nuevoEstado}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Recargamos los DataGridView para reflejar los cambios
                CargarPlatosTerminados();
                CargarPlatosPendientesYPreparacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPlatosTerminados()
        {
            try
            {
                // Traer la lista de platos terminados desde la BLL de pedidos
                var terminados = oBLLPedido.ObtenerPlatosTerminados();

                // Traer la lista completa de platos para obtener el nombre
                var todosPlatos = oBLLPlato.ListarTodo(); // List<BEPlato>

                // Proyectamos la lista a un objeto anónimo para mostrar en el DGV
                var listaParaDGV = terminados.Select(p =>
                {
                    // Buscar el nombre del plato por Id
                    var platoCompleto = todosPlatos.FirstOrDefault(x => x.Id == p.Plato.Id);

                    return new
                    {
                        Id = p.Id,
                        Plato = platoCompleto != null ? platoCompleto.Nombre : $"Plato #{p.Plato.Id}",
                        Cantidad = p.Cantidad,
                        Estado = p.Estado.ToString()
                    };
                }).ToList();

                // Asignar al DataGridView
                dgvTerminados.DataSource = null;
                dgvTerminados.DataSource = listaParaDGV;

                // Ajuste de cabeceras
                dgvTerminados.Columns["Id"].HeaderText = "ID";
                dgvTerminados.Columns["Plato"].HeaderText = "Plato";
                dgvTerminados.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvTerminados.Columns["Estado"].HeaderText = "Estado";
                dgvTerminados.Columns["Id"].Visible = false;

                dgvTerminados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ActualizarBotonesEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar platos terminados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoPreparacion_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPreparacion.Checked)
            {
                CargarPlatos("Preparacion");
            }
        }

        private void rdoPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPendientes.Checked)
            {
                // Traemos pendientes + preparación si querés, o solo pendientes
                CargarPlatos("Pendiente");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ActualizarBotonesEstado()
        {
            // Si no hay fila seleccionada, deshabilitamos todo
            if (dgvCocina.CurrentRow == null)
            {
                btnPreparacion.Enabled = false;
                btnTerminado.Enabled = false;
                return;
            }

            // Obtenemos el estado actual del plato seleccionado
            string estadoActual = dgvCocina.CurrentRow.Cells["Estado"].Value?.ToString();

            // Deshabilitamos ambos por defecto
            btnPreparacion.Enabled = false;
            btnTerminado.Enabled = false;

            // Activamos según el estado actual
            if (estadoActual == "Pendiente")
            {
                btnPreparacion.Enabled = true;  // Solo se puede pasar a Preparación
            }
            else if (estadoActual == "Preparacion")
            {
                btnTerminado.Enabled = true;    // Solo se puede pasar a Terminado
            }
        }

        private void dgvCocina_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonesEstado();
        }

    }
}
