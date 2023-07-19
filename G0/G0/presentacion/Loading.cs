using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G0.presentacion
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pBarCarga.Increment(4);
            lblCarga.Text = pBarCarga.Value.ToString() + "%";
            if (pBarCarga.Value == pBarCarga.Maximum)
            {
                timer1.Stop();
                this.Hide();
                FrmPrincipal nuevo = new FrmPrincipal();
                nuevo.Show();
            }
        }

        private void pBarCarga_Click(object sender, EventArgs e)
        {

        }
    }
}
