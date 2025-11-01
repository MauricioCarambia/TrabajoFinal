using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static Entity.BEMesa;

namespace UI
{
    public partial class frmReserva : Form
    {
        private Regex nwRegex;
        private BECliente oBECliente;
        private BLLCliente oBLLCliente;
        private BEMesa oBEMesa;
        private BLLMesa oBLLMesa;
        private BEReserva oBEReserva;
        private BLLReserva oBLLReserva;
        private frmMDI mdiParent;

        // Lista de reservas cargadas del día para evitar múltiples llamadas al BLL
        private List<BEReserva> reservasDelDia = new List<BEReserva>();

        public frmReserva(frmMDI mdiParent)
        {
            oBECliente = new BECliente();
            oBLLCliente = new BLLCliente();
            oBEMesa = new BEMesa();
            oBLLMesa = new BLLMesa();
            oBEReserva = new BEReserva();
            oBLLReserva = new BLLReserva();
            InitializeComponent();
            this.mdiParent = mdiParent;
        }

        private void frmReserva_Load(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        #region Métodos auxiliares

        private DateTime FechaSeleccionada() => dtpFecha.Value.Date;

        private BECliente ObtenerClienteSeleccionado()
        {
            if (!int.TryParse(txtIdCliente.Text.Trim(), out int idCliente))
                throw new Exception("Debe seleccionar un cliente válido.");

            var cliente = oBLLCliente.ListarObjetoPorId(new BECliente { Id = idCliente });
            if (cliente == null)
                throw new Exception("Cliente no encontrado.");

            return cliente;
        }

        private void LimpiarCamposReserva()
        {
            txtMesa.Clear();
            txtPersonas.Clear();
            txtNumeroReserva.Clear();
        }

        private void LimpiarCamposCliente()
        {
            txtIdCliente.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDNI.Clear();
        }

        private void ActualizarVista()
        {
            CargarMesasPorFecha(FechaSeleccionada());
            CargarReservas();
            txtFecha.Text = FechaSeleccionada().ToShortDateString();
        }

        #endregion

        #region Carga de Mesas

        private void CargarMesasPorFecha(DateTime fecha)
        {
            flpMesas.Controls.Clear();
            var listaMesas = oBLLMesa.ListarTodo()
                              .OrderBy(m => m.NumeroMesa)
                              .ToList();
            reservasDelDia = oBLLReserva.ListarPorFecha(fecha); // solo reservas del día

            foreach (var mesa in listaMesas)
            {
                Button btnMesa = new Button
                {
                    Text = $"Mesa {mesa.NumeroMesa} - Capacidad {mesa.Capacidad}",
                    Width = 100,
                    Height = 60,
                    Tag = mesa,
                    Margin = new Padding(8)
                };

                // 🔹 Buscar reserva de esta mesa para el día seleccionado
                var reservaMesa = reservasDelDia.FirstOrDefault(r => r.Mesa.IdMesa == mesa.IdMesa);

                // 🔹 Estado según si hay reserva hoy
                if (reservaMesa != null)
                    btnMesa.BackColor = Color.Goldenrod; // Reservada hoy
                else
                    btnMesa.BackColor = Color.LightGreen; // Libre

                btnMesa.Click += BtnMesa_Click;
                flpMesas.Controls.Add(btnMesa);
            }
        }

        private void BtnMesa_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var mesa = btn.Tag as BEMesa;
                if (mesa == null) return;

                txtMesa.Text = mesa.NumeroMesa.ToString();

                // Buscar reserva de la mesa en el día seleccionado
                var reservaMesa = reservasDelDia.FirstOrDefault(r => r.Mesa.IdMesa == mesa.IdMesa);

                // Cantidad de personas: si hay reserva, mostrar cantidad; si no, mostrar capacidad de la mesa
                txtPersonas.Text = reservaMesa?.CantidadPersonas.ToString() ?? mesa.Capacidad.ToString();

                // Número de reserva
                txtNumeroReserva.Text = reservaMesa?.NumeroReserva ?? string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar la mesa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Cambio de fecha

        private void dtpFechaReserva_ValueChanged(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            dtpFecha.Value = dtpFecha.Value.AddDays(1);
            ActualizarVista();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            dtpFecha.Value = dtpFecha.Value.AddDays(-1);
            ActualizarVista();
        }

        #endregion

        #region Cliente

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes
            {
                MdiParent = mdiParent
            };
            frmClientes.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarDatosCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarDatosCliente()
        {
            if (string.IsNullOrEmpty(txtDNI.Text))
            {
                MessageBox.Show("Debe ingresar un DNI para realizar la búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nwRegex = new Regex(@"^[0-9]{6,8}$");
            if (!nwRegex.IsMatch(txtDNI.Text.Trim()))
            {
                MessageBox.Show("El DNI ingresado no es válido. Debe tener entre 6 y 8 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            oBECliente = new BECliente { DNI = txtDNI.Text.Trim() };
            var clienteEncontrado = oBLLCliente.ListarObjeto(oBECliente);

            if (clienteEncontrado != null)
            {
                txtIdCliente.Text = clienteEncontrado.Id.ToString();
                txtNombre.Text = clienteEncontrado.Nombre;
                txtTelefono.Text = clienteEncontrado.Telefono;
            }
            else
            {
                MessageBox.Show("No se encontró ningún cliente con ese DNI.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Reservas

        private void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                var reserva = ValidarDatosReserva();
                DateTime fecha = reserva.FechaReserva.Date;

                ValidarMesaDisponible(reserva, fecha);
                ValidarClienteSinReserva(reserva, fecha);
                reserva.Cliente = oBLLCliente.ListarObjetoPorId(reserva.Cliente);

                // Completar la mesa
                reserva.Mesa = oBLLMesa.ListarObjetoPorId(reserva.Mesa);
                oBLLReserva.Guardar(reserva);

                reserva.Mesa.Estado = BEMesa.EstadoMesa.Reservada;
                oBLLMesa.Guardar(reserva.Mesa);

                ActualizarVista();
                LimpiarCamposCliente();
                LimpiarCamposReserva();

                MessageBox.Show("Reserva guardada correctamente. La mesa fue marcada como 'Reservada'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al reservar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private BEReserva ValidarDatosReserva()
        {
            var reserva = new BEReserva();

            if (!DateTime.TryParse(txtFecha.Text.Trim(), out DateTime fecha))
                throw new Exception("La fecha no es válida.");
            reserva.FechaReserva = fecha;

            reserva.Cliente = ObtenerClienteSeleccionado();

            if (!int.TryParse(txtPersonas.Text.Trim(), out int cant) || cant <= 0)
                throw new Exception("Cantidad de personas inválida.");
            reserva.CantidadPersonas = cant;

            if (!int.TryParse(txtMesa.Text.Trim(), out int numMesa))
                throw new Exception("Número de mesa inválido.");

            var mesa = oBLLMesa.ListarObjetoPorNumeroMesa(new BEMesa { NumeroMesa = numMesa });
            if (mesa == null)
                throw new Exception("Mesa no encontrada.");
            reserva.Mesa = mesa;

            return reserva;
        }

        private void ValidarMesaDisponible(BEReserva reserva, DateTime fecha)
        {
            var reservaMesa = reservasDelDia.FirstOrDefault(r => r.Mesa.IdMesa == reserva.Mesa.IdMesa);
            if (reserva.Mesa.Estado == BEMesa.EstadoMesa.Ocupada || reservaMesa != null)
                throw new Exception($"No se puede reservar la mesa {reserva.Mesa.NumeroMesa}. {(reservaMesa != null ? "Ya está reservada" : "Está ocupada")}.");
        }

        private void ValidarClienteSinReserva(BEReserva reserva, DateTime fecha)
        {
            var reservaCliente = reservasDelDia.FirstOrDefault(r => r.Cliente.Id == reserva.Cliente.Id);
            if (reservaCliente != null)
                throw new Exception($"El cliente {reserva.Cliente.Nombre} ya tiene una reserva para esta fecha.");
        }

        private void CargarReservas()
        {
            reservasDelDia = oBLLReserva.ListarPorFecha(FechaSeleccionada());

            var mostrar = reservasDelDia.Select(r => new
            {
                r.Id,
                r.NumeroReserva,
                Fecha = r.FechaReserva.ToString("yyyy-MM-dd"),
                Cliente = r.Cliente.Nombre,
                Mesa = r.Mesa.NumeroMesa,
                r.CantidadPersonas
            }).ToList();

            dgvReservas.DataSource = mostrar;
            dgvReservas.AutoResizeColumns();
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.MultiSelect = false;

            if (reservasDelDia.Count == 0)
                LimpiarCamposReserva();
        }

        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow != null)
            {
                txtNumeroReserva.Text = dgvReservas.CurrentRow.Cells["NumeroReserva"].Value.ToString();
            }
            else
            {
                txtNumeroReserva.Clear();
            }
        }

        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.Rows.Count == 0)
                    throw new Exception("Primero debe existir al menos una reserva para poder eliminarla.");

                string numeroReserva = txtNumeroReserva.Text.Trim();
                if (string.IsNullOrEmpty(numeroReserva))
                    throw new Exception("Debe ingresar un número de reserva válido para eliminar.");

                BEReserva oBEReserva = oBLLReserva.ListarObjeto(new BEReserva { NumeroReserva = numeroReserva });
                if (oBEReserva == null)
                    throw new Exception("No se encontró la reserva con el número especificado.");

                // Obtener la mesa real y solo liberar el estado
                BEMesa mesaReal = oBLLMesa.ListarObjeto(new BEMesa { IdMesa = oBEReserva.Mesa.IdMesa });
                if (mesaReal != null)
                {
                    mesaReal.Estado = EstadoMesa.Libre;
                    oBLLMesa.Guardar(mesaReal); // Solo cambia el estado
                }

                // Eliminar la reserva
                oBLLReserva.Eliminar(oBEReserva);

                // Refrescar UI
                ActualizarVista();
                LimpiarCamposReserva();

                MessageBox.Show("Reserva eliminada correctamente y la mesa ha sido liberada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
