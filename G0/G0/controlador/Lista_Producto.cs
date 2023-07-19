using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace G0.controlador
{
    public class Lista_Producto
    {
        public Nodo_Producto ini, fin;

        public Lista_Producto()
        {
            ini = fin = null;
        }

        public void insertar(Producto dato)
        {
            Nodo_Producto aux = new Nodo_Producto(dato);
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
            Nodo_Producto p = ini;
            int c = 0;
            while (p != null)
            {
                c++;
                p = p.siguiente;
            }
            return c;
        }

        public bool existeProducto(Producto x)
        {
            Nodo_Producto p = ini;
            bool existe = false;
            while (p != null)
            {
                if (p.dato.codigo.Equals(x.codigo) || p.dato.nombre.Equals(x.nombre))
                {
                    existe = true;
                    break;
                }
                p = p.siguiente;
            }
            return existe;
        }

        public bool modificar(string cod, Producto nuevo)
        {
            Nodo_Producto p = ini;
            bool seModifico = false;
            while (p != null)
            {
                if (p.dato.codigo.Equals(cod))
                {
                    if (!existeProducto(nuevo) || nuevo.codigo == cod)
                    {
                        p.dato = nuevo;
                        seModifico = true;
                        break;
                    }
                }
                p = p.siguiente;
            }
            return seModifico;
        }

        public Producto extraerPrimero()
        {
            Producto retira = null;
            if (!(contar() == 0))
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
            }
            return retira;
        }

        public bool eliminar(string cod)
        {
            Nodo_Producto p = ini;
            Nodo_Producto anteriorp = null;
            bool seElimino = false;
            while (p != null)
            {
                if (p.dato.codigo.Equals(cod))
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

        public Producto buscar(string cod)
        {
            Nodo_Producto p = ini;
            Producto encontrado = null;
            while (p != null)
            {
                if (p.dato.codigo.Equals(cod))
                {
                    encontrado = p.dato;
                    break;
                }
                p = p.siguiente;
            }
            return encontrado;
        }

        public double valorizacion()
        {
            double suma = 0;
            Nodo_Producto p = ini;
            while (p != null)
            {
                suma += p.dato.total();
                p = p.siguiente;
            }
            return suma;
        }

        public double TOTAL()
        {
            double suma = 0;
            Nodo_Producto p = ini;
            while (p != null)
            {
                suma += p.dato.totalventa();
                p = p.siguiente;
            }
            return suma;
        }

        public int cantidadTotal()
        {
            int suma = 0;
            Nodo_Producto p = ini;
            while (p != null)
            {
                suma += p.dato.cantidad;
                p = p.siguiente;
            }
            return suma;
        }

        public void imprimir(DataGridView dgv)
        {
            Nodo_Producto p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                dgv.Rows.Add(p.dato.codigo, p.dato.nombre, p.dato.proveedor,
                    p.dato.precioVenta.ToString("C"), p.dato.stock, p.dato.total().ToString("C"));
                p = p.siguiente;
            }
        }
    }
}
