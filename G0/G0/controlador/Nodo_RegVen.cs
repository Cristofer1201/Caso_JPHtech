using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_RegVen
    {
        public Registro_Venta dato;
        public Nodo_RegVen siguiente;

        public Nodo_RegVen()
        {
            dato = new Registro_Venta();
            siguiente = null;
        }

        public Nodo_RegVen(Registro_Venta x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
