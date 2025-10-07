namespace UI
{
    partial class frmClientes
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
            txtNombre = new TextBox();
            btnGuardar = new Button();
            dgvClientes = new DataGridView();
            groupBox1 = new GroupBox();
            btnCerrar = new Button();
            label3 = new Label();
            txtTelefono = new TextBox();
            label2 = new Label();
            txtDNI = new TextBox();
            label1 = new Label();
            btnEliminar = new Button();
            btnModificar = new Button();
            groupBox2 = new GroupBox();
            btnBuscar = new Button();
            label4 = new Label();
            txtBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(25, 51);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(210, 27);
            txtNombre.TabIndex = 0;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 11F);
            btnGuardar.Location = new Point(25, 281);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 39);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(15, 22);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.Size = new Size(556, 415);
            dgvClientes.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCerrar);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtDNI);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(btnGuardar);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Location = new Point(70, 154);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(263, 394);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Cliente";
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI", 11F);
            btnCerrar.Location = new Point(133, 326);
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
            label3.Location = new Point(25, 178);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 6;
            label3.Text = "Telefono";
            // 
            // txtTelefono
            // 
            txtTelefono.Font = new Font("Segoe UI", 11F);
            txtTelefono.Location = new Point(25, 201);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(210, 27);
            txtTelefono.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(25, 102);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 4;
            label2.Text = "DNI";
            // 
            // txtDNI
            // 
            txtDNI.Font = new Font("Segoe UI", 11F);
            txtDNI.Location = new Point(25, 125);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(210, 27);
            txtDNI.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(25, 28);
            label1.Name = "label1";
            label1.Size = new Size(134, 20);
            label1.TabIndex = 2;
            label1.Text = "Nombre y apellido";
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(25, 326);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 39);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 11F);
            btnModificar.Location = new Point(133, 281);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 39);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvClientes);
            groupBox2.Location = new Point(422, 154);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(585, 464);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Clientes";
            // 
            // btnBuscar
            // 
            btnBuscar.Font = new Font("Segoe UI", 11F);
            btnBuscar.Location = new Point(672, 53);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(99, 37);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(281, 61);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 6;
            label4.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.Location = new Point(351, 58);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(296, 27);
            txtBuscar.TabIndex = 7;
            // 
            // frmClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 684);
            Controls.Add(txtBuscar);
            Controls.Add(label4);
            Controls.Add(btnBuscar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmClientes";
            Text = "Clientes";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private Button btnGuardar;
        private DataGridView dgvClientes;
        private GroupBox groupBox1;
        private Label label3;
        private TextBox txtTelefono;
        private Label label2;
        private TextBox txtDNI;
        private Label label1;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnCerrar;
        private GroupBox groupBox2;
        private Button btnBuscar;
        private Label label4;
        private TextBox txtBuscar;
    }
}