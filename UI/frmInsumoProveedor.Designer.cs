namespace UI
{
    partial class frmInsumoProveedor
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
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            txtDomicilioProveedor = new TextBox();
            label6 = new Label();
            txtEmailProveedor = new TextBox();
            label5 = new Label();
            txtTelefonoProveedor = new TextBox();
            label1 = new Label();
            txtIdProveedor = new TextBox();
            label2 = new Label();
            txtCUILProveedor = new TextBox();
            txtNombreProveedor = new TextBox();
            label4 = new Label();
            label3 = new Label();
            cmbUnidadMedida = new ComboBox();
            label7 = new Label();
            txtIdInsumo = new TextBox();
            txtPrecio = new TextBox();
            label8 = new Label();
            txtCantidad = new TextBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            txtNombre = new TextBox();
            btnLimpiar = new Button();
            btnVincularInsumo = new Button();
            btnModificarInsumo = new Button();
            btnDesvincularInsumo = new Button();
            btnSalir = new Button();
            comboBox1 = new ComboBox();
            label12 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(381, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(617, 218);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView2);
            groupBox2.Location = new Point(381, 297);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(617, 271);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Productos Proveedor";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(605, 181);
            dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(6, 19);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.Size = new Size(595, 246);
            dataGridView2.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(txtDomicilioProveedor);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(txtEmailProveedor);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(txtTelefonoProveedor);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(txtIdProveedor);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(txtCUILProveedor);
            groupBox3.Controls.Add(txtNombreProveedor);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label3);
            groupBox3.Location = new Point(34, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(320, 278);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Datos Proveedor";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cmbUnidadMedida);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(txtIdInsumo);
            groupBox4.Controls.Add(txtPrecio);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(txtCantidad);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(txtNombre);
            groupBox4.Location = new Point(34, 297);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(320, 271);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Datos Insumos";
            // 
            // txtDomicilioProveedor
            // 
            txtDomicilioProveedor.Font = new Font("Segoe UI", 10F);
            txtDomicilioProveedor.Location = new Point(121, 172);
            txtDomicilioProveedor.Name = "txtDomicilioProveedor";
            txtDomicilioProveedor.ReadOnly = true;
            txtDomicilioProveedor.Size = new Size(166, 25);
            txtDomicilioProveedor.TabIndex = 20;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(6, 175);
            label6.Name = "label6";
            label6.Size = new Size(65, 19);
            label6.TabIndex = 29;
            label6.Text = "Domicilio";
            // 
            // txtEmailProveedor
            // 
            txtEmailProveedor.Font = new Font("Segoe UI", 10F);
            txtEmailProveedor.Location = new Point(121, 206);
            txtEmailProveedor.Name = "txtEmailProveedor";
            txtEmailProveedor.ReadOnly = true;
            txtEmailProveedor.Size = new Size(166, 25);
            txtEmailProveedor.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(6, 209);
            label5.Name = "label5";
            label5.Size = new Size(41, 19);
            label5.TabIndex = 28;
            label5.Text = "Email";
            // 
            // txtTelefonoProveedor
            // 
            txtTelefonoProveedor.Font = new Font("Segoe UI", 10F);
            txtTelefonoProveedor.Location = new Point(121, 236);
            txtTelefonoProveedor.Name = "txtTelefonoProveedor";
            txtTelefonoProveedor.ReadOnly = true;
            txtTelefonoProveedor.Size = new Size(166, 25);
            txtTelefonoProveedor.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(6, 239);
            label1.Name = "label1";
            label1.Size = new Size(60, 19);
            label1.TabIndex = 27;
            label1.Text = "Telefono";
            // 
            // txtIdProveedor
            // 
            txtIdProveedor.Font = new Font("Segoe UI", 10F);
            txtIdProveedor.Location = new Point(121, 76);
            txtIdProveedor.Name = "txtIdProveedor";
            txtIdProveedor.ReadOnly = true;
            txtIdProveedor.Size = new Size(166, 25);
            txtIdProveedor.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(6, 79);
            label2.Name = "label2";
            label2.Size = new Size(23, 19);
            label2.TabIndex = 25;
            label2.Text = "ID";
            // 
            // txtCUILProveedor
            // 
            txtCUILProveedor.Font = new Font("Segoe UI", 10F);
            txtCUILProveedor.Location = new Point(121, 140);
            txtCUILProveedor.Name = "txtCUILProveedor";
            txtCUILProveedor.ReadOnly = true;
            txtCUILProveedor.Size = new Size(166, 25);
            txtCUILProveedor.TabIndex = 19;
            // 
            // txtNombreProveedor
            // 
            txtNombreProveedor.Font = new Font("Segoe UI", 10F);
            txtNombreProveedor.Location = new Point(121, 106);
            txtNombreProveedor.Name = "txtNombreProveedor";
            txtNombreProveedor.ReadOnly = true;
            txtNombreProveedor.Size = new Size(166, 25);
            txtNombreProveedor.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(6, 109);
            label4.Name = "label4";
            label4.Size = new Size(59, 19);
            label4.TabIndex = 24;
            label4.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(6, 143);
            label3.Name = "label3";
            label3.Size = new Size(39, 19);
            label3.TabIndex = 23;
            label3.Text = "CUIL";
            // 
            // cmbUnidadMedida
            // 
            cmbUnidadMedida.FormattingEnabled = true;
            cmbUnidadMedida.Location = new Point(121, 130);
            cmbUnidadMedida.Name = "cmbUnidadMedida";
            cmbUnidadMedida.Size = new Size(166, 23);
            cmbUnidadMedida.TabIndex = 26;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(6, 35);
            label7.Name = "label7";
            label7.Size = new Size(23, 19);
            label7.TabIndex = 25;
            label7.Text = "ID";
            // 
            // txtIdInsumo
            // 
            txtIdInsumo.Font = new Font("Segoe UI", 10F);
            txtIdInsumo.Location = new Point(121, 32);
            txtIdInsumo.Name = "txtIdInsumo";
            txtIdInsumo.ReadOnly = true;
            txtIdInsumo.Size = new Size(166, 25);
            txtIdInsumo.TabIndex = 24;
            // 
            // txtPrecio
            // 
            txtPrecio.Font = new Font("Segoe UI", 10F);
            txtPrecio.Location = new Point(121, 218);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(166, 25);
            txtPrecio.TabIndex = 23;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(6, 221);
            label8.Name = "label8";
            label8.Size = new Size(46, 19);
            label8.TabIndex = 22;
            label8.Text = "Precio";
            // 
            // txtCantidad
            // 
            txtCantidad.Font = new Font("Segoe UI", 10F);
            txtCantidad.Location = new Point(121, 174);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(166, 25);
            txtCantidad.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(6, 130);
            label9.Name = "label9";
            label9.Size = new Size(103, 19);
            label9.TabIndex = 20;
            label9.Text = "Unidad Medida";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(6, 177);
            label10.Name = "label10";
            label10.Size = new Size(64, 19);
            label10.TabIndex = 19;
            label10.Text = "Cantidad";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F);
            label11.Location = new Point(6, 81);
            label11.Name = "label11";
            label11.Size = new Size(59, 19);
            label11.TabIndex = 18;
            label11.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(121, 78);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(166, 25);
            txtNombre.TabIndex = 17;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(786, 248);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(103, 42);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpia Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnVincularInsumo
            // 
            btnVincularInsumo.Location = new Point(381, 248);
            btnVincularInsumo.Name = "btnVincularInsumo";
            btnVincularInsumo.Size = new Size(103, 42);
            btnVincularInsumo.TabIndex = 5;
            btnVincularInsumo.Text = "Vincular Insumo";
            btnVincularInsumo.UseVisualStyleBackColor = true;
            // 
            // btnModificarInsumo
            // 
            btnModificarInsumo.Location = new Point(490, 248);
            btnModificarInsumo.Name = "btnModificarInsumo";
            btnModificarInsumo.Size = new Size(103, 42);
            btnModificarInsumo.TabIndex = 6;
            btnModificarInsumo.Text = "Modificar Insumo";
            btnModificarInsumo.UseVisualStyleBackColor = true;
            // 
            // btnDesvincularInsumo
            // 
            btnDesvincularInsumo.Location = new Point(599, 248);
            btnDesvincularInsumo.Name = "btnDesvincularInsumo";
            btnDesvincularInsumo.Size = new Size(103, 42);
            btnDesvincularInsumo.TabIndex = 7;
            btnDesvincularInsumo.Text = "Desvincular Insumo";
            btnDesvincularInsumo.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(895, 248);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(103, 42);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(121, 36);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(166, 23);
            comboBox1.TabIndex = 30;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F);
            label12.Location = new Point(6, 37);
            label12.Name = "label12";
            label12.Size = new Size(72, 19);
            label12.TabIndex = 31;
            label12.Text = "Proveedor";
            // 
            // frmInsumoProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1033, 581);
            Controls.Add(btnSalir);
            Controls.Add(btnDesvincularInsumo);
            Controls.Add(btnModificarInsumo);
            Controls.Add(btnVincularInsumo);
            Controls.Add(btnLimpiar);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmInsumoProveedor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Proveedor insumos";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private GroupBox groupBox2;
        private DataGridView dataGridView2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox txtDomicilioProveedor;
        private Label label6;
        private TextBox txtEmailProveedor;
        private Label label5;
        private TextBox txtTelefonoProveedor;
        private Label label1;
        private TextBox txtIdProveedor;
        private Label label2;
        private TextBox txtCUILProveedor;
        private TextBox txtNombreProveedor;
        private Label label4;
        private Label label3;
        private ComboBox cmbUnidadMedida;
        private Label label7;
        private TextBox txtIdInsumo;
        private TextBox txtPrecio;
        private Label label8;
        private TextBox txtCantidad;
        private Label label9;
        private Label label10;
        private Label label11;
        private TextBox txtNombre;
        private Label label12;
        private ComboBox comboBox1;
        private Button btnLimpiar;
        private Button btnVincularInsumo;
        private Button btnModificarInsumo;
        private Button btnDesvincularInsumo;
        private Button btnSalir;
    }
}