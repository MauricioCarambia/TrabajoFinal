namespace UI
{
    partial class frmLogin
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
            btnLogin = new Button();
            txtUsuario = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            chkEncriptado = new CheckBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 11F);
            btnLogin.Location = new Point(39, 247);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(148, 37);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Entrar";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(39, 107);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(148, 27);
            txtUsuario.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(39, 169);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(148, 27);
            txtPassword.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(39, 84);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 3;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(39, 146);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(20, 33);
            label3.Name = "label3";
            label3.Size = new Size(185, 20);
            label3.TabIndex = 5;
            label3.Text = "Por favor ingrese sus datos";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkEncriptado);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnLogin);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtUsuario);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Location = new Point(49, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(219, 299);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Login";
            // 
            // chkEncriptado
            // 
            chkEncriptado.AutoSize = true;
            chkEncriptado.Location = new Point(48, 211);
            chkEncriptado.Name = "chkEncriptado";
            chkEncriptado.Size = new Size(130, 19);
            chkEncriptado.TabIndex = 6;
            chkEncriptado.Text = "Mostrar Contraseña";
            chkEncriptado.UseVisualStyleBackColor = true;
            chkEncriptado.CheckedChanged += chkEncriptado_CheckedChanged;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(322, 393);
            Controls.Add(groupBox1);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += frmLogin_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLogin;
        private TextBox txtUsuario;
        private TextBox txtPassword;
        private Label label1;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private CheckBox chkEncriptado;
    }
}