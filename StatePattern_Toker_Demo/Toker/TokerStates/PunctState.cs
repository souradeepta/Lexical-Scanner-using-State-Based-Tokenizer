////////////////////////////////////////////////////////////////////////////
// PunctState.cs - Defines the Punctuator State token detection           //
// ver 1.0                                                                //
// Language:    C#, Visual Studio 2017, .Net Framework 4.7                // 
// Platform:    HP Pavillion , Win 10                                     //
// Application: Pr#2 , CSE681, Fall 2018                                  //
// Authors:  -  Jim Fawcett,                                              // 
//                CSE681 - Software Modeling and Analysis, Fall 2018      // 
//           -  Souradeepta Biswas,                                       // 
//                CSE681 - Software Modeling and Analysis, Fall 2018      //
//                sobiswas@syr.edu                                        //
////////////////////////////////////////////////////////////////////////////
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
    // PunctState class
    // - extracts contiguous punctuation chars as a token

    public class PunctState : TokenState
    {
        public PunctState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isPunctuation(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return (!Char.IsWhiteSpace(ch) && !Char.IsLetterOrDigit(ch) && !context_.dqs_.isDoubleQuote(ch) && !context_.sqs_.isSingleQuote(ch) && !context_.ccs_.isCComment(ch) && !context_.cppcs_.isCppComment(ch) && !context_.dps_.isDoublePunct(ch) && !context_.sps_.isSinglePunct(ch) );
        }
        //----< keep extracting until get none-punctuator >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());       

            while (isPunctuation(context_.src.peek()))  
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }
}
