///////////////////////////////////////////////////////////////////////////////
// TokenState.cs - Defines the Parent class of all States                   //
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
    // TokenState class
    // - base for all the tokenizer states

    public abstract class TokenState : ITokenState
    {

        internal TokenContext context_ { get; set; }  // derived classes store context ref here

        //----< delegate source opening to context's src >---------------

        public bool open(string path)
        {
            return context_.src.open(path);
        }
        //----< pass interface's requirement onto derived states >-------

        public abstract Token getTok();

        //----< derived states don't have to know about other states >---

        static public TokenState nextState(TokenContext context)
        {
            int nextItem = context.src.peek();
            if (nextItem < 0)
                return null;
            char ch = (char)nextItem;

            if (Char.IsWhiteSpace(ch))
                return context.ws_;
            if (Char.IsLetterOrDigit(ch))
                return context.as_;


            // Test for strings and comments here since we don't
            // want them classified as punctuators.

            // toker's definition of punctuation is anything that
            // is not whitespace and is not a letter or digit
            // Char.IsPunctuation is not inclusive enough
            if(context.sqs_.isSingleQuote(ch))
                return context.sqs_;
            if (context.dqs_.isDoubleQuote(ch))
                return context.dqs_;
            if(context.ccs_.isCComment(ch))
                return context.ccs_;
            if(context.cppcs_.isCppComment(ch))
                return context.cppcs_;
            if (context.sps_.isSpecialPunct(ch))
                return context.sps_;
            return context.sps_;
        }
        //----< has tokenizer reached the end of its source? >-----------

        public bool isDone()
        {
            if (context_.src == null)
                return true;
            return context_.src.end();
        }
    }
}
