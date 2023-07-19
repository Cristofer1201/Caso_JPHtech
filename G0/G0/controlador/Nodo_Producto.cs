using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_Producto
    {
        public Producto dato;
        public Nodo_Producto siguiente;

        public Nodo_Producto()
        {
            dato = new Producto();
            siguiente = null;
        }

        public Nodo_Producto(Producto x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
