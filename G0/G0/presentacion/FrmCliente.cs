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
using System.IO;
using System.Xml.Serialization;

namespace G0.presentacion
{
    public partial class FrmCliente : Form
    {
        public Cola_Cliente colaC = new Cola_Cliente(10);
        public Lista_RegVen ListaAux = new Lista_RegVen();

        public FrmCliente()
        {
            InitializeComponent();
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;

            if (File.Exists("RegistroVentas.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
                FileStream lector = File.OpenRead("RegistroVentas.xml");
                Lista_RegVen ListaAux2 = (Lista_RegVen)serializador.Deserialize(lector);
                lector.Close();
                Lista_RegVen lista = new Lista_RegVen();
                for (; ListaAux2.contar() > 0; lista.insertar(ListaAux2.extraerPrimero())) ;
                
                Nodo_RegVen nodoRV = new Nodo_RegVen();
                nodoRV = lista.ini;
                while (nodoRV != null)
                {
                    Nodo_RegVen p = new Nodo_RegVen();
                    p.dato.cliente = nodoRV.dato.cliente;
                    Lista_Producto q = nodoRV.dato.productos;
                    while (q.contar() > 0)
                    {
                        p.dato.productos.insertar(q.extraerPrimero());
                    }
                    ListaAux.insertar(p.dato);
                    nodoRV = nodoRV.siguiente;
                }
            }
            obtenerNumeroVenta();
            estadoDeBotones();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);        
        private void pbSalir_Click(object sender, EventArgs e)
        {
            if (colaC.tope > 0)
            {
                DialogResult d;
                d = MessageBox.Show("¿Quiere registrar los datos antes de salir?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (d == DialogResult.Yes)
                {
                    if (File.Exists("RegistroVentas.xml"))
                    {
                        colaC.archivo(ListaAux);
                        colaC.limite = 10;
                    }
                    else colaC.archivo();
                    MessageBox.Show("Se registró con éxito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (d == DialogResult.No) this.Close();
            }
            else this.Close();
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


        private void obtenerNumeroVenta()
        {
            Nodo_RegVen p = ListaAux.ini;
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
            Nodo_Cliente r = colaC.ini;
            while (r != null)
            {
                r.dato.producto.nroVenta = cod.Remove(cod.Length - numero.ToString().Length) + numero;
                r = r.siguiente;
                numero++;
            }
        }
        private void estadoDeBotones()
        {
            if (colaC.estaVacia())
            {
                tsbModificar.Enabled = tsbDesencolar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = tsbReporte.Enabled = false;
                tsbEncolar.Enabled = true;
            }
            if (colaC.tope > 0 && !colaC.estaLlena())
            {
                tsbEncolar.Enabled = tsbModificar.Enabled = tsbDesencolar.Enabled =
                    tsbEliminar.Enabled = tsbImprimir.Enabled = tsbBuscar.Enabled =
                    tsbReporte.Enabled = true;
            }
            if (colaC.estaLlena())
            {
                tsbEncolar.Enabled = false;
                tsbModificar.Enabled = tsbDesencolar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = tsbReporte.Enabled = true;
            }
        }
        private bool validarSoloNumero(TextBox txt)
        {
            errorProvider1.Clear();
            if (txt.Text == "" || txt.Text.Length != 9)
            {
                errorProvider1.SetError(txt, "Ingrese un numero de venta (9 caractéres) válido.");
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
                errorProvider1.SetError(txtNombre, "Ingrese un nombre válido.");
                txtNombre.Focus();
                return false;
            }
            int num1;
            if (!int.TryParse(txtDNI.Text, out num1) || num1 < 0 || (txtDNI.Text.Length != 8))
            {
                errorProvider1.SetError(txtDNI, "Ingrese un DNI (8 dígitos) válido.");
                txtDNI.Focus();
                return false;
            }
            int num2;
            if (!int.TryParse(txtEdad.Text, out num2) || num1 < 0 || (int.Parse(txtEdad.Text) > 100))
            {
                errorProvider1.SetError(txtEdad, "Ingrese una edad válida.");
                txtEdad.Focus();
                return false;
            }
            if (txtDireccion.Text == "")
            {
                errorProvider1.SetError(txtDireccion, "Ingrese una dirección válida.");
                txtDireccion.Focus();
                return false;
            }
            if (cboProducto.Text == "")
            {
                errorProvider1.SetError(cboProducto, "Seleccione un nombre de producto válido.");
                cboProducto.Focus();
                return false;
            }
            double num5;
            if (!double.TryParse(txtPrecio.Text, out num5) || num5 < 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio válido.");
                txtPrecio.Focus();
                return false;
            }
            int num3;
            if (!int.TryParse(txtCantidad.Text, out num3) || num1 < 0)
            {
                errorProvider1.SetError(txtCantidad, "Ingrese una cantidad válida.");
                txtCantidad.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            errorProvider1.Clear();
            txtDireccion.Clear();
            txtDNI.Clear();
            txtNombre.Clear();
            txtEdad.Clear();

            txtPrecio.Clear();
            txtCantidad.Clear();
            cboProducto.Text = "";

            txtModificar.Clear();
            txtEliminar.Clear();
            txtBuscar.Clear();
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < colaC.tope)
            {
                if (i % 2 == 0) dgvCola.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(30, 70, 90);
                else dgvCola.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(40, 80, 100);
                i++;
            }
        }

        private void tsbEncolar_Click(object sender, EventArgs e)
        {
            if (!validarDatos()) return;
            Cliente nuevo = new Cliente(txtNombre.Text, int.Parse(txtDNI.Text), int.Parse(txtEdad.Text), txtDireccion.Text,
                new Producto(cboProducto.Text, Convert.ToDouble(txtPrecio.Text), int.Parse(txtCantidad.Text), dtpFechaV.Value));
            colaC.encolar(nuevo);
            obtenerNumeroVenta();
            colaC.imprimir(dgvCola);
            decorarDGV();
            estadoDeBotones();
        }

        private void tsbModificar_Click(object sender, EventArgs e)
        {
            if (!validarSoloNumero(txtModificar)) return;
            if (colaC.buscar(txtModificar.Text) != null)
            {
                if (validarDatos())
                {
                    Cliente nuevo = new Cliente(txtNombre.Text, int.Parse(txtDNI.Text), int.Parse(txtEdad.Text), txtDireccion.Text,
                        new Producto(cboProducto.Text, Convert.ToDouble(txtPrecio.Text), int.Parse(txtCantidad.Text), dtpFechaV.Value));
                    if (colaC.modificar(txtModificar.Text, nuevo))
                    {
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        colaC.imprimir(dgvCola);
                        decorarDGV();
                        limpiar();
                    }
                    else MessageBox.Show("Error.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("El cliente no existe, ingrese otro nunmero de venta en la sección Modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbDesencolar_Click(object sender, EventArgs e)
        {
            colaC.desencolar();
            obtenerNumeroVenta();
            colaC.imprimir(dgvCola);
            decorarDGV();
            estadoDeBotones();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (!validarSoloNumero(txtEliminar)) return;
            if (colaC.eliminar(txtEliminar.Text))
            {
                txtEliminar.Clear();
                obtenerNumeroVenta();
                colaC.imprimir(dgvCola);
                decorarDGV();
                estadoDeBotones();
            }
            else MessageBox.Show("El cliente no existe, ingrese otro numero de venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            colaC.imprimir(dgvCola);
            decorarDGV();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!validarSoloNumero(txtBuscar)) return;
            if (colaC.buscar(txtBuscar.Text) != null)
            {
                Cliente encontrado = colaC.buscar(txtBuscar.Text);
                MessageBox.Show("Numero de venta: " + encontrado.producto.nroVenta +
                    "Nombres: " + encontrado.nombre +
                    "\nDNI: " + encontrado.DNI +
                    "\nEdad: " + encontrado.edad +
                    "\nDirección: " + encontrado.direccion +
                    "\nProducto: " + encontrado.producto.nombre +
                    "\nFecha de venta: " + encontrado.producto.fechaVenta +
                    "\nPrecio: " + encontrado.producto.precioVenta.ToString("C") +
                    "\nCantidad: " + encontrado.producto.cantidad +
                    "\nMonto: " + encontrado.producto.totalventa().ToString("C"),
                    "Cliente encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El cliente no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            txtBuscar.Clear();
        }

        private void tsbLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void tsbReporte_Click(object sender, EventArgs e)
        {
            if (!colaC.estaVacia())
            {
                DialogResult d;
                d = MessageBox.Show("¿Esta seguro de registrar los datos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    if (File.Exists("RegistroVentas.xml"))
                    {
                        colaC.archivo(ListaAux);
                        colaC.limite = 10;
                    }
                    else colaC.archivo();
                    MessageBox.Show("Se registró con éxito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    colaC.imprimir(dgvCola);
                    estadoDeBotones();

                    if (File.Exists("RegistroVentas.xml"))
                    {
                        XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
                        FileStream lector = File.OpenRead("RegistroVentas.xml");
                        ListaAux = (Lista_RegVen)serializador.Deserialize(lector);
                        lector.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("¡La cola de clientes se encuentra vacia!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsbAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guardar: Encola y guarda los datos insertados en las casillas, asegúrese de colocar los datos de manera correcta en la sección Datos del cliente y del producto." +
                "\n\nModificar: Modifica los datos del cliente especificado, ingrese el numero de venta en la sección Modificar y los nuevos datos en la sección Datos." +
                "\n\nDesencolar: Elimina los datos del primer cliente." +
                "\n\nEliminar: Elimina los datos del cliente especificado, ingrese el numero de venta en la sección Eliminar." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los DNI/Ruc ingresados y devuelve los datos del cliente, ingrese el numero de venta en la sección Buscar." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error." +
                "\n\nReporte: Registra la cola de clientes en una lista, acceda a través del boton registro de ventas en el menú.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
