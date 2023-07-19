using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G0.datos;
using System.Xml.Serialization;
using System.IO;

namespace G0.controlador
{
    public class Cola_Cliente
    {
        public Nodo_Cliente ini, fin;
        public int tope, limite;

        public Cola_Cliente()
        {
            ini = fin = null;
            tope = 0;
            limite = 5;
        }

        public Cola_Cliente(int L)
        {
            ini = fin = null;
            tope = 0;
            limite = L;
        }

        public bool estaVacia()
        { return tope == 0; }

        public bool estaLlena()
        { return tope == limite; }

        public void encolar(Cliente dato)
        {
            if (!estaLlena())
            {
                Nodo_Cliente aux = new Nodo_Cliente(dato);
                if (ini == null) ini = fin = aux;
                else
                {
                    fin.siguiente = aux;
                    fin = aux;
                }
                tope++;
            }
            else MessageBox.Show("¡La cola de clientes se encuentra llena!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void archivo()
        {
            Lista_RegVen lista = listaSinRepetir();
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
            TextWriter escribir = new StreamWriter("RegistroVentas.xml");
            serializador.Serialize(escribir, lista);
            escribir.Close();
        }
        public void archivo(Lista_RegVen listaaux)
        {
            Lista_RegVen lista = listaaux;
            Lista_RegVen aux = listaSinRepetir();
            while (aux.contar() > 0)
            {
                Registro_Venta RV = new Registro_Venta();
                RV = aux.extraerPrimero();
                if (lista.buscar(RV.cliente.DNI) != null)
                {
                    Nodo_RegVen p = lista.ini;
                    while (p != null)
                    {
                        if (p.dato.cliente.DNI.Equals(RV.cliente.DNI))
                        {
                            while (RV.productos.contar() > 0)
                            {
                                p.dato.productos.insertar(RV.productos.extraerPrimero());
                            }
                            break;
                        }
                        p = p.siguiente;
                    }
                }
                else
                {
                    lista.insertar(RV);
                }
            }
            XmlSerializer serializador = new XmlSerializer(typeof(Lista_RegVen));
            TextWriter escribir = new StreamWriter("RegistroVentas.xml");
            serializador.Serialize(escribir, lista);
            escribir.Close();
        }

        public Lista_RegVen listaSinRepetir()
        {
            Lista_RegVen sinR = new Lista_RegVen();
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                if (sinR.buscar(dato.DNI) == null)
                {
                    Lista_Producto LP = new Lista_Producto();
                    LP.insertar(dato.producto);
                    sinR.insertar(new Registro_Venta(dato, LP));
                }
                else
                {
                    Nodo_RegVen p = sinR.ini;
                    while (p != null)
                    {
                        if (p.dato.cliente.DNI.Equals(dato.DNI))
                        {
                            p.dato.productos.insertar(dato.producto);
                            break;
                        }
                        p = p.siguiente;
                    }
                }
            }
            return sinR;
        }

        public bool existeCliente(Cliente x)
        {
            Cola_Cliente aux = new Cola_Cliente(limite);
            bool existe = false;
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                if (dato.DNI.Equals(x.DNI))
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

        public bool modificar(string nro, Cliente nuevo)
        {
            Cola_Cliente aux = new Cola_Cliente(limite);
            bool seModifico = false;
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                if (dato.producto.nroVenta.Equals(nro))
                {
                    nuevo.producto.nroVenta = dato.producto.nroVenta;
                    aux.encolar(nuevo);
                    seModifico = true;
                    break;
                }
                aux.encolar(dato);
            }
            for (; !estaVacia(); aux.encolar(desencolar())) ;
            for (; !aux.estaVacia(); encolar(aux.desencolar())) ;
            return seModifico;
        }

        public Cliente desencolar()
        {
            Cliente retira = null;
            if (!estaVacia())
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
                tope--;
            }
            else MessageBox.Show("¡La cola de clientes se encuentra vacia!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return retira;
        }

        public bool eliminar(string nro)
        {
            Cola_Cliente aux = new Cola_Cliente(limite);
            bool seElimino = false;
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                if (dato.producto.nroVenta.Equals(nro))
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

        public Cliente buscar(string nro)
        {
            Cola_Cliente aux = new Cola_Cliente(limite);
            Cliente encontrado = null;
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                if (dato.producto.nroVenta.Equals(nro))
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
            Cola_Cliente temp = new Cola_Cliente(limite);
            while (!estaVacia())
            {
                Cliente dato = desencolar();
                dgv.Rows.Add(dato.producto.nroVenta, dato.nombre, dato.DNI, dato.direccion, dato.producto.nombre,
                    dato.producto.precioVenta.ToString("C"), dato.producto.cantidad, dato.producto.totalventa().ToString("C"));
                temp.encolar(dato);
            }
            for (; !temp.estaVacia(); encolar(temp.desencolar())) ;
        }
    }
}
