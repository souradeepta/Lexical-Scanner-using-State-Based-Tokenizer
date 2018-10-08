////////////////////////////////////////////////////////////////////////////////
// SemiInterfaces.cs - List of all interfaces in the SemiExpression package  //
// ver 1.0                                                                  //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2018         //
// Souradeepta Biswas, CSE681 - Software Modeling and Analysis, Fall 2018 //
///////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * SemiExp provides, via class SemiExp, facilities to extract semiExpressions.
 * A semiExpression is a sequence of tokens that is just the right amount
 * of information to parse for code analysis.  SemiExpressions are token
 * sequences that end in "{" or "}" or ";
 * 
 *  Class Operations:
 * -------------------
 * - Declares operations expected of any source of tokens
 * - Typically we would use either files or strings.  
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
 * ver 1.0 : 5 0ct 2018
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiExpressionNameSpace
{
    public interface ISemiSource
    {
        int FindFirst(string str);
        int FindLast(string str);
        bool open(string filename);
        void close();
        bool isWhiteSpace(string tok);
        void trim();
        bool insert(int loc, string tok);
        void Add(string[] source);
        bool initialize();
        void flush();
        void display();
        string displayStr();
        bool getSemi();
      
    }
}
