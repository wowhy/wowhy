//-----------------------------------------------------------------------
// <copyright file="Operator.cs" company="Howonder">
//     Copyright © Howonder 2015. All rights reserved.
// </copyright>
// <summary></summary>
//-----------------------------------------------------------------------
namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public enum Token
    {
        End = 0,
        Name,
        Number,

        Plus = '+',
        Minus = '-',
        Mul = '*',
        Div = '/',
        Mod = '%',

        Print = ';',
        Assign = '=',
        LP = '(',
        RP = ')'
    }
}
