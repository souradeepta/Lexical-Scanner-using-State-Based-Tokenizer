////////////////////////////////////////////////////////////////////////////////
// Toker.cs - Defines the toker class with the getTok function               //
// ver 1.3                                                                  //
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
 * ver 1.3 : 03 Oct 2018
 * - Seperated all classes for encapsulation added new states - CppCommentState, 
 *   CCommentState, DoubleQuoteState, SingleQuoteState, SpecialPunctState
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
    private TokenContext context_;       // holds single instance of all states and token source

    //----< initialize state machine >-------------------------------

    public Toker()
    {
      context_ = new TokenContext();      // context is the glue that holds all of the state machine parts 
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
      
      testToker("../../TestTokenizer.txt");
     //testToker("../../Toker.cs");

      Console.Write("\n\n");
            Console.ReadLine(); 
    }
  }
}
#endif
