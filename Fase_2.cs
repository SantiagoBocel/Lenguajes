using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Lenguajes
{

    class Fase_2
    {
        public Dictionary<int, string> Tokens = new Dictionary<int, string>();
        //public Dictionary<string, int> Archivo_salida = new Dictionary<string, int>();
        Dictionary<int, string> Actions = new Dictionary<int, string>();
        List<char> Datos = new List<char>();              
            List<string> L = new List<string>();
            List<string> D = new List<string>();
            List<string> C = new List<string>();
        string Error = string.Empty;            
        public void Start(Dictionary<string, List<string>> dato)
        {
            var P = "c:\\Temp\\NUMERROR.txt";
            var A = new StreamReader(P);
            Error = A.ReadLine();

            var path = "C:\\Temp\\Meta_Action.txt";
            var archivo = new StreamReader(path);
            var linea = archivo.ReadLine();         
            while (linea != null)
            {                              
                var Reservada = linea.Trim().ToLower().Split(',');
                Actions.Add(Convert.ToInt32(Reservada[0]),Reservada[1]);            
                linea = archivo.ReadLine();
            }

            //
            var path_2 = "C:\\Temp\\Expresion.txt";
            var archivo_2 = new StreamReader(path_2);
            var linea_2 = archivo_2.ReadLine();        
            while (linea_2 != null)
            {
                var num_token = Convert.ToInt32(linea_2.Substring(0, linea_2.IndexOf('=')));
                var valor_token = linea_2.Remove(0, linea_2.IndexOf('=') + 1);
                Tokens.Add(num_token, valor_token);
                linea_2 = archivo_2.ReadLine();
            }
            //

            Console.WriteLine("Archivo de Entada");
            var Archivo = Console.ReadLine().ToLower();
            char[] cadena;
            cadena = Archivo.ToCharArray();
            var VT = dato.Values;             
            int n = 0;
            //
            int mun = 1;
            foreach (var item in VT)
            {
                switch (mun)
                {
                    case 1:
                        L = item;
                        mun++;
                        break;
                    case 2:
                        D = item;
                        mun++;
                        break;
                    case 3:
                        C = item;
                        mun++;
                        break;
                    default:
                        mun++;
                        break;
                }
            }
            // 
            vuelta:
            while (n!=cadena.Length)
            {
                if (Convert.ToString(cadena[n]) == " ")
                {
                    n++;
                    goto vuelta;
                }
                if (L.Contains(Convert.ToString(cadena[n])))
                {                   
                 //Letras                        
                Token_Letra(ref n, cadena);                    
                    goto vuelta;
                }
                if (D.Contains(Convert.ToString(cadena[n])))
                {
                    //Digito
                    Token_Digito(ref n, cadena);
                    goto vuelta;
                }
                if (C.Contains(Convert.ToString(cadena[n])))
                {
                    //charset
                    Token_char(ref n,cadena);
                    goto vuelta;
                }
            }                                                       
        }
        public void Token_Letra(ref int n, char[] cadena)
        {
        Regreso:
            if (n > cadena.Length - 1)
            {
                Console.ReadKey();                    
            }
            if (L.Contains(Convert.ToString(cadena[n])))
            {
             switch (cadena[n])
              {
                    case ' ':
                        n++;
                        goto Regreso;                  
                case 'p':
                        if (n >= cadena.Length - 1)
                        {
                            Letra_T(ref n, cadena);
                        }
                        else
                        {
                            if (cadena[n + 6] == 'm')
                            {
                                string Frase = "";
                                for (int i = n; i != 7; i++)
                                {
                                    Frase = Frase + Convert.ToString(cadena[i]);
                                }
                                Frase = "'" + Frase + "'";
                                int y = 1;
                                while (!Actions.ContainsKey(y))
                                {
                                    y++;
                                }
                            prueba:
                                if (Actions[y] == Frase)
                                {
                                    Console.WriteLine("{0}={1}", Frase, y);
                                }
                                else
                                {
                                    y++;
                                    goto prueba;
                                }
                                n = 7;
                                goto Regreso;
                            }
                        }
                    break;
                case 'i':
                        if (n >= cadena.Length - 1)
                        {
                            Letra_T(ref n, cadena);
                        }
                        else
                        {
                            if (cadena[n + 6] == 'e')
                            {
                                string Frase = "";
                                for (int i = n; i != 7; i++)
                                {
                                    Frase = Frase + Convert.ToString(cadena[i]);
                                }
                                Frase = "'" + Frase + "'";
                                int y = 1;
                                while (!Actions.ContainsKey(y))
                                {
                                    y++;
                                }
                            prueba:
                                if (Actions[y] == Frase)
                                {
                                    Console.WriteLine("{0}={1}", Frase, y);
                                }
                                else
                                {
                                    y++;
                                    goto prueba;
                                }
                                n = 7;
                                goto Regreso;
                            }
                        }
                        break;
                case 'c':
                        if (n >= cadena.Length - 1)
                        {
                            Letra_T(ref n, cadena);
                        }
                        else
                        {

                        if (cadena[n + 4] == 't')
                        {
                            string Frase = "";
                            for (int i = n; i != n + 5; i++)
                            {
                                Frase = Frase + Convert.ToString(cadena[i]);
                            }
                            Frase = "'" + Frase + "'";
                            int y = 1;
                            while (!Actions.ContainsKey(y))
                            {
                                y++;
                            }
                          prueba:
                            if (Actions[y] == Frase)
                            {
                                Console.WriteLine("{0}={1}", Frase, y);
                            }
                            else
                            {
                                y++;
                                goto prueba;
                            }
                            n = n + 5;
                            goto Regreso;
                        }
                            else
                            {
                                Letra_T(ref n, cadena);
                            }
                        }
                        break;
                case 't':
                        if (n >= cadena.Length - 1)
                        {
                            Letra_T(ref n, cadena);
                        }
                        else
                        {
                            if (cadena[n + 3] == 'e')
                            {
                                string Frase = "";
                                for (int i = n; i != 4; i++)
                                {
                                    Frase = Frase + Convert.ToString(cadena[i]);
                                }
                                Frase = "'" + Frase + "'";
                                int y = 1;
                                while (!Actions.ContainsKey(y))
                                {
                                    y++;
                                }
                            prueba:
                                if (Actions[y] == Frase)
                                {
                                    Console.WriteLine("{0}={1}", Frase, y);
                                }
                                else
                                {
                                    y++;
                                    goto prueba;
                                }
                                n = 4;
                                goto Regreso;
                            }
                        }
                        break;
                    case 'v':
                        if (n >= cadena.Length - 1)
                        {
                            Letra_T(ref n, cadena);
                        }
                        else
                        {
                            if (cadena[n + 2] == 'r')
                            {
                                string Frase = "";
                                for (int i = n; i != 3; i++)
                                {
                                    Frase = Frase + Convert.ToString(cadena[i]);
                                }
                                Frase = "'" + Frase + "'";
                                int y = 1;
                                while (!Actions.ContainsKey(y))
                                {
                                    y++;
                                }
                            prueba:
                                if (Actions[y] == Frase)
                                {
                                    Console.WriteLine("{0}={1}", Frase, y);
                                }
                                else
                                {
                                    y++;
                                    goto prueba;
                                }
                                n = 3;
                                goto Regreso;
                            }
                        }
                        break;
                default:
                        Letra_T(ref n, cadena);
                        goto Regreso;
                                        
              }
            }
                                    
        }
        public void Letra_T(ref int n, char[] cadena)
        {
            int x = 1;
        Reingreso:
            while (!Tokens.ContainsKey(x))
            {
                x++;
            }
            var T_O = Tokens[x].Replace("(", ".").Split('.');
            int num = 0;
        Return:
            if (num > T_O.Length)
            {
                Console.WriteLine("Error {0}", Error);
            }
            if (T_O[num].TrimStart() == "LETRA" || T_O[num].TrimStart() == Convert.ToString(cadena[n]))
            {
                Console.WriteLine("{0}={1}", cadena[n], x);
                n++;               
            }
            else
            {
                if (num < T_O.Length - 1)
                {
                    num++;
                    goto Return;
                }
                else
                {
                    x++;
                    goto Reingreso;
                }
            }
        }
        public void Token_Digito(ref int n, char[] cadena)
        {
            int x = 1;
        Reingreso:
            while (!Tokens.ContainsKey(x))
            {
                x++;
            }
            var T_O = Tokens[x].Replace("(", ".").Split('.');
            int num = 0;
        Return:
            if (num > T_O.Length)
            {
                Console.WriteLine("Error {0}",Error);
            }
            if (T_O[num].TrimStart() == "DIGITO" || T_O[num].TrimStart() == Convert.ToString(cadena[n]))
            {
                Console.WriteLine("{0}={1}", cadena[n], x);
                n++;
            }
            else
            {
                if (num < T_O.Length - 1)
                {
                    num++;
                    goto Return;
                }
                else
                {
                    x++;
                    goto Reingreso;
                }
            }
        }
        public void Token_char(ref int n, char[] cadena)
        {
            int x = 1;
        Reingreso:
            while (!Tokens.ContainsKey(x))
            {
                x++;
            }
            var T_O = Tokens[x].Replace("(", ".").Split('.');
            int num = 0;
        Return:
            if (num > T_O.Length)
            {
                Console.WriteLine("Error {0}", Error);
            }
            if (T_O[num].TrimStart() == "CHARSET" || T_O[num].TrimStart() == Convert.ToString(cadena[n]))
            {
                Console.WriteLine("{0}={1}", cadena[n], x);
                n++;
            }
            else
            {
                if (num < T_O.Length - 1)
                {
                    num++;
                    goto Return;
                }
                else
                {
                    x++;
                    goto Reingreso;
                }
            }
        }
    }
}
