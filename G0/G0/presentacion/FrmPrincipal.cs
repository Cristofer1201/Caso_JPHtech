using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G0.presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);
        private void pbSalir_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("¿Esta seguro de salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (d == DialogResult.Yes) Application.Exit();
        }
        private void pbMinimizarT_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pbMinimizarT.Visible = false;
            pbMaximizarT.Visible = true;
        }
        private void pbMaximizarT_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pbMaximizarT.Visible = false;
            pbMinimizarT.Visible = true;
        }
        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void productosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmProducto nuevo = new FrmProducto();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void clientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmCliente nuevo = new FrmCliente();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void trabajadoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmTrabajador nuevo = new FrmTrabajador();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void proveedoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmProovedor nuevo = new FrmProovedor();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void registrosDeCompraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmReg_Com nuevo = new FrmReg_Com();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void registrosDeVentaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmReg_Ven nuevo = new FrmReg_Ven();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void kardexToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmKardex nuevo = new FrmKardex();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void listadoDeVentasMensualesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmListaVentasMensual nuevo = new FrmListaVentasMensual();
            nuevo.MdiParent = this;
            nuevo.Show();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
