////////////////////////////////////////////////////////////////////////////
// Client.cs - Calls the ITokCollection interface                         //
// ver 1.0                                                                //
// Souradeepta Biswas, CSE681 - Software Modeling and Analysis, Fall 2018 //
////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * The entry point for the project
 * 
 *  Class Operations:
 * -------------------
 * - calls the IToknCollection interface
 * 
 * Required Files:
 * ---------------
 * SemiExp.cs
 * SemiInterfaces.cs
 * Toker.cs
 * TokenContext.cs
 * TokenSourceFile.cs
 * TokenState.cs
 * TokerInterfaces.cs
 * 
 * Maintenance History
 * -------------------
 * ver 1.0 : 8 0ct 2018
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemiExpressionNameSpace;


namespace ClientNameSpace
{
    public class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path of file to be processed :");
            string input = Console.ReadLine();
            SemiExp test = new SemiExp();
                        
            string testFile = "../../testSemi.txt";
           
            if (!test.open(testFile))
                Console.Write("\n  Can't open file {0}", testFile);
                     
            test.CallSemiExpression();
            Console.ReadLine();
           
        }
    }
}
