using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lenguajes
{
    
    class Fase_2
    {
      public  Dictionary<int, string[]> Tokens = new Dictionary<int, string[]>();
       int Estado { get; set; }
        public void Valores(int n)
        {
            Estado = n;
        }
     
        public void Start()
        {
            Console.WriteLine("Archivo de Entada");
            var Archivo = Console.ReadLine();
            char[] cadena;
            cadena = Archivo.ToCharArray();
            switch (Estado)
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:

                    break;
                default:
                    break;
            }
        }
    }
}
