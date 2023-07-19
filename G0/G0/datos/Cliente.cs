using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.controlador;

namespace G0.datos
{
    public class Cliente
    {
        public string nombre;
        public int DNI, edad;
        public string direccion;

        public Producto producto;

        public double monto()
        {
            return producto.precioVenta * producto.cantidad;
        }

        public Cliente()
        {
            nombre = "--";
            DNI = edad = 0;
            direccion = "--";
            producto = new Producto();
        }

        public Cliente(string n, int dni, int e, string d, Producto pr)
        {
            nombre = n;
            DNI = dni;
            edad = e;
            direccion = d;
            
            producto = pr;
        }
    }
}
