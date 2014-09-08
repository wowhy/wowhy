namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Word : Token
    {
        public string Lexeme { get; protected set; }

        public Word(int t, string s)
            : base(t)
        {
            this.Lexeme = s;
        }

        public override string ToString()
        {
            return string.Format("{{ tag: {0}, lexeme: {1} }}", this.TagName(), this.Lexeme);
        }
    }
}