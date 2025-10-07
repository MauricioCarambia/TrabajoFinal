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
            dgvClientes = new DataGridView();
            groupBox1 = new GroupBox();
            numericUpDown1 = new NumericUpDown();
            label5 = new Label();
            textBox1 = new TextBox();
            btnCerrar = new Button();
            label3 = new Label();
            label2 = new Label();
            txtDNI = new TextBox();
            label1 = new Label();
            btnEliminar = new Button();
            btnModificar = new Button();
            btnGuardar = new Button();
            txtNombre = new TextBox();
            comboBox1 = new ComboBox();
            label4 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvClientes);
            groupBox2.Location = new Point(427, 109);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(706, 440);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Platos";
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
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(comboBox1);
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
            groupBox1.Location = new Point(63, 109);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(323, 440);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Plato";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(6, 217);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(210, 23);
            numericUpDown1.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(6, 258);
            label5.Name = "label5";
            label5.Size = new Size(112, 20);
            label5.TabIndex = 9;
            label5.Text = "Unidad Medida";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 11F);
            textBox1.Location = new Point(6, 281);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(210, 27);
            textBox1.TabIndex = 8;
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI", 11F);
            btnCerrar.Location = new Point(125, 389);
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
            label3.Location = new Point(6, 194);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 6;
            label3.Text = "Cantidad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(6, 131);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 4;
            label2.Text = "Precio";
            // 
            // txtDNI
            // 
            txtDNI.Font = new Font("Segoe UI", 11F);
            txtDNI.Location = new Point(6, 154);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(210, 27);
            txtDNI.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(6, 73);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(17, 389);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 39);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 11F);
            btnModificar.Location = new Point(125, 344);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 39);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 11F);
            btnGuardar.Location = new Point(17, 344);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 39);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(6, 96);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(210, 27);
            txtNombre.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(11, 47);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(205, 23);
            comboBox1.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(11, 19);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 12;
            label4.Text = "Categoria";
            // 
            // frmPlatos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 707);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmPlatos";
            Text = "frmPlatos";
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private DataGridView dgvClientes;
        private GroupBox groupBox1;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private TextBox textBox1;
        private Button btnCerrar;
        private Label label3;
        private Label label2;
        private TextBox txtDNI;
        private Label label1;
        private Button btnEliminar;
        private Button btnModificar;
        private Button btnGuardar;
        private TextBox txtNombre;
        private Label label4;
        private ComboBox comboBox1;
    }
}