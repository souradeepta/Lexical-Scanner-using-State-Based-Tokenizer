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
            SemiExp Semitest = new SemiExp();

            SemiExp test = new SemiExp();
            test.returnNewLines = true;
            test.displayNewLines = true;

            string testFile = path;
            //  string testFile = "../../testSemi.txt";
            if (!test.open(testFile))
                Console.Write("\n  Can't open file {0}", testFile);
          //  Console.Write("\n  processing file: {0}\n", testFile);
            while (test.getSemi())
                test.display();
            return true;
        }
    }
}
