namespace UI
{
    partial class frmReserva
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
            flpMesas = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            dtpFecha = new DateTimePicker();
            txtCantidadPer = new TextBox();
            groupBox2 = new GroupBox();
            label10 = new Label();
            txtIdCliente = new TextBox();
            btnRegistrar = new Button();
            btnBuscar = new Button();
            label5 = new Label();
            label4 = new Label();
            txtTelefono = new TextBox();
            txtNombre = new TextBox();
            txtDNI = new TextBox();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            btnReservar = new Button();
            groupBox3 = new GroupBox();
            label9 = new Label();
            txtMesa = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtPersonas = new TextBox();
            txtFecha = new TextBox();
            btnEliminar = new Button();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            dgvReservas = new DataGridView();
            groupBox4 = new GroupBox();
            label8 = new Label();
            txtNumeroReserva = new TextBox();
            groupBox5 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReservas).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // flpMesas
            // 
            flpMesas.AutoScroll = true;
            flpMesas.Location = new Point(6, 22);
            flpMesas.Name = "flpMesas";
            flpMesas.Size = new Size(412, 448);
            flpMesas.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flpMesas);
            groupBox1.Location = new Point(12, 123);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(426, 482);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mesas";
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI", 11F);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(173, 26);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(124, 27);
            dtpFecha.TabIndex = 2;
            // 
            // txtCantidadPer
            // 
            txtCantidadPer.Font = new Font("Segoe UI", 11F);
            txtCantidadPer.Location = new Point(183, 71);
            txtCantidadPer.Name = "txtCantidadPer";
            txtCantidadPer.Size = new Size(100, 27);
            txtCantidadPer.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(txtIdCliente);
            groupBox2.Controls.Add(btnRegistrar);
            groupBox2.Controls.Add(btnBuscar);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(txtTelefono);
            groupBox2.Controls.Add(txtNombre);
            groupBox2.Controls.Add(txtDNI);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(444, 361);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(345, 244);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos Cliente";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11F);
            label10.Location = new Point(6, 25);
            label10.Name = "label10";
            label10.Size = new Size(24, 20);
            label10.TabIndex = 9;
            label10.Text = "ID";
            // 
            // txtIdCliente
            // 
            txtIdCliente.Font = new Font("Segoe UI", 11F);
            txtIdCliente.Location = new Point(48, 22);
            txtIdCliente.Name = "txtIdCliente";
            txtIdCliente.ReadOnly = true;
            txtIdCliente.Size = new Size(147, 27);
            txtIdCliente.TabIndex = 8;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Font = new Font("Segoe UI", 11F);
            btnRegistrar.Location = new Point(6, 191);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(132, 45);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Registrar Cliente";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Font = new Font("Segoe UI", 11F);
            btnBuscar.Location = new Point(201, 65);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(95, 35);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(6, 155);
            label5.Name = "label5";
            label5.Size = new Size(67, 20);
            label5.TabIndex = 5;
            label5.Text = "Telefono";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(6, 72);
            label4.Name = "label4";
            label4.Size = new Size(35, 20);
            label4.TabIndex = 4;
            label4.Text = "DNI";
            // 
            // txtTelefono
            // 
            txtTelefono.Font = new Font("Segoe UI", 11F);
            txtTelefono.Location = new Point(149, 152);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.ReadOnly = true;
            txtTelefono.Size = new Size(147, 27);
            txtTelefono.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(149, 111);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(186, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtDNI
            // 
            txtDNI.Font = new Font("Segoe UI", 11F);
            txtDNI.Location = new Point(48, 69);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(147, 27);
            txtDNI.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(6, 114);
            label3.Name = "label3";
            label3.Size = new Size(132, 20);
            label3.TabIndex = 0;
            label3.Text = "Nombre y Apelido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(39, 74);
            label1.Name = "label1";
            label1.Size = new Size(130, 20);
            label1.TabIndex = 5;
            label1.Text = "Cantidad Personas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(39, 31);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 6;
            label2.Text = "Fecha";
            // 
            // btnReservar
            // 
            btnReservar.Font = new Font("Segoe UI", 11F);
            btnReservar.Location = new Point(31, 163);
            btnReservar.Name = "btnReservar";
            btnReservar.Size = new Size(95, 35);
            btnReservar.TabIndex = 7;
            btnReservar.Text = "Reservar";
            btnReservar.UseVisualStyleBackColor = true;
            btnReservar.Click += btnReservar_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(txtMesa);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(txtPersonas);
            groupBox3.Controls.Add(txtFecha);
            groupBox3.Controls.Add(btnReservar);
            groupBox3.Location = new Point(444, 26);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(345, 213);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Datos Reserva";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11F);
            label9.Location = new Point(24, 119);
            label9.Name = "label9";
            label9.Size = new Size(102, 20);
            label9.TabIndex = 15;
            label9.Text = "Numero Mesa";
            // 
            // txtMesa
            // 
            txtMesa.Font = new Font("Segoe UI", 11F);
            txtMesa.Location = new Point(167, 116);
            txtMesa.Name = "txtMesa";
            txtMesa.ReadOnly = true;
            txtMesa.Size = new Size(147, 27);
            txtMesa.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F);
            label6.Location = new Point(24, 74);
            label6.Name = "label6";
            label6.Size = new Size(130, 20);
            label6.TabIndex = 13;
            label6.Text = "Cantidad Personas";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F);
            label7.Location = new Point(24, 35);
            label7.Name = "label7";
            label7.Size = new Size(47, 20);
            label7.TabIndex = 12;
            label7.Text = "Fecha";
            // 
            // txtPersonas
            // 
            txtPersonas.Font = new Font("Segoe UI", 11F);
            txtPersonas.Location = new Point(167, 71);
            txtPersonas.Name = "txtPersonas";
            txtPersonas.ReadOnly = true;
            txtPersonas.Size = new Size(147, 27);
            txtPersonas.TabIndex = 11;
            // 
            // txtFecha
            // 
            txtFecha.Font = new Font("Segoe UI", 11F);
            txtFecha.Location = new Point(167, 32);
            txtFecha.Name = "txtFecha";
            txtFecha.ReadOnly = true;
            txtFecha.Size = new Size(147, 27);
            txtFecha.TabIndex = 9;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(201, 60);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 35);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(123, 26);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(46, 27);
            btnAnterior.TabIndex = 8;
            btnAnterior.Text = "<<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(303, 25);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(46, 28);
            btnSiguiente.TabIndex = 9;
            btnSiguiente.Text = ">>";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // dgvReservas
            // 
            dgvReservas.AllowUserToAddRows = false;
            dgvReservas.AllowUserToDeleteRows = false;
            dgvReservas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservas.Location = new Point(6, 22);
            dgvReservas.Name = "dgvReservas";
            dgvReservas.ReadOnly = true;
            dgvReservas.Size = new Size(537, 546);
            dgvReservas.TabIndex = 10;
            dgvReservas.SelectionChanged += dgvReservas_SelectionChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dgvReservas);
            groupBox4.Location = new Point(795, 25);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(549, 580);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Text = "Reservas";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11F);
            label8.Location = new Point(6, 30);
            label8.Name = "label8";
            label8.Size = new Size(118, 20);
            label8.TabIndex = 17;
            label8.Text = "Numero Reserva";
            // 
            // txtNumeroReserva
            // 
            txtNumeroReserva.Font = new Font("Segoe UI", 11F);
            txtNumeroReserva.Location = new Point(149, 27);
            txtNumeroReserva.Name = "txtNumeroReserva";
            txtNumeroReserva.ReadOnly = true;
            txtNumeroReserva.Size = new Size(147, 27);
            txtNumeroReserva.TabIndex = 16;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnEliminar);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(txtNumeroReserva);
            groupBox5.Location = new Point(444, 245);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(345, 110);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Eliminar Reserva";
            // 
            // frmReserva
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1356, 645);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(groupBox3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(txtCantidadPer);
            Controls.Add(dtpFecha);
            Controls.Add(groupBox1);
            Name = "frmReserva";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reserva";
            Load += frmReserva_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReservas).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpMesas;
        private GroupBox groupBox1;
        private DateTimePicker dtpFecha;
        private TextBox txtCantidadPer;
        private GroupBox groupBox2;
        private Button btnReservar;
        private Button btnBuscar;
        private Label label5;
        private Label label4;
        private TextBox txtTelefono;
        private TextBox txtNombre;
        private TextBox txtDNI;
        private Label label3;
        private Label label1;
        private Label label2;
        private GroupBox groupBox3;
        private Label label6;
        private Label label7;
        private TextBox txtPersonas;
        private TextBox txtFecha;
        private Button btnRegistrar;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label label9;
        private TextBox txtMesa;
        private DataGridView dgvReservas;
        private GroupBox groupBox4;
        private Button btnEliminar;
        private Label label10;
        private TextBox txtIdCliente;
        private Label label8;
        private TextBox txtNumeroReserva;
        private GroupBox groupBox5;
    }
}