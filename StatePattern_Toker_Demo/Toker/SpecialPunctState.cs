///////////////////////////////////////////////////////////////////////////////
// SpecialPunctState.cs - Defines the State for double punctuators detection//
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

namespace TokerNameSpace
{
    using Token = StringBuilder;

    ///////////////////////////////////////////////////////////////////
    // SpecialPunctState class
    // - extracts special double characters as a token
    class SpecialPunctState : TokenState
    {
        public SpecialPunctState(TokenContext context)
        {
            context_ = context;
        }
        
        public bool isSpecialPunct(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char first = (char)nextItem;
            StringBuilder opr = new StringBuilder();
                opr.Append(first.ToString());
            opr.Append(((char)context_.src.peek(1)).ToString());

            string[] opers = new string[]
            {
                 "!=", "==", ">=", "<=", "&&", "||", "--", "++", "::",
                  "+=", "-=", "*=", "/=", "%=", "&=", "^=", "|=", "<<", ">>"
            };

            if (opers.Contains(opr.ToString()))
                return true;
            else
                return false;
          /*  char second = (char)context_.src.peek(1);
            StringBuilder testDouble = new StringBuilder();
            testDouble.Append(first).Append(second);
            foreach (string oper in opers)
            if (oper.Equals(testDouble.ToString()))
                  return true;
            return false;*/
        }

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());
          while (isSpecialPunct(context_.src.peek()))
          {
               tok.Append((char)context_.src.next());
          }
            tok.Append((char)context_.src.next());

            return tok;
        }
    }
}

