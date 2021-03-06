﻿////////////////////////////////////////////////////////////////////////////
// Semi.cs   - filters token stream and collects semiExpressions          //
// ver 2.9                                                                //
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
 * SemiExp provides, via class SemiExp, facilities to extract semiExpressions.
 * A semiExpression is a sequence of tokens that is just the right amount
 * of information to parse for code analysis.  SemiExpressions are token
 * sequences that end in "{" or "}" or ";
 * 
 * SemiExp works with a private Toker object attached to a specified file.
 * It provides a get() function that extracts semiExpressions from the file
 * while filtering out comments and merging quotes into single tokens.
 * Required Files:
 * ---------------
 * SemiExp.cs
 * SemiInterfaces.cs
 * Toker.cs
 * TokenContext.cs
 * TokenSourceFile.cs
 * TokenState.cs
 * TokerInterfaces.cs
 *  
 * Public Interface
 * ================
 * SemiExp semi = new SemiEx;();      // constructs SemiExp object
 * if(semi.open(fileName)) ...        // attaches semi to specified file
 * semi.close();                      // closes file stream
 * if(semi.Equals(se)) ...            // do these semiExps have same tokens?
 * if(getSemi()) ...                  // extracts and stores next semiExp
 * int len = semi.count;              // length property
 * string tok = semi[2];              // access a semi token
 * string tok = semi[1];              // extract token
 * semi.flush();                      // removes all tokens
 * semi.initialize();                 // adds ";" to empty semi-expression
 * semi.insert(2,tok);                // inserts token as third element
 * semi.Add(tok);                     // appends token
 * semi.Add(tokArray);                // appends array of tokens
 * semi.display();                    // sends tokens to Console
 * string show = semi.displayStr();   // returns tokens as single string
  *                                    
 * Maintenance History
 * ===================
 * ver 2.9 : 03 Oct 18
 * - works along with a new state-based tokenizer now and handles a few new states and comments
 * ver 2.8 : 14 Oct 14
 * - fixed bug in extract that caused tokenizing of multiline string
 *   to loop endlessly
 * - reset lineCount in Attach function
 * ver 2.7 : 21 Sep 14
 * - made returning comments optional
 * - fixed handling of @"..." strings
 * ver 2.6 : 19 Sep 14
 * - stopped returning comments in getTok function
 * ver 2.5 : 14 Aug 14
 * - added patch to handle @"..." string format
 * ver 2.4 : 24 Sep 11
 * - added a thrown exception if extract encounters a string with the 
 *   substring "@.  This should be handled but raises two many changes
 *   to fix immediately.
 * ver 2.3 : 05 Sep 11
 * - fixed bug collecting C Comments in eatCComment()
 * - fixed bug where token contained embedded newline, now broken
 *   into seperate tokens
 * - fixed ackward display formatting
 * - replaced untyped ArrayList with generic List<string> 
 * - added lineCount property
 * ver 2.2 : 10 Jun 08
 * - added IsGrammarPunctuation to make tokenizer treat underscore
 *   as an ASCII char rather than a punctuator and used that in
 *   fillBuffer and eatASCII
 * ver 2.1 : 14 Jun 05
 * - fixed newline handling bug in buffer filling routines:
 *   readLine, getLine, fillbuffer
 * - fixed newline handling bug in extractComment
 * ver 2.0 : 30 May 05
 * - added extraction of comments and quotes as tokens
 * - added openString(...) to attach tokenizer to string
 * ver 1.1 : 21 Sep 04
 * - added toker.close() in test stub
 * - added processing for all command line args
 * ver 1.0 : 31 Aug 03
 * - first release
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokerNameSpace;

namespace SemiExpressionNameSpace

{
    public class SemiExp : ISemiSource, ITokenCollection
    {
        Toker toker = null;
        List<string> semiExp = null;
        string currTok = "";
        string prevTok = "";
        //----< implements ITokCollection iterfaces >------------------------

        public bool CallSemiExpression()
        {
            while (this.getSemi())
                this.display();
            return true;
        }
        //----< line count property >----------------------------------------

        public SemiExp()
        {
            toker = new Toker();
            semiExp = new List<string>();
        }
        //---< pos of first str in semi-expression if found, -1 otherwise >--

        public int FindFirst(string str)
        {
            for (int i = 0; i < count - 1; ++i)
                if (this[i] == str)
                    return i;
            return -1;
        }
        //---< pos of last str in semi-expression if found, -1 otherwise >--- 

        public int FindLast(string str)
        {
            for (int i = this.count - 1; i >= 0; --i)
                if (this[i] == str)
                    return i;
            return -1;
        }
        //----< opens member tokenizer with specified file >-----------------

