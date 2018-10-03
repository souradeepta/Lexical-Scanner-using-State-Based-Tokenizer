///////////////////////////////////////////////////////////////////////////////
// TokenContext.cs - Uses Token Context                                     //
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
            // more states here
            currentState_ = ws_;
        }
        internal WhiteSpaceState ws_ { get; set; }
        internal PunctState ps_ { get; set; }
        internal AlphaState as_ { get; set; }
        // more states here

        internal TokenState currentState_ { get; set; }
        internal ITokenSource src { get; set; }  // can hold any derived class
    }
    }

