﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemiExpressionNameSpace;

namespace TokenCollectionNameSpace
{
    ///////////////////////////////////////////////////////////////////
    // ITokenSource interface
    // - Declares operations expected of any source of tokens
    // - Typically we would use either files or strings.  This demo
    //   provides a source only for Files, e.g., TokenFileSource, below.

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
