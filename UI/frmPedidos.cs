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
    public partial class frmPedidos : Form
    {
        BLLPedido oBLLPedido;
        BLLReserva oBLLReserva;
        public frmPedidos()
        {
            InitializeComponent();
            oBLLPedido = new BLLPedido();
            oBLLReserva = new BLLReserva();
        }

        private void frmPedidos_Load(object sender, EventArgs e)
        {
            CargarPedidos();
        }
        private void CargarPedidos()
        {
            dgvPedidos.DataSource = null; // Limpiar DataSource

            // Traer todas las reservas de hoy desde la BLL
            List<BEReserva> listaReservas = oBLLReserva.ListarPorFecha(DateTime.Today);

            if (listaReservas != null && listaReservas.Count > 0)
            {
                // Creamos lista para el DataGridView
                var listaParaDGV = listaReservas.Select(r => new
                {
                    ReservaId = r.Id,
                    NumeroReserva = r.NumeroReserva,
                    Mesa = r.Mesa?.NumeroMesa.ToString() ?? "Sin mesa",
                    Cliente = r.Cliente?.Nombre ?? "Sin cliente",
                    Fecha = r.FechaReserva.ToString("yyyy-MM-dd HH:mm"),
                    Estado = oBLLPedido.ListarPorReserva(r.Id)?.Estado.ToString() ?? "Sin pedido",

                }).ToList();

                dgvPedidos.DataSource = listaParaDGV;
                dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvPedidos.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            }
            else
            {
                dgvPedidos.DataSource = null;
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.SelectedRows.Count == 0)
                return;

            try
            {
                int reservaId = Convert.ToInt32(dgvPedidos.SelectedRows[0].Cells["ReservaId"].Value);

                BEPedido pedido = oBLLPedido.ListarPorReserva(reservaId);

                if (pedido == null || pedido.Reserva == null || pedido.Cliente == null || pedido.Reserva.Mesa == null)
                {
                    dgvDetallePedido.DataSource = null;
                    txtReserva.Clear();
                    txtCliente.Clear();
                    txtMesa.Clear();
                    txtTotal.Clear();
                    return;
                }

                // Llenar TextBox
                txtReserva.Text = pedido.Reserva.NumeroReserva;
                txtCliente.Text = pedido.Cliente.Nombre;
                txtMesa.Text = pedido.Reserva.Mesa.NumeroMesa.ToString();
                txtTotal.Text = oBLLPedido.CalcularTotal(pedido).ToString("0.00");

                // Llenar DataGridView de platos
                var listaParaDGV = pedido.ListaPlatos.Select(p => new
                {
                    Plato = p.Plato?.Nombre ?? "Sin nombre",
                    Cantidad = p.Cantidad,
                    Estado = p.Estado.ToString()
                }).ToList();

                dgvDetallePedido.DataSource = listaParaDGV;
                dgvDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvDetallePedido.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles del pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
}
