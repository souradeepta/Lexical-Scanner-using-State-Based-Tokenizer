////////////////////////////////////////////////////////////////////////////
// DemoSemi.cs - Contains a test call class for semi expression           //
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
using SemiExpressionNameSpace;

namespace TestSuite
{
    class DemoSemi
    {
        public bool testSemi(string path)
        {
            SemiExp test = new SemiExp();
            string testFile = path;
            if (!test.open(testFile))
                Console.Write("\n  Can't open file {0}", testFile);
                      while (test.getSemi())
                test.display();
            return true;
        }
    }
}
