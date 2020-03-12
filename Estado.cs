using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lenguajes
{
    class Estado
    {
       public string letra { get; set; }
       public List<int> numeros { get; set; }
       public Estado siguiente_estado { get; set; }
       public Nodo camino { get; set; }
       public Estado(List<int> datos,string valor)
        {
            numeros = datos;
            letra = valor;
        }
    }
}
