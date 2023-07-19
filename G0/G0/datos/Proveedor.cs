using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0.datos
{
    public class Proveedor
    {
        public string nroCompra;
        public string nombreEmpresa;
        public long dniRuc;
        public string producto;
        public int cantidad;
        public double precio;
        public DateTime fechaC;
        public double monto()
        {
            return (precio * cantidad);
        }

        public Proveedor()
        {
            nroCompra = nombreEmpresa = producto = "--";
            dniRuc = cantidad = 0;
            precio = 0;
            fechaC = new DateTime(1, 1, 1, 1, 1, 1);
        }

        public Proveedor(string nE, long dniR, string pro, int c, double pre, DateTime now)
        {
            nroCompra = "--";
            nombreEmpresa = nE;
            dniRuc = dniR;
            producto = pro;
            cantidad = c;
            precio = pre;
            fechaC = now;
        }
    }
}
