namespace UI
{
    partial class frmMDI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            inicioToolStripMenuItem = new ToolStripMenuItem();
            loginToolStripMenuItem = new ToolStripMenuItem();
            logOutToolStripMenuItem = new ToolStripMenuItem();
            reservasToolStripMenuItem = new ToolStripMenuItem();
            registrarClienteToolStripMenuItem = new ToolStripMenuItem();
            hacerReservaToolStripMenuItem = new ToolStripMenuItem();
            cobrarPedidoToolStripMenuItem = new ToolStripMenuItem();
            inventarioToolStripMenuItem = new ToolStripMenuItem();
            verInsumosToolStripMenuItem = new ToolStripMenuItem();
            registrarPlatoToolStripMenuItem = new ToolStripMenuItem();
            registrarPromocionToolStripMenuItem = new ToolStripMenuItem();
            registrarProveedorToolStripMenuItem = new ToolStripMenuItem();
            registrarMesasToolStripMenuItem = new ToolStripMenuItem();
            pedidosToolStripMenuItem = new ToolStripMenuItem();
            cargarPedidoToolStripMenuItem = new ToolStripMenuItem();
            cocinaToolStripMenuItem = new ToolStripMenuItem();
            pedidosToolStripMenuItem1 = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            crearUsuarioToolStripMenuItem = new ToolStripMenuItem();
            permisosRolesUsuariosToolStripMenuItem = new ToolStripMenuItem();
            dashBoardToolStripMenuItem = new ToolStripMenuItem();
            verToolStripMenuItem = new ToolStripMenuItem();
            bitacoraToolStripMenuItem = new ToolStripMenuItem();
            verBitacoraToolStripMenuItem = new ToolStripMenuItem();
            hacerBackupToolStripMenuItem = new ToolStripMenuItem();
            haverRestoreToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { inicioToolStripMenuItem, reservasToolStripMenuItem, inventarioToolStripMenuItem, pedidosToolStripMenuItem, cocinaToolStripMenuItem, usuariosToolStripMenuItem, dashBoardToolStripMenuItem, bitacoraToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // inicioToolStripMenuItem
            // 
            inicioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loginToolStripMenuItem, logOutToolStripMenuItem });
            inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            inicioToolStripMenuItem.Size = new Size(48, 20);
            inicioToolStripMenuItem.Text = "Inicio";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(119, 22);
            loginToolStripMenuItem.Text = "Log-In";
            // 
            // logOutToolStripMenuItem
            // 
            logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            logOutToolStripMenuItem.Size = new Size(119, 22);
            logOutToolStripMenuItem.Text = "Log-Out";
            logOutToolStripMenuItem.Click += logOutToolStripMenuItem_Click;
            // 
            // reservasToolStripMenuItem
            // 
            reservasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { registrarClienteToolStripMenuItem, hacerReservaToolStripMenuItem, cobrarPedidoToolStripMenuItem });
            reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            reservasToolStripMenuItem.Size = new Size(95, 20);
            reservasToolStripMenuItem.Text = "Administrador";
            // 
            // registrarClienteToolStripMenuItem
            // 
            registrarClienteToolStripMenuItem.Name = "registrarClienteToolStripMenuItem";
            registrarClienteToolStripMenuItem.Size = new Size(180, 22);
            registrarClienteToolStripMenuItem.Text = "Clientes";
            registrarClienteToolStripMenuItem.Click += registrarClienteToolStripMenuItem_Click;
            // 
            // hacerReservaToolStripMenuItem
            // 
            hacerReservaToolStripMenuItem.Name = "hacerReservaToolStripMenuItem";
            hacerReservaToolStripMenuItem.Size = new Size(180, 22);
            hacerReservaToolStripMenuItem.Text = "Reserva";
            hacerReservaToolStripMenuItem.Click += hacerReservaToolStripMenuItem_Click;
            // 
            // cobrarPedidoToolStripMenuItem
            // 
            cobrarPedidoToolStripMenuItem.Name = "cobrarPedidoToolStripMenuItem";
            cobrarPedidoToolStripMenuItem.Size = new Size(180, 22);
            cobrarPedidoToolStripMenuItem.Text = "Cobrar Pedido";
            cobrarPedidoToolStripMenuItem.Click += cobrarPedidoToolStripMenuItem_Click;
            // 
            // inventarioToolStripMenuItem
            // 
            inventarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verInsumosToolStripMenuItem, registrarPlatoToolStripMenuItem, registrarPromocionToolStripMenuItem, registrarProveedorToolStripMenuItem, registrarMesasToolStripMenuItem });
            inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            inventarioToolStripMenuItem.Size = new Size(60, 20);
            inventarioToolStripMenuItem.Text = "Gerente";
            // 
            // verInsumosToolStripMenuItem
            // 
            verInsumosToolStripMenuItem.Name = "verInsumosToolStripMenuItem";
            verInsumosToolStripMenuItem.Size = new Size(180, 22);
            verInsumosToolStripMenuItem.Text = "Insumos";
            verInsumosToolStripMenuItem.Click += verInsumosToolStripMenuItem_Click;
            // 
            // registrarPlatoToolStripMenuItem
            // 
            registrarPlatoToolStripMenuItem.Name = "registrarPlatoToolStripMenuItem";
            registrarPlatoToolStripMenuItem.Size = new Size(180, 22);
            registrarPlatoToolStripMenuItem.Text = "Platos";
            registrarPlatoToolStripMenuItem.Click += registrarPlatoToolStripMenuItem_Click;
            // 
            // registrarPromocionToolStripMenuItem
            // 
            registrarPromocionToolStripMenuItem.Name = "registrarPromocionToolStripMenuItem";
            registrarPromocionToolStripMenuItem.Size = new Size(180, 22);
            registrarPromocionToolStripMenuItem.Text = "Promociones";
            // 
            // registrarProveedorToolStripMenuItem
            // 
            registrarProveedorToolStripMenuItem.Name = "registrarProveedorToolStripMenuItem";
            registrarProveedorToolStripMenuItem.Size = new Size(180, 22);
            registrarProveedorToolStripMenuItem.Text = "Proveedor";
            // 
            // registrarMesasToolStripMenuItem
            // 
            registrarMesasToolStripMenuItem.Name = "registrarMesasToolStripMenuItem";
            registrarMesasToolStripMenuItem.Size = new Size(180, 22);
            registrarMesasToolStripMenuItem.Text = "Mesas";
            registrarMesasToolStripMenuItem.Click += registrarMesasToolStripMenuItem_Click;
            // 
            // pedidosToolStripMenuItem
            // 
            pedidosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargarPedidoToolStripMenuItem });
            pedidosToolStripMenuItem.Name = "pedidosToolStripMenuItem";
            pedidosToolStripMenuItem.Size = new Size(49, 20);
            pedidosToolStripMenuItem.Text = "Mozo";
            pedidosToolStripMenuItem.Click += pedidosToolStripMenuItem_Click;
            // 
            // cargarPedidoToolStripMenuItem
            // 
            cargarPedidoToolStripMenuItem.Name = "cargarPedidoToolStripMenuItem";
            cargarPedidoToolStripMenuItem.Size = new Size(180, 22);
            cargarPedidoToolStripMenuItem.Text = "Cargar Pedido";
            // 
            // cocinaToolStripMenuItem
            // 
            cocinaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pedidosToolStripMenuItem1 });
            cocinaToolStripMenuItem.Name = "cocinaToolStripMenuItem";
            cocinaToolStripMenuItem.Size = new Size(56, 20);
            cocinaToolStripMenuItem.Text = "Cocina";
            // 
            // pedidosToolStripMenuItem1
            // 
            pedidosToolStripMenuItem1.Name = "pedidosToolStripMenuItem1";
            pedidosToolStripMenuItem1.Size = new Size(116, 22);
            pedidosToolStripMenuItem1.Text = "Pedidos";
            pedidosToolStripMenuItem1.Click += pedidosToolStripMenuItem1_Click;
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { crearUsuarioToolStripMenuItem, permisosRolesUsuariosToolStripMenuItem });
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(64, 20);
            usuariosToolStripMenuItem.Text = "Usuarios";
            // 
            // crearUsuarioToolStripMenuItem
            // 
            crearUsuarioToolStripMenuItem.Name = "crearUsuarioToolStripMenuItem";
            crearUsuarioToolStripMenuItem.Size = new Size(172, 22);
            crearUsuarioToolStripMenuItem.Text = "Crear Usuario";
            crearUsuarioToolStripMenuItem.Click += crearUsuarioToolStripMenuItem_Click;
            // 
            // permisosRolesUsuariosToolStripMenuItem
            // 
            permisosRolesUsuariosToolStripMenuItem.Name = "permisosRolesUsuariosToolStripMenuItem";
            permisosRolesUsuariosToolStripMenuItem.Size = new Size(172, 22);
            permisosRolesUsuariosToolStripMenuItem.Text = "Gestionar Usuarios";
            permisosRolesUsuariosToolStripMenuItem.Click += permisosRolesUsuariosToolStripMenuItem_Click;
            // 
            // dashBoardToolStripMenuItem
            // 
            dashBoardToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verToolStripMenuItem });
            dashBoardToolStripMenuItem.Name = "dashBoardToolStripMenuItem";
            dashBoardToolStripMenuItem.Size = new Size(76, 20);
            dashBoardToolStripMenuItem.Text = "DashBoard";
            // 
            // verToolStripMenuItem
            // 
            verToolStripMenuItem.Name = "verToolStripMenuItem";
            verToolStripMenuItem.Size = new Size(135, 22);
            verToolStripMenuItem.Text = "Ver Informe";
            verToolStripMenuItem.Click += verToolStripMenuItem_Click;
            // 
            // bitacoraToolStripMenuItem
            // 
            bitacoraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verBitacoraToolStripMenuItem, hacerBackupToolStripMenuItem, haverRestoreToolStripMenuItem });
            bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            bitacoraToolStripMenuItem.Size = new Size(62, 20);
            bitacoraToolStripMenuItem.Text = "Bitacora";
            // 
            // verBitacoraToolStripMenuItem
            // 
            verBitacoraToolStripMenuItem.Name = "verBitacoraToolStripMenuItem";
            verBitacoraToolStripMenuItem.Size = new Size(147, 22);
            verBitacoraToolStripMenuItem.Text = "Ver Bitacora";
            verBitacoraToolStripMenuItem.Click += verBitacoraToolStripMenuItem_Click;
            // 
            // hacerBackupToolStripMenuItem
            // 
            hacerBackupToolStripMenuItem.Name = "hacerBackupToolStripMenuItem";
            hacerBackupToolStripMenuItem.Size = new Size(147, 22);
            hacerBackupToolStripMenuItem.Text = "Hacer Backup";
            hacerBackupToolStripMenuItem.Click += hacerBackupToolStripMenuItem_Click;
            // 
            // haverRestoreToolStripMenuItem
            // 
            haverRestoreToolStripMenuItem.Name = "haverRestoreToolStripMenuItem";
            haverRestoreToolStripMenuItem.Size = new Size(147, 22);
            haverRestoreToolStripMenuItem.Text = "Hacer Restore";
            haverRestoreToolStripMenuItem.Click += haverRestoreToolStripMenuItem_Click;
            // 
            // frmMDI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "frmMDI";
            Text = "Inicio";
            WindowState = FormWindowState.Maximized;
            Load += frmMDI_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem reservasToolStripMenuItem;
        private ToolStripMenuItem registrarClienteToolStripMenuItem;
        private ToolStripMenuItem hacerReservaToolStripMenuItem;
        private ToolStripMenuItem inventarioToolStripMenuItem;
        private ToolStripMenuItem registrarPlatoToolStripMenuItem;
        private ToolStripMenuItem registrarPromocionToolStripMenuItem;
        private ToolStripMenuItem registrarProveedorToolStripMenuItem;
        private ToolStripMenuItem pedidosToolStripMenuItem;
        private ToolStripMenuItem cargarPedidoToolStripMenuItem;
        private ToolStripMenuItem cocinaToolStripMenuItem;
        private ToolStripMenuItem pedidosToolStripMenuItem1;
        private ToolStripMenuItem dashBoardToolStripMenuItem;
        private ToolStripMenuItem verToolStripMenuItem;
        private ToolStripMenuItem verInsumosToolStripMenuItem;
        private ToolStripMenuItem registrarMesasToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem crearUsuarioToolStripMenuItem;
        private ToolStripMenuItem inicioToolStripMenuItem;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem logOutToolStripMenuItem;
        private ToolStripMenuItem permisosRolesUsuariosToolStripMenuItem;
        private ToolStripMenuItem bitacoraToolStripMenuItem;
        private ToolStripMenuItem verBitacoraToolStripMenuItem;
        private ToolStripMenuItem hacerBackupToolStripMenuItem;
        private ToolStripMenuItem haverRestoreToolStripMenuItem;
        private ToolStripMenuItem cobrarPedidoToolStripMenuItem;
    }
}
