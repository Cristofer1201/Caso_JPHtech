using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_Cliente
    {
        public Cliente dato;
        public Nodo_Cliente siguiente;

        public Nodo_Cliente()
        {
            dato = new Cliente();
            siguiente = null;
        }

        public Nodo_Cliente(Cliente x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
