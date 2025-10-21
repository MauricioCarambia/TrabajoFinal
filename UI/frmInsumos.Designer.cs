namespace UI
{
    partial class frmInsumos
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
            txtBuscar = new TextBox();
            label4 = new Label();
            btnBuscar = new Button();
            groupBox2 = new GroupBox();
            dgvInsumos = new DataGridView();
            groupBox1 = new GroupBox();
            cmbUnidadMedida = new ComboBox();
            label6 = new Label();
            txtIdInsumo = new TextBox();
            txtPrecio = new TextBox();
            label2 = new Label();
            btnLimpiar = new Button();
            txtCantidad = new TextBox();
            label5 = new Label();
            btnSalir = new Button();
            label3 = new Label();
            label1 = new Label();
            btnEliminar = new Button();
            btnModificar = new Button();
            btnGuardar = new Button();
            txtNombre = new TextBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInsumos).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.Location = new Point(213, -52);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(296, 27);
            txtBuscar.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(143, -49);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 11;
            label4.Text = "Buscar";
            // 
            // btnBuscar
            // 
            btnBuscar.Font = new Font("Segoe UI", 11F);
            btnBuscar.Location = new Point(534, -57);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(99, 37);
            btnBuscar.TabIndex = 10;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvInsumos);
            groupBox2.Location = new Point(396, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(706, 462);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Insumos";
            // 
            // dgvInsumos
            // 
            dgvInsumos.AllowUserToAddRows = false;
            dgvInsumos.AllowUserToDeleteRows = false;
            dgvInsumos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInsumos.Location = new Point(6, 22);
            dgvInsumos.MultiSelect = false;
            dgvInsumos.Name = "dgvInsumos";
            dgvInsumos.ReadOnly = true;
            dgvInsumos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInsumos.Size = new Size(687, 434);
            dgvInsumos.TabIndex = 2;
            dgvInsumos.SelectionChanged += dgvInsumos_SelectionChanged_1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbUnidadMedida);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtIdInsumo);
            groupBox1.Controls.Add(txtPrecio);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(txtCantidad);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(btnSalir);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(btnGuardar);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Location = new Point(25, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(345, 462);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Insumos";
            // 
            // cmbUnidadMedida
            // 
            cmbUnidadMedida.FormattingEnabled = true;
            cmbUnidadMedida.Location = new Point(135, 130);
            cmbUnidadMedida.Name = "cmbUnidadMedida";
            cmbUnidadMedida.Size = new Size(166, 23);
            cmbUnidadMedida.TabIndex = 16;
            cmbUnidadMedida.SelectedIndexChanged += cmbUnidadesMedida_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(6, 35);
            label6.Name = "label6";
            label6.Size = new Size(23, 19);
            label6.TabIndex = 15;
            label6.Text = "ID";
            // 
            // txtIdInsumo
            // 
            txtIdInsumo.Font = new Font("Segoe UI", 10F);
            txtIdInsumo.Location = new Point(135, 32);
            txtIdInsumo.Name = "txtIdInsumo";
            txtIdInsumo.ReadOnly = true;
            txtIdInsumo.Size = new Size(166, 25);
            txtIdInsumo.TabIndex = 14;
            // 
            // txtPrecio
            // 
            txtPrecio.Font = new Font("Segoe UI", 10F);
            txtPrecio.Location = new Point(135, 218);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(166, 25);
            txtPrecio.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(6, 221);
            label2.Name = "label2";
            label2.Size = new Size(46, 19);
            label2.TabIndex = 12;
            label2.Text = "Precio";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.Location = new Point(16, 378);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(102, 51);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtCantidad
            // 
            txtCantidad.Font = new Font("Segoe UI", 10F);
            txtCantidad.Location = new Point(135, 174);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(166, 25);
            txtCantidad.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(6, 130);
            label5.Name = "label5";
            label5.Size = new Size(103, 19);
            label5.TabIndex = 9;
            label5.Text = "Unidad Medida";
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(237, 390);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(102, 39);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(6, 177);
            label3.Name = "label3";
            label3.Size = new Size(64, 19);
            label3.TabIndex = 6;
            label3.Text = "Cantidad";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(6, 81);
            label1.Name = "label1";
            label1.Size = new Size(59, 19);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.Location = new Point(233, 276);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 39);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 10F);
            btnModificar.Location = new Point(125, 276);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 39);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.Location = new Point(17, 276);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 39);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(135, 78);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(166, 25);
            txtNombre.TabIndex = 0;
            // 
            // frmInsumos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 504);
            Controls.Add(txtBuscar);
            Controls.Add(label4);
            Controls.Add(btnBuscar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmInsumos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Insumos";
            Load += frmInsumos_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInsumos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscar;
        private Label label4;
        private Button btnBuscar;
        private GroupBox groupBox2;
        private DataGridView dgvInsumos;
        private GroupBox groupBox1;
        private Button btnSalir;
        private Label label3;
        private Label label1;
        private Button btnEliminar;
        private Button btnModificar;
        private Button btnGuardar;
        private TextBox txtNombre;
        private Label label5;
        private Button btnLimpiar;
        private TextBox txtCantidad;
        private TextBox txtPrecio;
        private Label label2;
        private Label label6;
        private TextBox txtIdInsumo;
        private ComboBox cmbUnidadMedida;
    }
}