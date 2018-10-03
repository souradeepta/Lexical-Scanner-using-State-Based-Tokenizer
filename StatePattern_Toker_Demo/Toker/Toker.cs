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
 * AlphaState.cs
 * PunctState.cs
 * WhiteSpaceState.cs
 * 
 * Maintenance History
 * -------------------
 * ver 1.3 : 03 Oct 2018
 * - Seperated all classes for encapsulation
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

namespace Toker
{
  using Token = StringBuilder;

  ///////////////////////////////////////////////////////////////////
  // ITokenSource interface
  // - Declares operations expected of any source of tokens
  // - Typically we would use either files or strings.  This demo
  //   provides a source only for Files, e.g., TokenFileSource, below.

  public interface ITokenSource
  {
    bool open(string path);
    void close();
    int next();
    int peek(int n = 0);
    bool end();
    int lineCount { get; set; }
  }

  ///////////////////////////////////////////////////////////////////
  // ITokenState interface
  // - Declares operations expected of any token gathering state

  public interface ITokenState
  {
    Token getTok();
    bool isDone();
  }

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
  

 

  
  ///////////////////////////////////////////////////////////////////
  // Derived State Classes
  /* - WhiteSpaceState          Token with space, tab, and newline chars
   * - AlphaNumState            Token with letters and digits
   * - PunctuationState         Token holding anything not included above
   * ----------------------------------------------------------------
   * - Each state class accepts a reference to the context in its
   *   constructor and saves in its inherited context_ property.
   * - It is only required to provide a getTok() method which
   *   returns a token conforming to its state, e.g., whitespace, ...
   * - getTok() assumes that the TokenSource's first character 
   *   matches its type e.g., whitespace char, ...
   * - The nextState() method ensures that the condition, above, is
   *   satisfied.
   * - The getTok() method promises not to extract characters from
   *   the TokenSource that belong to another state.
   * - These requirements lead us to depend heavily on peeking into
   *   the TokenSource's content.
   */
  
  
  
  

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
      msg.Append("\n  Some things this demo does not do for CSE681 Project #2:");
      msg.Append("\n  - collect comments as tokens");
      msg.Append("\n  - collect double quoted strings as tokens");
      msg.Append("\n  - collect single quoted strings as tokens");
      msg.Append("\n  - collect specified single characters as tokens");
      msg.Append("\n  - collect specified character pairs as tokens");
      msg.Append("\n  - integrate with a SemiExpression collector");
      msg.Append("\n  - provide the required package structure");
      msg.Append("\n");

      Console.Write(msg);

      testToker("../../Test.txt");
     // testToker("../../Toker.cs");

      Console.Write("\n\n");
            Console.ReadLine(); 
    }
  }
}
  
#endif
