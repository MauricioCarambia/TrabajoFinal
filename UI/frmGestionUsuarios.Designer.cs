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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtID = new TextBox();
            txtNombre = new TextBox();
            txtPassword = new TextBox();
            chbBloqueado = new CheckBox();
            chbDescifrar = new CheckBox();
            chbActivo = new CheckBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            btnAsociarRolUsuario = new Button();
            btnQuitarRolUsuario = new Button();
            btnAsociarPermisoUsuario = new Button();
            btnQuitarPermisoSuario = new Button();
            groupBox5 = new GroupBox();
            txtIDRol = new TextBox();
            label4 = new Label();
            txtNombreRol = new TextBox();
            label5 = new Label();
            btnAltaRol = new Button();
            btnModificarRol = new Button();
            btnEliminarRol = new Button();
            groupBox6 = new GroupBox();
            btnAsociarRolesUsuario = new Button();
            btnQuitarRolesUsuario = new Button();
            cmbRoles = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtIDRoles = new TextBox();
            txtNombreRoles = new TextBox();
            groupBox7 = new GroupBox();
            txtNombrePermiso = new TextBox();
            label9 = new Label();
            txtIDPermiso = new TextBox();
            label10 = new Label();
            label11 = new Label();
            cmbMenu = new ComboBox();
            label12 = new Label();
            cmbItem = new ComboBox();
            btnEliminarPermiso = new Button();
            btnAltaPermiso = new Button();
            groupBox8 = new GroupBox();
            btnAsociarPermisoRol = new Button();
            btnQuitarPermisoRol = new Button();
            btnLimpiar = new Button();
            btnSalir = new Button();
            groupBox9 = new GroupBox();
            groupBox10 = new GroupBox();
            groupBox11 = new GroupBox();
            groupBox12 = new GroupBox();
            groupBox13 = new GroupBox();
            treeUsuarios = new TreeView();
            treeRoles = new TreeView();
            treePermisos = new TreeView();
            treePermisosRol = new TreeView();
            treeRolesPermisosUsuarios = new TreeView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
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
            groupBox1.Controls.Add(txtID);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 0;
            label1.Text = "ID";
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 101);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 2;
            label3.Text = "Password";
            // 
            // txtID
            // 
            txtID.Location = new Point(85, 27);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(154, 23);
            txtID.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(85, 63);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(154, 23);
            txtNombre.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(85, 98);
            txtPassword.Name = "txtPassword";
            txtPassword.ReadOnly = true;
            txtPassword.Size = new Size(154, 23);
            txtPassword.TabIndex = 5;
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
            // chbDescifrar
            // 
            chbDescifrar.AutoSize = true;
            chbDescifrar.Location = new Point(265, 101);
            chbDescifrar.Name = "chbDescifrar";
            chbDescifrar.Size = new Size(138, 19);
            chbDescifrar.TabIndex = 7;
            chbDescifrar.Text = "Descifrar/Cifrar Clave";
            chbDescifrar.UseVisualStyleBackColor = true;
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
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(12, 166);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(409, 122);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Roles / Permisos Usuario";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnQuitarRolUsuario);
            groupBox3.Controls.Add(btnAsociarRolUsuario);
            groupBox3.Location = new Point(14, 25);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(178, 86);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Roles a Usuario";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnQuitarPermisoSuario);
            groupBox4.Controls.Add(btnAsociarPermisoUsuario);
            groupBox4.Location = new Point(227, 25);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(176, 86);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Permisos a Usuario";
            // 
            // btnAsociarRolUsuario
            // 
            btnAsociarRolUsuario.Location = new Point(6, 22);
            btnAsociarRolUsuario.Name = "btnAsociarRolUsuario";
            btnAsociarRolUsuario.Size = new Size(77, 54);
            btnAsociarRolUsuario.TabIndex = 0;
            btnAsociarRolUsuario.Text = "Asociar Rol a Usuario";
            btnAsociarRolUsuario.UseVisualStyleBackColor = true;
            // 
            // btnQuitarRolUsuario
            // 
            btnQuitarRolUsuario.Location = new Point(89, 22);
            btnQuitarRolUsuario.Name = "btnQuitarRolUsuario";
            btnQuitarRolUsuario.Size = new Size(77, 54);
            btnQuitarRolUsuario.TabIndex = 1;
            btnQuitarRolUsuario.Text = "Quitar Rol a Usuario";
            btnQuitarRolUsuario.UseVisualStyleBackColor = true;
            // 
            // btnAsociarPermisoUsuario
            // 
            btnAsociarPermisoUsuario.Location = new Point(6, 22);
            btnAsociarPermisoUsuario.Name = "btnAsociarPermisoUsuario";
            btnAsociarPermisoUsuario.Size = new Size(77, 54);
            btnAsociarPermisoUsuario.TabIndex = 2;
            btnAsociarPermisoUsuario.Text = "Asociar Permiso a Usuario";
            btnAsociarPermisoUsuario.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermisoSuario
            // 
            btnQuitarPermisoSuario.Location = new Point(89, 21);
            btnQuitarPermisoSuario.Name = "btnQuitarPermisoSuario";
            btnQuitarPermisoSuario.Size = new Size(77, 54);
            btnQuitarPermisoSuario.TabIndex = 3;
            btnQuitarPermisoSuario.Text = "Quitar Permiso a Usuario";
            btnQuitarPermisoSuario.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(btnEliminarRol);
            groupBox5.Controls.Add(btnModificarRol);
            groupBox5.Controls.Add(btnAltaRol);
            groupBox5.Controls.Add(txtNombreRol);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(txtIDRol);
            groupBox5.Controls.Add(label4);
            groupBox5.Location = new Point(437, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(348, 276);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "Rol";
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
            // btnAltaRol
            // 
            btnAltaRol.Location = new Point(11, 55);
            btnAltaRol.Name = "btnAltaRol";
            btnAltaRol.Size = new Size(102, 39);
            btnAltaRol.TabIndex = 8;
            btnAltaRol.Text = "Alta";
            btnAltaRol.UseVisualStyleBackColor = true;
            // 
            // btnModificarRol
            // 
            btnModificarRol.Location = new Point(118, 55);
            btnModificarRol.Name = "btnModificarRol";
            btnModificarRol.Size = new Size(102, 39);
            btnModificarRol.TabIndex = 9;
            btnModificarRol.Text = "Modificar";
            btnModificarRol.UseVisualStyleBackColor = true;
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.Location = new Point(226, 56);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(102, 39);
            btnEliminarRol.TabIndex = 10;
            btnEliminarRol.Text = "Eliminar";
            btnEliminarRol.UseVisualStyleBackColor = true;
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
            groupBox6.Location = new Point(11, 100);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(331, 165);
            groupBox6.TabIndex = 11;
            groupBox6.TabStop = false;
            groupBox6.Text = "Rol para Asociar/Desasociar a otro Rol";
            // 
            // btnAsociarRolesUsuario
            // 
            btnAsociarRolesUsuario.Location = new Point(22, 100);
            btnAsociarRolesUsuario.Name = "btnAsociarRolesUsuario";
            btnAsociarRolesUsuario.Size = new Size(90, 50);
            btnAsociarRolesUsuario.TabIndex = 0;
            btnAsociarRolesUsuario.Text = "Asociar Roles a Usuario";
            btnAsociarRolesUsuario.UseVisualStyleBackColor = true;
            // 
            // btnQuitarRolesUsuario
            // 
            btnQuitarRolesUsuario.Location = new Point(156, 100);
            btnQuitarRolesUsuario.Name = "btnQuitarRolesUsuario";
            btnQuitarRolesUsuario.Size = new Size(90, 50);
            btnQuitarRolesUsuario.TabIndex = 1;
            btnQuitarRolesUsuario.Text = "Quitar Roles a Usuario";
            btnQuitarRolesUsuario.UseVisualStyleBackColor = true;
            // 
            // cmbRoles
            // 
            cmbRoles.FormattingEnabled = true;
            cmbRoles.Location = new Point(107, 25);
            cmbRoles.Name = "cmbRoles";
            cmbRoles.Size = new Size(210, 23);
            cmbRoles.TabIndex = 2;
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(131, 62);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 4;
            label7.Text = "Nombre";
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
            // txtIDRoles
            // 
            txtIDRoles.Location = new Point(32, 59);
            txtIDRoles.Name = "txtIDRoles";
            txtIDRoles.ReadOnly = true;
            txtIDRoles.Size = new Size(80, 23);
            txtIDRoles.TabIndex = 6;
            // 
            // txtNombreRoles
            // 
            txtNombreRoles.Location = new Point(188, 59);
            txtNombreRoles.Name = "txtNombreRoles";
            txtNombreRoles.ReadOnly = true;
            txtNombreRoles.Size = new Size(129, 23);
            txtNombreRoles.TabIndex = 12;
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
            groupBox7.Size = new Size(389, 172);
            groupBox7.TabIndex = 3;
            groupBox7.TabStop = false;
            groupBox7.Text = "Permisos";
            // 
            // txtNombrePermiso
            // 
            txtNombrePermiso.Location = new Point(254, 19);
            txtNombrePermiso.Name = "txtNombrePermiso";
            txtNombrePermiso.ReadOnly = true;
            txtNombrePermiso.Size = new Size(129, 23);
            txtNombrePermiso.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(179, 22);
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
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(182, 61);
            label12.Name = "label12";
            label12.Size = new Size(31, 15);
            label12.TabIndex = 15;
            label12.Text = "Item";
            // 
            // cmbItem
            // 
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(216, 58);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(167, 23);
            cmbItem.TabIndex = 14;
            // 
            // btnEliminarPermiso
            // 
            btnEliminarPermiso.Location = new Point(199, 105);
            btnEliminarPermiso.Name = "btnEliminarPermiso";
            btnEliminarPermiso.Size = new Size(90, 50);
            btnEliminarPermiso.TabIndex = 16;
            btnEliminarPermiso.Text = "Eliminar Permiso";
            btnEliminarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnAltaPermiso
            // 
            btnAltaPermiso.Location = new Point(52, 105);
            btnAltaPermiso.Name = "btnAltaPermiso";
            btnAltaPermiso.Size = new Size(90, 50);
            btnAltaPermiso.TabIndex = 17;
            btnAltaPermiso.Text = "Alta Permiso";
            btnAltaPermiso.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnQuitarPermisoRol);
            groupBox8.Controls.Add(btnAsociarPermisoRol);
            groupBox8.Location = new Point(802, 191);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(234, 100);
            groupBox8.TabIndex = 4;
            groupBox8.TabStop = false;
            groupBox8.Text = "Opciones Roles / Permisos";
            // 
            // btnAsociarPermisoRol
            // 
            btnAsociarPermisoRol.Location = new Point(11, 36);
            btnAsociarPermisoRol.Name = "btnAsociarPermisoRol";
            btnAsociarPermisoRol.Size = new Size(90, 50);
            btnAsociarPermisoRol.TabIndex = 18;
            btnAsociarPermisoRol.Text = "Asociar Permiso a Rol";
            btnAsociarPermisoRol.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermisoRol
            // 
            btnQuitarPermisoRol.Location = new Point(123, 36);
            btnQuitarPermisoRol.Name = "btnQuitarPermisoRol";
            btnQuitarPermisoRol.Size = new Size(90, 50);
            btnQuitarPermisoRol.TabIndex = 19;
            btnQuitarPermisoRol.Text = "Quitar Permiso a Rol";
            btnQuitarPermisoRol.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(1162, 185);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(90, 50);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "Limpiar Campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1162, 241);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(90, 50);
            btnSalir.TabIndex = 19;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(treeUsuarios);
            groupBox9.Location = new Point(12, 294);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(225, 349);
            groupBox9.TabIndex = 20;
            groupBox9.TabStop = false;
            groupBox9.Text = "Usuarios";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(treeRoles);
            groupBox10.Location = new Point(245, 297);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(232, 349);
            groupBox10.TabIndex = 21;
            groupBox10.TabStop = false;
            groupBox10.Text = "Roles";
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(treePermisos);
            groupBox11.Location = new Point(483, 297);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(234, 349);
            groupBox11.TabIndex = 22;
            groupBox11.TabStop = false;
            groupBox11.Text = "Permisos";
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(treePermisosRol);
            groupBox12.Location = new Point(723, 297);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(232, 349);
            groupBox12.TabIndex = 21;
            groupBox12.TabStop = false;
            groupBox12.Text = "Permisos Por Rol";
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(treeRolesPermisosUsuarios);
            groupBox13.Location = new Point(961, 297);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(249, 349);
            groupBox13.TabIndex = 21;
            groupBox13.TabStop = false;
            groupBox13.Text = "Roles y Permisos del Usuario";
            // 
            // treeUsuarios
            // 
            treeUsuarios.Location = new Point(6, 22);
            treeUsuarios.Name = "treeUsuarios";
            treeUsuarios.Size = new Size(213, 321);
            treeUsuarios.TabIndex = 0;
            // 
            // treeRoles
            // 
            treeRoles.Location = new Point(6, 22);
            treeRoles.Name = "treeRoles";
            treeRoles.Size = new Size(220, 321);
            treeRoles.TabIndex = 1;
            // 
            // treePermisos
            // 
            treePermisos.Location = new Point(6, 22);
            treePermisos.Name = "treePermisos";
            treePermisos.Size = new Size(222, 321);
            treePermisos.TabIndex = 1;
            // 
            // treePermisosRol
            // 
            treePermisosRol.Location = new Point(6, 22);
            treePermisosRol.Name = "treePermisosRol";
            treePermisosRol.Size = new Size(220, 321);
            treePermisosRol.TabIndex = 1;
            // 
            // treeRolesPermisosUsuarios
            // 
            treeRolesPermisosUsuarios.Location = new Point(6, 22);
            treeRolesPermisosUsuarios.Name = "treeRolesPermisosUsuarios";
            treeRolesPermisosUsuarios.Size = new Size(237, 321);
            treeRolesPermisosUsuarios.TabIndex = 1;
            // 
            // frmGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 655);
            Controls.Add(groupBox13);
            Controls.Add(groupBox12);
            Controls.Add(groupBox11);
            Controls.Add(groupBox10);
            Controls.Add(groupBox9);
            Controls.Add(btnSalir);
            Controls.Add(btnLimpiar);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox5);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmGestionUsuarios";
            Text = "Gestion de Usuarios";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
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
        private TextBox txtID;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
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
        private TreeView treeUsuarios;
        private GroupBox groupBox10;
        private TreeView treeRoles;
        private GroupBox groupBox11;
        private TreeView treePermisos;
        private GroupBox groupBox12;
        private TreeView treePermisosRol;
        private GroupBox groupBox13;
        private TreeView treeRolesPermisosUsuarios;
    }
}