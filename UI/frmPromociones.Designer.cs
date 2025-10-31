namespace UI
{
    partial class frmPromociones
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
            btnGuardar = new Button();
            txtNombrePromocion = new TextBox();
            label1 = new Label();
            btnSalir = new Button();
            label2 = new Label();
            label5 = new Label();
            cmbEstadoPromocion = new ComboBox();
            cmbTipoDescuento = new ComboBox();
            txtValor = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtMontoMinimo = new TextBox();
            groupBox1 = new GroupBox();
            btnLimpiar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvPromociones = new DataGridView();
            groupBox3 = new GroupBox();
            chkTarjetaCredito = new CheckBox();
            chkTarjetaDebito = new CheckBox();
            chkEfectivo = new CheckBox();
            chkMercadoPago = new CheckBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPromociones).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(6, 286);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(99, 34);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtNombrePromocion
            // 
            txtNombrePromocion.Location = new Point(6, 48);
            txtNombrePromocion.Name = "txtNombrePromocion";
            txtNombrePromocion.Size = new Size(192, 23);
            txtNombrePromocion.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(321, 349);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(99, 34);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 83);
            label2.Name = "label2";
            label2.Size = new Size(109, 15);
            label2.TabIndex = 5;
            label2.Text = "Tipo de promocion";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(216, 188);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 14;
            label5.Text = "Estado";
            // 
            // cmbEstadoPromocion
            // 
            cmbEstadoPromocion.FormattingEnabled = true;
            cmbEstadoPromocion.Location = new Point(216, 206);
            cmbEstadoPromocion.Name = "cmbEstadoPromocion";
            cmbEstadoPromocion.Size = new Size(121, 23);
            cmbEstadoPromocion.TabIndex = 15;
            // 
            // cmbTipoDescuento
            // 
            cmbTipoDescuento.FormattingEnabled = true;
            cmbTipoDescuento.Location = new Point(6, 103);
            cmbTipoDescuento.Name = "cmbTipoDescuento";
            cmbTipoDescuento.Size = new Size(192, 23);
            cmbTipoDescuento.TabIndex = 16;
            // 
            // txtValor
            // 
            txtValor.Location = new Point(216, 103);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(100, 23);
            txtValor.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(216, 83);
            label6.Name = "label6";
            label6.Size = new Size(68, 15);
            label6.TabIndex = 18;
            label6.Text = "Valor(% - $)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(216, 141);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 20;
            label7.Text = "Monto Minimo";
            // 
            // txtMontoMinimo
            // 
            txtMontoMinimo.Location = new Point(216, 159);
            txtMontoMinimo.Name = "txtMontoMinimo";
            txtMontoMinimo.Size = new Size(200, 23);
            txtMontoMinimo.TabIndex = 19;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(btnSalir);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnGuardar);
            groupBox1.Controls.Add(txtMontoMinimo);
            groupBox1.Controls.Add(txtNombrePromocion);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtValor);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmbTipoDescuento);
            groupBox1.Controls.Add(cmbEstadoPromocion);
            groupBox1.Controls.Add(label5);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(427, 389);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "ABM Promociones";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(321, 286);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(99, 34);
            btnLimpiar.TabIndex = 24;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(111, 286);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(99, 34);
            btnModificar.TabIndex = 23;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(216, 286);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(99, 34);
            btnEliminar.TabIndex = 22;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvPromociones
            // 
            dgvPromociones.AllowUserToAddRows = false;
            dgvPromociones.AllowUserToDeleteRows = false;
            dgvPromociones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromociones.Location = new Point(6, 22);
            dgvPromociones.Name = "dgvPromociones";
            dgvPromociones.ReadOnly = true;
            dgvPromociones.Size = new Size(843, 361);
            dgvPromociones.TabIndex = 22;
            dgvPromociones.CellContentClick += dgvPromociones_CellContentClick;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvPromociones);
            groupBox3.Location = new Point(445, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(855, 389);
            groupBox3.TabIndex = 23;
            groupBox3.TabStop = false;
            groupBox3.Text = "Promociones";
            // 
            // chkTarjetaCredito
            // 
            chkTarjetaCredito.AutoSize = true;
            chkTarjetaCredito.Location = new Point(15, 50);
            chkTarjetaCredito.Name = "chkTarjetaCredito";
            chkTarjetaCredito.Size = new Size(103, 19);
            chkTarjetaCredito.TabIndex = 7;
            chkTarjetaCredito.Text = "Tarjeta Credito";
            chkTarjetaCredito.UseVisualStyleBackColor = true;
            // 
            // chkTarjetaDebito
            // 
            chkTarjetaDebito.AutoSize = true;
            chkTarjetaDebito.Location = new Point(15, 75);
            chkTarjetaDebito.Name = "chkTarjetaDebito";
            chkTarjetaDebito.Size = new Size(99, 19);
            chkTarjetaDebito.TabIndex = 8;
            chkTarjetaDebito.Text = "Tarjeta Debito";
            chkTarjetaDebito.UseVisualStyleBackColor = true;
            // 
            // chkEfectivo
            // 
            chkEfectivo.AutoSize = true;
            chkEfectivo.Location = new Point(15, 100);
            chkEfectivo.Name = "chkEfectivo";
            chkEfectivo.Size = new Size(68, 19);
            chkEfectivo.TabIndex = 9;
            chkEfectivo.Text = "Efectivo";
            chkEfectivo.UseVisualStyleBackColor = true;
            // 
            // chkMercadoPago
            // 
            chkMercadoPago.AutoSize = true;
            chkMercadoPago.Location = new Point(15, 25);
            chkMercadoPago.Name = "chkMercadoPago";
            chkMercadoPago.Size = new Size(103, 19);
            chkMercadoPago.TabIndex = 6;
            chkMercadoPago.Text = "Mercado Pago";
            chkMercadoPago.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkMercadoPago);
            groupBox2.Controls.Add(chkEfectivo);
            groupBox2.Controls.Add(chkTarjetaDebito);
            groupBox2.Controls.Add(chkTarjetaCredito);
            groupBox2.Location = new Point(20, 141);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(123, 129);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tipo de Pago";
            // 
            // frmPromociones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1312, 474);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Name = "frmPromociones";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Promociones";
            Load += frmPromociones_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPromociones).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnGuardar;
        private TextBox txtNombrePromocion;
        private Label label1;
        private Button btnSalir;
        private Label label2;
        private Label label5;
        private ComboBox cmbEstadoPromocion;
        private ComboBox cmbTipoDescuento;
        private TextBox txtValor;
        private Label label6;
        private Label label7;
        private TextBox txtMontoMinimo;
        private GroupBox groupBox1;
        private DataGridView dgvPromociones;
        private GroupBox groupBox3;
        private Button btnLimpiar;
        private Button btnModificar;
        private Button btnEliminar;
        private GroupBox groupBox2;
        private CheckBox chkMercadoPago;
        private CheckBox chkEfectivo;
        private CheckBox chkTarjetaDebito;
        private CheckBox chkTarjetaCredito;
    }
}