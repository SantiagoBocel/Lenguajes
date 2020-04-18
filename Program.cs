using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Lenguajes
{
    class Program
    {      
        static void Main(string[] args)
        {
            // --... --
            List<string> Operadores = new List<string>();
            Dictionary<int, string> Actions = new Dictionary<int, string>();
            Arbol arbol = new Arbol();
            List<string> ListaLetras = new List<string>();
            Dictionary<string, List<string>> Set_NT = new Dictionary<string, List<string>>();
            Dictionary<int, string[]> Token_numero = new Dictionary<int, string[]>();
            Queue<string> pila_Token = new Queue<string>();
            List<string> caracteres = new List<string>();
            List<string> Validar_num = new List<string>();
            Fase_2 fase_2 = new Fase_2();
            // Console.WriteLine("Ingresar Archivo");
            var path = "C:\\Users\\Usuario\\Downloads\\archivo 19.txt";
            var archivo = new StreamReader(path);
            var linea = archivo.ReadLine();
            Operadores.Add(".");
            Operadores.Add("*");
            Operadores.Add("?");
            Operadores.Add("+");
            Operadores.Add("|");
            
            while (linea != null)
            {
                linea = linea.Trim().ToLower();
                switch (linea)
                {
                    case "sets":
                        #region SET
                        linea = archivo.ReadLine().Replace("\t", "").Replace(" ", "");
                        while (linea != "TOKENS")
                        {
                            int num = 0;
                            var Terminal = linea.Split('=');
                            Set_NT.Add(Terminal[num], new List<string>());
                            var Valores = Terminal[num + 1].Replace("'", "");
                            var Dato = Valores.Split('+');
                            foreach (var item in  Dato)
                            {
                             var Ndato = item.Replace("'", "").Replace("..", "").Replace("...", "").ToCharArray();                             
                                switch (Ndato.Length)
                                {
                                    case 14:
                                        var Primero14 = Convert.ToInt32(Ndato[4].ToString() + Ndato[5].ToString());
                                        var Segundo14 = Convert.ToInt32(Ndato[11].ToString() + Ndato[12].ToString());
                                        var n14 = Primero14;
                                        while (n14 != Segundo14)
                                        {
                                            caracteres.Add("" + (char)n14);
                                            n14++;
                                        }
                                        foreach (var dato in caracteres)
                                        {
                                            Set_NT[Terminal[num]].Add(dato);
                                        }
                                        break;                                       
                                    case 15:
                                        var Primero = Convert.ToInt32( Ndato[4].ToString() + Ndato[5].ToString());
                                        var Segundo = Convert.ToInt32( Ndato[11].ToString() + Ndato[12].ToString() + Ndato[13].ToString());
                                        var n = Primero;
                                        while (n != Segundo)
                                        {
                                            caracteres.Add("" + (char)n);
                                            n++;
                                        }
                                        foreach (var dato in caracteres)
                                        {
                                            Set_NT[Terminal[num]].Add(dato);
                                        }
                                        break;
                                    case 16:
                                        var Primero16 = Convert.ToInt32(Ndato[4].ToString() + Ndato[5].ToString() + Ndato[6].ToString());
                                        var Segundo16 = Convert.ToInt32(Ndato[12].ToString() + Ndato[13].ToString() + Ndato[14].ToString());
                                        var n16 = Primero16;
                                        while (n16 != Segundo16)
                                        {
                                            caracteres.Add("" + (char)n16);
                                            n16++;
                                        }
                                        foreach (var dato in caracteres)
                                        {
                                            Set_NT[Terminal[num]].Add(dato);
                                        }
                                        break;
                                    case 2:
                                        for (int i = Ndato[0]; i <= Ndato[1]; i++)
                                        {

                                            Set_NT[Terminal[num]].Add(Convert.ToString((char)i));
                                        }
                                        break;
                                    case 1:
                                        Set_NT[Terminal[num]].Add(Convert.ToString( Ndato[0]));
                                        break;
                                    default:
                                        throw new Exception(" Linea vacía Error");                                                                        
                                }

                            }                                                                                                                 
                            linea = archivo.ReadLine().Replace("\t", "").Replace(" ", "");
                            arbol.Insertar_Set(Set_NT);
                        }
                            #endregion
                       break;
                    case "tokens":
                        #region Tokens 
                        var comilla_S = ("''");
                        var Simbolos_P = ("\"|ç0");
                        var Simbolo_S = ("'''");                        
                        var Simbolo_mas = ("'+'");
                        var Simbolo_por = ("'*'");
                        var Simbolo_Or = ("'|'");
                        var Simbolo_Inte = ("'?'");
                        var Simbolo_punt = ("'.'");
                        linea = archivo.ReadLine().Replace("\t", "");                        
                        pila_Token.Enqueue("(");                        
                        
                        do
                        {
                            if (linea == " ")
                            {
                                break;
                            }
                            var token = linea.Substring(0, linea.IndexOf('=')).TrimStart().TrimEnd().ToLower();
                            var num_token = Convert.ToInt32( token.Remove(0,token.Length -1 ));
                            if (token.Length < 5 ||token.Substring(0,5) != "token")
                            {
                                throw new Exception("Error en las instrucciones");
                            }
                            var Arreglo_expresiones = linea.Remove(0, linea.IndexOf('=') + 1).Trim().Replace($"{Simbolo_mas}","+╚").Replace("(", "0(").Replace($"{Simbolo_Or}", "|╚").Replace($"{Simbolo_por}", "*╚").Replace($"{Simbolo_punt}", ".╚").Replace($"{Simbolo_Inte}", "?╚").Replace($"{Simbolo_S}","'ç0'").Replace($"{comilla_S}","  ").Replace("'", "").Split(' ');
                            Token_numero.Add(num_token,Arreglo_expresiones);
                            if (Arreglo_expresiones.Length == 0)
                            {
                                throw new Exception("Error en las instrucciones lista vacia");
                            }
                            //if (Arreglo_expresiones.Contains(")*"))
                            //{
                            //    Arreglo_expresiones[3] = Arreglo_expresiones[3] + "*";
                            //}
                            for (int i = 0; i < Arreglo_expresiones.Length; i++)
                            {
                                
                                string dato = Arreglo_expresiones[i];
                                #region Token 2
                                if (dato == "0(")
                                {
                                    pila_Token.Enqueue(".");
                                    pila_Token.Enqueue("(");
                                }
                                if (dato == ")*")
                                {
                                    pila_Token.Enqueue(")");
                                    pila_Token.Enqueue("*");
                                }
                                if (dato == "ç0")
                                {
                                    pila_Token.Enqueue(".");
                                    pila_Token.Enqueue("'");                                    
                                }
                                if (dato == Simbolos_P)
                                {
                                    pila_Token.Enqueue(".");
                                    pila_Token.Enqueue("\"");
                                    pila_Token.Enqueue(".");
                                    pila_Token.Enqueue("|");
                                    pila_Token.Enqueue(".");
                                    pila_Token.Enqueue("'");
                                    pila_Token.Enqueue(".");
                                }
                                #endregion                                
                                if (dato == "")
                                {
                                    dato = ".";
                                }
                                if (Set_NT.ContainsKey(dato))
                                {                                    
                                 pila_Token.Enqueue(dato);                                                                                                                                                                                                                            
                                }
                                if( arbol.ValorsNT.Contains(dato))                                
                                {                                 
                                    pila_Token.Enqueue(dato);
                                    //solo si es una vez
                                    //if (dato == "\"")
                                    //{
                                    //    //pila_Token.Dequeue();
                                    //    //pila_Token.Dequeue();
                                    //    //pila_Token.Enqueue("(");
                                    //    pila_Token.Enqueue(dato);
                                    //}                                   
                                }                                
                            }                            
                            linea = archivo.ReadLine();
                            if (linea == "ACTIONS")
                            {                                
                                pila_Token.Enqueue(".");
                                pila_Token.Enqueue("#");
                                pila_Token.Enqueue("|");
                            }
                            else
                            {
                                pila_Token.Enqueue("|");
                            }
                           
                        }
                        while (linea != "ACTIONS");
                        fase_2.Tokens = Token_numero;
                        arbol.insertar(pila_Token);                                               
                        #endregion
                        break;
                    case "actions":
                        var ultima = "";
                        #region Actions
                        do
                        {
                            linea = archivo.ReadLine().Replace("\t", ""); 
                            var Id_Action = linea.Substring(0, linea.IndexOf('('));
                            linea = archivo.ReadLine();
                            linea = archivo.ReadLine().Replace("\t\t", "");
                            while (!linea.Contains("}"))
                            {
                              var num_Action = linea.Substring(0, linea.IndexOf('='));
                              var Instruccion_Action = linea.Remove(0, linea.IndexOf('=') + 1).Trim();
                                Actions.Add(Convert.ToInt32(num_Action),Instruccion_Action);
                                linea = archivo.ReadLine();
                            }
                            linea = archivo.ReadLine();
                            linea = archivo.ReadLine();
                            ultima = linea.Substring(0, 5).ToLower();                            
                        } while ($"{ultima}" != "error");
                        #endregion
                        #region Error
                        var num_Error = linea.Substring(0, linea.IndexOf('=') + 1).Trim();
                        #endregion
                        break;                   
                    default:
                    case "":
                        Console.ReadKey();
                        break;
                     throw new Exception("Error en las instrucciones");
                }                
            }          
        }      
    }
} 