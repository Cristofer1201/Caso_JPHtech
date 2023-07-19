using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0.datos
{
    public class Registro_Compra
    {
        public Proveedor proveedor;

        public Registro_Compra()
        {
            proveedor = new Proveedor();
        }

        public Registro_Compra(Proveedor regC)
        {
            proveedor = regC;
        }
    }
}
