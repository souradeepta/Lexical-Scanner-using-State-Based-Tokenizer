////////////////////////////////////////////////////////////////////////////////
// TokerInterfaces.cs - List of all interfaces in the Toker package          //
// ver 1.0                                                                  //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2018         //
// Souradeepta Biswas, CSE681 - Software Modeling and Analysis, Fall 2018 //
///////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * Demonstrates how to build a tokenizer based on the State Pattern.
 * 
 * Required Files:
 * ---------------
 * Toker.cs
 * TokenContext.cs
 * TokenSourceFile.cs
 * TokenState.cs
 * TokerInterfaces.cs
 * 
 * Maintenance History
 * -------------------
 * ver 1.0 : 5 0ct 2018
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokerNameSpace
{
    using Token = StringBuilder;
    ///////////////////////////////////////////////////////////////////
    // ITokenSource interface
    // - Declares operations expected of any source of tokens
    // - Typically we would use either files or strings.  

    public interface ITokenSource
    {
        bool open(string path);
        void close();
        int next();
        int peek(int n = 0);
        bool end();
        int lineCount { get; set; }
    }

    ///////////////////////////////////////////////////////////////////
    // ITokenState interface
    // - Declares operations expected of any token gathering state

    public interface ITokenState
    {
        Token getTok();
        bool isDone();
    }
}
