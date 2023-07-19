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
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms.DataVisualization.Charting;

namespace G0.presentacion
{
    public partial class FrmReg_Com : Form
    {
        Lista_RegCom ListaRegC = new Lista_RegCom();
        public FrmReg_Com()
        {
            InitializeComponent();
            cboDescripcion.DropDownStyle = ComboBoxStyle.DropDownList;

            if (File.Exists("RegistroCompras.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
                FileStream lector = File.OpenRead("RegistroCompras.xml");
                Lista_RegCom listaAux = (Lista_RegCom)serializador.Deserialize(lector);
                lector.Close();
                while (listaAux.contar() != 0)
                {
                    ListaRegC.insertar(listaAux.extraerPrimero());
                }

                ListaRegC.graficar(Grafica);
            }
            obtenerNumeroCompra();
            estadoDeBotones();
            lblTotal.Text = ListaRegC.total().ToString("C");
            ListaRegC.imprimir(dgvLista);
            decorarDGV();
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

        private void obtenerNumeroCompra()
        {
            Nodo_RegCom p = ListaRegC.ini;
            int numero = 1;
            string cod = "C00000000";
            while (p != null)
            {
                p.dato.proveedor.nroCompra = cod.Remove(cod.Length - numero.ToString().Length) + numero;
                numero++;
                p = p.siguiente;
            }
        }
        private void estadoDeBotones()
        {
            int cantidad = ListaRegC.contar();
            if (cantidad == 0)
            {
                tsbModificar.Enabled = tsbEliminar.Enabled = tsbImprimir.Enabled =
                    tsbBuscar.Enabled = false;
            }
            if (cantidad > 0)
            {
                tsbModificar.Enabled = tsbEliminar.Enabled = tsbImprimir.Enabled =
                    tsbBuscar.Enabled = true;
            }
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < ListaRegC.contar())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.LightCyan;
                i++;
            }
        }
        private bool validarDatos()
        {
            errorProvider1.Clear();
            int num2;
            if (!int.TryParse(txtCantidad.Text, out num2) || num2 < 0)
            {
                errorProvider1.SetError(txtCantidad, "Ingrese una cantidad válida.");
                txtCantidad.Focus();
                return false;
            }
            if (cboDescripcion.Text == "")
            {
                errorProvider1.SetError(cboDescripcion, "Ingrese un nombre de producto válido.");
                cboDescripcion.Focus();
                return false;
            }
            double num3;
            if (!double.TryParse(txtPrecio.Text, out num3) || num3 < 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio válido.");
                txtPrecio.Focus();
                return false;
            }
            if (txtNombreEmpresa.Text == "")
            {
                errorProvider1.SetError(txtNombreEmpresa, "Ingrese un nombre válido.");
                txtNombreEmpresa.Focus();
                return false;
            }
            long num1;
            if (!long.TryParse(txtDNIRUC.Text, out num1) || num1 < 0 || (txtDNIRUC.Text.Length != 8 && txtDNIRUC.Text.Length != 13))
            {
                errorProvider1.SetError(txtDNIRUC, "Ingrese un DNI (8 dígitos) o Ruc (13 dígitos) válido.");
                txtDNIRUC.Focus();
                return false;
            }
            return true;
        }
        private bool validarSoloDNIRuc()
        {
            errorProvider1.Clear();
            long num1;
            if (!long.TryParse(txtModBusEl.Text, out num1) || num1 < 0 || (txtModBusEl.Text.Length != 8 && txtModBusEl.Text.Length != 13))
            {
                errorProvider1.SetError(txtModBusEl, "Ingrese un DNI (8 dígitos) o Ruc (13 dígitos) válido.");
                txtModBusEl.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            errorProvider1.Clear();
            txtCantidad.Clear();
            cboDescripcion.Text = "";
            txtPrecio.Clear();

            txtNombreEmpresa.Clear();
            txtDNIRUC.Clear();
            txtModBusEl.Clear();
        }
        public void archivo()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
            TextWriter escribir = new StreamWriter("RegistroCompras.xml");
            serializador.Serialize(escribir, ListaRegC);
            escribir.Close();
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc()) return;
            if (ListaRegC.buscar(long.Parse(txtModBusEl.Text)) != null)
            {
                long DNIRucAntiguo = long.Parse(txtModBusEl.Text);
                if (validarDatos())
                {
                    Registro_Compra nuevo = new Registro_Compra(new Proveedor(txtNombreEmpresa.Text, long.Parse(txtDNIRUC.Text),
                        cboDescripcion.Text, int.Parse(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), dtpFechaC.Value));
                    if (ListaRegC.modificar(DNIRucAntiguo, nuevo))
                    {
                        obtenerNumeroCompra();
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaRegC.imprimir(dgvLista);
                        obtenerNumeroCompra();
                        decorarDGV();
                        ListaRegC.graficar(Grafica);
                        archivo();
                        lblTotal.Text = ListaRegC.total().ToString("C");
                        limpiar();
                    }
                    else MessageBox.Show("El proveedor que intenta modificar ya existe, ingrese otro DNI/Ruc y otro nombre de empresa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("El proveedor no existe, ingrese otro DNI/Ruc en la sección Modificación/Búsqueda/Eliminación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc()) return;
            if (ListaRegC.eliminar(long.Parse(txtModBusEl.Text)))
            {
                obtenerNumeroCompra();
                ListaRegC.imprimir(dgvLista);
                decorarDGV();
                ListaRegC.graficar(Grafica);
                archivo();
                lblTotal.Text = ListaRegC.total().ToString("C");
                txtModBusEl.Clear();
                estadoDeBotones();
            }
            else MessageBox.Show("El proveedor no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ListaRegC.imprimir(dgvLista);
            decorarDGV();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc()) return;
            if (ListaRegC.buscar(long.Parse(txtModBusEl.Text)) != null)
            {
                Registro_Compra encontrado = ListaRegC.buscar(long.Parse(txtModBusEl.Text));
                MessageBox.Show("Nombre de la empresa: " + encontrado.proveedor.nombreEmpresa +
                    "\nDNI/Ruc: " + encontrado.proveedor.dniRuc +
                    "\nDescripción: " + encontrado.proveedor.producto +
                    "\nCantidad: " + encontrado.proveedor.cantidad +
                    "\nPrecio unitario: " + encontrado.proveedor.precio.ToString("C") +
                    "\nImporte: " + encontrado.proveedor.monto().ToString("C"),
                    "Proveedor encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El proveedor no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            txtModBusEl.Clear();
        }

        private void tsbLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void tsbAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modificar: Modifica los datos del proveedor especificado, ingrese el DNI/Ruc en la sección Modificación, busqueda y eliminación ¿? y los nuevos datos en la sección Datos." +
                "\n\nEliminar: Elimina los datos del proveedor especificado." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los datos del proveedor y devuelve los datos del proveeedor segun su DNI/Ruc." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
