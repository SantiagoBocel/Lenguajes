﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Lenguajes
{
    class Automata
    {
        //-... --  
       // Estado estado = null;
        Dictionary<int, List<int>> Follow = new Dictionary<int, List<int>>();
        string LLaves_Tabla = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        List<Nodo> Tabla_2 = new List<Nodo>();
        Dictionary<string, List<int>> Segunda_Tabla = new Dictionary<string, List<int>>();
        Dictionary<string, List<string>> camino = new Dictionary<string, List<string>>();
        Stack<Nodo> nodos = new Stack<Nodo>();
        List<int> grupos = new List<int>();

        #region Primera Tabla Follow
        public void Calcular_Follow(Queue<Nodo> arbol , int datos)
        {
            
            for (int i = 1; i < datos; i++)
            {
                Follow.Add(i, new List<int>());
            }
            while (arbol.Count != 0)
            {
                nodos.Push(arbol.Peek());
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
            //Escribir la tabla
            //Console.WriteLine("Tabla de Follow");
            //foreach (var item in Follow)
            //{
            //    Console.WriteLine("LLaves:{0}",item.Key);
            //    foreach (var Lista in item.Value)
            //    {
            //        Console.WriteLine("Valores asociados:{0}", Lista);    
            //    }
            //}
            Estados();
        }
        #endregion
        #region Segunda Tabla
        public void Calcular_Tabla(List<Nodo> raiz)
        {
            var letra_siguiente = LLaves_Tabla.Split(',');
            foreach (var item in letra_siguiente)
            {
                Segunda_Tabla.Add(item,new List<int>());
                camino.Add(item, new List<string>());
            }            
            foreach (var item in raiz[raiz.Count - 1].First)
            {
                Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[0]).Value.Add(item);              
            }
            int num = 0;

            foreach (var item in raiz)
            {
                if (item.Dato == "#")
                {
                    break;
                }
                if (item.numero != 0)
                {
                    Regresar:
                    if (Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num]).Value.Contains(item.numero))
                    {
                        foreach (var valores in Follow.FirstOrDefault(w => w.Key == item.numero).Value)
                        {
                          if (!Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value.Contains(valores))
                          {
                                //asociar tabla letra con caracter
                                //Camino.Add(letra_siguiente[num], item.Dato);
                                
                                camino.FirstOrDefault(x => x.Key == letra_siguiente[num]).Value.Add(item.Dato);
                                Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 1]).Value.Add(valores);
                          }
                            else
                            {
                                // asociar tabla letra con caracter
                                camino.FirstOrDefault(x => x.Key == letra_siguiente[num]).Value.Add(item.Dato);
                                //Camino.Add(letra_siguiente[num], item.Dato);
                                Segunda_Tabla.FirstOrDefault(x => x.Key == letra_siguiente[num + 2]).Value.Add(valores);
                                num++;
                                goto Regresar;
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
            Tabla_Automata();
        }
        #endregion    

         public void Estados()
        {
            int vueltas = 1;
            foreach (var item in nodos.Peek().First)
            {
                grupos.Add(item); 
            }            
            while (nodos.Count != 0)
            {
                if (grupos.Contains(nodos.Peek().numero))
                {
                    vueltas++;
                }
                nodos.Pop();
            }
            Cantidad_Estados(vueltas);
        }
       
        public void Cantidad_Estados(int n)
        {                       
            StreamWriter Estado = new StreamWriter(@"C:\Users\Usuario\Desktop\Cantidad_estados.txt");
            Estado.WriteLine("Estados: {0}", n);
            var dato = LLaves_Tabla.Split(',');
            for (int i = 0; i < n; i++)
            {
                Estado.Write("{0}",dato[i]);
            }
            Estado.Close();
        }
            public void Tabla_Automata()
            {
                StreamWriter Tabla_Automata = new StreamWriter(@"c:\Temp\Tabla_Automata.txt");
                Tabla_Automata.WriteLine("Segunda Tabla");
            foreach (KeyValuePair<string, List<int>> pair in Segunda_Tabla)
            {
                Tabla_Automata.WriteLine("Conjunto: {0}",pair.Key);
                for (int i = 0; i < pair.Value.Count; i++)
                {
                Tabla_Automata.WriteLine("Follow asociado:{0}",pair.Value[i]);
                }
            }
            Tabla_Automata.WriteLine("Camino de Automata");
            foreach (KeyValuePair<string, List<string>> camino_Conjunto in camino)
            {
               Tabla_Automata.WriteLine("Conjunto: {0}",camino_Conjunto.Key);
                for (int i = 0; i < camino_Conjunto.Value.Count; i++)
                {
                    Tabla_Automata.WriteLine("Caminos Terminales: {0}",camino_Conjunto.Value[i]);
                }
            }
            Tabla_Automata.Close();
            }
    }
}
