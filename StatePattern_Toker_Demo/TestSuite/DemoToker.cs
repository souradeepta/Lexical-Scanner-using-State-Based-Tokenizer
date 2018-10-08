////////////////////////////////////////////////////////////////////////////
// DemoToker.cs - Contains a test call class for Tokenizer                //
// ver 1.0                                                                //
// Souradeepta Biswas, CSE681 - Software Modeling and Analysis, Fall 2018 //
////////////////////////////////////////////////////////////////////////////
/*
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
using TokerNameSpace;
namespace TestSuite
{
    using Token = StringBuilder;

    class DemoToker
    {
        public bool testToker(string path)
        {
            Toker toker = new Toker();

            string fqf = System.IO.Path.GetFullPath(path);
            if (!toker.open(fqf))
            {
                Console.Write("\n can't open {0}\n", fqf);
                return false;
            }
            while (!toker.isDone())
            {
                Token tok = toker.getTok();
                Console.Write("\n -- line#{0, 4} : {1}", toker.lineCount(), tok);
            }
            toker.close();
            return true;
        }
    }

    class DemoToker2
    {
    public bool testToker2(string path)
    {
      Toker toker = new Toker();

      string fqf = System.IO.Path.GetFullPath(path);
      if (!toker.open(fqf))
      {
        Console.Write("\n can't open {0}\n", fqf);
        return false;
      }
      toker.setSpecialSingleChars(new List<string> { "<",">" });
      toker.setSpecialCharPairs(new List<string> { "<<","==" });

      while (!toker.isDone())
      {
        Token tok = toker.getTok();
        Console.Write("\n -- line#{0, 4} : {1}", toker.lineCount(), tok);
      }
     toker.close();
        
      return true;
    }
    
}
}
