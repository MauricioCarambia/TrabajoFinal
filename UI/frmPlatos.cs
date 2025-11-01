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
using System.Xml.Linq;

namespace UI
{
    public partial class frmPlatos : Form
    {
        BEPlato oBEPlato;
        BLLPlato oBLLPlato;
        BEPlatoInsumo oBEPlatoInsumo;
        BLLPlatoInsumo oBLLPlatoInsumo;
        BLLInsumo oBLLInsumo;
        List<BEInsumo> listaInsumos;
        public frmPlatos()
        {
            InitializeComponent();
            oBEPlato = new BEPlato();
            oBLLPlato = new BLLPlato();
            oBEPlatoInsumo = new BEPlatoInsumo();
            oBLLPlatoInsumo = new BLLPlatoInsumo();
            oBLLInsumo = new BLLInsumo();
            listaInsumos = new List<BEInsumo>();
        }

        private void frmPlatos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarInsumosDisponibles();
            ConfigurarDgvDetalle();
            CargarTreeViewPlatos();
        }

        #region Configuración y carga
        private void CargarCategorias()
        {
            cmbCategorias.DataSource = Enum.GetValues(typeof(BEPlato.CategoriasPlato));
            cmbCategorias.SelectedIndex = -1;
        }

        private void CargarInsumosDisponibles()
        {
            try
            {
                // Llenamos la lista global del formulario
                listaInsumos = new BLLInsumo().ListarTodo();

                // Asignamos al DataGridView
                dgvInsumos.DataSource = null; // limpiar antes
                dgvInsumos.DataSource = listaInsumos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar insumos: " + ex.Message);
            }
        }

        private void ConfigurarDgvDetalle()
        {
            dgvDetallePlato.Columns.Clear();
            dgvDetallePlato.Columns.Add("Insumo", "Insumo");
            dgvDetallePlato.Columns.Add("Cantidad", "Cantidad");
            dgvDetallePlato.Columns.Add("Precio", "Precio Proporcional");

            var colObjeto = new DataGridViewTextBoxColumn
            {
                Name = "InsumoObjeto",
                Visible = false
            };
            dgvDetallePlato.Columns.Add(colObjeto);
        }
        #endregion

