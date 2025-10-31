namespace UI
{
    partial class frmCobrarPedido
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
            btnRecargar = new Button();
            groupBox3 = new GroupBox();
            dgvDetallePedido = new DataGridView();
            groupBox2 = new GroupBox();
            dgvPedidos = new DataGridView();
            groupBox1 = new GroupBox();
            label3 = new Label();
            txtTotalCobrar = new TextBox();
            label2 = new Label();
            groupBox4 = new GroupBox();
            rdoEfectivo = new RadioButton();
            rdoTarjetaDebito = new RadioButton();
            rdoTarjetaCredito = new RadioButton();
            rdoMercadoPago = new RadioButton();
            label1 = new Label();
            cmbPromociones = new ComboBox();
            label7 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtReserva = new TextBox();
            txtCliente = new TextBox();
            txtMesa = new TextBox();
            txtTotal = new TextBox();
            btnCobrar = new Button();
            btnSalir = new Button();
            btnLimpiar = new Button();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // btnRecargar
            // 
            btnRecargar.Font = new Font("Segoe UI", 10F);
            btnRecargar.Location = new Point(12, 401);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(342, 34);
            btnRecargar.TabIndex = 35;
            btnRecargar.Text = "Recargar Reservas";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvDetallePedido);
            groupBox3.Location = new Point(374, 283);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(532, 272);
            groupBox3.TabIndex = 33;
            groupBox3.TabStop = false;
            groupBox3.Text = "Detalle Reserva";
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
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvPedidos);
            groupBox2.Location = new Point(374, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(532, 265);
            groupBox2.TabIndex = 32;
            groupBox2.TabStop = false;
            groupBox2.Text = "Reservas";
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
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(520, 231);
            dgvPedidos.TabIndex = 16;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtTotalCobrar);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbPromociones);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtReserva);
            groupBox1.Controls.Add(txtCliente);
            groupBox1.Controls.Add(txtMesa);
            groupBox1.Controls.Add(txtTotal);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(342, 359);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "Informacion Pedido";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(6, 323);
            label3.Name = "label3";
            label3.Size = new Size(92, 19);
            label3.TabIndex = 38;
            label3.Text = "Total a cobrar";
            // 
            // txtTotalCobrar
            // 
            txtTotalCobrar.Font = new Font("Segoe UI", 10F);
            txtTotalCobrar.Location = new Point(132, 320);
            txtTotalCobrar.Name = "txtTotalCobrar";
            txtTotalCobrar.ReadOnly = true;
            txtTotalCobrar.Size = new Size(192, 25);
            txtTotalCobrar.TabIndex = 39;
            txtTotalCobrar.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 7F);
            label2.Location = new Point(10, 296);
            label2.Name = "label2";
            label2.Size = new Size(79, 12);
            label2.TabIndex = 37;
            label2.Text = "Segun tipo Pago";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(rdoEfectivo);
            groupBox4.Controls.Add(rdoTarjetaDebito);
            groupBox4.Controls.Add(rdoTarjetaCredito);
            groupBox4.Controls.Add(rdoMercadoPago);
            groupBox4.Location = new Point(6, 145);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(123, 129);
            groupBox4.TabIndex = 36;
            groupBox4.TabStop = false;
            groupBox4.Text = "Tipo de Pago";
            // 
            // rdoEfectivo
            // 
            rdoEfectivo.AutoSize = true;
            rdoEfectivo.Location = new Point(9, 100);
            rdoEfectivo.Name = "rdoEfectivo";
            rdoEfectivo.Size = new Size(67, 19);
            rdoEfectivo.TabIndex = 3;
            rdoEfectivo.TabStop = true;
            rdoEfectivo.Text = "Efectivo";
            rdoEfectivo.UseVisualStyleBackColor = true;
            rdoEfectivo.CheckedChanged += rdoEfectivo_CheckedChanged;
            // 
            // rdoTarjetaDebito
            // 
            rdoTarjetaDebito.AutoSize = true;
            rdoTarjetaDebito.Location = new Point(9, 75);
            rdoTarjetaDebito.Name = "rdoTarjetaDebito";
            rdoTarjetaDebito.Size = new Size(98, 19);
            rdoTarjetaDebito.TabIndex = 2;
            rdoTarjetaDebito.TabStop = true;
            rdoTarjetaDebito.Text = "Tarjeta Debito";
            rdoTarjetaDebito.UseVisualStyleBackColor = true;
            rdoTarjetaDebito.CheckedChanged += rdoTarjetaDebito_CheckedChanged;
            // 
            // rdoTarjetaCredito
            // 
            rdoTarjetaCredito.AutoSize = true;
            rdoTarjetaCredito.Location = new Point(9, 50);
            rdoTarjetaCredito.Name = "rdoTarjetaCredito";
            rdoTarjetaCredito.Size = new Size(102, 19);
            rdoTarjetaCredito.TabIndex = 1;
            rdoTarjetaCredito.TabStop = true;
            rdoTarjetaCredito.Text = "Tarjeta Credito";
            rdoTarjetaCredito.UseVisualStyleBackColor = true;
            rdoTarjetaCredito.CheckedChanged += rdoTarjetaCredito_CheckedChanged;
            // 
            // rdoMercadoPago
            // 
            rdoMercadoPago.AutoSize = true;
            rdoMercadoPago.Location = new Point(9, 25);
            rdoMercadoPago.Name = "rdoMercadoPago";
            rdoMercadoPago.Size = new Size(102, 19);
            rdoMercadoPago.TabIndex = 0;
            rdoMercadoPago.TabStop = true;
            rdoMercadoPago.Text = "Mercado Pago";
            rdoMercadoPago.UseVisualStyleBackColor = true;
            rdoMercadoPago.CheckedChanged += rdoMercadoPago_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(6, 277);
            label1.Name = "label1";
            label1.Size = new Size(75, 19);
            label1.TabIndex = 17;
            label1.Text = "Promocion";
            // 
            // cmbPromociones
            // 
            cmbPromociones.FormattingEnabled = true;
            cmbPromociones.Location = new Point(132, 276);
            cmbPromociones.Name = "cmbPromociones";
            cmbPromociones.Size = new Size(192, 23);
            cmbPromociones.TabIndex = 16;
            cmbPromociones.SelectedIndexChanged += cmbPromociones_SelectedIndexChanged;
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
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(6, 109);
            label6.Name = "label6";
            label6.Size = new Size(83, 19);
            label6.TabIndex = 10;
            label6.Text = "Total Pedido";
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
            // btnCobrar
            // 
            btnCobrar.Font = new Font("Segoe UI", 10F);
            btnCobrar.Location = new Point(12, 441);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(342, 34);
            btnCobrar.TabIndex = 30;
            btnCobrar.Text = "Cobrar";
            btnCobrar.UseVisualStyleBackColor = true;
            btnCobrar.Click += btnCobrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(240, 521);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(114, 34);
            btnSalir.TabIndex = 29;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.Location = new Point(12, 481);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(342, 34);
            btnLimpiar.TabIndex = 27;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // frmCobrarPedido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 571);
            Controls.Add(btnRecargar);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnCobrar);
            Controls.Add(btnSalir);
            Controls.Add(btnLimpiar);
            Name = "frmCobrarPedido";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cobrar Pedido";
            Load += frmCobrarPedido_Load;
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnRecargar;
        private GroupBox groupBox3;
        private DataGridView dgvDetallePedido;
        private GroupBox groupBox2;
        private DataGridView dgvPedidos;
        private GroupBox groupBox1;
        private Label label7;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtReserva;
        private TextBox txtCliente;
        private TextBox txtMesa;
        private TextBox txtTotal;
        private Button btnCobrar;
        private Button btnSalir;
        private Button btnLimpiar;
        private Label label1;
        private ComboBox cmbPromociones;
        private GroupBox groupBox4;
        private Label label2;
        private RadioButton rdoEfectivo;
        private RadioButton rdoTarjetaDebito;
        private RadioButton rdoTarjetaCredito;
        private RadioButton rdoMercadoPago;
        private Label label3;
        private TextBox txtTotalCobrar;
    }
}