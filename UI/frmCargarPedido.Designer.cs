namespace UI
{
    partial class frmCargarPedido
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
            cmbReservas = new ComboBox();
            label1 = new Label();
            dgvPlatos = new DataGridView();
            cmbCategoria = new ComboBox();
            label2 = new Label();
            txtCantidad = new TextBox();
            label3 = new Label();
            dgvPedidos = new DataGridView();
            groupBox1 = new GroupBox();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnConfirmar = new Button();
            btnSalir = new Button();
            btnLimpiar = new Button();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            label7 = new Label();
            txtReserva = new TextBox();
            txtMesa = new TextBox();
            txtTotal = new TextBox();
            txtCliente = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPlatos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // cmbReservas
            // 
            cmbReservas.Font = new Font("Segoe UI", 10F);
            cmbReservas.FormattingEnabled = true;
            cmbReservas.Location = new Point(6, 54);
            cmbReservas.Name = "cmbReservas";
            cmbReservas.Size = new Size(172, 25);
            cmbReservas.TabIndex = 0;
            cmbReservas.SelectedIndexChanged += cmbReservas_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(42, 19);
            label1.TabIndex = 1;
            label1.Text = "Mesa";
            // 
            // dgvPlatos
            // 
            dgvPlatos.AllowUserToAddRows = false;
            dgvPlatos.AllowUserToDeleteRows = false;
            dgvPlatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlatos.Location = new Point(6, 24);
            dgvPlatos.MultiSelect = false;
            dgvPlatos.Name = "dgvPlatos";
            dgvPlatos.ReadOnly = true;
            dgvPlatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlatos.Size = new Size(497, 280);
            dgvPlatos.TabIndex = 2;
            // 
            // cmbCategoria
            // 
            cmbCategoria.Font = new Font("Segoe UI", 10F);
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(6, 119);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(172, 25);
            cmbCategoria.TabIndex = 3;
            cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(3, 90);
            label2.Name = "label2";
            label2.Size = new Size(68, 19);
            label2.TabIndex = 4;
            label2.Text = "Categoria";
            // 
            // txtCantidad
            // 
            txtCantidad.Font = new Font("Segoe UI", 10F);
            txtCantidad.Location = new Point(6, 183);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(100, 25);
            txtCantidad.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(6, 161);
            label3.Name = "label3";
            label3.Size = new Size(64, 19);
            label3.TabIndex = 6;
            label3.Text = "Cantidad";
            // 
            // dgvPedidos
            // 
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.AllowUserToDeleteRows = false;
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Location = new Point(355, 17);
            dgvPedidos.MultiSelect = false;
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(454, 233);
            dgvPedidos.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvPlatos);
            groupBox1.Location = new Point(243, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(509, 310);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Platos";
            // 
            // btnAgregar
            // 
            btnAgregar.Font = new Font("Segoe UI", 10F);
            btnAgregar.Location = new Point(9, 233);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(101, 35);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar Plato";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.Location = new Point(118, 233);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(101, 35);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar Plato";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click_1;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Font = new Font("Segoe UI", 10F);
            btnConfirmar.Location = new Point(21, 339);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(210, 34);
            btnConfirmar.TabIndex = 12;
            btnConfirmar.Text = "Confirmar Pedido";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(21, 554);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(210, 35);
            btnSalir.TabIndex = 13;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.Location = new Point(21, 407);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(210, 35);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(cmbReservas);
            groupBox3.Controls.Add(cmbCategoria);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(txtCantidad);
            groupBox3.Controls.Add(btnEliminar);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(btnAgregar);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(225, 310);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "ABM Pedido";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dgvPedidos);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(txtReserva);
            groupBox4.Controls.Add(txtMesa);
            groupBox4.Controls.Add(txtTotal);
            groupBox4.Controls.Add(txtCliente);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label4);
            groupBox4.Location = new Point(243, 339);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(822, 264);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Informacion Pedido";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(21, 58);
            label7.Name = "label7";
            label7.Size = new Size(56, 19);
            label7.TabIndex = 7;
            label7.Text = "Reserva";
            // 
            // txtReserva
            // 
            txtReserva.Location = new Point(147, 54);
            txtReserva.Name = "txtReserva";
            txtReserva.ReadOnly = true;
            txtReserva.Size = new Size(100, 23);
            txtReserva.TabIndex = 6;
            // 
            // txtMesa
            // 
            txtMesa.Font = new Font("Segoe UI", 10F);
            txtMesa.Location = new Point(147, 114);
            txtMesa.Name = "txtMesa";
            txtMesa.ReadOnly = true;
            txtMesa.Size = new Size(192, 25);
            txtMesa.TabIndex = 5;
            // 
            // txtTotal
            // 
            txtTotal.Font = new Font("Segoe UI", 10F);
            txtTotal.Location = new Point(147, 145);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(192, 25);
            txtTotal.TabIndex = 4;
            // 
            // txtCliente
            // 
            txtCliente.Font = new Font("Segoe UI", 10F);
            txtCliente.Location = new Point(147, 83);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(192, 25);
            txtCliente.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(21, 148);
            label6.Name = "label6";
            label6.Size = new Size(38, 19);
            label6.TabIndex = 2;
            label6.Text = "Total";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(21, 117);
            label5.Name = "label5";
            label5.Size = new Size(42, 19);
            label5.TabIndex = 1;
            label5.Text = "Mesa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(21, 86);
            label4.Name = "label4";
            label4.Size = new Size(51, 19);
            label4.TabIndex = 0;
            label4.Text = "Cliente";
            // 
            // frmCargarPedido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1075, 654);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(btnLimpiar);
            Controls.Add(btnSalir);
            Controls.Add(btnConfirmar);
            Controls.Add(groupBox1);
            Name = "frmCargarPedido";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cargar Pedido";
            Load += frmCargarPedido_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPlatos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbReservas;
        private Label label1;
        private DataGridView dgvPlatos;
        private ComboBox cmbCategoria;
        private Label label2;
        private TextBox txtCantidad;
        private Label label3;
        private DataGridView dgvPedidos;
        private GroupBox groupBox1;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnConfirmar;
        private Button btnSalir;
        private Button btnLimpiar;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox txtMesa;
        private TextBox txtTotal;
        private TextBox txtCliente;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label7;
        private TextBox txtReserva;
    }
}