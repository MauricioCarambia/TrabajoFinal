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
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            SuspendLayout();
            // 
            // dgvDetallePedido
            // 
            dgvDetallePedido.AllowUserToAddRows = false;
            dgvDetallePedido.AllowUserToDeleteRows = false;
            dgvDetallePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallePedido.Location = new Point(550, 288);
            dgvDetallePedido.MultiSelect = false;
            dgvDetallePedido.Name = "dgvDetallePedido";
            dgvDetallePedido.ReadOnly = true;
            dgvDetallePedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetallePedido.Size = new Size(510, 286);
            dgvDetallePedido.TabIndex = 0;
            // 
            // btnCancelarPedido
            // 
            btnCancelarPedido.Location = new Point(262, 313);
            btnCancelarPedido.Name = "btnCancelarPedido";
            btnCancelarPedido.Size = new Size(114, 34);
            btnCancelarPedido.TabIndex = 1;
            btnCancelarPedido.Text = "Cancelar Pedido";
            btnCancelarPedido.UseVisualStyleBackColor = true;
            // 
            // btnEnviarCocina
            // 
            btnEnviarCocina.Location = new Point(142, 313);
            btnEnviarCocina.Name = "btnEnviarCocina";
            btnEnviarCocina.Size = new Size(114, 34);
            btnEnviarCocina.TabIndex = 2;
            btnEnviarCocina.Text = "Enviar a cocina";
            btnEnviarCocina.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(262, 365);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(114, 34);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(52, 155);
            label7.Name = "label7";
            label7.Size = new Size(56, 19);
            label7.TabIndex = 15;
            label7.Text = "Reserva";
            // 
            // txtReserva
            // 
            txtReserva.Location = new Point(178, 151);
            txtReserva.Name = "txtReserva";
            txtReserva.ReadOnly = true;
            txtReserva.Size = new Size(100, 23);
            txtReserva.TabIndex = 14;
            // 
            // txtMesa
            // 
            txtMesa.Font = new Font("Segoe UI", 10F);
            txtMesa.Location = new Point(178, 211);
            txtMesa.Name = "txtMesa";
            txtMesa.ReadOnly = true;
            txtMesa.Size = new Size(192, 25);
            txtMesa.TabIndex = 13;
            // 
            // txtTotal
            // 
            txtTotal.Font = new Font("Segoe UI", 10F);
            txtTotal.Location = new Point(178, 242);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(192, 25);
            txtTotal.TabIndex = 12;
            // 
            // txtCliente
            // 
            txtCliente.Font = new Font("Segoe UI", 10F);
            txtCliente.Location = new Point(178, 180);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(192, 25);
            txtCliente.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(52, 245);
            label6.Name = "label6";
            label6.Size = new Size(38, 19);
            label6.TabIndex = 10;
            label6.Text = "Total";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(52, 214);
            label5.Name = "label5";
            label5.Size = new Size(42, 19);
            label5.TabIndex = 9;
            label5.Text = "Mesa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(52, 183);
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
            dgvPedidos.Location = new Point(426, 29);
            dgvPedidos.MultiSelect = false;
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(506, 231);
            dgvPedidos.TabIndex = 16;
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
            // 
            // btnEnviarCobranza
            // 
            btnEnviarCobranza.Location = new Point(142, 365);
            btnEnviarCobranza.Name = "btnEnviarCobranza";
            btnEnviarCobranza.Size = new Size(114, 34);
            btnEnviarCobranza.TabIndex = 17;
            btnEnviarCobranza.Text = "Enviar a cobranza";
            btnEnviarCobranza.UseVisualStyleBackColor = true;
            // 
            // frmPedidos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 652);
            Controls.Add(btnEnviarCobranza);
            Controls.Add(dgvPedidos);
            Controls.Add(label7);
            Controls.Add(txtReserva);
            Controls.Add(txtMesa);
            Controls.Add(txtTotal);
            Controls.Add(txtCliente);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(btnSalir);
            Controls.Add(btnEnviarCocina);
            Controls.Add(btnCancelarPedido);
            Controls.Add(dgvDetallePedido);
            Name = "frmPedidos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pedidos";
            Load += frmPedidos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
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
    }
}