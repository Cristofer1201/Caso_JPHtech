using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G0.controlador;

namespace G0.datos
{
    public class Registro_Venta
    {
        public Cliente cliente;
        public Lista_Producto productos;

        public Registro_Venta()
        {
            cliente = new Cliente();
            productos = new Lista_Producto();
        }

        public Registro_Venta(Cliente c, Lista_Producto LP)
        {
            cliente = c;
            productos = LP;
        }
    }
}
