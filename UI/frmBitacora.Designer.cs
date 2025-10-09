namespace UI
{
    partial class frmBitacora
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
            groupBox1 = new GroupBox();
            dgvBitacora = new DataGridView();
            btnActualizar = new Button();
            btnSalir = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvBitacora);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(477, 366);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bitacora";
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Location = new Point(6, 22);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
            dgvBitacora.Size = new Size(465, 338);
            dgvBitacora.TabIndex = 0;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(495, 146);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(94, 38);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(495, 190);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 38);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // frmBitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 389);
            Controls.Add(btnSalir);
            Controls.Add(btnActualizar);
            Controls.Add(groupBox1);
            Name = "frmBitacora";
            Text = "frmBitacora";
            Load += frmBitacora_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dgvBitacora;
        private Button btnActualizar;
        private Button btnSalir;
    }
}