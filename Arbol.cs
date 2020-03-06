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
        Nodo nodo = new Nodo();
        Queue<string> pila_Token = new Queue<string>();
        Dictionary<string, List<char>> NT = new Dictionary<string, List<char>>();
        public string ExpresionesRegulares = string.Empty;
        List<string> Operadores = new List<string>();
        Stack<Nodo> S = new Stack<Nodo>();       
        Stack<string> T = new Stack<string>();       
        public Nodo raiz;
        public Arbol()
        {
            raiz = null;
        }
        public void Insertar_Set(Dictionary<string, List<char>> dato)
        {
            var llave = dato.Keys;
            var valor = dato.Values;
            NT = dato;
        }
        public void ConvertirExprecionaTokens()
        {
            for (int i = 0; i < ExpresionesRegulares.Length; i++)
            {

                // IDEA: realizar un for para recorrer todas las posibles combinaciones
                //    if (ExpresionesRegulares.Substring(i,1)== "D"&& ExpresionesRegulares.Substring(i + 1, 1)== "I")
                //    {
                //        pila_Token.Enqueue("DIGITO");
                //        i = i + 5;
                //    }
                //    else if (ExpresionesRegulares.Substring(i, 2) == "LE")
                //    {
                //        pila_Token.Enqueue("LETRAS");
                //        i = i + 4;
                //    }
                //    else if (ExpresionesRegulares.Substring(i, 2) == "CH")
                //    {
                //        pila_Token.Enqueue("CHARSET");
                //        i = i + 6;
                //    }
                //    else
                //    {
                pila_Token.Enqueue(ExpresionesRegulares.Substring(i, 1));
            //    }
            }
        }

        #region Metodos_del_arbol
        public void insertar()
        {
            while (pila_Token.Count != 0)
            {
                var Evaluar = pila_Token.Dequeue();
                if (NT.ContainsKey(Evaluar))
                {

                }
                else if (Evaluar == "(")
                {
                    T.Push(Evaluar);
                }

            }

            #endregion
        }
    }
}
