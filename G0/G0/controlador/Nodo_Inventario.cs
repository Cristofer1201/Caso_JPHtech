using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    class Nodo_Inventario
    {
        public Inventario dato;
        public Nodo_Inventario siguiente;

        public Nodo_Inventario()
        {
            dato = new Inventario();
            siguiente = null;
        }

        public Nodo_Inventario(Inventario x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
