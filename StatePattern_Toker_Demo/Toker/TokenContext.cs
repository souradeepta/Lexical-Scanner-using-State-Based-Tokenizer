////////////////////////////////////////////////////////////////////////////
// TokenContext.cs - Uses Token Context                                   //
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
    ///////////////////////////////////////////////////////////////////
    // TokenContext class
    // - holds all the tokenizer states
    // - holds source of tokens
    // - internal qualification limits access to this assembly
    public class TokenContext
    {
        internal TokenContext()
        {
            ws_ = new WhiteSpaceState(this);
            ps_ = new PunctState(this);
            as_ = new AlphaState(this);
            sqs_ = new SingleQuoteState(this);
            dqs_ = new DoubleQuoteState(this);
            ccs_ = new CCommentState(this);
            cppcs_ = new CppCommentState(this);
            sps_ = new SpecialPunctState(this);

            currentState_ = ws_;
        }
        internal WhiteSpaceState ws_ { get; set; }
        internal PunctState ps_ { get; set; }
        internal AlphaState as_ { get; set; }
        internal SingleQuoteState sqs_ { get; set; }
        internal DoubleQuoteState dqs_ { get; set; }
        internal CCommentState ccs_ { get; set; }
        internal CppCommentState cppcs_ { get; set; }
        internal SpecialPunctState sps_ { get; set; }

        internal TokenState currentState_ { get; set; }
        internal ITokenSource src { get; set; }  
    }
    }

