using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace UI
{
    public partial class frmReserva : Form
    {
        Regex nwRegex;
        BECliente oBECliente;
        BLLCliente oBLLCliente;
        BEMesa oBEMesa;
        BLLMesa oBLLMesa;
        BEReserva oBEReserva;
        BLLReserva oBLLReserva;
        private frmMDI mdiParent;
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
            CargarMesas();
            CargarReservas();
             txtFecha.Text = dtpFecha.Value.ToShortDateString();
        }
        private void CargarMesas()
        {
            flowLayoutPanel1.Controls.Clear();

            List<BEMesa> listaMesas = oBLLMesa.ListarTodo();

            foreach (var m in listaMesas)
            {
                Button btn = new Button();
                btn.Text = $"Mesa {m.NumeroMesa}\nCapacidad: {m.Capacidad}";
                btn.Width = 100;
                btn.Height = 60;
                btn.Tag = m.IdMesa;
                btn.BackColor = ObtenerColorEstado(m.Estado);

                btn.Click += (s, e) =>
                {
                    txtMesa.Text = m.NumeroMesa.ToString();
                    txtPersonas.Text = m.Capacidad.ToString();
                };

                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private Color ObtenerColorEstado(BEMesa.EstadoMesa estado)
        {
            switch (estado)
            {
                case BEMesa.EstadoMesa.Libre:
                    return Color.LightGreen;
                case BEMesa.EstadoMesa.Reservada:
                    return Color.Yellow;
                case BEMesa.EstadoMesa.Ocupada:
                    return Color.IndianRed;
                default:
                    return Color.LightGray;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes();
            frmClientes.MdiParent = mdiParent;
            frmClientes.Show();
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
                            txtIdCliente.Text = clienteEncontrado.IdCliente.ToString();
                            txtNombre.Text = clienteEncontrado.Nombre;
                            txtTelefono.Text = clienteEncontrado.Telefono;
                        }
                        else
                        {
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

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            dtpFecha.Value = dtpFecha.Value.AddDays(1);
            txtFecha.Text = dtpFecha.Value.ToShortDateString();

            // Opcional: volver a cargar las mesas o reservas de esa fecha
            // CargarMesasPorFecha(dtpFecha.Value);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            // Retrocede un día
            dtpFecha.Value = dtpFecha.Value.AddDays(-1);
            txtFecha.Text = dtpFecha.Value.ToShortDateString();

            // Opcional: volver a cargar las mesas o reservas de esa fecha
            //CargarMesasPorFecha(dtpFecha.Value);
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar datos
                BEReserva reserva = ValidarDatosReserva();

                // Guardar reserva
                oBLLReserva.Guardar(reserva);

                // Cambiar estado de la mesa a "Reservada"
                reserva.Mesa.Estado = BEMesa.EstadoMesa.Reservada;
                oBLLMesa.Guardar(reserva.Mesa); // Actualiza el XML de mesas

                // Refrescar visualmente las mesas
                CargarMesas();

                LimpiarCampos();
                MessageBox.Show("Reserva guardada correctamente. La mesa fue marcada como 'Reservada'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private BEReserva ValidarDatosReserva()
        {
            BEReserva reserva = new BEReserva();

            // Parseo de fecha
            if (!DateTime.TryParse(txtFecha.Text.Trim(), out DateTime fecha))
                throw new Exception("La fecha no es válida.");
            reserva.FechaReserva = fecha;

            // Validar cliente usando IdCliente
            if (!int.TryParse(txtIdCliente.Text.Trim(), out int idCliente))
                throw new Exception("Debe seleccionar un cliente válido.");

            BECliente cliente = oBLLCliente.ListarObjetoPorId(new BECliente { IdCliente = idCliente });
            if (cliente == null)
                throw new Exception("Cliente no encontrado.");
            reserva.Cliente = cliente; // Ahora tiene IdCliente y Nombre

            // Cantidad de personas
            if (!int.TryParse(txtPersonas.Text.Trim(), out int cant) || cant <= 0)
                throw new Exception("Cantidad de personas inválida.");
            reserva.CantidadPersonas = cant;

            // Validar mesa usando número de mesa
            if (!int.TryParse(txtMesa.Text.Trim(), out int numMesa))
                throw new Exception("Número de mesa inválido.");

            //// Primero obtener la IdMesa a partir del número de mesa
            //BEMesa mesaNumero = oBLLMesa.ListarObjeto(new BEMesa { NumeroMesa = numMesa });
            //if (mesaNumero == null)
            //    throw new Exception("Mesa no encontrada.");

            // Ahora obtengo la mesa completa usando IdMesa
            BEMesa mesa = oBLLMesa.ListarObjetoPorId(new BEMesa { IdMesa = numMesa });
            if (mesa == null)
                throw new Exception("Mesa no encontrada.");

            reserva.Mesa = mesa; // Ahora tiene IdMesa, NumeroMesa, Capacidad y Estado

            return reserva;
        }

        private void LimpiarCampos()
        {
            txtPersonas.Text = "";
            txtMesa.Text = "";
            txtMesa.Tag = null;
            txtFecha.Text = "";
            CargarReservas();
        }
        private void CargarReservas()
        {
            try
            {
                List<BEReserva> listaReservas = oBLLReserva.ListarTodo(); // obtiene todas las reservas

                // Proyección a un tipo que el DataGrid puede mostrar fácilmente
                var mostrar = listaReservas.Select(r => new
                {
                    r.Id,
                    r.NumeroReserva,
                    Fecha = r.FechaReserva.ToString("yyyy-MM-dd"),
                    Cliente = r.Cliente.Nombre, // mostramos nombre del cliente
                    Mesa = r.Mesa.NumeroMesa,   // mostramos número de mesa
                    r.CantidadPersonas
                }).ToList();

                dgvReservas.DataSource = mostrar;
                dgvReservas.AutoResizeColumns();
                dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReservas.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    
}
