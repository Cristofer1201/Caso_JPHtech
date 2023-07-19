using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_Trabajador
    {
        public Trabajador dato;
        public Nodo_Trabajador siguiente;

        public Nodo_Trabajador()
        {
            dato = new Trabajador();
            siguiente = null;
        }

        public Nodo_Trabajador(Trabajador x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
