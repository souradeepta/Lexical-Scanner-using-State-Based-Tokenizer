using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITokenCollectionNameSpace;
using ITokenCollectionNameSpace;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path of file to be processed :");
            string input = Console.ReadLine();
            CallSemiExpression(input);
        }
    }
}
