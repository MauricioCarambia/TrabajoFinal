namespace UI
{
    partial class frmPedidos
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
            dgvDetallePedido = new DataGridView();
            btnCancelarPedido = new Button();
            btnEnviarCocina = new Button();
            btnSalir = new Button();
            label7 = new Label();
            txtReserva = new TextBox();
            txtMesa = new TextBox();
            txtTotal = new TextBox();
            txtCliente = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            dgvPedidos = new DataGridView();
            btnEnviarCobranza = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            btnEliminarPlato = new Button();
            btnRecargar = new Button();
            txtCantidad = new TextBox();
            label1 = new Label();
            btnEntregado = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDetallePedido
            // 
            dgvDetallePedido.AllowUserToAddRows = false;
            dgvDetallePedido.AllowUserToDeleteRows = false;
            dgvDetallePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallePedido.Location = new Point(6, 22);
            dgvDetallePedido.MultiSelect = false;
            dgvDetallePedido.Name = "dgvDetallePedido";
            dgvDetallePedido.ReadOnly = true;
            dgvDetallePedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetallePedido.Size = new Size(520, 244);
            dgvDetallePedido.TabIndex = 0;
            // 
            // btnCancelarPedido
            // 
            btnCancelarPedido.Font = new Font("Segoe UI", 10F);
            btnCancelarPedido.Location = new Point(23, 481);
            btnCancelarPedido.Name = "btnCancelarPedido";
            btnCancelarPedido.Size = new Size(342, 34);
            btnCancelarPedido.TabIndex = 1;
            btnCancelarPedido.Text = "Cancelar Pedido";
            btnCancelarPedido.UseVisualStyleBackColor = true;
            // 
            // btnEnviarCocina
            // 
            btnEnviarCocina.Font = new Font("Segoe UI", 10F);
            btnEnviarCocina.Location = new Point(23, 248);
            btnEnviarCocina.Name = "btnEnviarCocina";
            btnEnviarCocina.Size = new Size(342, 34);
            btnEnviarCocina.TabIndex = 2;
            btnEnviarCocina.Text = "Enviar a cocina";
            btnEnviarCocina.UseVisualStyleBackColor = true;
            btnEnviarCocina.Click += btnEnviarCocina_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(251, 550);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(114, 34);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(6, 19);
            label7.Name = "label7";
            label7.Size = new Size(56, 19);
            label7.TabIndex = 15;
            label7.Text = "Reserva";
            // 
            // txtReserva
            // 
            txtReserva.Location = new Point(132, 15);
            txtReserva.Name = "txtReserva";
            txtReserva.ReadOnly = true;
            txtReserva.Size = new Size(133, 23);
            txtReserva.TabIndex = 14;
            txtReserva.TextAlign = HorizontalAlignment.Center;
            // 
            // txtMesa
            // 
            txtMesa.Font = new Font("Segoe UI", 10F);
            txtMesa.Location = new Point(132, 75);
            txtMesa.Name = "txtMesa";
            txtMesa.ReadOnly = true;
            txtMesa.Size = new Size(192, 25);
            txtMesa.TabIndex = 13;
            txtMesa.TextAlign = HorizontalAlignment.Center;
            // 
            // txtTotal
            // 
            txtTotal.Font = new Font("Segoe UI", 10F);
            txtTotal.Location = new Point(132, 106);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(192, 25);
            txtTotal.TabIndex = 12;
            txtTotal.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCliente
            // 
            txtCliente.Font = new Font("Segoe UI", 10F);
            txtCliente.Location = new Point(132, 44);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(192, 25);
            txtCliente.TabIndex = 11;
            txtCliente.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(6, 109);
            label6.Name = "label6";
            label6.Size = new Size(38, 19);
            label6.TabIndex = 10;
            label6.Text = "Total";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(6, 78);
            label5.Name = "label5";
            label5.Size = new Size(42, 19);
            label5.TabIndex = 9;
            label5.Text = "Mesa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(6, 47);
            label4.Name = "label4";
            label4.Size = new Size(51, 19);
            label4.TabIndex = 8;
            label4.Text = "Cliente";
            // 
            // dgvPedidos
            // 
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.AllowUserToDeleteRows = false;
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Location = new Point(6, 22);
            dgvPedidos.MultiSelect = false;
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.Size = new Size(520, 231);
            dgvPedidos.TabIndex = 16;
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
            // 
            // btnEnviarCobranza
            // 
            btnEnviarCobranza.Font = new Font("Segoe UI", 10F);
            btnEnviarCobranza.Location = new Point(23, 348);
            btnEnviarCobranza.Name = "btnEnviarCobranza";
            btnEnviarCobranza.Size = new Size(342, 34);
            btnEnviarCobranza.TabIndex = 17;
            btnEnviarCobranza.Text = "Enviar a cobranza";
            btnEnviarCobranza.UseVisualStyleBackColor = true;
            btnEnviarCobranza.Click += btnEnviarCobranza_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtReserva);
            groupBox1.Controls.Add(txtCliente);
            groupBox1.Controls.Add(txtMesa);
            groupBox1.Controls.Add(txtTotal);
            groupBox1.Location = new Point(23, 29);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(342, 153);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Informacion Pedido";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvPedidos);
            groupBox2.Location = new Point(385, 29);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(532, 265);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "Pedidos";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvDetallePedido);
            groupBox3.Location = new Point(385, 300);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(532, 272);
            groupBox3.TabIndex = 20;
            groupBox3.TabStop = false;
            groupBox3.Text = "Platos del pedido";
            // 
            // btnEliminarPlato
            // 
            btnEliminarPlato.Font = new Font("Segoe UI", 10F);
            btnEliminarPlato.Location = new Point(251, 388);
            btnEliminarPlato.Name = "btnEliminarPlato";
            btnEliminarPlato.Size = new Size(114, 34);
            btnEliminarPlato.TabIndex = 22;
            btnEliminarPlato.Text = "Eliminar Plato";
            btnEliminarPlato.UseVisualStyleBackColor = true;
            btnEliminarPlato.Click += btnEliminarPlato_Click;
            // 
            // btnRecargar
            // 
            btnRecargar.Font = new Font("Segoe UI", 10F);
            btnRecargar.Location = new Point(23, 197);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(342, 34);
            btnRecargar.TabIndex = 23;
            btnRecargar.Text = "Recargar pedidos";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(109, 396);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(100, 23);
            txtCantidad.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 399);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 25;
            label1.Text = "Cantidad";
            // 
            // btnEntregado
            // 
            btnEntregado.Font = new Font("Segoe UI", 10F);
            btnEntregado.Location = new Point(23, 300);
            btnEntregado.Name = "btnEntregado";
            btnEntregado.Size = new Size(342, 34);
            btnEntregado.TabIndex = 26;
            btnEntregado.Text = "Marcar Entregado";
            btnEntregado.UseVisualStyleBackColor = true;
            btnEntregado.Click += btnEntregado_Click;
            // 
            // frmPedidos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 604);
            Controls.Add(btnEntregado);
            Controls.Add(label1);
            Controls.Add(txtCantidad);
            Controls.Add(btnRecargar);
            Controls.Add(btnEliminarPlato);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnEnviarCobranza);
            Controls.Add(btnSalir);
            Controls.Add(btnEnviarCocina);
            Controls.Add(btnCancelarPedido);
            Name = "frmPedidos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pedidos";
            Load += frmPedidos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDetallePedido;
        private Button btnCancelarPedido;
        private Button btnEnviarCocina;
        private Button btnSalir;
        private Label label7;
        private TextBox txtReserva;
        private TextBox txtMesa;
        private TextBox txtTotal;
        private TextBox txtCliente;
        private Label label6;
        private Label label5;
        private Label label4;
        private DataGridView dgvPedidos;
        private Button btnEnviarCobranza;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button btnEliminarPlato;
        private Button btnRecargar;
        private TextBox txtCantidad;
        private Label label1;
        private Button btnEntregado;
    }
}