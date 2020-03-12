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
        string LLaves_Tabla = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        List<Nodo> Tabla_2 = new List<Nodo>();
        Dictionary<string, List<int>> Segunda_Tabla = new Dictionary<string, List<int>>();
     
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
        #region Segunda Tabla
        public void Calcular_Tabla(List<Nodo> raiz)
        {
            var letra_siguiente = LLaves_Tabla.Split(',');
            foreach (var item in letra_siguiente)
            {
                Segunda_Tabla.Add(item,new List<int>());
            }
            // estado = new Estado(raiz[raiz.Count - 1].First, letra_siguiente[0]);
            foreach (var item in raiz[raiz.Count - 1].First)
            {
                Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[0]).Value.Add(item);
            }
            int num = 0;
            foreach (var item in raiz)
            {
                if (item.numero != 0)
                {

                    if (Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num]).Value.Contains(item.numero))
                    {
                        foreach (var valores in Follow.FirstOrDefault(w => w.Key == item.numero).Value)
                        {
                          if (!Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value.Contains(valores))
                          {
                           Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value.Add(valores);
                          }
                            else
                            {
                                Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 2]).Value.Add(valores);
                            }
                        }
                        var a = Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value;
                        var b = Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num]).Value;
                        if (a.SequenceEqual(b))
                        {
                            Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value.Clear();
                            num--;
                        }
                        else
                        {
                        num++;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
