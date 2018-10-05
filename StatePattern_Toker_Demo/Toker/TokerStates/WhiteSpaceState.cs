////////////////////////////////////////////////////////////////////////////////
// WhiteSpaceState.cs - Defines the Whitespace State token detection         //
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
 * AlphaState.cs
 * PunctState.cs
 * WhiteSpaceState.cs
 * CCommentState.cs
 * CppCommentState.cs
 * SingleQuoteState.cs
 * DoubleQuoteState.cs
 * SpecialPunctState.cs
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

namespace TokerNameSpace
{
    using Token = StringBuilder;

    ///////////////////////////////////////////////////////////////////
    // WhiteSpaceState class
    // - extracts contiguous whitespace chars as a token
    // - will be thrown away by tokenizer

    public class WhiteSpaceState : TokenState
    {
        public WhiteSpaceState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isWhiteSpace(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsWhiteSpace(ch);
        }
        //----< keep extracting until get none-whitespace >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());     // first is WhiteSpace

            while (isWhiteSpace(context_.src.peek()))  // stop when non-WhiteSpace
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
}
