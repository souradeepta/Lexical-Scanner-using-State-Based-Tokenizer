﻿///////////////////////////////////////////////////////////////////////////////
// DoublePunctCharState.cs - Defines the State for double punctuators detection //
// ver 1.0                                                                   //
// Language:    C#, Visual Studio 2017, .Net Framework 4.7                   // 
// Platform:    HP Pavillion , Win 10                                        //
// Application: Pr#2 , CSE681, Fall 2018                                     //
// Authors:  -  Jim Fawcett,                                                 // 
//                CSE681 - Software Modeling and Analysis, Fall 2018         // 
//           -  Souradeepta Biswas,                                          // 
//                CSE681 - Software Modeling and Analysis, Fall 2018         //
//                sobiswas@syr.edu                                           //
///////////////////////////////////////////////////////////////////////////////
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
 * DoublePunctCharState.cs
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
    // DoublePunctCharState class
    // - extracts special double characters as a token
    class DoublePunctCharState : TokenState
    {
        public DoublePunctCharState(TokenContext context)
        {
            context_ = context;
        }
        
        public bool isDoublePunct(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char first = (char)nextItem;
            StringBuilder opr = new StringBuilder();
                opr.Append(first.ToString());
            opr.Append(((char)context_.src.peek(1)).ToString());
            
            if (context_.SpecialDoubleCharsList.Contains(opr.ToString()))
                return true;
            else 
                return false;
        }

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());
          while (isDoublePunct(context_.src.peek()))
          {
               tok.Append((char)context_.src.next());
          }
            tok.Append((char)context_.src.next());

            return tok;
        }
    }
}
