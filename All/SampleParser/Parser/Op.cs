namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Op : Token
    {
        public static readonly char[] OpCodes = new char[] 
        {
            '+','-','*','/','%'
        };

        public char OpCode { get; private set; }

        public Op(char op)
            : base((int)KeyTag.OP)
        {
            this.OpCode = op;
        }

        public override string ToString()
        {
            return string.Format("{{ tag: {0}, op_code: {1} }}", this.TagName(), this.OpCode);
        }
    }
}