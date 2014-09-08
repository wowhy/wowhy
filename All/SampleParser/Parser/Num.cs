namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Num : Token
    {
        public int Value { get; protected set; }

        public Num(int v)
            : base((int)Parser.KeyTag.NUM)
        {
            this.Value = v;
        }

        public override string ToString()
        {
            return string.Format("{{ tag: {0}, value: {1} }}", this.TagName(), this.Value);
        }
    }
}