namespace UI
{
    partial class frmProveedores
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
            txtDomicilioProveedor = new TextBox();
            label6 = new Label();
            txtEmailProveedor = new TextBox();
            label5 = new Label();
            txtTelefonoProveedor = new TextBox();
            label1 = new Label();
            btnLimpiar = new Button();
            txtIdProveedor = new TextBox();
            btnSalir = new Button();
            label2 = new Label();
            btnGuardar = new Button();
            txtCUILProveedor = new TextBox();
            btnEliminar = new Button();
            txtNombreProveedor = new TextBox();
            btnModificar = new Button();
            label4 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            dgvProveedores = new DataGridView();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDomicilioProveedor);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(txtEmailProveedor);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtTelefonoProveedor);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnLimpiar);
            groupBox2.Controls.Add(txtIdProveedor);
            groupBox2.Controls.Add(btnSalir);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnGuardar);
            groupBox2.Controls.Add(txtCUILProveedor);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(txtNombreProveedor);
            groupBox2.Controls.Add(btnModificar);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(26, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(363, 451);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "ABM Proveedor";
            // 
            // txtDomicilioProveedor
            // 
            txtDomicilioProveedor.Font = new Font("Segoe UI", 10F);
            txtDomicilioProveedor.Location = new Point(121, 158);
            txtDomicilioProveedor.Name = "txtDomicilioProveedor";
            txtDomicilioProveedor.Size = new Size(150, 25);
            txtDomicilioProveedor.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(33, 161);
            label6.Name = "label6";
            label6.Size = new Size(65, 19);
            label6.TabIndex = 17;
            label6.Text = "Domicilio";
            // 
            // txtEmailProveedor
            // 
            txtEmailProveedor.Font = new Font("Segoe UI", 10F);
            txtEmailProveedor.Location = new Point(121, 201);
            txtEmailProveedor.Name = "txtEmailProveedor";
            txtEmailProveedor.Size = new Size(150, 25);
            txtEmailProveedor.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(33, 204);
            label5.Name = "label5";
            label5.Size = new Size(41, 19);
            label5.TabIndex = 15;
            label5.Text = "Email";
            // 
            // txtTelefonoProveedor
            // 
            txtTelefonoProveedor.Font = new Font("Segoe UI", 10F);
            txtTelefonoProveedor.Location = new Point(121, 244);
            txtTelefonoProveedor.Name = "txtTelefonoProveedor";
            txtTelefonoProveedor.Size = new Size(150, 25);
            txtTelefonoProveedor.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(33, 247);
            label1.Name = "label1";
            label1.Size = new Size(60, 19);
            label1.TabIndex = 13;
            label1.Text = "Telefono";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.Location = new Point(13, 381);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(102, 50);
            btnLimpiar.TabIndex = 8;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtIdProveedor
            // 
            txtIdProveedor.Font = new Font("Segoe UI", 10F);
            txtIdProveedor.Location = new Point(121, 39);
            txtIdProveedor.Name = "txtIdProveedor";
            txtIdProveedor.ReadOnly = true;
            txtIdProveedor.Size = new Size(150, 25);
            txtIdProveedor.TabIndex = 11;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Location = new Point(229, 393);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(102, 38);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(33, 42);
            label2.Name = "label2";
            label2.Size = new Size(23, 19);
            label2.TabIndex = 10;
            label2.Text = "ID";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.Location = new Point(13, 316);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 38);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtCUILProveedor
            // 
            txtCUILProveedor.Font = new Font("Segoe UI", 10F);
            txtCUILProveedor.Location = new Point(121, 118);
            txtCUILProveedor.Name = "txtCUILProveedor";
            txtCUILProveedor.Size = new Size(150, 25);
            txtCUILProveedor.TabIndex = 1;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.Location = new Point(229, 316);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 38);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtNombreProveedor
            // 
            txtNombreProveedor.Font = new Font("Segoe UI", 10F);
            txtNombreProveedor.Location = new Point(121, 78);
            txtNombreProveedor.Name = "txtNombreProveedor";
            txtNombreProveedor.Size = new Size(150, 25);
            txtNombreProveedor.TabIndex = 0;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 10F);
            btnModificar.Location = new Point(121, 316);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 38);
            btnModificar.TabIndex = 6;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(33, 81);
            label4.Name = "label4";
            label4.Size = new Size(59, 19);
            label4.TabIndex = 7;
            label4.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(33, 121);
            label3.Name = "label3";
            label3.Size = new Size(39, 19);
            label3.TabIndex = 6;
            label3.Text = "CUIL";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvProveedores);
            groupBox1.Location = new Point(430, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(617, 451);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Proveedores";
            // 
            // dgvProveedores
            // 
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.AllowUserToDeleteRows = false;
            dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedores.Location = new Point(16, 22);
            dgvProveedores.MultiSelect = false;
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.ReadOnly = true;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.Size = new Size(595, 423);
            dgvProveedores.TabIndex = 10;
            dgvProveedores.CellContentClick += dgvProveedores_CellContentClick;
            dgvProveedores.SelectionChanged += dgvProveedores_SelectionChanged;
            // 
            // frmProveedores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1069, 505);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmProveedores";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmProveedores";
            Load += frmProveedores_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private TextBox txtDomicilioProveedor;
        private Label label6;
        private TextBox txtEmailProveedor;
        private Label label5;
        private TextBox txtTelefonoProveedor;
        private Label label1;
        private Button btnLimpiar;
        private TextBox txtIdProveedor;
        private Button btnSalir;
        private Label label2;
        private Button btnGuardar;
        private TextBox txtCUILProveedor;
        private Button btnEliminar;
        private TextBox txtNombreProveedor;
        private Button btnModificar;
        private Label label4;
        private Label label3;
        private GroupBox groupBox1;
        private DataGridView dgvProveedores;
    }
}