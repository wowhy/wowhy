namespace HyLibrary.Parser.Expr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    internal class AToken
    {
        internal int start_pos;
        internal TokenType tok_type;
        internal AnOperator op;
        internal string value;

        internal AToken(int start_pos, TokenType tok_type, AnOperator op, string value)
        {
            this.start_pos = start_pos;
            this.tok_type = tok_type;
            this.op = op;
            this.value = value;
        }
    }
}