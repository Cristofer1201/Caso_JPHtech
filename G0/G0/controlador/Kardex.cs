using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;
using System.Windows.Forms;

namespace G0.controlador
{
    class Kardex
    {
        public Nodo_Inventario ini, fin;
        public int minimo, maximo;

        public Kardex()
        {
            ini = fin = null;
            minimo = maximo = 0;
        }

        public Kardex(int min, int max)
        {
            ini = fin = null;
            minimo = min;
            maximo = max;
        }

        public void filtrarXproducto(Lista_RegVen listaV, Lista_RegCom listaC, string producto, int mes, int anio)
        {
            ini = fin = null;
            Nodo_RegCom NC = listaC.ini;
            while (NC != null)
            {
                if (NC.dato.proveedor.producto.Equals(producto) && NC.dato.proveedor.fechaC.Month.Equals(mes) && NC.dato.proveedor.fechaC.Year.Equals(anio))
                {
                    insertar(new Inventario(NC.dato.proveedor.fechaC, NC.dato.proveedor.nroCompra, NC.dato.proveedor.precio, true, NC.dato.proveedor.cantidad));
                }
                NC = NC.siguiente;
            }
            Nodo_RegVen NV = listaV.ini;
            while (NV != null)
            {
                Nodo_Producto NP = NV.dato.productos.ini;
                while (NP != null)
                {
                    if (NP.dato.nombre.Equals(producto) && NP.dato.fechaVenta.Month.Equals(mes) && NP.dato.fechaVenta.Year.Equals(anio))
                    {
                        insertar(new Inventario(NP.dato.fechaVenta, NP.dato.nroVenta, NP.dato.precioVenta, false, NP.dato.cantidad));
                    }
                    NP = NP.siguiente;
                }
                NV = NV.siguiente;
            }
        }

        public void ordenar()
        {
            Inventario aux = new Inventario();
            for (Nodo_Inventario q = ini; q != null; q = q.siguiente)
            {
                for (Nodo_Inventario p = ini; p.siguiente != null; p = p.siguiente)
                {
                    if (p.dato.fechaC_V > p.siguiente.dato.fechaC_V)
                    {
                        aux = p.dato;
                        p.dato = p.siguiente.dato;
                        p.siguiente.dato = aux;
                    }
                }
            }
        }

        public void insertar(Inventario dato)
        {
            Nodo_Inventario aux = new Nodo_Inventario(dato);
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
            Nodo_Inventario p = ini;
            int c = 0;
            while (p != null)
            {
                c++;
                p = p.siguiente;
            }
            return c;
        }

        public int cantidadTotal()
        {
            int suma = 0;
            Nodo_Inventario p = ini;
            while (p != null)
            {
                suma += p.dato.cantidadEntrada;
                suma -= p.dato.cantidadSalida;
                p = p.siguiente;
            }
            return suma;
        }

        public Inventario extraerPrimero()
        {
            Inventario retira = null;
            if (contar() != 0)
            {
                retira = ini.dato;
                if (ini == fin) ini = fin = null;
                else ini = ini.siguiente;
            }
            return retira;
        }

        public double hallarprecioProPonderado(double totalA, int cantidadA)
        {
            return totalA / cantidadA;
        }

        public void enlistar()
        {
            Nodo_Inventario p = ini;
            Nodo_Inventario anteriorp = null;
            int cantidadAcumulada = 0;
            double totalAcumulado = 0;

            // proceso del kardex
            while (p != null)
            {
                if (p.dato.esCompra())
                {
                    if (anteriorp == null)
                    {
                        p.dato.TotalEntrada = p.dato.precioU * p.dato.cantidadEntrada;

                        cantidadAcumulada += p.dato.cantidadEntrada;
                        totalAcumulado += p.dato.TotalEntrada;
                        p.dato.cantidadExistencia = p.dato.cantidadEntrada;
                        p.dato.TotalExistencia = p.dato.TotalEntrada;
                    }
                    else
                    {
                        if (anteriorp.dato.esCompra())
                        {
                            p.dato.TotalEntrada = p.dato.precioU * p.dato.cantidadEntrada;

                            cantidadAcumulada += p.dato.cantidadEntrada;
                            totalAcumulado += p.dato.TotalEntrada;
                            p.dato.cantidadExistencia = cantidadAcumulada;
                            p.dato.TotalExistencia = totalAcumulado;

                            p.dato.precioU = hallarprecioProPonderado(p.dato.TotalExistencia, p.dato.cantidadExistencia);
                        }
                        else
                        {
                            p.dato.TotalEntrada = anteriorp.dato.precioU * p.dato.cantidadEntrada;

                            cantidadAcumulada += p.dato.cantidadEntrada;
                            totalAcumulado += p.dato.TotalEntrada;
                            p.dato.cantidadExistencia = cantidadAcumulada;
                            p.dato.TotalExistencia = totalAcumulado;

                            p.dato.precioU = hallarprecioProPonderado(p.dato.TotalExistencia, p.dato.cantidadExistencia);
                        }
                    }
                }
                else
                {
                    if (anteriorp == null)
                    {
                        MessageBox.Show("No hay inventario inicial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (anteriorp.dato.esCompra())
                        {
                            p.dato.TotalSalida = p.dato.precioU * p.dato.cantidadSalida;

                            cantidadAcumulada -= p.dato.cantidadSalida;
                            totalAcumulado -= p.dato.TotalSalida;
                            p.dato.cantidadExistencia = cantidadAcumulada;
                            p.dato.TotalExistencia = totalAcumulado;

                            p.dato.precioU = anteriorp.dato.precioU;
                        }
                        else
                        {
                            p.dato.TotalSalida = anteriorp.dato.precioU * p.dato.cantidadSalida;

                            cantidadAcumulada -= p.dato.cantidadSalida;
                            totalAcumulado -= p.dato.TotalSalida;
                            p.dato.cantidadExistencia = cantidadAcumulada;
                            p.dato.TotalExistencia = totalAcumulado;

                            p.dato.precioU = anteriorp.dato.precioU;
                        }
                    }
                }
                anteriorp = p;
                p = p.siguiente;
            }
        }

        public void imprimir(DataGridView dgv)
        {
            Nodo_Inventario p = ini;
            dgv.Rows.Clear();
            while (p != null)
            {
                if (p.dato.esCompra())
                {
                    dgv.Rows.Add(p.dato.fechaC_V.ToShortDateString(), p.dato.nroC_V, p.dato.precioU.ToString("C"), p.dato.cantidadEntrada, p.dato.TotalEntrada.ToString("C"), "", "", p.dato.cantidadExistencia, p.dato.TotalExistencia.ToString("C"));
                }
                else
                {
                    dgv.Rows.Add(p.dato.fechaC_V.ToShortDateString(), p.dato.nroC_V, p.dato.precioU.ToString("C"), "", "", p.dato.cantidadSalida, p.dato.TotalSalida.ToString("C"), p.dato.cantidadExistencia, p.dato.TotalExistencia.ToString("C"));
                }
                p = p.siguiente;
            }
        }
    }
}
