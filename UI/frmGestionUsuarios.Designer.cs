namespace UI
{
    partial class frmGestionUsuarios
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
            chbActivo = new CheckBox();
            chbDescifrar = new CheckBox();
            chbBloqueado = new CheckBox();
            txtPassword = new TextBox();
            txtNombre = new TextBox();
            txtIDUsuario = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox4 = new GroupBox();
            btnQuitarPermisoSuario = new Button();
            btnAsociarPermisoUsuario = new Button();
            groupBox3 = new GroupBox();
            btnQuitarRolUsuario = new Button();
            btnAsociarRolUsuario = new Button();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            txtNombreRoles = new TextBox();
            txtIDRoles = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            cmbRoles = new ComboBox();
            btnQuitarRolesUsuario = new Button();
            btnAsociarRolesUsuario = new Button();
            btnEliminarRol = new Button();
            btnModificarRol = new Button();
            btnAltaRol = new Button();
            txtNombreRol = new TextBox();
            label5 = new Label();
            txtIDRol = new TextBox();
            label4 = new Label();
            groupBox7 = new GroupBox();
            btnAltaPermiso = new Button();
            btnEliminarPermiso = new Button();
            label12 = new Label();
            cmbItem = new ComboBox();
            label11 = new Label();
            cmbMenu = new ComboBox();
            txtNombrePermiso = new TextBox();
            label9 = new Label();
            txtIDPermiso = new TextBox();
            label10 = new Label();
            groupBox8 = new GroupBox();
            btnQuitarPermisoRol = new Button();
            btnAsociarPermisoRol = new Button();
            btnLimpiar = new Button();
            btnSalir = new Button();
            groupBox9 = new GroupBox();
            treeVwUsuarios = new TreeView();
            groupBox10 = new GroupBox();
            treeVwRoles = new TreeView();
            groupBox11 = new GroupBox();
            treeVwPermisos = new TreeView();
            groupBox12 = new GroupBox();
            treeVwPermisosPorRol = new TreeView();
            groupBox13 = new GroupBox();
            treeVwUsuarioPermisosRoles = new TreeView();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox12.SuspendLayout();
            groupBox13.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chbActivo);
            groupBox1.Controls.Add(chbDescifrar);
            groupBox1.Controls.Add(chbBloqueado);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtIDUsuario);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(409, 143);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Usuario";
            // 
            // chbActivo
            // 
            chbActivo.AutoSize = true;
            chbActivo.Location = new Point(265, 66);
            chbActivo.Name = "chbActivo";
            chbActivo.Size = new Size(60, 19);
            chbActivo.TabIndex = 8;
            chbActivo.Text = "Activo";
            chbActivo.UseVisualStyleBackColor = true;
            // 
            // chbDescifrar
            // 
            chbDescifrar.AutoSize = true;
            chbDescifrar.Location = new Point(265, 101);
            chbDescifrar.Name = "chbDescifrar";
            chbDescifrar.Size = new Size(138, 19);
            chbDescifrar.TabIndex = 7;
            chbDescifrar.Text = "Descifrar/Cifrar Clave";
            chbDescifrar.UseVisualStyleBackColor = true;
            chbDescifrar.CheckedChanged += chbDescifrar_CheckedChanged;
            // 
            // chbBloqueado
            // 
            chbBloqueado.AutoSize = true;
            chbBloqueado.Location = new Point(265, 30);
            chbBloqueado.Name = "chbBloqueado";
            chbBloqueado.Size = new Size(83, 19);
            chbBloqueado.TabIndex = 6;
            chbBloqueado.Text = "Bloqueado";
            chbBloqueado.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(85, 98);
            txtPassword.Name = "txtPassword";
            txtPassword.ReadOnly = true;
            txtPassword.Size = new Size(154, 23);
            txtPassword.TabIndex = 5;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(85, 63);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(154, 23);
            txtNombre.TabIndex = 4;
            // 
            // txtIDUsuario
            // 
            txtIDUsuario.Location = new Point(85, 27);
            txtIDUsuario.Name = "txtIDUsuario";
            txtIDUsuario.ReadOnly = true;
            txtIDUsuario.Size = new Size(154, 23);
            txtIDUsuario.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 101);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 2;
            label3.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 66);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 1;
            label2.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnQuitarPermisoSuario);
            groupBox4.Controls.Add(btnAsociarPermisoUsuario);
            groupBox4.Location = new Point(483, 591);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(234, 66);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Permisos a Usuario";
            // 
            // btnQuitarPermisoSuario
            // 
            btnQuitarPermisoSuario.Location = new Point(151, 22);
            btnQuitarPermisoSuario.Name = "btnQuitarPermisoSuario";
            btnQuitarPermisoSuario.Size = new Size(77, 32);
            btnQuitarPermisoSuario.TabIndex = 3;
            btnQuitarPermisoSuario.Text = "Quitar Permiso\r\n";
            btnQuitarPermisoSuario.UseVisualStyleBackColor = true;
            btnQuitarPermisoSuario.Click += btnQuitarPermisoSuario_Click;
            // 
            // btnAsociarPermisoUsuario
            // 
            btnAsociarPermisoUsuario.Location = new Point(6, 22);
            btnAsociarPermisoUsuario.Name = "btnAsociarPermisoUsuario";
            btnAsociarPermisoUsuario.Size = new Size(77, 32);
            btnAsociarPermisoUsuario.TabIndex = 2;
            btnAsociarPermisoUsuario.Text = "Asociar Permiso";
            btnAsociarPermisoUsuario.UseVisualStyleBackColor = true;
            btnAsociarPermisoUsuario.Click += btnAsociarPermisoUsuario_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnQuitarRolUsuario);
            groupBox3.Controls.Add(btnAsociarRolUsuario);
            groupBox3.Location = new Point(245, 519);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(232, 66);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Roles a Usuario";
            // 
            // btnQuitarRolUsuario
            // 
            btnQuitarRolUsuario.Location = new Point(144, 22);
            btnQuitarRolUsuario.Name = "btnQuitarRolUsuario";
            btnQuitarRolUsuario.Size = new Size(77, 32);
            btnQuitarRolUsuario.TabIndex = 1;
            btnQuitarRolUsuario.Text = "Quitar Rol";
            btnQuitarRolUsuario.UseVisualStyleBackColor = true;
            btnQuitarRolUsuario.Click += btnQuitarRolUsuario_Click;
            // 
            // btnAsociarRolUsuario
            // 
            btnAsociarRolUsuario.Location = new Point(6, 22);
            btnAsociarRolUsuario.Name = "btnAsociarRolUsuario";
            btnAsociarRolUsuario.Size = new Size(77, 32);
            btnAsociarRolUsuario.TabIndex = 0;
            btnAsociarRolUsuario.Text = "Asociar Rol";
            btnAsociarRolUsuario.UseVisualStyleBackColor = true;
            btnAsociarRolUsuario.Click += btnAsociarRolUsuario_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnEliminarRol);
            groupBox5.Controls.Add(btnModificarRol);
            groupBox5.Controls.Add(btnAltaRol);
            groupBox5.Controls.Add(txtNombreRol);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(txtIDRol);
            groupBox5.Controls.Add(label4);
            groupBox5.Location = new Point(437, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(348, 143);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "Rol";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(txtNombreRoles);
            groupBox6.Controls.Add(txtIDRoles);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(cmbRoles);
            groupBox6.Controls.Add(btnQuitarRolesUsuario);
            groupBox6.Controls.Add(btnAsociarRolesUsuario);
            groupBox6.Location = new Point(729, 519);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(331, 152);
            groupBox6.TabIndex = 11;
            groupBox6.TabStop = false;
            groupBox6.Text = "Rol para Asociar/Desasociar a otro Rol";
            // 
            // txtNombreRoles
            // 
            txtNombreRoles.Location = new Point(188, 59);
            txtNombreRoles.Name = "txtNombreRoles";
            txtNombreRoles.ReadOnly = true;
            txtNombreRoles.Size = new Size(129, 23);
            txtNombreRoles.TabIndex = 12;
            // 
            // txtIDRoles
            // 
            txtIDRoles.Location = new Point(32, 59);
            txtIDRoles.Name = "txtIDRoles";
            txtIDRoles.ReadOnly = true;
            txtIDRoles.Size = new Size(80, 23);
            txtIDRoles.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 28);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 5;
            label8.Text = "Roles";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(131, 62);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 4;
            label7.Text = "Nombre";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 62);
            label6.Name = "label6";
            label6.Size = new Size(18, 15);
            label6.TabIndex = 3;
            label6.Text = "ID";
            // 
            // cmbRoles
            // 
            cmbRoles.FormattingEnabled = true;
            cmbRoles.Location = new Point(107, 25);
            cmbRoles.Name = "cmbRoles";
            cmbRoles.Size = new Size(210, 23);
            cmbRoles.TabIndex = 2;
            cmbRoles.SelectedIndexChanged += cmbRoles_SelectedIndexChanged;
            // 
            // btnQuitarRolesUsuario
            // 
            btnQuitarRolesUsuario.Location = new Point(123, 100);
            btnQuitarRolesUsuario.Name = "btnQuitarRolesUsuario";
            btnQuitarRolesUsuario.Size = new Size(90, 28);
            btnQuitarRolesUsuario.TabIndex = 1;
            btnQuitarRolesUsuario.Text = "Quitar Roles a Rol";
            btnQuitarRolesUsuario.UseVisualStyleBackColor = true;
            btnQuitarRolesUsuario.Click += btnQuitarRolesUsuario_Click;
            // 
            // btnAsociarRolesUsuario
            // 
            btnAsociarRolesUsuario.Location = new Point(22, 100);
            btnAsociarRolesUsuario.Name = "btnAsociarRolesUsuario";
            btnAsociarRolesUsuario.Size = new Size(90, 28);
            btnAsociarRolesUsuario.TabIndex = 0;
            btnAsociarRolesUsuario.Text = "Asociar Roles a Rol";
            btnAsociarRolesUsuario.UseVisualStyleBackColor = true;
            btnAsociarRolesUsuario.Click += btnAsociarRolesUsuario_Click;
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.Location = new Point(226, 56);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(102, 39);
            btnEliminarRol.TabIndex = 10;
            btnEliminarRol.Text = "Eliminar";
            btnEliminarRol.UseVisualStyleBackColor = true;
            btnEliminarRol.Click += btnEliminarRol_Click;
            // 
            // btnModificarRol
            // 
            btnModificarRol.Location = new Point(118, 55);
            btnModificarRol.Name = "btnModificarRol";
            btnModificarRol.Size = new Size(102, 39);
            btnModificarRol.TabIndex = 9;
            btnModificarRol.Text = "Modificar";
            btnModificarRol.UseVisualStyleBackColor = true;
            btnModificarRol.Click += btnModificarRol_Click;
            // 
            // btnAltaRol
            // 
            btnAltaRol.Location = new Point(11, 55);
            btnAltaRol.Name = "btnAltaRol";
            btnAltaRol.Size = new Size(102, 39);
            btnAltaRol.TabIndex = 8;
            btnAltaRol.Text = "Alta";
            btnAltaRol.UseVisualStyleBackColor = true;
            btnAltaRol.Click += btnAltaRol_Click;
            // 
            // txtNombreRol
            // 
            txtNombreRol.Location = new Point(199, 24);
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(129, 23);
            txtNombreRol.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(142, 27);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 6;
            label5.Text = "Nombre";
            // 
            // txtIDRol
            // 
            txtIDRol.Location = new Point(43, 24);
            txtIDRol.Name = "txtIDRol";
            txtIDRol.ReadOnly = true;
            txtIDRol.Size = new Size(80, 23);
            txtIDRol.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 29);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 4;
            label4.Text = "ID";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btnAltaPermiso);
            groupBox7.Controls.Add(btnEliminarPermiso);
            groupBox7.Controls.Add(label12);
            groupBox7.Controls.Add(cmbItem);
            groupBox7.Controls.Add(label11);
            groupBox7.Controls.Add(cmbMenu);
            groupBox7.Controls.Add(txtNombrePermiso);
            groupBox7.Controls.Add(label9);
            groupBox7.Controls.Add(txtIDPermiso);
            groupBox7.Controls.Add(label10);
            groupBox7.Location = new Point(802, 12);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(408, 143);
            groupBox7.TabIndex = 3;
            groupBox7.TabStop = false;
            groupBox7.Text = "Permisos";
            // 
            // btnAltaPermiso
            // 
            btnAltaPermiso.Location = new Point(27, 87);
            btnAltaPermiso.Name = "btnAltaPermiso";
            btnAltaPermiso.Size = new Size(103, 34);
            btnAltaPermiso.TabIndex = 17;
            btnAltaPermiso.Text = "Alta ";
            btnAltaPermiso.UseVisualStyleBackColor = true;
            btnAltaPermiso.Click += btnAltaPermiso_Click;
            // 
            // btnEliminarPermiso
            // 
            btnEliminarPermiso.Location = new Point(136, 89);
            btnEliminarPermiso.Name = "btnEliminarPermiso";
            btnEliminarPermiso.Size = new Size(95, 31);
            btnEliminarPermiso.TabIndex = 16;
            btnEliminarPermiso.Text = "Eliminar";
            btnEliminarPermiso.UseVisualStyleBackColor = true;
            btnEliminarPermiso.Click += btnEliminarPermiso_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(199, 61);
            label12.Name = "label12";
            label12.Size = new Size(31, 15);
            label12.TabIndex = 15;
            label12.Text = "Item";
            // 
            // cmbItem
            // 
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(233, 58);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(167, 23);
            cmbItem.TabIndex = 14;
            cmbItem.SelectedIndexChanged += cmbItem_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 61);
            label11.Name = "label11";
            label11.Size = new Size(38, 15);
            label11.TabIndex = 13;
            label11.Text = "Menu";
            // 
            // cmbMenu
            // 
            cmbMenu.FormattingEnabled = true;
            cmbMenu.Location = new Point(50, 58);
            cmbMenu.Name = "cmbMenu";
            cmbMenu.Size = new Size(130, 23);
            cmbMenu.TabIndex = 12;
            cmbMenu.SelectedIndexChanged += cmbMenu_SelectedIndexChanged;
            // 
            // txtNombrePermiso
            // 
            txtNombrePermiso.Location = new Point(271, 19);
            txtNombrePermiso.Name = "txtNombrePermiso";
            txtNombrePermiso.ReadOnly = true;
            txtNombrePermiso.Size = new Size(129, 23);
            txtNombrePermiso.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(196, 22);
            label9.Name = "label9";
            label9.Size = new Size(51, 15);
            label9.TabIndex = 10;
            label9.Text = "Nombre";
            // 
            // txtIDPermiso
            // 
            txtIDPermiso.Location = new Point(50, 19);
            txtIDPermiso.Name = "txtIDPermiso";
            txtIDPermiso.ReadOnly = true;
            txtIDPermiso.Size = new Size(80, 23);
            txtIDPermiso.TabIndex = 9;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 24);
            label10.Name = "label10";
            label10.Size = new Size(18, 15);
            label10.TabIndex = 8;
            label10.Text = "ID";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnQuitarPermisoRol);
            groupBox8.Controls.Add(btnAsociarPermisoRol);
            groupBox8.Location = new Point(483, 519);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(234, 66);
            groupBox8.TabIndex = 4;
            groupBox8.TabStop = false;
            groupBox8.Text = "Permisos a Roles";
            // 
            // btnQuitarPermisoRol
            // 
            btnQuitarPermisoRol.Location = new Point(151, 23);
            btnQuitarPermisoRol.Name = "btnQuitarPermisoRol";
            btnQuitarPermisoRol.Size = new Size(77, 28);
            btnQuitarPermisoRol.TabIndex = 19;
            btnQuitarPermisoRol.Text = "Quitar Permiso\r\n";
            btnQuitarPermisoRol.UseVisualStyleBackColor = true;
            btnQuitarPermisoRol.Click += btnQuitarPermisoRol_Click;
            // 
            // btnAsociarPermisoRol
            // 
            btnAsociarPermisoRol.Location = new Point(6, 23);
            btnAsociarPermisoRol.Name = "btnAsociarPermisoRol";
            btnAsociarPermisoRol.Size = new Size(77, 28);
            btnAsociarPermisoRol.TabIndex = 18;
            btnAsociarPermisoRol.Text = "Asociar Permiso\r\n";
            btnAsociarPermisoRol.UseVisualStyleBackColor = true;
            btnAsociarPermisoRol.Click += btnAsociarPermisoRol_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(1120, 519);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(90, 50);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1120, 573);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(90, 50);
            btnSalir.TabIndex = 19;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(treeVwUsuarios);
            groupBox9.Location = new Point(12, 161);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(225, 349);
            groupBox9.TabIndex = 20;
            groupBox9.TabStop = false;
            groupBox9.Text = "Usuarios";
            // 
            // treeVwUsuarios
            // 
            treeVwUsuarios.Location = new Point(6, 22);
            treeVwUsuarios.Name = "treeVwUsuarios";
            treeVwUsuarios.Size = new Size(213, 321);
            treeVwUsuarios.TabIndex = 0;
            treeVwUsuarios.AfterSelect += treeVwUsuarios_AfterSelect;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(treeVwRoles);
            groupBox10.Location = new Point(245, 164);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(232, 349);
            groupBox10.TabIndex = 21;
            groupBox10.TabStop = false;
            groupBox10.Text = "Roles";
            // 
            // treeVwRoles
            // 
            treeVwRoles.Location = new Point(6, 22);
            treeVwRoles.Name = "treeVwRoles";
            treeVwRoles.Size = new Size(220, 321);
            treeVwRoles.TabIndex = 1;
            treeVwRoles.AfterSelect += treeVwRoles_AfterSelect;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(treeVwPermisos);
            groupBox11.Location = new Point(483, 164);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(234, 349);
            groupBox11.TabIndex = 22;
            groupBox11.TabStop = false;
            groupBox11.Text = "Permisos";
            // 
            // treeVwPermisos
            // 
            treeVwPermisos.Location = new Point(6, 22);
            treeVwPermisos.Name = "treeVwPermisos";
            treeVwPermisos.Size = new Size(222, 321);
            treeVwPermisos.TabIndex = 1;
            treeVwPermisos.AfterSelect += treeVwPermisos_AfterSelect;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(treeVwPermisosPorRol);
            groupBox12.Location = new Point(723, 164);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(232, 349);
            groupBox12.TabIndex = 21;
            groupBox12.TabStop = false;
            groupBox12.Text = "Permisos Por Rol";
            // 
            // treeVwPermisosPorRol
            // 
            treeVwPermisosPorRol.Location = new Point(6, 22);
            treeVwPermisosPorRol.Name = "treeVwPermisosPorRol";
            treeVwPermisosPorRol.Size = new Size(220, 321);
            treeVwPermisosPorRol.TabIndex = 1;
            treeVwPermisosPorRol.AfterSelect += treeVwPermisosRol_AfterSelect;
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(treeVwUsuarioPermisosRoles);
            groupBox13.Location = new Point(961, 164);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(249, 349);
            groupBox13.TabIndex = 21;
            groupBox13.TabStop = false;
            groupBox13.Text = "Roles y Permisos del Usuario";
            // 
            // treeVwUsuarioPermisosRoles
            // 
            treeVwUsuarioPermisosRoles.Location = new Point(6, 22);
            treeVwUsuarioPermisosRoles.Name = "treeVwUsuarioPermisosRoles";
            treeVwUsuarioPermisosRoles.Size = new Size(237, 321);
            treeVwUsuarioPermisosRoles.TabIndex = 1;
            treeVwUsuarioPermisosRoles.AfterSelect += treeVwRolesPermisosUsuarios_AfterSelect;
            // 
            // frmGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 722);
            Controls.Add(groupBox6);
            Controls.Add(groupBox4);
            Controls.Add(groupBox13);
            Controls.Add(groupBox3);
            Controls.Add(groupBox12);
            Controls.Add(groupBox11);
            Controls.Add(groupBox10);
            Controls.Add(groupBox9);
            Controls.Add(btnSalir);
            Controls.Add(btnLimpiar);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox5);
            Controls.Add(groupBox1);
            Name = "frmGestionUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestion de Usuarios";
            Load += frmGestionUsuarios_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox12.ResumeLayout(false);
            groupBox13.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox chbActivo;
        private CheckBox chbDescifrar;
        private CheckBox chbBloqueado;
        private TextBox txtPassword;
        private TextBox txtNombre;
        private TextBox txtIDUsuario;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private Button btnQuitarPermisoSuario;
        private Button btnAsociarPermisoUsuario;
        private GroupBox groupBox3;
        private Button btnQuitarRolUsuario;
        private Button btnAsociarRolUsuario;
        private GroupBox groupBox5;
        private Button btnEliminarRol;
        private Button btnModificarRol;
        private Button btnAltaRol;
        private TextBox txtNombreRol;
        private Label label5;
        private TextBox txtIDRol;
        private Label label4;
        private GroupBox groupBox6;
        private TextBox txtNombreRoles;
        private TextBox txtIDRoles;
        private Label label8;
        private Label label7;
        private Label label6;
        private ComboBox cmbRoles;
        private Button btnQuitarRolesUsuario;
        private Button btnAsociarRolesUsuario;
        private GroupBox groupBox7;
        private Button btnAltaPermiso;
        private Button btnEliminarPermiso;
        private Label label12;
        private ComboBox cmbItem;
        private Label label11;
        private ComboBox cmbMenu;
        private TextBox txtNombrePermiso;
        private Label label9;
        private TextBox txtIDPermiso;
        private Label label10;
        private GroupBox groupBox8;
        private Button btnQuitarPermisoRol;
        private Button btnAsociarPermisoRol;
        private Button btnLimpiar;
        private Button btnSalir;
        private GroupBox groupBox9;
        private TreeView treeVwUsuarios;
        private GroupBox groupBox10;
        private TreeView treeVwRoles;
        private GroupBox groupBox11;
        private TreeView treeVwPermisos;
        private GroupBox groupBox12;
        private TreeView treeVwPermisosPorRol;
        private GroupBox groupBox13;
        private TreeView treeVwUsuarioPermisosRoles;
    }
}