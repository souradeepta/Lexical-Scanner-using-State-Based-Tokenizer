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

    public void setSpecialSingleChars(List<string> s)
    {
        List<string> st =s;
        context_.SpecialSingleCharsList.Add("s");
        context_.SpecialSingleCharsList.Clear();
        context_.SpecialSingleCharsList.AddRange(st);
    }
    public void setSpecialCharPairs(List<string> s)
    {
        List<string> st =s;
        context_.SpecialDoubleCharsList.Add("s");
        context_.SpecialDoubleCharsList.Clear();
        context_.SpecialDoubleCharsList.AddRange(st);
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
     /*
    public bool setSpecialSingleChars()
    {
        string input= null;
        while (input != "")
        {
            Console.WriteLine("Please enter another single character token: ");
            input = Console.ReadLine();
            context_.SpecialSingleCharsList.Clear();
            context_.SpecialSingleCharsList.Add(input);
        }
        return true;
    }
    
    public bool setSpecialCharPairs()
    {
        string input = null;
        while (input != "")
        {
           Console.WriteLine("Please enter another double character token: ");
           input = Console.ReadLine();
           context_.SpecialSingleCharsList.Clear();
           context_.SpecialDoubleCharsList.Add(input);
        }
        return true;
           
    }*/
  }
    ///////////////////////////////////////////////////////////////////
    // TokenContext class
    // - holds all the tokenizer states
    // - holds source of tokens
    // - internal qualification limits access to this assembly

    public class TokenContext
    {
      public List<string> SpecialSingleCharsList { get; set; }
      public List<string> SpecialDoubleCharsList { get; set; }

      internal TokenContext()
      {
         ws_ = new WhiteSpaceState(this);
         ps_ = new PunctState(this);
         as_ = new AlphaState(this);
         sqs_ = new SingleQuoteState(this);
         dqs_ = new DoubleQuoteState(this);
         ccs_ = new CCommentState(this);
         cppcs_ = new CppCommentState(this);
         dps_ = new DoublePunctCharState(this);
         sps_ = new SinglePunctCharState(this);

         currentState_ = ws_;

         SpecialSingleCharsList = new List<string> { "<", ">", "[", "]", "(", ")", "{", "}", ":", "=", "+", "-", "*" };
                                                       
         SpecialDoubleCharsList = new List<string> {"!=", "==", ">=", "<=", "&&", "||", "--", "++", "::","+=", "-=", "*=",
                                                             "/=", "%=","&=", "^=", "|=", "<<", ">>" };
      }
      internal WhiteSpaceState ws_ { get; set; }
      internal PunctState ps_ { get; set; }
      internal AlphaState as_ { get; set; }
      internal SingleQuoteState sqs_ { get; set; }
      internal DoubleQuoteState dqs_ { get; set; }
      internal CCommentState ccs_ { get; set; }
      internal CppCommentState cppcs_ { get; set; }
      internal SinglePunctCharState sps_ { get; set; }
      internal DoublePunctCharState dps_ { get; set; }

      internal TokenState currentState_ { get; set; }
      internal ITokenSource src { get; set; }  
    }

    ///////////////////////////////////////////////////////////////////
    // TokenState class
    // - base for all the tokenizer states

    public abstract class TokenState : ITokenState
    {
      internal TokenContext context_ { get; set; }  

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
         if(context.sqs_.isSingleQuote(ch))
              return context.sqs_;
         if (context.dqs_.isDoubleQuote(ch))
              return context.dqs_;
         if(context.ccs_.isCComment(ch))
              return context.ccs_;
         if(context.cppcs_.isCppComment(ch))
              return context.cppcs_;
         if (context.dps_.isDoublePunct(ch))
              return context.dps_;
         if (context.sps_.isSinglePunct(ch))
              return context.sps_;
                  
         return context.ps_;
        }
        //----< has tokenizer reached the end of its source? >-----------

        public bool isDone()
        {
          if (context_.src == null)
              return true;
          return context_.src.end();
        }
    }
    ///////////////////////////////////////////////////////////////////
    // WhiteSpaceState class
    // - extracts contiguous whitespace chars as a token
    // - will be thrown away by tokenizer

    public class WhiteSpaceState : TokenState
    {
        public WhiteSpaceState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isWhiteSpace(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsWhiteSpace(ch);
        }
        //----< keep extracting until get none-whitespace >--------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());     

            while (isWhiteSpace(context_.src.peek()))  
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }

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
            return (!Char.IsWhiteSpace(ch) && !Char.IsLetterOrDigit(ch) && !context_.dqs_.isDoubleQuote(ch) && !context_.sqs_.isSingleQuote(ch) && !context_.ccs_.isCComment(ch) && !context_.cppcs_.isCppComment(ch) && !context_.dps_.isDoublePunct(ch) && !context_.sps_.isSinglePunct(ch));
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

    ///////////////////////////////////////////////////////////////////
    // AlphaState class
    // - extracts contiguous letter and digit chars as a token

    public class AlphaState : TokenState
    {
        public AlphaState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        bool isLetterOrDigit(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            return Char.IsLetterOrDigit(ch);
        }
        //----< keep extracting until get none-alpha >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());          

            while (isLetterOrDigit(context_.src.peek()))    
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }

    ///////////////////////////////////////////////////////////////////
    // CCommentState class
    // - extracts c style comments

    class CCommentState : TokenState
    {
        public CCommentState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        public bool isCComment(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            if (ch == '/')
            {
                char nextItem2 = (char)context_.src.peek(1);
                if (nextItem2 == '/')
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        //----< keep extracting until get none-in-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());
            while ((char)context_.src.peek() != '\r')
            {
                tok.Append((char)context_.src.next());
            }
            return tok;
        }
    }

    ///////////////////////////////////////////////////////////////////
    // CppCommentState class
    // - extracts c++ style comments

    class CppCommentState : TokenState
    {
        public CppCommentState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        public bool isCppComment(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            if (ch == '/')
            {
                char nextItem2 = (char)context_.src.peek(1);
                if (nextItem2 == '*')
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        //----< keep extracting until get none-in-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());
            while (((char)context_.src.peek() != '/'))
            {
                tok.Append((char)context_.src.next());
            }
            tok.Append((char)context_.src.next());
            tok = tok.Replace(Environment.NewLine, " ");
            return tok;
        }
    }

    ///////////////////////////////////////////////////////////////////
    // DoublePunctCharState class
    // - extracts special double punctuators


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
    ///////////////////////////////////////////////////////////////////
    // SinglePunctCharState class
    // - extracts special single punctuators

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

    ///////////////////////////////////////////////////////////////////
    // DoubleQuoteState class
    // - extracts double quoted strings

    class DoubleQuoteState : TokenState
    {
        public DoubleQuoteState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        public bool isDoubleQuote(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            
            if (ch == '\"')
                return true;
            else
                return false;
        }
        //----< keep extracting until get none-in-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());        
               
            while (!isDoubleQuote((char)context_.src.peek()))
            {
                tok.Append((char)context_.src.next());
            }
            tok.Append((char)context_.src.next());
            return tok;
        }
    }

    ///////////////////////////////////////////////////////////////////
    // SingleQuoteState class
    // - extracts single quoted strings

    class SingleQuoteState : TokenState
    {
        public SingleQuoteState(TokenContext context)
        {
            context_ = context;
        }
        //----< manage converting extracted ints to chars >--------------

        public bool isSingleQuote(int i)
        {
            int nextItem = context_.src.peek();
            if (nextItem < 0)
                return false;
            char ch = (char)nextItem;
            if (ch == '\'')
                return true;
            else
                return false;
        }
        //----< keep extracting until get none-in-quote >-------------------

        override public Token getTok()
        {
            Token tok = new Token();
            tok.Append((char)context_.src.next());         
            
            while ((char)context_.src.peek() != '\'')
            {
                tok.Append((char)context_.src.next());
            }
            tok.Append((char)context_.src.next());
            return tok;
        }

   

    
    }
#if (TEST_TOKER)

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
    
    class DemoToker2
    {
    static bool testToker2(string path)
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
      toker.setSpecialSingleChars(new List<string> { "<",">" });
      toker.setSpecialCharPairs(new List<string> { "<<","==" });

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
      testToker2("../../TestTokenizer.txt");

           
      Console.ReadLine();
    }
  }
}
}
#endif
