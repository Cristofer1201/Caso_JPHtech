using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.controlador;

namespace G0.datos
{
    class Inventario
    {
        //public Producto producto;

        public DateTime fechaC_V;
        public string nroC_V;
        public double precioU;

        public int cantidadEntrada;
        public double TotalEntrada;

        public int cantidadSalida;
        public double TotalSalida;

        public int cantidadExistencia;
        public double TotalExistencia;

        public bool esCompra()
        {
            return cantidadSalida == 0;
        }
        public bool esVenta()
        {
            return cantidadEntrada == 0;
        }
        public Inventario()
        {
            fechaC_V = new DateTime(1, 1, 1);
            nroC_V = "--";
            precioU = 0;
            cantidadEntrada = 0;
            TotalEntrada = 0;
            cantidadSalida = 0;
            TotalSalida = 0;
            cantidadExistencia = 0;
            TotalExistencia = 0;
        }

        public Inventario(DateTime fechaC_V, string nroC_V, double precioU, bool esCompra, int cantidad)
        {
            this.fechaC_V = fechaC_V;
            this.nroC_V = nroC_V;
            this.precioU = precioU;

            if (esCompra)
            {
                cantidadEntrada = cantidad;
                TotalEntrada = 0;
                cantidadSalida = 0;
                TotalSalida = 0;
                cantidadExistencia = 0;
                TotalExistencia = 0;
            }
            else
            {
                cantidadEntrada = 0;
                TotalEntrada = 0;
                cantidadSalida = cantidad;
                TotalSalida = 0;
                cantidadExistencia = 0;
                TotalExistencia = 0;
            }
        }
    }
}
