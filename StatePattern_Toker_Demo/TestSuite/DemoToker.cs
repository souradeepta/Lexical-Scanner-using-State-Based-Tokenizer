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
            else
            {
                Console.Write("\n  processing file: {0}", fqf);
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
} 
