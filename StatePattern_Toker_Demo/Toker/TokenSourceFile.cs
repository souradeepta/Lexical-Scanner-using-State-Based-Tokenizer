////////////////////////////////////////////////////////////////////////////
// TokenSourceFile.cs - Uses Token Source File                            //
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
        private System.IO.StreamReader fs_;           
        private List<int> charQ_ = new List<int>();   
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
        
        public int next()
        {
            int ch;
             if (charQ_.Count == 0)  
            {
                if (end())
                    return -1;
                ch = fs_.Read();
            }
            else                    
            {
                ch = charQ_[0];
                charQ_.Remove(ch);
            }
            if ((char)ch == '\n')  
                ++lineCount;
            return ch;
        }
        //----< peek n ints into source without extracting them >--------
       
        public int peek(int n = 0)
        {
            if (n < charQ_.Count)  
            {
                return charQ_[n];
            }
            else                  
            {
                for (int i = charQ_.Count; i <= n; ++i)
                {
                    if (end())
                        return -1;
                    charQ_.Add(fs_.Read());  
                }
                return charQ_[n];   
            }
        }
        //----< reached the end of the file stream? >--------------------

        public bool end()
        {
            return fs_.EndOfStream;
        }
    }
}
