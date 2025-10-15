namespace UI
{
    partial class frmMesas
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
            txtNumeroMesa = new TextBox();
            label5 = new Label();
            cmbEstado = new ComboBox();
            btnLimpiar = new Button();
            txtIDMesa = new TextBox();
            btnSalir = new Button();
            label2 = new Label();
            label1 = new Label();
            btnGuardar = new Button();
            btnEliminar = new Button();
            txtCapacidad = new TextBox();
            btnModificar = new Button();
            label4 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            dgvMesas = new DataGridView();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMesas).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtNumeroMesa);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(cmbEstado);
            groupBox2.Controls.Add(btnLimpiar);
            groupBox2.Controls.Add(txtIDMesa);
            groupBox2.Controls.Add(btnSalir);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnGuardar);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(txtCapacidad);
            groupBox2.Controls.Add(btnModificar);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(24, 25);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(367, 436);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "ABM Mesa";
            // 
            // txtNumeroMesa
            // 
            txtNumeroMesa.Font = new Font("Segoe UI", 11F);
            txtNumeroMesa.Location = new Point(140, 179);
            txtNumeroMesa.Name = "txtNumeroMesa";
            txtNumeroMesa.Size = new Size(150, 27);
            txtNumeroMesa.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(44, 217);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 15;
            label5.Text = "Estado";
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(140, 218);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.TabIndex = 14;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 11F);
            btnLimpiar.Location = new Point(32, 335);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(102, 50);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtIDMesa
            // 
            txtIDMesa.Font = new Font("Segoe UI", 11F);
            txtIDMesa.Location = new Point(140, 96);
            txtIDMesa.Name = "txtIDMesa";
            txtIDMesa.ReadOnly = true;
            txtIDMesa.Size = new Size(150, 27);
            txtIDMesa.TabIndex = 11;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 11F);
            btnSalir.Location = new Point(248, 335);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(102, 50);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(44, 99);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 10;
            label2.Text = "ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(120, 41);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 4;
            label1.Text = "Crear Mesa";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 11F);
            btnGuardar.Location = new Point(32, 270);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 38);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(248, 270);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 38);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtCapacidad
            // 
            txtCapacidad.Font = new Font("Segoe UI", 11F);
            txtCapacidad.Location = new Point(140, 135);
            txtCapacidad.Name = "txtCapacidad";
            txtCapacidad.Size = new Size(150, 27);
            txtCapacidad.TabIndex = 8;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 11F);
            btnModificar.Location = new Point(140, 270);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 38);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(44, 138);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 7;
            label4.Text = "Capacidad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(44, 182);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 6;
            label3.Text = "Num Mesa";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvMesas);
            groupBox1.Location = new Point(470, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(594, 436);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mesas";
            // 
            // dgvMesas
            // 
            dgvMesas.AllowUserToAddRows = false;
            dgvMesas.AllowUserToDeleteRows = false;
            dgvMesas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMesas.Location = new Point(16, 22);
            dgvMesas.MultiSelect = false;
            dgvMesas.Name = "dgvMesas";
            dgvMesas.ReadOnly = true;
            dgvMesas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMesas.Size = new Size(572, 408);
            dgvMesas.TabIndex = 10;
            dgvMesas.SelectionChanged += dgvMesas_SelectionChanged_1;
            // 
            // frmMesas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1088, 493);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmMesas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mesas";
            Load += frmMesas_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMesas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private CheckBox chkEncriptar;
        private Button btnLimpiar;
        private TextBox txtIDMesa;
        private Button btnSalir;
        private Label label2;
        private Label label1;
        private Button btnGuardar;
        private TextBox txtPassword;
        private Button btnEliminar;
        private TextBox txtCapacidad;
        private Button btnModificar;
        private Label label4;
        private Label label3;
        private GroupBox groupBox1;
        private DataGridView dgvMesas;
        private ComboBox cmbEstado;
        private Label label5;
        private TextBox txtNumeroMesa;
    }
}