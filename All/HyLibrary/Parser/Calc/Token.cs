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
        Name,
        Number,

        Plus = '+',
        Minus = '-',
        Mul = '*',
        Div = '/',
        Mod = '%',

        End = ';',
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