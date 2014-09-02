namespace HyLibrary.Parser.Calc
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