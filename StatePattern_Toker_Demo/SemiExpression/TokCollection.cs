﻿////////////////////////////////////////////////////////////////////////////
// TokCollection.cs - Calls the SemiExpression                            //
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
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiExpressionNameSpace
{
    ///////////////////////////////////////////////////////////////////
    // ITokenCollection interface


    public interface ITokenCollection
    {

        bool CreateSemiObject(string name);

    }
    class TokCollection
    {
        bool CreateSemiObject(string SemiObj)
        {
            //      SemiExp SemiObj = new SemiExp();
            return true;
        }
        /*  static void Main(string[] args)
          {

          }
      }*/

    }
}
