using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.datos;

namespace G0.controlador
{
    public class Nodo_RegCom
    {
        public Registro_Compra dato;
        public Nodo_RegCom siguiente;

        public Nodo_RegCom()
        {
            dato = new Registro_Compra();
            siguiente = null;
        }

        public Nodo_RegCom(Registro_Compra x)
        {
            dato = x;
            siguiente = null;
        }
    }
}
