///////////////////////////////////////////////////////////////////////////////
// SinglePunctCharState.cs - Defines the State for single special punctuators detection //
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
    // SinglePunctCharState class
    // - extracts special single characters as a token
    class SinglePunctCharState : TokenState
    {
        public SinglePunctCharState(TokenContext context)
        {
            context_ = context;
        }

        public bool isSinglePunct(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            string single = ((char)nextItem).ToString();
            bool result = context_.SpecialSingleCharsList.Contains(single);
       
            return result;
        }

        override public Token getTok()
        {
            Token tok = new Token();
        
            if (isSinglePunct(context_.src.peek()))
            {
               tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
}

