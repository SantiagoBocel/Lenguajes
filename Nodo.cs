using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lenguajes
{
    class Nodo
    {
        public string Padre { get; set; }
        public string Dato { get; set; }
        public Nodo Izq { set; get; }
        public Nodo Der { set; get; }
        public Nodo(string data)
        {
            Dato = data;
            Der = null;
            Izq = null;

        }


    }
}
