using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemiExpressionNameSpace;


namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path of file to be processed :");
            string input = Console.ReadLine();
            SemiExp test = new SemiExp();
            test.returnNewLines = true;
            test.displayNewLines = true;

            string testFile = "../../testSemi.txt";
           
            if (!test.open(testFile))
                Console.Write("\n  Can't open file {0}", testFile);
            Console.Write("\n  processing file: {0}\n", testFile);
         
            test.CallSemiExpression();
            Console.ReadLine();
           
        }
    }
}
