namespace UI
{
    partial class frmCocina
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
            dgvTerminados = new DataGridView();
            groupBox2 = new GroupBox();
            dgvCocina = new DataGridView();
            btnTerminado = new Button();
            btnSalir = new Button();
            btnPreparacion = new Button();
            rdoPreparacion = new RadioButton();
            rdoPendientes = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTerminados).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCocina).BeginInit();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // btnRecargar
            // 
            btnRecargar.Font = new Font("Segoe UI", 10F);
            btnRecargar.Location = new Point(6, 124);
            btnRecargar.Name = "btnRecargar";
            btnRecargar.Size = new Size(176, 34);
            btnRecargar.TabIndex = 33;
            btnRecargar.Text = "Recargar pedidos";
            btnRecargar.UseVisualStyleBackColor = true;
            btnRecargar.Click += btnRecargar_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvTerminados);
            groupBox3.Location = new Point(365, 313);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(532, 272);
            groupBox3.TabIndex = 31;
            groupBox3.TabStop = false;
            groupBox3.Text = "Platos Terminados";
            // 
            // dgvTerminados
            // 
            dgvTerminados.AllowUserToAddRows = false;
            dgvTerminados.AllowUserToDeleteRows = false;
            dgvTerminados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTerminados.Location = new Point(6, 22);
            dgvTerminados.MultiSelect = false;
            dgvTerminados.Name = "dgvTerminados";
            dgvTerminados.ReadOnly = true;
            dgvTerminados.Size = new Size(520, 244);
            dgvTerminados.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvCocina);
            groupBox2.Location = new Point(365, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(532, 265);
            groupBox2.TabIndex = 30;
            groupBox2.TabStop = false;
            groupBox2.Text = "Platos en Preparacion";
            // 
            // dgvCocina
            // 
            dgvCocina.AllowUserToAddRows = false;
            dgvCocina.AllowUserToDeleteRows = false;
            dgvCocina.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCocina.Location = new Point(6, 22);
            dgvCocina.MultiSelect = false;
            dgvCocina.Name = "dgvCocina";
            dgvCocina.ReadOnly = true;
            dgvCocina.Size = new Size(520, 237);
            dgvCocina.TabIndex = 16;
            dgvCocina.SelectionChanged += dgvCocina_SelectionChanged;
            // 
            // btnTerminado
            // 
            btnTerminado.Font = new Font("Segoe UI", 10F);
            btnTerminado.Location = new Point(6, 76);
            btnTerminado.Name = "btnTerminado";
            btnTerminado.Size = new Size(176, 34);
            btnTerminado.TabIndex = 29;
            btnTerminado.Text = "Plato terminado";
            btnTerminado.UseVisualStyleBackColor = true;
            btnTerminado.Click += btnTerminado_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(195, 124);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(114, 34);
            btnSalir.TabIndex = 28;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnPreparacion
            // 
            btnPreparacion.Font = new Font("Segoe UI", 10F);
            btnPreparacion.Location = new Point(6, 30);
            btnPreparacion.Name = "btnPreparacion";
            btnPreparacion.Size = new Size(176, 34);
            btnPreparacion.TabIndex = 27;
            btnPreparacion.Text = "Empezar a preparar";
            btnPreparacion.UseVisualStyleBackColor = true;
            btnPreparacion.Click += btnPreparacion_Click;
            // 
            // rdoPreparacion
            // 
            rdoPreparacion.AutoSize = true;
            rdoPreparacion.Location = new Point(20, 20);
            rdoPreparacion.Name = "rdoPreparacion";
            rdoPreparacion.Size = new Size(88, 19);
            rdoPreparacion.TabIndex = 34;
            rdoPreparacion.TabStop = true;
            rdoPreparacion.Text = "Preparacion";
            rdoPreparacion.UseVisualStyleBackColor = true;
            rdoPreparacion.CheckedChanged += rdoPreparacion_CheckedChanged;
            // 
            // rdoPendientes
            // 
            rdoPendientes.AutoSize = true;
            rdoPendientes.Location = new Point(20, 45);
            rdoPendientes.Name = "rdoPendientes";
            rdoPendientes.Size = new Size(83, 19);
            rdoPendientes.TabIndex = 35;
            rdoPendientes.TabStop = true;
            rdoPendientes.Text = "Pendientes";
            rdoPendientes.UseVisualStyleBackColor = true;
            rdoPendientes.CheckedChanged += rdoPendientes_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(btnRecargar);
            groupBox1.Controls.Add(btnPreparacion);
            groupBox1.Controls.Add(btnSalir);
            groupBox1.Controls.Add(btnTerminado);
            groupBox1.Location = new Point(27, 225);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(315, 172);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "Administrar Platos";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(rdoPreparacion);
            groupBox4.Controls.Add(rdoPendientes);
            groupBox4.Location = new Point(197, 31);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(112, 87);
            groupBox4.TabIndex = 36;
            groupBox4.TabStop = false;
            groupBox4.Text = "Filtro";
            // 
            // frmCocina
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913, 609);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Name = "frmCocina";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cocina";
            Load += frmCocina_Load;
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTerminados).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCocina).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox txtCantidad;
        private Button btnRecargar;
        private Button btnEliminarPlato;
        private GroupBox groupBox3;
        private DataGridView dgvTerminados;
        private GroupBox groupBox2;
        private DataGridView dgvCocina;
        private Button btnTerminado;
        private Button btnSalir;
        private Button btnPreparacion;
        private Button btnCancelarPedido;
        private RadioButton rdoPreparacion;
        private RadioButton rdoPendientes;
        private GroupBox groupBox1;
        private GroupBox groupBox4;
    }
}