        #region Selección de insumo
        private void dgvInsumos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInsumos.CurrentRow != null)
            {
                BEInsumo insumo = (BEInsumo)dgvInsumos.CurrentRow.DataBoundItem;
                txtIdInsumo.Text = insumo.Id.ToString();
                txtNombreInsumo.Text = insumo.Nombre;
                txtUnidadMedida.Text = insumo.UnidadMedida.ToString();
                txtCantidad.Text = "";
            }
        }
        #endregion

        #region Agregar insumo al plato

        private void btnAgregarInsumo_Click_1(object sender, EventArgs e)
        {
            if (dgvInsumos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un insumo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BEInsumo insumoSeleccionado = (BEInsumo)dgvInsumos.CurrentRow.DataBoundItem;

            // Verificar que la cantidad no supere la disponible
            if (cantidad > insumoSeleccionado.Cantidad)
            {
                MessageBox.Show("La cantidad ingresada supera la disponible.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que no se agregue duplicado
            if (oBEPlato.ListaInsumos.Any(i => i.Insumo.Id == insumoSeleccionado.Id))
            {
                MessageBox.Show("El insumo ya fue agregado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idInsumo = Convert.ToInt32(dgvInsumos.CurrentRow.Cells["Id"].Value);
            decimal cantidadUsada = cantidad; // Usamos la cantidad ingresada por el usuario
            //oBLLInsumo.DescontarStock(idInsumo, cantidadUsada);

            // Crear objeto PlatoInsumo
            BEPlatoInsumo platoInsumo = new BEPlatoInsumo
            {
                Insumo = insumoSeleccionado,
                Cantidad = cantidad,
                CostoUnitario = insumoSeleccionado.Precio
            };

            // Agregar a la lista en memoria
            oBEPlato.ListaInsumos.Add(platoInsumo);
            CargarInsumosDisponibles();
            // Actualizar DataGridView del detalle
            ActualizarDgvDetallePlato();

            // Limpiar controles de insumo
            LimpiarControlesInsumo();

        }

        private bool ValidarAgregarInsumo(out decimal cantidad)
        {
            cantidad = 0;

            if (dgvInsumos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un insumo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var insumoSeleccionado = (BEInsumo)dgvInsumos.CurrentRow.DataBoundItem;
            if (cantidad > insumoSeleccionado.Cantidad)
            {
                MessageBox.Show("La cantidad ingresada supera la cantidad disponible.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ActualizarDgvDetallePlato()
        {
            dgvDetallePlato.Rows.Clear();

            foreach (var insumo in oBEPlato.ListaInsumos)
            {
                dgvDetallePlato.Rows.Add(
                    insumo.Insumo.Nombre,
                    $"{insumo.Cantidad} {insumo.Insumo.UnidadMedida}",
                    insumo.CostoProporcional.ToString("F2"),
                    insumo.Insumo
                );
            }

            ActualizarPrecios();
        }


        #endregion

        #region Precios


        private void ActualizarPrecios()
        {
            oBEPlato.PrecioCosto = oBEPlato.ListaInsumos.Sum(i => i.CostoUnitario * i.Cantidad);
            oBEPlato.PrecioVenta = oBEPlato.PrecioCosto * (1 + oBEPlato.PorcentajeGanancia / 100);

            txtPrecioCosto.Text = oBEPlato.PrecioCosto.ToString("F2");
            txtPrecioVenta.Text = oBEPlato.PrecioVenta.ToString("F2");

        }
        #endregion

        #region Guardar plato
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombrePlato.Text))
            {
                MessageBox.Show("Ingrese un nombre para el plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!oBEPlato.ListaInsumos.Any())
            {
                MessageBox.Show("Agregue al menos un insumo al plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategorias.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una categoría.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar datos del formulario al objeto
            oBEPlato.Nombre = txtNombrePlato.Text.Trim();
            oBEPlato.Categoria = (BEPlato.CategoriasPlato)cmbCategorias.SelectedItem;
            oBEPlato.PorcentajeGanancia = trackPorcentaje.Value;

            // Calcular precios
            oBEPlato.PrecioCosto = oBEPlato.ListaInsumos.Sum(i => i.CostoUnitario * i.Cantidad);
            oBEPlato.PrecioVenta = oBEPlato.PrecioCosto * (1 + oBEPlato.PorcentajeGanancia / 100);

            oBEPlato.Activo = chkActivo.Checked;

            // Guardar en XML usando BLL
            oBLLPlato.Guardar(oBEPlato);              // Guarda nodo <Platos> con datos del plato
            //oBLLPlatoInsumo.GuardarLista(oBEPlato);   // Guarda nodo <PlatoInsumos> con los insumos
            oBEPlato.Activo = chkActivo.Checked;

            MessageBox.Show("Plato guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar memoria y controles
            oBEPlato = new BEPlato { ListaInsumos = new List<BEPlatoInsumo>() };
            CargarTreeViewPlatos();
            LimpiarControlesPlato();
        }

        private bool ValidarPlato()
        {
            if (string.IsNullOrWhiteSpace(txtNombrePlato.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (oBEPlato.ListaInsumos.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un insumo al plato.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarControlesInsumo()
        {
            txtIdInsumo.Clear();
            txtNombreInsumo.Clear();
            txtUnidadMedida.Clear();
            txtCantidad.Clear();
        }
        private void LimpiarControlesPlato()
        {
            txtNombrePlato.Clear();
            dgvDetallePlato.Rows.Clear();
            cmbCategorias.SelectedIndex = -1;
            trackPorcentaje.Value = 0;
            txtPorcentaje.Text = "0%";
            txtPrecioCosto.Clear();
            txtPrecioVenta.Clear();
            chkActivo.Checked = false;

        }
        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void trackPorcentaje_Scroll_1(object sender, EventArgs e)
        {
            oBEPlato.PorcentajeGanancia = trackPorcentaje.Value; // <--- actualizar objeto
            txtPorcentaje.Text = trackPorcentaje.Value + "%";
            ActualizarPrecios();
        }

        private BEPlato CrearPlatoDesdeControles()
        {
            BEPlato platoTemp = new BEPlato
            {
                Nombre = txtNombrePlato.Text.Trim(),
                PorcentajeGanancia = trackPorcentaje.Value,
                Categoria = (BEPlato.CategoriasPlato)cmbCategorias.SelectedItem,
                Activo = true,
                ListaInsumos = new List<BEPlatoInsumo>(oBEPlato.ListaInsumos)
            };

            platoTemp.PrecioCosto = platoTemp.CalcularCosto();
            platoTemp.PrecioVenta = platoTemp.CalcularVenta(platoTemp.PorcentajeGanancia);

            txtPrecioCosto.Text = platoTemp.PrecioCosto.ToString("F2");
            txtPrecioVenta.Text = platoTemp.PrecioVenta.ToString("F2");

            return platoTemp;
        }
        private void CargarTreeViewPlatos()
        {
            try
            {
                tvwPlato.Nodes.Clear();

                // Traigo todos los platos desde la BLL/Mapper
                List<BEPlato> listaPlatos = oBLLPlato.ListarTodo(); // Asegurate que este método devuelva lista con ListaInsumos cargada

                //if (listaPlatos == null || !listaPlatos.Any())
                //{
                //    MessageBox.Show("No hay platos para mostrar.");
                //    return;
                //}

                // Agrupar por categoría
                var grupos = listaPlatos.GroupBy(p => p.Categoria);

                foreach (var grupo in grupos)
                {
                    TreeNode nodoCategoria = new TreeNode(grupo.Key.ToString());

                    foreach (var plato in grupo)
                    {
                        string textoPlato = $"{plato.Nombre} - Venta: {plato.PrecioVenta:F2}";
                        TreeNode nodoPlato = new TreeNode(textoPlato) { Tag = plato };

                        // Agregar insumos del plato
                        foreach (var insumoPlato in plato.ListaInsumos)
                        {
                            string textoInsumo = $"{insumoPlato.Insumo.Nombre} - Cant: {insumoPlato.Cantidad} {insumoPlato.Insumo.UnidadMedida} - Costo: {insumoPlato.CostoProporcional:F2}";
                            TreeNode nodoInsumo = new TreeNode(textoInsumo);
                            nodoPlato.Nodes.Add(nodoInsumo);
                        }

                        nodoCategoria.Nodes.Add(nodoPlato);
                    }

                    tvwPlato.Nodes.Add(nodoCategoria);
                }

                tvwPlato.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar TreeView: " + ex.Message);
            }
        }

        private void tvwPlato_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BEPlato platoSeleccionado)
            {
                CargarDetallePlato(platoSeleccionado);
                CargarDatosPlato(platoSeleccionado);
            }
        }

        private void CargarDetallePlato(BEPlato plato)
        {
            dgvDetallePlato.Rows.Clear();
            dgvDetallePlato.Columns.Clear();

            // Crear columnas
            dgvDetallePlato.Columns.Add("Insumo", "Insumo");
            dgvDetallePlato.Columns.Add("Cantidad", "Cantidad");
            dgvDetallePlato.Columns.Add("CostoProporcional", "Costo Proporcional");

            if (plato.ListaInsumos != null && plato.ListaInsumos.Count > 0)
            {
                foreach (var insumo in plato.ListaInsumos)
                {
                    decimal costoProporcional = insumo.CostoUnitario * insumo.Cantidad;

                    DataGridViewRow fila = new DataGridViewRow();
                    fila.CreateCells(dgvDetallePlato,
                        insumo.Insumo?.Nombre ?? "(Sin nombre)",
                        insumo.Cantidad.ToString("N2"),
                        costoProporcional.ToString("C2")
                    );

                    // Aquí asignamos el BEPlatoInsumo al Tag de la fila
                    fila.Tag = insumo;

                    dgvDetallePlato.Rows.Add(fila);
                }
            }
            else
            {
                dgvDetallePlato.Rows.Add("Sin insumos", "-", "-");
            }
        }

        private void CargarDatosPlato(BEPlato plato)
        {
            // Cargar los valores en los controles del formulario
            txtNombrePlato.Text = plato.Nombre;
            txtPrecioCosto.Text = plato.PrecioCosto.ToString("C2");
            txtPrecioVenta.Text = plato.PrecioVenta.ToString("C2");
            trackPorcentaje.Value = (int)plato.PorcentajeGanancia;
            txtPorcentaje.Text = plato.PorcentajeGanancia.ToString("F0") + " %";
            chkActivo.Checked = plato.Activo;

            // Seleccionar la categoría correspondiente en el ComboBox
            cmbCategorias.SelectedItem = plato.Categoria;

            // Guardar plato actual en memoria (por si luego querés modificarlo)
            oBEPlato = plato;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControlesPlato();
        }

        private void dgvDetallePlato_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetallePlato.CurrentRow == null || oBEPlato == null || oBEPlato.ListaInsumos == null)
                return;

            string nombreInsumo = dgvDetallePlato.CurrentRow.Cells["Insumo"].Value?.ToString();

            var insumoSeleccionado = oBEPlato.ListaInsumos
                .FirstOrDefault(i => i.Insumo.Nombre == nombreInsumo);

            if (insumoSeleccionado != null)
            {
                txtIdInsumo.Text = insumoSeleccionado.Insumo.Id.ToString();
                txtNombreInsumo.Text = insumoSeleccionado.Insumo.Nombre;
                txtUnidadMedida.Text = insumoSeleccionado.Insumo.UnidadMedida.ToString();
                txtCantidad.Text = insumoSeleccionado.Cantidad.ToString("N2");

                // Guardamos el insumo actual para modificar
                dgvDetallePlato.Tag = insumoSeleccionado;
            }
        }

        private void btnModificarInsumo_Click(object sender, EventArgs e)
        {
            // Obtener el insumo seleccionado en el detalle del plato
            BEPlatoInsumo insumoSeleccionado = dgvDetallePlato.Tag as BEPlatoInsumo;
            if (insumoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un insumo de la lista para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que la nueva cantidad sea decimal y mayor a 0
            if (!decimal.TryParse(txtCantidad.Text, out decimal nuevaCantidad) || nuevaCantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar el insumo original en la lista de insumos (inventario)
            BEInsumo insumoOriginal = listaInsumos.FirstOrDefault(i => i.Id == insumoSeleccionado.Insumo.Id);
            if (insumoOriginal == null)
            {
                MessageBox.Show("No se pudo obtener el stock del insumo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calcular stock total disponible: stock real + cantidad ya usada en el plato
            decimal stockTotalDisponible = insumoOriginal.Cantidad + insumoSeleccionado.Cantidad;

            if (nuevaCantidad > stockTotalDisponible)
            {
                MessageBox.Show("La cantidad ingresada supera la disponible.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calcular la diferencia entre nueva cantidad y la cantidad anterior
            decimal cantidadAnterior = insumoSeleccionado.Cantidad;
            decimal diferencia = nuevaCantidad - cantidadAnterior;

            // Actualizar cantidad usada en el plato
            insumoSeleccionado.Cantidad = nuevaCantidad;

            // Ajustar stock en inventario (XML)
            oBLLInsumo.DescontarStock(insumoOriginal.Id, diferencia);

            // Recalcular precio de costo y venta del plato
            oBEPlato.PrecioCosto = oBEPlato.ListaInsumos.Sum(i => i.CostoUnitario * i.Cantidad);
            oBEPlato.PrecioVenta = oBEPlato.PrecioCosto * (1 + oBEPlato.PorcentajeGanancia / 100);

            // Actualizar controles en formulario
            txtPrecioCosto.Text = oBEPlato.PrecioCosto.ToString("C2");
            txtPrecioVenta.Text = oBEPlato.PrecioVenta.ToString("C2");

            // Actualizar grilla y lista de insumos disponibles
            CargarInsumosDisponibles();
            CargarDetallePlato(oBEPlato);

            MessageBox.Show("Insumo modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarInsumo_Click(object sender, EventArgs e)
        {
            // Verificar que haya un insumo seleccionado en el detalle
            BEPlatoInsumo insumoSeleccionado = dgvDetallePlato.Tag as BEPlatoInsumo;
            if (insumoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un insumo de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar eliminación
            if (MessageBox.Show($"¿Desea eliminar el insumo {insumoSeleccionado.Insumo.Nombre} del plato?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Recuperar el stock original del inventario
            BEInsumo insumoOriginal = listaInsumos.FirstOrDefault(i => i.Id == insumoSeleccionado.Insumo.Id);
            if (insumoOriginal == null)
            {
                MessageBox.Show("No se pudo obtener el stock del insumo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sumar la cantidad usada de vuelta al stock del insumo
            decimal cantidadUsada = insumoSeleccionado.Cantidad;
            oBLLInsumo.DescontarStock(insumoOriginal.Id, -cantidadUsada); // pasamos negativo para sumar

            // Eliminar el insumo de la lista del plato
            oBEPlato.ListaInsumos.Remove(insumoSeleccionado);

            // Recalcular precios
            oBEPlato.PrecioCosto = oBEPlato.ListaInsumos.Sum(i => i.CostoUnitario * i.Cantidad);
            oBEPlato.PrecioVenta = oBEPlato.PrecioCosto * (1 + oBEPlato.PorcentajeGanancia / 100);

            // Actualizar controles
            txtPrecioCosto.Text = oBEPlato.PrecioCosto.ToString("C2");
            txtPrecioVenta.Text = oBEPlato.PrecioVenta.ToString("C2");

            // Actualizar grilla y lista de insumos disponibles
            CargarDetallePlato(oBEPlato);
            CargarInsumosDisponibles();
            LimpiarControlesInsumo();

            MessageBox.Show("Insumo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivo.Checked)
            {
                chkActivo.Text = "Desactivar";
            }
            else
            {
                chkActivo.Text = "Activar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificamos que haya un plato seleccionado
            if (oBEPlato == null)
            {
                MessageBox.Show("Seleccione un plato del árbol para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar eliminación
            var confirmar = MessageBox.Show(
                $"¿Está seguro de eliminar el plato '{oBEPlato.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar == DialogResult.Yes)
            {
                oBLLPlato.Eliminar(oBEPlato);
                MessageBox.Show("Plato eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar controles
                LimpiarControlesPlato();

                // Refrescar el TreeView
                CargarTreeViewPlatos();

            }
        }

       

        private void btnModificarPlato_Click(object sender, EventArgs e)
        {
            //if (oBEPlato == null)
            //{
            //    MessageBox.Show("Seleccione un plato para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //string nuevoNombre = txtNombrePlato.Text.Trim();

            //if (string.IsNullOrEmpty(nuevoNombre))
            //{
            //    MessageBox.Show("Ingrese un nombre válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// 🔹 Actualizamos el objeto
            //oBEPlato.Nombre = nuevoNombre;
            //oBEPlato.Categoria = (BEPlato.CategoriasPlato)cmbCategorias.SelectedItem;
            //oBEPlato.PrecioCosto = decimal.Parse(txtPrecioCosto.Text, System.Globalization.NumberStyles.Currency);
            //oBEPlato.PrecioVenta = decimal.Parse(txtPrecioVenta.Text, System.Globalization.NumberStyles.Currency);
            //oBEPlato.Activo = chkActivo.Checked;

            //// 🔹 Llamamos al mapper o BLL para guardar los cambios en el XML

            //oBLLPlato.Guardar(oBEPlato);

            //MessageBox.Show("Plato modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //// Refrescamos el TreeView
            //CargarTreeViewPlatos();


        }
    }
}
