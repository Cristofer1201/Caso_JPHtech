using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0.datos
{
    public class Trabajador
    {
        public string nombre;
        public int DNI, edad;
        public string direccion;
        public DateTime fechaContrato;

        public Trabajador()
        {
            nombre = "--";
            DNI = edad = 0;
            direccion = "--";
            fechaContrato = new DateTime(1, 1, 1);
        }

        public Trabajador(string n, int dni, int e, string d, DateTime fecha)
        {
            nombre = n;
            DNI = dni;
            edad = e;
            direccion = d;
            fechaContrato = fecha;
        }
    }
}
