﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto_Lenguajes
{
    class Arbol
    {
        //Terminar el arbol
        static public List<string> Operadores = new List<string>();
        private int conteo_FL = 1;
        Automata auto = new Automata();
        Nodo nodo = null;
        Nodo Temp = null;
        Nodo TokenOp = null;
        public List<string> ValorsNT = new List<string>();
        Dictionary<string, List<string>> NT = new Dictionary<string, List<string>>();                
        Stack<Nodo> S = new Stack<Nodo>();       
        Stack<string> T = new Stack<string>();
        private Nodo Arbol_e;
        Queue<Nodo> ContenidoArbol = new Queue<Nodo>();
        public void Insertar_Set(Dictionary<string, List<string>> dato)
        {
            var llave = dato.Keys;
            foreach (var item in dato.Values)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    ValorsNT.Add(item[i]);
                }
            }
            ValorsNT.Add("<>");
            ValorsNT.Add(">=");
            ValorsNT.Add("<=");
            ValorsNT.Add("+╚");
            NT = dato;
        }       
        #region Metodos_del_arbol
        public void insertar(Queue<string> Expresion_token)
        {
            
            Operadores.Add(".");
            Operadores.Add("*");
            Operadores.Add("?");
            Operadores.Add("+");
            Operadores.Add("|"); 
            while (Expresion_token.Count != 0)
            {
                var Evaluar = Expresion_token.Dequeue();
                if (NT.ContainsKey(Evaluar))
                {
                    nodo = new Nodo(Evaluar);
                    nodo.Padre = null;
                    S.Push(nodo);
                }
                else if (ValorsNT.Contains(Evaluar))
                {
                    if (Evaluar == "(")
                    {
                        T.Push(Evaluar);
                    }
                    else if(Operadores.Contains(Evaluar))
                        {
                        if (Evaluar == "+" || Evaluar == "?" || Evaluar == "*")
                        {
                            TokenOp = new Nodo(Evaluar);
                            TokenOp.Padre = null;

                            if (S.Count < 0)
                            {
                                throw new Exception("faltan operadandos");
                            }
                            TokenOp.Izq = S.Pop();
                            TokenOp.Izq.Padre = TokenOp.Dato;
                            S.Push(TokenOp);
                        }
                        else if (T.Count != 0 && T.Peek() != "(" && (VerificarPrecedencia(Evaluar, T.Peek()) == true))
                        {
                            Nodo Temp = new Nodo(T.Pop());
                            Temp.Padre = null;
                            if (S.Count < 2)
                            {
                                throw new Exception("Faltan operandos");
                            }
                            else
                            {
                                Temp.Der = S.Pop();
                                Temp.Der.Padre = Temp.Dato;
                                Temp.Izq = S.Pop();
                                Temp.Izq.Padre = Temp.Dato;
                                S.Push(Temp);
                            }
                        }
                        if (Evaluar == "." || Evaluar == "|")
                        {
                            T.Push(Evaluar);
                        }                        
                    }
                    else if (Evaluar == ")")
                    {
                        while (T.Count > 0 && (T.Peek() != "("))
                        {
                            if (T.Count == 0)
                            {
                                Console.WriteLine("faltan operandos");
                            }
                            if (S.Count < 2)
                            {
                                Console.WriteLine("faltan operadandos");
                            }
                            Temp = new Nodo(T.Pop());
                            Temp.Padre = null;
                            Temp.Der = S.Pop();
                            Temp.Der.Padre = Temp.Dato;
                            Temp.Izq = S.Pop();
                            Temp.Izq.Padre = Temp.Dato;
                            S.Push(Temp);
                        }
                        T.Pop();
                    }
                    else
                    {
                        nodo = new Nodo(Evaluar);
                        nodo.Padre = null;
                        S.Push(nodo);
                    }
                }
                else if (Evaluar == "(")
                {
                    T.Push(Evaluar);
                }
                else if (Operadores.Contains(Evaluar))
                {
                    if (Evaluar == "+" || Evaluar == "?" || Evaluar == "*")
                    {
                        TokenOp = new Nodo(Evaluar);
                        TokenOp.Padre = null;
                        if (S.Count < 0)
                        {
                            throw new Exception("faltan operadandos");
                        }
                        TokenOp.Izq = S.Pop();
                        TokenOp.Izq.Padre = TokenOp.Dato;
                        S.Push(TokenOp);
                    }
                    else if (T.Count != 0 && T.Peek() != "(" && (VerificarPrecedencia(Evaluar, T.Peek()) == true))
                    {
                        Nodo Temp = new Nodo(T.Pop());
                        Temp.Padre = null;
                        if (S.Count < 2)
                        {
                            throw new Exception("Faltan operandos");
                        }

                        else
                        {
                            Temp.Der = S.Pop();
                            Temp.Der.Padre = Temp.Dato;
                            Temp.Izq = S.Pop();
                            Temp.Izq.Padre = Temp.Dato;
                            S.Push(Temp);
                        }
                    }
                    if (Evaluar == "." || Evaluar == "|")
                    {
                        T.Push(Evaluar);
                    }
                }
                else
                {
                    Console.WriteLine("El Dato:{0} No existe en los Set",Evaluar);
                }

            } 
               
           Arbol_e = S.Pop();
            Recorridoposorden(Arbol_e);
          auto.Calcular_Follow(ContenidoArbol,conteo_FL );
        }
        #endregion
        public void Recorridoposorden(Nodo raiz)
        {
            if (raiz != null)
            {
                Recorridoposorden(raiz.Izq);
                Recorridoposorden(raiz.Der);
                ContenidoArbol.Enqueue(raiz);                
                if (NT.ContainsKey(raiz.Dato) || ValorsNT.Contains(raiz.Dato))
                {
                    if (raiz.Dato == "*")
                    {
                        raiz.First = raiz.Izq.First;
                        raiz.Last = raiz.Izq.Last;
                        raiz.Nulable = true;
                    }
                    else if (raiz.Dato == "|")
                    {
                        foreach (var item in raiz.Izq.First)
                        {
                            raiz.First.Add(item);
                        }
                        foreach (var item in raiz.Der.First)
                        {
                            raiz.First.Add(item);
                        }
                        foreach (var item in raiz.Izq.Last)
                        {
                            raiz.Last.Add(item);
                        }
                        foreach (var item in raiz.Der.Last)
                        {
                            raiz.Last.Add(item);
                        }
                    }
                    else if (raiz.Dato == ".")
                    {
                        if (raiz.Izq.Nulable == true)
                        {
                            foreach (var item in raiz.Izq.First)
                            {
                                raiz.First.Add(item);
                            }
                            foreach (var item in raiz.Der.First)
                            {
                                raiz.First.Add(item);
                            }
                        }
                        else
                        {
                            raiz.First = raiz.Izq.First;
                        }
                        if (raiz.Der.Nulable == true)
                        {
                            foreach (var item in raiz.Izq.Last)
                            {
                                raiz.Last.Add(item);
                            }
                            foreach (var item in raiz.Der.Last)
                            {
                                raiz.Last.Add(item);
                            }
                        }
                        else
                        {
                            raiz.Last = raiz.Der.Last;
                        }
                    }
                    else
                    {
                    raiz.First.Add(conteo_FL);
                    raiz.Last.Add(conteo_FL);
                    conteo_FL++;
                    }
                }
                else
                {
                    // Todos los operadores que no se encuentre en el diccionario
                }
            }

        }
        public bool VerificarPrecedencia(string TokenPrecedencia, string UltimoOperadorLista)
        {
            int IndexToken = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            int IndexUltimo = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            return IndexToken >= IndexUltimo;
        }
    }
}
