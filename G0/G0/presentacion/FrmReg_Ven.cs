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
using System.Windows.Forms.DataVisualization.Charting;

namespace G0.presentacion
{
    public partial class FrmReg_Ven : Form
    {
        Lista_RegVen ListaRegV = new Lista_RegVen();
        public FrmReg_Ven()
        {
            InitializeComponent();
            cboDescripcion.DropDownStyle = ComboBoxStyle.DropDownList;

            if (File.Exists("RegistroVentas.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
                FileStream lector = File.OpenRead("RegistroVentas.xml");
                Lista_RegVen listaAux = (Lista_RegVen)serializador.Deserialize(lector);
                lector.Close();
                Lista_RegVen listaAux2 = new Lista_RegVen();
                for (; listaAux.contar() > 0; listaAux2.insertar(listaAux.extraerPrimero())) ;
                
                Nodo_RegVen nodoRV = new Nodo_RegVen();
                nodoRV = listaAux2.ini;
                while (nodoRV != null)
                {
                    Nodo_RegVen p = new Nodo_RegVen();
                    p.dato.cliente = nodoRV.dato.cliente;
                    Lista_Producto q = nodoRV.dato.productos;
                    while (q.contar() > 0)
                    {
                        p.dato.productos.insertar(q.extraerPrimero());
                    }
                    ListaRegV.insertar(p.dato);
                    nodoRV = nodoRV.siguiente;
                }
                ListaRegV.graficarResumido(Grafica);
            }
            estadoDeBotones();
            lblTotal.Text = ListaRegV.total().ToString("C");
            ListaRegV.imprimirResumido(dgvLista);
            decorarDGVR();
            obtenerNumeroVenta();
            pbImprimirResumen.Enabled = false;
            pbImprimirDetalles.Enabled = true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);
        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
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
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void estadoDeBotones()
        {
            int cantidad = ListaRegV.contar();
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
        private void obtenerNumeroVenta()
        {
            Nodo_RegVen p = ListaRegV.ini;
            int numero = 1;
            string cod = "N00000000";
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    q.dato.nroVenta = cod.Remove(cod.Length - numero.ToString().Length) + numero;
                    numero++;
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
        }
        private bool validarDatos()
        {
            errorProvider1.Clear();
            int num3;
            if (!int.TryParse(txtCantidad.Text, out num3) || num3 < 0)
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
            double num4;
            if (!double.TryParse(txtPrecio.Text, out num4) || num4 < 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio válido.");
                txtPrecio.Focus();
                return false;
            }
            return true;
        }
        private void decorarDGVR()
        {
            int i = 0;
            while (i < ListaRegV.contar())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(155, 222, 155);
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(141, 201, 141);
                i++;
            }
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < ListaRegV.contarProductos())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(155, 222, 155);
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(141, 201, 141);
                i++;
            }
        }
        
        private bool validarSoloNumero()
        {
            errorProvider1.Clear();
            if (txtModBusEl.Text == "" || txtModBusEl.Text.Length != 9)
            {
                errorProvider1.SetError(txtModBusEl, "Ingrese un numero de venta válido (9 caracteres).");
                txtModBusEl.Focus();
                return false;
            }
            return true;
        }
        private bool validarSoloDNI()
        {
            errorProvider1.Clear();
            if (txtModBusEl.Text == "" || txtModBusEl.Text.Length != 8)
            {
                errorProvider1.SetError(txtModBusEl, "Ingrese un DNI válido (8 dígitos).");
                txtModBusEl.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            txtPrecio.Clear();
            txtModBusEl.Clear();
            txtCantidad.Clear();
            cboDescripcion.Text = "";
        }
        public void archivo()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
            TextWriter escribir = new StreamWriter("RegistroVentas.xml");
            serializador.Serialize(escribir, ListaRegV);
            escribir.Close();
        }
        public string buscarProductos(Registro_Venta RV)
        {
            Nodo_Producto p = RV.productos.ini;
            int i = 1;
            string cadena = "";
            for (; p != null; p = p.siguiente)
            {
                cadena += "\n\nPRODUCTO " + i;
                cadena += "\nFecha de venta:" + p.dato.fechaVenta.ToShortDateString();
                cadena += "\nCantidad: " + p.dato.cantidad;
                cadena += "\nDescripción: " + p.dato.nombre;
                cadena += "\nPrecio Unitario: " + p.dato.precioVenta.ToString("C");
                cadena += "\nImporte: " + p.dato.totalventa().ToString("C");
                i++;
            }
            return cadena;
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (!validarSoloNumero()) return;
            if (ListaRegV.existeVenta(txtModBusEl.Text))
            {
                if (validarDatos())
                {
                    Producto nuevo = new Producto(cboDescripcion.Text, Convert.ToDouble(txtPrecio.Text), int.Parse(txtCantidad.Text), dtpFechaV.Value);
                    if (ListaRegV.modificar(txtModBusEl.Text, nuevo))
                    {
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListaRegV.imprimirResumido(dgvLista);
                        decorarDGVR();
                        ListaRegV.graficarResumido(Grafica);
                        archivo();
                        lblTotal.Text = ListaRegV.total().ToString("C");
                        limpiar();
                    }
                    else MessageBox.Show("ERROR.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("La venta no existe, ingrese otro numero de venta en la sección Modificación/Búsqueda/Eliminación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (!validarSoloNumero()) return;
            if (ListaRegV.eliminar(txtModBusEl.Text))
            {
                obtenerNumeroVenta();
                ListaRegV.imprimirResumido(dgvLista);
                decorarDGVR();
                ListaRegV.graficarResumido(Grafica);
                archivo();
                lblTotal.Text = ListaRegV.total().ToString("C");
                txtModBusEl.Clear();
                estadoDeBotones();
            }
            else MessageBox.Show("El numero de venta no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            ListaRegV.imprimirResumido(dgvLista);
            decorarDGVR();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!validarSoloDNI()) return;
            if (ListaRegV.buscar(int.Parse(txtModBusEl.Text)) != null)
            {
                Registro_Venta encontrado = ListaRegV.buscar(int.Parse(txtModBusEl.Text));
                MessageBox.Show("Nombre del cliente: " + encontrado.cliente.nombre +
                    "\nDNI: " + encontrado.cliente.DNI +
                    "\nEdad: " + encontrado.cliente.edad +
                    "\nDirección: " + encontrado.cliente.direccion +
                    buscarProductos(encontrado),
                    "Cliente encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El cliente no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            MessageBox.Show("Modificar: Modifica los datos del cliente especificado, ingrese el numero de venta en la sección Modificación, busqueda y eliminación y los nuevos datos en la sección Datos." +
                "\n\nEliminar: Elimina los datos del cliente especificado, ingrese el numero de venta en la sección Modificación, busqueda y eliminación." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los datos del cliente y devuelve sus datos segun su DNI." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbImprimirDetalles_Click(object sender, EventArgs e)
        {
            pbImprimirDetalles.Enabled = false;
            pbImprimirResumen.Enabled = true;
            ListaRegV.imprimir(dgvLista);
            decorarDGV();
        }

        private void pbImprimirResumen_Click(object sender, EventArgs e)
        {
            pbImprimirDetalles.Enabled = true;
            pbImprimirResumen.Enabled = false;
            ListaRegV.imprimirResumido(dgvLista);
            decorarDGVR();
        }

    }
}
