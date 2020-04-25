using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Proyecto_Lenguajes
{
    class Arbol
    {
        //-... --       
        static public List<string> Operadores = new List<string>();
        public List<Nodo> contenido = new List<Nodo>();
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
        readonly Dictionary<string, int> dicPrecedence = new Dictionary<string, int> {{ "(", 5 }, { ")", 5 }, { "+", 4 }, { "?", 4 }, { "*", 4 }, { "·", 3 }, { "^", 2 }, { "$", 2 },
            { "|", 1 } };
        public Dictionary<string, List<string>> Insertar_Set(Dictionary<string, List<string>> dato)
        {            
            var llave = dato.Keys;
            foreach (var item in dato.Values)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    ValorsNT.Add(item[i]);
                }
            }
            #region valores Extra            
            ValorsNT.Add("+╚");
            ValorsNT.Add("*╚");
            ValorsNT.Add(".╚");
            ValorsNT.Add("?╚");
            ValorsNT.Add("|╚");

            #endregion
            NT = dato;
            return(dato);            
           // Rangos_Completos();
        }
        public void Rangos_Completos()
        {
            StreamWriter Rangos = new StreamWriter(@"c:\Temp\Rangos.txt");
            Rangos.WriteLine("Rangos Completos");
            foreach (KeyValuePair<string, List<string>> pair in NT)
            {
                Rangos.WriteLine("Conjunto:{0}",pair.Key);
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    Rangos.WriteLine("Valores{0}",pair.Value[i]);
                }
            }
            Rangos.Close();
        }
        public void First_Last()
        {
            StreamWriter First_Last = new StreamWriter(@"c:\Temp\First_Last.txt");
            First_Last.WriteLine("First And Last");
            foreach (var pair in contenido)
            {
                First_Last.WriteLine("Termino: {0}", pair.Dato);
                for (int i = 0; i < pair.First.Count; i++)
                {
                    First_Last.WriteLine("First:{0}", pair.First[i]);
                }
                for (int i = 0; i < pair.Last.Count; i++)
                {

                 First_Last.WriteLine("Last:{0}",pair.Last[i]);                
                }
            }

            First_Last.Close();
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
                else if (Evaluar == "(")
                {
                    T.Push(Evaluar);
                }
                else if (Evaluar == ")")
                {
                    while (T.Count != 0 && T.Peek() != "(")
                    {
                        if (T.Count() == 0)
                        {
                            Console.WriteLine("Faltan operandos 1");
                        }
                        if (S.Count() < 2)
                        {
                            Console.WriteLine("Faltan operandos 2");
                        }

                        Temp = new Nodo(T.Pop());
                        Temp.Der = S.Pop();
                        Temp.Der.Padre = Temp;
                        Temp.Izq = S.Pop();
                        Temp.Izq.Padre = Temp;
                        S.Push(Temp);
                    }
                    T.Pop();
                }
                else if (Operadores.Contains(Evaluar))
                {
                    if (Evaluar == "*" || Evaluar == "+" || Evaluar == "?")
                    {
                        Nodo opNode = new Nodo(Evaluar);
                        if (S.Count() == 0)
                        {
                            Console.WriteLine("Faltan operandos 3");

                        }
                        opNode.Izq = S.Pop();
                        opNode.Izq.Padre = opNode;
                        S.Push(opNode);
                    }
                    else if (T.Count() != 0)
                    {
                        while (T.Count() != 0 && T.Peek() != "(" && HasMinorPrecedence(Evaluar))
                        {
                            if (S.Count() < 2)
                            {
                                Console.WriteLine("Faltan operandos 4");
                            }
                            Nodo temp = new Nodo(T.Pop());
                            temp.Der = S.Pop();
                            temp.Der.Padre = temp;
                            temp.Izq = S.Pop();
                            temp.Izq.Padre = temp;
                            S.Push(temp);
                        }
                    }
                    if (Evaluar != "*" && Evaluar != "?" && Evaluar != "+")
                    {
                        T.Push(Evaluar);
                    }
                    else
                    {
                        Console.WriteLine( "Faltan operandos 5");                       
                    }
                }
                while (T.Count() > 0)
                {
                    if (T.Peek() == "(")
                    {
                        Console.WriteLine("Faltan operandos 6");                        
                    }
                    if (S.Count() < 2)
                    {
                        Console.WriteLine("Faltan operandos 7");
                    }
                    Nodo temp = new Nodo(T.Pop());
                    temp.Der = S.Pop();
                    temp.Der.Padre = temp;
                    temp.Izq = S.Pop();
                    temp.Izq.Padre = temp;
                    S.Push(temp);
                }
                if (S.Count() != 1)
                {
                    Console.WriteLine("Faltan operandos 8");                    
                }                          
            }
                Arbol_e = S.Pop();
                Recorrido(Arbol_e);
                auto.Calcular_Follow(ContenidoArbol, conteo_FL);
                auto.Calcular_Tabla(contenido);
        }
        #endregion
        public void Recorrido(Nodo raiz)
        {
            if (raiz != null)
            {
                Recorrido(raiz.Izq);
                Recorrido(raiz.Der);
                ContenidoArbol.Enqueue(raiz);
               // First_Last();
                contenido.Add(raiz);
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
                     raiz.Numero(conteo_FL);
                    conteo_FL++;
                    }
                }
                else
                {
                    // Todos los operadores que no se encuentre en el diccionario
                }
            }//end if

        }
        public bool HasMinorPrecedence(string Token)
        {
            dicPrecedence.TryGetValue(Token, out int tokenValue);
            dicPrecedence.TryGetValue(T.Peek(), out int opValue);
            if (tokenValue <= opValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerificarPrecedencia(string TokenPrecedencia)
        {
            int IndexToken = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            int IndexUltimo = Operadores.FindIndex(x => x.Equals(TokenPrecedencia));

            return IndexToken >= IndexUltimo;
        }
    }
}
