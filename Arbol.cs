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
        Queue<string> pila_Token = new Queue<Arbol><string>();
        Dictionary<string, List<char>> NT = new Dictionary<string, List<char>>();
        public string ExpresionesRegulares = string.Empty;
        // declaracion del primer nodo
        public Nodo raiz;
        public Arbol()
        {
            raiz = null;
        }      
        public void Insertar_Set(Dictionary<string,List<char>> dato)
        {
            var llave = dato.Keys;
            var valor = dato.Values;
            NT = dato;
        }
        public void ConvertirExprecionaTokens()
        {
            for (int i = 0; i < ExpresionesRegulares.Length; i++)
            {
                if ((ExpresionesRegulares.Substring(i, 1) == @"\" && ExpresionesRegulares.Substring(i + 1, 1) == "+") || (ExpresionesRegulares.Substring(i, 1) == @"." && ExpresionesRegulares.Substring(i + 1, 1) == ".") ||
                    (ExpresionesRegulares.Substring(i, 1) == @"\" && ExpresionesRegulares.Substring(i + 1, 1) == "(") || (ExpresionesRegulares.Substring(i, 1) == @"\" && ExpresionesRegulares.Substring(i + 1, 1) == ")"))
                {
                    pila_Token.Enqueue(Cadena.Substring(i, 2));
                    i = i + 1;
                }
                else
                {
                    pila_Token.Enqueue(Cadena.Substring(i, 1));
                }
            }
        }
            public void Insertar_Token()
        {
            //(_*ASCII_*0..9+_*=_*[ID|T]+[ID|T]*)



        }
        #region Metodos_del_arbol
        public void insertar(string letra)
        {
            Nodo nuevo = new Nodo();
            if (this.raiz == null)
            {

            }
        }
        
        #endregion
    }
}
