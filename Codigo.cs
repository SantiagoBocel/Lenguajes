using System;
using Microsoft.VisualBasic.Devices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Lenguajes
{
    class Codigo
    {
        Fase_2 fase_2 = new Fase_2();
        public void GC()
        {             
            //string P = "c:\\Temp\\NUMERROR.txt";
            //string path = "\"C:\\Temp\\Meta_Action.txt\"";
            //string path_2 = "C:\\Temp\\Expresion.txt";
            ////string I = "Archivo de Entada";
            //string AE = "\"Archivo de Entada\"";
            //using (var archivoCodigo = new FileStream(@"C:\Users\Usuario\Desktop\Fase_2.cs", FileMode.OpenOrCreate))
            //{
            //    using (var texto = new StreamWriter(archivoCodigo))
            //    {
            //        texto.WriteLine("using System;");
            //        texto.WriteLine("using System.Collections.Generic;");
            //        texto.WriteLine("using System.Linq;");
            //        texto.WriteLine("using System.Text;");
            //        texto.WriteLine("using System.Threading.Tasks;");
            //        texto.WriteLine("using System.IO;");
            //        texto.WriteLine("namespace Proyecto_Lenguajes");
            //        texto.WriteLine("{");
            //        texto.WriteLine("class Fase_2");
            //        texto.WriteLine("{");
            //        texto.WriteLine("public Dictionary<int, string> Tokens = new Dictionary<int, string>();\nDictionary<int, string> Actions = new Dictionary<int, string>();\nList<char> Datos = new List<char>();");
            //        texto.WriteLine("List<string> L = new List<string>();\nList<string> D = new List<string>();\n List<string> C = new List<string>();");
            //        texto.WriteLine("int Error = 0;");
            //        texto.WriteLine("public void Start(Dictionary<string, List<string>> dato)");
            //        texto.WriteLine("{");
            //        texto.WriteLine("var P = {0};", P);
            //        texto.WriteLine(" var A = new StreamReader(P);");
            //        texto.WriteLine("Error = Convert.ToInt32(A.ReadLine());");
            //        texto.WriteLine("var path = {0}", path);
            //        texto.WriteLine("var archivo = new StreamReader(path);\nvar linea = archivo.ReadLine(); ");
            //        texto.WriteLine("while (linea != null)");
            //        texto.WriteLine("{");
            //        texto.WriteLine("var Reservada = linea.Trim().ToLower().Split(',');\n Actions.Add(Convert.ToInt32(Reservada[0]), Reservada[1]); \n linea = archivo.ReadLine();");
            //        texto.WriteLine("}");
            //        texto.WriteLine(" var path_2 = {0};\n var archivo_2 = new StreamReader(path_2);\n var linea_2 = archivo_2.ReadLine();", path_2);
            //        texto.WriteLine("while (linea_2 != null)");
            //        texto.WriteLine("{");
            //        texto.WriteLine("var num_token = Convert.ToInt32(linea_2.Substring(0, linea_2.IndexOf('=')));\n var valor_token = linea_2.Remove(0, linea_2.IndexOf('=') + 1);\n Tokens.Add(num_token, valor_token); \n linea_2 = archivo_2.ReadLine(); ");
            //        texto.WriteLine("}");
            //        texto.WriteLine("Console.WriteLine({0});\n var Archivo = Console.ReadLine().ToLower();\n char[] cadena; \n cadena = Archivo.ToCharArray();\n var VT = dato.Values;  int n = 0; ",AE);
            //        texto.WriteLine("}");
            //        texto.WriteLine("}");

            //    }
            //    archivoCodigo.Close();
            //}
        }
        public void P(Dictionary<string, List<string>> dato)
        {
            
            Computer mycomputer = new Computer();
            string cureFile_1 = @"C:\Users\Usuario\Desktop\Fase_2.cs";
            if (!File.Exists(cureFile_1))
            {
                mycomputer.FileSystem.CopyFile(@"C:\Users\Usuario\Desktop\Quinto ciclo\Lenguajes Automatas\Proyecto_Lenguajes\Fase_2.cs", @"C:\Users\Usuario\Desktop\Fase_2.cs");
                Console.ReadKey();
            }
            string cureFile = @"C:\Users\Usuario\Desktop\Fase_2.exe";
            if (!File.Exists(cureFile))
            {
             mycomputer.FileSystem.CopyFile(@"C:\Users\Usuario\Desktop\Quinto ciclo\Lenguajes Automatas\Proyecto_Lenguajes\bin\Debug\Proyecto_Lenguajes.exe", @"C:\Users\Usuario\Desktop\Fase_2.exe");
             Console.ReadKey();
            }
            else
            {
                fase_2.Start(dato);
            }            
        }
    }
}
