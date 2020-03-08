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
        Nodo nodo = null;
        Nodo Temp = null;
        Nodo TokenOp = null;
        Dictionary<string, List<char>> NT = new Dictionary<string, List<char>>();                
        Stack<Nodo> S = new Stack<Nodo>();       
        Stack<string> T = new Stack<string>();
        private Nodo Arbol_e;
        List<Nodo> ContenidoArbol = new List<Nodo>();
        public void Insertar_Set(Dictionary<string, List<char>> dato)
        {
            var llave = dato.Keys;
            var valor = dato.Values;
            NT = dato;
        }
       
        #region Metodos_del_arbol
        public void insertar(Queue<string> Expresion_token)
        {
            Operadores.Add("");
            Operadores.Add("*");
            Operadores.Add("?");
            Operadores.Add("+");
            while (Expresion_token.Count != 0)
            {
                var Evaluar = Expresion_token.Dequeue();
                if (NT.ContainsKey(Evaluar))
                {
                    nodo = new Nodo(Evaluar);
                    nodo.Padre = null;
                    S.Push(nodo);
                }
                else if (Evaluar == "0(")
                {
                    T.Push(Evaluar);
                }
                else if (Evaluar == ")0")
                {
                    while (T.Count > 0 && (T.Peek() != "0("))
                    {
                        if (T.Count == 0)
                        {
                            throw new Exception("faltan operandos");
                        }
                        if (S.Count < 2)
                        {
                            throw new Exception("faltan operadandos");
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
                    else if (T.Count != 0 && T.Peek() != "0(" && (Precedencia(Evaluar, T.Peek()) == true))
                    {
                        Temp = new Nodo(T.Pop());
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
                    if (Evaluar == "" || Evaluar == "|")
                    {
                        T.Push(Evaluar);
                    }
                }
                else
                 {
                    throw new Exception("Token No Reconocido");
                 }
                //while (T.Count > 0)
                //{
                //    if (T.Peek() == "0(")
                //    {
                //        throw new Exception("Faltan operandos");
                //    }
                //    //if (S.Count < 2)
                //    //{
                //    //    throw new Exception("Faltan operandos");
                //    //}

                //    Nodo Temp = new Nodo(T.Pop());
                //    Temp.Padre = null;
                //    Temp.Der = S.Pop();
                //    Temp.Der.Padre = Temp.Dato;
                //    Temp.Izq = S.Pop();
                //    Temp.Izq.Padre = Temp.Dato;
                //    S.Push(Temp);

                //    if (S.Count != 1)
                //    {
                //        throw new Exception("Faltan operandos");
                //    }

                //}
                Arbol_e = S.Pop();
                //
                RecorridoInorden(Arbol_e);
            }
        }
        public bool Precedencia(string T_Precedencia, string OperadorLista)
        {
            int IndexToken = Operadores.FindIndex(x => x.Equals(T_Precedencia));

            int IndexUltimo = Operadores.FindIndex(x => x.Equals(T_Precedencia));

            return IndexToken >= IndexUltimo;
        }
        #endregion
        public void RecorridoInorden(Nodo raiz)
        {

            if (raiz != null)
            {
                RecorridoInorden(raiz.Izq);
                ContenidoArbol.Add(raiz);
                RecorridoInorden(raiz.Der);
            }
        }
    }
}
