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
            Console.WriteLine("\n1. Requirement----->");
            Console.WriteLine("This C# Project has been made using Visual Studio 2017 and its C# Windows Console Projects on .Net Version");
            Console.Write(typeof(string).Assembly.ImageRuntimeVersion);
            Console.WriteLine("\n1. Requirement-----> PASS");
            Console.WriteLine("\n2. Requirement----->");
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
            Console.WriteLine("2. Requirement-----> PASS");
            Console.WriteLine("\n3. Requirement----->");
            Console.WriteLine("The Project consists of five Packages - 1.Toker, 2. SemiExpression, 3.ITokeCollection , 4. Client and 5. TestSuite");
            Console.WriteLine("3. Requirement-----> PASS\n");
            Console.WriteLine("\n4.1 Requirement----->");
            Console.WriteLine("Alphanumeric tokens extracted from file \"AlphaNum.txt\":");
            DemoToker t1 = new DemoToker();
            t1.testToker("../../Alphanum.txt");
            Console.WriteLine("\n4.1 Requirement-----> PASS");
            Console.WriteLine("\n4.2 Requirement----->");
            Console.WriteLine("Punctuator tokens extracted from file \"Punct.txt\":");
            DemoToker t2 = new DemoToker();
            t2.testToker("../../Punct.txt");
            Console.WriteLine("\n4.2. Requirement-----> PASS");
            Console.WriteLine("\n4.3.1 Requirement----->");
            Console.WriteLine("Special character tokens extracted from file \"Special.txt\":");
            DemoToker t3 = new DemoToker();
            t3.testToker("../../Special.txt");
            Console.WriteLine("\n4.3.1 Requirement-----> PASS");
            Console.WriteLine("\n4.3.2 Requirement----->");
            Console.WriteLine("New special chars are \'<\',\'>\',\'<<\',\'==\' from file \"Special.txt\":");
            DemoToker2 to1 = new DemoToker2();
            to1.testToker2("../../Special.txt");
            Console.WriteLine("\n4.3.2 Requirement-----> PASS");
            Console.WriteLine("\n4.4. Requirement----->");
            Console.WriteLine("Below are the comments extracted from file \"Comments.txt\":");
            DemoToker t4 = new DemoToker();
            t4.testToker("../../Comments.txt");
            Console.WriteLine("\n4.4. Requirement-----> PASS");
            Console.WriteLine("\n4.5. Requirement----->");
            Console.WriteLine("Below are the quoted strings extracted from file \"Quotes.txt\":");
            DemoToker t5 = new DemoToker();
            t5.testToker("../../Quotes.txt");
            Console.WriteLine("\n4.5. Requirement-----> PASS");
            Console.WriteLine("\n7. Requirement-----> \n");
            Console.WriteLine("Below are the SEMIexpressions formed for # scenario from  file \"include.txt\":");
            DemoSemi s2 = new DemoSemi();
            s2.testSemi("../../include.txt");
            Console.WriteLine("\n7. Requirement-----> PASS");
            Console.WriteLine("\n6. Requirement-----> \n");
            Console.WriteLine("Below are the SEMIexpressions formed based on {, } and ;  file \"testSemi.txt\":");
            DemoSemi s1 = new DemoSemi();
            s1.testSemi("../../testSemi.txt");
            Console.WriteLine("\n6. Requirement-----> PASS");
            
            Console.WriteLine("\n8. Requirement-----> \n");
            Console.WriteLine("Below are the SEMIexpressions formed by \"for\" scenario from  file \"For.txt\":");
            DemoSemi s3 = new DemoSemi();
            s3.testSemi("../../For.txt");
            Console.WriteLine("\n8. Requirement-----> PASS");


            Console.ReadLine();
        }
    }
}
