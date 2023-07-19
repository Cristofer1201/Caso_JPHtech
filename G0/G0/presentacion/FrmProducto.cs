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
using G0.controlador;
using G0.datos;
using System.Xml.Serialization;
using System.IO;

namespace G0.presentacion
{
    public partial class FrmProducto : Form
    {
        Lista_Producto ListaP = new Lista_Producto();
        public FrmProducto()
        {
            InitializeComponent();
            cboNombre.DropDownStyle = ComboBoxStyle.DropDownList;
            if (File.Exists("Productos.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_Producto));
                FileStream lector = File.OpenRead("Productos.xml");
                Lista_Producto listaAux = (Lista_Producto)serializador.Deserialize(lector);
                lector.Close();
                Nodo_Producto p = listaAux.ini;
                while (listaAux.contar() != 0)
                {
                    ListaP.insertar(listaAux.extraerPrimero());
                }
            }
            ListaP.imprimir(dgvLista);
            decorarDGV();
            lblValorizacion.Text = ListaP.valorizacion().ToString("C");
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
        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void estadoDeBotones()
        {
            if (ListaP.contar() == 0)
            {
                tsbInsertar.Enabled = true;
                tsbModificar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = false;
            }
            if ((ListaP.contar() > 0) && (ListaP.contar() < cboNombre.Items.Count))
            {
                tsbInsertar.Enabled = tsbModificar.Enabled =
                    tsbEliminar.Enabled = tsbImprimir.Enabled = tsbBuscar.Enabled = true;
            }
            if (ListaP.contar() == cboNombre.Items.Count)
            {
                tsbInsertar.Enabled = false;
                tsbModificar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = true;
            }
        }
        private bool validarCodigo(TextBox txt)
        {
            errorProvider1.Clear();
            if ((txt.Text == "") || (txt.Text.Length != 8))
            {
                errorProvider1.SetError(txt, "Ingrese un código válido de 8 caractéres.");
                txt.Focus();
                return false;
            }
            return true;
        }
        private bool validarDatos()
        {
            errorProvider1.Clear();
            if ((txtCodigo.Text == "") || (txtCodigo.Text.Length != 8))
            {
                errorProvider1.SetError(txtCodigo, "Ingrese un código válido de 8 caractéres.");
                txtCodigo.Focus();
                return false;
            }
            if (cboNombre.Text == "")
            {
                errorProvider1.SetError(cboNombre, "Seleccione un nombre de producto válido.");
                cboNombre.Focus();
                return false;
            }
            if (txtProveedor.Text == "")
            {
                errorProvider1.SetError(txtProveedor, "Ingrese un nombre válido");
                txtProveedor.Focus();
                return false;
            }
            int num2;
            if (!int.TryParse(txtPrecio.Text, out num2) || num2 < 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio válido.");
                txtPrecio.Focus();
                return false;
            }
            int num3;
            if (!int.TryParse(txtStock.Text, out num3) || num3 < 0)
            {
                errorProvider1.SetError(txtStock, "Ingrese una cantidad válida.");
                txtStock.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            errorProvider1.Clear();
            txtCodigo.Clear();
            cboNombre.Text = "";
            txtProveedor.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            txtModificar.Clear();
            txtEliminar.Clear();
            txtBuscar.Clear();
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < ListaP.contar())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(80, 130, 130);
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(65, 115, 115);
                i++;
            }
        }
        private void total()
        {
            lblValorizacion.Text = ListaP.valorizacion().ToString("C");
        }
        public void archivo()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_Producto));
            TextWriter escribir = new StreamWriter("Productos.xml");
            serializador.Serialize(escribir, ListaP);
            escribir.Close();
        }

        private void tsbInsertar_Click(object sender, EventArgs e)
        {
            if (!validarDatos()) return;
            Producto nuevo = new Producto(txtCodigo.Text, cboNombre.Text, txtProveedor.Text,
                Convert.ToDouble(txtPrecio.Text), int.Parse(txtStock.Text));
            if (!ListaP.existeProducto(nuevo))
            {
                ListaP.insertar(nuevo);
                ListaP.imprimir(dgvLista);
                decorarDGV();
                total();
                archivo();
                estadoDeBotones();
            }
            else MessageBox.Show("El producto ya existe en la lista, ingrese un código y nombre de producto diferente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (!validarCodigo(txtModificar)) return;
            if (ListaP.buscar(txtModificar.Text) != null)
            {
                string codigoAntiguo = txtModificar.Text;
                if (validarDatos())
                {
                    Producto nuevo = new Producto(txtCodigo.Text, cboNombre.Text, txtProveedor.Text,
                        Convert.ToDouble(txtPrecio.Text), int.Parse(txtStock.Text));
                    if (ListaP.modificar(codigoAntiguo, nuevo))
                    {
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaP.imprimir(dgvLista);
                        decorarDGV();
                        total();
                        archivo();
                        limpiar();
                    }
                    else MessageBox.Show("El producto que intenta modificar ya existe, ingrese un código y nombre de producto diferente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("El producto no existe, ingrese otro código en la sección Modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (!validarCodigo(txtEliminar)) return;
            if (ListaP.eliminar(txtEliminar.Text))
            {
                txtEliminar.Clear();
                ListaP.imprimir(dgvLista);
                decorarDGV();
                total();
                archivo();
                estadoDeBotones();
            }
            else MessageBox.Show("El producto no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ListaP.imprimir(dgvLista);
            decorarDGV();
            total();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!validarCodigo(txtBuscar)) return;
            if (ListaP.buscar(txtBuscar.Text) != null)
            {
                Producto encontrado = ListaP.buscar(txtBuscar.Text);
                MessageBox.Show("Codigo: " + encontrado.codigo +
                    "\nNombre: " + encontrado.nombre +
                    "\nProveedor: " + encontrado.proveedor +
                    "\nPrecio: " + encontrado.precioVenta.ToString("C") +
                    "\nStock: " + encontrado.stock +
                    "\nValorizado en: " + encontrado.total().ToString("C"),
                    "Producto encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El producto no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                "\n\nModificar: Modifica los datos del producto especificado, ingrese el código en la sección Modificar y los nuevos datos en la sección Datos." +
                "\n\nEliminar: Elimina los datos del producto especificado, ingrese el código en la sección Eliminar." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los DNI/Ruc ingresados y devuelve los datos del producto, ingrese el código en la sección Buscar." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
