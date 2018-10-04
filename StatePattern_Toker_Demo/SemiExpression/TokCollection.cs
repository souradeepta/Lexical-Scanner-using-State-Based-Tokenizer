using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiExpressionNameSpace
{
    ///////////////////////////////////////////////////////////////////
    // ITokenCollection interface
   

    public interface ITokenCollection
    {

        bool CreateSemiObject(string name);

    }
    class TokCollection
    {
        bool CreateSemiObject(string SemiObj)
        {
            SemiExp SemiObj = new SemiExp();
            return true;
        }
        static void Main(string[] args)
        {

        }
    }
   
}
