namespace UI
{
    partial class frmRestore
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
            btnRestore = new Button();
            groupBox1 = new GroupBox();
            btnSalir = new Button();
            treeVwRestore = new TreeView();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnRestore
            // 
            btnRestore.Location = new Point(434, 34);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(94, 35);
            btnRestore.TabIndex = 1;
            btnRestore.Text = "Hacer Restore";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += btnRestore_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(treeVwRestore);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(416, 370);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "BackUp Disponibles";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(434, 75);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 35);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // treeVwRestore
            // 
            treeVwRestore.Location = new Point(6, 22);
            treeVwRestore.Name = "treeVwRestore";
            treeVwRestore.Size = new Size(404, 342);
            treeVwRestore.TabIndex = 0;
            // 
            // frmRestore
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 401);
            Controls.Add(btnSalir);
            Controls.Add(groupBox1);
            Controls.Add(btnRestore);
            Name = "frmRestore";
            Text = "Restore";
            Load += frmRestore_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnRestore;
        private GroupBox groupBox1;
        private Button btnSalir;
        private TreeView treeVwRestore;
    }
}