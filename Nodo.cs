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
        public List<int> First = new List<int>();
        public List<int> Last = new List<int>();
        public int numero { get; set; }
        public bool Nulable = false;
        public Nodo Izq { set; get; }
        public Nodo Der { set; get; }
        public Nodo(string data)
        {
            Dato = data;
            Der = null;
            Izq = null;           
        }
        public void Numero(int dato)
        {
            numero = dato;
        }


    }
}