        public bool open(string fileName)
        {
            return toker.open(fileName);
        }
        //----< close file stream >------------------------------------------

        public void close()
        {
            toker.close();
        }
        //----< is this the last token in the current semiExpression? >------

        bool isTerminator(string tok)
        {
            switch (tok)
            {
                case ";": return true;
                case "{": return true;
                case "}": return true;
                case ">":
                    if (this.FindFirst("#") != -1)
                        return true;
                    return false;
                case "\n":
                    if (this.FindFirst("#") != -1)
                        return true;
                    return false;
                case "\r\n":
                    return true;


                default: return false;
            }
        }
        //----< get next token, saving previous token >----------------------

        string get()
        {
            while (!toker.isDone())
            {
                prevTok = currTok;
                currTok = toker.getTok().ToString();

                return currTok;
            }
            toker.close();
            return null;
        }
        //----< is this character a punctuator> >----------------------------

        bool IsPunc(char ch)
        {
            return (Char.IsPunctuation(ch) || Char.IsSymbol(ch));
        }

        //----< collect semiExpression from filtered token stream >----------

        public bool isWhiteSpace(string tok)
        {
            Char ch = tok[0];
            return Char.IsWhiteSpace(tok[0]);
        }

        public void trim()
        {
            SemiExp temp = new SemiExp();
            foreach (string tok in semiExp)
            {
                if (isWhiteSpace(tok))
                    continue;
                temp.Add(tok);
            }
            semiExp = temp.semiExp;
        }

        public bool getSemi()
        {
            semiExp.RemoveRange(0, semiExp.Count);
            do
            {
                get();
                if (currTok == "")
                    return false;

                semiExp.Add(currTok);
            } while (!isTerminator(currTok) || count == 0);

            trim();

            if (semiExp.Contains("for"))
            {
                SemiExp se = clone();
                getSemi();
                se.Add(semiExp.ToArray());
                getSemi();
                se.Add(semiExp.ToArray());
                semiExp.Clear();
                for (int i = 0; i < se.count; ++i)
                    semiExp.Add(se[i]);
            }
            return (semiExp.Count > 0);
        }
        //----< get length property >----------------------------------------

        public int count
        {
            get { return semiExp.Count; }
        }
        //----< indexer for semiExpression >---------------------------------

        public string this[int i]
        {
            get { return semiExp[i]; }
            set { semiExp[i] = value; }
        }
        //----< insert token - fails if out of range and returns false>------

        public bool insert(int loc, string tok)
        {
            if (0 <= loc && loc < semiExp.Count)
            {
                semiExp.Insert(loc, tok);
                return true;
            }
            return false;
        }
        //----< append token to end of semiExp >-----------------------------

        public SemiExp Add(string token)
        {
            semiExp.Add(token);
            return this;
        }
        //----< load semiExp from array of strings >-------------------------

        public void Add(string[] source)
        {
            foreach (string tok in source)
                semiExp.Add(tok);
        }
        //--< initialize semiExp with single ";" token - used for testing >--

        public bool initialize()
        {
            if (semiExp.Count > 0)
                return false;
            semiExp.Add(";");
            return true;
        }
        //----< remove all contents of semiExp >-----------------------------

        public void flush()
        {
            semiExp.RemoveRange(0, semiExp.Count);
        }

        //----< display semiExpression on Console >--------------------------

        public void display()
        {
            Console.Write("\n -- ");
            Console.Write(displayStr());
        }
        //----< return display string >--------------------------------------

        public string displayStr()
        {

            StringBuilder disp = new StringBuilder("");
            foreach (string tok in semiExp)
            {

                disp.Append(tok);
                if (tok.IndexOf('\n') != tok.Length - 1)
                    disp.Append(" ");
            }
            return disp.ToString();
        }

        //----< make a copy of semiEpression >-------------------------------

        public SemiExp clone()
        {
            SemiExp copy = new SemiExp();
            for (int i = 0; i < count; ++i)
            {
                copy.Add(this[i]);
            }
            return copy;
        }

#if (TEST_SEMI)

        //----< test stub >--------------------------------------------------
        class DemoSemi
        {
            public bool testSemi(string path)
            {
                SemiExp Semitest = new SemiExp();

                SemiExp test = new SemiExp();
                string testFile = path;
                if (!test.open(testFile))
                    Console.Write("\n  Can't open file {0}", testFile);
                while (test.getSemi())
                    test.display();
                return true;
            }
            static void Main(string[] args)
            {
                Console.Write("\n  Testing semiExp Operations");
                Console.Write("\n ============================\n");
                DemoSemi s1 = new DemoSemi();
                s1.testSemi("../../testSemi.txt");
            }
#endif
        }
    }
}
