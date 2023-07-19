using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;
using G0.controlador;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace G0.controlador
{
    public class Lista_RegCom
    {
        public Nodo_RegCom ini, fin;

        public Lista_RegCom()
        {
            ini = fin = null;
        }

        public void insertar(Registro_Compra dato)
        {
            Nodo_RegCom aux = new Nodo_RegCom(dato);
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
            Nodo_RegCom p = ini;
            int c = 0;
            while (p != null)
            {
                c++;
                p = p.siguiente;
            }
            return c;
        }

        public bool existeCompra(Registro_Compra x)
        {
            Nodo_RegCom p = ini;
            bool existe = false;
            while (p != null)
            {
                if (p.dato.proveedor.dniRuc.Equals(x.proveedor.dniRuc) || p.dato.proveedor.nombreEmpresa.Equals(x.proveedor.nombreEmpresa))
                {
                    existe = true;
                    break;
                }
                p = p.siguiente;
            }
            return existe;
        }

        public bool modificar(long dnr, Registro_Compra nuevo)
        {
            Nodo_RegCom p = ini;
            bool seModifico = false;
            while (p != null)
            {
                if (p.dato.proveedor.dniRuc.Equals(dnr))
                {
                    if (!existeCompra(nuevo))
                    {
                        nuevo.proveedor.nroCompra = p.dato.proveedor.nroCompra;
                        p.dato = nuevo;
                        seModifico = true;
                        break;
                    }
                }
                p = p.siguiente;
            }
            return seModifico;
        }

        public Registro_Compra extraerPrimero()
        {
            Registro_Compra retira = null;
            if (!(contar() == 0))
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
            }
            return retira;
        }

        public bool eliminar(long dnr)
        {
            Nodo_RegCom p = ini;
            Nodo_RegCom anteriorp = null;
            bool seElimino = false;
            while (p != null)
            {
                if (p.dato.proveedor.dniRuc.Equals(dnr))
                {
                    seElimino = true;
                    if (p == ini) ini = ini.siguiente;
                    else if (p == fin) fin = anteriorp;
                    else anteriorp.siguiente = p.siguiente;
                    break;
                }
                anteriorp = p;
                p = p.siguiente;
            };
            return seElimino;
        }

        public Registro_Compra buscar(long dnr)
        {
            Nodo_RegCom p = ini;
            Registro_Compra encontrado = null;
            while (p != null)
            {
                if (p.dato.proveedor.dniRuc.Equals(dnr))
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
            Nodo_RegCom p = ini;
            while (p != null)
            {
                suma += p.dato.proveedor.monto();
                p = p.siguiente;
            }
            return suma;
        }

        public void imprimir(DataGridView dgv)
        {
            Nodo_RegCom p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                dgv.Rows.Add(p.dato.proveedor.nroCompra, p.dato.proveedor.cantidad, p.dato.proveedor.producto, 
                    p.dato.proveedor.precio.ToString("C"), p.dato.proveedor.monto().ToString("C"));
                p = p.siguiente;
            }
        }

        public void graficar(Chart ch)
        {
            ch.Series.Clear();
            ch.Titles.Clear();
            ch.Palette = ChartColorPalette.Pastel;
            ch.Titles.Add("Grafica de reporte");
            Nodo_RegCom p = ini;
            for (; p != null; p = p.siguiente)
            {
                Series serie = ch.Series.Add(p.dato.proveedor.nombreEmpresa);
                //serie.Label = p.dato.proveedor.monto().ToString("C");
                serie.Points.Add(p.dato.proveedor.monto());
            }
        }
    }
}
