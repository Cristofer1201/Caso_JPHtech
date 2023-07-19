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
using G0.datos;
using G0.controlador;
using System.Xml.Serialization;
using System.IO;

namespace G0.presentacion
{
    public partial class FrmTrabajador : Form
    {
        public Lista_Trabajador listaT = new Lista_Trabajador();
        public FrmTrabajador()
        {
            InitializeComponent();
            if (File.Exists("Trabajadores.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_Trabajador));
                FileStream lector = File.OpenRead("Trabajadores.xml");
                Lista_Trabajador listaAux = (Lista_Trabajador)serializador.Deserialize(lector);
                lector.Close();
                Nodo_Trabajador p = listaAux.ini;
                while (listaAux.contar() != 0)
                {
                    listaT.insertar(listaAux.extraerPrimero());
                }
            }
            listaT.imprimir(dgvLista);
            decorarDGV();
            estadoDeBotones();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);
        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pbMinimizarT_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pbMinimizarT.Visible = false;
            pbMaximizarT.Visible = true;
        }
        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pbMaximizarT_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pbMaximizarT.Visible = false;
            pbMinimizarT.Visible = true;
        }
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void estadoDeBotones()
        {
            if (listaT.contar() == 0)
            {
                tsbInsertar.Enabled = true;
                tsbModificar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = false;
            }
            if (listaT.contar() > 0)
            {
                tsbInsertar.Enabled = tsbModificar.Enabled =
                    tsbEliminar.Enabled = tsbImprimir.Enabled = tsbBuscar.Enabled = true;
            }
        }
        private bool validarDNI(TextBox txt)
        {
            errorProvider1.Clear();
            if ((txt.Text == "") || (txt.Text.Length != 8))
            {
                errorProvider1.SetError(txt, "Ingrese un DNI válido (8 caractéres).");
                txt.Focus();
                return false;
            }
            return true;
        }
        private bool validarDatos()
        {
            errorProvider1.Clear();
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Ingrese un nombre válido");
                txtNombre.Focus();
                return false;
            }
            if ((txtDNI.Text == "") || (txtDNI.Text.Length != 8))
            {
                errorProvider1.SetError(txtDNI, "Ingrese un DNI válido (8 caractéres).");
                txtDNI.Focus();
                return false;
            }
            int num1;
            if (!int.TryParse(txtEdad.Text, out num1) || num1 < 0 || num1 > 70)
            {
                errorProvider1.SetError(txtEdad, "Ingrese una edad válida.");
                txtEdad.Focus();
                return false;
            }
            if (txtDireccion.Text == "")
            {
                errorProvider1.SetError(txtDireccion, "Ingrese una dirección.");
                txtDireccion.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            errorProvider1.Clear();
            txtDNI.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtEdad.Clear();

            txtModificar.Clear();
            txtEliminar.Clear();
            txtBuscar.Clear();
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < listaT.contar())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(90, 135, 6);
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(100, 150, 6);
                i++;
            }
        }
        public void archivo()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_Trabajador));
            TextWriter escribir = new StreamWriter("Trabajadores.xml");
            serializador.Serialize(escribir, listaT);
            escribir.Close();
        }

        private void tsbInsertar_Click(object sender, EventArgs e)
        {
            if (!validarDatos()) return;
            Trabajador nuevo = new Trabajador(txtNombre.Text, int.Parse(txtDNI.Text), int.Parse(txtEdad.Text), txtDireccion.Text, dtpFechaContrato.Value);
            if (!listaT.existeTrabajador(nuevo))
            {
                listaT.insertar(nuevo);
                listaT.imprimir(dgvLista);
                decorarDGV();;
                archivo();
                estadoDeBotones();
            }
            else MessageBox.Show("El trabajador ya existe en la lista, ingrese un DNI diferente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (!validarDNI(txtModificar)) return;
            if (listaT.buscar(int.Parse(txtModificar.Text)) != null)
            {
                int DNIantiguo = int.Parse(txtModificar.Text);
                if (validarDatos())
                {
                    Trabajador nuevo = new Trabajador(txtNombre.Text, int.Parse(txtDNI.Text), int.Parse(txtEdad.Text), txtDireccion.Text,dtpFechaContrato.Value);
                    if (listaT.modificar(DNIantiguo, nuevo))
                    {
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listaT.imprimir(dgvLista);
                        decorarDGV();
                        archivo();
                        limpiar();
                    }
                    else MessageBox.Show("El trabajador que intenta modificar ya existe, ingrese un DNI diferente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("El trabajador no existe, ingrese otro DNI en la sección Modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (!validarDNI(txtEliminar)) return;
            if (listaT.eliminar(int.Parse(txtEliminar.Text)))
            {
                MessageBox.Show("Se eliminó correctamente", "Eliminación exitosa", MessageBoxButtons.OK);
                txtEliminar.Clear();
                listaT.imprimir(dgvLista);
                decorarDGV();
                archivo();
                estadoDeBotones();
            }
            else MessageBox.Show("El trabajador no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            listaT.imprimir(dgvLista);
            decorarDGV();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!validarDNI(txtBuscar)) return;
            if (listaT.buscar(int.Parse(txtBuscar.Text)) != null)
            {
                Trabajador encontrado = listaT.buscar(int.Parse(txtBuscar.Text));
                MessageBox.Show("DNI: " + encontrado.DNI +
                    "\nNombre: " + encontrado.nombre +
                    "\nEdad: " + encontrado.edad +
                    "\nDirección: " + encontrado.direccion,
                    "Trabajador encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El Trabajador no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            txtBuscar.Clear();
        }

        private void tsbLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void tsbAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Insertar: Guarda los datos insertados en las casillas, asegúrese de colocar los datos de manera correcta en la sección Datos." +
                "\n\nModificar: Modifica los datos del trabajador especificado, ingrese el DNI en la sección Modificar y los nuevos datos en la sección Datos." +
                "\n\nEliminar: Elimina los datos del trabajador especificado, ingrese el DNI en la sección Eliminar." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los DNI ingresados y devuelve los datos del trabajador, ingrese el DNI en la sección Buscar." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
