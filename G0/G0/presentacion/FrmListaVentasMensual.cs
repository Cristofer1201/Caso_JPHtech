using G0.controlador;
using G0.datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace G0.presentacion
{
    public partial class FrmListaVentasMensual : Form
    {
        Lista_RegVen ListaRegV = new Lista_RegVen();
        ListaVentasMensuales ListaVM = new ListaVentasMensuales();

        public FrmListaVentasMensual()
        {
            InitializeComponent();
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
            }
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

        private bool validarcbos()
        {
            if (cboMes.Text == "")
            {
                return false;
            }
            if (cboAnio.Text == "")
            {
                return false;
            }
            return true;
        }
        private bool validartxt()
        {
            if (txtBuscar.Text == "")
            {
                MessageBox.Show("Ingrese un numero de venta valido", "Aviso", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        private int obtenerMes(string mes)
        {
            switch (mes)
            {
                case "Enero": return 1;
                case "Febrero": return 2;
                case "Marzo": return 3;
                case "Abril": return 4;
                case "Mayo": return 5;
                case "Junio": return 6;
                case "Julio": return 7;
                case "Agosto": return 8;
                case "Septiembre": return 9;
                case "Octubre": return 10;
                case "Noviembre": return 11;
                case "Diciembre": return 12;
            }
            return 0;
        }
        private void hallarTotal()
        {
            lblTotal.Text = ListaVM.total().ToString("C");
        }
        private void decorarDGV()
        {
            int i = 0;
            while (i < ListaVM.contar())
            {
                if (i % 2 == 0) dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(125, 99, 15);
                else dgvLista.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(145, 116, 17);
                i++;
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validarcbos()) return;
            ListaVM.enlistar(ListaRegV, obtenerMes(cboMes.Text), int.Parse(cboAnio.Text));
            ListaVM.imprimir(dgvLista);
            decorarDGV();
            hallarTotal();
        }

        private void cboAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validarcbos()) return;
            ListaVM.enlistar(ListaRegV, obtenerMes(cboMes.Text), int.Parse(cboAnio.Text));
            ListaVM.imprimir(dgvLista);
            decorarDGV();
            hallarTotal();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!validartxt()) return;
            if (ListaVM.buscar(txtBuscar.Text) != null)
            {
                Registro_Venta encontrado = ListaVM.buscar(txtBuscar.Text);
                MessageBox.Show("Fecha de venta: " + encontrado.productos.ini.dato.fechaVenta.ToShortDateString() +
                    "\nNumero de venta: " + encontrado.productos.ini.dato.nroVenta +
                    "\nNombre del cliente: " + encontrado.cliente.nombre +
                    "\nDNI: " + encontrado.cliente.DNI +
                    "\nDirección: " + encontrado.cliente.direccion +
                    "\nProducto: " + encontrado.productos.ini.dato.nombre +
                    "\nPrecio: " + encontrado.productos.ini.dato.precioVenta.ToString("C") +
                    "\nCantidad: " + encontrado.productos.ini.dato.cantidad +
                    "\nMonto: " + encontrado.productos.ini.dato.totalventa().ToString("C"),
                    "Cliente encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("La venta no existe, ingrese otro numero de venta", "Busqueda", MessageBoxButtons.OK);
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mes y Año: Seleccione un mes y un año de las celdas para que se muestren los datos segun su fecha de venta." +
                "\n\nBuscar: Ingrese el numero de venta en la celda buscar y se mostrarán los datos del producto vendido.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
