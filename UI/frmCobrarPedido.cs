using BLL;
using Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class frmCobrarPedido : Form
    {
        private BLLPedido oBLLPedido;
        private BLLReserva oBLLReserva;
        private BLLCliente oBLLCliente;
        private BLLPromociones oBLLPromociones;
        private BLLFactura oBLLFactura;
        private BLLCobro oBLLCobro;
        private BLLMesa oBLLMesa;

        private BEPedido pedidoSeleccionado;

        public frmCobrarPedido()
        {
            InitializeComponent();
            oBLLPedido = new BLLPedido();
            oBLLReserva = new BLLReserva();
            oBLLCliente = new BLLCliente();
            oBLLCobro = new BLLCobro();
            oBLLFactura = new BLLFactura();
            oBLLPromociones = new BLLPromociones();
            oBLLMesa = new BLLMesa();
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
        }

        private void frmCobrarPedido_Load(object sender, EventArgs e)
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

                // ✅ Armamos los datos de forma segura
                dgvPedidos.DataSource = pedidos.Select(p => new
                {
                    PedidoId = p.Id,
                    NumeroReserva = p.Reserva?.NumeroReserva ?? "Sin reserva",
                    Cliente = p.Reserva?.Cliente?.Nombre ?? "Desconocido",
                    Mesa = p.Reserva?.Mesa?.NumeroMesa.ToString() ?? "-",
                    Total = p.Total.ToString("0.00"),
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
                MessageBox.Show("Error al cargar pedidos: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPedidos.CurrentRow == null) return;

                int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["PedidoId"].Value);
                pedidoSeleccionado = oBLLPedido.ListarObjetoPorId(pedidoId);

               
                if (pedidoSeleccionado == null)
                {
                    LimpiarDetalle();
                    return;
                }

                // ✅ Mostrar detalle completo del pedido
                //MostrarDetallePedido(pedidoSeleccionado);

                txtReserva.Text = pedidoSeleccionado.Reserva?.NumeroReserva ?? "N/A";
                txtCliente.Text = pedidoSeleccionado.Reserva?.Cliente?.Nombre ?? "N/A";
                txtMesa.Text = pedidoSeleccionado.Reserva?.Mesa?.NumeroMesa.ToString() ?? "N/A";

                // 🔹 Priorizar el valor mostrado en el DataGridView (ya formateado)
                if (dgvPedidos.CurrentRow.Cells["Total"].Value != null)
                {
                    txtTotal.Text = dgvPedidos.CurrentRow.Cells["Total"].Value.ToString();
                }
                else
                {
                    txtTotal.Text = pedidoSeleccionado.Total.ToString("0.00");
                }

                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el detalle del pedido: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void MostrarDetallePedido(BEPedido pedido)
        //{
        //    try
        //    {
        //        dgvDetallePedido.DataSource = null;

        //        if (pedido == null || pedido.ListaPlatos == null || pedido.ListaPlatos.Count == 0)
        //            return;

        //        var listaParaDGV = pedido.ListaPlatos.Select(p => new
        //        {
        //            Id = p.Id,
        //            IdPedido = pedido.Id,
        //            Plato = p.Plato?.Nombre ?? "Sin nombre",
        //            Cantidad = p.Cantidad,
        //            PrecioVenta = p.Plato?.PrecioVenta.ToString("0.00") ?? "0.00",
        //            Subtotal = p.Subtotal.ToString("0.00"),
        //            Estado = p.Estado.ToString()
        //        }).ToList();

        //        dgvDetallePedido.DataSource = listaParaDGV;

        //        if (dgvDetallePedido.Columns.Contains("Id"))
        //            dgvDetallePedido.Columns["Id"].Visible = false;

        //        if (dgvDetallePedido.Columns.Contains("IdPedido"))
        //            dgvDetallePedido.Columns["IdPedido"].Visible = false;

        //        dgvDetallePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        //        dgvDetallePedido.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
        //        dgvDetallePedido.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
        //        dgvDetallePedido.DefaultCellStyle.SelectionForeColor = Color.White;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al mostrar el detalle del pedido: " + ex.Message);
        //    }
        //}

        private void LimpiarDetalle()
        {
            txtReserva.Clear();
            txtCliente.Clear();
            txtMesa.Clear();
            txtTotal.Clear();
            txtTotalCobrar.Clear();
            dgvDetallePedido.DataSource = null;
            cmbPromociones.DataSource = null;
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarPedidosHoy();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDetalle();
        }

        private void CargarPromocionesPorPago()
        {
            try
            {
                List<BEPromociones> listaPromociones = oBLLPromociones.ListarTodo();

                string tipoPagoSeleccionado = "";
                if (rdoEfectivo.Checked) tipoPagoSeleccionado = "Efectivo";
                else if (rdoTarjetaCredito.Checked) tipoPagoSeleccionado = "TarjetaCredito";
                else if (rdoTarjetaDebito.Checked) tipoPagoSeleccionado = "TarjetaDebito";
                else if (rdoMercadoPago.Checked) tipoPagoSeleccionado = "MercadoPago";

                if (!string.IsNullOrEmpty(tipoPagoSeleccionado))
                {
                    var promocionesFiltradas = listaPromociones
                        .Where(p => p.MetodosPago.Contains(tipoPagoSeleccionado))
                        .ToList();

                    cmbPromociones.DataSource = promocionesFiltradas;
                    cmbPromociones.DisplayMember = "Nombre";
                    cmbPromociones.ValueMember = "Id";
                    cmbPromociones.SelectedIndex = -1;
                }
                else
                {
                    cmbPromociones.DataSource = null;
                }

                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar promociones: " + ex.Message);
            }
        }

        private void rdoEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEfectivo.Checked)
                CargarPromocionesPorPago();
        }

        private void rdoTarjetaCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTarjetaCredito.Checked)
                CargarPromocionesPorPago();
        }

        private void rdoTarjetaDebito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTarjetaDebito.Checked)
                CargarPromocionesPorPago();
        }

        private void rdoMercadoPago_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMercadoPago.Checked)
                CargarPromocionesPorPago();
        }

        private void cmbPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            try
            {
                decimal totalPedido = 0;

                // ✅ Si hay un pedido cargado, usamos su total real
                if (pedidoSeleccionado != null)
                {
                    totalPedido = pedidoSeleccionado.ListaPlatos
                                    .Sum(p => p.Plato.PrecioVenta * p.Cantidad);
                }
                else
                {
                    // Si no hay pedido cargado, intentamos leer de txtTotal
                    decimal.TryParse(txtTotal.Text, out totalPedido);
                }

                decimal totalCobrar = totalPedido;

                // ✅ Aplicar promoción si existe una seleccionada
                if (cmbPromociones.SelectedItem is BEPromociones promo)
                {
                    switch (promo.Tipo)
                    {
                        case BEPromociones.TipoPromocion.Porcentaje:
                            totalCobrar -= totalPedido * (promo.ValorDescuento / 100);
                            break;

                        case BEPromociones.TipoPromocion.MontoFijo:
                            totalCobrar -= promo.ValorDescuento;
                            break;
                    }

                    // Evitar negativos
                    if (totalCobrar < 0)
                        totalCobrar = 0;
                }

                // ✅ Mostrar totales actualizados
                txtTotal.Text = totalPedido.ToString("0.00");
                txtTotalCobrar.Text = totalCobrar.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular el total: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pedidoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un pedido primero.");
                    return;
                }
               
                // Obtener promoción seleccionada
                var promo = cmbPromociones.SelectedItem as BEPromociones;

                // Total a cobrar calculado
                decimal totalCobrar = decimal.Parse(txtTotalCobrar.Text);

                var cliente = pedidoSeleccionado.Reserva.Cliente;
                if (cliente == null)
                {
                    MessageBox.Show("No se encontró el cliente de la reserva.");
                    return;
                }

                // Asegurarse de que tenga DNI
                if (string.IsNullOrEmpty(cliente.DNI))
                    cliente.DNI = "Sin DNI";

                // Crear factura
                BEFactura factura = new BEFactura
                {
                    // Número de factura correlativo con formato 0000001
                    NumeroFactura = oBLLFactura.ObtenerProximoNumeroFactura(),
                    Cliente = cliente,
                    DetallePlatos = pedidoSeleccionado.ListaPlatos,
                    PromocionAplicada = promo,
                    DescuentoAplicado = pedidoSeleccionado.Total - totalCobrar,
                    Total = totalCobrar,
                    Pedido = pedidoSeleccionado,
                    Fecha = DateTime.Now
                };

                // Guardar factura en XML
                oBLLFactura.Guardar(factura);

                // Crear cobro
                BECobro cobro = new BECobro
                {
                    Factura = factura,
                    Pedido = pedidoSeleccionado,
                    Promocion = promo,
                    Total = totalCobrar
                };

                oBLLCobro.Guardar(cobro);

                // Generar PDF de factura
                GenerarFacturaPDF(factura.Cliente, factura.DetallePlatos,
                                  factura.SubTotal, factura.Total);

                // Actualizar estado del pedido y liberar mesa
               
                oBLLMesa.ActualizarEstadoMesa(pedidoSeleccionado.Reserva.Mesa.IdMesa, BEMesa.EstadoMesa.Libre);

                MessageBox.Show($"Cobro registrado correctamente. Factura N° {factura.NumeroFactura}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cobrar: " + ex.Message);
            }
        }



        private void GenerarFacturaPDF(BECliente cliente, List<BEPedidoPlato> platos, decimal subTotal, decimal total)
        {
            try
            {
                string nroFactura = oBLLFactura.ObtenerProximoNumeroFactura(); // 0000001
                string nombreArchivo = $"Factura_{nroFactura}.pdf";
                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();

                doc.Add(new Paragraph($"Factura N° {nroFactura}") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph(" "));
                // ✅ Mostrar todos los datos del cliente
                doc.Add(new Paragraph($"Cliente: {cliente.Nombre}"));
                doc.Add(new Paragraph($"DNI: {cliente.DNI}"));
                doc.Add(new Paragraph($"Teléfono: {cliente.Telefono ?? "Sin Teléfono"}"));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}"));
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(4) { WidthPercentage = 100 };
                tabla.AddCell("Plato");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Precio Unitario");
                tabla.AddCell("Subtotal");

                foreach (var p in platos)
                {
                    tabla.AddCell(p.Plato.Nombre);
                    tabla.AddCell(p.Cantidad.ToString());
                    tabla.AddCell(p.Plato.PrecioVenta.ToString("0.00"));
                    tabla.AddCell((p.Plato.PrecioVenta * p.Cantidad).ToString("0.00"));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Subtotal: ${subTotal:0.00}"));
                if (total < subTotal)
                    doc.Add(new Paragraph($"Descuento aplicado: ${subTotal - total:0.00}"));
                doc.Add(new Paragraph($"Total a cobrar: ${total:0.00}"));

                doc.Close();

                MessageBox.Show($"Factura generada correctamente:\n{ruta}", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando factura: " + ex.Message);
            }
        }

    }
}
