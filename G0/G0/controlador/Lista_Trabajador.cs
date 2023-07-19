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
    public class Lista_Trabajador
    {
        public Nodo_Trabajador ini, fin;

        public Lista_Trabajador()
        {
            ini = fin = null;
        }

        public void insertar(Trabajador dato)
        {
            Nodo_Trabajador aux = new Nodo_Trabajador(dato);
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
            Nodo_Trabajador p = ini;
            int c = 0;
            while (p != null)
            {
                c++;
                p = p.siguiente;
            }
            return c;
        }

        public bool existeTrabajador(Trabajador x)
        {
            Nodo_Trabajador p = ini;
            bool existe = false;
            while (p != null)
            {
                if (p.dato.DNI.Equals(x.DNI))
                {
                    existe = true;
                    break;
                }
                p = p.siguiente;
            }
            return existe;
        }

        public bool modificar(int dni, Trabajador nuevo)
        {
            Nodo_Trabajador p = ini;
            bool seModifico = false;
            while (p != null)
            {
                if (p.dato.DNI.Equals(dni))
                {
                    if (!existeTrabajador(nuevo) || nuevo.DNI == dni)
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

        public Trabajador extraerPrimero()
        {
            Trabajador retira = null;
            if (!(contar() == 0))
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
            }
            return retira;
        }

        public bool eliminar(int dni)
        {
            Nodo_Trabajador p = ini;
            Nodo_Trabajador anteriorp = null;
            bool seElimino = false;
            while (p != null)
            {
                if (p.dato.DNI.Equals(dni))
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

        public Trabajador buscar(int dni)
        {
            Nodo_Trabajador p = ini;
            Trabajador encontrado = null;
            while (p != null)
            {
                if (p.dato.DNI.Equals(dni))
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
            Nodo_Trabajador p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                dgv.Rows.Add(p.dato.nombre, p.dato.DNI, p.dato.edad, p.dato.direccion, p.dato.fechaContrato.ToShortDateString());
                p = p.siguiente;
            }
        }
    }
}
