namespace UI
{
    partial class frmPlatos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox2 = new GroupBox();
            dgvDetallePlato = new DataGridView();
            groupBox1 = new GroupBox();
            btnEliminarInsumo = new Button();
            label4 = new Label();
            cmbCategorias = new ComboBox();
            btnModificarInsumo = new Button();
            label1 = new Label();
            btnAgregarInsumo = new Button();
            txtNombrePlato = new TextBox();
            btnLimpiar = new Button();
            btnSalir = new Button();
            label5 = new Label();
            txtPrecioVenta = new TextBox();
            label3 = new Label();
            txtPrecioCosto = new TextBox();
            txtPorcentaje = new TextBox();
            label2 = new Label();
            trackPorcentaje = new TrackBar();
            btnEliminar = new Button();
            btnGuardar = new Button();
            groupBox3 = new GroupBox();
            dgvInsumos = new DataGridView();
            groupBox4 = new GroupBox();
            txtUnidadMedida = new TextBox();
            label7 = new Label();
            txtIdInsumo = new TextBox();
            txtCantidad = new TextBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            txtNombreInsumo = new TextBox();
            tvwPlato = new TreeView();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            btnModificarPlato = new Button();
            chkActivo = new CheckBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetallePlato).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackPorcentaje).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInsumos).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDetallePlato);
            groupBox2.Location = new Point(721, 290);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(566, 362);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle Plato";
            // 
            // dgvDetallePlato
            // 
            dgvDetallePlato.AllowUserToAddRows = false;
            dgvDetallePlato.AllowUserToDeleteRows = false;
            dgvDetallePlato.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallePlato.Location = new Point(6, 22);
            dgvDetallePlato.MultiSelect = false;
            dgvDetallePlato.Name = "dgvDetallePlato";
            dgvDetallePlato.ReadOnly = true;
            dgvDetallePlato.Size = new Size(554, 334);
            dgvDetallePlato.TabIndex = 2;
            dgvDetallePlato.SelectionChanged += dgvDetallePlato_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnEliminarInsumo);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cmbCategorias);
            groupBox1.Controls.Add(btnModificarInsumo);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnAgregarInsumo);
            groupBox1.Controls.Add(txtNombrePlato);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(703, 250);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Plato";
            // 
            // btnEliminarInsumo
            // 
            btnEliminarInsumo.Font = new Font("Microsoft Sans Serif", 10F);
            btnEliminarInsumo.Location = new Point(262, 176);
            btnEliminarInsumo.Name = "btnEliminarInsumo";
            btnEliminarInsumo.Size = new Size(102, 42);
            btnEliminarInsumo.TabIndex = 5;
            btnEliminarInsumo.Text = "Eliminar Insumo";
            btnEliminarInsumo.UseVisualStyleBackColor = true;
            btnEliminarInsumo.Click += btnEliminarInsumo_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10F);
            label4.Location = new Point(11, 31);
            label4.Name = "label4";
            label4.Size = new Size(69, 17);
            label4.TabIndex = 12;
            label4.Text = "Categoria";
            // 
            // cmbCategorias
            // 
            cmbCategorias.Font = new Font("Microsoft Sans Serif", 10F);
            cmbCategorias.FormattingEnabled = true;
            cmbCategorias.Location = new Point(11, 59);
            cmbCategorias.Name = "cmbCategorias";
            cmbCategorias.Size = new Size(210, 24);
            cmbCategorias.TabIndex = 0;
            // 
            // btnModificarInsumo
            // 
            btnModificarInsumo.Font = new Font("Microsoft Sans Serif", 10F);
            btnModificarInsumo.Location = new Point(142, 176);
            btnModificarInsumo.Name = "btnModificarInsumo";
            btnModificarInsumo.Size = new Size(102, 42);
            btnModificarInsumo.TabIndex = 4;
            btnModificarInsumo.Text = "Modificar Insumo";
            btnModificarInsumo.UseVisualStyleBackColor = true;
            btnModificarInsumo.Click += btnModificarInsumo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F);
            label1.Location = new Point(11, 95);
            label1.Name = "label1";
            label1.Size = new Size(58, 17);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // btnAgregarInsumo
            // 
            btnAgregarInsumo.Font = new Font("Microsoft Sans Serif", 10F);
            btnAgregarInsumo.Location = new Point(21, 176);
            btnAgregarInsumo.Name = "btnAgregarInsumo";
            btnAgregarInsumo.Size = new Size(102, 42);
            btnAgregarInsumo.TabIndex = 2;
            btnAgregarInsumo.Text = "Agregar Insumo";
            btnAgregarInsumo.UseVisualStyleBackColor = true;
            btnAgregarInsumo.Click += btnAgregarInsumo_Click_1;
            // 
            // txtNombrePlato
            // 
            txtNombrePlato.Font = new Font("Microsoft Sans Serif", 10F);
            txtNombrePlato.Location = new Point(11, 115);
            txtNombrePlato.Name = "txtNombrePlato";
            txtNombrePlato.Size = new Size(210, 23);
            txtNombrePlato.TabIndex = 1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Microsoft Sans Serif", 10F);
            btnLimpiar.Location = new Point(21, 244);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(102, 49);
            btnLimpiar.TabIndex = 13;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Microsoft Sans Serif", 10F);
            btnSalir.Location = new Point(263, 586);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(102, 39);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 10F);
            label5.Location = new Point(10, 163);
            label5.Name = "label5";
            label5.Size = new Size(89, 17);
            label5.TabIndex = 21;
            label5.Text = "Precio Venta";
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Font = new Font("Microsoft Sans Serif", 10F);
            txtPrecioVenta.Location = new Point(10, 183);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.ReadOnly = true;
            txtPrecioVenta.Size = new Size(210, 23);
            txtPrecioVenta.TabIndex = 20;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10F);
            label3.Location = new Point(11, 102);
            label3.Name = "label3";
            label3.Size = new Size(88, 17);
            label3.TabIndex = 19;
            label3.Text = "Precio Costo";
            // 
            // txtPrecioCosto
            // 
            txtPrecioCosto.Font = new Font("Microsoft Sans Serif", 10F);
            txtPrecioCosto.Location = new Point(11, 122);
            txtPrecioCosto.Name = "txtPrecioCosto";
            txtPrecioCosto.ReadOnly = true;
            txtPrecioCosto.Size = new Size(210, 23);
            txtPrecioCosto.TabIndex = 18;
            // 
            // txtPorcentaje
            // 
            txtPorcentaje.Font = new Font("Segoe UI", 10F);
            txtPorcentaje.Location = new Point(279, 54);
            txtPorcentaje.Name = "txtPorcentaje";
            txtPorcentaje.PlaceholderText = "%";
            txtPorcentaje.ReadOnly = true;
            txtPorcentaje.Size = new Size(40, 25);
            txtPorcentaje.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(10, 22);
            label2.Name = "label2";
            label2.Size = new Size(65, 19);
            label2.TabIndex = 15;
            label2.Text = "Ganancia";
            // 
            // trackPorcentaje
            // 
            trackPorcentaje.Location = new Point(10, 54);
            trackPorcentaje.Maximum = 100;
            trackPorcentaje.Name = "trackPorcentaje";
            trackPorcentaje.Size = new Size(263, 45);
            trackPorcentaje.TabIndex = 14;
            trackPorcentaje.Scroll += trackPorcentaje_Scroll_1;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Microsoft Sans Serif", 10F);
            btnEliminar.Location = new Point(251, 247);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 46);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar Plato";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Microsoft Sans Serif", 10F);
            btnGuardar.Location = new Point(251, 143);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 46);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Confirmar Plato";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvInsumos);
            groupBox3.Location = new Point(721, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(566, 278);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Insumos";
            // 
            // dgvInsumos
            // 
            dgvInsumos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInsumos.Location = new Point(6, 22);
            dgvInsumos.MultiSelect = false;
            dgvInsumos.Name = "dgvInsumos";
            dgvInsumos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInsumos.Size = new Size(554, 250);
            dgvInsumos.TabIndex = 0;
            dgvInsumos.SelectionChanged += dgvInsumos_SelectionChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txtUnidadMedida);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(txtIdInsumo);
            groupBox4.Controls.Add(txtCantidad);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(txtNombreInsumo);
            groupBox4.Location = new Point(392, 31);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(317, 216);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            groupBox4.Text = "Datos Insumos";
            // 
            // txtUnidadMedida
            // 
            txtUnidadMedida.Font = new Font("Segoe UI", 10F);
            txtUnidadMedida.Location = new Point(121, 127);
            txtUnidadMedida.Name = "txtUnidadMedida";
            txtUnidadMedida.ReadOnly = true;
            txtUnidadMedida.Size = new Size(166, 25);
            txtUnidadMedida.TabIndex = 26;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(6, 35);
            label7.Name = "label7";
            label7.Size = new Size(23, 19);
            label7.TabIndex = 25;
            label7.Text = "ID";
            // 
            // txtIdInsumo
            // 
            txtIdInsumo.Font = new Font("Segoe UI", 10F);
            txtIdInsumo.Location = new Point(121, 32);
            txtIdInsumo.Name = "txtIdInsumo";
            txtIdInsumo.ReadOnly = true;
            txtIdInsumo.Size = new Size(166, 25);
            txtIdInsumo.TabIndex = 24;
            // 
            // txtCantidad
            // 
            txtCantidad.Font = new Font("Segoe UI", 10F);
            txtCantidad.Location = new Point(121, 174);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(166, 25);
            txtCantidad.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(6, 130);
            label9.Name = "label9";
            label9.Size = new Size(103, 19);
            label9.TabIndex = 20;
            label9.Text = "Unidad Medida";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(6, 177);
            label10.Name = "label10";
            label10.Size = new Size(64, 19);
            label10.TabIndex = 19;
            label10.Text = "Cantidad";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F);
            label11.Location = new Point(6, 81);
            label11.Name = "label11";
            label11.Size = new Size(59, 19);
            label11.TabIndex = 18;
            label11.Text = "Nombre";
            // 
            // txtNombreInsumo
            // 
            txtNombreInsumo.Font = new Font("Segoe UI", 10F);
            txtNombreInsumo.Location = new Point(121, 78);
            txtNombreInsumo.Name = "txtNombreInsumo";
            txtNombreInsumo.ReadOnly = true;
            txtNombreInsumo.Size = new Size(166, 25);
            txtNombreInsumo.TabIndex = 17;
            // 
            // tvwPlato
            // 
            tvwPlato.Location = new Point(6, 22);
            tvwPlato.Name = "tvwPlato";
            tvwPlato.Size = new Size(311, 356);
            tvwPlato.TabIndex = 14;
            tvwPlato.AfterSelect += tvwPlato_AfterSelect;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(tvwPlato);
            groupBox5.Location = new Point(392, 268);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(323, 384);
            groupBox5.TabIndex = 15;
            groupBox5.TabStop = false;
            groupBox5.Text = "Platos";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btnModificarPlato);
            groupBox6.Controls.Add(btnLimpiar);
            groupBox6.Controls.Add(chkActivo);
            groupBox6.Controls.Add(label5);
            groupBox6.Controls.Add(label2);
            groupBox6.Controls.Add(txtPrecioVenta);
            groupBox6.Controls.Add(btnGuardar);
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(txtPrecioCosto);
            groupBox6.Controls.Add(btnEliminar);
            groupBox6.Controls.Add(txtPorcentaje);
            groupBox6.Controls.Add(trackPorcentaje);
            groupBox6.Location = new Point(12, 268);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(374, 307);
            groupBox6.TabIndex = 16;
            groupBox6.TabStop = false;
            groupBox6.Text = "Confirma Plato";
            // 
            // btnModificarPlato
            // 
            btnModificarPlato.Font = new Font("Microsoft Sans Serif", 10F);
            btnModificarPlato.Location = new Point(251, 195);
            btnModificarPlato.Name = "btnModificarPlato";
            btnModificarPlato.Size = new Size(102, 46);
            btnModificarPlato.TabIndex = 22;
            btnModificarPlato.Text = "Modificar Plato";
            btnModificarPlato.UseVisualStyleBackColor = true;
            btnModificarPlato.Click += btnModificarPlato_Click;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(279, 102);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(80, 19);
            chkActivo.TabIndex = 13;
            chkActivo.Text = "Desactivar";
            chkActivo.UseVisualStyleBackColor = true;
            chkActivo.CheckedChanged += chkActivo_CheckedChanged;
            // 
            // frmPlatos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1299, 664);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(btnSalir);
            Controls.Add(groupBox1);
            Name = "frmPlatos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Platos";
            Load += frmPlatos_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetallePlato).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackPorcentaje).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInsumos).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private DataGridView dgvDetallePlato;
        private GroupBox groupBox1;
        private Button btnSalir;
        private Label label1;
        private Button btnEliminar;
        private Button btnGuardar;
        private TextBox txtNombrePlato;
        private Button btnLimpiar;
        private Label label4;
        private ComboBox cmbCategorias;
        private GroupBox groupBox3;
        private TrackBar trackPorcentaje;
        private Label label2;
        private TextBox txtPorcentaje;
        private GroupBox groupBox4;
        private TextBox txtUnidadMedida;
        private Label label7;
        private TextBox txtIdInsumo;
        private TextBox txtCantidad;
        private Label label9;
        private Label label10;
        private Label label11;
        private TextBox txtNombreInsumo;
        private Label label5;
        private TextBox txtPrecioVenta;
        private Label label3;
        private TextBox txtPrecioCosto;
        private Button btnEliminarInsumo;
        private Button btnModificarInsumo;
        private Button btnAgregarInsumo;
        private TreeView tvwPlato;
        private GroupBox groupBox5;
        private DataGridView dgvInsumos;
        private GroupBox groupBox6;
        private CheckBox chkActivo;
        private Button btnModificarPlato;
    }
}