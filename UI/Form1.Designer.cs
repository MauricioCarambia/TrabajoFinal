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
            reservasToolStripMenuItem = new ToolStripMenuItem();
            registrarClienteToolStripMenuItem = new ToolStripMenuItem();
            hacerReservaToolStripMenuItem = new ToolStripMenuItem();
            verReservaToolStripMenuItem = new ToolStripMenuItem();
            registrarReservaToolStripMenuItem = new ToolStripMenuItem();
            inventarioToolStripMenuItem = new ToolStripMenuItem();
            verInsumosToolStripMenuItem = new ToolStripMenuItem();
            verInsumosToolStripMenuItem1 = new ToolStripMenuItem();
            registrarInsumosToolStripMenuItem = new ToolStripMenuItem();
            registrarPlatoToolStripMenuItem = new ToolStripMenuItem();
            verPlatosToolStripMenuItem = new ToolStripMenuItem();
            registrarPlatoToolStripMenuItem1 = new ToolStripMenuItem();
            registrarPromocionToolStripMenuItem = new ToolStripMenuItem();
            registrarPromocionToolStripMenuItem1 = new ToolStripMenuItem();
            administrarPromocionToolStripMenuItem = new ToolStripMenuItem();
            registrarProveedorToolStripMenuItem = new ToolStripMenuItem();
            registrarMesasToolStripMenuItem = new ToolStripMenuItem();
            verMesasToolStripMenuItem = new ToolStripMenuItem();
            registrarMesasToolStripMenuItem1 = new ToolStripMenuItem();
            pedidosToolStripMenuItem = new ToolStripMenuItem();
            cargarPedidoToolStripMenuItem = new ToolStripMenuItem();
            cobrarToolStripMenuItem = new ToolStripMenuItem();
            pedidosToolStripMenuItem2 = new ToolStripMenuItem();
            cocinaToolStripMenuItem = new ToolStripMenuItem();
            pedidosToolStripMenuItem1 = new ToolStripMenuItem();
            dashBoardToolStripMenuItem = new ToolStripMenuItem();
            verToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { reservasToolStripMenuItem, inventarioToolStripMenuItem, pedidosToolStripMenuItem, cobrarToolStripMenuItem, cocinaToolStripMenuItem, dashBoardToolStripMenuItem, salirToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // reservasToolStripMenuItem
            // 
            reservasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { registrarClienteToolStripMenuItem, hacerReservaToolStripMenuItem });
            reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            reservasToolStripMenuItem.Size = new Size(64, 20);
            reservasToolStripMenuItem.Text = "Reservas";
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
            hacerReservaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verReservaToolStripMenuItem, registrarReservaToolStripMenuItem });
            hacerReservaToolStripMenuItem.Name = "hacerReservaToolStripMenuItem";
            hacerReservaToolStripMenuItem.Size = new Size(180, 22);
            hacerReservaToolStripMenuItem.Text = "Reserva";
            // 
            // verReservaToolStripMenuItem
            // 
            verReservaToolStripMenuItem.Name = "verReservaToolStripMenuItem";
            verReservaToolStripMenuItem.Size = new Size(163, 22);
            verReservaToolStripMenuItem.Text = "Ver Reserva";
            // 
            // registrarReservaToolStripMenuItem
            // 
            registrarReservaToolStripMenuItem.Name = "registrarReservaToolStripMenuItem";
            registrarReservaToolStripMenuItem.Size = new Size(163, 22);
            registrarReservaToolStripMenuItem.Text = "Registrar Reserva";
            // 
            // inventarioToolStripMenuItem
            // 
            inventarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verInsumosToolStripMenuItem, registrarPlatoToolStripMenuItem, registrarPromocionToolStripMenuItem, registrarProveedorToolStripMenuItem, registrarMesasToolStripMenuItem });
            inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            inventarioToolStripMenuItem.Size = new Size(72, 20);
            inventarioToolStripMenuItem.Text = "Inventario";
            // 
            // verInsumosToolStripMenuItem
            // 
            verInsumosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verInsumosToolStripMenuItem1, registrarInsumosToolStripMenuItem });
            verInsumosToolStripMenuItem.Name = "verInsumosToolStripMenuItem";
            verInsumosToolStripMenuItem.Size = new Size(180, 22);
            verInsumosToolStripMenuItem.Text = "Insumos";
            // 
            // verInsumosToolStripMenuItem1
            // 
            verInsumosToolStripMenuItem1.Name = "verInsumosToolStripMenuItem1";
            verInsumosToolStripMenuItem1.Size = new Size(168, 22);
            verInsumosToolStripMenuItem1.Text = "Ver Insumos";
            // 
            // registrarInsumosToolStripMenuItem
            // 
            registrarInsumosToolStripMenuItem.Name = "registrarInsumosToolStripMenuItem";
            registrarInsumosToolStripMenuItem.Size = new Size(168, 22);
            registrarInsumosToolStripMenuItem.Text = "Registrar Insumos";
            // 
            // registrarPlatoToolStripMenuItem
            // 
            registrarPlatoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verPlatosToolStripMenuItem, registrarPlatoToolStripMenuItem1 });
            registrarPlatoToolStripMenuItem.Name = "registrarPlatoToolStripMenuItem";
            registrarPlatoToolStripMenuItem.Size = new Size(180, 22);
            registrarPlatoToolStripMenuItem.Text = "Platos";
            // 
            // verPlatosToolStripMenuItem
            // 
            verPlatosToolStripMenuItem.Name = "verPlatosToolStripMenuItem";
            verPlatosToolStripMenuItem.Size = new Size(150, 22);
            verPlatosToolStripMenuItem.Text = "Ver platos";
            // 
            // registrarPlatoToolStripMenuItem1
            // 
            registrarPlatoToolStripMenuItem1.Name = "registrarPlatoToolStripMenuItem1";
            registrarPlatoToolStripMenuItem1.Size = new Size(150, 22);
            registrarPlatoToolStripMenuItem1.Text = "Registrar Plato";
            // 
            // registrarPromocionToolStripMenuItem
            // 
            registrarPromocionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { registrarPromocionToolStripMenuItem1, administrarPromocionToolStripMenuItem });
            registrarPromocionToolStripMenuItem.Name = "registrarPromocionToolStripMenuItem";
            registrarPromocionToolStripMenuItem.Size = new Size(180, 22);
            registrarPromocionToolStripMenuItem.Text = "Promociones";
            // 
            // registrarPromocionToolStripMenuItem1
            // 
            registrarPromocionToolStripMenuItem1.Name = "registrarPromocionToolStripMenuItem1";
            registrarPromocionToolStripMenuItem1.Size = new Size(198, 22);
            registrarPromocionToolStripMenuItem1.Text = "Registrar Promocion";
            // 
            // administrarPromocionToolStripMenuItem
            // 
            administrarPromocionToolStripMenuItem.Name = "administrarPromocionToolStripMenuItem";
            administrarPromocionToolStripMenuItem.Size = new Size(198, 22);
            administrarPromocionToolStripMenuItem.Text = "Administrar Promocion";
            // 
            // registrarProveedorToolStripMenuItem
            // 
            registrarProveedorToolStripMenuItem.Name = "registrarProveedorToolStripMenuItem";
            registrarProveedorToolStripMenuItem.Size = new Size(180, 22);
            registrarProveedorToolStripMenuItem.Text = "Registrar Proveedor";
            // 
            // registrarMesasToolStripMenuItem
            // 
            registrarMesasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verMesasToolStripMenuItem, registrarMesasToolStripMenuItem1 });
            registrarMesasToolStripMenuItem.Name = "registrarMesasToolStripMenuItem";
            registrarMesasToolStripMenuItem.Size = new Size(180, 22);
            registrarMesasToolStripMenuItem.Text = "Mesas";
            // 
            // verMesasToolStripMenuItem
            // 
            verMesasToolStripMenuItem.Name = "verMesasToolStripMenuItem";
            verMesasToolStripMenuItem.Size = new Size(156, 22);
            verMesasToolStripMenuItem.Text = "Ver Mesas";
            // 
            // registrarMesasToolStripMenuItem1
            // 
            registrarMesasToolStripMenuItem1.Name = "registrarMesasToolStripMenuItem1";
            registrarMesasToolStripMenuItem1.Size = new Size(156, 22);
            registrarMesasToolStripMenuItem1.Text = "Registrar Mesas";
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
            cargarPedidoToolStripMenuItem.Size = new Size(149, 22);
            cargarPedidoToolStripMenuItem.Text = "Cargar Pedido";
            // 
            // cobrarToolStripMenuItem
            // 
            cobrarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pedidosToolStripMenuItem2 });
            cobrarToolStripMenuItem.Name = "cobrarToolStripMenuItem";
            cobrarToolStripMenuItem.Size = new Size(55, 20);
            cobrarToolStripMenuItem.Text = "Cobrar";
            // 
            // pedidosToolStripMenuItem2
            // 
            pedidosToolStripMenuItem2.Name = "pedidosToolStripMenuItem2";
            pedidosToolStripMenuItem2.Size = new Size(116, 22);
            pedidosToolStripMenuItem2.Text = "Pedidos";
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
            verToolStripMenuItem.Size = new Size(90, 22);
            verToolStripMenuItem.Text = "Ver";
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(41, 20);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
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
        private ToolStripMenuItem cobrarToolStripMenuItem;
        private ToolStripMenuItem pedidosToolStripMenuItem2;
        private ToolStripMenuItem cocinaToolStripMenuItem;
        private ToolStripMenuItem pedidosToolStripMenuItem1;
        private ToolStripMenuItem dashBoardToolStripMenuItem;
        private ToolStripMenuItem verToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem verInsumosToolStripMenuItem;
        private ToolStripMenuItem verInsumosToolStripMenuItem1;
        private ToolStripMenuItem registrarInsumosToolStripMenuItem;
        private ToolStripMenuItem verPlatosToolStripMenuItem;
        private ToolStripMenuItem registrarPlatoToolStripMenuItem1;
        private ToolStripMenuItem registrarPromocionToolStripMenuItem1;
        private ToolStripMenuItem administrarPromocionToolStripMenuItem;
        private ToolStripMenuItem registrarMesasToolStripMenuItem;
        private ToolStripMenuItem verMesasToolStripMenuItem;
        private ToolStripMenuItem registrarMesasToolStripMenuItem1;
        private ToolStripMenuItem verReservaToolStripMenuItem;
        private ToolStripMenuItem registrarReservaToolStripMenuItem;
    }
}
