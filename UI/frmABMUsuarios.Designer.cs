namespace UI
{
    partial class frmABMUsuarios
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
            btnEliminar = new Button();
            btnModificar = new Button();
            btnSalir = new Button();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtUsuario = new TextBox();
            txtContrasenia = new TextBox();
            dgvUsuarios = new DataGridView();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnLimpiar = new Button();
            txtID = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 11F);
            btnGuardar.Location = new Point(19, 248);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(102, 38);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI", 11F);
            btnEliminar.Location = new Point(235, 248);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 38);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Font = new Font("Segoe UI", 11F);
            btnModificar.Location = new Point(127, 248);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(102, 38);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 11F);
            btnSalir.Location = new Point(235, 313);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(102, 38);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(120, 41);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 4;
            label1.Text = "Crear Usuario";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(54, 183);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 6;
            label3.Text = "Contraseña";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(54, 139);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 7;
            label4.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(140, 135);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(150, 27);
            txtUsuario.TabIndex = 8;
            // 
            // txtContrasenia
            // 
            txtContrasenia.Font = new Font("Segoe UI", 11F);
            txtContrasenia.Location = new Point(140, 175);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.Size = new Size(150, 27);
            txtContrasenia.TabIndex = 9;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(16, 22);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(572, 370);
            dgvUsuarios.TabIndex = 10;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvUsuarios);
            groupBox1.Location = new Point(502, 54);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(594, 398);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Usuarios";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnLimpiar);
            groupBox2.Controls.Add(txtID);
            groupBox2.Controls.Add(btnSalir);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnGuardar);
            groupBox2.Controls.Add(txtContrasenia);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(txtUsuario);
            groupBox2.Controls.Add(btnModificar);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(56, 54);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(367, 392);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "ABM Usuario";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 11F);
            btnLimpiar.Location = new Point(19, 313);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(102, 50);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtID
            // 
            txtID.Font = new Font("Segoe UI", 11F);
            txtID.Location = new Point(140, 96);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(150, 27);
            txtID.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(54, 100);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 10;
            label2.Text = "ID";
            // 
            // frmABMUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 608);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmABMUsuarios";
            Text = "frmABMUsuarios";
            Load += frmABMUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnModificar;
        private Button btnSalir;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox txtUsuario;
        private TextBox txtContrasenia;
        private DataGridView dgvUsuarios;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox txtID;
        private Label label2;
        private Button btnLimpiar;
    }
}