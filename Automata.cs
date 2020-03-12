using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lenguajes
{
    class Automata
    {
        //-... --
        Dictionary<int, List<int>> Follow = new Dictionary<int, List<int>>();
        #region Primera Tabla Follow
        public void Calcular_Follow(Queue<Nodo> arbol , int datos)
        {            
            for (int i = 1; i < datos; i++)
            {
                Follow.Add(i, new List<int>());
            }
            while (arbol.Count != 0)
            {
                if (arbol.Peek().Dato == "*")
                {                  
                    foreach (var item in arbol.Peek().Izq.Last)
                    {
                        foreach (var numero_F in arbol.Peek().Izq.First)
                        {
                          Follow.FirstOrDefault(t => t.Key == item).Value.Add(numero_F);                                                    
                        }
                    }
                }
              if (arbol.Peek().Dato == ".")
              {
                foreach (var item in arbol.Peek().Izq.Last)
                {
                  foreach (var numero_F in arbol.Peek().Der.First)
                  {
                    Follow.FirstOrDefault(t => t.Key == item).Value.Add(numero_F);
                  }
                }
              }
             arbol.Dequeue();
            }
            //Escribir en disco la tabla
            Console.WriteLine("Tabla de Follow");
            foreach (var item in Follow)
            {
                Console.WriteLine("LLaves:{0}",item.Key);
                foreach (var Lista in item.Value)
                {
                    Console.WriteLine("Valores asociados:{0}", Lista);    
                }
            }
        }
        #endregion
    }
}
