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
        //public Dictionary<int, string[]> Tokens = new Dictionary<int, string[]>();
        //public Dictionary<string, int> Archivo_salida = new Dictionary<string, int>();
        Dictionary<int, string> Actions = new Dictionary<int, string>();
        List<char> Datos = new List<char>();              
       public void Empezar()
        {
            var path = "C:\\Temp\\Meta_E.txt";
            var archivo = new StreamReader(path);
            var linea = archivo.ReadLine();
        }
      
        public void Start(Dictionary<string, List<string>> dato)
        {
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
            Console.WriteLine("Archivo de Entada");
            var Archivo = Console.ReadLine().ToLower();
            char[] cadena;
            cadena = Archivo.ToCharArray();
            var VT = dato.Values;             
            int n = 0;
            int xy = 1;
            foreach (var item in VT)
            {
                if (item.Contains(Convert.ToString(cadena[n])))
                {
                    switch (xy)
                    {
                        case 1:
                            Token_Letra(ref n, cadena);
                            break;
                        case 2:
                            break;
                        case 4:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                   xy++;
                }
            }            
        }
        public void Token_Letra(ref int n, char[] cadena)
        {
        Regreso:
            if (n < cadena.Length)
            {

              switch (cadena[n])
              {
                case 'p':
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
                        if (Actions[y] == Frase)
                        {
                            Console.WriteLine("{0}={1}",Frase,y);
                        } 
                        n = 7;
                        goto Regreso;
                    }
                    break;
                case 'i':
                    break;
                case 'c':
                    break;
                case 't':
                    break;
                default:
                    break;
              }
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void Token_Digito()
        {

        }
    }
}
