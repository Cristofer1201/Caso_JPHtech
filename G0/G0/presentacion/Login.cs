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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            lblEstado.Text = "";
        }

        private string Codigo = "JPH1201";
        private string Password = "Electro2021";

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);  
        private void pbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "Usuario")
            {
                txtCodigo.Text = "";
                txtCodigo.ForeColor = Color.White;
            }
        }
        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                txtCodigo.Text = "Usuario";
                txtCodigo.ForeColor = Color.Gray;
            }
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Contraseña")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.White;
                txtPassword.UseSystemPasswordChar = true;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Contraseña";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == Codigo && txtPassword.Text == Password)
            {
                Loading carga = new Loading();
                carga.Show();
                this.Hide();
            }
            else lblEstado.Text = "El usuario no existe.";
        }

    }
}
