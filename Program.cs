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
            Arbol arbol = new Arbol();
            List<string> ListaLetras = new List<string>();
            Dictionary<string, List<char>> Set_NT = new Dictionary<string, List<char>>();           
             List<string> caracteres = new List<string>();
            Console.WriteLine("Ingresar archivo");
            var path = Console.ReadLine();
            var archivo = new StreamReader(path);
            var linea = archivo.ReadLine();
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
                            Set_NT.Add(Terminal[num], new List<char>());
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
                                            Set_NT[Terminal[num]].Add(Convert.ToChar(dato));
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
                                            Set_NT[Terminal[num]].Add(Convert.ToChar(dato));
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
                                            Set_NT[Terminal[num]].Add(Convert.ToChar(dato));
                                        }
                                        break;
                                    case 2:
                                        for (int i = Ndato[0]; i <= Ndato[1]; i++)
                                        {

                                            Set_NT[Terminal[num]].Add((char)i);
                                        }
                                        break;
                                    case 1:
                                        Set_NT[Terminal[num]].Add(Ndato[0]);
                                        break;
                                    default:
                                        Console.WriteLine("Error");
                                        break;
                                }

                            }                                                                                                                 
                            linea = archivo.ReadLine().Replace("\t", "").Replace(" ", "");
                            arbol.Insertar_Set(Set_NT);
                        }
                            #endregion
                       break;
                    case "tokens":
                        #region Tokens                        
                        linea = archivo.ReadLine().Replace("\t", "");
                        do
                        {
                            //crear expresion regular
                            if (linea.Contains("'''"))
                            {
                                linea = linea.Replace("\t", "").Replace("'''", "'").Replace("  ", " ").Replace(("'" + '"' + "'").ToString(), ('"').ToString());
                            }
                            else
                            {
                                linea = linea.Replace("\t", "").Replace("'", "").Replace("  ", " ");
                            }
                            var T_Id = linea.Substring(0, linea.IndexOf('='));
                            var Expresion = linea.Remove(0, linea.IndexOf('=') + 1).Trim();
                            arbol.ExpresionesRegulares += $"({Expresion})|";
                            linea = archivo.ReadLine();
                        }
                        while (linea != "ACTIONS");
                        arbol.ConvertirExprecionaTokens();
                        #endregion
                        break;
                    case "actions":
                        #region Actions

                        #endregion
                        break;
                    default:
                        break;
                }                
            }    
        }
       
    }
}
