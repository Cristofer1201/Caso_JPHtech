using G0.controlador;
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
    public partial class FrmKardex : Form
    {
        Kardex kardex = new Kardex();
        Lista_RegCom ListaRegC = new Lista_RegCom();
        Lista_RegVen ListaRegV = new Lista_RegVen();
        public FrmKardex()
        {
            InitializeComponent();
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAnio.DropDownStyle = ComboBoxStyle.DropDownList;

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
            }

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
            ListaRegC.imprimir(dgvEntradas);
            decorarDGVEntradas();
            ListaRegV.imprimir(dgvSalidas);
            decorarDGVSalidas();
            lblMinimo.Text = 10.ToString();
            lblMaximo.Text = 1000.ToString(); 
            lblSalidas.Text = ListaRegV.total().ToString("C");
            lblEntradas.Text = ListaRegC.total().ToString("C");
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
        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void calcularMinyMax(string producto, Kardex k)
        {
            switch (producto)
            {
                case "Estufa domestica": lblMinimo.Text = 10 + ""; lblMaximo.Text = 60 + ""; break;
                case "Horno": lblMinimo.Text = 10 + ""; lblMaximo.Text = 60 + ""; break;
                case "Microondas": lblMinimo.Text = 50 + ""; lblMaximo.Text = 200 + ""; break;
                case "Refrigerador": lblMinimo.Text = 100 + ""; lblMaximo.Text = 300 + ""; break;
                case "Lavaplatos": lblMinimo.Text = 50 + ""; lblMaximo.Text = 200 + ""; break;
                case "Lavadora": lblMinimo.Text = 20 + ""; lblMaximo.Text = 100 + ""; break;
                case "Secadora": lblMinimo.Text = 20 + ""; lblMaximo.Text = 100 + ""; break;
                case "Termo eléctrico": lblMinimo.Text = 100 + ""; lblMaximo.Text = 300 + ""; break;
                case "Calefactor": lblMinimo.Text = 40 + ""; lblMaximo.Text = 100 + ""; break;
                case "Aire acondicionado": lblMinimo.Text = 10 + ""; lblMaximo.Text = 60 + ""; break;
            }
            k.minimo = int.Parse(lblMinimo.Text);
            k.maximo = int.Parse(lblMaximo.Text);
        }
        private void decorarDGVEntradas()
        {
            int i = 0;
            while (i < ListaRegC.contar())
            {
                if (i % 2 == 0) dgvEntradas.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(52, 90, 107);
                else dgvEntradas.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(66, 114, 133);
                i++;
            }
        }
        private void decorarDGVSalidas()
        {
            int i = 0;
            while (i < ListaRegV.contarProductos())
            {
                if (i % 2 == 0) dgvSalidas.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(52, 90, 107);
                else dgvSalidas.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(66, 114, 133);
                i++;
            }
        }
        private void decorarKardex()
        {
            int i = 0;
            while (i < kardex.contar())
            {
                if (i % 2 == 0) dgvKardex.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(52, 90, 107);
                else dgvKardex.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(66, 114, 133);
                i++;
            }
            dgvKardex.Columns[8].DefaultCellStyle.BackColor = Color.FromArgb(209, 203, 21);
        }
        private bool validarcbos()
        {
            if (cboProducto.Text == "")
            {
                return false;
            }
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
        private bool validarMinMax()
        {
            if (kardex.cantidadTotal() < kardex.minimo)
            {
                MessageBox.Show("Ariculos insuficientes, realize mas compras.", "Error", MessageBoxButtons.OK);
                return false;
            }
            if (kardex.cantidadTotal() > kardex.maximo)
            {
                MessageBox.Show("Demasiados articulos, realize mas ventas.", "Error", MessageBoxButtons.OK);
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

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validarcbos()) return;
            calcularMinyMax(cboProducto.Text, kardex);
            kardex.filtrarXproducto(ListaRegV, ListaRegC, cboProducto.Text, obtenerMes(cboMes.Text), int.Parse(cboAnio.Text));
            kardex.ordenar();
            kardex.enlistar();
            kardex.imprimir(dgvKardex);
            decorarKardex();
        }

        private void cboMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validarcbos()) return;
            calcularMinyMax(cboProducto.Text, kardex);
            kardex.filtrarXproducto(ListaRegV, ListaRegC, cboProducto.Text, obtenerMes(cboMes.Text), int.Parse(cboAnio.Text));
            if (!validarMinMax()) return;
            kardex.ordenar();
            kardex.enlistar();
            kardex.imprimir(dgvKardex);
            decorarKardex();
        }

        private void cboAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validarcbos()) return;
            calcularMinyMax(cboProducto.Text, kardex);
            kardex.filtrarXproducto(ListaRegV, ListaRegC, cboProducto.Text, obtenerMes(cboMes.Text), int.Parse(cboAnio.Text));
            if (!validarMinMax()) return;
            kardex.ordenar();
            kardex.enlistar();
            kardex.imprimir(dgvKardex);
            decorarKardex();
        }


    }
}
