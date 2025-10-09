namespace UI
{
    partial class frmBackup
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
            dgvBackUp = new DataGridView();
            btnBackUp = new Button();
            btnSalir = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBackUp).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvBackUp);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(458, 378);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "BackUp";
            // 
            // dgvBackUp
            // 
            dgvBackUp.AllowUserToAddRows = false;
            dgvBackUp.AllowUserToDeleteRows = false;
            dgvBackUp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBackUp.Location = new Point(6, 22);
            dgvBackUp.Name = "dgvBackUp";
            dgvBackUp.ReadOnly = true;
            dgvBackUp.Size = new Size(446, 350);
            dgvBackUp.TabIndex = 0;
            // 
            // btnBackUp
            // 
            btnBackUp.Location = new Point(476, 34);
            btnBackUp.Name = "btnBackUp";
            btnBackUp.Size = new Size(93, 38);
            btnBackUp.TabIndex = 1;
            btnBackUp.Text = "Hacer BackUp";
            btnBackUp.UseVisualStyleBackColor = true;
            btnBackUp.Click += btnBackUp_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(476, 78);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(93, 38);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // frmBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 400);
            Controls.Add(btnSalir);
            Controls.Add(btnBackUp);
            Controls.Add(groupBox1);
            Name = "frmBackup";
            Text = "BackUp";
            Load += frmBackup_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBackUp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dgvBackUp;
        private Button btnBackUp;
        private Button btnSalir;
    }
}