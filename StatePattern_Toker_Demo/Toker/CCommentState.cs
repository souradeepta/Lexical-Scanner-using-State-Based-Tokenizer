///////////////////////////////////////////////////////////////////////////////
// CCommentState.cs - Defines the State for c style comment detection       //
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
    // CCommentState class
    // - extracts c comment style characters as a token
    class CCommentState : TokenState
    {
        public CCommentState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        public bool isCComment(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            if (ch == '\/')

                return true;
            else
                return false;
        }
        //----< keep extracting until get none-in-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          // first is alpha
            //char terminator_quote = (char)context_.src.peek();
            //while (terminator_quote != '\'')    // stop when non-alpha
            while ((char)context_.src.peek() != '\"')
            {
                tok.Append((char)context_.src.next());
            }
            tok.Append((char)context_.src.next());
            return tok;
        }
    }
}
}
