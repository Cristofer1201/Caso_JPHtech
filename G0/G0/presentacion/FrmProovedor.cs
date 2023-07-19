using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G0.controlador;
using G0.datos;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;

namespace G0.presentacion
{
    public partial class FrmProovedor : Form
    {
        public Cola_Proveedor C_Pro = new Cola_Proveedor(10);
        public Lista_RegCom listaAux = new Lista_RegCom();

        public FrmProovedor()
        {
            InitializeComponent();
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;

            if (File.Exists("RegistroCompras.xml"))
            {
                XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
                FileStream lector = File.OpenRead("RegistroCompras.xml");
                listaAux = (Lista_RegCom)serializador.Deserialize(lector);
                lector.Close();
            }
            estadoDeBotones();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParse, int lParam);        
        private void pbSalir_Click(object sender, EventArgs e)
        {
            if (C_Pro.tope > 0)
            {
                DialogResult d;
                d = MessageBox.Show("¿Quiere registrar los datos antes de salir?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (d == DialogResult.Yes)
                {
                    if (File.Exists("RegistroCompras.xml"))
                    {
                        C_Pro.archivo(listaAux);
                        C_Pro.limite = 10;
                    }
                    else C_Pro.archivo();
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


        private void obtenerNumeroCompra()
        {
            Nodo_RegCom p = listaAux.ini;
            int numero = 1;
            string cod = "C00000000";
            while (p != null)
            {
                p.dato.proveedor.nroCompra = cod.Remove(cod.Length - numero.ToString().Length) + numero;
                numero++;
                p = p.siguiente;
            }
            Nodo_Proveedor q = C_Pro.ini;
            while (q != null)
            {
                q.dato.nroCompra = cod.Remove(cod.Length - numero.ToString().Length) + numero;
                q = q.siguiente;
                numero++;
            }
        }
        private void estadoDeBotones()
        {
            if (C_Pro.estaVacia())
            {
                tsbModificar.Enabled = tsbDesencolar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = tsbReporte.Enabled = false;
                tsbEncolar.Enabled = true;
            }
            if (C_Pro.tope > 0 && !C_Pro.estaLlena())
            {
                tsbEncolar.Enabled = tsbModificar.Enabled = tsbDesencolar.Enabled = 
                    tsbEliminar.Enabled = tsbImprimir.Enabled = tsbBuscar.Enabled =
                    tsbReporte.Enabled = true;
            }
            if (C_Pro.estaLlena())
            {
                tsbEncolar.Enabled = false;
                tsbModificar.Enabled = tsbDesencolar.Enabled = tsbEliminar.Enabled =
                    tsbImprimir.Enabled = tsbBuscar.Enabled = tsbReporte.Enabled = true;
            }
        }
        private bool validarSoloDNIRuc(TextBox txt)
        {
            errorProvider1.Clear();
            long num1;
            if (!long.TryParse(txt.Text, out num1) || num1 < 0 || (txt.Text.Length != 8 && txt.Text.Length != 13))
            {
                errorProvider1.SetError(txt, "Ingrese un DNI (8 dígitos) o Ruc (13 dígitos) válido.");
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
            long num1;
            if (!long.TryParse(txtDniRuc.Text, out num1) || num1 < 0 || (txtDniRuc.Text.Length != 8 && txtDniRuc.Text.Length != 13))
            {
                errorProvider1.SetError(txtDniRuc, "Ingrese un DNI (8 dígitos) o Ruc (13 dígitos) válido.");
                txtDniRuc.Focus();
                return false;
            }
            if (cboProducto.Text == "")
            {
                errorProvider1.SetError(cboProducto, "Ingrese un nombre de producto válido.");
                cboProducto.Focus();
                return false;
            }
            int num2;
            if (!int.TryParse(txtCantidad.Text, out num2) || num2 < 0)
            {
                errorProvider1.SetError(txtCantidad, "Ingrese una cantidad válida.");
                txtCantidad.Focus();
                return false;
            }
            double num3;
            if (!double.TryParse(txtPrecio.Text, out num3) || num3 < 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio válido.");
                txtPrecio.Focus();
                return false;
            }
            return true;
        }
        private void limpiar()
        {
            errorProvider1.Clear();
            txtCantidad.Clear();
            txtDniRuc.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            cboProducto.Text = "";

            txtModificar.Clear();
            txtEliminar.Clear();
            txtBuscar.Clear();
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < C_Pro.tope)
            {
                if (i % 2 == 0) dgvCola.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(90, 90, 150);
                else dgvCola.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(75, 75, 135);
                i++;
            }
        }

        private void tsbEncolar_Click_1(object sender, EventArgs e)
        {
            if (!validarDatos()) return;
            Proveedor nuevo = new Proveedor(txtNombre.Text, long.Parse(txtDniRuc.Text), cboProducto.Text, int.Parse(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), dtpFechaC.Value);
            Registro_Compra nuevoRC = new Registro_Compra(nuevo);
            if (listaAux.contar() != 0)
            {
                if (!(C_Pro.existeProveedor(nuevo)) && !(listaAux.existeCompra(nuevoRC)))
                {
                    C_Pro.encolar(nuevo);
                    obtenerNumeroCompra();
                    C_Pro.imprimir(dgvCola);
                    decorarDGV();
                    estadoDeBotones();
                }
                else MessageBox.Show("El proveedor ya existe en la cola o registro, ingrese un nombre y DNI/Ruc diferente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (!C_Pro.existeProveedor(nuevo))
                {
                    C_Pro.encolar(nuevo);
                    C_Pro.imprimir(dgvCola);
                    decorarDGV();
                    estadoDeBotones();
                }
                else MessageBox.Show("El proveedor ya existe en la cola, ingrese un nombre y DNI/Ruc diferente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tsbModificar_Click_1(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc(txtModificar)) return;
            if (C_Pro.buscar(long.Parse(txtModificar.Text)) != null)
            {
                long DNIRucAntiguo = long.Parse(txtModificar.Text);
                if (validarDatos())
                {
                    Proveedor nuevo = new Proveedor(txtNombre.Text, long.Parse(txtDniRuc.Text), cboProducto.Text, int.Parse(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), dtpFechaC.Value);
                    if (C_Pro.modificar(DNIRucAntiguo, nuevo))
                    {
                        MessageBox.Show("¡Se modificó con éxito!", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        C_Pro.imprimir(dgvCola);
                        decorarDGV();
                        limpiar();
                    }
                    else MessageBox.Show("El proveedor que intenta modificar ya existe, ingrese otro DNI/Ruc y otro nombre de empresa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else MessageBox.Show("El proveedor no existe, ingrese otro DNI/Ruc en la sección Modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbDesencolar_Click_1(object sender, EventArgs e)
        {
            C_Pro.desencolar();
            obtenerNumeroCompra();
            C_Pro.imprimir(dgvCola);
            decorarDGV();
            estadoDeBotones();
        }

        private void tsbEliminar_Click_1(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc(txtEliminar)) return;
            if (C_Pro.eliminar(long.Parse(txtEliminar.Text)))
            {
                obtenerNumeroCompra();
                txtEliminar.Clear();
                C_Pro.imprimir(dgvCola);
                decorarDGV();
                estadoDeBotones();
            }
            else MessageBox.Show("El proveedor no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbImprimir_Click_1(object sender, EventArgs e)
        {
            C_Pro.imprimir(dgvCola);
            decorarDGV();
        }

        private void tsbBuscar_Click_1(object sender, EventArgs e)
        {
            if (!validarSoloDNIRuc(txtBuscar)) return;
            if (C_Pro.buscar(long.Parse(txtBuscar.Text)) != null)
            {
                Proveedor encontrado = C_Pro.buscar(long.Parse(txtBuscar.Text));
                MessageBox.Show("Nombre de la empresa: " + encontrado.nombreEmpresa +
                    "\nDNI/Ruc: " + encontrado.dniRuc +
                    "\nProducto: " + encontrado.producto +
                    "\nFecha de compra:" + encontrado.fechaC.ToShortDateString() +
                    "\nCantidad: " + encontrado.cantidad +
                    "\nPrecio: " + encontrado.precio.ToString("C") +
                    "\nMonto: " + encontrado.monto().ToString("C"),
                    "Proveedor encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El proveedor no existe.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            txtBuscar.Clear();
        }

        private void tsbLimpiar_Click_1(object sender, EventArgs e)
        {
            limpiar();
        }

        private void tsbReporte_Click_1(object sender, EventArgs e)
        {
            if (!C_Pro.estaVacia())
            {
                DialogResult d;
                d = MessageBox.Show("¿Esta seguro de registrar los datos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    if (File.Exists("RegistroCompras.xml"))
                    {
                        C_Pro.archivo(listaAux);
                        C_Pro.limite = 10;
                    }
                    else C_Pro.archivo();
                    MessageBox.Show("Se registró con éxito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    C_Pro.imprimir(dgvCola);
                    estadoDeBotones();

                    if (File.Exists("RegistroCompras.xml"))
                    {
                        XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
                        FileStream lector = File.OpenRead("RegistroCompras.xml");
                        listaAux = (Lista_RegCom)serializador.Deserialize(lector);
                        lector.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("¡La cola de proveedores se encuentra vacia!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsbAyuda_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Guardar: Encola y guarda los datos insertados en las casillas, asegúrese de colocar los datos de manera correcta en la sección Datos." +
                "\n\nModificar: Modifica los datos del proveedor especificado, ingrese el DNI/Ruc en la sección Modificar y los nuevos datos en la sección Datos." +
                "\n\nDesencolar: Elimina los datos del primer proveedor." +
                "\n\nEliminar: Elimina los datos del proveedor especificado, ingrese el DNI/Ruc en la sección Eliminar." +
                "\n\nImprimir: Proporciona todos los datos guardados a través de una cuadrícula." +
                "\n\nBuscar: Recorre los DNI/Ruc ingresados y devuelve los datos del proveeedor, ingrese el DNI/Ruc en la sección Buscar." +
                "\n\nLimpiar: Depura todas las casillas y los íconos de error." +
                "\n\nReporte: Registra la cola de proveedores en una lista, acceda a través del boton registro de compras en el menú.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmProovedor_Load(object sender, EventArgs e)
        {

        }
    }
}
