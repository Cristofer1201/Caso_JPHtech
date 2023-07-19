using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace G0.controlador
{
    public class Lista_RegVen
    {
        public Nodo_RegVen ini, fin;

        public Lista_RegVen()
        {
            ini = fin = null;
        }

        public void insertar(Registro_Venta dato)
        {
            Nodo_RegVen aux = new Nodo_RegVen(dato);
            if (ini == null)
                ini = fin = aux;
            else
            {
                fin.siguiente = aux;
                fin = aux;
            }
        }

        public int contar()
        {
            Nodo_RegVen p = ini;
            int c = 0;
            while (p != null)
            {
                c++;
                p = p.siguiente;
            }
            return c;
        }
        public int contarProductos()
        {
            Nodo_RegVen p = ini;
            int c = 0;
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    c++;
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
            return c;
        }

        public bool existeVenta(string nro)
        {
            Nodo_RegVen p = ini;
            bool existe = false;
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    if (q.dato.nroVenta.Equals(nro))
                    {
                        existe = true;
                        break;
                    }
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
            return existe;
        }

        public bool modificar(string nro, Producto nuevo)
        {
            Nodo_RegVen p = ini;
            bool seModifico = false;
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    if (q.dato.nroVenta.Equals(nro))
                    {
                        nuevo.nroVenta = q.dato.nroVenta;
                        q.dato = nuevo;
                        seModifico = true;
                        break;
                    }
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
            return seModifico;
        }

        public Registro_Venta extraerPrimero()
        {
            Registro_Venta retira = null;
            if (contar() != 0)
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
            }
            return retira;
        }

        public bool eliminar(string nro)
        {
            Nodo_RegVen p = ini;
            Nodo_RegVen anteriorp = null;
            bool seElimino = false;
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                Nodo_Producto anteriorq = null;
                while (q != null)
                {
                    if (q.dato.nroVenta.Equals(nro))
                    {
                        seElimino = true;
                        if (q == p.dato.productos.ini) p.dato.productos.ini = p.dato.productos.ini.siguiente;
                        else if (q == p.dato.productos.fin) p.dato.productos.fin = anteriorq;
                        else anteriorq.siguiente = q.siguiente;
                        if (p.dato.productos.contar() == 0)
                        {
                            if (p == ini) p = ini.siguiente;
                            else if (p == fin) fin = anteriorp;
                            else anteriorp.siguiente = p.siguiente;
                        }
                        break;
                    }
                    anteriorq = q;
                    q = q.siguiente;
                }
                anteriorp = p;
                p = p.siguiente;
            }
            return seElimino;
        }

        public Registro_Venta buscar(int dni)
        {
            Nodo_RegVen p = ini;
            Registro_Venta encontrado = null;
            while (p != null)
            {
                if (p.dato.cliente.DNI.Equals(dni))
                {
                    encontrado = p.dato;
                    break;
                }
                p = p.siguiente;
            }
            return encontrado;
        }

        public double total()
        {
            double suma = 0;
            Nodo_RegVen p = ini;
            while (p != null)
            {
                suma += p.dato.productos.TOTAL();
                p = p.siguiente;
            }
            return suma;
        }

        public void imprimirResumido(DataGridView dgv)
        {
            Nodo_RegVen p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                if (p.dato.productos.contar() > 1)
                {
                    dgv.Rows.Add("(...)", p.dato.productos.cantidadTotal(), "(...)",
                        "(...)", p.dato.productos.TOTAL().ToString("C"));
                }
                else
                {
                    dgv.Rows.Add(p.dato.productos.ini.dato.nroVenta, p.dato.productos.cantidadTotal(), p.dato.productos.ini.dato.nombre,
                        p.dato.productos.ini.dato.precioVenta.ToString("C"), p.dato.productos.TOTAL().ToString("C"));
                }
                p = p.siguiente;
            }
        }

        public void imprimir(DataGridView dgv)
        {
            Nodo_RegVen p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    dgv.Rows.Add(q.dato.nroVenta, q.dato.cantidad, q.dato.nombre,
                        q.dato.precioVenta.ToString("C"), q.dato.totalventa().ToString("C"));
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
        }

        public void graficarResumido(Chart ch)
        {
            ch.Series.Clear();
            ch.Titles.Clear();
            ch.Palette = ChartColorPalette.SeaGreen;
            ch.Titles.Add("Grafica de reporte");
            Nodo_RegVen p = ini;
            for (; p != null; p = p.siguiente)
            {
                Series serie = ch.Series.Add(p.dato.cliente.DNI.ToString());
                serie.Points.Add(p.dato.productos.TOTAL());
            }
        }
    }
}
