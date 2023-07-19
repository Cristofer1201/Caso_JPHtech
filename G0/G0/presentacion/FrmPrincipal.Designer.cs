namespace G0.presentacion
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pbMaximizarT = new System.Windows.Forms.PictureBox();
            this.pbMinimizarT = new System.Windows.Forms.PictureBox();
            this.pbMinimizar = new System.Windows.Forms.PictureBox();
            this.pbSalir = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mantenimientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trabajadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrosDeCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrosDeVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kardexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listadoDeVentasMensualesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaximizarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizarT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBarraTitulo
            // 
            this.panelBarraTitulo.BackColor = System.Drawing.Color.Black;
            this.panelBarraTitulo.Controls.Add(this.label6);
            this.panelBarraTitulo.Controls.Add(this.pbMaximizarT);
            this.panelBarraTitulo.Controls.Add(this.pbMinimizarT);
            this.panelBarraTitulo.Controls.Add(this.pbMinimizar);
            this.panelBarraTitulo.Controls.Add(this.pbSalir);
            this.panelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelBarraTitulo.Name = "panelBarraTitulo";
            this.panelBarraTitulo.Size = new System.Drawing.Size(882, 40);
            this.panelBarraTitulo.TabIndex = 42;
            this.panelBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarraTitulo_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(26, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Menú";
            // 
            // pbMaximizarT
            // 
            this.pbMaximizarT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMaximizarT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMaximizarT.Image = ((System.Drawing.Image)(resources.GetObject("pbMaximizarT.Image")));
            this.pbMaximizarT.Location = new System.Drawing.Point(806, 6);
            this.pbMaximizarT.Name = "pbMaximizarT";
            this.pbMaximizarT.Size = new System.Drawing.Size(25, 25);
            this.pbMaximizarT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMaximizarT.TabIndex = 7;
            this.pbMaximizarT.TabStop = false;
            this.pbMaximizarT.Click += new System.EventHandler(this.pbMaximizarT_Click);
            // 
            // pbMinimizarT
            // 
            this.pbMinimizarT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinimizarT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinimizarT.Image = ((System.Drawing.Image)(resources.GetObject("pbMinimizarT.Image")));
            this.pbMinimizarT.Location = new System.Drawing.Point(806, 6);
            this.pbMinimizarT.Name = "pbMinimizarT";
            this.pbMinimizarT.Size = new System.Drawing.Size(25, 25);
            this.pbMinimizarT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMinimizarT.TabIndex = 6;
            this.pbMinimizarT.TabStop = false;
            this.pbMinimizarT.Click += new System.EventHandler(this.pbMinimizarT_Click);
            // 
            // pbMinimizar
            // 
            this.pbMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pbMinimizar.Image")));
            this.pbMinimizar.Location = new System.Drawing.Point(766, 6);
            this.pbMinimizar.Name = "pbMinimizar";
            this.pbMinimizar.Size = new System.Drawing.Size(25, 25);
            this.pbMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMinimizar.TabIndex = 5;
            this.pbMinimizar.TabStop = false;
            this.pbMinimizar.Click += new System.EventHandler(this.pbMinimizar_Click);
            // 
            // pbSalir
            // 
            this.pbSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSalir.Image = ((System.Drawing.Image)(resources.GetObject("pbSalir.Image")));
            this.pbSalir.Location = new System.Drawing.Point(846, 6);
            this.pbSalir.Name = "pbSalir";
            this.pbSalir.Size = new System.Drawing.Size(25, 25);
            this.pbSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSalir.TabIndex = 4;
            this.pbSalir.TabStop = false;
            this.pbSalir.Click += new System.EventHandler(this.pbSalir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mantenimientoToolStripMenuItem,
            this.registrosToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 40);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(148, 804);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mantenimientoToolStripMenuItem
            // 
            this.mantenimientoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.trabajadoresToolStripMenuItem,
            this.proveedoresToolStripMenuItem});
            this.mantenimientoToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mantenimientoToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.mantenimientoToolStripMenuItem.Name = "mantenimientoToolStripMenuItem";
            this.mantenimientoToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.mantenimientoToolStripMenuItem.Text = "Mantenimiento";
            // 
            // productosToolStripMenuItem
            // 
            this.productosToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.productosToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productosToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.productosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("productosToolStripMenuItem.Image")));
            this.productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            this.productosToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.productosToolStripMenuItem.Text = "Productos";
            this.productosToolStripMenuItem.Click += new System.EventHandler(this.productosToolStripMenuItem_Click_1);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.clientesToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.clientesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clientesToolStripMenuItem.Image")));
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click_1);
            // 
            // trabajadoresToolStripMenuItem
            // 
            this.trabajadoresToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(230)))), ((int)(((byte)(10)))));
            this.trabajadoresToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trabajadoresToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.trabajadoresToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("trabajadoresToolStripMenuItem.Image")));
            this.trabajadoresToolStripMenuItem.Name = "trabajadoresToolStripMenuItem";
            this.trabajadoresToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.trabajadoresToolStripMenuItem.Text = "Trabajadores";
            this.trabajadoresToolStripMenuItem.Click += new System.EventHandler(this.trabajadoresToolStripMenuItem_Click_1);
            // 
            // proveedoresToolStripMenuItem
            // 
            this.proveedoresToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this.proveedoresToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proveedoresToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.proveedoresToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("proveedoresToolStripMenuItem.Image")));
            this.proveedoresToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            this.proveedoresToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.proveedoresToolStripMenuItem.Text = "Proveedores";
            this.proveedoresToolStripMenuItem.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click_1);
            // 
            // registrosToolStripMenuItem
            // 
            this.registrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrosDeCompraToolStripMenuItem,
            this.registrosDeVentaToolStripMenuItem});
            this.registrosToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrosToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.registrosToolStripMenuItem.Name = "registrosToolStripMenuItem";
            this.registrosToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.registrosToolStripMenuItem.Text = "Registros";
            // 
            // registrosDeCompraToolStripMenuItem
            // 
            this.registrosDeCompraToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.registrosDeCompraToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrosDeCompraToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.registrosDeCompraToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registrosDeCompraToolStripMenuItem.Image")));
            this.registrosDeCompraToolStripMenuItem.Name = "registrosDeCompraToolStripMenuItem";
            this.registrosDeCompraToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.registrosDeCompraToolStripMenuItem.Text = "Registros de Compra";
            this.registrosDeCompraToolStripMenuItem.Click += new System.EventHandler(this.registrosDeCompraToolStripMenuItem_Click_1);
            // 
            // registrosDeVentaToolStripMenuItem
            // 
            this.registrosDeVentaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(120)))), ((int)(((byte)(8)))));
            this.registrosDeVentaToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrosDeVentaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.registrosDeVentaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registrosDeVentaToolStripMenuItem.Image")));
            this.registrosDeVentaToolStripMenuItem.Name = "registrosDeVentaToolStripMenuItem";
            this.registrosDeVentaToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.registrosDeVentaToolStripMenuItem.Text = "Registros de Venta";
            this.registrosDeVentaToolStripMenuItem.Click += new System.EventHandler(this.registrosDeVentaToolStripMenuItem_Click_1);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kardexToolStripMenuItem,
            this.listadoDeVentasMensualesToolStripMenuItem});
            this.reportesToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportesToolStripMenuItem.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.reportesToolStripMenuItem.Text = "Reportes";
            this.reportesToolStripMenuItem.Click += new System.EventHandler(this.reportesToolStripMenuItem_Click);
            // 
            // kardexToolStripMenuItem
            // 
            this.kardexToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(150)))), ((int)(((byte)(179)))));
            this.kardexToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kardexToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.kardexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("kardexToolStripMenuItem.Image")));
            this.kardexToolStripMenuItem.Name = "kardexToolStripMenuItem";
            this.kardexToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.kardexToolStripMenuItem.Text = "Kardex";
            this.kardexToolStripMenuItem.Click += new System.EventHandler(this.kardexToolStripMenuItem_Click_1);
            // 
            // listadoDeVentasMensualesToolStripMenuItem
            // 
            this.listadoDeVentasMensualesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(177)))), ((int)(((byte)(29)))));
            this.listadoDeVentasMensualesToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listadoDeVentasMensualesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.listadoDeVentasMensualesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("listadoDeVentasMensualesToolStripMenuItem.Image")));
            this.listadoDeVentasMensualesToolStripMenuItem.Name = "listadoDeVentasMensualesToolStripMenuItem";
            this.listadoDeVentasMensualesToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.listadoDeVentasMensualesToolStripMenuItem.Text = "Listado de ventas mensuales";
            this.listadoDeVentasMensualesToolStripMenuItem.Click += new System.EventHandler(this.listadoDeVentasMensualesToolStripMenuItem_Click_1);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(882, 844);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelBarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "FrmPrincipal";
            this.Text = "Comercialización de electrodomésticos";
            this.panelBarraTitulo.ResumeLayout(false);
            this.panelBarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaximizarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizarT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbMaximizarT;
        private System.Windows.Forms.PictureBox pbMinimizarT;
        private System.Windows.Forms.PictureBox pbMinimizar;
        private System.Windows.Forms.PictureBox pbSalir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mantenimientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kardexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listadoDeVentasMensualesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trabajadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrosDeCompraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrosDeVentaToolStripMenuItem;

    }
}