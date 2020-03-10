using System;
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
        List<string> ValorsNT = new List<string>();
        Dictionary<string, List<string>> NT = new Dictionary<string, List<string>>();                
        Stack<Nodo> S = new Stack<Nodo>();       
        Stack<string> T = new Stack<string>();
        private Nodo Arbol_e;
        List<Nodo> ContenidoArbol = new List<Nodo>();
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
          
            
            NT = dato;
        }
       
        #region Metodos_del_arbol
        public void insertar(Queue<string> Expresion_token)
        {
            Operadores.Add(".");
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
                else
                    {
                        nodo = new Nodo(Evaluar);
                        nodo.Padre = null;
                        S.Push(nodo);
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
                    Console.WriteLine("El Dato:{0} No existe en los Set",Evaluar);
                }

            } //
               
           Arbol_e = S.Pop();
            RecorridoInorden(Arbol_e);            
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
        public bool VerificarPrecedencia(string TokenPrecedencia, string UltimoOperadorLista)
        {
            int IndexToken = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            int IndexUltimo = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            return IndexToken >= IndexUltimo;
        }
    }
}
