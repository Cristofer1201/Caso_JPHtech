using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G0.datos;
using System.IO;
using System.Xml.Serialization;

namespace G0.controlador
{
    public class Cola_Proveedor
    {
        public Nodo_Proveedor ini, fin;
        public int tope, limite;

        public Cola_Proveedor()
        {
            ini = fin = null;
            tope = 0;
            limite = 5;
        }

        public Cola_Proveedor(int L)
        {
            ini = fin = null;
            tope = 0;
            limite = L;
        }

        public bool estaVacia()
        { return tope == 0; }

        public bool estaLlena()
        { return tope == limite; }

        public void encolar(Proveedor dato)
        {
            if (!estaLlena())
            {
                Nodo_Proveedor aux = new Nodo_Proveedor(dato);
                if (ini == null) ini = fin = aux;
                else
                {
                    fin.siguiente = aux;
                    fin = aux;
                }
                tope++;
            }
            else MessageBox.Show("¡La cola de proveedores se encuentra llena!, registre los proveedores al registro de compras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void archivo()
        {
            Lista_RegCom lista = new Lista_RegCom();
            while (!estaVacia())
            {
                Registro_Compra RC = new Registro_Compra();
                RC.proveedor = desencolar();
                lista.insertar(RC);
            }
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
            TextWriter escribir = new StreamWriter("RegistroCompras.xml");
            serializador.Serialize(escribir, lista);
            escribir.Close();
        }
        public void archivo(Lista_RegCom listaaux)
        {
            Cola_Proveedor aux = new Cola_Proveedor(limite);
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            limite += listaaux.contar();
            for (; !(listaaux.contar() == 0); encolar(listaaux.extraerPrimero().proveedor)) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;

            Lista_RegCom lista = new Lista_RegCom();
            while (!estaVacia())
            {
                Registro_Compra RC = new Registro_Compra();
                RC.proveedor = desencolar();
                lista.insertar(RC);
            }
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegCom));
            TextWriter escribir = new StreamWriter("RegistroCompras.xml");
            serializador.Serialize(escribir, lista);
            escribir.Close();
        }

        public bool existeProveedor(Proveedor x)
        {
            Cola_Proveedor aux = new Cola_Proveedor(limite);
            bool existe = false;
            while (!estaVacia())
            {
                Proveedor dato = desencolar();
                if (dato.dniRuc.Equals(x.dniRuc) || dato.nombreEmpresa.Equals(x.nombreEmpresa))
                {
                    existe = true;
                    aux.encolar(dato);
                    break;
                }
                aux.encolar(dato);
            }
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;
            return existe;
        }

        public bool modificar(long dnr, Proveedor nuevo)
        {
            Cola_Proveedor aux = new Cola_Proveedor(limite);
            bool seModifico = false;
            while (!estaVacia())
            {
                Proveedor dato = desencolar();
                if (dato.dniRuc.Equals(dnr))
                {
                    if (!existeProveedor(nuevo) && !aux.existeProveedor(nuevo))
                    {
                        nuevo.nroCompra = dato.nroCompra;
                        aux.encolar(nuevo);
                        seModifico = true;
                        break;
                    }
                }
                aux.encolar(dato);
            }
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;
            return seModifico;
        }

        public Proveedor desencolar()
        {
            Proveedor retira = null;
            if (!estaVacia())
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
                tope--;
            }
            else MessageBox.Show("¡La cola de proveedores se encuentra vacia!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return retira;
        }

        public bool eliminar(long dnr)
        {
            Cola_Proveedor aux = new Cola_Proveedor(limite);
            bool seElimino = false;
            while (!estaVacia())
            {
                Proveedor dato = desencolar();
                if (dato.dniRuc.Equals(dnr))
                {
                    seElimino = true;
                    break;
                }
                aux.encolar(dato);
            }
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;
            return seElimino;
        }

        public Proveedor buscar(long dnr)
        {
            Cola_Proveedor aux = new Cola_Proveedor(limite);
            Proveedor encontrado = null;
            while (!estaVacia())
            {
                Proveedor dato = desencolar();
                if (dato.dniRuc.Equals(dnr))
                {
                    encontrado = dato;
                    aux.encolar(dato);
                    break;
                }
                aux.encolar(dato);
            }
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;
            return encontrado;
        }

        public void imprimir(DataGridView dgv)
        {
            dgv.Rows.Clear();
            Cola_Proveedor temp = new Cola_Proveedor(limite);
            while (!estaVacia())
            {
                Proveedor dato = desencolar();
                dgv.Rows.Add(dato.nombreEmpresa, dato.dniRuc, dato.producto, dato.fechaC.ToShortDateString(), dato.cantidad, dato.precio.ToString("C"), dato.monto().ToString("C"));
                temp.encolar(dato);
            }
            for (; !temp.estaVacia(); encolar(temp.desencolar())) ;
        }

        
    }
}
