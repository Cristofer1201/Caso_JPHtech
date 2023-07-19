using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0.datos
{
    public class Producto
    {
        public string codigo, nombre, proveedor;
        public double precioVenta;
        public int stock, cantidad;
        public string nroVenta;
        public DateTime fechaVenta;

        public double total()
        {
            return precioVenta * stock;
        }
        public double totalventa()
        {
            return precioVenta * cantidad;
        }
        public Producto()
        {
            codigo = nombre = proveedor = "";
            precioVenta = 0.0;
            stock = 0;
            cantidad = 0;
            nroVenta = "--";
            fechaVenta = new DateTime(1, 1, 1, 1, 1, 1);
        }

        //para su formulario
        public Producto(string cod, string nom, string pro, double pre, int sto)
        {
            codigo = cod;
            nombre = nom;
            proveedor = pro;
            precioVenta = pre;
            stock = sto;
        }

        //para cliente
        public Producto(string nom, double pre, int can, DateTime fv)
        {
            nombre = nom;
            precioVenta = pre;
            cantidad = can;
            fechaVenta = fv;
        }
    }
}
