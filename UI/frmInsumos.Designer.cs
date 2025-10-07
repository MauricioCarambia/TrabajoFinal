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
            dgvClientes = new DataGridView();
            groupBox1 = new GroupBox();
            btnCerrar = new Button();
            label3 = new Label();
            label2 = new Label();
            txtDNI = new TextBox();
            label1 = new Label();
            btnEliminar = new Button();
            btnModificar = new Button();
            btnGuardar = new Button();
            txtNombre = new TextBox();
            label5 = new Label();
            textBox1 = new TextBox();
            numericUpDown1 = new NumericUpDown();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
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
            groupBox2.Controls.Add(dgvClientes);
            groupBox2.Location = new Point(431, 69);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(706, 440);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Insumos";
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(6, 22);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.Size = new Size(687, 405);
            dgvClientes.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(btnCerrar);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtDNI);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(btnGuardar);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Location = new Point(67, 69);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(272, 398);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Insumos";
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI", 11F);
            btnCerrar.Location = new Point(125, 338);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(102, 39);
            btnCerrar.TabIndex = 7;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(6, 143);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 6;
            label3.Text = "Cantidad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(6, 80);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 4;
            label2.Text = "Precio";
            // 
            // txtDNI
            // 
            txtDNI.Font = new Font("Segoe UI", 11F);
            txtDNI.Location = new Point(6, 103);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(210, 27);
            txtDNI.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(6, 22);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(17, 338);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 39);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 11F);
            btnModificar.Location = new Point(125, 293);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 39);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 11F);
            btnGuardar.Location = new Point(17, 293);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 39);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(6, 45);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(210, 27);
            txtNombre.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(6, 207);
            label5.Name = "label5";
            label5.Size = new Size(112, 20);
            label5.TabIndex = 9;
            label5.Text = "Unidad Medida";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 11F);
            textBox1.Location = new Point(6, 230);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(210, 27);
            textBox1.TabIndex = 8;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(6, 166);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(210, 23);
            numericUpDown1.TabIndex = 10;
            // 
            // frmInsumos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1299, 717);
            Controls.Add(txtBuscar);
            Controls.Add(label4);
            Controls.Add(btnBuscar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmInsumos";
            Text = "frmInsumos";
            WindowState = FormWindowState.Maximized;
            Load += frmInsumos_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscar;
        private Label label4;
        private Button btnBuscar;
        private GroupBox groupBox2;
        private DataGridView dgvClientes;
        private GroupBox groupBox1;
        private Button btnCerrar;
        private Label label3;
        private Label label2;
        private TextBox txtDNI;
        private Label label1;
        private Button btnEliminar;
        private Button btnModificar;
        private Button btnGuardar;
        private TextBox txtNombre;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private TextBox textBox1;
    }
}