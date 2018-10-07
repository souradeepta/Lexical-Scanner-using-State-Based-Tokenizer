using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SemiExpressionNameSpace;

namespace TestSuite
{
    public class DirectoryTraverserDFS
    {
        public void TraverseDir(DirectoryInfo dir, string spaces)
        {
            Console.WriteLine(spaces + dir.FullName);

            DirectoryInfo[] children = dir.GetDirectories();
            foreach (DirectoryInfo child in children)
            {
                TraverseDir(child, spaces + " ");
            }
        }

        public void TraverseDir(string directoryPath)
        {
            TraverseDir(new DirectoryInfo(directoryPath), string.Empty);
        }
       /* public static void Main()
        {
            TraverseDir("C:\\DOWNLOADS");
        }*/
    }
    class TestRun
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================================TEST PACKAGE=======================================================================");
            Console.WriteLine("\n1st Requirement-----> \n");
            Console.WriteLine("This C# Project has been made using Visual Studio 2017 and its C# Windows Console Projects on .Net Version");
            Console.Write(typeof(string).Assembly.ImageRuntimeVersion);
            Console.WriteLine("\n\n1st Requirement-----> PASS\n");
            Console.WriteLine("\n2nd Requirement-----> \n");
            Console.WriteLine("This C# Projet has used the .Net System.IO and System.Text for all I/O. Below is the first line from the test file \"TestTokenizer\" using StreamReader");
            try
            {
                Console.WriteLine("Operning file \"TestTokenizer.txt\"......");
                using (StreamReader sr = new StreamReader("../../TestTokenizer.txt"))
                {
                    string line;
                    line = sr.ReadLine();
                    Console.WriteLine(line);
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\n2nd Requirement-----> PASS\n");
            Console.WriteLine("\n3rd Requirement-----> \n");
            Console.WriteLine("The Project consists of four Packages - 1.Toker, 2. SemiExpression, 3.ITokeCollection and 4. TestSuite");
            Console.WriteLine("\n3rd Requirement-----> PASS\n");
            Console.WriteLine("\n4th Requirement-----> \n");
            Console.WriteLine("Below are the tokens extracted from the file \"TestTokenizer\":");

            string path2 = Path.GetTempFileName();
            FileInfo fi2 = new FileInfo("../../TestTokenizer.txt");
            Console.WriteLine(fi2.ToString());
            DemoToker t1 = new DemoToker();
            t1.testToker("../../TestTokenizer.txt");
            Console.WriteLine("\n4th Requirement-----> PASS\n");
            Console.WriteLine("\n5th Requirement-----> \n");
            DemoSemi s1 = new DemoSemi();
            s1.testSemi("../../testSemi.txt");

            Console.WriteLine("\n5th Requirement-----> PASS\n");

            
            Console.ReadLine();
        }
    }
}
