using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_Proveedor
    {
        public Proveedor dato;
        public Nodo_Proveedor siguiente;

        public Nodo_Proveedor()
        {
            dato = new Proveedor();
            siguiente = null;
        }

        public Nodo_Proveedor(Proveedor x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
