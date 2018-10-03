///////////////////////////////////////////////////////////////////////////////
// AlphaState.cs - Defines the Alphanumeric State token detection           //
// ver 1.0                                                                 //
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
 * AlphaState.cs
 * PunctState.cs
 * WhiteSpaceState.cs
 * 
 * Maintenance History
 * -------------------
 * ver 1.0 : 3 0ct 2018
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toker
{
    using Token = StringBuilder;

    ///////////////////////////////////////////////////////////////////
    // AlphaState class
    // - extracts contiguous letter and digit chars as a token

    public class AlphaState : TokenState
    {
        public AlphaState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isLetterOrDigit(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsLetterOrDigit(ch);
        }
        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is alpha

            while (isLetterOrDigit(context_.src.peek()))    // stop when non-alpha
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
}
