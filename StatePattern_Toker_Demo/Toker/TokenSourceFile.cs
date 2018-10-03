///////////////////////////////////////////////////////////////////////////////
// TokenSourceFile.cs - Uses Token Source File                              //
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
    // TokenSourceFile class
    // - extracts integers from token source
    // - Streams often use terminators that can't be represented by
    //   a character, so we collect all elements as ints
    // - keeps track of the line number where a token is found
    // - uses StreamReader which correctly handles byte order mark
    //   characters and alternate text encodings.

    public class TokenSourceFile : ITokenSource
    {
        public int lineCount { get; set; } = 1;
        private System.IO.StreamReader fs_;           // physical source of text
        private List<int> charQ_ = new List<int>();   // enqueing ints but using as chars
        private TokenContext context_;

        public TokenSourceFile(TokenContext context)
        {
            context_ = context;
        }
        //----< attempt to open file with a System.IO.StreamReader >-----

        public bool open(string path)
        {
            try
            {
                fs_ = new System.IO.StreamReader(path, true);
                context_.currentState_ = TokenState.nextState(context_);
            }
            catch (Exception ex)
            {
                Console.Write("\n  {0}\n", ex.Message);
                return false;
            }
            return true;
        }
        //----< close file >---------------------------------------------

        public void close()
        {
            fs_.Close();
        }
        //----< extract the next available integer >---------------------
        /*
         *  - checks to see if previously enqueued peeked ints are available
         *  - if not, reads from stream
         */
        public int next()
        {
            int ch;
            if (charQ_.Count == 0)  // no saved peeked ints
            {
                if (end())
                    return -1;
                ch = fs_.Read();
            }
            else                    // has saved peeked ints, so use the first
            {
                ch = charQ_[0];
                charQ_.Remove(ch);
            }
            if ((char)ch == '\n')   // track the number of newlines seen so far
                ++lineCount;
            return ch;
        }
        //----< peek n ints into source without extracting them >--------
        /*
         *  - This is an organizing prinicple that makes tokenizing easier
         *  - We enqueue because file streams only allow peeking at the first int
         *    and even that isn't always reliable if an error occurred.
         *  - When we look for two punctuator tokens, like ==, !=, etc. we want
         *    to detect their presence without removing them from the stream.
         *    Doing that is a small part of your work on this project.
         */
        public int peek(int n = 0)
        {
            if (n < charQ_.Count)  // already peeked, so return
            {
                return charQ_[n];
            }
            else                  // nth int not yet peeked
            {
                for (int i = charQ_.Count; i <= n; ++i)
                {
                    if (end())
                        return -1;
                    charQ_.Add(fs_.Read());  // read and enqueue
                }
                return charQ_[n];   // now return the last peeked
            }
        }
        //----< reached the end of the file stream? >--------------------

        public bool end()
        {
            return fs_.EndOfStream;
        }
    }
}
