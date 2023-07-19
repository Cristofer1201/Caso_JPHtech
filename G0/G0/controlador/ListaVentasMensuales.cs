using G0.datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G0.controlador
{
    class ListaVentasMensuales
    {
        public Nodo_RegVen ini, fin;

        public ListaVentasMensuales()
        {
            ini = fin = null;
        }

        public void enlistar(Lista_RegVen listaregV, int mes, int anio)
        {
            ini = fin = null;
            Nodo_RegVen p = listaregV.ini;
            while (p != null)
            {
                Nodo_Producto q = p.dato.productos.ini;
                while (q != null)
                {
                    if ((q.dato.fechaVenta.Month == mes) && (q.dato.fechaVenta.Year == anio))
                    {
                        Lista_Producto LP = new Lista_Producto();
                        LP.insertar(q.dato);
                        insertar(new Registro_Venta(p.dato.cliente, LP));
                    }
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
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
        public double total()
        {
            double suma = 0;
            Nodo_RegVen p = ini;
            while (p != null)
            {
                suma += p.dato.productos.ini.dato.totalventa();
                p = p.siguiente;
            }
            return suma;
        }

        public Registro_Venta buscar(string nro)
        {
            Nodo_RegVen p = ini;
            Registro_Venta encontrado = null;
            while (p != null)
            {
                if (p.dato.productos.ini.dato.nroVenta.Equals(nro))
                {
                    encontrado = p.dato;
                    break;
                }
                p = p.siguiente;
            }
            return encontrado;
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
                    dgv.Rows.Add(q.dato.nroVenta, q.dato.fechaVenta.ToShortDateString(), p.dato.cliente.nombre, p.dato.cliente.DNI, q.dato.nombre,
                        q.dato.precioVenta.ToString("C"), q.dato.cantidad, q.dato.totalventa().ToString("C"));
                    q = q.siguiente;
                }
                p = p.siguiente;
            }
        }
    }
}
