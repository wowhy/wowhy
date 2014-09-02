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
    internal enum TokenType
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

    internal struct Token
    {
        public TokenType type;
        public string text;
    }
}