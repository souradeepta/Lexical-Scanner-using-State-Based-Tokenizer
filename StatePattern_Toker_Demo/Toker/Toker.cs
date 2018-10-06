////////////////////////////////////////////////////////////////////////////
// Toker.cs  -  Defines the toker class with the getTok function          //    
// ver 1.3                                                                //
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
 * Public Interface
 * ================
 * Toker toker = new Toker();      // constructs Toker object
 * public bool open(string path);  // attempt to open source of tokens 
 * public void close()             // close source of tokens
 * public Token getTok()           // extract a token from source
 * public bool isDone()            // checks if toker reached end of source
 * public int next()               // extracts the next available integer
 * public int peek(int n = 0)      // peek n ints into source without extracting them
 * public bool end()               // checks end of file stream
 * public bool isDone()            // checks if tokenizer has reached the end of its source
 *
 * Maintenance History
 * -------------------
 * ver 1.3 : 06 Oct 2018
 * - Seperated all classes for encapsulation added new states - CppCommentState, 
 *   CCommentState, DoubleQuoteState, SingleQuoteState, DoublePunctCharState
 * ver 1.2 : 03 Sep 2018
 * - added comments just above the definition of derived states, near line #209
 * ver 1.1 : 02 Sep 2018
 * - Changed Toker, TokenState, TokenFileSource, and TokenContext to fix a bug
 *   in setting the initial state.  These changes are cited, below.
 * - Removed TokenState state_ from toker so only TokenContext instance manages 
 *   the current state.
 * - Changed TokenFileSource() to TokenFileSource(TokenContext context) to allow the 
 *   TokenFileSource instance to set the initial state correctly.
 * - Changed TokenState.nextState() to static TokenState.nextState(TokenContext context).
 *   That allows TokenFileSource to use nextState to set the initial state correctly.
 * - Changed TokenState.nextState(context) to treat everything that is not whitespace
 *   and is not a letter or digit as punctuation.  Char.IsPunctuation was not inclusive
 *   enough for Toker.
 * - changed current_ to currentState_ for readability
 * ver 1.0 : 30 Aug 2018
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
    // Toker class
    // - applications need to use only this class to collect tokens

    public class Toker
  {
    private TokenContext context_;
        
            

    //----< initialize state machine >-------------------------------

    public Toker()
    {
      context_ = new TokenContext();
    }
    //----< attempt to open source of tokens >-----------------------
    /*
     * If src is successfully opened, it uses TokenState.nextState(context_)
     * to set the initial state, based on the source content.
     */
    public bool open(string path)
    {
      TokenSourceFile src = new TokenSourceFile(context_);
      context_.src = src;
      return src.open(path);
    }
    //----< close source of tokens >---------------------------------

    public void close()
    {
      context_.src.close();
    }
    //----< extract a token from source >----------------------------

    private bool isWhiteSpaceToken(Token tok)
    {
      return (tok.Length > 0 && Char.IsWhiteSpace(tok[0]));
    }

    public Token getTok()
    {
      Token tok = null;
      while(!isDone())
      {
        tok = context_.currentState_.getTok();
        context_.currentState_ = TokenState.nextState(context_);
        if (!isWhiteSpaceToken(tok))
          break;
      }
      return tok;
    }
    //----< has Toker reached end of its source? >-------------------

    public bool isDone()
    {
      if (context_.currentState_ == null)
        return true;
      return context_.currentState_.isDone();
    }
    public int lineCount() { return context_.src.lineCount; }
  }
  
#if(TEST_TOKER)

  class DemoToker
  {
    static bool testToker(string path)
    {
      Toker toker = new Toker();

      string fqf = System.IO.Path.GetFullPath(path);
      if (!toker.open(fqf))
      {
        Console.Write("\n can't open {0}\n", fqf);
        return false;
      }
      else
      {
        Console.Write("\n  processing file: {0}", fqf);
      }
      while (!toker.isDone())
      {
        Token tok = toker.getTok();
        Console.Write("\n -- line#{0, 4} : {1}", toker.lineCount(), tok);
      }
      toker.close();
      return true;
    }
    static void Main(string[] args)
    {
      Console.Write("\n  Demonstrate Toker class");
      Console.Write("\n =========================");

      StringBuilder msg = new StringBuilder();

      msg.Append("\n");

      Console.Write(msg);
      
        testToker("../../TestSemi.txt");
   
 //   testToker("../../TestTokenizer.txt");
     //testToker("../../Test.txt");
          //  toker.SinglePunctCharState = "+";
     //testToker("../../Test.txt");
      Console.Write("\n\n");
            Console.ReadLine(); 
    }
  }
}
#endif
